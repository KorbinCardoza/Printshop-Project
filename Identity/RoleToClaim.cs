using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PrintShop.Models.Entity;

namespace PrintShop.Identity
{
    public class RolesToClaim : IClaimsTransformation
    {
        private readonly PrintshopContext _dbContext;
        private readonly AdUserInfo _adUserInfo;
        private bool _isTransformed;
        public RolesToClaim(PrintshopContext dbContext, AdUserInfo adUserInfo)
        {
            _dbContext = dbContext;
            _adUserInfo = adUserInfo;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (_isTransformed) return await Task.FromResult(principal);

            var currentClaimsIdentity = (ClaimsIdentity)principal.Identity;
            var dbUser = await _dbContext.Users.SingleOrDefaultAsync(i => i.Username.Equals(currentClaimsIdentity.Name));

            var claimsIdentity = new ClaimsIdentity(principal.Identity.Name!);
            // Debug always admin
            // claimsIdentity.AddClaim(new Claim(claimsIdentity.RoleClaimType, "Admin"));

            if (dbUser != null)
            {
                claimsIdentity.AddClaim(new Claim(claimsIdentity.RoleClaimType, dbUser.Role.ToString()));
            }

            _isTransformed = true;
            return await Task.FromResult(new ClaimsPrincipal(claimsIdentity));
        }
    }
}

