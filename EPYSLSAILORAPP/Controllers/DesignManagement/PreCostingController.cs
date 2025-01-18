using AutoMapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.Security;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
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
using System.Web.Helpers;
using Web.Core.Frame.Helpers;


namespace EPYSLSAILORAPP.Controllers.DesignManagement
{
    [RequestFormLimits(MultipartBodyLengthLimit = 2000000000)]
    [RequestSizeLimit(2000000000)]
   
    public class PreCostingController : ExtendedBaseController
    {
        private readonly ILogger<PreCostingController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBusinessPlanService _BP_service;
        private readonly IGenProcessMasterService _GenProcessMasterService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        private readonly IStylecategoryService _StylecategoryService;
        private readonly IStylegenderService _StylegenderService;
        private readonly IStyleitemoriginService _StyleitemoriginService;
        private readonly IStyleitemproductService _StyleitemproductService;
        private readonly IStyleitemtypeService _StyleitemtypeService;
        private readonly IStylelabelService _StylelabelService;
        private readonly IStyleproductunitService _StyleproductunitService;
        private readonly IStyleitemproductsubcategoryService _IStyleitemproductsubcategoryService;
        private readonly IStylemastercategoryService _StylemastercategoryService;
        private readonly IStyleproducttypeService _StyleproducttypeService;

        private readonly ITranPreCostingReviewService _TranPreCostingReviewService;

        private readonly IGenItemStructureGroupSubSegmentMappingService _GenItemStructureGroupSubSegmentMappingService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;

        // private readonly IStyleembellishmentService _StyleembellishmentService;
        private readonly IStyleproductsizegroupdetailsService _StyleproductsizegroupdetailsService;


        //private readonly ITranvaplandetlService _TranvaplandetlService;
        //
        private readonly ITranPlanAllocateService _TranPlanAllocateService;
        private readonly ITransampleorderService _TransampleorderService;
        private readonly ITransampleorderembellishmentService _TransampleorderembellishmentService;

        private readonly ITransampleorderdetlService _TransampleorderdetlService;
        //  private readonly ITranvaproductembellishmentmappingService _TranvaproductembellishmentmappingService;

        private readonly ITranpreconstingService _TranpreconstingService;

        private readonly IGenOutletService _gen_outlet_entity_service;
        private readonly IGenUnitService _GenUnitService;
        private readonly IGenItemStructureGroupSubService _IGenItemStructureGroupSubService;
        private readonly IGenPriorityService _gen_priority_service;
        private readonly ITran_BP_YearService _tran_bp_yearservice;

        private readonly ITran_BP_EventMonthService _tran_bp_eventmonthservice;
        private readonly ITran_BP_EventMonthOutletService _tran_bp_eventmonthoutletservice;

        private readonly ITranrangeplandetailssizeService _tran_rangeplandetailssizeservice;

        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private readonly IOwinUserService _OwinUserService;

        private readonly ITranrangeplanService _tran_rangeplanservice;
        private readonly ITranPlanAllocateStyleService _tranvaplandetlstyleService;

        private readonly IRPCDbService _rpc_db_service;

        private readonly IGenSeasonEventConfigurationService _genseasoneventconfigurationservice;


        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        private IRedisService<rpc_range_plan_getfor_createupdate_DTO> _redisRangePlanService;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside SampleOrder !");
            return View();
        }

        public PreCostingController(IBusinessPlanService BusinessPlanService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            ITranPreCostingReviewService TranPreCostingReviewService,
            IGenFiscalYearService gen_fiscal_year_Service, IGenOutletService gen_outlet_entity_service, ITran_BP_YearService tran_bp_yearservice,
            ITran_BP_EventMonthService tran_bp_eventmonthservice, ITran_BP_EventMonthOutletService tran_bp_eventmonthoutletservice,
            IMapper mapper, ILogger<PreCostingController> logger, IHttpContextAccessor contextAccessor,
            IStylecategoryService StylecategoryService,
            IStylegenderService StylegenderService,
            IStyleitemoriginService StyleitemoriginService,
            IStyleitemproductService StyleitemproductService,
            IStyleitemtypeService StyleitemtypeService,

            //ITranvaplandetlService TranvaplandetlService,
            //
            ITranPlanAllocateService TranPlanAllocateService,
            IStyleproductunitService StyleproductunitService,
            //ITranvaproductembellishmentmappingService TranvaproductembellishmentmappingService,
            //IStyleembellishmentService StyleembellishmentService,
            IStyleitemproductsubcategoryService IStyleitemproductsubcategoryService,
            IStylelabelService StylelabelService, ITranrangeplandetailssizeService tran_rangeplandetailssizeservice,
            ITranPlanAllocateStyleService tranvaplandetlstyleService,
            IStylemastercategoryService StylemastercategoryService,
            ITransampleorderdetlService TransampleorderdetlService,
             IGenProcessMasterService GenProcessMasterService,
             ITranpreconstingService TranpreconstingService,
            IGenItemStructureGroupSubSegmentMappingService GenItemStructureGroupSubSegmentMappingService,
            IStyleproductsizegroupdetailsService StyleproductsizegroupdetailsService,
            ITransampleorderembellishmentService TransampleorderembellishmentService,
            IStyleproducttypeService StyleproducttypeService, ITranrangeplanService tran_rangeplanservice
            , IGenSeasonEventConfigurationService genseasoneventconfigurationservice, IOwinUserService OwinUserService
            , IRPCDbService rpc_db_service, IGenPriorityService gen_priority_service, IConfiguration configuration,
            ITransampleorderService transampleorderService, IGenItemStructureGroupSubService IGenItemStructureGroupSubService, IGenUnitService GenUnitService) : base(contextAccessor, configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _BP_service = BusinessPlanService;
            _gen_fiscal_year_Service = gen_fiscal_year_Service;
            _gen_outlet_entity_service = gen_outlet_entity_service;
            _tran_bp_yearservice = tran_bp_yearservice;
            _mapper = mapper;
            _logger = logger;
            _tran_rangeplanservice = tran_rangeplanservice;
            _genseasoneventconfigurationservice = genseasoneventconfigurationservice;

            _TranPreCostingReviewService = TranPreCostingReviewService;
            // _StyleembellishmentService = StyleembellishmentService;
            _IStyleitemproductsubcategoryService = IStyleitemproductsubcategoryService;
            _rpc_db_service = rpc_db_service;
            _tran_bp_eventmonthservice = tran_bp_eventmonthservice;
            _tran_bp_eventmonthoutletservice = tran_bp_eventmonthoutletservice;
            _tranvaplandetlstyleService = tranvaplandetlstyleService;
            _OwinUserService = OwinUserService;

            _contextAccessor = contextAccessor;
            _StyleproductunitService = StyleproductunitService;
            _tran_rangeplandetailssizeservice = tran_rangeplandetailssizeservice;
            _TranpreconstingService = TranpreconstingService;


            //_TranvaplandetlService = TranvaplandetlService;
            //_TranvaplaneventsService = TranvaplaneventsService;
            _TranPlanAllocateService = TranPlanAllocateService;
            // _TranvaproductembellishmentmappingService = TranvaproductembellishmentmappingService;
            _StyleproductsizegroupdetailsService = StyleproductsizegroupdetailsService;
            _GenItemStructureGroupSubSegmentMappingService = GenItemStructureGroupSubSegmentMappingService;


            _StylecategoryService = StylecategoryService;
            _StylegenderService = StylegenderService;
            _StyleitemoriginService = StyleitemoriginService;
            _StyleitemproductService = StyleitemproductService;
            _StyleitemtypeService = StyleitemtypeService;
            _StylelabelService = StylelabelService;
            _StylemastercategoryService = StylemastercategoryService;
            _StyleproducttypeService = StyleproducttypeService;
            _TransampleorderdetlService = TransampleorderdetlService;

            _logger.LogInformation("Hello from inside SampleOrder !");
            _gen_priority_service = gen_priority_service;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);

