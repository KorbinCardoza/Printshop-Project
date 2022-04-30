using PrintShop.Models.Entity;
using System;
using System.Collections.Generic;

namespace PrintShop.ViewModels
{
    public class CopyCenterViewModel
    {
        public decimal totalCost { get; set; }
        public decimal laborCharge { get; set; }
        public DateTime? woBillDt { get; set; }
        public List<CopyCenter> copyCenters { get; set; }
        public WorkOrder workOrder { get; set; }
        public FimsBilling fims { get; set; }
        public CopyCenterCreateViewModel CCCViewModel { get; set; }
    }
}
