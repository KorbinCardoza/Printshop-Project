using System;
using PrintShop.ViewModels;

#nullable disable

namespace PrintShop.Models.Entity
{
    public partial class FimsBilling
    {
        public int BillPkey { get; set; }
        public string BillTransSource { get; set; }
        public DateTime BillBatchName { get; set; }
        public DateTime BillExpendEndingDate { get; set; }
        public DateTime BillExpendItemDate { get; set; }
        public string BillProjectNumber { get; set; }
        public string BillTaskNumber { get; set; }
        public string BillExpendType { get; set; }
        public string BillTransStatus { get; set; }
        public string BillNonLabor { get; set; }
        public string BillNonLaborName { get; set; }
        public decimal BillQuantity { get; set; }
        public decimal BillRawCost { get; set; }
        public decimal BillRawCostRate { get; set; }
        public string BillExpendComment { get; set; }
        public string BillOrigTransRef { get; set; }
        public string BillStockNbr { get; set; }
        public string BillUnitMeasure { get; set; }
        public string BillBudget { get; set; }
        public string BillObject { get; set; }

        public void CreateMailRoomBill(MailTransactionViewModel vm, DateTime currentDate, string billObj)
        {
            BillTransSource = "Mail";
            BillBatchName = currentDate;
            BillExpendEndingDate = currentDate;
            BillExpendItemDate = currentDate;
            BillProjectNumber = vm.Project;
            BillTaskNumber = vm.Task;
            BillExpendType = "Mail Room";
            BillTransStatus = "P";
            BillNonLabor = "Mail Room";
            BillNonLaborName = "City of Salem";
            BillQuantity = 1;
            BillRawCost = vm.Cost;
            BillRawCostRate = vm.Cost;
            BillExpendComment = vm.Activity;
            BillOrigTransRef = "PRT_XFACE";
            BillUnitMeasure = "EA";
            BillBudget = vm.CostCenter;
            BillObject = billObj;
        }

        public void CreateCopiersOfficeBilling(MailTransactionViewModel vm, DateTime currentDate, string billObj)
        {
            BillTransSource = "Photocopies";
            BillBatchName = currentDate;
            BillExpendEndingDate = currentDate;
            BillExpendItemDate = currentDate;
            BillProjectNumber = vm.Project;
            BillTaskNumber = vm.Task;
            BillExpendType = "Office Copier Charges";
            BillTransStatus = "P";
            BillNonLabor = "Office Copier Charges";
            BillNonLaborName = "City of Salem";
            BillQuantity = 1;
            BillRawCost = vm.Cost * vm.Quantity;
            BillRawCostRate = vm.Cost * vm.Quantity;
            BillExpendComment = "Office Copier Charges";
            BillOrigTransRef = "PRT_XFACE";
            BillUnitMeasure = "EA";
            BillBudget = vm.CostCenter;
            BillObject = billObj;
        }
    }
}
