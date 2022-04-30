using Microsoft.AspNetCore.Mvc.Rendering;
using PrintShop.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PrintShop.ViewModels
{
    public class CopyCenterCreateViewModel
    {
        public int WorkPkey { get; set; }
        public List<SelectListItem> DAliasList { get; set; }
        public string DAlias { get; set; }
        public List<SelectListItem> AccountList { get; set; }
        public string Account { get; set; }
        public List<SelectListItem> DeptList { get; set; }
        public string Dept { get; set; }
        public List<SelectListItem> Projects { get; set; }
        public string Project { get; set; }
        public List<SelectListItem> Tasks { get; set; }
        public string Task { get; set; }
        public string CostCenter { get; set; }

        public string name { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
        public string JobDescription { get; set; }
        public string Note { get; set; }
        public List<SelectListItem> ProductNamesList { get; set; }
        public List<SelectListItem> ProductSizes { get; set; }
        [DisplayName("Type")]
        public string ProductNames { get; set; }
        [DisplayName("Size")]
        public string Size { get; set; }
        [DisplayName("Description")]
        public string Product_Description { get; set; }
        [DisplayName("Unit Type")]
        public string Unit_Type { get; set; }
        public List<SelectListItem> ProductDescriptionList { get; set; }
        public List<SelectListItem> UnitTypeList { get; set; }
        public int Copies { get; set; }
        public int Qty { get; set; }
        public List<Product> products { get; set; }
        public List<CopyCenter> copyCenters { get; set; }
        [DisplayName("Labor Hours")]
        public double press_labor_hours { get; set; }
  
        
       
        [Required(ErrorMessage = "Please enter the Form number")]
        public string wo_nmbr { get; set; }
    }
}
