using Microsoft.AspNetCore.Mvc;


namespace EPYSLSAILORAPP.ViewComponents
{

    public class S2PRAccViewComponent : ViewComponent
    {
       
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;

      
        public S2PRAccViewComponent(IHttpContextAccessor contextAccessor, 
          
           Microsoft.Extensions.Configuration.IConfiguration config)
        {
            _config = config;
            _contextAccessor = contextAccessor;
          
        }


        public IViewComponentResult Invoke(string ddlId, string selectedvalue="", string cssclass = "",
            bool isRequired= false, bool isReadOnly = false, string placeholder="",string preloaded="",string model_parentid="")
        {
            
            ViewBag.ddlTitle = "";
            ViewBag.data = !string.IsNullOrEmpty(preloaded) ? preloaded : null;
            ViewBag.selectid = ddlId;
            ViewBag.ddlcss = cssclass;
            ViewBag.selectedvalue = selectedvalue;
            ViewBag.isRequired = isRequired;
            ViewBag.placeholder = placeholder;
            ViewBag.isReadOnly = isReadOnly;
            ViewBag.model_parentid = model_parentid;
           

            return View("~/Views/Shared/Components/Select2/S2PRAcc.cshtml");
        }

    }
}

