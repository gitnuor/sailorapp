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
using EPYSLSAILORAPP.Application.Interface.Security;
using EPYSLSAILORAPP.Domain.Security;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.SignalR;

namespace EPYSLSAILORAPP.Controllers
{
    public class OwinUserController : BaseController
    {
        private readonly ILogger<OwinUserController> _logger;
        private readonly IOwinRoleService _OwinRoleService;
        private readonly IGenTeamGroupService _GenTeamGroupService;
        private IRedisService<SecurityCapsule> _objUser;
        private readonly IGenDesignationService _GenDesignationService;
        private readonly IOwinUserService _OwinUserService;
        private readonly IMemoryCache _cache;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<ChatHub> _hubContext;


        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside OwinUserController !");
            return View();
        }

        public OwinUserController(IGenDesignationService GenDesignationService, IHubContext<ChatHub> hubContext,
           IMapper mapper, ILogger<OwinUserController> logger, IHttpContextAccessor contextAccessor,
            IOwinUserService OwinUserService, IOwinRoleService OwinRoleService, IGenTeamGroupService GenTeamGroupService,
            IRPCDbService rpc_db_service, IConfiguration configuration, IMemoryCache cache)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _OwinUserService = OwinUserService;
            _OwinRoleService = OwinRoleService;
            _GenTeamGroupService = GenTeamGroupService;
            _hubContext = hubContext;
            _cache = cache;

            _GenDesignationService = GenDesignationService;

