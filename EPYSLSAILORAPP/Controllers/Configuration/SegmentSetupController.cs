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

namespace EPYSLSAILORAPP.Controllers.Configuration
{
    public class SegmentSetupController : BaseController
    {
        private readonly ILogger<SegmentSetupController> _logger;
        private readonly IConfiguration _configuration;

        private readonly IGenSegmentService _GenSegmentService;
        private readonly IGenSegmentDetlService _GenSegmentDetlService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        private IRedisService<rpc_range_plan_getfor_createupdate_DTO> _redisRangePlanService;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside SegmentSetupController !");
            return View();
        }

        public SegmentSetupController(
           IMapper mapper, ILogger<SegmentSetupController> logger, IHttpContextAccessor contextAccessor,

            IGenSegmentService GenSegmentService,
            IGenSegmentDetlService GenSegmentDetlService,

            IRPCDbService rpc_db_service, IConfiguration configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenSegmentService = GenSegmentService;
            _GenSegmentDetlService = GenSegmentDetlService;
            _configuration = configuration;
            _redisRangePlanService = new RedisService<rpc_range_plan_getfor_createupdate_DTO>(_configuration);

            

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> SegmentSetupLanding()
        {

            return View("~/Views/Configuration/SegmentSetup/SegmentSetupLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> SegmentSetupNew(string fiscal_year_id)
        {
            gen_segment_DTO model = new gen_segment_DTO();

            model.DetList = new List<gen_segment_detl_DTO>();

            return PartialView("~/Views/Configuration/SegmentSetup/_SegmentSetupNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> SegmentSetupEdit(string gen_segment_id)
        {

            string decoded_gen_segment_id = clsUtil.DecodeString(gen_segment_id);

            gen_segment_DTO model = new gen_segment_DTO();

            model = await _GenSegmentService.GetAsync(Convert.ToInt64(decoded_gen_segment_id));

            var rowindex = 1;
            foreach (var item in model.DetList)
            {
                item.RowNumber = rowindex++;
            }

            return PartialView("~/Views/Configuration/SegmentSetup/_SegmentSetupEdit.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetSegmentSetupData(DtParameters request)
        {

            var records = await _GenSegmentService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.gen_segment_id,
                            t.gen_segment_name,
                            t.display_name,
                            t.is_active,
                            t.is_item_segment,
                            str_is_active = t.is_active == true ? "Active" : "Inactive",
                            str_is_item_segment = t.is_item_segment == true ? "Item" : "Non Item",

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditClick(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  gen_segment_id='" + clsUtil.EncodeString(t.gen_segment_id.ToString()) + "'>Edit</button></div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.FirstOrDefault().total_rows.Value, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetSegmentSetup_Detl_Data(DtParameters request)
        {

            var records = await _GenSegmentDetlService.GetPagedData(request);

            var index = request.Start + 1;

            var data = (from t in records
                        select new
                        {
                            row_number = index++,
                            t.gen_segment_id,
                            t.gen_segment_detl_id,
                            t.segment_value,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='RemoveRow(this)' style='width: 120px;' class='btn btn-secondary btnRemove'  gen_segment_detl_id='" + t.gen_segment_detl_id + "'>Remove</button></div>"
                        }).ToList();
            if (records.Count!= 0 || data.Count!=0)
            {
                var ret_obj = new AjaxResponse(records.FirstOrDefault().total_rows.Value, data);
                return Json(ret_obj);
            }
            else
            {
                var ret_obj = new AjaxResponse(records.Count, data);
                return Json(ret_obj);
            }

            

        }


        [HttpPost]
        public async Task<IActionResult> SaveSegmentSetup([FromBody] gen_segment_DTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                if (request.gen_segment_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;

                    foreach (var obj in request.DetList)
                    {
                        obj.added_by = sec_object.userid.Value;
                        obj.date_added = DateTime.Now;
                    }
                }
                else
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;

                    request.updated_by = sec_object.userid.Value;

                    request.date_updated = DateTime.Now;

                    foreach (var obj in request.DetList)
                    {
                        obj.added_by = sec_object.userid.Value;
                        obj.date_added = DateTime.Now;
                        obj.updated_by = sec_object.userid.Value;
                        obj.date_updated = DateTime.Now;
                    }
                }
            }

            try
            {
                ret = await _GenSegmentService.SaveAsync(request);
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
        public async Task<IActionResult> UpdateSegmentSetup([FromBody] gen_segment_DTO request)
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

                foreach (var obj in request.DetList)
                {


                    obj.added_by = sec_object.userid.Value;
                    obj.date_added = DateTime.Now;
                    obj.updated_by = sec_object.userid.Value;
                    obj.date_updated = DateTime.Now;
                }
            }

            try
            {
                ret = await _GenSegmentService.UpdateAsync(request);
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


    }
}
