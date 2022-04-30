using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrintShop.Models.Entity;
using PrintShop.Services;
using PrintShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintShop.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class ImagingController : Controller
    {
        private static List<Product> _productslist;
        private static List<CopyCenter> _CopyCenterList;
        private readonly PrintshopContext _context;
        private readonly ICopyCenterServices _services;
        private readonly string[] _activities = { "Microfiche/Aperture Card", "Document Imaging", "Film Rolls"};
        public ImagingController(PrintshopContext context, ICopyCenterServices services)
        {
            _context = context;
            _services = services;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewBag.Activities = _activities.ToList();
            var Imagingitems = _context.ImagingTrans
                .Where(m => m.TransDt.Date >= DateTime.Today && m.Action == "Imaging").ToList();


            return View(Imagingitems);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Activ, DateTime tripstart, DateTime tripend)
        {
            if (tripend.Date.ToString("M-d-YYYY") != "1-1-YYYY" && tripstart.Date.ToString("M-d-YYYY") != "1-1-YYYY")
            {
                if (Activ == null)
                {
                    var Imagingitems = _context.ImagingTrans
                        .Where(m => m.TransDt.Date >= tripstart && m.TransDt.Date <= tripend && _activities.Contains(m.Activity)).ToList();
                    return View(Imagingitems);
                }
                if (Activ != null)
                {
                    var Imagingitems = _context.ImagingTrans
                        .Where(m => m.TransDt.Date >= tripstart && m.TransDt.Date <= tripend && m.Activity == Activ).ToList();
                    return View(Imagingitems);
                }
            }
            else
            {
                if (Activ == null)
                {
                    var Imagingitems = _context.ImagingTrans
                        .Where(m => _activities.Contains(m.Activity)).ToList();
                    return View(Imagingitems);
                }
                if (Activ != null)
                {
                    var Imagingitems = _context.ImagingTrans
                        .Where(m => m.Activity == Activ).ToList();
                    return View(Imagingitems);
                }
            }

            return View();
        }
        public async Task<IActionResult> Create()
        {
            var projects = await _context.FimsCostCenters
                .Select(m => new SelectListItem { Text = m.ProjectNumber, Value = m.ProjectNumber }).Distinct()
                .ToListAsync();

            var vm = new ImagingViewModel()
            {
                Activities = _activities.Select(m => new SelectListItem { Text = m, Value = m }).ToList(),
                Projects = projects,
                Tasks = _context.FimsCostCenters
                .Select(m => new SelectListItem { Text = m.TaskNbr, Value = m.ProjectNumber })
                .ToList(),
            };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Activity,Project,Task,CostCenter,Quantity")] ImagingViewModel vm)
        {
            
            if (!ModelState.IsValid) return View(vm);

            ImagingTrans newImagingTransaction = new ImagingTrans
            {
                Action = "Imaging",
                Activity = vm.Activity,
                ProjectNumber = vm.Project,
                TaskNbr = vm.Task,
                CostCntr = vm.CostCenter,
                TransDt = DateTime.Now
            };
            if(newImagingTransaction.Activity == "Film Rolls")           
                newImagingTransaction.Cost = (decimal)_context.Systems.First().FilmRollCost * vm.Quantity;
            else
                newImagingTransaction.Cost = (decimal)_context.Systems.First().Imagingcost * vm.Quantity;
            await _context.AddAsync(newImagingTransaction);

            var fimsBill = new FimsBilling
            {
              BillTransSource = "Imaging",
            BillBatchName = DateTime.Now,
            BillExpendEndingDate = DateTime.Now,
            BillExpendItemDate = DateTime.Now,
            BillProjectNumber = vm.Project,
            BillTaskNumber = vm.Task,
            BillExpendType = "Imaging",
            BillTransStatus = "P",
            BillNonLabor = "Imaging",
            BillNonLaborName = "City of Salem",
            BillQuantity = 1,
            BillRawCost = vm.Cost,
            BillRawCostRate = vm.Cost,
            BillExpendComment = vm.Activity,
            BillOrigTransRef = "PRT_XFACE",
            BillUnitMeasure = "EA",
            BillBudget = vm.CostCenter,
            BillObject = "53853"
            };
          
            
            await _context.AddAsync(fimsBill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
