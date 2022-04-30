using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using Microsoft.Extensions.Configuration;
using static System.DirectoryServices.AccountManagement.UserPrincipal;

namespace PrintShop.Identity
{
    public class AdUserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public AdUserInfo(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _config = configuration;
        }

        private static PrincipalContext Domain => new PrincipalContext(ContextType.Domain, "cityofsalem");
        private UserPrincipal AdUser => FindByIdentity(Domain, UserName);

        private string WindowsUser => _httpContextAccessor.HttpContext?.User.Identity?.Name ??
                                      _httpContextAccessor.HttpContext?.User.Identity?.AuthenticationType;

        public Guid? Guid => AdUser.Guid;
        public string Name => AdUser.Name;
        public string UserName => WindowsUser.Split('\\')[1];

        public IEnumerable<string> AllowedAdGroups => _config.GetSection("AllowedAdGroups").GetChildren()
            .Select(group => group.Value);

        public static string GetNameByUserName(string username)
        {
            if (username == null) return null;
            var currentUser = FindByIdentity(Domain, username);
            return currentUser == null ? "" : currentUser.ToString();
        }

        public static string GetUserNameByName(string name)
        {
            if (name == null) return null;
            var currentUser = FindByIdentity(Domain, name);
            return currentUser == null ? "" : currentUser.ToString();
        }

        private GroupPrincipal GetGroup(string groupName)
        {
            return !string.IsNullOrWhiteSpace(groupName) ? GroupPrincipal.FindByIdentity(Domain, groupName) : null;
        }

        // Return true if the user is not null and the user is a member of the group
        public bool IsInGroup(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName)) return false;

            var group = GetGroup(groupName);
            return AdUser != null && group != null && AdUser.IsMemberOf(group);
        }
    }
}
