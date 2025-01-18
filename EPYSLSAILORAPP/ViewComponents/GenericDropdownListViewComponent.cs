
using EPYSLSAILORAPP.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EPYSLSAILORAPP.ViewComponents
{
    /// <summary>
    /// SideBarMenu
    /// </summary>
    public class GenericDropdownListViewComponent : ViewComponent
    {
        //private IPostRepository repository;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// AboutUsViewComponent
        /// </summary>
        public GenericDropdownListViewComponent(IHttpContextAccessor contextAccessor,

           Microsoft.Extensions.Configuration.IConfiguration config)
        {
            _config = config;
            _contextAccessor = contextAccessor;


        }


        public IViewComponentResult Invoke(string ddlId, string strobjlist, string selectedvalue = "", string cssclass = "", bool enable = true, bool isRequired = false, string placeholder = "", bool isBindDataSource = false)
        {
            List<SelectListItemExtended> dlList = new List<SelectListItemExtended>();


            if (!string.IsNullOrEmpty(strobjlist))
            {
                var objlist = JsonConvert.DeserializeObject<List<SelectListItemExtended>>(strobjlist);

                foreach (var p in objlist)
                {
                    dlList.Add(p);
                }
            }

            ViewBag.ddlTitle = "";
            ViewBag.ddlName = dlList;
            ViewBag.ddlId = ddlId;
            ViewBag.ddlcss = cssclass;
            ViewBag.enable = enable;
            ViewBag.selectedvalue = selectedvalue;
            ViewBag.isRequired = isRequired;
            ViewBag.placeholder = placeholder;
            ViewBag.isBindDataSource = isBindDataSource;

            return View("~/Views/Shared/Components/Select2/GenericDropDownList.cshtml");
        }

    }
}
