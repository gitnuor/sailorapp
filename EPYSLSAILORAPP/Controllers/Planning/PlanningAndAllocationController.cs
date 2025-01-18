using AutoMapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.Security;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack;
using System.Text.Json;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers.BusinessPlanning
{
    public class PlanningAndAllocationController : ExtendedBaseController
    {
        private readonly ILogger<PlanningAndAllocationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBusinessPlanService _BP_service;
        private readonly IStylecategoryService _StylecategoryService;
        private readonly IStylegenderService _StylegenderService;
        private readonly IStyleitemoriginService _StyleitemoriginService;
        private readonly IStyleitemproductService _StyleitemproductService;
        private readonly IStyleitemtypeService _StyleitemtypeService;
        private readonly IStylelabelService _StylelabelService;
        private readonly IStyleitemproductsubcategoryService _IStyleitemproductsubcategoryService;
        private readonly IStylemastercategoryService _StylemastercategoryService;
        private readonly IStyleproducttypeService _StyleproducttypeService;
        private readonly IGenProcessMasterService _GenProcessMasterService;  
        private readonly ITranPlanAllocateService _TranPlanAllocateService;
        private readonly IGenOutletService _gen_outlet_entity_service;
        private readonly IGenPriorityService _gen_priority_service;
        private readonly ITran_BP_EventMonthService _tran_bp_eventmonthservice;
        private readonly ITran_BP_EventMonthOutletService _tran_bp_eventmonthoutletservice;
        private readonly ITranrangeplandetailssizeService _tran_rangeplandetailssizeservice;
        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private readonly IOwinUserService _OwinUserService;
        private readonly ITranrangeplanService _tran_rangeplanservice;
        private readonly ITranPlanAllocateStyleService _tranvaplandetlstyleService;
        private readonly IRPCDbService _rpc_db_service;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
            private readonly IGenSeasonEventConfigurationService _genseasoneventconfigurationservice;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private HelperService objHelperService;
        private IRedisService<rpc_range_plan_getfor_createupdate_DTO> _redisRangePlanService;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside PlanningAndAllocation !");
            return View();
        }

        public PlanningAndAllocationController(IBusinessPlanService BusinessPlanService,
            IGenFiscalYearService gen_fiscal_year_Service, IGenOutletService gen_outlet_entity_service,
            ITran_BP_EventMonthService tran_bp_eventmonthservice, ITran_BP_EventMonthOutletService tran_bp_eventmonthoutletservice,
            IMapper mapper, ILogger<PlanningAndAllocationController> logger, IHttpContextAccessor contextAccessor,
            IStylecategoryService StylecategoryService,
            IStylegenderService StylegenderService,
            IStyleitemoriginService StyleitemoriginService,
            IStyleitemproductService StyleitemproductService,
            IStyleitemtypeService StyleitemtypeService,
            ITranPlanAllocateService TranPlanAllocateService,     
            IGenProcessMasterService GenProcessMasterService,
            IStyleitemproductsubcategoryService IStyleitemproductsubcategoryService,
            IStylelabelService StylelabelService, ITranrangeplandetailssizeService tran_rangeplandetailssizeservice,
            ITranPlanAllocateStyleService tranvaplandetlstyleService,
            IStylemastercategoryService StylemastercategoryService,
            IStyleproducttypeService StyleproducttypeService, ITranrangeplanService tran_rangeplanservice, 
            IGenSeasonEventConfigurationService genseasoneventconfigurationservice, IOwinUserService OwinUserService
            , IRPCDbService rpc_db_service, IGenPriorityService gen_priority_service, IConfiguration configuration) : base(contextAccessor, configuration)

        {
            _BP_service = BusinessPlanService;
            _gen_fiscal_year_Service = gen_fiscal_year_Service;
            _gen_outlet_entity_service = gen_outlet_entity_service;

            _mapper = mapper;
            _logger = logger;
            _tran_rangeplanservice = tran_rangeplanservice;
            _genseasoneventconfigurationservice = genseasoneventconfigurationservice;

            _GenProcessMasterService = GenProcessMasterService;
            _IStyleitemproductsubcategoryService = IStyleitemproductsubcategoryService;
            _rpc_db_service = rpc_db_service;
            _tran_bp_eventmonthservice = tran_bp_eventmonthservice;
            _tran_bp_eventmonthoutletservice = tran_bp_eventmonthoutletservice;
            _tranvaplandetlstyleService = tranvaplandetlstyleService;
            _OwinUserService = OwinUserService;

            _contextAccessor = contextAccessor;
            _tran_rangeplandetailssizeservice = tran_rangeplandetailssizeservice;



            _TranPlanAllocateService = TranPlanAllocateService;
            //  _TranvaproductembellishmentmappingService = TranvaproductembellishmentmappingService;

            _StylecategoryService = StylecategoryService;
            _StylegenderService = StylegenderService;
            _StyleitemoriginService = StyleitemoriginService;
            _StyleitemproductService = StyleitemproductService;
            _StyleitemtypeService = StyleitemtypeService;
            _StylelabelService = StylelabelService;
            _StylemastercategoryService = StylemastercategoryService;
            _StyleproducttypeService = StyleproducttypeService;

            _logger.LogInformation("Hello from inside PlanningAndAllocation !");
            _gen_priority_service = gen_priority_service;
            _configuration = configuration;

            _redisRangePlanService = new RedisService<rpc_range_plan_getfor_createupdate_DTO>(_configuration);

            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);

            
        }

        [HttpGet]
        public async Task<IActionResult> StyleBreakdownLanding()
        {


            if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            {
                GenSeasonEventConfigurationDTO obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                if (obj != null)
                {
                    var retFilter = await _rpc_db_service.GetAllsp_get_event_details_allphaseAsync(obj.fiscal_year_id, obj.event_id);

                    if (retFilter.Count > 0)
                    {
                        return RedirectToAction("StyleBreakdownCreate", "PlanningAndAllocation", new
                        {
                            fiscal_year_id = clsUtil.EncodeString(obj.fiscal_year_id.ToString()),
                            eventid = clsUtil.EncodeString(obj.event_id.ToString()),
                            range_plan_id = clsUtil.EncodeString(retFilter.FirstOrDefault().range_plan_id.ToString()),
                        });
                    }
                }
                //PlanningAndAllocation/PlanningAndAllocationCreate?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&eventid=" + clsUtil.EncodeString(t.event_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "&tran_va_plan_event_id=" + clsUtil.EncodeString(t.tran_va_plan_event_id != null ? t.tran_va_plan_event_id.ToString() : "0") + "'\"
            }


            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _BP_service.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.
                Where(p => tran_bp_year.All(q => q.fiscal_year_id == p.fiscal_year_id)).OrderByDescending(p => p.fiscal_year_id).ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()
                                                   }
                                                   ).ToList();

            return View("~/Views/PlanningAndAllocation/StyleBreakdownLanding.cshtml");

        }

        [HttpGet]
        public async Task<IActionResult> DesignerDistributionLanding()
        {

            if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            {
                GenSeasonEventConfigurationDTO obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                if (obj != null)
                {
                    var retFilter = await _rpc_db_service.GetAllsp_get_event_details_allphaseAsync(obj.fiscal_year_id, obj.event_id);

                    if (retFilter.Count > 0)
                    {
                        return RedirectToAction("DesignerDistributionCreate", "PlanningAndAllocation", new
                        {
                            fiscal_year_id = clsUtil.EncodeString(obj.fiscal_year_id.ToString()),
                            eventid = clsUtil.EncodeString(obj.event_id.ToString()),
                            range_plan_id = clsUtil.EncodeString(retFilter.FirstOrDefault().range_plan_id.ToString()),

                        });
                    }
                }
                //PlanningAndAllocation/PlanningAndAllocationCreate?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&eventid=" + clsUtil.EncodeString(t.event_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "&tran_va_plan_event_id=" + clsUtil.EncodeString(t.tran_va_plan_event_id != null ? t.tran_va_plan_event_id.ToString() : "0") + "'\"
            }


            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _BP_service.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.
                Where(p => tran_bp_year.All(q => q.fiscal_year_id == p.fiscal_year_id)).OrderByDescending(p => p.fiscal_year_id).ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()
                                                   }
                                                   ).ToList();

            return View("~/Views/PlanningAndAllocation/DesignerDistributionLanding.cshtml", new tran_va_plan_DTO());

        }

        [HttpPost]
        public async Task<IActionResult> GetPlanningAndAllocationSummaryData(tran_va_plan_DTO request)
        {
            var records = await _rpc_db_service.GetAll_VA_plan_summaryAsync();

            var index = 1;

            var data = (from t in records
                        select new
                        {
                            t.yearly_gross_sales,
                            t.yearly_total_cpu,
                            row_index = index++,
                            t.yearly_total_quantity,

                            t.yearly_total_mrp,
                            t.year_name,
                            t.is_approved,
                            t.is_submitted,
                            t.tran_va_plan_id,
                            strStatus = GetPlanStatus(t),
                            strBtns = (t.is_approved != true ? "<button type='button' style='width: 130px;'  onclick=\"location.href='/PlanningAndAllocation/PlanningAndAllocationCreateLanding?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "&va_plan_id=" + clsUtil.EncodeString(t.tran_va_plan_id != null ? t.tran_va_plan_id.ToString() : "0") + "'\"   class='btn btn-secondary BtnSize'>Style BreakDown</button>" : "") +
                            (t.is_approved != true ? "<button type='button' style='width: 130px;'  onclick=\"location.href='/PlanningAndAllocation/DesignerDistributionLanding?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "&va_plan_id=" + clsUtil.EncodeString(t.tran_va_plan_id != null ? t.tran_va_plan_id.ToString() : "0") + "'\"   class='btn btn-secondary BtnSize'>Designer Distribution</button>" : "") +
                            "<button type='button' style='width: 70px;'  onclick=\"location.href='/PlanningAndAllocation/PlanningAndAllocationViewLanding?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "&va_plan_id=" + clsUtil.EncodeString(t.tran_va_plan_id != null ? t.tran_va_plan_id.ToString() : "0") + "'\"  class='btn btn-secondary BtnView'>View </button>",

                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count(), data);

            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetDesignerDistributionData(Filter_RangePlan_DataTable request)
        {
            var records = await _rpc_db_service.GetAllsp_get_va_plan_eventsAsync(request.fiscal_year_id);

            var index = 1;

            var data = (from t in records
                        select new
                        {
                            t.yearly_gross_sales,
                            t.tran_bp_event_id,
                            t.tran_va_plan_event_id,
                            t.tran_range_plan_event_id,
                            row_index = index++,
                            t.event_gross_sales,
                            t.event_id,
                            t.added_product,
                            t.fiscal_year_id,
                            t.event_title,
                            t.total_range_plan_quantity,
                            t.total_mrp_value,
                            t.total_cpu_value,

                            t.is_finalised,
                            strIsFinalized = t.is_finalised == true ? "Finalized" : "Not Finalized",
                            strBtns = (t.is_finalised != true ? "<button type='button' style='width: 110px;'  onclick=\"location.href='/PlanningAndAllocation/DesignerDistributionCreate?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&eventid=" + clsUtil.EncodeString(t.event_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + " & tran_va_plan_event_id=" + clsUtil.EncodeString(t.tran_va_plan_event_id != null ? t.tran_va_plan_event_id.ToString() : "0") + "'\"  class='btn btn-secondary btnRangePlanAddModify'>Assign Designer </button>" : "") +
                            "<button type='button' style='width: 120px;' onclick='LoadRangePlanDetails(this)' fiscal_year_id='" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "' eventid='" + clsUtil.EncodeString(t.event_id.ToString()) + "' tran_va_plan_event_id='" + clsUtil.EncodeString(t.tran_va_plan_event_id != null ? t.tran_va_plan_event_id.ToString() : "0") + "' range_plan_id='" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "'   class='btn btn-secondary BtnView'>View Range Plan</button>"
                          
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count(), data);

            return Json(ret_obj);

        }

        [HttpGet]
        public async Task<IActionResult> PlanningAndAllocationCreateLanding(string fiscal_year_id, string range_plan_id, string va_plan_id)
        {

            tran_va_plan_DTO model = new tran_va_plan_DTO();

            string decoded_fiscal_year_id = clsUtil.DecodeString(fiscal_year_id);

            if (!string.IsNullOrEmpty(range_plan_id))
            {
                string decoded_range_plan_id = clsUtil.DecodeString(range_plan_id);

                model.tran_range_plan_id = Convert.ToInt64(decoded_range_plan_id);


            }
            if (!string.IsNullOrEmpty(va_plan_id))
            {
                string decoded_va_plan_id = clsUtil.DecodeString(va_plan_id);

                if (decoded_va_plan_id != "0")
                {
                    var PlanningAndAllocation = await _TranPlanAllocateService.GetAsync(Convert.ToInt64(decoded_va_plan_id));

                    if (PlanningAndAllocation.Count > 0)
                    {
                        model = _mapper.Map<tran_va_plan_DTO>(PlanningAndAllocation.FirstOrDefault());
                    }
                }
            }

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _BP_service.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()

                                                   }
                                                   ).ToList();

            ViewBag.fiscal_year_id = decoded_fiscal_year_id;

            return View("~/Views/PlanningAndAllocation/PlanningAndAllocationCreateLanding.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetPlanningAndAllocationEventData(Filter_RangePlan_DataTable request)
        {
            var records = await _rpc_db_service.GetAllsp_get_va_plan_eventsAsync(request.fiscal_year_id);

            var index = 1;

            var data = (from t in records
                        select new
                        {
                            t.yearly_gross_sales,
                            t.tran_bp_event_id,
                            t.tran_va_plan_event_id,
                            t.tran_range_plan_event_id,
                            row_index = index++,
                            t.event_gross_sales,
                            t.event_id,
                            t.added_product,
                            t.fiscal_year_id,
                            t.event_title,
                            t.total_range_plan_quantity,
                            t.total_mrp_value,
                            t.total_cpu_value,

                            t.is_finalised,
                            strIsFinalized = t.is_finalised == true ? "Finalized" : "Not Finalized",
                            strBtns = (t.is_finalised != true ? "<button type='button' style='width: 110px;'  onclick=\"location.href='/PlanningAndAllocation/PlanningAndAllocationCreate?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&eventid=" + clsUtil.EncodeString(t.event_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "&tran_va_plan_event_id=" + clsUtil.EncodeString(t.tran_va_plan_event_id != null ? t.tran_va_plan_event_id.ToString() : "0") + "'\"  class='btn btn-secondary btnRangePlanAddModify'>Style Breakdown </button>" : "") +
                            "<button type='button' style='width: 120px;' onclick='LoadRangePlanDetails(this)' fiscal_year_id='" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "' eventid='" + clsUtil.EncodeString(t.event_id.ToString()) + "' tran_va_plan_event_id='" + clsUtil.EncodeString(t.tran_va_plan_event_id != null ? t.tran_va_plan_event_id.ToString() : "0") + "' range_plan_id='" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "'   class='btn btn-secondary BtnView'>View Range Plan</button>"
                         
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count(), data);

            return Json(ret_obj);

        }

        [HttpGet]
        public async Task<IActionResult> StyleBreakdownCreate()//(string fiscal_year_id, string eventid, string range_plan_id, string va_plan_id)
        {
            tran_va_plan_DTO model = new tran_va_plan_DTO();

            ViewBag.encoded_fiscal_year = objFilter.str_fiscal_year_id;

            return View("~/Views/PlanningAndAllocation/PlanningAndAllocationNew.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> StyleBreakdownApproval()//(string fiscal_year_id, string eventid, string range_plan_id, string va_plan_id)
        {
            tran_va_plan_DTO model = new tran_va_plan_DTO();



            ViewBag.encoded_fiscal_year = objFilter.str_fiscal_year_id;

            return View("~/Views/PlanningAndAllocation/StyleBreakdownApprovalLanding.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> DesignerDistributionCreate()//(string fiscal_year_id, string eventid, string range_plan_id, string va_plan_id)
        {
            tran_va_plan_DTO model = new tran_va_plan_DTO();


            return View("~/Views/PlanningAndAllocation/DesignerDistributionCreate.cshtml", model);
        }

        private string GetNumberOfStyleHTML(Int64? no_of_style, bool? isEnable)
        {

            string str = " <select " + (isEnable == false ? "disabled" : "") + " style='width:100%;' class='form-control ddlNoOfStyle'>" +
                                   " <option value='0'>Select</option>";

            for (int i = 1; i <= 10; i++)
            {
                var selected = (no_of_style.HasValue && no_of_style.Value == i) ? "selected" : "";
                str += "<option value='" + i.ToString() + "' " + selected + ">" + i.ToString() + "</option>";
            }

            str += "</select>";

            return str;
        }
        //

        [HttpPost]
        public async Task<IActionResult> GetTranVADesignerDistributionData(Filter_RangePlan_DataTable request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            List<rpc_tran_plan_allocate_getfor_designer_distribution_DTO> filter_records = await _rpc_db_service.GetAlltran_plan_allocate_getfor_designer_distribution(request);

            filter_records = filter_records.Where(p => p.no_of_style > 0).ToList();

            var index = request.Start + 1;
            var data = (from t in filter_records.OrderBy(p => p.style_item_product_name)
                        select new
                        {
                            t.style_product_size_group_id,
                            row_index = index++,
                            t.approved_style,                          
                            t.range_plan_quantity,                   
                            t.tran_va_plan_id,
                            t.tran_va_plan_detl_id,
                            t.tran_va_plan_event_id,
                            t.is_separate_price,
                            t.no_of_style,
                            t.style_code_gen,
                            total_unassigned = (t.approved_style - (t.total_assigned == null ? 0 : t.total_assigned)),
                            t.total_assigned,
                           strjsonSizeList = t.stylesize_list_json,
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,
                            strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",
                            strBtnStyleSetup = "<button type='button' style='width: 110px;' onclick='BtnAssignDesignerAddUpdate(this)' iscomplete='NotAdded' style_item_product_id='" + t.style_item_product_id.ToString() + "' class='btn btn-secondary BtnAssignDesigner'>Assign Designer</button>",
                            t.style_product_type_id

                        }).ToList();

            var ret_obj = new AjaxResponse(filter_records.Count() > 0 ? filter_records[0].total_rows.Value : 0, data);

            return Json(ret_obj);

        }

        private (string HTML, Int64  Qty) GetAllStyleHtml(rpc_plan_allocate_getfor_createupdate_DTO obj, Int64 style_item_product_id, Int64? tran_va_plan_detl_id, int? isfilter)
        {
            string strHTML = "";
            Int64? Qty = 0;

            if (!string.IsNullOrEmpty(obj.all_styles))
            {
                var List = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_DTO>>(obj.all_styles);

                if (isfilter.HasValue && isfilter == 0) //draft
                {
                    List = List.Where(p => (p.is_submitted == 0 || p.is_submitted == null)
                    && (p.is_approved == null || p.is_approved == 0)).ToList();

                    foreach (var objsingle in List)
                    {
                        strHTML += "<a style_quantity=" + objsingle.style_quantity.ToString() + " onclick='BtnStyleClickView(this,0);' tran_va_plan_detl_style_id=" + objsingle.tran_va_plan_detl_style_id.ToString() + " style_item_product_id=" + style_item_product_id.ToString() + " tran_va_plan_detl_id=" + (tran_va_plan_detl_id != null ? tran_va_plan_detl_id.ToString() : "") + " style='font-weight:bold;font-size:13px;' class='clsStyle'>" + objsingle.style_code + "(" + objsingle.style_quantity.ToString() + ")" + "</a> ,";
                    }

                    Qty=List.Sum(p => p.style_quantity);
                }
                else if (isfilter.HasValue && isfilter == 1) //submitted
                {
                    List = List.Where(p => p.is_submitted == 1 &&
                    (p.is_approved == null || p.is_approved == 0)).ToList();

                    foreach (var objsingle in List)
                    {
                        strHTML += "<a style_quantity=" + objsingle.style_quantity.ToString() + " onclick='BtnStyleClickView(this,1);' tran_va_plan_detl_style_id=" + objsingle.tran_va_plan_detl_style_id.ToString() + " style_item_product_id=" + style_item_product_id.ToString() + " tran_va_plan_detl_id=" + (tran_va_plan_detl_id != null ? tran_va_plan_detl_id.ToString() : "") + " style='font-weight:bold;font-size:13px;' class='clsStyle'>" + objsingle.style_code + "(" + objsingle.style_quantity.ToString() + ")" + "</a> ,";
                    }
                    Qty = List.Sum(p => p.style_quantity);
                }
                else if (isfilter.HasValue && isfilter == 2) //rejected
                {
                    List = List.Where(p => p.is_submitted == 0 && p.is_approved == 2).ToList();

                    foreach (var objsingle in List)
                    {
                        strHTML += "<a style_quantity=" + objsingle.style_quantity.ToString() + " onclick='BtnStyleClickView(this,0);' tran_va_plan_detl_style_id=" + objsingle.tran_va_plan_detl_style_id.ToString() + " style_item_product_id=" + style_item_product_id.ToString() + " tran_va_plan_detl_id=" + (tran_va_plan_detl_id != null ? tran_va_plan_detl_id.ToString() : "") + " style='font-weight:bold;font-size:13px;' class='clsStyle'>" + objsingle.style_code + "(" + objsingle.style_quantity.ToString() + ")" + "</a> ,";
                    }
                    Qty = List.Sum(p => p.style_quantity);
                }
                else if (isfilter.HasValue && isfilter == 3) //approved
                {
                    List = List.Where(p => p.is_submitted == 1 && p.is_approved == 1).ToList();

                    foreach (var objsingle in List)
                    {
                        strHTML += "<a style_quantity=" + objsingle.style_quantity.ToString() + " onclick='BtnStyleClickView(this,1);' tran_va_plan_detl_style_id=" + objsingle.tran_va_plan_detl_style_id.ToString() + " style_item_product_id=" + style_item_product_id.ToString() + " tran_va_plan_detl_id=" + (tran_va_plan_detl_id != null ? tran_va_plan_detl_id.ToString() : "") + " style='font-weight:bold;font-size:13px;' class='clsStyle'>" + objsingle.style_code + "(" + objsingle.style_quantity.ToString() + ")" + "</a> ,";
                    }
                    Qty = List.Sum(p => p.style_quantity);
                }
            }

            return (strHTML.Length > 0 ? strHTML.Substring(0, strHTML.Length - 1) : "", Qty.Value);

        }

        [HttpPost]
        public async Task<IActionResult> GetTranPlanningAndAllocationYearData(Filter_RangePlan_DataTable request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            List<rpc_plan_allocate_getfor_createupdate_DTO> filter_records = await _rpc_db_service.GetAllva_plan_getfor_createupdateAsync(request);

            var index = request.Start + 1;
            var data = (from t in filter_records.OrderBy(p => p.style_item_product_name)
                        select new
                        {
                            t.style_product_size_group_id,
                            row_index = index++,
                            t.range_plan_detail_id,
                            t.style_item_product_id,
                            t.range_plan_quantity,
                            t.trading_type_name,
                            t.tran_va_plan_id,
                            t.tran_va_plan_detl_id,
                            t.tran_va_plan_event_id,
                            strAllSubmittedStyles = GetAllStyleHtml(t, t.style_item_product_id.Value, t.tran_va_plan_detl_id, 1).HTML,//submitted
                            strAllRejectedStyles = GetAllStyleHtml(t, t.style_item_product_id.Value, t.tran_va_plan_detl_id, 2).HTML,//rejected
                            strAllDraftStyles = GetAllStyleHtml(t, t.style_item_product_id.Value, t.tran_va_plan_detl_id, 0).HTML,//draft
                            strAllApprovedStyles = GetAllStyleHtml(t, t.style_item_product_id.Value, t.tran_va_plan_detl_id, 3).HTML,//approved

                            total_added = (GetAllStyleHtml(t, t.style_item_product_id.Value, t.tran_va_plan_detl_id, 1).Qty
                             +GetAllStyleHtml(t, t.style_item_product_id.Value, t.tran_va_plan_detl_id, 2).Qty
                             +GetAllStyleHtml(t, t.style_item_product_id.Value, t.tran_va_plan_detl_id, 0).Qty
                             +GetAllStyleHtml(t, t.style_item_product_id.Value, t.tran_va_plan_detl_id, 3).Qty),

                            t.is_separate_price,                 
                            t.no_of_style,
                            t.style_code_gen,
                            t.all_styles,
                            strjsonSizeList = t.stylesize_list_json,
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,
                            strtxt_style_code_gen = "<input style='width: 80px;text-align:center;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",
                            strBtnStyleSetup = "<button type='button' style='width: 110px;text-align:center;' onclick='BtnStyleClickAdd(this)' iscomplete='NotAdded' style_item_product_id='" + t.style_item_product_id.ToString() + "' class='btn btn-secondary BtnStyleSetup'>Add Style</button>",

                            strApprovalBtnStyleSetup = (t.tran_va_plan_detl_id != null ? "<button type='button' style='width: 110px;text-align:center;" +
                            "' tran_va_plan_detl_id='" + t.tran_va_plan_detl_id.ToString() + "' tran_va_plan_event_id='" + t.tran_va_plan_event_id.ToString() + "' tran_va_plan_id='" + t.tran_va_plan_id.ToString() + "' onclick='BtnStyleClickApprove(this)' iscomplete='NotAdded' style_item_product_id='" + t.style_item_product_id.ToString() + "' class='btn btn-secondary BtnStyleSetup'>Approve Style </button>" : ""),

                            t.style_product_type_id

                        }).ToList();

            var ret_obj = new AjaxResponse((filter_records.Count() > 0 ? filter_records[0].total_rows.Value : 0), data);

            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetTranPlanningAndAllocationYearApprovalData(Filter_RangePlan_DataTable request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            List<rpc_plan_allocate_getfor_approval_DTO> filter_records = await _rpc_db_service.GetAllplan_allocate_getfor_approvalAsync(request);

            var index = request.Start + 1;
            var data = (from t in filter_records.OrderBy(p => p.style_item_product_name)
                        select new
                        {
                            row_index = index++,
                            t.style_item_product_id,
                            t.tran_va_plan_detl_id,
                            t.no_of_style,
                            t.style_code_gen,
                            t.style_quantity,
                            t.style_code,
                            t.trading_type_name,
                            t.tran_va_plan_detl_style_id,
                             strjsonSizeList = t.stylesize_list_json, 
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,
                            ddlNoOfStyle = GetNumberOfStyleHTML(t.no_of_style, t.tran_va_plan_detl_id == null ? true : false),//"<input style='width: 70px;' min='0' type='number' class='border-gray-200 rounded-sm ddlNoOfStyle' value='" + (t.no_of_style != null ? t.no_of_style.ToString() : "0") + "' />",
                            strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",
                            strApprovalBtnStyleSetup = (t.tran_va_plan_detl_id != null ? "<button type='button' style='width: 110px;' tran_va_plan_detl_style_id='" + t.tran_va_plan_detl_style_id.ToString() + "' tran_va_plan_detl_id='" + t.tran_va_plan_detl_id.ToString() + "' onclick='BtnStyleClickApprove(this,1)' iscomplete='NotAdded' style_item_product_id='" + t.style_item_product_id.ToString() + "' class='btn btn-secondary BtnStyleSetup'>Approve Style </button>" : ""),
                            strViewBtnStyleSetup = (t.tran_va_plan_detl_id != null ? "<button type='button' style='width: 110px;' tran_va_plan_detl_style_id='" + t.tran_va_plan_detl_style_id.ToString() + "' tran_va_plan_detl_id='" + t.tran_va_plan_detl_id.ToString() + "' onclick='BtnStyleClickApprove(this,0)' iscomplete='NotAdded' style_item_product_id='" + t.style_item_product_id.ToString() + "' class='btn btn-secondary BtnStyleSetup'>View Style </button>" : ""),

                            t.style_product_type_id

                        }).ToList();

            var ret_obj = new AjaxResponse(filter_records.Count() > 0 ? filter_records[0].total_rows.Value : 0, data);

            return Json(ret_obj);

        }

        [HttpGet]
        public async Task<IActionResult> StyleSetupNewTab(string input)
        {

            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            tran_plan_allocate_DTO model = _mapper.Map<tran_plan_allocate_DTO>(temp_obj);

            if (model == null)
                model = new tran_plan_allocate_DTO();

            model.List_style = new List<tran_plan_allocate_style_DTO>();

            var product_size_list = JsonConvert.DeserializeObject<List<rpd_sp_get_style_group_size_by_fiscalyearid_DTO>>(temp_obj.str_style_size);

            model.List_product_size = product_size_list;

            for (int style_count = 1; style_count <= model.no_of_style; style_count++)
            {
                tran_plan_allocate_style_DTO objStyle = new tran_plan_allocate_style_DTO();
                objStyle.style_code = model.style_code_gen + "-" + style_count.ToString();

                model.List_style.Add(objStyle);
            }

            ViewBag.style_product = temp_obj.style_product;
            ViewBag.range_plan_qnty = temp_obj.range_plan_qnty;

            return PartialView("~/Views/PlanningAndAllocation/_StyleSetupNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> StyleSetupNew(string input)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {

            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            tran_plan_allocate_DTO model = _mapper.Map<tran_plan_allocate_DTO>(temp_obj);

            if (model == null)
                model = new tran_plan_allocate_DTO();

            model.List_style = new List<tran_plan_allocate_style_DTO>();

            var product_sub_category_List = await _IStyleitemproductsubcategoryService.GetAsync(model.style_item_product_id.Value);

            var category_List = (List<SelectListItem>)product_sub_category_List.ToList().Select(a =>
                                                 new SelectListItem
                                                 {
                                                     Text = a.sub_category_name,
                                                     Value = a.style_item_product_sub_category_id.ToString()
                                                 }
                                                 ).ToList();

            if (temp_obj.str_style_size is not null)
            {
                model.List_product_size = JsonConvert.DeserializeObject<List<rpd_sp_get_style_group_size_by_fiscalyearid_DTO>>(temp_obj.str_style_size);
            }
          
            List<lookup_trading_DTO> tradingList = await _rpc_db_service.GetAllTradingData(1);
             

            for (int style_count = 1; style_count <= model.no_of_style; style_count++)
            {
                tran_plan_allocate_style_DTO objStyle = new tran_plan_allocate_style_DTO();

                objStyle.style_code = model.style_code_gen;

                objStyle.List_ProductSubCategory = category_List;

                model.List_style.Add(objStyle);
            }

            ViewBag.style_product = temp_obj.style_product;

            ViewBag.range_plan_qnty = temp_obj.range_plan_qnty;

            Int64 total_style = 0;

            if (temp_obj.tran_va_plan_detl_id != null) //style_item_product_id
            {
                total_style = _tranvaplandetlstyleService.GetTotalAddedStyle(temp_obj.tran_va_plan_detl_id.Value, 0).Result.style_quantity.Value;
                ViewBag.no_of_style = (_tranvaplandetlstyleService.GetTotalAddedStyle(temp_obj.tran_va_plan_detl_id.Value, 0).Result.total_rows.Value + 1);
            }

            ViewBag.added_style = total_style;
            ViewBag.tradingList =tradingList.ToList().Select(a =>
                new SelectListItem
                {
                    Text = a.lookup_value.ToString(),
                    Value = a.lookup_id.ToString()
                }).ToList();
            ViewBag.remaining_style = temp_obj.range_plan_qnty - total_style;

            return PartialView("~/Views/PlanningAndAllocation/_StyleSetupNew.cshtml", model);

        }
        //

        [HttpGet]
        public async Task<IActionResult> DesignerAssignDetailsView(string fiscal_year_id, string event_id)
        {

            var event_id_filter = Convert.ToInt64((event_id));
            var fiscal_year_id_filter = Convert.ToInt64((fiscal_year_id));
            var List_products = await _rpc_db_service.GetAllva_plan_get_designer_assign_detailsAsync(fiscal_year_id_filter, event_id_filter);
            var List_Designer_Assign = await _rpc_db_service.GetAllva_plan_get_designer_assign_details_detAsync(fiscal_year_id_filter, event_id_filter);
            va_plan_get_designer_assign_details_DTO_master model = new va_plan_get_designer_assign_details_DTO_master();
            model.List_Products = List_products;
            model.List_Designer_Assign = List_Designer_Assign;
            var unique_designer_List = List_Designer_Assign.GroupBy(p =>
            new
            {
                p.designer_member_id,
                p.team_member_marketing_name
            })
            .Select(p => new rpc_va_plan_get_designer_assign_details_det_DTO
            {
                designer_member_id = p.Key.designer_member_id,
                team_member_marketing_name = p.Key.team_member_marketing_name
            }).ToList();

            model.List_Unique_Designer = unique_designer_List;
            return PartialView("~/Views/PlanningAndAllocation/_DesignerAssignDetailView.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> AssignDesignerAddUpdate(string input)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {

            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            tran_plan_allocate_DTO model = _mapper.Map<tran_plan_allocate_DTO>(temp_obj);

            if (model == null)
                model = new tran_plan_allocate_DTO();

            model.List_style = new List<tran_plan_allocate_style_DTO>();

            var product_sub_category_List = await _IStyleitemproductsubcategoryService.GetAsync(model.style_item_product_id.Value);

            var category_List = (List<SelectListItem>)product_sub_category_List.ToList().Select(a =>
                                                 new SelectListItem
                                                 {
                                                     Text = a.sub_category_name,
                                                     Value = a.style_item_product_sub_category_id.ToString()
                                                 }
                                                 ).ToList();

            var list_color_size = await _rpc_db_service.GetAllsp_get_color_detl_size_by_vaplandetlidAsync(model.tran_va_plan_detl_id);

            var ListStyle = list_color_size.Where(p => p.is_approved == 1).GroupBy(p =>
            new
            {
                p.tran_va_plan_detl_style_id,
                p.tran_va_plan_detl_id,
                p.style_code,
                p.style_quantity,
                p.style_embellishment_ids,
                p.no_of_color,
                p.color_code_gen,
                p.style_item_product_sub_category_id,
                p.designer_member_id,
                p.tran_sample_order_id,
                p.trading_type,
                p.trading_type_name

            })
            .Select(p => new tran_plan_allocate_style_DTO
            {
                tran_va_plan_detl_style_id = p.Key.tran_va_plan_detl_style_id,
                tran_va_plan_detl_id = p.Key.tran_va_plan_detl_id,
                style_code = p.Key.style_code,
                style_quantity = p.Key.style_quantity,
                style_embellishment_ids = JArray.Parse(p.Key.style_embellishment_ids),//"'" + p.Key.style_embellishment_ids + "'",
                List_Embellishment = JsonConvert.DeserializeObject<List<dropdown_entity>>(p.Key.style_embellishment_ids.ToString()),
                no_of_color = p.Key.no_of_color,
                color_code_gen = p.Key.color_code_gen,
                style_item_product_sub_category_id = p.Key.style_item_product_sub_category_id,
                designer_member_id = p.Key.designer_member_id,
                tran_sample_order_id=p.Key.tran_sample_order_id,
                trading_type=p.Key.trading_type,
                trading_type_name=p.Key.trading_type_name

            }).ToList();



            foreach (var singleStyle in ListStyle)
            {
                JArray jsonList = JArray.Parse(singleStyle.style_embellishment_ids.ToString());

                List<ddlEntity> embelishments = JsonConvert.DeserializeObject<List<ddlEntity>>(jsonList.ToString());

                var str_embelishments = String.Join(",", embelishments.ToList().Select(s => s.text));

                var Style_Wise_Color_List = list_color_size
                    .Where(q => q.tran_va_plan_detl_style_id == singleStyle.tran_va_plan_detl_style_id)
                    .GroupBy(p =>
                    new
                    {
                        p.tran_va_plan_detl_style_color_id,
                        p.tran_va_plan_detl_style_id,
                        p.color_code,
                        p.style_color_quantity
                    })
                .Select(p => new tran_plan_allocate_style_color_DTO
                {
                    tran_va_plan_detl_style_color_id = p.Key.tran_va_plan_detl_style_color_id,
                    tran_va_plan_detl_style_id = p.Key.tran_va_plan_detl_style_id,
                    color_code = p.Key.color_code,
                    style_color_quantity = p.Key.style_color_quantity
                }).ToList();

                foreach (var singlestylecolor in Style_Wise_Color_List)
                {
                    var Color_Wise_Size_List = list_color_size
                   .Where(q => q.tran_va_plan_detl_style_color_id == singlestylecolor.tran_va_plan_detl_style_color_id)
                   .GroupBy(p =>
                   new
                   {
                       p.tran_va_plan_detl_style_color_size_id,
                       p.tran_va_plan_detl_style_color_id,
                       p.style_product_size_group_detid,
                       p.style_product_size,
                       p.style_color_size_quantity
                   })
                   .Select(p => new tran_plan_allocate_style_color_size_DTO
                   {
                       tran_va_plan_detl_style_color_size_id = p.Key.tran_va_plan_detl_style_color_size_id,
                       tran_va_plan_detl_style_color_id = p.Key.tran_va_plan_detl_style_color_id,
                       style_product_size_group_detid = p.Key.style_product_size_group_detid,
                       style_product_size = p.Key.style_product_size,
                       style_color_size_quantity = p.Key.style_color_size_quantity
                   }).ToList();

                    singlestylecolor.List_ColorSizeInfo = Color_Wise_Size_List;
                }

                singleStyle.List_ColorInfo = Style_Wise_Color_List;
                singleStyle.List_ProductSubCategory = category_List;
            }

            model.List_style = ListStyle.OrderBy(p => p.tran_va_plan_detl_style_id).ToList();

            ViewBag.style_product = temp_obj.style_product;
            ViewBag.range_plan_qnty = temp_obj.range_plan_qnty;

            var MemberListDto = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

            model.List_team_members = _mapper.Map<List<owin_user_DTO>>(MemberListDto);

            var List_Designer_Assign = await _rpc_db_service.GetAllva_plan_get_designer_assign_details_det_by_detidAsync
                (model.tran_va_plan_detl_id.Value);

            model.List_Designer_Assign = List_Designer_Assign;

            return PartialView("~/Views/PlanningAndAllocation/_DesignerDistributionNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GetStyleDetails(string input)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {

            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            tran_plan_allocate_DTO model = _mapper.Map<tran_plan_allocate_DTO>(temp_obj);

            if (model == null)
                model = new tran_plan_allocate_DTO();

            model.List_style = new List<tran_plan_allocate_style_DTO>();

            var product_sub_category_List = await _IStyleitemproductsubcategoryService.GetAsync(model.style_item_product_id.Value);

            var category_List = (List<SelectListItem>)product_sub_category_List.ToList().Select(a =>
                                                 new SelectListItem
                                                 {
                                                     Text = a.sub_category_name,
                                                     Value = a.style_item_product_sub_category_id.ToString()
                                                 }
                                                 ).ToList();

            var list_color_size = await _rpc_db_service.GetAllsp_get_color_detl_size_by_vaplandetlidAsync(model.tran_va_plan_detl_id);

            var ListStyle = list_color_size.GroupBy(p =>
            new
            {
                p.tran_va_plan_detl_style_id,
                p.tran_va_plan_detl_id,
                p.style_code,
                p.style_quantity,
                p.style_embellishment_ids,
                p.no_of_color,
                p.color_code_gen,
                p.style_item_product_sub_category_id
            })
                .Select(p => new tran_plan_allocate_style_DTO
                {
                    tran_va_plan_detl_style_id = p.Key.tran_va_plan_detl_style_id,
                    tran_va_plan_detl_id = p.Key.tran_va_plan_detl_id,
                    style_code = p.Key.style_code,
                    style_quantity = p.Key.style_quantity,
                    style_embellishment_ids = JArray.Parse(p.Key.style_embellishment_ids),//"'" + p.Key.style_embellishment_ids + "'",

                    no_of_color = p.Key.no_of_color,
                    color_code_gen = p.Key.color_code_gen,
                    style_item_product_sub_category_id = p.Key.style_item_product_sub_category_id

                }).ToList();

            foreach (var singleStyle in ListStyle.ToList().OrderBy(p => p.tran_va_plan_detl_style_id))
            {

                var Style_Wise_Color_List = list_color_size
                    .Where(q => q.tran_va_plan_detl_style_id == singleStyle.tran_va_plan_detl_style_id)
                    .GroupBy(p =>
                    new
                    {
                        p.tran_va_plan_detl_style_color_id,
                        p.tran_va_plan_detl_style_id,
                        p.color_code,
                        p.style_color_quantity
                    })
                .Select(p => new tran_plan_allocate_style_color_DTO
                {
                    tran_va_plan_detl_style_color_id = p.Key.tran_va_plan_detl_style_color_id,
                    tran_va_plan_detl_style_id = p.Key.tran_va_plan_detl_style_id,
                    color_code = p.Key.color_code,
                    style_color_quantity = p.Key.style_color_quantity
                }).ToList();

                foreach (var singlestylecolor in Style_Wise_Color_List)
                {
                    var Color_Wise_Size_List = list_color_size
                   .Where(q => q.tran_va_plan_detl_style_color_id == singlestylecolor.tran_va_plan_detl_style_color_id)
                   .GroupBy(p =>
                   new
                   {
                       p.tran_va_plan_detl_style_color_size_id,
                       p.tran_va_plan_detl_style_color_id,
                       p.style_product_size_group_detid,
                       p.style_product_size,
                       p.style_color_size_quantity
                   })
                   .Select(p => new tran_plan_allocate_style_color_size_DTO
                   {
                       tran_va_plan_detl_style_color_size_id = p.Key.tran_va_plan_detl_style_color_size_id,
                       tran_va_plan_detl_style_color_id = p.Key.tran_va_plan_detl_style_color_id,
                       style_product_size_group_detid = p.Key.style_product_size_group_detid,
                       style_product_size = p.Key.style_product_size,
                       style_color_size_quantity = p.Key.style_color_size_quantity
                   }).ToList();

                    singlestylecolor.List_ColorSizeInfo = Color_Wise_Size_List;
                }

                singleStyle.List_ColorInfo = Style_Wise_Color_List;
                singleStyle.List_Embellishment = JsonConvert.DeserializeObject<List<dropdown_entity>>(singleStyle.style_embellishment_ids.ToString());
                singleStyle.List_ProductSubCategory = category_List;
            }

            model.List_style = ListStyle.OrderBy(p => p.tran_va_plan_detl_style_id).ToList();

            ViewBag.style_product = temp_obj.style_product;
            ViewBag.range_plan_qnty = temp_obj.range_plan_qnty;

            return PartialView("~/Views/PlanningAndAllocation/_StyleDetail.cshtml", model);

        }
        [HttpGet]
        public async Task<IActionResult> StyleSetupView(string input)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {

            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            tran_plan_allocate_DTO model = _mapper.Map<tran_plan_allocate_DTO>(temp_obj);

            if (model == null)
                model = new tran_plan_allocate_DTO();

            model.List_style = new List<tran_plan_allocate_style_DTO>();

            var product_sub_category_List = await _IStyleitemproductsubcategoryService.GetAsync(model.style_item_product_id.Value);

            var category_List = (List<SelectListItem>)product_sub_category_List.ToList().Select(a =>
                                                 new SelectListItem
                                                 {
                                                     Text = a.sub_category_name,
                                                     Value = a.style_item_product_sub_category_id.ToString()
                                                 }
                                                 ).ToList();

            var list_color_size = await _rpc_db_service.GetAllsp_get_color_detl_size_by_style_id(temp_obj.tran_va_plan_detl_style_id);

            var ListStyle = list_color_size
                .GroupBy(p =>
            new
            {

                p.tran_va_plan_detl_style_id,
                p.tran_va_plan_detl_id,
                p.style_code,
                p.style_quantity,
                p.style_embellishment_ids,
                p.no_of_color,
                p.color_code_gen,
                p.style_item_product_sub_category_id,
                p.is_submitted,
                p.is_approved,
                p.trading_type
            })
                .Select(p => new tran_plan_allocate_style_DTO
                {
                    tran_va_plan_detl_style_id = p.Key.tran_va_plan_detl_style_id,
                    tran_va_plan_detl_id = p.Key.tran_va_plan_detl_id,
                    style_code = p.Key.style_code,
                    style_quantity = p.Key.style_quantity,
                    style_embellishment_ids = JArray.Parse(p.Key.style_embellishment_ids),//"'" + p.Key.style_embellishment_ids + "'",
                    no_of_color = p.Key.no_of_color,
                    color_code_gen = p.Key.color_code_gen,
                    style_item_product_sub_category_id = p.Key.style_item_product_sub_category_id,
                    is_submitted = p.Key.is_submitted,
                    is_approved = p.Key.is_approved,
                    trading_type=p.Key.trading_type
    }).ToList();
            List<lookup_trading_DTO> tradingList = await _rpc_db_service.GetAllTradingData(1);
            ViewBag.tradingList = tradingList.ToList().Select(a =>
              new SelectListItem
              {
                  Text = a.lookup_value.ToString(),
                  Value = a.lookup_id.ToString()
              }).ToList();
            foreach (var singleStyle in ListStyle.ToList().OrderBy(p => p.tran_va_plan_detl_style_id))
            {
                singleStyle.is_delete = 0;
                singleStyle.is_submitted = singleStyle.is_submitted == null ? 0 : singleStyle.is_submitted;
                singleStyle.is_approved = singleStyle.is_approved == null ? 0 : singleStyle.is_approved;

                var Style_Wise_Color_List = list_color_size
                    .Where(q => q.tran_va_plan_detl_style_id == singleStyle.tran_va_plan_detl_style_id)
                    .GroupBy(p =>
                    new
                    {
                        p.tran_va_plan_detl_style_color_id,
                        p.tran_va_plan_detl_style_id,
                        p.color_code,
                        p.style_color_quantity
                    })
                .Select(p => new tran_plan_allocate_style_color_DTO
                {
                    tran_va_plan_detl_style_color_id = p.Key.tran_va_plan_detl_style_color_id,
                    tran_va_plan_detl_style_id = p.Key.tran_va_plan_detl_style_id,
                    color_code = p.Key.color_code,
                    style_color_quantity = p.Key.style_color_quantity
                }).ToList();

                foreach (var singlestylecolor in Style_Wise_Color_List)
                {
                    var Color_Wise_Size_List = list_color_size
                   .Where(q => q.tran_va_plan_detl_style_color_id == singlestylecolor.tran_va_plan_detl_style_color_id)
                   .GroupBy(p =>
                   new
                   {
                       p.tran_va_plan_detl_style_color_size_id,
                       p.tran_va_plan_detl_style_color_id,
                       p.style_product_size_group_detid,
                       p.style_product_size,
                       p.style_color_size_quantity
                   })
                   .Select(p => new tran_plan_allocate_style_color_size_DTO
                   {
                       tran_va_plan_detl_style_color_size_id = p.Key.tran_va_plan_detl_style_color_size_id,
                       tran_va_plan_detl_style_color_id = p.Key.tran_va_plan_detl_style_color_id,
                       style_product_size_group_detid = p.Key.style_product_size_group_detid,
                       style_product_size = p.Key.style_product_size,
                       style_color_size_quantity = p.Key.style_color_size_quantity
                   }).ToList();

                    singlestylecolor.List_ColorSizeInfo = Color_Wise_Size_List;
                }

                singleStyle.List_ColorInfo = Style_Wise_Color_List;
                singleStyle.List_Embellishment = JsonConvert.DeserializeObject<List<dropdown_entity>>(singleStyle.style_embellishment_ids.ToString());
                singleStyle.List_ProductSubCategory = category_List;
            }

            model.List_style = ListStyle.OrderBy(p => p.tran_va_plan_detl_style_id)
                .Where(p => p.tran_va_plan_detl_style_id == temp_obj.tran_va_plan_detl_style_id).ToList();

            ViewBag.style_product = temp_obj.style_product;
            ViewBag.range_plan_qnty = temp_obj.range_plan_qnty;
            ViewBag.is_view = temp_obj.is_view;
            ViewBag.current_style_id = temp_obj.tran_va_plan_detl_style_id;

            if (temp_obj.is_view == 1)
                return PartialView("~/Views/PlanningAndAllocation/_StyleSetupView.cshtml", model);
            else
                return PartialView("~/Views/PlanningAndAllocation/_StyleSetupEdit.cshtml", model);

        }


        [HttpGet]
        public async Task<IActionResult> StyleSetupEdit(string input)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {

            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            tran_plan_allocate_DTO model = _mapper.Map<tran_plan_allocate_DTO>(temp_obj);

            if (model == null)
                model = new tran_plan_allocate_DTO();

            model.List_style = new List<tran_plan_allocate_style_DTO>();

            var product_sub_category_List = await _IStyleitemproductsubcategoryService.GetAsync(model.style_item_product_id.Value);

            var category_List = (List<SelectListItem>)product_sub_category_List.ToList().Select(a =>
                                                 new SelectListItem
                                                 {
                                                     Text = a.sub_category_name,
                                                     Value = a.style_item_product_sub_category_id.ToString()
                                                 }
                                                 ).ToList();

            var list_color_size = await _rpc_db_service.GetAllsp_get_color_detl_size_by_style_id(temp_obj.tran_va_plan_detl_style_id);

            var ListStyle = list_color_size.GroupBy(p =>
            new
            {

                p.tran_va_plan_detl_style_id,
                p.tran_va_plan_detl_id,
                p.style_code,
                p.style_quantity,
                p.style_embellishment_ids,
                p.no_of_color,
                p.color_code_gen,
                p.style_item_product_sub_category_id,
                p.is_submitted,
                p.is_approved
            })
                .Select(p => new tran_plan_allocate_style_DTO
                {
                    tran_va_plan_detl_style_id = p.Key.tran_va_plan_detl_style_id,
                    tran_va_plan_detl_id = p.Key.tran_va_plan_detl_id,
                    style_code = p.Key.style_code,
                    style_quantity = p.Key.style_quantity,
                    style_embellishment_ids = JArray.Parse(p.Key.style_embellishment_ids),//"'" + p.Key.style_embellishment_ids + "'",
                    no_of_color = p.Key.no_of_color,
                    color_code_gen = p.Key.color_code_gen,
                    style_item_product_sub_category_id = p.Key.style_item_product_sub_category_id,
                    is_submitted = p.Key.is_submitted,
                    is_approved = p.Key.is_approved
                }).ToList();

            foreach (var singleStyle in ListStyle.ToList().OrderBy(p => p.tran_va_plan_detl_style_id))
            {
                singleStyle.is_delete = 0;
                singleStyle.is_submitted = singleStyle.is_submitted == null ? 0 : singleStyle.is_submitted;
                singleStyle.is_approved = singleStyle.is_approved == null ? 0 : singleStyle.is_approved;

                var Style_Wise_Color_List = list_color_size
                    .Where(q => q.tran_va_plan_detl_style_id == singleStyle.tran_va_plan_detl_style_id)
                    .GroupBy(p =>
                    new
                    {
                        p.tran_va_plan_detl_style_color_id,
                        p.tran_va_plan_detl_style_id,
                        p.color_code,
                        p.style_color_quantity
                    })
                .Select(p => new tran_plan_allocate_style_color_DTO
                {
                    tran_va_plan_detl_style_color_id = p.Key.tran_va_plan_detl_style_color_id,
                    tran_va_plan_detl_style_id = p.Key.tran_va_plan_detl_style_id,
                    color_code = p.Key.color_code,
                    style_color_quantity = p.Key.style_color_quantity
                }).ToList();

                foreach (var singlestylecolor in Style_Wise_Color_List)
                {
                    var Color_Wise_Size_List = list_color_size
                   .Where(q => q.tran_va_plan_detl_style_color_id == singlestylecolor.tran_va_plan_detl_style_color_id)
                   .GroupBy(p =>
                   new
                   {
                       p.tran_va_plan_detl_style_color_size_id,
                       p.tran_va_plan_detl_style_color_id,
                       p.style_product_size_group_detid,
                       p.style_product_size,
                       p.style_color_size_quantity
                   })
                   .Select(p => new tran_plan_allocate_style_color_size_DTO
                   {
                       tran_va_plan_detl_style_color_size_id = p.Key.tran_va_plan_detl_style_color_size_id,
                       tran_va_plan_detl_style_color_id = p.Key.tran_va_plan_detl_style_color_id,
                       style_product_size_group_detid = p.Key.style_product_size_group_detid,
                       style_product_size = p.Key.style_product_size,
                       style_color_size_quantity = p.Key.style_color_size_quantity
                   }).ToList();

                    singlestylecolor.List_ColorSizeInfo = Color_Wise_Size_List;
                }

                singleStyle.List_ColorInfo = Style_Wise_Color_List;
                singleStyle.List_Embellishment = JsonConvert.DeserializeObject<List<dropdown_entity>>(singleStyle.style_embellishment_ids.ToString());
                singleStyle.List_ProductSubCategory = category_List;
            }

            model.List_style = ListStyle.OrderBy(p => p.tran_va_plan_detl_style_id).ToList();

            ViewBag.style_product = temp_obj.style_product;
            ViewBag.range_plan_qnty = temp_obj.range_plan_qnty;
            ViewBag.is_view = temp_obj.is_view;
            ViewBag.current_style_id = temp_obj.tran_va_plan_detl_style_id;

            return PartialView("~/Views/PlanningAndAllocation/_StyleSetupEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> StyleSetupApprove(string input)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {

            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            tran_plan_allocate_DTO model = _mapper.Map<tran_plan_allocate_DTO>(temp_obj);

            if (model == null)
                model = new tran_plan_allocate_DTO();

            model.List_style = new List<tran_plan_allocate_style_DTO>();

            var product_sub_category_List = await _IStyleitemproductsubcategoryService.GetAsync(model.style_item_product_id.Value);

            var category_List = (List<SelectListItem>)product_sub_category_List.ToList().Select(a =>
                                                 new SelectListItem
                                                 {
                                                     Text = a.sub_category_name,
                                                     Value = a.style_item_product_sub_category_id.ToString()
                                                 }
                                                 ).ToList();

            var list_color_size = await _rpc_db_service.GetAllsp_get_color_detl_size_by_style_id(temp_obj.tran_va_plan_detl_style_id);

            var ListStyle = list_color_size.GroupBy(p =>
            new
            {

                p.tran_va_plan_detl_style_id,
                p.tran_va_plan_detl_id,
                p.style_code,
                p.style_quantity,
                p.style_embellishment_ids,
                p.no_of_color,
                p.color_code_gen,
                p.style_item_product_sub_category_id,
                p.is_submitted,
                p.trading_type_name
            })
                .Select(p => new tran_plan_allocate_style_DTO
                {
                    tran_va_plan_detl_style_id = p.Key.tran_va_plan_detl_style_id,
                    tran_va_plan_detl_id = p.Key.tran_va_plan_detl_id,
                    style_code = p.Key.style_code,
                    style_quantity = p.Key.style_quantity,
                    style_embellishment_ids = JArray.Parse(p.Key.style_embellishment_ids),//"'" + p.Key.style_embellishment_ids + "'",
                    no_of_color = p.Key.no_of_color,
                    color_code_gen = p.Key.color_code_gen,
                    style_item_product_sub_category_id = p.Key.style_item_product_sub_category_id,
                    is_submitted = p.Key.is_submitted,
                    trading_type_name = p.Key.trading_type_name
                }).ToList();

            foreach (var singleStyle in ListStyle.ToList().OrderBy(p => p.tran_va_plan_detl_style_id))
            {
                singleStyle.is_delete = 0;
                singleStyle.is_submitted = singleStyle.is_submitted == null ? 0 : singleStyle.is_submitted;
                singleStyle.is_approved = singleStyle.is_approved == null ? 0 : singleStyle.is_approved;

                var Style_Wise_Color_List = list_color_size
                    .Where(q => q.tran_va_plan_detl_style_id == singleStyle.tran_va_plan_detl_style_id)
                    .GroupBy(p =>
                    new
                    {
                        p.tran_va_plan_detl_style_color_id,
                        p.tran_va_plan_detl_style_id,
                        p.color_code,
                        p.style_color_quantity
                    })
                .Select(p => new tran_plan_allocate_style_color_DTO
                {
                    tran_va_plan_detl_style_color_id = p.Key.tran_va_plan_detl_style_color_id,
                    tran_va_plan_detl_style_id = p.Key.tran_va_plan_detl_style_id,
                    color_code = p.Key.color_code,
                    style_color_quantity = p.Key.style_color_quantity
                }).ToList();

                foreach (var singlestylecolor in Style_Wise_Color_List)
                {
                    var Color_Wise_Size_List = list_color_size
                   .Where(q => q.tran_va_plan_detl_style_color_id == singlestylecolor.tran_va_plan_detl_style_color_id)
                   .GroupBy(p =>
                   new
                   {
                       p.tran_va_plan_detl_style_color_size_id,
                       p.tran_va_plan_detl_style_color_id,
                       p.style_product_size_group_detid,
                       p.style_product_size,
                       p.style_color_size_quantity
                   })
                   .Select(p => new tran_plan_allocate_style_color_size_DTO
                   {
                       tran_va_plan_detl_style_color_size_id = p.Key.tran_va_plan_detl_style_color_size_id,
                       tran_va_plan_detl_style_color_id = p.Key.tran_va_plan_detl_style_color_id,
                       style_product_size_group_detid = p.Key.style_product_size_group_detid,
                       style_product_size = p.Key.style_product_size,
                       style_color_size_quantity = p.Key.style_color_size_quantity
                   }).ToList();

                    singlestylecolor.List_ColorSizeInfo = Color_Wise_Size_List;
                }

                singleStyle.List_ColorInfo = Style_Wise_Color_List;
                singleStyle.List_Embellishment = JsonConvert.DeserializeObject<List<dropdown_entity>>(singleStyle.style_embellishment_ids.ToString());
                singleStyle.List_ProductSubCategory = category_List;
            }

            model.List_style = ListStyle.OrderBy(p => p.tran_va_plan_detl_style_id).ToList();

            ViewBag.style_product = temp_obj.style_product;
            ViewBag.range_plan_qnty = temp_obj.range_plan_qnty;
            ViewBag.is_view = temp_obj.is_view;
            ViewBag.current_style_id = temp_obj.tran_va_plan_detl_style_id;
            ViewBag.style_code_gen = temp_obj.style_code_gen;

            return PartialView("~/Views/PlanningAndAllocation/_StyleSetupApprove.cshtml", model);

        }


        [HttpGet]
        public async Task<IActionResult> LoadAllStyle(Int64 style_item_product_id, Int64 tran_va_plan_detl_id)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {

            tran_plan_allocate_DTO model = new tran_plan_allocate_DTO();

            model.List_style = new List<tran_plan_allocate_style_DTO>();

            var product_sub_category_List = await _IStyleitemproductsubcategoryService.GetAsync(style_item_product_id);

            var category_List = (List<SelectListItem>)product_sub_category_List.ToList().Select(a =>
                                                 new SelectListItem
                                                 {
                                                     Text = a.sub_category_name,
                                                     Value = a.style_item_product_sub_category_id.ToString()
                                                 }
                                                 ).ToList();

            var list_color_size = await _rpc_db_service.GetAllsp_get_color_detl_size_by_vaplandetlidAsync(tran_va_plan_detl_id);

            var ListStyle = list_color_size.GroupBy(p =>
            new
            {
                p.is_submitted,
                p.is_approved,
                p.tran_va_plan_detl_style_id,
                p.tran_va_plan_detl_id,
                p.style_code,
                p.style_quantity,
                p.style_embellishment_ids,
                p.no_of_color,
                p.color_code_gen,
                p.style_item_product_sub_category_id
            })
                .Select(p => new tran_plan_allocate_style_DTO
                {
                    is_approved = p.Key.is_approved,
                    is_submitted = p.Key.is_submitted,
                    tran_va_plan_detl_style_id = p.Key.tran_va_plan_detl_style_id,
                    tran_va_plan_detl_id = p.Key.tran_va_plan_detl_id,
                    style_code = p.Key.style_code,
                    style_quantity = p.Key.style_quantity,
                    style_embellishment_ids = JArray.Parse(p.Key.style_embellishment_ids),//"'" + p.Key.style_embellishment_ids + "'",

                    no_of_color = p.Key.no_of_color,
                    color_code_gen = p.Key.color_code_gen,
                    style_item_product_sub_category_id = p.Key.style_item_product_sub_category_id

                }).ToList();

            foreach (var singleStyle in ListStyle.ToList().OrderBy(p => p.tran_va_plan_detl_style_id))
            {

                var Style_Wise_Color_List = list_color_size
                    .Where(q => q.tran_va_plan_detl_style_id == singleStyle.tran_va_plan_detl_style_id)
                    .GroupBy(p =>
                    new
                    {
                        p.tran_va_plan_detl_style_color_id,
                        p.tran_va_plan_detl_style_id,
                        p.color_code,
                        p.style_color_quantity
                    })
                .Select(p => new tran_plan_allocate_style_color_DTO
                {
                    tran_va_plan_detl_style_color_id = p.Key.tran_va_plan_detl_style_color_id,
                    tran_va_plan_detl_style_id = p.Key.tran_va_plan_detl_style_id,
                    color_code = p.Key.color_code,
                    style_color_quantity = p.Key.style_color_quantity
                }).ToList();

                foreach (var singlestylecolor in Style_Wise_Color_List)
                {
                    var Color_Wise_Size_List = list_color_size
                   .Where(q => q.tran_va_plan_detl_style_color_id == singlestylecolor.tran_va_plan_detl_style_color_id)
                   .GroupBy(p =>
                   new
                   {
                       p.tran_va_plan_detl_style_color_size_id,
                       p.tran_va_plan_detl_style_color_id,
                       p.style_product_size_group_detid,
                       p.style_product_size,
                       p.style_color_size_quantity
                   })
                   .Select(p => new tran_plan_allocate_style_color_size_DTO
                   {
                       tran_va_plan_detl_style_color_size_id = p.Key.tran_va_plan_detl_style_color_size_id,
                       tran_va_plan_detl_style_color_id = p.Key.tran_va_plan_detl_style_color_id,
                       style_product_size_group_detid = p.Key.style_product_size_group_detid,
                       style_product_size = p.Key.style_product_size,
                       style_color_size_quantity = p.Key.style_color_size_quantity
                   }).ToList();

                    singlestylecolor.List_ColorSizeInfo = Color_Wise_Size_List;
                }

                singleStyle.List_ColorInfo = Style_Wise_Color_List;
                singleStyle.List_Embellishment = JsonConvert.DeserializeObject<List<dropdown_entity>>(singleStyle.style_embellishment_ids.ToString());
                singleStyle.List_ProductSubCategory = category_List;
            }

            model.List_style = ListStyle.OrderBy(p => p.tran_va_plan_detl_style_id).ToList();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                WriteIndented = true // Optional: formatting for readability
            };

            return new JsonResult(model, options);

        }


        [HttpPost]
        public async Task<IActionResult> SavePlanningAndAllocation([FromBody] tran_plan_allocate_DTO request)
        {

            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var ret = await _TranPlanAllocateService.SaveAsync(request);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? "Style Added Successful" : "Style Add Failed")
            });

        }

        [HttpPost]
        public async Task<IActionResult> UpdatePlanningAndAllocation([FromBody] tran_plan_allocate_DTO request)
        {

            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.fiscal_year_id = Fiscal_Year_ID;

            request.event_id = Event_ID;

            var ret = await _TranPlanAllocateService.UpdateAsync(request);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? "Style Updated Successful" : "Style Update Failed")
            });

        }

        [HttpPost]
        public async Task<IActionResult> ApproveRejectStyle([FromBody] tran_plan_allocate_style_DTO request)
        {

            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.approved_by = request.added_by;
            request.approve_date = DateTime.Now;

            var ret = await _tranvaplandetlstyleService.tran_plan_allocate_style_approve_reject(request);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? "Value Addition Plan Successful" : "Value Addition Plan Data Saving Failed")
            });

        }


        [HttpPost]
        public async Task<IActionResult> DeleteStyle([FromBody] tran_plan_allocate_style_DTO request)
        {

            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            var ret = await _tranvaplandetlstyleService.DeleteAsync(request.tran_va_plan_detl_style_id.Value);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? "Value Addition Plan Successful" : "Value Addition Plan Data Saving Failed")
            });

        }


        private string GetPlanStatus(rpc_sp_get_tran_va_plan_summary_DTO obj)
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
        public async Task<IActionResult> SaveDesignerAssign([FromBody] tran_plan_allocate_DTO request)
        {


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            var objList_style = await _tranvaplandetlstyleService.GetStyleListByPlanDetlID(request.tran_va_plan_detl_id.Value);

            var List_style = _mapper.Map<List<tran_plan_allocate_style_entity>>(objList_style);

            foreach (var style in List_style)
            {
                if (request.List_style.Where(p => p.tran_va_plan_detl_style_id == style.tran_va_plan_detl_style_id).ToList().Count > 0)
                {
                    style.designer_member_id = request.List_style.
                        Where(p => p.tran_va_plan_detl_style_id == style.tran_va_plan_detl_style_id).ToList().
                            FirstOrDefault().designer_member_id;

                    style.added_by = request.added_by;

                    style.date_added = request.date_added;
                }
            }

            var ret = await _tranvaplandetlstyleService.DesignerAssignListAsync(List_style);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? "Designer Assigned Successful" : "Designer Assign Failed")
            });

        }


    }
}
