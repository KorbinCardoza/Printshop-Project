#nullable disable

namespace PrintShop.Models.Entity
{
    public partial class System
    {
        public int SystemPkey { get; set; }
        public decimal WoPkey { get; set; }
        public decimal? DepartmentPkey { get; set; }
        public decimal? CostCtrPkey { get; set; }
        public decimal? CopyCntrPkey { get; set; }
        public decimal? ProductPkey { get; set; }
        public decimal? DeskTopPkey { get; set; }
        public decimal? ProductTbl { get; set; }
        public decimal? DeskTopTbl { get; set; }
        public decimal? DarkRmTbl { get; set; }
        public decimal? BindTbl { get; set; }
        public decimal? PressTbl { get; set; }
        public decimal? CostctrTbl { get; set; }
        public decimal? CopyCntrTbl { get; set; }
        public decimal? DepartTbl { get; set; }
        public decimal? SchedTbl { get; set; }
        public decimal? BinderyLaborCst { get; set; }
        public decimal? PressScoreCst { get; set; }
        public decimal? PressWashupCst { get; set; }
        public decimal? PressSetupCst { get; set; }
        public decimal? DeskTopLaborCost { get; set; }
        public decimal? PressLaborCst { get; set; }
        public decimal? CopyCntrLaborCst { get; set; }
        public decimal? CopierCost { get; set; }
        public decimal? CopierCostColor { get; set; }
        public decimal? Imagingcost { get; set; }
        public decimal? FilmRollCost { get; set; }
        public decimal? DrAddedLaborCst { get; set; }
        public decimal? DrDoubleBurnCst { get; set; }
        public decimal? DrPlateCst { get; set; }
        public decimal? WoTableNo { get; set; }
        public decimal? MailMeterBegin { get; set; }
        public decimal? MailMeterEnd { get; set; }
        public decimal? PostageDueLimit { get; set; }
        public decimal? UsmailLimit { get; set; }
        public decimal? CopyCenterLimit { get; set; }
        public decimal? CopierQtyLimit { get; set; }
    }
}
