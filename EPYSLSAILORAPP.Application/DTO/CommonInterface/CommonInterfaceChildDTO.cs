using EPYSLSAILORAPP.Domain.Entity.CommonInterface;

namespace EPYSLSAILORAPP.Application.DTO.CommonInterface
{
    public class CommonInterfaceChildDTO : CommonInterfaceChild
    {
        public CommonInterfaceChildDTO()
        {
            Scale = 0;
            IsSys = false;
            IsRequired = false;
            IsHidden = true;
            IsEnabled = true;
            HasFinder = false;
            HasSelectionChangeMethod = false;
            HasDefault = false;
        }
    }
}
