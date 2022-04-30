using System;

#nullable disable

namespace PrintShop.Models.Entity
{
    public partial class Product
    {
        public int ProductPkey { get; set; }
        public string ProductCategory { get; set; }
        public string StockSize { get; set; }
        public string ProductDesc { get; set; }
        public string ProductWeight { get; set; }
        public string ProductVendor { get; set; }
        public string ProductItemNmbr { get; set; }
        public decimal? ProductVendorPrice { get; set; }
        public string ProductType { get; set; }
        public decimal? UnitCost { get; set; }
        public string PressDrType { get; set; }
        public int? ProductQuantity { get; set; }
        public bool? ProductActive { get; set; }
        public string UnitType { get; set; }
        public DateTime? ProductUpdateDt { get; set; }
        public DateTime? ProductCreateDt { get; set; }
    }
}
