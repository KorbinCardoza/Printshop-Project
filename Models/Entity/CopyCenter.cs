using System;

#nullable disable

namespace PrintShop.Models.Entity
{
    public partial class CopyCenter
    {
        public int CopyCntrPkey { get; set; }
        public int WoPkey { get; set; }
        public int? OriginalQty { get; set; }
        public int? CopiesQty { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? ExtendedCost { get; set; }
        public string StockSize { get; set; }
        public string ProductDesc { get; set; }
        public string ProductWeight { get; set; }
        public string ProductType { get; set; }
        public string ProductCategory { get; set; }
        public string Note { get; set; }
        public int ProductPkey { get; set; }
        public bool? NoteIndicator { get; set; }
        public DateTime PressLstUpdateDt { get; set; }
        public DateTime CreateDt { get; set; }
        public int? ImpressionCnt { get; set; }
    }
}
