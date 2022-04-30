using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PrintShop.ViewModels
{
    public class MailTransactionViewModel
    {
        public string Activity { get; set; }
        public string Project { get; set; }
        public string Task { get; set; }
        [DisplayName("Cost Center")]
        public string CostCenter { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public DateTime BillDate { get; set; }
        public List<SelectListItem> Activities { get; set; }
        public List<SelectListItem> Projects { get; set; }
        public List<SelectListItem> Tasks { get; set; }
        public List<SelectListItem> Colors { get; set; }
        public string Color { get; set; }

    }
}
