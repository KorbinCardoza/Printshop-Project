using System;
using PrintShop.ViewModels;

#nullable disable

namespace PrintShop.Models.Entity
{
    public partial class ImagingTrans
    {
        public ImagingTrans()
        {
        }
        public int Imaging_Pkey { get; set; }
        public string Action { get; set; }
        public string Activity { get; set; }
        public string ProjectNumber { get; set; }
        public string TaskNbr { get; set; }
        public string CostCntr { get; set; }
        public decimal Cost { get; set; }
        public DateTime TransDt { get; set; }
    }
}