using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using Microsoft.AspNetCore.Mvc;

namespace EPYSLSAILORAPP.Controllers
{
    public class CommonController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly ITransampleorderService _TransampleorderService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public CommonController(
            IMenuService menuService, 
            ITransampleorderService TransampleorderService,
             Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment
            )
        {
            _menuService = menuService;
            _TransampleorderService = TransampleorderService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        
        public async Task<ActionResult> GetMenus(int applicationId)
        {
            //return Ok(await _menuService.GetMenusAsync(UserId, applicationId, AppUser.CompanyId));
            return Ok(await _menuService.GetMenusAsync(0, 0, 0));
        }





        [HttpGet]
        public async Task<string> SampleOrderFrontImageHandler(Int64 sample_order_id)
        {
            try
            {
                var obj_sampleorder = await _TransampleorderService.GetSingleWithImageByIdAsync(sample_order_id);

                if (obj_sampleorder != null)
                {
                    var front_image = obj_sampleorder.List_base64String_File.FirstOrDefault();
                    if (front_image != null)
                    { 
                        return front_image.base64string;
                    }
                }
                else
                {
                    return "";
                }

                return "";
            }
            catch(Exception es)
            {
                return "";
            }
           
        }




        #region Helpers

        //private void GenerateMenus(ref List<Menu> menuList, List<Menu> list, int parentId)
        //{
        //    var childList = new List<Menu>();
        //    if (parentId == 0)
        //        childList = list.Where(x => x.MenuId == x.ParentId).ToList();
        //    else
        //        childList = list.Where(x => x.ParentId == parentId && x.MenuId != parentId).ToList();

        //    foreach (var item in childList)
        //    {
        //        var menu = _mapper.Map<Menu, Menu>(item);
        //        menuList.Add(menu);
        //        menu.Childs = new List<Menu>();
        //        var cnList = list.FindAll(x => x.ParentId == item.MenuId && x.MenuId != item.MenuId);
        //        if (!cnList.Any())
        //            continue;

        //        foreach (var c in cnList)
        //        {
        //            var cMenu = _mapper.Map<Menu, Menu>(c);
        //            menu.Childs.Add(cMenu);
        //            cMenu.Childs = new List<Menu>();
        //            var ncnList = list.FindAll(x => x.ParentId == c.MenuId && x.MenuId != c.MenuId);
        //            if (!ncnList.Any())
        //                continue;
        //            var childs = cMenu.Childs;
        //            GenerateMenus(ref childs, list, cMenu.MenuId);
        //        }
        //    }
        //}

        #endregion Helpers
    }
}
