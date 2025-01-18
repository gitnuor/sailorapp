using EPYSLSAILORAPP.Domain.Entity.CommonInterface;

namespace EPYSLSAILORAPP.Application.DTO.CommonInterface
{
    public class CommonInterfaceMasterDTO : CommonInterfaceMaster
    {
        #region Additional Fields

        public List<CommonInterfaceChild> Childs { get; set; }

        public List<CommonInterfaceChildGrid> ChildGrids { get; set; }

        public List<CommonInterfaceChildGridColumn> ChildGridColumns { get; set; }

        #endregion Additional Fields

        public CommonInterfaceMasterDTO()
        {
            IsInsertAllow = true;
            IsUpdateAllow = true;
            IsDeleteAllow = true;
            HasGrid = false;
            Childs = new List<CommonInterfaceChild>();
            ChildGrids = new List<CommonInterfaceChildGrid>();
            ChildGridColumns = new List<CommonInterfaceChildGridColumn>();
        }
    }
}
