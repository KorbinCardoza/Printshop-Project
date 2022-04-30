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
    public class MailTransactionsController : Controller
    {
        private readonly PrintshopContext _context;
        private readonly string[] _activities = { "US Mail", "FedEx", "UPS", "Postage Due", "MicroFilm", "Special Postage" };
        private readonly string[] _actions = { "Adjust Meter","Bill","Credit","ReBill" };
        public MailTransactionsController(PrintshopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> UpdateMailMeter()
        {
            var system = await _context.Systems.FirstOrDefaultAsync();
            var vm = new MailMeterViewModel
            {
                SystemId = system.SystemPkey,
                CurrentMailMeter = system.MailMeterBegin.GetValueOrDefault(),
                NewMailMeter = system.MailMeterBegin.GetValueOrDefault()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMailMeter(
            [Bind("SystemId,CurrentMailMeter,NewMailMeter,ReceiptNumber,AdjustmentAmount")] MailMeterViewModel mailMeter)
        {
            
            if (!ModelState.IsValid) return View(mailMeter);

            var previousMeter = await _context.Systems.FirstOrDefaultAsync();
            previousMeter.MailMeterBegin += mailMeter.AdjustmentAmount;
            _context.Update(previousMeter);

            await _context.MailTrans.AddAsync(new MailTran(mailMeter, previousMeter));
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            ViewBag.Actions = _actions.ToList();
            ViewBag.Activities = _activities.ToList();


            var mailitems = _context.MailTrans
                .Where(m => m.TransDt.Date >= DateTime.Today && _activities.Contains(m.Activity)).ToList();


            return View(mailitems);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string action, string Activ,DateTime tripstart, DateTime tripend)
        {
            // 1/1/0001

            ViewBag.Actions = _actions.ToList();
            ViewBag.Activities = _activities.ToList();
            if (tripend.Date.ToString("M-d-YYYY") != "1-1-YYYY" && tripstart.Date.ToString("M-d-YYYY") != "1-1-YYYY")
            {
                if (action == null && Activ == null)
                {
                    var mailitems = _context.MailTrans
                        .Where(m => m.TransDt.Date >= tripstart && m.TransDt.Date <= tripend && _activities.Contains(m.Activity)).ToList();
                    return View(mailitems);
                }
                if (action != null && Activ == null)
                {
                    var mailitems = _context.MailTrans
                        .Where(m => m.TransDt.Date >= tripstart && m.TransDt.Date <= tripend && m.Action == action).ToList();
                    return View(mailitems);
                }
                if (action == null && Activ != null)
                {
                    var mailitems = _context.MailTrans
                        .Where(m => m.TransDt.Date >= tripstart && m.TransDt.Date <= tripend && m.Activity == Activ).ToList();
                    return View(mailitems);
                }
                if (action != null && Activ != null)
                {
                    var mailitems = _context.MailTrans
                        .Where(m => m.TransDt.Date >= tripstart && m.TransDt.Date <= tripend && m.Activity == Activ && m.Action == action).ToList();
                    return View(mailitems);
                }
            }
            else
            {
                if (action == null && Activ == null)
                {
                    var mailitems = _context.MailTrans
                        .Where(m => _activities.Contains(m.Activity)).ToList();
                    return View(mailitems);
                }
                if (action != null && Activ == null)
                {
                    var mailitems = _context.MailTrans
                        .Where(m => m.Action == action).ToList();
                    return View(mailitems);
                }
                if (action == null && Activ != null)
                {
                    var mailitems = _context.MailTrans
                        .Where(m => m.Activity == Activ).ToList();
                    return View(mailitems);
                }
                if (action != null && Activ != null)
                {
                    var mailitems = _context.MailTrans
                        .Where(m => m.Activity == Activ && m.Action == action).ToList();
                    return View(mailitems);
                }
            }

            return View();
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            var projects = await _context.FimsCostCenters
                .Select(m => new SelectListItem { Text = m.ProjectNumber, Value = m.ProjectNumber }).Distinct()
                .ToListAsync();
           
            var vm = new MailTransactionViewModel()
            {
                Activities = _activities.Select(m => new SelectListItem { Text = m, Value = m }).ToList(),
                Projects = projects,
                Tasks = _context.FimsCostCenters
                .Select(m => new SelectListItem { Text = m.TaskNbr, Value = m.ProjectNumber })
                .ToList(),
            };
           
               
                

            
         return View(vm);
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Activity,Project,Task,CostCenter,Cost")] MailTransactionViewModel vm)
        {
            //Start needs to be higher than end
            var a = vm.Activity;
            if (!ModelState.IsValid) return View(vm);
            var previousMeter = await _context.Systems.FirstOrDefaultAsync();
            var endMeter = previousMeter.MailMeterBegin - vm.Cost;
            var currentDate = DateTime.Now;
            MailTran newMailTransactionn = new MailTran(vm, previousMeter, endMeter, currentDate);

            await _context.AddAsync(new MailTran(vm, previousMeter, endMeter, currentDate));

            var fimsBill = new FimsBilling();
            var billObj = vm.Activity == "Microfilm" ? "53852" : "52120";
            fimsBill.CreateMailRoomBill(vm, currentDate, billObj);
            await _context.AddAsync(fimsBill);

            previousMeter.MailMeterBegin = endMeter;
            _context.Update(previousMeter);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
