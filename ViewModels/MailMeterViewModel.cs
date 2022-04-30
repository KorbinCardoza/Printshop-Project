using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PrintShop.ViewModels
{
    public class MailMeterViewModel
    {
        public int SystemId { get; set; }
        [DisplayName("Current Mail Meter")]
        public decimal CurrentMailMeter { get; set; }
        [DisplayName("New Mail Meter")]
        public decimal NewMailMeter { get; set; }
        [DisplayName("Receipt Number")]
        public string ReceiptNumber { get; set; }
        [DisplayName("Adjustment Amount")]
        [Required]
        public decimal AdjustmentAmount { get; set; }
    }
}
