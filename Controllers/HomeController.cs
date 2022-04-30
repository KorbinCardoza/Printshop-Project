using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrintShop.Identity;
using PrintShop.Models.Entity;
using PrintShop.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace PrintShop.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class HomeController : Controller
    {
        private readonly PrintshopContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly AdUserInfo _adUserInfo;
        public HomeController(ILogger<HomeController> logger, AdUserInfo adUserInfo, PrintshopContext context)
        {
            _logger = logger;
            _adUserInfo = adUserInfo;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public string GetUserName()
        {
            try
            {
                return _adUserInfo.Name;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
       
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<PartialViewResult> GetProjectsByDeptName(string deptName)
        {
            var projects = await _context.FimsCostCenters
                .Where(m => m.OrganizationName == deptName)
                .Select(i => new SelectListItem { Text = i.ProjectNumber, Value = i.ProjectNumber })
                .Distinct()
                .ToListAsync();

            var model = new MailTransactionViewModel
            {
                Projects = projects
            };
            return PartialView("_ProjectsDropdown", model);
        }
        public async Task<PartialViewResult> GetSizesByProduct(string productType)
        {
            var sizes = await _context.Products
                .Where(m => m.ProductCategory == productType)
                .Select(i => new SelectListItem { Text = i.StockSize, Value = i.StockSize })
                .Distinct()
                .ToListAsync();

            var model = new CopyCenterCreateViewModel
            {
                ProductSizes = sizes
            };
            return PartialView("_SizeDropdown", model);
        }
        public async Task<PartialViewResult> UpdateTotal(int woKey)
        {
            decimal total = 0;
            var copyCenters = _context.CopyCenters.Where(c => c.WoPkey == woKey).ToList();
            foreach (var copy in copyCenters)
            {
                total += (decimal)(copy.UnitCost * copy.ImpressionCnt);
            }
            

            var model = new CopyCenterViewModel
            {
                totalCost = total
            };
            return PartialView("_TotalCost", model);
        }
        public async Task<PartialViewResult> HiddenProject(string projectId)
        {
            var tasks = await _context.FimsCostCenters
                 .Where(m => m.TaskNbr == projectId)
                 .Select(i => new SelectListItem { Text = i.TaskNbr, Value = i.TaskNbr })
                 .ToListAsync();

            var model = new CopyCenterCreateViewModel
            {
                Tasks = tasks
            };
            return PartialView("_TasksDropdown", model);
        }
        [HttpGet]
        public async Task<PartialViewResult> Test(CopyCenterCreateViewModel data)
        {


            var model = data;

            return PartialView("_SizeDropdown", model);
        }
        public Product AddProduct(string productType, string size, string desc, string qty)
        {
            var product = _context.Products
                .Where(m => m.ProductCategory == productType && m.StockSize == size && m.ProductDesc == desc)
                .FirstOrDefault();

            product.ProductQuantity = Convert.ToInt32(qty);

            return product;
        }
        public async Task<PartialViewResult> GetDescByProductandSize(string product, string size)
        {
            var sizes = await _context.Products
                .Where(m => m.ProductCategory == product && m.StockSize == size)
                .Select(i => new SelectListItem { Text = i.ProductDesc, Value = i.ProductDesc })
                .Distinct()
                .ToListAsync();

            var model = new CopyCenterCreateViewModel
            {
                ProductDescriptionList = sizes
            };
            return PartialView("_DescDropdown", model);
        }
        public async Task<PartialViewResult> GetUnit(string product, string size, string desc)
        {
            var units = await _context.Products
                .Where(m => m.ProductCategory == product && m.StockSize == size && m.ProductDesc == desc)
                .Select(i => new SelectListItem { Text = i.UnitType, Value = i.UnitType })
                .Distinct()
                .ToListAsync();

            var model = new CopyCenterCreateViewModel
            {
                UnitTypeList = units
            };
            return PartialView("_UnitTypeDropdown", model);
        }
        public async Task<PartialViewResult> GetTasksByProjectId(string projectId)
        {
            var tasks = await _context.FimsCostCenters
                .Where(m => m.ProjectNumber == projectId)
                .Select(i => new SelectListItem { Text = i.TaskNbr, Value = i.TaskNbr })
                .ToListAsync();

            var model = new MailTransactionViewModel
            {
                Tasks = tasks
            };
            return PartialView("_TasksDropdown", model);
        }

        public async Task<string> GetCostCenterByProjectIdTaskId(string projectId, string taskId)
        {
            var costCenter = await _context.FimsCostCenters
                .FirstOrDefaultAsync(m => m.ProjectNumber == projectId && m.TaskNbr == taskId);
            return costCenter?.CostCntr;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
