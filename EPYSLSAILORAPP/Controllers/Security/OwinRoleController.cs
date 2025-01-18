using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Application.DTO;
using System.Security.Claims;
using EPYSLSAILORAPP.Application.Interface;
using BDO.Core.Base;
using Web.Core.Frame.Helpers;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.SystemServices;
using EPYSLSAILORAPP.Domain.DTO;
using ServiceStack;
using Serilog.Context;

using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Controllers
{
    public class OwinRoleController : BaseController
    {
        private readonly ILogger<OwinRoleController> _logger;
        private readonly IMenuService _MenuService;
        private readonly IMenuActionService _MenuActionService;
        private readonly IOwinRoleService _OwinRoleService;
        private readonly IOwinRolePermissionService _OwinRolePermissionService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside OwinRoleController !");
            return View();
        }

        public OwinRoleController(
           IMapper mapper, ILogger<OwinRoleController> logger, IHttpContextAccessor contextAccessor,
            IOwinRoleService OwinRoleService, IOwinRolePermissionService OwinRolePermissionService,
            IMenuActionService MenuActionService, IMenuService MenuService,
            IRPCDbService rpc_db_service)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _OwinRoleService = OwinRoleService;
            _OwinRolePermissionService = OwinRolePermissionService;
            _MenuActionService = MenuActionService;
            _MenuService = MenuService;

            

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> OwinRoleLanding()
        {

            return View("~/Views/Security/OwinRole/OwinRoleLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> OwinRoleNew()
        {
            owin_role_DTO model = new owin_role_DTO();

            var menu_actionList=await _MenuActionService.GetAllAsync();

            model.MenuAction_List = menu_actionList;

            var menu_List = await _MenuService.GetAllAsync();

            model.Menu_List = menu_List;

            return PartialView("~/Views/Security/OwinRole/_OwinRoleNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> OwinRoleEdit(string owin_role_id)
        {

            string decoded_owin_role_id = clsUtil.DecodeString(owin_role_id);

            owin_role_DTO model = new owin_role_DTO();

            model = await _OwinRoleService.GetAsync(Convert.ToInt64(decoded_owin_role_id));

            var menu_actionList = await _MenuActionService.GetAllAsync();

            model.MenuAction_List = menu_actionList;

            var menu_List = await _MenuService.GetAllAsync();

            model.Menu_List = menu_List;

            var role_permissionList = await _OwinRolePermissionService.GetByRoleIdAsync(Convert.ToInt64(decoded_owin_role_id));

            model.Role_Permission_List = role_permissionList;

            return PartialView("~/Views/Security/OwinRole/_OwinRoleEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> OwinRoleView(string owin_role_id)
        {

            string decoded_owin_role_id = clsUtil.DecodeString(owin_role_id);

            owin_role_DTO model = new owin_role_DTO();

            model = await _OwinRoleService.GetAsync(Convert.ToInt64(decoded_owin_role_id));

            return PartialView("~/Views/Security/OwinRole/_OwinRoleView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetOwinRoleData(DtParameters request)
        {

            var records = await _OwinRoleService.GetAllAsync();

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                string searchValue = request.Search.Value.ToLower();
                records = records.Where(t =>
                    t.role_name.ToLower().Contains(searchValue)
                 
                ).ToList();
            }

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.owin_role_id,
                            t.role_name,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditOwinRole(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  owin_role_id='" + clsUtil.EncodeString(t.owin_role_id.ToString()) + "'>Edit</button>" +
                           // "<button type='button' onclick='ViewOwinRole(this)' style='width: 120px;' class='btn btn-secondary btnView'  owin_role_id='" + clsUtil.EncodeString(t.owin_role_id.ToString()) + "'>View</button>"+
                            "<button type='button' onclick='DeleteOwinRole(\""+ clsUtil.EncodeString(t.owin_role_id.ToString()) + "\")' style='width: 120px;' class='btn btn-secondary btnDelete'  owin_role_id='" + clsUtil.EncodeString(t.owin_role_id.ToString()) + "'>Delete</button>" +
                            "</div>"
                        }
                        ).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }



        [HttpPost]
        public async Task<IActionResult> SaveOwinRole([FromBody] owin_role_DTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                if (request.owin_role_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;
                }

            }

            try
            {
                
                ret = await _OwinRoleService.SaveAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : "Data Saving Failed")
                });
            }
            catch (Exception ex)
            {
                ret = false;

                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    _logger.LogError(ex.ToString());
                }

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : ex.Message)
                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateOwinRole([FromBody] owin_role_DTO request)
        {
            var ret = true;

            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

                request.updated_by = sec_object.userid.Value;

                request.date_updated = DateTime.Now;
            }

            try
            {
                //var model = JsonConvert.DeserializeObject<owin_role_entity>(JsonConvert.SerializeObject(request));

                ret = await _OwinRoleService.UpdateAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : "Data Saving Failed")
                });
            }
            catch (Exception ex)
            {
                ret = false;
                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    _logger.LogError(ex.ToString());
                }
                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : ex.Message)
                });
            }


        }

        [HttpPost]
        public async Task<IActionResult> DeleteOwinRole([FromBody] owin_role_DTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                if (request.owin_role_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;
                }

            }

            try
            {
                string decoded_owin_role_id = clsUtil.DecodeString(request.strMasterID);

                ret = await _OwinRoleService.DeleteAsync(Convert.ToInt64( decoded_owin_role_id));

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : "Data Deletion Failed")
                });
            }
            catch (Exception ex)
            {
                ret = false;

                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    _logger.LogError(ex.ToString());
                }

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Operation Failed" : ex.Message)
                });
            }

        }


    }
}

