using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers.BusinessPlanning
{
    public class RangePlanController : ExtendedBaseController
    {
        private readonly ILogger<RangePlanController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IStylegenderService _StylegenderService;
        private readonly IStyleitemoriginService _StyleitemoriginService;
        private readonly IStyleitemtypeService _StyleitemtypeService;
        private readonly IStyleproducttypeService _StyleproducttypeService;
        private readonly IGenOutletService _gen_outlet_entity_service;
        private readonly IGenPriorityService _gen_priority_service;
        private readonly IBusinessPlanService _tran_bp_yearservice;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private readonly ITranrangeplanService _tran_rangeplanservice;
        private readonly ITranrangeplaneventsService _tran_rangeplan_event_service;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IGenSeasonEventConfigurationService _genseasoneventconfigurationservice;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private HelperService objHelperService;

        private IRedisService<rpc_range_plan_getfor_createupdate_DTO> _redisRangePlanService;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside RangePlan !");
            return View();
        }

        public RangePlanController(
            IGenFiscalYearService gen_fiscal_year_Service, IGenOutletService gen_outlet_entity_service, IBusinessPlanService tran_bp_yearservice,
            IMapper mapper, ILogger<RangePlanController> logger, IHttpContextAccessor contextAccessor,
            IStylegenderService StylegenderService,
            IStyleitemoriginService StyleitemoriginService,
            IStyleitemtypeService StyleitemtypeService,
            ITranrangeplaneventsService tran_rangeplan_event_service,
            IStyleproducttypeService StyleproducttypeService, ITranrangeplanService tran_rangeplanservice
            , IGenSeasonEventConfigurationService genseasoneventconfigurationservice
            , IRPCDbService rpc_db_service, IGenPriorityService gen_priority_service, IConfiguration configuration) : base(contextAccessor, configuration,true)
        {
            _tran_rangeplan_event_service = tran_rangeplan_event_service;
            _gen_fiscal_year_Service = gen_fiscal_year_Service;
            _gen_outlet_entity_service = gen_outlet_entity_service;
            _tran_bp_yearservice = tran_bp_yearservice;
            _mapper = mapper;
            _logger = logger;
            _tran_rangeplanservice = tran_rangeplanservice;
            _genseasoneventconfigurationservice = genseasoneventconfigurationservice;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _StylegenderService = StylegenderService;
            _StyleitemoriginService = StyleitemoriginService;
            _StyleitemtypeService = StyleitemtypeService;
            _StyleproducttypeService = StyleproducttypeService;
            _logger.LogInformation("Hello from inside RangePlan !");
            _gen_priority_service = gen_priority_service;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);

            _redisRangePlanService = new RedisService<rpc_range_plan_getfor_createupdate_DTO>(_configuration);

            
        }

        [HttpGet]
        public async Task<IActionResult> RangePlanCreate(string fiscal_year_id, string eventid, string range_plan_id)
        {
            tran_range_plan_DTO model = new tran_range_plan_DTO();

            string decoded_fiscal_year_id = clsUtil.DecodeString(fiscal_year_id);

            string decoded_event_id = clsUtil.DecodeString(eventid);

            string decoded_range_plan_id = clsUtil.DecodeString(range_plan_id);

            ViewBag.range_plan_id = decoded_range_plan_id;
            ViewBag.fiscal_year_id = decoded_fiscal_year_id;
            ViewBag.event_id = decoded_event_id;

            if (decoded_range_plan_id != "0")
            {
                var rangeplan = await _tran_rangeplanservice.GetAllByRangePlanIDAsync(Convert.ToInt64(decoded_range_plan_id));

                if (rangeplan.Count > 0)
                {
                    model = _mapper.Map<tran_range_plan_DTO>(rangeplan.FirstOrDefault());
                }
            }

            return View("~/Views/RangePlanning/RangePlanNew.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> RangePlanViewPopup(string fiscal_year_id, string eventid, string range_plan_id)
        {
            tran_range_plan_DTO model = new tran_range_plan_DTO();

            string decoded_fiscal_year_id = clsUtil.DecodeString(fiscal_year_id);

            string decoded_event_id = clsUtil.DecodeString(eventid);

            string decoded_range_plan_id = clsUtil.DecodeString(range_plan_id);

            ViewBag.range_plan_id = decoded_range_plan_id;

            if (decoded_range_plan_id != "0")
            {
                var rangeplan = await _tran_rangeplanservice.GetAllByRangePlanIDAsync(Convert.ToInt64(decoded_range_plan_id));

                if (rangeplan.Count > 0)
                {
                    model = _mapper.Map<tran_range_plan_DTO>(rangeplan.FirstOrDefault());
                }
            }

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _tran_bp_yearservice.get_tran_BP_YearService_GetAll();
            ViewBag.fiscal_year_id = decoded_fiscal_year_id;
            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString(),
                                                       Selected = (a.fiscal_year_id == Convert.ToInt64(decoded_fiscal_year_id) ? true : false)
                                                   }
                                                   ).ToList();
            DtParameters dtparam = new DtParameters();
            dtparam.fiscal_year_id = Convert.ToInt64(decoded_fiscal_year_id);
            dtparam.Start = 0;
            dtparam.Length = 100;
            dtparam.Search = new DtSearch();
            var gen_event_list = await _genseasoneventconfigurationservice.GetAllPagedData(dtparam);


            ViewBag.event_id = decoded_event_id;
            ViewBag.event_list = (List<SelectListItem>)gen_event_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.event_title.ToString(),
                                                       Value = a.event_id.ToString(),
                                                       Selected = (a.event_id == Convert.ToInt64(decoded_event_id) ? true : false)

                                                   }
                                                   ).ToList();

            return PartialView("~/Views/RangePlanning/_RangePlanViewDetails.cshtml", model);
        }


        [HttpGet]
        public async Task<IActionResult> RangePlanApprovalViewPopup(string is_approved, string is_finalized, string tran_range_plan_event_id, string fiscal_year_id, string eventid, string range_plan_id)
        {
            tran_range_plan_events_DTO model = new tran_range_plan_events_DTO();


            string decoded_fiscal_year_id = clsUtil.DecodeString(fiscal_year_id);

            string decoded_event_id = clsUtil.DecodeString(eventid);

            string decoded_range_plan_id = clsUtil.DecodeString(range_plan_id);

            string decoded_tran_range_plan_event_id = clsUtil.DecodeString(tran_range_plan_event_id);

            string decoded_is_finalized = clsUtil.DecodeString(is_finalized).ToLower();

            string decoded_is_approved = clsUtil.DecodeString(is_approved);

            ViewBag.range_plan_id = decoded_range_plan_id;

            if (decoded_range_plan_id != "0")
            {
                var rangeplan = await _tran_rangeplanservice.GetAllByRangePlanIDAsync(Convert.ToInt64(decoded_range_plan_id));

                if (rangeplan.Count > 0)
                {
                    model.is_finalized = Convert.ToBoolean(decoded_is_finalized);
                    model.tran_range_plan_event_id = Convert.ToInt64(decoded_tran_range_plan_event_id);
                    model.is_approved = Convert.ToInt64(is_approved);
                }
            }

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _tran_bp_yearservice.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_id = decoded_fiscal_year_id;
            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString(),
                                                       Selected = (a.fiscal_year_id == Convert.ToInt64(decoded_fiscal_year_id) ? true : false)
                                                   }
                                                   ).ToList();
            DtParameters dtparam = new DtParameters();
            dtparam.event_id = Convert.ToInt64(decoded_event_id);
            dtparam.Start = 0;
            dtparam.Length = 100;
            dtparam.Search = new DtSearch();
            var gen_event_list = await _genseasoneventconfigurationservice.GetAllPagedData(dtparam);


            ViewBag.event_id = decoded_event_id;
            ViewBag.event_list = (List<SelectListItem>)gen_event_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.event_title.ToString(),
                                                       Value = a.event_id.ToString(),
                                                       Selected = (a.event_id == Convert.ToInt64(decoded_event_id) ? true : false)

                                                   }
                                                   ).ToList();


            return PartialView("~/Views/RangePlanning/_RangePlanApprovalViewDetails.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> RangePlanView(string fiscal_year_id, string eventid)
        {

            _redisRangePlanService.RemoveKey("_redisRangePlanService_" + objHelperService.GetSecurityCapsuleFromClaim().userid.ToString());

            string decoded_fiscal_year_id = clsUtil.DecodeString(fiscal_year_id);

            string decoded_event_id = clsUtil.DecodeString(eventid);

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _tran_bp_yearservice.get_tran_BP_YearService_GetAll();
            ViewBag.fiscal_year_id = decoded_fiscal_year_id;
            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString(),
                                                       Selected = (a.fiscal_year_id == Convert.ToInt64(decoded_fiscal_year_id) ? true : false)
                                                   }
                                                   ).ToList();

            DtParameters dtparam = new DtParameters();
            dtparam.event_id = Convert.ToInt64(decoded_event_id);
            dtparam.Start = 0;
            dtparam.Length = 100;
            dtparam.Search = new DtSearch();
            var gen_event_list = await _genseasoneventconfigurationservice.GetAllPagedData(dtparam);


            ViewBag.event_id = decoded_event_id;
            ViewBag.event_list = (List<SelectListItem>)gen_event_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.event_title.ToString(),
                                                       Value = a.event_id.ToString(),
                                                       Selected = (a.event_id == Convert.ToInt64(decoded_event_id) ? true : false)

                                                   }
                                                   ).ToList();


            var style_item_type_list = await _StyleitemtypeService.GetAllAsync();

            ViewBag.item_type_list = (List<SelectListItem>)style_item_type_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_item_type_name.ToString(),
                                                       Value = a.style_item_type_id.ToString()
                                                   }
                                                   ).ToList();


            var product_type_list = await _StyleproducttypeService.GetAllAsync();

            ViewBag.product_type_list = (List<SelectListItem>)product_type_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_product_type_name.ToString(),
                                                       Value = a.style_product_type_id.ToString()
                                                   }
                                                   ).ToList();


            var gender_list = await _StylegenderService.GetAllAsync();

            ViewBag.gender_list = (List<SelectListItem>)gender_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_gender_name.ToString(),
                                                       Value = a.style_gender_id.ToString()
                                                   }
                                                   ).ToList();

            var origin_list = await _StyleitemoriginService.GetAllAsync();

            ViewBag.origin_list = (List<SelectListItem>)origin_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_item_origin_name.ToString(),
                                                       Value = a.style_item_origin_id.ToString()

                                                   }
                                                   ).ToList();

            return View("~/Views/RangePlanning/RangePlanView.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> RangePlanCreateLanding()//(string fiscal_year_id, string range_plan_id)
        {

            tran_range_plan_DTO model = new tran_range_plan_DTO();

            if (objFilter.range_plan_id != null && objFilter.range_plan_id != 0)
            {
                var rangeplan = await _tran_rangeplanservice.GetAllByRangePlanIDAsync(objFilter.range_plan_id);

                if (rangeplan.Count > 0)
                {
                    model = _mapper.Map<tran_range_plan_DTO>(rangeplan.FirstOrDefault());
                }

            }

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _tran_bp_yearservice.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list
                .Select(a =>
                            new SelectListItem
                            {
                                Text = a.year_name.ToString(),
                                Value = a.fiscal_year_id.ToString(),
                                Selected = a.is_used

                            }).ToList();

            ViewBag.fiscal_year_id = objFilter.fiscal_year_id==0? (fiscal_year_list.Where(p=>p.is_used==true).FirstOrDefault().fiscal_year_id) : objFilter.fiscal_year_id;

            return View("~/Views/RangePlanning/RangePlanCreateLanding.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> RangePlanViewLanding(string fiscal_year_id, string range_plan_id)
        {

            tran_range_plan_DTO model = new tran_range_plan_DTO();

            string decoded_fiscal_year_id = clsUtil.DecodeString(fiscal_year_id);

            if (!string.IsNullOrEmpty(range_plan_id))
            {
                string decoded_range_plan_id = clsUtil.DecodeString(range_plan_id);

                var rangeplan = await _tran_rangeplanservice.GetAllByRangePlanIDAsync(Convert.ToInt64(decoded_range_plan_id));

                if (rangeplan.Count > 0)
                {
                    model = _mapper.Map<tran_range_plan_DTO>(rangeplan.FirstOrDefault());
                }
            }

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _tran_bp_yearservice.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()
                                                       ,
                                                       Selected = a.is_used
                                                   }
                                                   ).ToList();

            ViewBag.fiscal_year_id = decoded_fiscal_year_id;

            return View("~/Views/RangePlanning/RangePlanViewLanding.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> RangePlanApprovalViewLanding(string fiscal_year_id, string range_plan_id)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            tran_range_plan_DTO model = new tran_range_plan_DTO();

            string decoded_fiscal_year_id = clsUtil.DecodeString(fiscal_year_id);

            string decoded_range_plan_id = clsUtil.DecodeString(range_plan_id);

            if (decoded_range_plan_id != "0")
            {
                var rangeplan = await _tran_rangeplanservice.GetAllByRangePlanIDAsync(Convert.ToInt64(decoded_range_plan_id));

                if (rangeplan.Count > 0)
                {
                    model = _mapper.Map<tran_range_plan_DTO>(rangeplan.FirstOrDefault());
                }
            }

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _tran_bp_yearservice.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()
                                                       ,
                                                       Selected = a.is_used
                                                   }
                                                   ).ToList();

            ViewBag.fiscal_year_id = decoded_fiscal_year_id;

            return View("~/Views/RangePlanning/RangePlanApprovalViewLanding.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> RangePlanApproval(string fiscal_year_id, string range_plan_id)
        {

            tran_range_plan_DTO model = new tran_range_plan_DTO();

            string decoded_fiscal_year_id = clsUtil.DecodeString(fiscal_year_id);

            string decoded_range_plan_id = clsUtil.DecodeString(range_plan_id);

            if (decoded_range_plan_id != "0")
            {
                var rangeplan = await _tran_rangeplanservice.GetAllByRangePlanIDAsync(Convert.ToInt64(decoded_range_plan_id));

                if (rangeplan.Count > 0)
                {
                    model = _mapper.Map<tran_range_plan_DTO>(rangeplan.FirstOrDefault());
                }
            }

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _tran_bp_yearservice.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()
                                                       ,
                                                       Selected = a.is_used
                                                   }
                                                   ).ToList();

            ViewBag.fiscal_year_id = decoded_fiscal_year_id;

            return View("~/Views/RangePlanning/RangePlanApproval.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> RangePlanLanding()
        {

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _tran_bp_yearservice.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.
                Where(p => tran_bp_year.All(q => q.fiscal_year_id == p.fiscal_year_id)).OrderByDescending(p => p.fiscal_year_id).ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString(),
                                                       Selected = a.is_used
                                                   }
                                                   ).ToList();

            if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            {
                GenSeasonEventConfigurationDTO obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                if (obj != null)
                {
                    var retFilter = await _rpc_db_service.GetAllsp_get_event_details_allphaseAsync(obj.fiscal_year_id, obj.event_id);

                    if (retFilter.Count > 0)
                    {
                        return RedirectToAction("RangePlanCreateLanding", "RangePlan", new
                        {
                            fiscal_year_id = clsUtil.EncodeString(obj.fiscal_year_id.ToString()),

                            range_plan_id = clsUtil.EncodeString(retFilter.FirstOrDefault().range_plan_id.ToString()),
                        });
                    }
                }
            }

            return View("~/Views/RangePlanning/RangePlanLanding.cshtml");

        }

        [HttpGet]
        public async Task<IActionResult> RangePlanApprovalLanding()
        {

            tran_range_plan_DTO model = new tran_range_plan_DTO();

            if (objFilter.range_plan_id != null && objFilter.range_plan_id != 0)
            {

                var rangeplan = await _tran_rangeplanservice.GetAllByRangePlanIDAsync(objFilter.range_plan_id);

                if (rangeplan.Count > 0)
                {
                    model = _mapper.Map<tran_range_plan_DTO>(rangeplan.FirstOrDefault());
                }

            }

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            //var tran_bp_year = await _tran_bp_yearservice.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString(),
                                                       Selected=a.is_used

                                                   }
                                                   ).ToList();

            ViewBag.fiscal_year_id = objFilter.fiscal_year_id!=0? objFilter.fiscal_year_id.ToString(): fiscal_year_list.Where(p=>p.is_used==true).FirstOrDefault().fiscal_year_id.ToString();

            return View("~/Views/RangePlanning/RangePlanApprovalLanding.cshtml", model);

        }

        private string GetPriorityHTML(string List_Priority_str, Int64? PriorityID)
        {
            List<gen_priority_entity> List_Priority = JsonConvert.DeserializeObject<List<gen_priority_entity>>(List_Priority_str);

            string dropdownHTML = "<select onchange='ddlPriorityChange(this);' class='ddlPriority' style='line-height: 0.5rem;font-size:11px;'>";

            dropdownHTML += "<option value=\"\">Select</option>";

            foreach (var objsingle in List_Priority)
            {
                string strSelected = PriorityID.HasValue && objsingle.priority_id == PriorityID ? "selected" : "";

                dropdownHTML += "<option " + strSelected + " value=" + objsingle.priority_id.ToString() + ">" + objsingle.priority_name + "</option>";
            }

            dropdownHTML += "</select>";

            return dropdownHTML;
        }

        [HttpPost]
        public async Task<IActionResult> GetTranRangePlanYearDataView(Filter_RangePlan_DataTable request)
        {

            List<rpc_range_plan_getfor_view_DTO> filter_records = await _rpc_db_service.GetAll_Range_plan_getfor_View(request);

            var index = request.Start + 1;

            var data = (from t in filter_records.OrderBy(p => p.style_item_product_name)
                        select new
                        {
                            t.bp_yearly_gross_sales,
                            t.style_product_size_group_id,
                            row_index = index++,
                            t.event_gross_sales,
                            t.range_plan_detail_id,
                            t.range_plan_id,
                            t.tran_bp_event_id,
                            t.tran_bp_year_id,
                            t.style_item_product_id,
                            t.total_rangeplan_mrp_value,
                            t.total_rangeplan_cpu_value,
                            t.total_rangeplan_quantity,
                            t.range_plan_quantity,
                            t.cpu_per_pc_value,
                            t.mrp_per_pc_value,
                            t.mrp_value,
                            t.cpu_value,
                            t.is_separate_price,
                            t.remarks,
                            t.tran_range_plan_event_id,
                            t.is_finalized,
                            t.priority_id,
                            t.total_rows,
                            //  totaladded = records.Where(p => p.range_plan_id == t.range_plan_id && p.tran_bp_event_id == t.tran_bp_event_id).Count(),
                            //  totalnotadded = t.total_product - (records.Where(p => p.range_plan_id == t.range_plan_id && p.tran_bp_event_id == t.tran_bp_event_id).Count()),

                            strStyleSizes = t.stylesize_list_json,// GetJsonSizeHTML(t.style_product_size_group_id.Value, t.stylesize_list_json
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,
                            strtxtRangePlanQnty = "<input style='width: 100px;' min='0' type='number' class='border-gray-200 rounded-sm txtRangePlanQnty' value='" + (t.range_plan_quantity != null ? t.range_plan_quantity.ToString() : "0") + "' />",
                            strBtnSize = "<button type='button' style='width: 60px;' onclick='BtnSizeViewClick(this,\"DTTranRangePlan\")' iscomplete='NotAdded' style_item_product_id='" + t.style_item_product_id.ToString() + "' class='btn btn-secondary BtnSize'>View</button>",
                            strtxtPerPcMRPVal = "<input style='width: 100px;' min='0'  type='number' class='border-gray-200 rounded-sm txtPerPcMRPVal' value='" + (t.mrp_per_pc_value != null ? t.mrp_per_pc_value.ToString() : "0") + "' />",
                            strtxtMRPVal = "<input style='width: 100px;'  disabled type='number' class='border-gray-200 rounded-sm txtMRPVal' value='" + (t.mrp_value != null ? t.mrp_value.ToString() : "0") + "' />",

                            strtxtPerPcCPUVal = "<input style='width: 100px;' min='0'  type='number' class='border-gray-200 rounded-sm txtPerPcCPUVal' value='" + (t.cpu_per_pc_value != null ? t.cpu_per_pc_value.ToString() : "0") + "' />",
                            strtxtCPUVal = "<input style='width: 100px;'  disabled type='number' class='border-gray-200 rounded-sm txtCPUVal' value='" + (t.cpu_value != null ? t.cpu_value.ToString() : "0") + "' />",

                            strPriorityDropDownHTML = GetPriorityHTML(t.priority_list_json, t.priority_id),
                            t.style_product_type_id

                        }).ToList();

            var ret_obj = new AjaxResponse(filter_records.Count() > 0 ? filter_records.FirstOrDefault().total_rows.Value : 0, data);

            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetTranRangePlanYearData(Filter_RangePlan_DataTable request)
        {
            List<rpc_range_plan_getfor_createupdate_DTO> filter_records = await _rpc_db_service.GetAll_Range_plan_getfor_CreateUpdate(request);

            var index = request.Start + 1;

            var data = (from t in filter_records.OrderBy(p => p.style_item_product_name)
                        select new
                        {
                            t.bp_yearly_gross_sales,
                            t.style_product_size_group_id,
                            row_index = index++,
                            t.event_gross_sales,
                            t.range_plan_detail_id,
                            t.range_plan_id,
                            t.tran_bp_event_id,
                            t.tran_bp_year_id,
                            t.style_item_product_id,
                            t.total_rangeplan_mrp_value,
                            t.total_rangeplan_cpu_value,
                            t.total_rangeplan_quantity,
                            t.range_plan_quantity,
                            t.cpu_per_pc_value,
                            t.mrp_per_pc_value,
                            t.mrp_value,
                            t.cpu_value,
                            t.remarks,
                            t.is_separate_price,
                            t.tran_range_plan_event_id,
                            t.is_finalized,
                            t.priority_id,

                            //   totaladded = records.Where(p => p.range_plan_id == t.range_plan_id && p.tran_bp_event_id == t.tran_bp_event_id && p.range_plan_detail_id != null).Count(),
                            //  totalnotadded = records.Where(p => p.range_plan_detail_id == null).Count(),
                            strStyleSizes = t.stylesize_list_json,
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,
                            strtxtRangePlanQnty = "<input style='width: 100px;' min='0' onchange='txtRangePlanQntyChange(this);' type='number' class='border-gray-200 rounded-sm txtRangePlanQnty' value='" + (t.range_plan_quantity != null ? t.range_plan_quantity.ToString() : "0") + "' />",
                            strBtnSize = "<button type='button' style='width: 60px;' onclick='BtnSizeClick(this,\"DTTranRangePlan\")' iscomplete='NotAdded' style_item_product_id='" + t.style_item_product_id.ToString() + "' class='btn btn-secondary BtnSize'>Size</button>",
                            strtxtPerPcMRPVal = "<input style='width: 100px;' min='0'  type='number' class='border-gray-200 rounded-sm txtPerPcMRPVal' value='" + (t.mrp_per_pc_value != null ? t.mrp_per_pc_value.ToString() : "0") + "' />",
                            strtxtMRPVal = "<input style='width: 100px;'  disabled type='number' class='border-gray-200 rounded-sm txtMRPVal' value='" + (t.mrp_value != null ? t.mrp_value.ToString() : "0") + "' />",
                            strtxtPerPcCPUVal = "<input style='width: 100px;' min='0'  type='number' class='border-gray-200 rounded-sm txtPerPcCPUVal' value='" + (t.cpu_per_pc_value != null ? t.cpu_per_pc_value.ToString() : "0") + "' />",
                            strtxtCPUVal = "<input style='width: 100px;'  disabled type='number' class='border-gray-200 rounded-sm txtCPUVal' value='" + (t.cpu_value != null ? t.cpu_value.ToString() : "0") + "' />",

                            strPriorityDropDownHTML = GetPriorityHTML(t.priority_list_json, t.priority_id),
                            t.style_product_type_id

                        }).ToList();

            var ret_obj = new AjaxResponse(filter_records.Count() > 0 ? filter_records[0].total_rows.Value : 0, data);

            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> SaveRangePlan([FromBody] tran_range_plan_DTO request)
        {

            request.added_by = sec_object.userid.Value;
            request.date_added = DateTime.Now;
            request.is_submitted = false;
            request.is_approved = false;

            var ret = await _tran_rangeplanservice.SaveMasterDetailsAsync(request);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? "Range Plan Successful" : "Range Plan Data Saving Failed"),
                link=(request.Event_Detail.is_finalized==true? "/RangePlan/RangePlanApprovalLanding?isLoadApproval=1&primaryid="+clsUtil.EncodeString(request.Event_Detail.tran_range_plan_event_id.ToString()) : "")
            });

        }

        [HttpPost]
        public async Task<IActionResult> UpdateRangePlan([FromBody] tran_range_plan_events_DTO request)
        {


            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            var model = _mapper.Map<tran_range_plan_entity>(request);

            var ret = await _tran_rangeplanservice.UpdateAsync(model);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? "Range Plan Submitted Successful" : "Range Plan Submission Failed"),
                link = (request.is_finalized == true ? "RangePlan/RangePlanApprovalLanding?isLoadApproval=1&primaryid=" + clsUtil.EncodeString(request.tran_range_plan_event_id.ToString()) : "")
            });

        }

        [HttpPost]
        public async Task<IActionResult> UpdateRangePlanEvent([FromBody] tran_range_plan_events_DTO request)
        {

            var dbmodel = await _tran_rangeplan_event_service.GetAsync(request.tran_range_plan_event_id.Value);

            dbmodel.updated_by = sec_object.userid.Value;

            dbmodel.date_updated = DateTime.Now;

            dbmodel.is_finalized = request.is_finalized;

            var ret = await _tran_rangeplan_event_service.UpdateAsync(dbmodel);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? "Range Plan Event Finalized Successful" : "Range Plan Finalization Failed")
            });

        }

        [HttpPost]
        public async Task<IActionResult> ApproveRejectRangePlanEvent([FromBody] tran_range_plan_events_DTO request)
        {

            var dbmodel = await _tran_rangeplan_event_service.GetAsync(request.tran_range_plan_event_id.Value);

            dbmodel.updated_by = sec_object.userid.Value;

            dbmodel.date_updated = DateTime.Now;
            dbmodel.is_finalized= request.is_finalized;
            dbmodel.is_approved = request.is_approved.Value;
            dbmodel.approved_by = sec_object.userid.Value;
            dbmodel.approve_date = DateTime.Now;
            dbmodel.approve_remarks = request.approve_remarks;

            var ret = await _tran_rangeplan_event_service.UpdateAsync(dbmodel);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? "Range Plan Approved Successful" : "Range Plan Approval Failed")
            });

        }


        [HttpGet]
        public async Task<IActionResult> GetRangePlanDetails(string fiscal_year_id, string range_plan_id)
        {
            var records = await _gen_outlet_entity_service.GetAllAsync();

            return PartialView("~/Views/Shared/GenViews/GenOutlet.cshtml", records);

        }

        [HttpPost]
        public async Task<IActionResult> GetRangePlanEventData(Filter_RangePlan_DataTable request)
        {
            Int64? primary_id=null;

            if(!string.IsNullOrEmpty(request.str_primary_id))
            {
                primary_id = Convert.ToInt64(clsUtil.DecodeString(request.str_primary_id));
            }

            var records = await _rpc_db_service.GetAll_Tran_range_plan_eventsAsync(request.fiscal_year_id,request.Search.Value,primary_id);

            var index = 1;

            var data = (from t in records
                        select new
                        {
                            t.is_approved,
                            t.yearly_gross_sales,
                            t.tran_bp_event_id,
                            row_index = index++,
                            t.event_gross_sales,
                            t.event_id,
                            t.added_product,
                            t.fiscal_year_id,
                            t.event_title,
                            t.total_range_plan_quantity,
                            t.total_mrp_value,
                            t.total_cpu_value,
                            t.is_finalized,
                            t.tran_range_plan_event_id,
                            t.readygoods_qnty,
                            t.readygoods_value,
                            t.readygoods_cpu,
                            str_total_range_plan_quantity = "<input disabled type=\"currency_ext\"  style=\"text-align:center;width:100%;color: blue;\" value=\"" + (t.total_range_plan_quantity != null ? (t.total_range_plan_quantity.Value).ToString("0.00") : "0") + "\"   class=\"border-gray-200 form-control\">",

                            str_remaining_qty = "<input disabled type=\"currency_ext\"  style=\"text-align:center;width:100%;color: blue;\" value=\"" + (t.readygoods_qnty != null ? (t.readygoods_qnty.Value-(t.total_range_plan_quantity!=null?t.total_range_plan_quantity.Value:0)).ToString("0.00") : "0") + "\"   class=\"border-gray-200 form-control\">",

                            str_gross_sales = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;color: blue;\" value=\"" + (t.yearly_gross_sales != null ? t.yearly_gross_sales.Value.ToString("0.00") : "0") + "\"  class=\"border-gray-200 form-control\">",
                            str_event_gross_sales = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;color: blue;\" value=\"" + (t.event_gross_sales != null ? t.event_gross_sales.Value.ToString("0.00") : "0") + "\"   class=\"border-gray-200 form-control\">",
                            str_total_mrp_value = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;color: blue;\" value=\"" + (t.total_mrp_value != null ? t.total_mrp_value.Value.ToString("0.00") : "0") + "\"  class=\"border-gray-200 form-control\">",
                            str_total_cpu_value = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;color: blue;\" value=\"" + (t.total_cpu_value != null ? t.total_cpu_value.Value.ToString("0.00") : "0") + "\"   class=\"border-gray-200 form-control\">",
                            str_readygoods_qnty = "<input disabled type=\"currency_ext\"  style=\"text-align:center;width:100%;color: blue;\" value=\"" + (t.readygoods_qnty != null ? t.readygoods_qnty.Value.ToString("0.00") : "0") + "\"   class=\"border-gray-200 form-control\">",
                            str_readygoods_value = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;color: blue;\" value=\"" + (t.readygoods_value != null ? t.readygoods_value.Value.ToString("0.00") : "0") + "\"   class=\"border-gray-200 form-control\">",
                            str_readygoods_cpu = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;color: blue;\" value=\"" + (t.readygoods_cpu != null ? t.readygoods_cpu.Value.ToString("0.00") : "0") + "\"   class=\"border-gray-200 form-control\">",

                            strApproveBtns = "<button type='button' style='width: 180px;text-align:center;'  onclick='LoadRangePlanApprovalDetails(this)'  is_approved='" + t.is_approved.ToString() + "' is_finalized='" + (t.is_finalized == true ? clsUtil.EncodeString("true") : clsUtil.EncodeString("false")) + "'  tran_range_plan_event_id='" + clsUtil.EncodeString(t.tran_range_plan_event_id.ToString()) + "'  fiscal_year_id='" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "' eventid='" + clsUtil.EncodeString(t.event_id.ToString()) + "' range_plan_id='" + clsUtil.EncodeString(t.range_plan_id != null ? t.range_plan_id.ToString() : "0") + "'   class='btn btn-secondary btnRangePlanAddModify'>Approve / Reject / Revise</button>" +
                            (t.event_gross_sales > 0 ? ("<button type='button' style='width: 70px;text-align:center' onclick='LoadRangePlanDetails(this)' fiscal_year_id='" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "' eventid='" + clsUtil.EncodeString(t.event_id.ToString()) + "' range_plan_id='" + clsUtil.EncodeString(t.range_plan_id != null ? t.range_plan_id.ToString() : "0") + "'   class='btn btn-secondary BtnView'>View </button>") : ""),

                            strIsFinalized = t.is_finalized == true ? "Finalized" : "Not Finalized",
                            strIsApproved = t.is_approved == 1 ? "Approved" : (t.is_approved==2?"Rejected":"No Action Taken"),
                            strBtns = ((t.is_finalized != true && t.event_gross_sales > 0) ? "<button type='button' style='width: 90px;text-align:center' onclick=\"location.href='/RangePlan/RangePlanCreate?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&eventid=" + clsUtil.EncodeString(t.event_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString((t.range_plan_id != null ? t.range_plan_id.ToString() : "0")) + "'\" class='btn btn-secondary btnRangePlanAddModify'>Range Plan</button>" : "") +
                                       (t.event_gross_sales > 0 ? ("<button type='button' style='width: 70px;text-align:center' onclick='LoadRangePlanDetails(this)' fiscal_year_id='" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "' eventid='" + clsUtil.EncodeString(t.event_id.ToString()) + "' range_plan_id='" + clsUtil.EncodeString((t.range_plan_id != null ? t.range_plan_id.ToString() : "0")) + "' class='btn btn-secondary BtnView'>View</button>") : "")
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count(), data);

            return Json(ret_obj);

        }

        private string GetPlanStatus(rpc_sp_get_tran_range_plan_summary_DTO obj)
        {
            string status = "";

            if (obj.is_submitted != true)
            {
                status = "Draft";
            }
            else
            {
                if (obj.is_approved == true)
                {
                    status = "Approved";
                }
                else
                {
                    status = "Submitted For Approval";
                }
            }
            return status;
        }


        [HttpPost]
        public async Task<IActionResult> GetRangePlanSummaryData(Filter_RangePlan_DataTable request)
        {
            var records = await _rpc_db_service.GetAll_Tran_range_plan_summaryAsync();

            var index = 1;

            var data = (from t in records
                        select new
                        {
                            t.yearly_gross_sales,
                            t.yearly_total_cpu,
                            row_index = index++,
                            t.yearly_total_quantity,
                            t.yearly_total_product,
                            t.yearly_total_mrp,
                            t.year_name,
                            t.is_approved,
                            t.is_submitted,
                            strStatus = GetPlanStatus(t),
                            strBtns = (t.is_submitted != true ? "<button type='button' style='width: 120px;'  onclick=\"location.href='/RangePlan/RangePlanCreateLanding?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "'\"  class='btn btn-secondary BtnSize'>Add/Modify</button>" : "") +
                           "<button type='button' style='width: 70px;'  onclick=\"location.href='/RangePlan/RangePlanViewLanding?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "'\"  class='btn btn-secondary BtnView'>View </button>",

                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count(), data);

            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetRangePlanSummaryDataForApproval(Filter_RangePlan_DataTable request)
        {
            var records = await _rpc_db_service.GetAll_Tran_range_plan_summaryAsync();

            var index = 1;

            var data = (from t in records.Where(p => p.is_submitted == true).ToList()
                        select new
                        {
                            t.yearly_gross_sales,
                            t.yearly_total_cpu,
                            row_index = index++,
                            t.yearly_total_quantity,
                            t.yearly_total_product,
                            t.yearly_total_mrp,
                            t.year_name,
                            t.is_approved,
                            t.is_submitted,
                            strStatus = GetPlanStatus(t),
                            //strBtns = (t.is_approved != true ? "<button type='button' style='width: 120px;'  onclick=\"location.href='/RangePlan/RangePlanApproval?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "'\"  class='btn btn-secondary BtnSize'>Approve/Reject</button>" : "") +
                            strBtns = "<button type='button' style='width: 160px;'  onclick=\"location.href='/RangePlan/RangePlanApproval?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "'\"  class='btn btn-secondary BtnSize'>Approve/Reject/Revise</button>" +

                            "<button type='button' style='width: 70px;'  onclick=\"location.href='/RangePlan/RangePlanApprovalViewLanding?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "'\"  class='btn btn-secondary BtnView'>View </button>",

                        }).ToList();

            var ret_obj = new AjaxResponse(records.Where(p => p.is_submitted == true).ToList().Count(), data);

            return Json(ret_obj);

        }
    }
}
