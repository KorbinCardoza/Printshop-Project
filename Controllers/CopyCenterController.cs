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
    public class CopyCenterController : Controller
    {
        private static List<Product> _productslist;
        private static List<CopyCenter> _CopyCenterList;
        private readonly PrintshopContext _context;
        private readonly ICopyCenterServices _services;
        public CopyCenterController(PrintshopContext context, ICopyCenterServices services)
        {
            _context = context;
            _services = services;
        }
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Index()
        {         
            var workOrders = _context.WorkOrders.Where(m => m.DeptName != "" && m.WoCreateDt.Date == DateTime.Today.Date).ToList();         
            return View(workOrders);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public async Task<IActionResult> Index(DateTime tripstart, DateTime tripend)
        {
            var start = tripstart.Date.ToString("M-d-yyyy");
            var end = tripend.Date.ToString("M-d-yyyy");
            if (tripend.Date.ToString("M-d-yyyy") != "1-1-0001" && tripstart.Date.ToString("M-d-yyyy") != "1-1-0001")
            {
                var mailitems = _context.WorkOrders
                    .Where(m => m.DeptName != ""
                    && m.WoCreateDt.Date >= tripstart.Date
                    && m.WoCreateDt.Date <= tripend.Date).ToList();
                return View(mailitems);
            }
            if(tripend.Date.ToString("M-d-yyyy") != "1-1-0001" && tripstart.Date.ToString("M-d-yyyy") == "1-1-0001")
            {
                var mailitems = _context.WorkOrders
                 .Where(m => m.DeptName != "" && m.WoCreateDt.Date <= tripend.Date).ToList();
                return View(mailitems);
            }
            if (tripend.Date.ToString("M-d-yyyy") == "1-1-0001" && tripstart.Date.ToString("M-d-yyyy") != "1-1-0001")
            {
                var mailitems = _context.WorkOrders
                 .Where(m => m.DeptName != "" && m.WoCreateDt.Date >= tripstart.Date).ToList();
                return View(mailitems);
            }
            return View();
        }

        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var vm = new CopyCenterViewModel();
            decimal? totalCost = 0;
            var copyCenters = _context.CopyCenters.Where(m => m.WoPkey == id).ToList();
            vm.workOrder = _context.WorkOrders.Where(m => m.WoPkey == id).FirstOrDefault();
            vm.copyCenters = _context.CopyCenters.Where(c => c.WoPkey == id).ToList();
            foreach (var copy in copyCenters)
            {
                totalCost += copy.ExtendedCost; 
            }           
            for (int i = 0; i < vm.copyCenters.Count; i++)
            {
                vm.totalCost += (decimal)vm.copyCenters[i].ExtendedCost;
            }           
            vm.workOrder.WoCopyCntrLaborChg = (decimal?)Math.Round(Convert.ToDouble(vm.workOrder.WoCopyCntrLaborChg), 2);
            vm.laborCharge = (decimal)vm.workOrder.WoCopyCntrLaborChg * (decimal)vm.workOrder.PressLaborHrs;
            vm.totalCost += vm.laborCharge;
            return View(vm);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<IActionResult> Create(CopyCenterCreateViewModel vm)
        {
            var task = vm.Task;
            vm = _services.CreateViewModel(_context).Result;
            vm.Task = task;
            if (_CopyCenterList == null)
            { _CopyCenterList = new List<CopyCenter>();}

            if (_productslist == null)
            { _productslist = new List<Product>();}

            if (vm.Tasks == null)
            {vm.Tasks = new List<SelectListItem>();}

            if (_productslist != null)
            {vm.products = _productslist;}

            if (_CopyCenterList != null)
            {vm.copyCenters = _CopyCenterList;}

            return View("Create",vm);
        }
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public async Task<IActionResult> Add([Bind("DAlias,Account,Dept,Project,Task,CostCenter,name,Phone,JobDescription,Note,ProductNames,Size,Product_Description,Qty,products,UnitTypeList,press_labor_hours,wo_nmbr,copyCenters")] CopyCenterCreateViewModel vm)
        {
            var workOrder = _services.CreateWO(vm, _context);
            await _context.AddAsync(workOrder);
            await _context.SaveChangesAsync();
            var productList = new List<Product>();
            var CurrentWorkOrder = _context.WorkOrders.Where(m => m.WoCreateDt == workOrder.WoCreateDt && m.WoNmbr == workOrder.WoNmbr).First();

            foreach (var product in vm.products)
            {
                var formattedProduct = _context.Products.Where(m => m.ProductPkey == product.ProductPkey).FirstOrDefault();
                productList.Add(formattedProduct);
            }          
            foreach (var p in productList)
            {
                var impress = vm.copyCenters.Where(m => m.ProductPkey == p.ProductPkey).FirstOrDefault().OriginalQty * vm.copyCenters.Where(m => m.ProductPkey == p.ProductPkey).FirstOrDefault().CopiesQty;
                CopyCenter cc = new CopyCenter()
                {
                    WoPkey = CurrentWorkOrder.WoPkey,
                    UnitCost = p.UnitCost,
                    CopiesQty = vm.copyCenters.Where(m => m.ProductPkey == p.ProductPkey).FirstOrDefault().CopiesQty, 
                    OriginalQty = vm.copyCenters.Where(m => m.ProductPkey == p.ProductPkey).FirstOrDefault().OriginalQty, 
                    ExtendedCost = p.UnitCost * impress,
                    StockSize = p.StockSize,
                    ProductDesc = p.ProductDesc,
                    ProductWeight = p.ProductWeight,
                    ProductCategory = p.ProductCategory,
                    ProductType = p.ProductType,
                    ProductPkey = p.ProductPkey,
                    PressLstUpdateDt = DateTime.Now,
                    CreateDt = DateTime.Now,
                    ImpressionCnt = impress
                };
                await _context.AddAsync(cc);
                await _context.SaveChangesAsync();
            }           
            return View();
        }
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Update([Bind("DAlias,Account,Dept,Project,Task,CostCenter,name,Phone,copyCenters,JobDescription,Note,ProductNames,Size,Product_Description,Qty,products,Unit_Type,press_labor_hours,wo_nmbr,Copies")] CopyCenterCreateViewModel vm)
        {            
            var products = _context.Products
                .Where(m => m.ProductCategory == vm.ProductNames && m.StockSize == vm.Size && m.ProductDesc == vm.Product_Description && m.UnitType == vm.Unit_Type)
                .FirstOrDefault();
            products.ProductQuantity = vm.Qty;
            var preFormatUnt = Convert.ToDouble(products.UnitCost);
            products.UnitCost = (decimal?)Math.Round(preFormatUnt, 2);
            _productslist.Add(products);

            var CopyCenter = new CopyCenter();
            CopyCenter.CopiesQty = vm.Copies;
            CopyCenter.OriginalQty = vm.Qty;
            CopyCenter.ProductPkey = products.ProductPkey;
            CopyCenter.UnitCost = products.UnitCost;
            CopyCenter.StockSize = products.StockSize;
            CopyCenter.ProductDesc = products.ProductDesc;
            CopyCenter.ProductWeight = products.ProductWeight;
            CopyCenter.ProductType = products.ProductType;
            CopyCenter.ProductCategory = products.ProductCategory;
            CopyCenter.PressLstUpdateDt = (DateTime)products.ProductUpdateDt;
            CopyCenter.CreateDt = (DateTime)products.ProductCreateDt;
            CopyCenter.ImpressionCnt = vm.Copies * vm.Qty;
            CopyCenter.ExtendedCost = CopyCenter.UnitCost * CopyCenter.ImpressionCnt;
            _CopyCenterList.Add(CopyCenter);

            return Create(vm);
        }
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Bill([Bind("totalCost,laborCharge,copyCenters,workOrder")] CopyCenterViewModel vm)
        {
           FimsBilling Billing = _services.CreateBilling(vm);
           await _context.AddAsync(Billing);
           await _context.SaveChangesAsync();
           var workOrder = _context.WorkOrders.Where(m => m.WoPkey == vm.workOrder.WoPkey).FirstOrDefault();
           workOrder.BillDt = DateTime.Now;
           _context.Update(workOrder);
           await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = vm.workOrder.WoPkey });
        }
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateWorkOrder([Bind("totalCost,laborCharge,copyCenters,workOrder")] CopyCenterViewModel vm)
        {
            vm.workOrder = _context.WorkOrders.Where(m => m.WoPkey == vm.workOrder.WoPkey).FirstOrDefault();            
            CopyCenterCreateViewModel CreateVM = _services.CreateViewModel(_context).Result;

            if (_CopyCenterList == null)
            { _CopyCenterList = new List<CopyCenter>();}

            if (_productslist == null)
            {_productslist = new List<Product>();}

            if (CreateVM.Tasks == null)
            {CreateVM.Tasks = new List<SelectListItem>();}

            if (_productslist != null)
            {CreateVM.products = _productslist;}

            if (_CopyCenterList != null)
            {CreateVM.copyCenters = _CopyCenterList;}

            CreateVM = _services.updateCopyCenterCreateFromWo(CreateVM, vm);
            CreateVM.copyCenters = _context.CopyCenters.Where(m => m.WoPkey == vm.workOrder.WoPkey).ToList();         
            return View(CreateVM);
        }
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> RemoveProduct(int woKey,int pPkey)
        {
            var copyCenter = _context.CopyCenters.Where(m => m.WoPkey == woKey && m.ProductPkey == pPkey).FirstOrDefault();
            var workOrder = _context.WorkOrders.Where(w => w.WoPkey == woKey).FirstOrDefault();
            workOrder.OriginalsQuantity = workOrder.OriginalsQuantity - copyCenter.OriginalQty;
            workOrder.OriginalNmbr = workOrder.OriginalNmbr - copyCenter.CopiesQty;
            var bill = _context.FimsBillings.Where(b => b.BillStockNbr == workOrder.WoNmbr).FirstOrDefault();
            bill.BillRawCost = bill.BillRawCost - (decimal)copyCenter.ExtendedCost;
            bill.BillRawCostRate = bill.BillRawCostRate - (decimal)copyCenter.ExtendedCost;
            _context.Remove(copyCenter);
            _context.Update(workOrder);
            _context.Update(bill);
            await _context.SaveChangesAsync();
            CopyCenterViewModel viewModel = new();
            viewModel.workOrder = new WorkOrder();
            viewModel.workOrder.WoPkey = woKey;
            return await UpdateWorkOrder(viewModel);
        }
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void AddItemsRebill(CopyCenterCreateViewModel vm)
        {

            var products = _context.Products
                .Where(m => m.ProductCategory == vm.ProductNames && m.StockSize == vm.Size && m.ProductDesc == vm.Product_Description && m.UnitType == vm.Unit_Type)
                .FirstOrDefault();
            
            var preFormatUnt = Convert.ToDouble(products.UnitCost);
            products.UnitCost = (decimal?)Math.Round(preFormatUnt, 2);


            var CopyCenter = new CopyCenter();
            CopyCenter.WoPkey = vm.WorkPkey;
            CopyCenter.CopiesQty = vm.Copies;
            CopyCenter.OriginalQty = vm.Qty;
            CopyCenter.ProductPkey = products.ProductPkey;
            CopyCenter.UnitCost = products.UnitCost;
            CopyCenter.StockSize = products.StockSize;
            CopyCenter.ProductDesc = products.ProductDesc;
            CopyCenter.ProductWeight = products.ProductWeight;
            CopyCenter.ProductType = products.ProductType;
            CopyCenter.ProductCategory = products.ProductCategory;
            CopyCenter.PressLstUpdateDt = (DateTime)products.ProductUpdateDt;
            CopyCenter.CreateDt = (DateTime)products.ProductCreateDt;
            CopyCenter.ImpressionCnt = vm.Copies * vm.Qty;
            CopyCenter.ExtendedCost = CopyCenter.UnitCost * CopyCenter.ImpressionCnt;
            await _context.AddAsync(CopyCenter);


            var wo = _context.WorkOrders.Where(w => w.WoPkey == vm.WorkPkey).FirstOrDefault();
            wo.OriginalsQuantity += CopyCenter.OriginalQty;
            wo.OriginalNmbr += CopyCenter.CopiesQty;
            _context.Update(wo);

            if (wo.BillDt != null)
            {
                var bill = _context.FimsBillings.Where(b => b.BillStockNbr == wo.WoNmbr).FirstOrDefault();
                bill.BillQuantity += (int)CopyCenter.ImpressionCnt;
                bill.BillRawCost += (decimal)(CopyCenter.UnitCost * CopyCenter.ImpressionCnt);
                bill.BillRawCostRate += (decimal)(CopyCenter.UnitCost * CopyCenter.ImpressionCnt);
                _context.Update(bill);
            }
            _context.SaveChangesAsync();
          }
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public async Task<IActionResult> Rebill([Bind("DAlias,Account,Dept,Project,Task,CostCenter,name,Phone,JobDescription,Note,ProductNames,Size,Product_Description,Qty,products,UnitTypeList,press_labor_hours,wo_nmbr,copyCenters")] CopyCenterCreateViewModel vm)
        {
            var old_wo = _context.WorkOrders.Where(w => w.WoNmbr == vm.wo_nmbr).FirstOrDefault();
            old_wo.RebillDt = DateTime.Now;
            int? original_nmbr = 0;
            int? original_qty = 0;

            if (vm.copyCenters == null)
                vm.copyCenters = new List<CopyCenter>();


            foreach(var c in vm.copyCenters)
            {
                var product = _context.Products.Where(p => p.ProductPkey == c.ProductPkey).FirstOrDefault();
                var impress = c.OriginalQty * c.CopiesQty;
                original_nmbr += c.CopiesQty;
                original_qty += c.OriginalQty;

                    c.CopiesQty = c.CopiesQty;
                    c.OriginalQty = c.OriginalQty;
                    c.ProductPkey = product.ProductPkey;
                    c.WoPkey = c.WoPkey;
                    c.UnitCost = product.UnitCost;
                    c.ExtendedCost = product.UnitCost * impress;
                    c.StockSize = product.StockSize;
                    c.ProductDesc = product.ProductDesc;
                    c.ProductWeight = product.ProductWeight;
                    c.ProductCategory = product.ProductCategory;
                    c.ProductType = product.ProductType;
                    c.PressLstUpdateDt = DateTime.Now;
                    c.CreateDt = DateTime.Now;
                    c.ImpressionCnt = impress;
                _context.Update(c);
            }

            old_wo = _services.UpdateWorkOrderFromCopyVM(old_wo, vm, original_nmbr, original_qty);
            old_wo.WoCopyCntrLaborChg = Math.Round(Convert.ToDecimal(_context.Systems.FirstOrDefault().CopyCntrLaborCst), 2);
            var old_bill = _context.FimsBillings.Where(b => b.BillStockNbr == old_wo.WoNmbr).FirstOrDefault();
            var reversalBill = old_bill;
            reversalBill.BillPkey = 0;
            reversalBill.BillRawCost = reversalBill.BillRawCost * -1;
            reversalBill.BillRawCostRate = reversalBill.BillRawCostRate * -1;
            FimsBilling Billing = _services.CreateBilling(old_wo,vm);
            _context.Update(old_wo);            
            await _context.AddAsync(reversalBill);
            await _context.AddAsync(Billing);
            await _context.SaveChangesAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminView([Bind("totalCost,laborCharge,copyCenters,workOrder")] CopyCenterViewModel id)
        {
            id.totalCost = 0;
            id.workOrder =  _context.WorkOrders.Where(m => m.WoPkey == id.workOrder.WoPkey).FirstOrDefault();
            
            id.copyCenters =  _context.CopyCenters.Where(c => c.WoPkey == id.workOrder.WoPkey).ToList();           
            id.fims = _context.FimsBillings.Where(f => f.BillStockNbr == id.workOrder.WoNmbr).FirstOrDefault();
            id.CCCViewModel = _services.CreateViewModel(_context).Result;
            foreach(var c in id.copyCenters)
            {
                id.totalCost += (decimal)(c.ImpressionCnt * c.UnitCost);
            }
            return View(id);
        }
        [Authorize(Roles = "Admin")]
        public async void RemoveProductForAdmin(int woKey, int pPkey)
        {
            var copyCenter = _context.CopyCenters.Where(m => m.WoPkey == woKey && m.ProductPkey == pPkey).FirstOrDefault();
             _context.Remove(copyCenter);
            var workOrder = _context.WorkOrders.Where(w => w.WoPkey == woKey).FirstOrDefault();
            workOrder.OriginalsQuantity -= copyCenter.OriginalQty;
            workOrder.OriginalNmbr -= copyCenter.CopiesQty;
             _context.Update(workOrder);

            if (workOrder.BillDt != null)
            {
                var bill = _context.FimsBillings.Where(b => b.BillStockNbr == workOrder.WoNmbr).FirstOrDefault();
                bill.BillQuantity -= (int)copyCenter.ImpressionCnt;
                bill.BillRawCost -= (decimal)(copyCenter.UnitCost * copyCenter.ImpressionCnt);
                bill.BillRawCostRate -= (decimal)(copyCenter.UnitCost * copyCenter.ImpressionCnt);
                _context.Update(bill);
            }
             _context.SaveChangesAsync();          
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void AddForAdmin(CopyCenterViewModel vm)
        {          
            var products = _context.Products
                .Where(m => m.ProductCategory == vm.CCCViewModel.ProductNames && m.StockSize == vm.CCCViewModel.Size && m.ProductDesc == vm.CCCViewModel.Product_Description && m.UnitType == vm.CCCViewModel.Unit_Type)
                .FirstOrDefault();
            products.ProductQuantity = vm.CCCViewModel.Qty;
            var preFormatUnt = Convert.ToDouble(products.UnitCost);
            products.UnitCost = (decimal?)Math.Round(preFormatUnt, 2);
            

            var CopyCenter = new CopyCenter();
            CopyCenter.WoPkey = vm.workOrder.WoPkey;
            CopyCenter.CopiesQty = vm.CCCViewModel.Copies;
            CopyCenter.OriginalQty = vm.CCCViewModel.Qty;
            CopyCenter.ProductPkey = products.ProductPkey;
            CopyCenter.UnitCost = products.UnitCost;
            CopyCenter.StockSize = products.StockSize;
            CopyCenter.ProductDesc = products.ProductDesc;
            CopyCenter.ProductWeight = products.ProductWeight;
            CopyCenter.ProductType = products.ProductType;
            CopyCenter.ProductCategory = products.ProductCategory;
            CopyCenter.PressLstUpdateDt = (DateTime)products.ProductUpdateDt;
            CopyCenter.CreateDt = (DateTime)products.ProductCreateDt;
            CopyCenter.ImpressionCnt = vm.CCCViewModel.Copies * vm.CCCViewModel.Qty;
            CopyCenter.ExtendedCost = CopyCenter.UnitCost * CopyCenter.ImpressionCnt;
            await _context.AddAsync(CopyCenter);
            

            var wo = _context.WorkOrders.Where(w => w.WoPkey == vm.workOrder.WoPkey).FirstOrDefault();
            wo.OriginalsQuantity += CopyCenter.OriginalQty;
            wo.OriginalNmbr += CopyCenter.CopiesQty;
            _context.Update(wo);

            if (wo.BillDt != null)
            {
                var bill = _context.FimsBillings.Where(b => b.BillStockNbr == wo.WoNmbr).FirstOrDefault();
                bill.BillQuantity += (int)CopyCenter.ImpressionCnt;
                bill.BillRawCost += (decimal)(CopyCenter.UnitCost * CopyCenter.ImpressionCnt);
                bill.BillRawCostRate += (decimal)(CopyCenter.UnitCost * CopyCenter.ImpressionCnt);
                _context.Update(bill);
            }
             _context.SaveChangesAsync();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAdmin(CopyCenterViewModel vm)
        {
            var currentWo = _context.WorkOrders.Where(w => w.WoPkey == vm.workOrder.WoPkey).FirstOrDefault();
            currentWo.AcctCode = vm.workOrder.AcctCode;
            currentWo.CostCenter = vm.workOrder.CostCenter;
            currentWo.DeptName = vm.workOrder.DeptName;
            currentWo.ImpessionCount = 0;
            currentWo.JobDescription = vm.workOrder.JobDescription;
            currentWo.LstUpdateDt = DateTime.Now;
            currentWo.Note = vm.workOrder.Note;
            currentWo.OriginalNmbr = 0;
            currentWo.OriginalsQuantity = 0;
            currentWo.Phone = vm.workOrder.Phone;
            currentWo.PressLaborHrs = vm.workOrder.PressLaborHrs;
            currentWo.Project = vm.workOrder.Project;
            currentWo.SenderName = vm.workOrder.SenderName;
            currentWo.Task = vm.workOrder.Task;
            currentWo.WoDeptAlias = vm.workOrder.WoDeptAlias;
            currentWo.WoNmbr = vm.workOrder.WoNmbr;
            _context.Update(currentWo);
            await _context.SaveChangesAsync();

            var ccList = _context.CopyCenters.Where(c => c.WoPkey == vm.workOrder.WoPkey).ToList();
            decimal cost = (decimal)(currentWo.PressLaborHrs * currentWo.WoCopyCntrLaborChg);
            foreach(var c in ccList)
            {
                currentWo.OriginalNmbr += c.OriginalQty;
                currentWo.OriginalsQuantity += c.CopiesQty;
                currentWo.ImpessionCount += (c.CopiesQty * c.OriginalQty);
                cost += (decimal)(c.ImpressionCnt * c.UnitCost);
            }

            if(currentWo.BillDt != null)
            {
                var old_bill = _context.FimsBillings.Where(f => f.BillStockNbr == vm.workOrder.WoNmbr).FirstOrDefault();
                old_bill.BillProjectNumber = currentWo.Project;
                old_bill.BillTaskNumber = currentWo.Task;
                old_bill.BillRawCost = cost;
                old_bill.BillRawCostRate = cost;
                old_bill.BillExpendComment = currentWo.JobDescription;
                old_bill.BillStockNbr = currentWo.WoNmbr;
                old_bill.BillBudget = currentWo.CostCenter;
                old_bill.BillObject = currentWo.AcctCode;
                _context.Update(old_bill);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", new { id = currentWo.WoPkey });
        }

        
    }
}

