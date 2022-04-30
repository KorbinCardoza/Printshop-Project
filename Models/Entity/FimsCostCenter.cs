#nullable disable

namespace PrintShop.Models.Entity
{
    public partial class FimsCostCenter
    {
        public int CostCenterPkey { get; set; }
        public string ProjectNumber { get; set; }
        public string OrganizationName { get; set; }
        public string TaskNbr { get; set; }
        public string TaskName { get; set; }
        public string CostCntr { get; set; }
        public string CcName { get; set; }
    }
}
