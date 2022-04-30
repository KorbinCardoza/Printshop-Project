using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrintShop.Models.Entity;
using PrintShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintShop.Services
{
    public class CopyCenterServices : ICopyCenterServices
    {



        public async Task<CopyCenterCreateViewModel> CreateViewModel(PrintshopContext context)
        {
            CopyCenterCreateViewModel CreateVM = new();
            CreateVM.DAliasList = await context.DeptAliases
               .Select(m => new SelectListItem { Text = m.Alias, Value = m.Alias }).Distinct()
               .ToListAsync();
            CreateVM.AccountList = await context.Accounts
                .Select(m => new SelectListItem { Text = m.Account1.ToString(), Value = m.Account1.ToString() }).Distinct()
                .ToListAsync();
            CreateVM.DeptList = await context.WoCostCenterProjTasks
               .Select(m => new SelectListItem { Text = m.DeptName, Value = m.DeptName }).Distinct()
               .ToListAsync();
            CreateVM.Projects = await context.FimsCostCenters
                .Select(m => new SelectListItem { Text = m.ProjectNumber, Value = m.ProjectNumber }).Distinct()
                .ToListAsync();
            CreateVM.ProductNamesList = await context.Products
                .Select(m => new SelectListItem { Text = m.ProductCategory, Value = m.ProductCategory }).Distinct()
                .ToListAsync();

            

            return CreateVM;
        }
        public WorkOrder CreateWO(CopyCenterCreateViewModel vm, PrintshopContext context)
        {
            if (vm.products == null)
                vm.products = new List<Product>();
            int? original_nmbr = 0;
            int? original_qty = 0;
            if (vm.copyCenters != null)
            {
                foreach (var c in vm.copyCenters)
                {
                    original_nmbr += c.CopiesQty;
                    original_qty += c.OriginalQty;
                }
            }

            var workOrder = new WorkOrder()
            {
                WoNmbr = vm.wo_nmbr,
                DeptName = vm.Dept,
                JobDescription = vm.JobDescription,
                SenderName = vm.name,
                Phone = vm.Phone,
                CostCenter = vm.CostCenter,
                AcctCode = vm.Account,
                Task = vm.Task,
                Project = vm.Project,
                Note = vm.Note,
                OrderDate = DateTime.Now,
                WoCreateDt = DateTime.Now,
                LstUpdateDt = DateTime.Now,
                OriginalNmbr = original_nmbr,
                OriginalsQuantity = original_qty,
                WoDeptAlias = vm.DAlias,
                PressLaborHrs = Convert.ToDecimal(vm.press_labor_hours),
                WoType = "C",
                WoCopyCntrLaborChg = Math.Round(Convert.ToDecimal(context.Systems.FirstOrDefault().CopyCntrLaborCst), 2),

                
            };
            return workOrder;
        }

        public FimsBilling CreateBilling(CopyCenterViewModel vm)
        {
            FimsBilling Billing = new FimsBilling()
            {

                BillTransSource = "Copy",
                BillBatchName = DateTime.Now,
                BillExpendEndingDate = DateTime.Now,
                BillExpendItemDate = DateTime.Now,
                BillProjectNumber = vm.workOrder.Project,
                BillTaskNumber = vm.workOrder.Task,
                BillExpendType = "Copy Center",
                BillTransStatus = "P",
                BillNonLabor = "Copy Center",
                BillNonLaborName = "City of Salem",
                BillQuantity = 1,
                BillRawCost = vm.totalCost,
                BillRawCostRate = vm.totalCost,
                BillExpendComment = vm.workOrder.JobDescription,
                BillOrigTransRef = "PRT_XFACE",
                BillStockNbr = vm.workOrder.WoNmbr,
                BillUnitMeasure = "EA",
                BillBudget = vm.workOrder.CostCenter,
                BillObject = vm.workOrder.AcctCode
            };
            return Billing;
        }
        public FimsBilling CreateBilling(WorkOrder old_wo, CopyCenterCreateViewModel vm)
        {
            int? original_nmbr = 0;
            int? original_qty = 0;
            decimal totalCost = 0;

            totalCost += (decimal)vm.press_labor_hours * (decimal)old_wo.WoCopyCntrLaborChg;
            if(vm.copyCenters != null) { 
            foreach (var c in vm.copyCenters)
            {
                totalCost += (decimal)c.ImpressionCnt * (decimal)c.UnitCost;              
            }
            }
            FimsBilling Billing = new FimsBilling()
            {

                BillTransSource = "Copy",
                BillBatchName = DateTime.Now,
                BillExpendEndingDate = DateTime.Now,
                BillExpendItemDate = DateTime.Now,
                BillProjectNumber = old_wo.Project,
                BillTaskNumber = old_wo.Task,
                BillExpendType = "Copy Center",
                BillTransStatus = "P",
                BillNonLabor = "Copy Center",
                BillNonLaborName = "City of Salem",
                BillRawCost = totalCost,
                BillRawCostRate = totalCost,
                BillQuantity = 1,
                BillExpendComment = old_wo.JobDescription,
                BillOrigTransRef = "PRT_XFACE",
                BillStockNbr = old_wo.WoNmbr,
                BillUnitMeasure = "EA",
                BillBudget = old_wo.CostCenter,
                BillObject = old_wo.AcctCode
            };
            return Billing;
        }
        public CopyCenterCreateViewModel updateCopyCenterCreateFromWo(CopyCenterCreateViewModel CreateVM, CopyCenterViewModel vm)
        {
            CreateVM.WorkPkey = vm.workOrder.WoPkey;
            CreateVM.Account = vm.workOrder.AcctCode;

            CreateVM.CostCenter = vm.workOrder.CostCenter;
            CreateVM.Dept = vm.workOrder.DeptName;
            CreateVM.JobDescription = vm.workOrder.JobDescription;
            CreateVM.Note = vm.workOrder.Note;
            CreateVM.Phone = vm.workOrder.Phone;
            CreateVM.press_labor_hours = (double)vm.workOrder.PressLaborHrs;
            CreateVM.Project = vm.workOrder.Project;
            CreateVM.name = vm.workOrder.SenderName;
            CreateVM.Task = vm.workOrder.Task;
            CreateVM.DAlias = vm.workOrder.WoDeptAlias;
            CreateVM.wo_nmbr = vm.workOrder.WoNmbr;

            return CreateVM;
        }
        public WorkOrder UpdateWorkOrderFromCopyVM(WorkOrder old_wo, CopyCenterCreateViewModel vm, int? original_nmbr, int? original_qty)
        {
            old_wo.WoNmbr = vm.wo_nmbr;
            old_wo.DeptName = vm.Dept;
            old_wo.JobDescription = vm.JobDescription;
            old_wo.SenderName = vm.name;
            old_wo.Phone = vm.Phone;
            old_wo.CostCenter = vm.CostCenter;
            old_wo.AcctCode = vm.Account;
            old_wo.Task = vm.Task;
            old_wo.Project = vm.Project;
            old_wo.Note = vm.Note;
            old_wo.LstUpdateDt = DateTime.Now;
            old_wo.OriginalNmbr = original_nmbr;
            old_wo.OriginalsQuantity = original_qty;
            old_wo.WoDeptAlias = vm.DAlias;
            old_wo.PressLaborHrs = Convert.ToDecimal(vm.press_labor_hours);
            return old_wo;
        }
    }
}
