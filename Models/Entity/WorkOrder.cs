using System;

#nullable disable

namespace PrintShop.Models.Entity
{
    public partial class WorkOrder
    {
        public int WoPkey { get; set; }
        public string WoNmbr { get; set; }
        public string DeptName { get; set; }
        public string JobDescription { get; set; }
        public string SenderName { get; set; }
        public string Phone { get; set; }
        public string CostCenter { get; set; }
        public string AcctCode { get; set; }
        public string Task { get; set; }
        public string Project { get; set; }
        public string DeptPh { get; set; }
        public string Note { get; set; }
        public string RebillWo { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? OriginalNmbr { get; set; }
        public decimal? OriginalsQuantity { get; set; }
        public decimal? WashUpUnitCost { get; set; }
        public decimal? SetUpUnitCost { get; set; }
        public DateTime? BillDt { get; set; }
        public DateTime? RebillDt { get; set; }
        public decimal? DrAddLabor { get; set; }
        public decimal? PressLaborHrs { get; set; }
        public decimal? DrPlateQty { get; set; }
        public decimal? DrDoubleBurnQty { get; set; }
        public decimal? SetupQty { get; set; }
        public decimal? WashupQty { get; set; }
        public decimal? ScoreQty { get; set; }
        public string PanToneColorOrder1 { get; set; }
        public string PanToneColorOrder2 { get; set; }
        public string PanToneColorOrder3 { get; set; }
        public string PanToneColorOrder4 { get; set; }
        public string PanToneColorOrder5 { get; set; }
        public string BillReversalInd { get; set; }
        public bool? ReadyBillInd { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public decimal? ImpessionCount { get; set; }
        public decimal? PlateFileNmbr { get; set; }
        public DateTime? DtAppovalDate { get; set; }
        public string DtApprovalName { get; set; }
        public string DtScans { get; set; }
        public decimal? DtHours { get; set; }
        public DateTime? DtFinalDueDate { get; set; }
        public DateTime? DtFirstProofDate { get; set; }
        public DateTime? DtDptDate { get; set; }
        public DateTime LstUpdateDt { get; set; }
        public DateTime WoCreateDt { get; set; }
        public string WoType { get; set; }
        public string CopyCntrDrill { get; set; }
        public string CopyCntrStaple { get; set; }
        public string CopyCntrPunch { get; set; }
        public string CopyCntrPrint { get; set; }
        public string CopyCntrCollate { get; set; }
        public string WoDeptAlias { get; set; }
        public decimal? WoCopyCntrLaborChg { get; set; }
    }
}
