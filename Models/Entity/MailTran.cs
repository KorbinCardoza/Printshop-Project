using System;
using PrintShop.ViewModels;

#nullable disable

namespace PrintShop.Models.Entity
{
    public partial class MailTran
    {
        public MailTran()
        {
        }

        public MailTran(MailMeterViewModel model, System previousMeter)
        {
            Action = "Adjust Meter";
            Activity = "Meter";
            MailMeterBegin = previousMeter.MailMeterBegin;
            MailMeterEnd = previousMeter.MailMeterBegin - model.AdjustmentAmount;
            Cost = model.AdjustmentAmount;
            ReceiptNumber = model.ReceiptNumber;
            TransDt = DateTime.Now;
        }

        public MailTran(MailTransactionViewModel vm, System previousMeter, decimal? newMeter, DateTime currentDate)
        {
            Action = "Bill";
            Activity = vm.Activity;
            TaskNbr = vm.Task;
            CostCntr = vm.CostCenter;
            Cost = vm.Cost;
            TransDt = currentDate;
            MailMeterBegin = previousMeter.MailMeterBegin;
            MailMeterEnd = newMeter;
            ProjectNumber = vm.Project;
        }

        public void CreateCopiersOfficeBilling(MailTransactionViewModel vm, DateTime currentDate, decimal? charge)
        {
            Action = "Bill";
            Activity = "Copiers";
            TaskNbr = vm.Task;
            CostCntr = vm.CostCenter;
            Cost = (decimal)(vm.Quantity * charge);           
            TransDt = currentDate;
            MailMeterBegin = 0;
            MailMeterEnd = 0;
            ProjectNumber = vm.Project;
            BillDt = vm.BillDate;
        }

        public int MailPkey { get; set; }
        public string Action { get; set; }
        public string Activity { get; set; }
        public string ProjectNumber { get; set; }
        public string TaskNbr { get; set; }
        public string CostCntr { get; set; }
        public decimal? MailMeterBegin { get; set; }
        public decimal? MailMeterEnd { get; set; }
        public decimal Cost { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime? ReBillDt { get; set; }
        public int? RelPkey { get; set; }
        public DateTime TransDt { get; set; }
        public DateTime? BillDt { get; set; }
    }
}