            _configuration = configuration;
            _objUser = new RedisService<SecurityCapsule>(_configuration);

        }

        [HttpGet]
        public async Task<IActionResult> OwinUserLanding()
        {
            List<SecurityCapsule> list = new List<SecurityCapsule>();

            if (_cache.TryGetValue("online_user_list", out list))
            {
                ViewBag.UserList = list;
            }

            return View("~/Views/Security/OwinUser/OwinUserLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> OwinUserNew()
        {
            owin_user_DTO model = new owin_user_DTO();

            var team_group = await _GenTeamGroupService.GetAllAsync();

            ViewBag.team_group = (List<SelectListItem>)team_group.ToList()
                                               .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.team_group_name.ToString(),
                                                    Value = a.gen_team_group_id.ToString(),
                                                }).ToList();


            var roles = await _OwinRoleService.GetAllAsync();

            ViewBag.roles = (List<SelectListItem>)roles.ToList()
                                                .Select(a =>
                                                 new SelectListItem
                                                 {
                                                     Text = a.role_name.ToString(),
                                                     Value = a.owin_role_id.ToString(),
                                                 }
          ).ToList();

            var designation = await _GenDesignationService.GetAllAsync();

            ViewBag.designation = (List<SelectListItem>)designation.ToList()
                                               .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.designation_name.ToString(),
                                                    Value = a.designation_id.ToString(),
                                                }).ToList();


            return PartialView("~/Views/Security/OwinUser/_OwinUserNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> OwinUserEdit(string userid)
        {

            string decoded_userid = clsUtil.DecodeString(userid);

            owin_user_DTO model = new owin_user_DTO();

            model = await _OwinUserService.GetSingleAsync(Convert.ToInt64(decoded_userid));

            var roles = await _OwinRoleService.GetAllAsync();

            ViewBag.roles = (List<SelectListItem>)roles.ToList()
                                                .Select(a =>
                                                 new SelectListItem
                                                 {
                                                     Text = a.role_name.ToString(),
                                                     Value = a.owin_role_id.ToString(),
                                                 }
          ).ToList();

            var team_group = await _GenTeamGroupService.GetAllAsync();

            ViewBag.team_group = (List<SelectListItem>)team_group.ToList()
                                               .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.team_group_name.ToString(),
                                                    Value = a.gen_team_group_id.ToString(),
                                                }).ToList();


            var designation = await _GenDesignationService.GetAllAsync();

            ViewBag.designation = (List<SelectListItem>)designation.ToList()
                                               .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.designation_name.ToString(),
                                                    Value = a.designation_id.ToString(),
                                                }).ToList();

            return PartialView("~/Views/Security/OwinUser/_OwinUserEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> OwinUserView(string userid)
        {

            string decoded_userid = clsUtil.DecodeString(userid);

            owin_user_DTO model = new owin_user_DTO();

            model = await _OwinUserService.GetSingleAsync(Convert.ToInt64(decoded_userid));

            var roles = await _OwinRoleService.GetAllAsync();

            ViewBag.roles = (List<SelectListItem>)roles.ToList()
                                                .Select(a =>
                                                 new SelectListItem
                                                 {
                                                     Text = a.role_name.ToString(),
                                                     Value = a.owin_role_id.ToString(),
                                                 }
          ).ToList();

            var team_group = await _GenTeamGroupService.GetAllAsync();

            ViewBag.team_group = (List<SelectListItem>)team_group.ToList()
                                               .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.team_group_name.ToString(),
                                                    Value = a.gen_team_group_id.ToString(),
                                                }).ToList();


            var designation = await _GenDesignationService.GetAllAsync();

            ViewBag.designation = (List<SelectListItem>)designation.ToList()
                                               .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.designation_name.ToString(),
                                                    Value = a.designation_id.ToString(),
                                                }).ToList();

            return PartialView("~/Views/Security/OwinUser/_OwinUserView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetOwinUserData(DtParameters request)
        {
            var roles = await _OwinRoleService.GetAllAsync();

            var teamlist = await _GenTeamGroupService.GetAllAsync();

            var records = await _OwinUserService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.userid,
                            t.name,
                            t.user_name,
                            t.password,
                            t.email,
                            t.email_password,
                            t.is_super_user,
                            t.is_admin,
                            t.is_active,
                            t.is_loggedin,
                            t.logon_time,
                            t.employee_code,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,
                            t.role_id,
                            strTeam = t.gen_team_group_id != null && teamlist.Where(p => p.gen_team_group_id == t.gen_team_group_id).FirstOrDefault() != null ? (teamlist.Where(p => p.gen_team_group_id == t.gen_team_group_id).FirstOrDefault().team_group_name) : "",
                            str_IsActive = t.is_active == true ? "Active" : "Inactive",
                            strRole = roles.Where(p => p.owin_role_id == t.role_id).FirstOrDefault() != null ? roles.Where(p => p.owin_role_id == t.role_id).FirstOrDefault().role_name : "No-Role",

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditOwinUser(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  userid='" + clsUtil.EncodeString(t.userid.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewOwinUser(this)' style='width: 120px;' class='btn btn-secondary btnView'  userid='" + clsUtil.EncodeString(t.userid.ToString()) + "'>View</button></div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.FirstOrDefault() != null ? records.FirstOrDefault().total_rows.Value : 0, data);
            return Json(ret_obj);

        }



        [HttpPost]
        public async Task<IActionResult> SaveOwinUser([FromBody] owin_user_DTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                if (request.userid == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;
                }

            }

            try
            {

                ret = await _OwinUserService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateOwinUser([FromBody] owin_user_DTO request)
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
                ret = await _OwinUserService.UpdateAsync(request);

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


        [HttpGet]
        public async Task<IActionResult> OwinUserProfileView(string userid)
        {

            string decoded_userid = clsUtil.DecodeString(userid);

            owin_user_DTO model = new owin_user_DTO();

            model = await _OwinUserService.GetSingleAsync(Convert.ToInt64(decoded_userid));


            var roles = await _OwinRoleService.GetAllAsync();

            ViewBag.roles = (List<SelectListItem>)roles.ToList()
                                                .Select(a =>
                                                 new SelectListItem
                                                 {
                                                     Text = a.role_name.ToString(),
                                                     Value = a.owin_role_id.ToString(),
                                                 }
          ).ToList();

            var team_group = await _GenTeamGroupService.GetAllAsync();

            ViewBag.team_group = (List<SelectListItem>)team_group.ToList()
                                               .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.team_group_name.ToString(),
                                                    Value = a.gen_team_group_id.ToString(),
                                                }).ToList();


            var designation = await _GenDesignationService.GetAllAsync();

            ViewBag.designation = (List<SelectListItem>)designation.ToList()
                                               .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.designation_name.ToString(),
                                                    Value = a.designation_id.ToString(),
                                                }).ToList();

            return PartialView("~/Views/Security/OwinUser/_OwinUserUpdatePassword.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> UpdateOwinUserPaswword([FromBody] owin_user_DTO request)
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
                request.password = request.newPassword;
            }

            try
            {
                //var model = JsonConvert.DeserializeObject<owin_user_entity>(JsonConvert.SerializeObject(request));

                ret = await _OwinUserService.UpdatePasswordAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Password Updated Successful" : "Data Saving Failed")
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


    }
}