            _GenProcessMasterService = GenProcessMasterService;

            _redisRangePlanService = new RedisService<rpc_range_plan_getfor_createupdate_DTO>(_configuration);


            _TransampleorderService = transampleorderService;
            _IGenItemStructureGroupSubService = IGenItemStructureGroupSubService;
            _TransampleorderembellishmentService = TransampleorderembellishmentService;

            _GenUnitService = GenUnitService;
        }


        [HttpGet]
        public async Task<IActionResult> PreCostingCreate()//(string fiscal_year_id, string eventid, string range_plan_id, string tran_va_plan_event_id)
        {
            tran_va_plan_DTO model = new tran_va_plan_DTO();

            #region

            //var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            //List<Claim> listClaims = (List<Claim>)claim.Claims;

            //SecurityCapsule sec_object = new SecurityCapsule();

            //if (listClaims.Count > 0)
            //{
            //    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

            //    sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);
            //}

            //if (!listClaims.Exists(c => c.Type == "secobject"))
            //{
            //    Response.Redirect("/account/LogOff", true);
            //}
            //else if (sec_object.gen_team_group_id != Convert.ToInt64(Team_Category.Designer) &&
            //    sec_object.roleid != Convert.ToInt64(Enum_Role.Admin))
            //{
            //    ViewBag.IsNotDesigner = "This page is only for the Designer";
            //}
            #endregion

            return View("~/Views/DesignMgt/PreCosting/PreCostingCreate.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> PreCostingApprovalLanding()//(string fiscal_year_id, string eventid, string range_plan_id, string tran_va_plan_event_id)
        {
            tran_va_plan_DTO model = new tran_va_plan_DTO();

            #region commentSectionForFilteringController

            //var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            //List<Claim> listClaims = (List<Claim>)claim.Claims;

            //SecurityCapsule sec_object = new SecurityCapsule();

            //if (listClaims.Count > 0)
            //{
            //    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

            //    sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);
            //}

            //if (!listClaims.Exists(c => c.Type == "secobject"))
            //{
            //    Response.Redirect("/account/LogOff", true);
            //}
            #endregion



            return View("~/Views/DesignMgt/PreCosting/PreCostingApprovalLanding.cshtml", model);
        }



        private string GetHtmlPreCostingTableHTML(Int64 tran_va_plan_detl_id,
           List<rpc_sp_get_pre_costing_details_DTO> sample_order_details_List, List<gen_unit_entity> unit_list)
        {
            var rowindex = 1;

            foreach (var obj in sample_order_details_List)
            {
                obj.RowNumber = rowindex++;
                obj.str_delivery_unit = unit_list.Where(p => p.gen_unit_id == obj.delivery_unit_id).FirstOrDefault().unit_name;
            }


            var html = clsUtil.RenderViewAsync(this,
                            "_PreCostingList",
                            sample_order_details_List.Where(p => p.tran_va_plan_detl_id == tran_va_plan_detl_id).ToList(), true).Result;


            return html;

        }

        private string GetHtmlSampleOrderTableHTML(rpc_sp_get_data_for_pre_costing_DTO sample_order)
        {
            string strhtml = "<div class='row'>";

            try
            {
                if (!string.IsNullOrEmpty(sample_order.sample_photos))
                {
                    var temparray = JArray.Parse(sample_order.sample_photos);

                    sample_order.sample_photos_List = JArray.Parse(sample_order.sample_photos).ToObject<List<file_upload>>();

                    strhtml += @"<div class='col-lg-12'>
                                    <div class=''>
                                    <img class='grid_img' src='../Images/loading-icon-file.gif'  tran_sample_order_id='" + sample_order.tran_sample_order_id + @"'  onclick='ShowImage(this);'  style='width:50px;height:40px;' />
                                    <a class='anch_link' onclick='EditSampleOrder(this," + sample_order.tran_sample_order_id.ToString() + ")' style='font-size:12px;'>" + sample_order.tran_sample_order_number + @"</a>
                                    " + (!string.IsNullOrEmpty(sample_order.version_no) ? "(" + sample_order.version_no + ")" : "") + @"
                                    </div>
                                 </div>";

                }


                strhtml += "</div>";


                return strhtml;
            }


            catch (Exception es)
            {
                return "";
            }

        }
        private string GetHtmlSampleOrderTableHTML(rpc_sp_get_data_for_pre_costing_for_review_approval_DTO sample_order)
        {
            string strhtml = "<div class='row'>";

            try
            {

                if (!string.IsNullOrEmpty(sample_order.sample_photos))
                {
                    var temparray = JArray.Parse(sample_order.sample_photos);

                    sample_order.sample_photos_List = JArray.Parse(sample_order.sample_photos).ToObject<List<file_upload>>();

                    strhtml += "<div class='col-lg-12'><div class=''><img class='grid_img' src=\"../Images/loading-icon-file.gif\"  tran_sample_order_id='" + sample_order.tran_sample_order_id + "'  onclick='ShowImage(this);'  style='width:50px;height:40px;' /><a class='anch_link' onclick='EditSampleOrder(this," + sample_order.tran_sample_order_id.ToString() + ")' style='font-size:12px;'>" + sample_order.tran_sample_order_number + "</a></div></div>";
                }


                strhtml += "</div>";


                return strhtml;
            }


            catch (Exception es)
            {
                return "";
            }

        }

        [HttpPost]
        public async Task<IActionResult> GetTranPreCostingData(Filter_RangePlan_DataTable request)
        {
            List<rpc_sp_get_data_for_pre_costing_DTO> records = new List<rpc_sp_get_data_for_pre_costing_DTO>();
            
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            if (sec_object.roleid == Convert.ToInt64(Enum_Role.Admin))
            {
                request.userid = (long?)null;
                var saved_records = await _rpc_db_service.GetAllsp_get_data_for_pre_costingAsync(
                request);

                records = _mapper.Map<List<rpc_sp_get_data_for_pre_costing_DTO>>(saved_records);

            }
            else
            {
                request.userid = sec_object.userid.Value;
                var saved_records = await _rpc_db_service.GetAllsp_get_data_for_pre_costingAsync(
                               request);

                records = _mapper.Map<List<rpc_sp_get_data_for_pre_costing_DTO>>(saved_records);

            }

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            t.tran_pre_costing_id,
                            t.tran_sample_order_id,
                            t.tran_sample_order_number,
                            t.order_date,
                            t.delivery_date,
                            t.unit_name,
                            t.order_quantity,
                            t.style_quantity,
                            t.style_code,
                            t.tran_va_plan_detl_style_id,
                            // t.bp_yearly_gross_sales,
                            t.style_product_size_group_id,
                            row_index = index++,
                            //t.event_gross_sales,
                            t.range_plan_detail_id,
                            t.range_plan_id,
                            //  t.tran_bp_event_id,
                            //   t.tran_bp_year_id,
                            t.style_item_product_id,
                            //   t.total_rangeplan_mrp_value,
                            //t.total_rangeplan_cpu_value,
                            //t.total_rangeplan_quantity,
                            t.range_plan_quantity,
                            t.cpu_per_pc_value,
                            t.mrp_per_pc_value,
                            t.mrp_value,
                            t.cpu_value,
                            t.tran_va_plan_id,
                            t.tran_va_plan_detl_id,
                            t.tran_va_plan_event_id,
                            t.remarks,
                            t.style_master_category_id,
                            t.is_separate_price,
                            //t.tran_range_plan_event_id,
                            //t.is_finalized,
                            t.priority_id,
                            t.no_of_style,
                            t.style_code_gen,
                            t.tran_pre_costing_review_id,
                            status = t.is_reviewed == 1 ? "REVIEWED(" + t.version_no + ")" : "NEW",

                            sample_photos_html = GetHtmlSampleOrderTableHTML(t),
                            //  totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
                            //  totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),

                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,
                            strtxt_style_code_gen = "<input style='width: 80px;text-align:center;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",

                            btnAddEditPreCosting = t.tran_pre_costing_id == null ?
                            "<input style='width:150px;text-align:center;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' type='button' value='Add' onclick='AddPreCosting(this)'>"
                            : (t.is_reviewed == 1 ? "<input style='width:150px;background-color:darkcyan;text-align:center;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' type='button' value='Review Pre Costing' onclick='ReviewPreCostingLoad(this);'>" :
                            "<input style='width:150px;background-color:darkcyan;text-align:center;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' type='button' value='Edit' onclick='EditPreCosting(this)'>"),

                            t.style_product_type_id,
                            btnView = "<input style='width:150px;background-color:darkcyan;text-align:center;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' type='button' value='View' onclick='ViewPreCosting(this)'>",
                            btnApprove = "<input style='width:150px;background-color:darkcyan;text-align:center;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' type='button' value='Approve' onclick='LoadPreCostingApprove(this)'>",

                            btnReviewnPreCosting = "<input style='width:150px;background-color:darkcyan;text-align:center;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' type='button' value='Review Pre Costing' onclick='ReviewPreCostingLoad(this);'>",


                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);

            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetTr6anPreCostingDataForReviewRequest(tran_va_plan_Filter_DTO request)
        {

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            List<rpc_sp_get_data_for_pre_costing_for_review_approval_DTO> records = new List<rpc_sp_get_data_for_pre_costing_for_review_approval_DTO>();

            if (sec_object.roleid == Convert.ToInt64(Enum_Role.Admin))
            {
                var saved_records = await _rpc_db_service.GetAllsp_get_data_for_pre_costing_for_review_approvalAsync(
                request.fiscal_year_id.Value, request.event_id.Value, null, null, request.data_filter_type.Value);

                records = _mapper.Map<List<rpc_sp_get_data_for_pre_costing_for_review_approval_DTO>>(saved_records);
            }
            else
            {
                var saved_records = await _rpc_db_service.GetAllsp_get_data_for_pre_costing_for_review_approvalAsync(
                               request.fiscal_year_id.Value, request.event_id.Value, sec_object.userid.Value, null, request.data_filter_type.Value);

                records = _mapper.Map<List<rpc_sp_get_data_for_pre_costing_for_review_approval_DTO>>(saved_records);

            }
            var index = 1;
            var data = (from t in records
                        select new
                        {
                            t.tran_pre_costing_review_id,
                            t.version_no,
                            t.is_ack_by_merchant,
                            t.is_approved_by_designer,
                            t.designer_approve_date,
                            t.approve_date,
                            t.tran_pre_costing_id,
                            t.tran_sample_order_id,
                            t.tran_sample_order_number,
                            t.order_date,
                            t.delivery_date,
                            t.unit_name,
                            t.order_quantity,
                            t.style_quantity,
                            t.style_code,
                            t.tran_va_plan_detl_style_id,
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
                            t.tran_va_plan_id,
                            t.tran_va_plan_detl_id,
                            t.tran_va_plan_event_id,
                            t.remarks,
                            t.style_master_category_id,
                            t.is_separate_price,
                            t.tran_range_plan_event_id,
                            t.is_finalized,
                            t.priority_id,
                            t.no_of_style,
                            t.style_code_gen,

                            totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
                            totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),

                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,

                            sample_photos_html = GetHtmlSampleOrderTableHTML(t),

                            strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",
                            btnAddEditPreCosting = t.tran_pre_costing_id == null ?
                            "<input style='width:150px;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' type='button' value='Add Pre Costing' onclick='AddPreCosting(this)'>"
                            : "<input style='width:150px;background-color:darkcyan;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' type='button' value='Edit Pre Costing' onclick='EditPreCosting(this)'>",
                            t.style_product_type_id,
                            btnReviewnPreCosting = "<input style='width:150px;background-color:darkcyan;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' tran_pre_costing_review_id='" + t.tran_pre_costing_review_id.ToString() + "' type='button' value='Review Pre Costing' onclick='ReviewPreCostingLoad(this);'>",
                            btnViewAlreadyModifiedByDesignerPreCosting = "<input style='width:150px;background-color:#617575;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' tran_pre_costing_review_id='" + t.tran_pre_costing_review_id.ToString() + "' type='button' value='View' onclick='ViewAlreadyModifiedByDesignerPreCosting(this);'>",
                            btnViewApprovedByDesigner = "<input style='width:150px;background-color:#617575;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' tran_pre_costing_review_id='" + t.tran_pre_costing_review_id.ToString() + "' type='button' value='View' onclick='ViewApprovedByDesignerPreCosting(this);'>",


                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count(), data);

            return Json(ret_obj);

        }


        [HttpGet]
        public async Task<IActionResult> PreCostingLanding(string fiscal_year_id, string range_plan_id, string va_plan_id)
        {


            tran_va_plan_DTO model = new tran_va_plan_DTO();



            if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            {
                GenSeasonEventConfigurationDTO obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                if (obj != null)
                {
                    var retFilter = await _rpc_db_service.GetAllsp_get_event_details_allphaseAsync(obj.fiscal_year_id, obj.event_id);

                    if (retFilter.Count > 0)
                    {
                        return RedirectToAction("PreCostingCreate", "PreCosting", new
                        {
                            fiscal_year_id = clsUtil.EncodeString(obj.fiscal_year_id.ToString()),
                            eventid = clsUtil.EncodeString(obj.event_id.ToString()),
                            range_plan_id = clsUtil.EncodeString(retFilter.FirstOrDefault().range_plan_id.ToString()),
                            //tran_va_plan_event_id = clsUtil.EncodeString(retFilter.FirstOrDefault().tran_va_plan_event_id.ToString()),
                        });
                    }
                }
            }

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
                    var SampleOrder = await _TranPlanAllocateService.GetAsync(Convert.ToInt64(decoded_va_plan_id));

                    if (SampleOrder.Count > 0)
                    {
                        model = _mapper.Map<tran_va_plan_DTO>(SampleOrder.FirstOrDefault());
                    }
                }
            }

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            //var tran_bp_year = await _tran_bp_yearservice.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_list = fiscal_year_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()

                                                   }
                                                   ).ToList();

            ViewBag.fiscal_year_id = decoded_fiscal_year_id;

            return View("~/Views/DesignMgt/SampleOrder/SampleOrderCreateLanding.cshtml", model);

        }



        [HttpGet]
        public async Task<IActionResult> PreCostingAdd(string input)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {


            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            var proc_sp_get_pre_costing_New = await _rpc_db_service.GetAllproc_sp_get_pre_costing_dataAsync
                (
                    temp_obj.tran_sample_order_id.Value, temp_obj.style_item_product_id.Value,
                    Fiscal_Year_ID,
                    Convert.ToInt64(Team_Category.Designer)
                );

            tran_sample_order_DTO tran_sample_order_Model = _mapper.Map<tran_sample_order_DTO>(proc_sp_get_pre_costing_New);

            tran_pre_costing_DTO model = _mapper.Map<tran_pre_costing_DTO>(tran_sample_order_Model);

            model.List_base64String_File = proc_sp_get_pre_costing_New.List_SampleOrderFiles;
            model.style_product = temp_obj.style_product;
            model.style_product_details = temp_obj.style_product_detail;
            model.style_quantity = temp_obj.style_quantity;
            model.style_code = temp_obj.style_code;

            model.fiscal_year_id = Fiscal_Year_ID;
            model.event_id = Event_ID;

            var list_color_size = JsonConvert.DeserializeObject<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>>(proc_sp_get_pre_costing_New.color_detl_size_list);

            List<tran_plan_allocate_style_DTO> ListStyle = GenerateStyleList(temp_obj, list_color_size);

            model.List_plan_detl_style = ListStyle;

            var mapping_structure_List = JsonConvert.DeserializeObject<List<rpc_sp_get_mapped_item_structure_DTO>>(proc_sp_get_pre_costing_New.mapping_structure_list);

            var objstructure_List_DB = JsonConvert.DeserializeObject<List<rpc_sp_get_sampleorder_subgroup_det_DTO>>(proc_sp_get_pre_costing_New.sampleorder_subgroup_list);

            var structure_List_DB = objstructure_List_DB.ToList();

            var structure_group_List = mapping_structure_List
                .GroupBy(p => new { p.item_structure_group_id, p.structure_group_name })
                .Select(p => new rpc_sp_get_mapped_item_structure_DTO
                {
                    item_structure_group_id = p.Key.item_structure_group_id,
                    structure_group_name = p.Key.structure_group_name
                }).ToList();

            foreach (var singlegroup in structure_group_List)
            {
                singlegroup.List_sub_group = structure_List_DB
                    .Where(p => p.item_structure_group_id == singlegroup.item_structure_group_id).ToList();
            }

            model.List_Mapped_Structure = structure_group_List;

            var list_structure = JsonConvert.DeserializeObject<List<gen_item_structure_group_sub_DTO>>(proc_sp_get_pre_costing_New.gen_itemstructure_groupsub_list);

            model.List_Structure_det = list_structure;

            var unit_list = JsonConvert.DeserializeObject<List<gen_unit_DTO>>(proc_sp_get_pre_costing_New.gen_unit_list);

            var list_unitsdto = unit_list;

            model.List_Unit = list_unitsdto.Select(p => new SelectListItem
            {
                Text = p.unit_name,
                Value = p.gen_unit_id.ToString()
            }).ToList();

            model.designer = proc_sp_get_pre_costing_New.team_member_marketing_name;

            model.designer_member_id = proc_sp_get_pre_costing_New.designer_member_id;

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.sample_order_embellishmentlist))
            {
                var objembellishment_db = JsonConvert.DeserializeObject<List<rpc_sp_get_sampleorder_embellishment_det_DTO>>(proc_sp_get_pre_costing_New.sample_order_embellishmentlist);

                ViewBag.embellishment = objembellishment_db.ToList();
            }

            var List_Unit = JsonConvert.DeserializeObject<List<style_product_unit_DTO>>(proc_sp_get_pre_costing_New.style_product_unit_list);

            var List_SizeGroupDetl = JsonConvert.DeserializeObject<List<style_product_size_group_details_DTO>>(proc_sp_get_pre_costing_New.style_product_sizegroupdetails_list);

            model.List_Detl = JsonConvert.DeserializeObject<List<tran_sample_order_detl_DTO>>(proc_sp_get_pre_costing_New.sample_order_detaillist);

            foreach (var obj in model.List_Detl)
            {
                obj.txtcolor_code = "<input type='color' value='" + obj.color_code + "' class='border-gray-200 rounded-sm txtvalcolor_code' onchange='LoadColorFromBox(this)' style='width:30%;float:left;' />" +
                     "<input   pattern='^#+([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$' value='" + obj.color_code + "' type='text' class='border-gray-200 rounded-sm txtColorCode' style='width:65%;float:right;' /> ";

                obj.txtorder_quantity = "<input type='number' style='width:80px;' min='0' value='" + obj.order_quantity.ToString() + "' onchange='calc_total();'  class='border-gray-200 rounded-sm txtorder_quantity' />";
                obj.strddlSizeHTML = GetDDLProductSizeHTML(List_SizeGroupDetl, obj.style_product_size_group_detid);
                obj.strddlUnitHTML = GetDDLUnitHTML(List_Unit, obj.style_product_unit_id);
                obj.btnAddDeleteRowHTML = "<button tran_sample_order_detl_id='" + obj.tran_sample_order_detl_id + "' type='button' style='width: 90px;background-color:darkred' onclick='btnAddDeleteRow(this)' class='btn btn-primary btnAddDeleteRow '>Remove</button>";

            }

            var list = JsonConvert.DeserializeObject<List<gen_process_master_DTO>>(proc_sp_get_pre_costing_New.gen_process_master_list);

            ViewBag.process_list = list.ToList()
             .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.process_name.ToString(),
                                                    Value = a.gen_process_master_id.ToString(),
                                                }
                                                ).ToList();

            return PartialView("~/Views/DesignMgt/PreCosting/_PreCostingNew.cshtml", model);

        }

        private static List<tran_plan_allocate_style_DTO> GenerateStyleList(tran_va_plan_Filter_DTO? temp_obj, List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>? list_color_size)
        {
            var ListStyle = list_color_size
                //.Where(p => p.tran_va_plan_detl_style_id == temp_obj.tran_va_plan_detl_style_id)
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

            }

            return ListStyle;
        }

        [HttpGet]
        public async Task<IActionResult> PreCostingReviewVersons(string input)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {


            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            //  var review_data =await _TranPreCostingReviewService.GetSingleAsync(temp_obj.tran_pre_costing_review_id.Value);

            var proc_sp_get_pre_costing_Review = await _rpc_db_service.GetAllproc_sp_get_pre_costing_modification_request_data_Async
              (
                  temp_obj.tran_pre_costing_review_id,
                  temp_obj.tran_pre_costing_id,
                  temp_obj.tran_sample_order_id.Value, temp_obj.style_item_product_id.Value,
                  temp_obj.fiscal_year_id.Value,
                  Convert.ToInt64(Team_Category.Designer)
              );

            proc_sp_get_pre_costing_Review.obj_tpc_new_data = JArray.Parse(proc_sp_get_pre_costing_Review.tpc_new_data).ToObject<List<tran_pre_costing_entity>>().FirstOrDefault();
            proc_sp_get_pre_costing_Review.obj_tpc_old_data = JArray.Parse(proc_sp_get_pre_costing_Review.tpc_old_data).ToObject<List<tran_pre_costing_entity>>().FirstOrDefault();
            proc_sp_get_pre_costing_Review.style_product = temp_obj.style_product;
            proc_sp_get_pre_costing_Review.style_product_details = temp_obj.style_product_detail;
            proc_sp_get_pre_costing_Review.style_quantity = temp_obj.style_quantity;
            proc_sp_get_pre_costing_Review.style_code = temp_obj.style_code;
            proc_sp_get_pre_costing_Review.order_quantity = temp_obj.order_quantity;
            proc_sp_get_pre_costing_Review.tran_pre_costing_review_id = temp_obj.tran_pre_costing_review_id;

            var mapping_structure_List = JsonConvert.DeserializeObject<List<rpc_sp_get_mapped_item_structure_DTO>>(proc_sp_get_pre_costing_Review.mapping_structure_list);

            var objstructure_List_DB = JsonConvert.DeserializeObject<List<rpc_sp_get_sampleorder_subgroup_det_DTO>>(proc_sp_get_pre_costing_Review.sampleorder_subgroup_list);

            var structure_List_DB = objstructure_List_DB.ToList();

            var structure_group_List = mapping_structure_List
                .GroupBy(p => new { p.item_structure_group_id, p.structure_group_name })
                .Select(p => new rpc_sp_get_mapped_item_structure_DTO
                {
                    item_structure_group_id = p.Key.item_structure_group_id,
                    structure_group_name = p.Key.structure_group_name
                }).ToList();

            foreach (var singlegroup in structure_group_List)
            {
                singlegroup.List_sub_group = structure_List_DB
                    .Where(p => p.item_structure_group_id == singlegroup.item_structure_group_id).ToList();
            }

            proc_sp_get_pre_costing_Review.List_Mapped_Structure = structure_group_List;

            proc_sp_get_pre_costing_Review.List_Structure_det = JsonConvert.DeserializeObject<List<gen_item_structure_group_sub_DTO>>(proc_sp_get_pre_costing_Review.gen_itemstructure_groupsub_list);

            var list_unitsdto = JsonConvert.DeserializeObject<List<gen_unit_DTO>>(proc_sp_get_pre_costing_Review.gen_unit_list);

            proc_sp_get_pre_costing_Review.List_Unit = list_unitsdto.Select(p => new SelectListItem
            {
                Text = p.unit_name,
                Value = p.gen_unit_id.ToString()
            }).ToList();


            var List_Unit = JsonConvert.DeserializeObject<List<style_product_unit_DTO>>(proc_sp_get_pre_costing_Review.style_product_unit_list);

            var List_SizeGroupDetl = JsonConvert.DeserializeObject<List<style_product_size_group_details_DTO>>(proc_sp_get_pre_costing_Review.style_product_sizegroupdetails_list);

            var list = JsonConvert.DeserializeObject<List<gen_process_master_DTO>>(proc_sp_get_pre_costing_Review.gen_process_master_list);

            ViewBag.process_list = list.ToList()
             .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.process_name.ToString(),
                                                    Value = a.gen_process_master_id.ToString(),
                                                }
                                                ).ToList();

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_Review.pre_costing_detail_list))
            {
                var jsonstring = clsUtil.CleanJsonString(proc_sp_get_pre_costing_Review.pre_costing_detail_list);

                proc_sp_get_pre_costing_Review.List_PreCostingDet_rpc = JsonConvert.DeserializeObject<List<rpc_sp_get_pre_costing_details_by_masterid_DTO>>(jsonstring);

                foreach (var objsingle in proc_sp_get_pre_costing_Review.List_PreCostingDet_rpc)
                {
                    objsingle.List_placement_info = objsingle.placement_info != null ? objsingle.placement_info.ToObject<List<style_placement_information_DTO>>() : new List<style_placement_information_DTO>();
                    objsingle.placement_info = null;
                }
            }
            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_Review.pre_costing_subcontract_list))
            {
                var jsonstring = clsUtil.CleanJsonString(proc_sp_get_pre_costing_Review.pre_costing_subcontract_list);
                proc_sp_get_pre_costing_Review.List_SubContractDetl = JsonConvert.DeserializeObject<List<tran_pre_costing_item_subcontract_detail_DTO>>(jsonstring);
            }
            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_Review.pre_costing_embellishment_list))
            {
                var jsonstring = clsUtil.CleanJsonString(proc_sp_get_pre_costing_Review.pre_costing_embellishment_list);
                proc_sp_get_pre_costing_Review.List_EmbellishmentDetl = JsonConvert.DeserializeObject<List<tran_pre_costing_item_embellishment_detail_DTO>>(jsonstring);
            }
            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_Review.pre_costing_detail_list_old))
            {
                var jsonstring = clsUtil.CleanJsonString(proc_sp_get_pre_costing_Review.pre_costing_detail_list_old);
                proc_sp_get_pre_costing_Review.List_PreCostingDet_rpc_old = JsonConvert.DeserializeObject<List<rpc_sp_get_pre_costing_details_by_masterid_DTO>>(jsonstring);

                foreach (var objsingle in proc_sp_get_pre_costing_Review.List_PreCostingDet_rpc_old)
                {
                    objsingle.List_placement_info = objsingle.placement_info != null ? objsingle.placement_info.ToObject<List<style_placement_information_DTO>>() : new List<style_placement_information_DTO>();
                    objsingle.placement_info = null;
                }
            }

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_Review.pre_costing_subcontract_list_old))
            {
                var jsonstring = clsUtil.CleanJsonString(proc_sp_get_pre_costing_Review.pre_costing_subcontract_list_old);
                proc_sp_get_pre_costing_Review.List_SubContractDetl_old = JsonConvert.DeserializeObject<List<tran_pre_costing_item_subcontract_detail_DTO>>(jsonstring);
            }
            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_Review.pre_costing_embellishment_list))
            {
                var jsonstring = clsUtil.CleanJsonString(proc_sp_get_pre_costing_Review.pre_costing_embellishment_list);

                proc_sp_get_pre_costing_Review.List_EmbellishmentDetl_old = JsonConvert.DeserializeObject<List<tran_pre_costing_item_embellishment_detail_DTO>>(jsonstring);

            }
            var row_index = 0;

            foreach (var item in proc_sp_get_pre_costing_Review.List_PreCostingDet_rpc)
            {
                item.row_index = row_index++;
            }

            var list_color_size = JsonConvert.DeserializeObject<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>>(proc_sp_get_pre_costing_Review.color_detl_size_list);

            List<tran_plan_allocate_style_DTO> ListStyle = GenerateStyleList(temp_obj, list_color_size);

            proc_sp_get_pre_costing_Review.List_plan_detl_style = ListStyle;

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_Review.color_wise_size_quantity))
            {
                var jsonstring = clsUtil.CleanJsonString(proc_sp_get_pre_costing_Review.color_wise_size_quantity);

                proc_sp_get_pre_costing_Review.List_detl_style_color = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(jsonstring);
            }
            else
                proc_sp_get_pre_costing_Review.List_detl_style_color = new List<tran_plan_allocate_style_color_DTO>();

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_Review.color_wise_size_quantity_old))
            {
                var jsonstring = clsUtil.CleanJsonString(proc_sp_get_pre_costing_Review.color_wise_size_quantity_old);

                proc_sp_get_pre_costing_Review.List_detl_style_color_Old = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(jsonstring);
            }
            else
                proc_sp_get_pre_costing_Review.List_detl_style_color_Old = new List<tran_plan_allocate_style_color_DTO>();

            return PartialView("~/Views/DesignMgt/PreCosting/_PreCostingRequestForReview.cshtml", proc_sp_get_pre_costing_Review);

        }


        [HttpGet]
        public async Task<IActionResult> PreCostingEdit(string input)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {


            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            var proc_sp_get_pre_costing_New = await _rpc_db_service.GetAllproc_sp_get_pre_costing_data_editAsync
              (
                  temp_obj.tran_pre_costing_id,
                  temp_obj.tran_sample_order_id.Value, temp_obj.style_item_product_id.Value,
                  Fiscal_Year_ID,
                  Convert.ToInt64(Team_Category.Designer)
              );

            tran_sample_order_DTO objSampleOrder = _mapper.Map<tran_sample_order_DTO>(proc_sp_get_pre_costing_New);

            tran_pre_costing_DTO model = _mapper.Map<tran_pre_costing_DTO>(proc_sp_get_pre_costing_New);

            model.tran_pre_costing_id = temp_obj.tran_pre_costing_id.Value;
            model.sample_order = objSampleOrder;

            model.style_product = temp_obj.style_product;
            model.style_product_details = temp_obj.style_product_detail;
            model.style_quantity = temp_obj.style_quantity;
            model.style_code = temp_obj.style_code;
            model.order_quantity = temp_obj.order_quantity;
            model.sample_order.List_base64String_File = proc_sp_get_pre_costing_New.List_SampleOrderFiles;

            var mapping_structure_List = JsonConvert.DeserializeObject<List<rpc_sp_get_mapped_item_structure_DTO>>(proc_sp_get_pre_costing_New.mapping_structure_list);

            var objstructure_List_DB = JsonConvert.DeserializeObject<List<rpc_sp_get_sampleorder_subgroup_det_DTO>>(proc_sp_get_pre_costing_New.sampleorder_subgroup_list);

            var structure_List_DB = objstructure_List_DB.ToList();

            var structure_group_List = mapping_structure_List
                .GroupBy(p => new { p.item_structure_group_id, p.structure_group_name })
                .Select(p => new rpc_sp_get_mapped_item_structure_DTO
                {
                    item_structure_group_id = p.Key.item_structure_group_id,
                    structure_group_name = p.Key.structure_group_name
                }).ToList();

            foreach (var singlegroup in structure_group_List)
            {
                singlegroup.List_sub_group = structure_List_DB
                    .Where(p => p.item_structure_group_id == singlegroup.item_structure_group_id).ToList();
            }

            model.List_Mapped_Structure = structure_group_List;

            model.List_Structure_det = JsonConvert.DeserializeObject<List<gen_item_structure_group_sub_DTO>>(proc_sp_get_pre_costing_New.gen_itemstructure_groupsub_list);

            var list_unitsdto = JsonConvert.DeserializeObject<List<gen_unit_DTO>>(proc_sp_get_pre_costing_New.gen_unit_list);

            model.List_Unit = list_unitsdto.Select(p => new SelectListItem
            {
                Text = p.unit_name,
                Value = p.gen_unit_id.ToString()
            }).ToList();

            model.designer = proc_sp_get_pre_costing_New.name;
            model.designer_member_id = proc_sp_get_pre_costing_New.designer_member_id;

            var List_Unit = JsonConvert.DeserializeObject<List<style_product_unit_DTO>>(proc_sp_get_pre_costing_New.style_product_unit_list);

            var List_SizeGroupDetl = JsonConvert.DeserializeObject<List<style_product_size_group_details_DTO>>(proc_sp_get_pre_costing_New.style_product_sizegroupdetails_list);

            var list = JsonConvert.DeserializeObject<List<gen_process_master_DTO>>(proc_sp_get_pre_costing_New.gen_process_master_list);

            model.List_PreCostingDet_rpc = new List<rpc_sp_get_pre_costing_details_by_masterid_DTO>();
            model.List_SubContractDetl = new List<tran_pre_costing_item_subcontract_detail_DTO>();
            model.List_EmbellishmentDetl = new List<tran_pre_costing_item_embellishment_detail_DTO>();

            ViewBag.process_list = list.ToList()
             .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.process_name.ToString(),
                                                    Value = a.gen_process_master_id.ToString(),
                                                }
                                                ).ToList();
            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.pre_costing_detail_list))
            {
                model.List_PreCostingDet_rpc = JsonConvert.DeserializeObject<List<rpc_sp_get_pre_costing_details_by_masterid_DTO>>(proc_sp_get_pre_costing_New.pre_costing_detail_list);

                foreach (var objsingle in model.List_PreCostingDet_rpc)
                {
                    objsingle.List_placement_info = objsingle.placement_info != null ? objsingle.placement_info.ToObject<List<style_placement_information_DTO>>() : new List<style_placement_information_DTO>();
                    objsingle.placement_info = null;
                }
            }
            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.pre_costing_subcontract_list))
                model.List_SubContractDetl = JsonConvert.DeserializeObject<List<tran_pre_costing_item_subcontract_detail_DTO>>(proc_sp_get_pre_costing_New.pre_costing_subcontract_list);
            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.pre_costing_embellishment_list))
                model.List_EmbellishmentDetl = JsonConvert.DeserializeObject<List<tran_pre_costing_item_embellishment_detail_DTO>>(proc_sp_get_pre_costing_New.pre_costing_embellishment_list);

            var row_index = 0;

            foreach (var item in model.List_PreCostingDet_rpc)
            {
                item.row_index = row_index++;
                item.current_state = 2;
            }

            var list_color_size = JsonConvert.DeserializeObject<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>>(proc_sp_get_pre_costing_New.color_detl_size_list);

            List<tran_plan_allocate_style_DTO> ListStyle = GenerateStyleList(temp_obj, list_color_size);

            model.List_plan_detl_style = ListStyle;

            model.List_detl_style_color = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(model.color_wise_size_quantity);

            var List_Designer = JsonConvert.DeserializeObject<List<owin_user_DTO>>(proc_sp_get_pre_costing_New.designer_list);

            if (List_Designer.Where(p => p.userid == proc_sp_get_pre_costing_New.designer_member_id).FirstOrDefault() != null)
            {
                model.designer = List_Designer.Where(p => p.userid == proc_sp_get_pre_costing_New.designer_member_id).FirstOrDefault().name;
            }

            model.fiscal_year_id = Fiscal_Year_ID;
            model.event_id = Event_ID;

            return PartialView("~/Views/DesignMgt/PreCosting/_PreCostingEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> PreCostingApproveReject(string input)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {


            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            var proc_sp_get_pre_costing_New = await _rpc_db_service.GetAllproc_sp_get_pre_costing_data_editAsync
              (
                  temp_obj.tran_pre_costing_id,
                  temp_obj.tran_sample_order_id.Value, temp_obj.style_item_product_id.Value,
                  temp_obj.fiscal_year_id.Value,
                  Convert.ToInt64(Team_Category.Designer)
              );

            tran_sample_order_DTO objSampleOrder = _mapper.Map<tran_sample_order_DTO>(proc_sp_get_pre_costing_New);

            tran_pre_costing_DTO model = _mapper.Map<tran_pre_costing_DTO>(proc_sp_get_pre_costing_New);

            model.tran_pre_costing_id = temp_obj.tran_pre_costing_id.Value;
            model.sample_order = objSampleOrder;

            model.fiscal_year_id = Fiscal_Year_ID;
            model.event_id = Event_ID;

            model.style_product = temp_obj.style_product;
            model.style_product_details = temp_obj.style_product_detail;
            model.style_quantity = temp_obj.style_quantity;
            model.style_code = temp_obj.style_code;
            model.order_quantity = temp_obj.order_quantity;
            model.sample_order.List_base64String_File = proc_sp_get_pre_costing_New.List_SampleOrderFiles;

            var mapping_structure_List = JsonConvert.DeserializeObject<List<rpc_sp_get_mapped_item_structure_DTO>>(proc_sp_get_pre_costing_New.mapping_structure_list);

            var objstructure_List_DB = JsonConvert.DeserializeObject<List<rpc_sp_get_sampleorder_subgroup_det_DTO>>(proc_sp_get_pre_costing_New.sampleorder_subgroup_list);

            var structure_List_DB = objstructure_List_DB.ToList();

            var structure_group_List = mapping_structure_List
                .GroupBy(p => new { p.item_structure_group_id, p.structure_group_name })
                .Select(p => new rpc_sp_get_mapped_item_structure_DTO
                {
                    item_structure_group_id = p.Key.item_structure_group_id,
                    structure_group_name = p.Key.structure_group_name
                }).ToList();

            foreach (var singlegroup in structure_group_List)
            {
                singlegroup.List_sub_group = structure_List_DB
                    .Where(p => p.item_structure_group_id == singlegroup.item_structure_group_id).ToList();
            }

            model.List_Mapped_Structure = structure_group_List;

            model.List_Structure_det = JsonConvert.DeserializeObject<List<gen_item_structure_group_sub_DTO>>(proc_sp_get_pre_costing_New.gen_itemstructure_groupsub_list);

            var list_unitsdto = JsonConvert.DeserializeObject<List<gen_unit_DTO>>(proc_sp_get_pre_costing_New.gen_unit_list);

            model.List_Unit = list_unitsdto.Select(p => new SelectListItem
            {
                Text = p.unit_name,
                Value = p.gen_unit_id.ToString()
            }).ToList();

            model.designer = proc_sp_get_pre_costing_New.name;
            model.designer_member_id = proc_sp_get_pre_costing_New.designer_member_id;

            var List_Unit = JsonConvert.DeserializeObject<List<style_product_unit_DTO>>(proc_sp_get_pre_costing_New.style_product_unit_list);

            var List_SizeGroupDetl = JsonConvert.DeserializeObject<List<style_product_size_group_details_DTO>>(proc_sp_get_pre_costing_New.style_product_sizegroupdetails_list);

            var list = JsonConvert.DeserializeObject<List<gen_process_master_DTO>>(proc_sp_get_pre_costing_New.gen_process_master_list);

            model.List_PreCostingDet_rpc = new List<rpc_sp_get_pre_costing_details_by_masterid_DTO>();
            model.List_SubContractDetl = new List<tran_pre_costing_item_subcontract_detail_DTO>();
            model.List_EmbellishmentDetl = new List<tran_pre_costing_item_embellishment_detail_DTO>();

            ViewBag.process_list = list.ToList()
             .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.process_name.ToString(),
                                                    Value = a.gen_process_master_id.ToString(),
                                                }
                                                ).ToList();
            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.pre_costing_detail_list))
            {
                model.List_PreCostingDet_rpc = JsonConvert.DeserializeObject<List<rpc_sp_get_pre_costing_details_by_masterid_DTO>>(proc_sp_get_pre_costing_New.pre_costing_detail_list);

                foreach (var objsingle in model.List_PreCostingDet_rpc)
                {
                    objsingle.List_placement_info = objsingle.placement_info != null ? objsingle.placement_info.ToObject<List<style_placement_information_DTO>>() : new List<style_placement_information_DTO>();
                    objsingle.placement_info = null;
                }
            }
            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.pre_costing_subcontract_list))
                model.List_SubContractDetl = JsonConvert.DeserializeObject<List<tran_pre_costing_item_subcontract_detail_DTO>>(proc_sp_get_pre_costing_New.pre_costing_subcontract_list);
            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.pre_costing_embellishment_list))
                model.List_EmbellishmentDetl = JsonConvert.DeserializeObject<List<tran_pre_costing_item_embellishment_detail_DTO>>(proc_sp_get_pre_costing_New.pre_costing_embellishment_list);

            var row_index = 0;

            foreach (var item in model.List_PreCostingDet_rpc)
            {
                item.row_index = row_index++;
                item.current_state = 2;
            }

            var list_color_size = JsonConvert.DeserializeObject<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>>(proc_sp_get_pre_costing_New.color_detl_size_list);

            List<tran_plan_allocate_style_DTO> ListStyle = GenerateStyleList(temp_obj, list_color_size);

            model.List_plan_detl_style = ListStyle;

            model.List_detl_style_color = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(model.color_wise_size_quantity);



            return PartialView("~/Views/DesignMgt/PreCosting/_PreCostingApproveReject.cshtml", model);

        }

        private string GetDDLUnitHTML(List<style_product_unit_DTO> List_ProductUnit, Int64? UnitID)
        {
            string dropdownHTML = "<select class='ddlProductUnit form-control' style='line-height: 0.5rem;font-size:11px;'>";

            dropdownHTML += "<option value=\"\">Select</option>";

            foreach (var objsingle in List_ProductUnit)
            {
                string strSelected = UnitID.HasValue && objsingle.style_product_unit_id == UnitID.Value ? "selected" : "";

                dropdownHTML += "<option " + strSelected + " value=" + objsingle.style_product_unit_id.ToString() + ">" + objsingle.style_product_unit_name + "</option>";
            }

            dropdownHTML += "</select>";

            return dropdownHTML;
        }

        private string GetDDLProductSizeHTML(List<style_product_size_group_details_DTO> List_ProductSizeDet, Int64? SizeDetID)
        {
            string dropdownHTML = "<select class='ddlProductSizeDet form-control' style='line-height: 0.5rem;font-size:11px;'>";

            dropdownHTML += "<option value=\"\">Select</option>";

            foreach (var objsingle in List_ProductSizeDet)
            {
                string strSelected = SizeDetID.HasValue && objsingle.style_product_size_group_detid == SizeDetID ? "selected" : "";

                dropdownHTML += "<option " + strSelected + " value=" + objsingle.style_product_size_group_detid.ToString() + ">" + objsingle.style_product_size + "</option>";
            }

            dropdownHTML += "</select>";

            return dropdownHTML;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewFabric(string item_structure_group_id, string gen_item_structure_group_sub_id, string measurement_unit_id, string default_measurement_unit_detail_id)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {


            var list = await _rpc_db_service.GetAllsp_get_mapped_segment_by_gen_item_structure_group_sub_idAsync(Convert.ToInt32(gen_item_structure_group_sub_id));

            if (item_structure_group_id == Convert.ToInt32(Enum_gen_item_structure_group_sub.Fabric).ToString())
            {
                var mlist = new List<SelectListItem>();
                mlist.Add(new SelectListItem
                {
                    Text = "yds",
                    Value = "72"
                });
                mlist.Add(new SelectListItem
                {
                    Text = "mtr",
                    Value = "73"
                });
                mlist.Add(new SelectListItem
                {
                    Text = "kg",
                    Value = "86"
                });

                ViewBag.list_measurement = mlist;
            }
            else
            {
                var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(measurement_unit_id));

                ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
                {
                    Text = p.unit_detail_display,
                    Value = p.gen_measurement_unit_detail_id.ToString(),
                    Selected = p.gen_measurement_unit_detail_id.ToString() == default_measurement_unit_detail_id ? true : false,
                }).ToList();
            }

            var list_placement = JsonConvert.DeserializeObject<List<style_placement_information_DTO>>(list[0].styleplacementinformation_list);

            ViewBag.list_placement = list_placement.Select(p => new SelectListItem
            {
                Text = p.placement,
                Value = p.style_placement_information_id.ToString(),
            }).ToList();

            ViewBag.gen_item_structure_group_sub_id = Convert.ToInt32(gen_item_structure_group_sub_id);

            ViewBag.item_structure_group_id = Convert.ToInt32(item_structure_group_id);

            return PartialView("~/Views/DesignMgt/PreCosting/_AddFabricDetails.cshtml", list);

        }


        [HttpGet]
        public async Task<IActionResult> AddNewSubContractDetails(string index)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {


            var list = await _GenProcessMasterService.GetAllAsync();

            ViewBag.process_list = list.ToList()
             .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.process_name.ToString(),
                                                    Value = a.gen_process_master_id.ToString(),
                                                }
                                                ).ToList();

            ViewBag.Index = Convert.ToInt32(index);
            // var list = await _rpc_db_service.GetAllsp_get_mapped_segment_by_gen_item_structure_group_sub_idAsync(Convert.ToInt32(gen_item_structure_group_sub_id));

            return PartialView("~/Views/DesignMgt/PreCosting/_AddSubContractDetails.cshtml");

        }

        [HttpGet]
        public async Task<IActionResult> AddNewEmbellishmentDetails(string embellishment_id, string embellishment)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {


            ViewBag.embellishment_id = Convert.ToInt32(embellishment_id);
            ViewBag.embellishment = embellishment;

            return PartialView("~/Views/DesignMgt/PreCosting/_AddEmbellishmentDetails.cshtml");

        }


        [HttpPost]
        public async Task<ActionResult> SavePreCosting([FromBody] tran_pre_costing_DTO model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {

            model.added_by = sec_object.userid;

            model.fiscal_year_id = Fiscal_Year_ID;
            model.event_id = Event_ID;

            model.pre_costing_date = DateTime.Now;
            model.date_added = DateTime.Now;

            if (model.is_submitted == 1)
            {
                model.submitted_by = model.added_by;
            }

            var ret = await _TranpreconstingService.SaveAsync(model);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Saving Failed"
            });

        }


        [HttpPost]
        public async Task<ActionResult> UpdatePreCosting([FromBody] tran_pre_costing_DTO model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {


            model.added_by = sec_object.userid;

            model.pre_costing_date = DateTime.Now;
            model.date_added = DateTime.Now;

            model.fiscal_year_id = Fiscal_Year_ID;
            model.event_id = Event_ID;

            if (model.is_submitted == 1)
            {
                model.submitted_by = model.added_by;
            }

            //var str = JsonConvert.SerializeObject(model);

            var ret = await _TranpreconstingService.UpdateAsync(model);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Updating Failed"
            });

        }

        [HttpPost]
        public async Task<ActionResult> ApproveRejectPreCosting([FromBody] tran_pre_costing_DTO model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {

            model.added_by = sec_object.userid;

            model.pre_costing_date = DateTime.Now;
            model.date_added = DateTime.Now;
            model.approve_date = DateTime.Now;
            model.approved_by = model.added_by;

            var str = JsonConvert.SerializeObject(model);

            var ret = await _TranpreconstingService.tran_pre_costing_approve_reject(model);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? (model.is_approved == 1 ? "Pre-Costing Approved Successful" : "Pre-Costing Approval Failed") :
               (model.is_approved == 1 ? "Pre-Costing Rejected Successful" : "Pre-Costing Rejection Failed")
            });

        }



        [HttpPost]
        public async Task<ActionResult> PreCostingReviewAction([FromBody] tran_pre_costing_DTO model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {

            model.added_by = sec_object.userid;

            model.pre_costing_review.added_by = sec_object.userid;
            model.pre_costing_review.designer_approved_by = sec_object.userid;
            model.pre_costing_review.designer_approve_date = DateTime.Now;
            model.pre_costing_review.date_added = DateTime.Now;
            model.is_submitted = 2;

            var ret = await _TranpreconstingService.PreCostingReviewAction(model);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Updating Failed"
            });

        }


        [HttpPost]
        public async Task<ActionResult> DesignerApprovePreCosting([FromBody] tran_pre_costing_review_entity model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {
            // tran_sample_order_DTO model = new tran_sample_order_DTO();


            {


                model.added_by = sec_object.userid;
            }

            var retdb = await _TranPreCostingReviewService.GetSingleAsync(model.tran_pre_costing_review_id.Value);

            retdb.designer_approved_by = sec_object.userid;
            retdb.designer_approve_date = DateTime.Now;
            retdb.designer_approve_remarks = model.designer_approve_remarks;
            retdb.is_approved_by_designer = 1;

            retdb.is_ack_by_merchant = null;
            retdb.merchant_ack_by = null;
            retdb.merchant_ack_date = null;

            var ret = await _TranPreCostingReviewService.UpdateAsync(retdb);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Updating Failed"
            });

        }

    }
}
