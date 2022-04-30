using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrintShop.Models.Entity;
using PrintShop.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace PrintShop.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class CopiersOfficeController : Controller
    {
        private readonly PrintshopContext _context;
        private readonly string[] _actions = { "Adjust Meter", "Bill", "Credit", "ReBill" };
        private readonly string[] _colors = { "Black/White", "Color" };
        public CopiersOfficeController(PrintshopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Actions = _actions.ToList();

            return View(await _context.MailTrans
               .Where(m => m.Activity == "Copiers" && m.TransDt.Date == DateTime.Today)
               .ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string action,DateTime tripstart, DateTime tripend)
        {
            ViewBag.Actions = _actions.ToList();

            if (tripend.Date.ToString("M-d-YYYY") != "1-1-YYYY" && tripstart.Date.ToString("M-d-YYYY") != "1-1-YYYY")
            {
                if (action == null)
                {
                    var mailitems = _context.MailTrans
                        .Where(m => m.TransDt.Date >= tripstart && m.TransDt.Date <= tripend && m.Activity == "Copiers").ToList();
                    return View(mailitems);
                }
                if (action != null)
                {
                    var mailitems = _context.MailTrans
                        .Where(m => m.TransDt.Date >= tripstart && m.TransDt.Date <= tripend && m.Action == action && m.Activity == "Copiers").ToList();
                    return View(mailitems);
                }
            }
            else
            {
                if (action == null)
                {
                    var mailitems = _context.MailTrans
                        .Where(m => m.Activity == "Copiers").ToList();
                    return View(mailitems);
                }
                if (action != null)
                {
                    var mailitems = _context.MailTrans
                        .Where(m => m.Action == action && m.Activity == "Copiers").ToList();
                    return View(mailitems);
                }
            }
            return View(await _context.MailTrans
               .Where(m => m.Activity == "Copiers" && m.Action == action)
               .ToListAsync());
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            
            var projects = await _context.FimsCostCenters
                .Select(m => new SelectListItem { Text = m.ProjectNumber, Value = m.ProjectNumber }).Distinct()
                .ToListAsync();
            var vm = new MailTransactionViewModel()
            {
                Projects = projects,
                Tasks = new List<SelectListItem>(),
                Colors = _colors.Select(m => new SelectListItem { Text = m, Value = m }).ToList()
            };

            return View(vm);
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillDate,Quantity,Project,Task,CostCenter,Color")] MailTransactionViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var currentDate = DateTime.Now;
            decimal? charge = 0;
            if (vm.Color == "Color")
                charge = _context.Systems.First().CopierCostColor;
            if (vm.Color == "Black/White")
                charge = _context.Systems.First().CopierCost;
            var copiersBilling = new MailTran();
            copiersBilling.CreateCopiersOfficeBilling(vm, currentDate, charge);
            await _context.AddAsync(copiersBilling);
            await _context.SaveChangesAsync();

            var fimsBill = new FimsBilling();
            fimsBill.CreateCopiersOfficeBilling(vm, currentDate, "53854");
            fimsBill.BillExpendType = "Office Copier";
            fimsBill.BillNonLabor = "Office Copier";
            await _context.AddAsync(fimsBill);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
