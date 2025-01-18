using BDO.Core.Base;
using DnsClient.Protocol;
using Elasticsearch.Net;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;

using EPYSLSAILORAPP.Application.DTO.TranTables;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.Security;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.FilterAndAttributes;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Ocsp;
using ServiceStack;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using System.Threading;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    //[AuthorizeActionFilter]
    public class DashboardController : BaseController
    {
        private readonly IRPCDbService _rpc_db_service;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private readonly ITranChatService _TranChatService;
        private readonly ITranNotificationService _TranNotificationService;
        private readonly IGenSeasonEventConfigurationService _seasoneventconfig_service;
        private readonly IRPCDbService _rpcdb_service;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private IRedisService<get_table_data_count_for_select2_DTO> _redis_select2_service;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public DashboardController(IGenSeasonEventConfigurationService seasoneventconfig_service, ITranChatService TranChatService,
            IRPCDbService rpc_db_service, IRPCDbService rpcdb_service, IHttpContextAccessor contextAccessor,
            IConfiguration configuration, IGenFiscalYearService gen_fiscal_year_Service, ITranNotificationService TranNotificationService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            //_menuService = menuService;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
            _gen_fiscal_year_Service = gen_fiscal_year_Service;
            _seasoneventconfig_service = seasoneventconfig_service;
            _rpcdb_service = rpcdb_service;
            _rpc_db_service = rpc_db_service;
            _TranChatService = TranChatService;
            _hostingEnvironment = hostingEnvironment;
            _TranNotificationService = TranNotificationService;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            _redis_select2_service = new RedisService<get_table_data_count_for_select2_DTO>(_configuration);
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {

            ViewBag.ProfilePic = "/images/user.png";

            if(HttpContext.Session==null)
            {
                Response.Redirect("/account/LogOff");
            }

            string jsonobject = string.Empty;
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            //if(listClaims.Count==0)
            //    Response.Redirect("/account/LogOff");
            if (listClaims.Find(c => c.Type == "username") != null)
            {
                HttpContext.Session.SetString("LoginUserName", (listClaims.Find(c => c.Type == "username").Value).ToString());
            }

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;
                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                HttpContext.Session.SetString("LoginusernameP", (sec_object.username).ToString());
                HttpContext.Session.SetString("Loginemail", (sec_object.email).ToString());

            }
            else
            {
                Response.Redirect("/account/LogOff");
            }

            if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            {
                GenSeasonEventConfigurationDTO obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                ViewBag.FilterExist = obj != null ? 1 : 0;
            }
            else
            {
                ViewBag.FilterExist = 0;
            }
            return View("~/Views/Dashboard/DashBoardHome.cshtml");
        }


        [HttpGet]
        public ActionResult CommonInterface(int menuId)
        {
            ViewBag.MenuId = menuId;
            return PartialView("~/Views/Dashboard/_CommonInterface.cshtml");
        }
        [HttpGet]
        public async Task<JsonResult> GetCalenderData(string fiscal_year_id)
        {
            GenSeasonEventConfigurationDTO objFilter = new GenSeasonEventConfigurationDTO();

            if (!string.IsNullOrEmpty(fiscal_year_id))
            {
                var records = await _rpc_db_service.get_calendar_chart_data(Convert.ToInt64(fiscal_year_id));
                return Json(records);
            }
            else
            {
                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {
                    objFilter = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();
                    var records = await _rpc_db_service.get_calendar_chart_data(objFilter.fiscal_year_id);
                    return Json(records);
                }
                else
                {
                    var records = await _rpc_db_service.get_calendar_chart_data(1);
                    return Json(records);
                }
            }




        }



        [HttpGet]
        public async Task<JsonResult> GetOutlateData(string fiscal_year_id)
        {
            GenSeasonEventConfigurationDTO objFilter = new GenSeasonEventConfigurationDTO();
            if (!string.IsNullOrEmpty(fiscal_year_id))
            {
                var records = await _rpc_db_service.GetAllfn_get_outlet_salesAsync(Convert.ToInt64(fiscal_year_id));
                return Json(records);
            }
            else
            {
                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {
                    objFilter = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();
                    var records = await _rpc_db_service.GetAllfn_get_outlet_salesAsync(objFilter.fiscal_year_id);
                    return Json(records);
                }
                else
                {
                    var records = await _rpc_db_service.GetAllfn_get_outlet_salesAsync(1);
                    return Json(records);
                }
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetMonthlyWiseSummaryData(string fiscal_year_filter)
        {

            if (!string.IsNullOrEmpty(fiscal_year_filter))
            {
                var records = await _rpcdb_service.GetAllrpt_bp_data_month_wiseAsync(Convert.ToInt64(fiscal_year_filter));
                return Json(records);
            }

            return Json("");
        }

        [HttpGet]
        public async Task<JsonResult> GetDistrictWiseSummaryData(string fiscal_year_filter)
        {

            if (!string.IsNullOrEmpty(fiscal_year_filter))
            {
                var records = await _rpcdb_service.GetAllrpt_bp_data_districtwiseAsync(Convert.ToInt64(fiscal_year_filter));

                return Json(records);
            }

            return Json("");
        }

        [HttpGet]
        public async Task<JsonResult> GetMonthWiseOutletSummaryData(string fiscal_year_filter)
        {

            if (!string.IsNullOrEmpty(fiscal_year_filter))
            {
                var records = await _rpcdb_service.GetAllrpt_bp_data_month_wise_outletAsync(Convert.ToInt64(fiscal_year_filter));

                return Json(records);
            }

            return Json("");
        }
        //
        [HttpGet]
        public async Task<JsonResult> GetMonthWiseCompareData(string fiscal_year_filter, string compare_with_fiscal_year_id)
        {

            if (!string.IsNullOrEmpty(fiscal_year_filter))
            {
                var records = await _rpcdb_service.GetAllrpt_bp_data_month_compare_dataAsync(Convert.ToInt64(fiscal_year_filter),
                    Convert.ToInt64(compare_with_fiscal_year_id));
                return Json(records);
            }

            return Json("");
        }
        [HttpGet]
        public async Task<JsonResult> GetOutletWiseSummaryData(string fiscal_year_filter)
        {
            if (!string.IsNullOrEmpty(fiscal_year_filter))
            {
                var records = await _rpc_db_service.GetAllrpt_bp_data_outlet_wiseAsync(Convert.ToInt64(fiscal_year_filter));
                return Json(records);
            }

            return Json("");
        }

        [HttpGet]
        public async Task<IActionResult> SetFilterDataLoadPopup()//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {
            SecurityCapsule sec_object = new SecurityCapsule();

            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);
            }

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.OrderBy(p => p.year_no)
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name,
                                                       Value = a.fiscal_year_id.ToString(),
                                                       Selected = a.is_used

                                                   }
                                                   ).ToList();

            return PartialView("_SetFilterData");

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SetFilterData([FromBody] GenSeasonEventConfigurationDTO request)
        {

            var retFilter = await _rpc_db_service.GetAllsp_get_event_details_allphaseAsync(request.fiscal_year_id, request.event_id);

            if (retFilter.Count > 0)
            {
                request.range_plan_id = retFilter.FirstOrDefault().range_plan_id != null ? retFilter.FirstOrDefault().range_plan_id.Value : 0;

                request.str_fiscal_year_id = clsUtil.EncodeString(request.fiscal_year_id.ToString());
                request.str_event_id = clsUtil.EncodeString(request.event_id.ToString());
                request.str_range_plan_id = clsUtil.EncodeString(retFilter.FirstOrDefault().range_plan_id.ToString());

                _redisgenseasoneventconfigurationservice.SaveDataInRedisCache("redis_set_user_filter", request);
            }
            else
            {
                request.range_plan_id = 0;
                request.str_fiscal_year_id = clsUtil.EncodeString(request.fiscal_year_id.ToString());
                request.str_event_id = clsUtil.EncodeString(request.event_id.ToString());
                request.str_range_plan_id = clsUtil.EncodeString("0".ToString());

                _redisgenseasoneventconfigurationservice.SaveDataInRedisCache("redis_set_user_filter", request);

            }

            return Json(new ResultEntity
            {
                Status_Code = "200",
                Status_Result = "Filter Set Successful",
                data = JsonConvert.SerializeObject(request)
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetEventByFiscalYear(string fiscal_year_id)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {


            DtParameters dtparam = new DtParameters();
            dtparam.fiscal_year_id = Convert.ToInt64(fiscal_year_id);
            dtparam.Start = 0;
            dtparam.Length = 100;
            dtparam.Search = new DtSearch();

            var records = await _seasoneventconfig_service.GetAllPagedData(dtparam);

            return Json(records);
        }

        [HttpGet]
        public async Task<IActionResult> StoreCache()
        {
            get_table_data_count_for_select2_DTO counter = await _rpcdb_service.GetCountForSelect2Async();

            _redis_select2_service.SaveDataInRedisCache("Select2_Counter", counter);

            return Json("records");
        }

        [HttpGet]
        public async Task<JsonResult> GetFilterDropdowns()
        {
            List<gen_season_event_config_ext> event_list = new List<gen_season_event_config_ext>();

            var fiscalyearlist = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var fiscal_year_list = (List<SelectListItem>)fiscalyearlist
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString(),
                                                       Selected = a.is_used
                                                   }
                                                   ).ToList();

            GenSeasonEventConfigurationDTO objFilter = new GenSeasonEventConfigurationDTO();

            if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            {
                objFilter = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                DtParameters dtparam = new DtParameters();
                dtparam.fiscal_year_id = objFilter.fiscal_year_id;
                dtparam.Start = 0;
                dtparam.Length = 100;
                dtparam.Search = new DtSearch();

                event_list = await _seasoneventconfig_service.GetAllPagedData(dtparam);

            }

            return Json(new
            {
                fiscal_year_list = fiscal_year_list,
                selected_fiscal_year_id = objFilter.fiscal_year_id != 0 ? objFilter.fiscal_year_id : Convert.ToInt64(fiscal_year_list.Where(p => p.Selected == true).FirstOrDefault().Value),
                event_list = event_list,
                selected_event_id = objFilter.event_id

            });

        }

        [HttpGet]
        public async Task<JsonResult> GetUserChatHistory(int pageIndex, string fromusername, string tousername)
        {

            DtParameters dtParameters = new DtParameters();
            dtParameters.Start = (pageIndex - 1) * 30;
            dtParameters.strMasterID = fromusername;
            dtParameters.strSecondID = tousername;
            dtParameters.Length = 30;
            dtParameters.Search = new DtSearch();
            dtParameters.Search.Value = "";

            var records = await _TranChatService.GetAllPagedDataAsync(dtParameters);
            return Json(records);

        }

        //
        [HttpGet]
        public async Task<JsonResult> GetAllSystemNotificationHistory(int pageIndex)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            HttpContext.Session.SetString("LoginUserName", (listClaims.Find(c => c.Type == "username").Value).ToString());

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;
                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                DtParameters dtParameters = new DtParameters();
                dtParameters.Start = (pageIndex - 1) * 30;
                dtParameters.strMasterID = sec_object.username;

                dtParameters.Length = 30;
                dtParameters.Search = new DtSearch();
                dtParameters.Search.Value = "";

                var records = await _TranNotificationService.GetAllPagedDataAsync(dtParameters);
                return Json(records);
            }
            return Json(null);

        }

        [HttpGet]
        public async Task UpdateNotification(int notification_id)
        {
             await _TranNotificationService.UpdateAsync(notification_id);
        }

        [HttpGet]
        public async Task<JsonResult> GetGroupChatHistory(int pageIndex,string group_id, string fromusername, string tousername)
        {

            Chat_DtParameters dtParameters = new Chat_DtParameters();
            dtParameters.Start = (pageIndex - 1) * 30;
            dtParameters.to_user_name = tousername;
            dtParameters.group_id = Convert.ToInt64(group_id);
            dtParameters.Length = 30;
            dtParameters.Search = new DtSearch();
            dtParameters.Search.Value = "";

            var records = await _TranChatService.GetAllPagedGroupChatDetailsDataAsync(dtParameters);
            return Json(records);

        }

        [HttpGet]
        public async Task<JsonResult> GetMessageBox()
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            HttpContext.Session.SetString("LoginUserName", (listClaims.Find(c => c.Type == "username").Value).ToString());

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;
                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                Chat_DtParameters dtParameters = new Chat_DtParameters();
                dtParameters.Start = 0;
                dtParameters.to_user_name = sec_object.username;

                dtParameters.Length = 10;
                dtParameters.Search = new DtSearch();
                dtParameters.Search.Value = "";

                var records = await _TranChatService.GetAllPagedMessageBox(dtParameters);
                return Json(records);
            }

            return Json(null);

        }

        [HttpPost]
        public async Task<ActionResult> UploadChatAtachment([FromBody] file_upload objfile)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {
            try
            {
                objfile.server_filename = Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(objfile.filename);

                byte[] byteArray = Convert.FromBase64String(objfile.base64string);

                string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder"), "Chat_Files");

                if (!Directory.Exists(filePath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(filePath);
                }

                System.IO.File.WriteAllBytes(Path.Combine(filePath, objfile.server_filename), byteArray);

                objfile.base64string = "";

            }
            catch (Exception ex)
            {

            }

            return Json(new ResultEntity
            {
                Status_Code=string.IsNullOrEmpty(objfile.base64string) ? "200" : "400",
                data = JsonConvert.SerializeObject(objfile)
            }); 

        }

        [HttpGet]
        public async Task<IActionResult> DownloadChatFile(string server_file_name,string file_name)
        {
            string base64String = "";
            //var filePath = "path/to/your/file/sailor_dhan_01 - Copy_LE_auto_x2 (2).jpg"; // Replace with your file path
            try
            {
                string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder"), "Chat_Files", clsUtil.GetBase64DecodedString(server_file_name));

                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                base64String = Convert.ToBase64String(fileBytes);
            }
            catch (Exception ex)
            {

            }

            return Json(new ResultEntity
            {
                Status_Code = string.IsNullOrEmpty(base64String) ? "200" : "400",
                data = base64String
            });

        }

        public async Task<IActionResult> CreateNewChat()
        {
            return PartialView("~/Views/Shared/GenViews/_ChatNew.cshtml");
        }
    }
}