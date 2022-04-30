using PrintShop.Models.Entity;
using PrintShop.ViewModels;
using System.Threading.Tasks;

namespace PrintShop.Services
{
    public interface ICopyCenterServices
    {
        public  Task<CopyCenterCreateViewModel> CreateViewModel(PrintshopContext context);
        public WorkOrder CreateWO(CopyCenterCreateViewModel vm, PrintshopContext context);
        public FimsBilling CreateBilling(CopyCenterViewModel vm);
        public FimsBilling CreateBilling(WorkOrder old_wo, CopyCenterCreateViewModel vm);
        public CopyCenterCreateViewModel updateCopyCenterCreateFromWo(CopyCenterCreateViewModel CreateVM, CopyCenterViewModel vm);
        public WorkOrder UpdateWorkOrderFromCopyVM(WorkOrder old_wo, CopyCenterCreateViewModel vm, int? original_nmbr, int? original_qty);
    }
}
