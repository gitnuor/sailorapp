using AutoMapper;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Application.DTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using EPYSLSAILORAPP.Application.Interface;
using BDO.Core.Base;
using Web.Core.Frame.Helpers;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.SystemServices;
using ServiceStack;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain;
using Azure;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using Newtonsoft.Json.Linq;
using EPYSLSAILORAPP.Application.Interface.Security;
using EPYSLSAILORAPP.Domain.Enum;

namespace EPYSLSAILORAPP.Controllers.DesignManagement
{
    [RequestFormLimits(MultipartBodyLengthLimit = 2000000000)]
    [RequestSizeLimit(2000000000)]

    public class DesignReviewController : ExtendedBaseController
    {
        private readonly ILogger<DesignReviewController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IStylegenderService _StylegenderService;
        private readonly IStyleitemoriginService _StyleitemoriginService;
        private readonly IStyleitemtypeService _StyleitemtypeService;
        private readonly IStyleproducttypeService _StyleproducttypeService;
        private readonly ITranPlanAllocateService _TranPlanAllocateService;
        private readonly ITransampleorderService _TransampleorderService;
        private readonly IGenUnitService _GenUnitService;
        private readonly ITran_BP_YearService _tran_bp_yearservice;
        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private readonly IOwinUserService _OwinUserService;
        private readonly ITranDesignerReviewService _TranDesignerReviewService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IGenSeasonEventConfigurationService _genseasoneventconfigurationservice;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        private IRedisService<rpc_range_plan_getfor_createupdate_DTO> _redisRangePlanService;

        public DesignReviewController(ITranDesignerReviewService TranDesignerReviewService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IGenFiscalYearService gen_fiscal_year_Service, ITran_BP_YearService tran_bp_yearservice,
            IMapper mapper, ILogger<DesignReviewController> logger, IHttpContextAccessor contextAccessor,
            IStylegenderService StylegenderService,
            IStyleitemoriginService StyleitemoriginService,
            IStyleitemtypeService StyleitemtypeService,
            ITranPlanAllocateService TranPlanAllocateService,
            IStyleproducttypeService StyleproducttypeService
            , IGenSeasonEventConfigurationService genseasoneventconfigurationservice, IOwinUserService OwinUserService
            , IRPCDbService rpc_db_service, IConfiguration configuration,
            ITransampleorderService transampleorderService, IGenItemStructureGroupSubService IGenItemStructureGroupSubService, 
            IGenUnitService GenUnitService):base(contextAccessor,configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _gen_fiscal_year_Service = gen_fiscal_year_Service;
            _tran_bp_yearservice = tran_bp_yearservice;
            _mapper = mapper;
            _logger = logger;
            _genseasoneventconfigurationservice = genseasoneventconfigurationservice;
            _rpc_db_service = rpc_db_service;
            _OwinUserService = OwinUserService;
            _contextAccessor = contextAccessor;
            _TranPlanAllocateService = TranPlanAllocateService;
            _StylegenderService = StylegenderService;
            _StyleitemoriginService = StyleitemoriginService;
            _StyleitemtypeService = StyleitemtypeService;
            _StyleproducttypeService = StyleproducttypeService;
            _logger.LogInformation("Hello from inside SampleOrder !");
            _configuration = configuration;
            
            _TransampleorderService = transampleorderService;
            _TranDesignerReviewService = TranDesignerReviewService;
            _GenUnitService = GenUnitService;
        }


        [HttpGet]
        public async Task<IActionResult> DesignReviewLanding()
        {
            Int64 fiscal_year_id = 0, eventid = 0, range_plan_id = 0, tran_va_plan_event_id = 0;

            tran_va_plan_DTO model = new tran_va_plan_DTO();


            return View("~/Views/DesignMgt/DesignReview/DesignReviewLanding.cshtml", model);
        }

        private string GetHtmlSampleOrderTableHTML(rpc_sp_get_data_for_designer_review_DTO sample_order)
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

        #region oldGridMethod
        // [HttpPost]
        //public async Task<IActionResult> GetDesignReviewData(Filter_RangePlan_DataTable request)
        //{
        //    request.fiscal_year_id = Fiscal_Year_ID;
        //    request.event_id = Event_ID;

        //    var designer_list = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

        //    List<rpc_sp_get_data_for_designer_review_DTO> records =  await _rpc_db_service.GetAllsp_get_data_for_designer_reviewAsync(
        //         request);

        //    var index = request.Start+1;
        //    var data = (from t in records
        //                select new
        //                {
        //                    t.tran_designer_review_id,
        //                    t.tran_pre_costing_id,
        //                    t.tran_sample_order_id,
        //                    t.tran_sample_order_number,
        //                    t.order_date,
        //                    t.delivery_date,
        //                    t.unit_name,
        //                    t.order_quantity,
        //                    t.style_quantity,
        //                    t.style_code,
        //                    sample_photos_html = GetHtmlSampleOrderTableHTML(t),
        //                    //t.bp_yearly_gross_sales,
        //                    //t.style_product_size_group_id,
        //                    row_index = index++,
        //                    //t.event_gross_sales,
        //                    //t.range_plan_detail_id,
        //                    //t.range_plan_id,
        //                    //t.tran_bp_event_id,
        //                    //t.tran_bp_year_id,
        //                    //t.style_item_product_id,
        //                    //t.total_rangeplan_mrp_value,
        //                    //t.total_rangeplan_cpu_value,
        //                    //t.total_rangeplan_quantity,
        //                    //t.range_plan_quantity,
        //                    //t.cpu_per_pc_value,
        //                    //t.mrp_per_pc_value,
        //                    //t.mrp_value,
        //                    //t.cpu_value,
        //                    t.tran_va_plan_id,
        //                    t.tran_va_plan_detl_id,
        //                    t.tran_va_plan_event_id,
        //                    // t.remarks,
        //                    t.style_master_category_id,
        //                    t.is_separate_price,
        //                    //t.tran_range_plan_event_id,
        //                    //t.is_finalized,
        //                    //t.priority_id,
        //                    t.no_of_style,
        //                    t.style_code_gen,
        //                    // t.is_submitted,
        //                   // totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
        //                    //totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),
        //                    t.master_category_name,
        //                    product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
        //                    "-" + t.style_gender_name + "-" + t.master_category_name,
        //                    t.style_item_product_name,

        //                    strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",

        //                    btnAddEditPreCosting = t.tran_designer_review_id == null ?
        //                    "<input  class='btn btn-success' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' type='button' value='Add Design Review' onclick='AddDesignReview(this)'>"
        //                    : t.is_submitted == false ?
        //                    "<input style='background-color:darkcyan;' class='btn btn-success' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='Edit Design Review' onclick='EditDesignReview(this)'>" :
        //                    "<input  class='btn btn-warning' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='View Design Review' onclick='ViewDesignReview(this)'>",
        //                    t.style_product_type_id

        //                }).ToList();

        //    var ret_obj = new AjaxResponse(data.Count()>0? records[0].total_rows.Value:0, data);

        //    return Json(ret_obj);

        //}
        #endregion


        public async Task<IActionResult> GetDesignReviewData(Filter_RangePlan_DataTable request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var designer_list = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

            List<rpc_sp_get_data_for_designer_review_DTO> records = await _TranDesignerReviewService.GetAllsp_get_data_for_designer_reviewAsync(
                 request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            t.tran_designer_review_id,
                            t.tran_pre_costing_id,
                            t.tran_sample_order_id,
                            t.tran_sample_order_number,
                            t.order_date,
                            t.delivery_date,
                            t.unit_name,
                            t.order_quantity,
                            t.style_quantity,
                            t.style_code,
                            sample_photos_html = GetHtmlSampleOrderTableHTML(t),
                            //t.bp_yearly_gross_sales,
                            //t.style_product_size_group_id,
                            row_index = index++,
                            //t.event_gross_sales,
                            //t.range_plan_detail_id,
                            //t.range_plan_id,
                            //t.tran_bp_event_id,
                            //t.tran_bp_year_id,
                            //t.style_item_product_id,
                            //t.total_rangeplan_mrp_value,
                            //t.total_rangeplan_cpu_value,
                            //t.total_rangeplan_quantity,
                            //t.range_plan_quantity,
                            //t.cpu_per_pc_value,
                            //t.mrp_per_pc_value,
                            //t.mrp_value,
                            //t.cpu_value,
                            t.tran_va_plan_id,
                            t.tran_va_plan_detl_id,
                            t.tran_va_plan_event_id,
                            // t.remarks,
                            t.style_master_category_id,
                            t.is_separate_price,
                            //t.tran_range_plan_event_id,
                            //t.is_finalized,
                            //t.priority_id,
                            t.no_of_style,
                            t.style_code_gen,
                            // t.is_submitted,
                            // totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
                            //totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,

                            strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",

                            //btnAddEditPreCosting = t.tran_designer_review_id == null ?
                            //"<input  class='btn btn-success' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' type='button' value='Add Design Review' onclick='AddDesignReview(this)'>"
                            //: t.is_submitted == false ?
                            //"<input style='background-color:darkcyan;' class='btn btn-success' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='Edit Design Review' onclick='EditDesignReview(this)'>" :
                            //"<input  class='btn btn-warning' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='View Design Review' onclick='ViewDesignReview(this)'>",
                            //t.style_product_type_id



                            btnAddEditPreCosting = 
                            "<input  class='btn btn-success' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' type='button' value='Add Design Review' onclick='AddDesignReview(this)'>",
                            t.style_product_type_id




                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);

            return Json(ret_obj);

        }


        public async Task<IActionResult> GetDesignReviewDraftData(Filter_RangePlan_DataTable request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var actionType = 1;

            var designer_list = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

            List<rpc_sp_get_data_for_designer_review_DTO> records = await _TranDesignerReviewService.GetAllsp_get_data_for_designer_review_dataAsync(
                 request, actionType);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            t.tran_designer_review_id,
                            t.tran_pre_costing_id,
                            t.tran_sample_order_id,
                            t.tran_sample_order_number,
                            t.order_date,
                            t.delivery_date,
                            t.unit_name,
                            t.order_quantity,
                            t.style_quantity,
                            t.style_code,
                            sample_photos_html = GetHtmlSampleOrderTableHTML(t),
                            //t.bp_yearly_gross_sales,
                            //t.style_product_size_group_id,
                            row_index = index++,
                            //t.event_gross_sales,
                            //t.range_plan_detail_id,
                            //t.range_plan_id,
                            //t.tran_bp_event_id,
                            //t.tran_bp_year_id,
                            //t.style_item_product_id,
                            //t.total_rangeplan_mrp_value,
                            //t.total_rangeplan_cpu_value,
                            //t.total_rangeplan_quantity,
                            //t.range_plan_quantity,
                            //t.cpu_per_pc_value,
                            //t.mrp_per_pc_value,
                            //t.mrp_value,
                            //t.cpu_value,
                            t.tran_va_plan_id,
                            t.tran_va_plan_detl_id,
                            t.tran_va_plan_event_id,
                            // t.remarks,
                            t.style_master_category_id,
                            t.is_separate_price,
                            //t.tran_range_plan_event_id,
                            //t.is_finalized,
                            //t.priority_id,
                            t.no_of_style,
                            t.style_code_gen,
                            // t.is_submitted,
                            // totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
                            //totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,

                            strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",

                            btnAddEditPreCosting = 
                           
                            "<input style='background-color:darkcyan;' class='btn btn-success' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='Edit Design Review' onclick='EditDesignReview(this)'>" +


                            "<input  class='btn btn-warning' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='View Design Review' onclick='ViewDesignReview(this)'>",
                            t.style_product_type_id



                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);

            return Json(ret_obj);

        }


        public async Task<IActionResult> GetDesignReviewProposedData(Filter_RangePlan_DataTable request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var actionType = 2;

            var designer_list = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

            List<rpc_sp_get_data_for_designer_review_DTO> records = await _TranDesignerReviewService.GetAllsp_get_data_for_designer_review_dataAsync(
                 request, actionType);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            t.tran_designer_review_id,
                            t.tran_pre_costing_id,
                            t.tran_sample_order_id,
                            t.tran_sample_order_number,
                            t.order_date,
                            t.delivery_date,
                            t.unit_name,
                            t.order_quantity,
                            t.style_quantity,
                            t.style_code,
                            sample_photos_html = GetHtmlSampleOrderTableHTML(t),
                            //t.bp_yearly_gross_sales,
                            //t.style_product_size_group_id,
                            row_index = index++,
                            //t.event_gross_sales,
                            //t.range_plan_detail_id,
                            //t.range_plan_id,
                            //t.tran_bp_event_id,
                            //t.tran_bp_year_id,
                            //t.style_item_product_id,
                            //t.total_rangeplan_mrp_value,
                            //t.total_rangeplan_cpu_value,
                            //t.total_rangeplan_quantity,
                            //t.range_plan_quantity,
                            //t.cpu_per_pc_value,
                            //t.mrp_per_pc_value,
                            //t.mrp_value,
                            //t.cpu_value,
                            t.tran_va_plan_id,
                            t.tran_va_plan_detl_id,
                            t.tran_va_plan_event_id,
                            // t.remarks,
                            t.style_master_category_id,
                            t.is_separate_price,
                            //t.tran_range_plan_event_id,
                            //t.is_finalized,
                            //t.priority_id,
                            t.no_of_style,
                            t.style_code_gen,
                            // t.is_submitted,
                            // totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
                            //totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,

                            strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",

                            btnAddEditPreCosting =


                            "<input  class='btn btn-warning' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='View Design Review' onclick='ViewDesignReview(this)'>",
                            t.style_product_type_id



                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);

            return Json(ret_obj);

        }





        [HttpGet]
        public async Task<IActionResult> DesignReviewAdd(string input)
        {
            #region commentSectionForFilteringController
            //SecurityCapsule sec_object = new SecurityCapsule();

            //var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            //List<Claim> listClaims = (List<Claim>)claim.Claims;

            //if (listClaims.Exists(c => c.Type == "secobject"))
            //{
            //    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

            //    sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);
            //}
            #endregion

            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            var objDbModel = await _TransampleorderService.GetSingleWithImageByIdAsync(temp_obj.tran_sample_order_id.Value);

            tran_designer_review_DTO model = new tran_designer_review_DTO();
            model.List_base64String_File = objDbModel.List_base64String_File;
            model.style_product = temp_obj.style_product;
            model.style_product_details = temp_obj.style_product_detail;
            model.style_quantity = temp_obj.style_quantity;
            model.style_code = temp_obj.style_code;
            model.tran_sample_order_number = objDbModel.tran_sample_order_number;
            model.order_date = objDbModel.order_date;
            model.date_added = DateTime.Now.Date;
            model.tran_pre_costing_id = temp_obj.tran_pre_costing_id;
            var designer_list = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

            model.designer = designer_list.Where(p => p.userid == sec_object.userid).FirstOrDefault().name;
            model.order_quantity = objDbModel.order_quantity;



            return PartialView("~/Views/DesignMgt/DesignReview/_DesignReviewNew.cshtml", model);

        }


        [HttpGet]
        public async Task<IActionResult> DesignReviewEdit(string input)
        {
           
            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            var objDbModel = await _TransampleorderService.GetSingleWithImageByIdAsync(temp_obj.tran_sample_order_id.Value);

            tran_designer_review_DTO model = new tran_designer_review_DTO();
            model.List_base64String_File = objDbModel.List_base64String_File;
            model.style_product = temp_obj.style_product;
            model.tran_designer_review_id = temp_obj.tran_designer_review_id;
            var tdr = await _TranDesignerReviewService.GetFullAsyncDesinerData(temp_obj.tran_designer_review_id ?? 0);

            model.style_product_details = temp_obj.style_product_detail;
            model.style_quantity = temp_obj.style_quantity;
            model.style_code = temp_obj.style_code;
            model.tran_sample_order_number = objDbModel.tran_sample_order_number;
            model.no_review = tdr.no_review;
            model.no_confirmation_with_modification = tdr.no_confirmation_with_modification;
            model.no_confirmation = tdr.no_confirmation;
            model.no_of_not_confirmed = tdr.no_of_not_confirmed;
            model.order_date = objDbModel.order_date;
            model.date_added = tdr.date_added;
            model.remarks = tdr.remarks;
            model.List_Files = tdr.List_Files;
            model.tran_pre_costing_id = temp_obj.tran_pre_costing_id;

            var designer_list = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

            model.designer = designer_list.Where(p => p.userid == sec_object.userid).FirstOrDefault().name;
            model.order_quantity = objDbModel.order_quantity;

            return PartialView("~/Views/DesignMgt/DesignReview/_DesignReviewEdit.cshtml", model);

        }


        [HttpGet]
        public async Task<IActionResult> ViewDesignReview(string input)
        {
           
            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            var objDbModel = await _TransampleorderService.GetSingleWithImageByIdAsync(temp_obj.tran_sample_order_id.Value);


            tran_designer_review_DTO model = new tran_designer_review_DTO();
            model.List_base64String_File = objDbModel.List_base64String_File;
            model.style_product = temp_obj.style_product;
            model.tran_designer_review_id = temp_obj.tran_designer_review_id;
            var tdr = await _TranDesignerReviewService.GetFullAsyncDesinerData(temp_obj.tran_designer_review_id ?? 0);

            model.style_product_details = temp_obj.style_product_detail;
            model.style_quantity = temp_obj.style_quantity;
            model.style_code = temp_obj.style_code;
            model.tran_sample_order_number = objDbModel.tran_sample_order_number;
            model.no_review = tdr.no_review;
            model.no_confirmation_with_modification = tdr.no_confirmation_with_modification;
            model.no_confirmation = tdr.no_confirmation;
            model.no_of_not_confirmed = tdr.no_of_not_confirmed;
            model.order_date = objDbModel.order_date;
            model.date_added = tdr.date_added;
            model.remarks = tdr.remarks;
            model.List_Files = tdr.List_Files;
            model.tran_pre_costing_id = temp_obj.tran_pre_costing_id;

            var designer_list = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

            if (designer_list.Where(p => p.userid == sec_object.userid).FirstOrDefault() != null)
            {
                model.designer = designer_list.Where(p => p.userid == sec_object.userid).FirstOrDefault().name;
            }

            model.order_quantity = objDbModel.order_quantity;

            return PartialView("~/Views/DesignMgt/DesignReview/_ViewDesignReview.cshtml", model);

        }

        [HttpPost]
        public async Task<ActionResult> SaveDesignReview([FromBody] tran_designer_review_DTO model)
        {

           
            tran_designer_review_entity entity = new tran_designer_review_entity();

            entity.added_by = sec_object.userid;
            entity.date_added = DateTime.Now;
            entity.tran_pre_costing_id = model.tran_pre_costing_id;

            entity.no_review = model.no_review == null ? 0 : model.no_review;
            entity.no_confirmation_with_modification = model.no_confirmation_with_modification==null?0:model.no_confirmation_with_modification;
            entity.no_confirmation = model.no_confirmation == null ? 0 : model.no_confirmation;
            entity.no_of_not_confirmed = model.no_of_not_confirmed == null ? 0 : model.no_of_not_confirmed;

            entity.remarks = model.remarks;
            entity.is_submitted = model.is_submitted;
            entity.is_approved = model.is_approved;

            entity.fiscal_year_id = Fiscal_Year_ID;
            entity.event_id = Event_ID;

            var ret = false;
            if (model.tran_designer_review_id is null)
                ret = await _TranDesignerReviewService.SaveAsync(entity, model.List_Files);
            else
            {
                entity.tran_designer_review_id = model.tran_designer_review_id;

                ret = await _TranDesignerReviewService.UpdateAsync(entity, model.List_Files, model.DeleteList);
            }

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Saving Failed"
            });

        }

        [HttpGet]
        public async Task<IActionResult> DesignReviewApprovalLanding()
        {
            Int64 fiscal_year_id = 0, eventid = 0, range_plan_id = 0, tran_va_plan_event_id = 0;
            

            tran_va_plan_DTO model = new tran_va_plan_DTO();

            GenSeasonEventConfigurationDTO objFilter = new GenSeasonEventConfigurationDTO();

            if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            {
                objFilter = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                if (objFilter == null)
                {
                    return RedirectToAction("Index", "Dashboard", new { load_filter = "1" });
                }
            }
            else
            {
                return RedirectToAction("Index", "Dashboard", new { load_filter = "1" });
            }


            model.tran_range_plan_id = objFilter.range_plan_id;
            fiscal_year_id = objFilter.fiscal_year_id;
            eventid = objFilter.event_id;
            range_plan_id = objFilter.range_plan_id;
            tran_va_plan_event_id = objFilter.tran_va_plan_event_id;
            ViewBag.tran_va_plan_event_id = tran_va_plan_event_id.ToString();

            var objFlter = await _rpc_db_service.GetAllproc_get_filter_itemsAsync(objFilter.fiscal_year_id);

            var fiscal_year_list = JsonConvert.DeserializeObject<List<gen_fiscal_year_dto>>(objFlter.genfiscalyear_list);//await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            ViewBag.fiscal_year_id = objFilter.fiscal_year_id.ToString();

            ViewBag.fiscal_year_list = fiscal_year_list.ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString(),
                                                       Selected = a.fiscal_year_id == objFilter.fiscal_year_id ? true : false
                                                   }
                                                   ).ToList();

            var gen_event_list = JsonConvert.DeserializeObject<List<gen_season_event_config>>(objFlter.genseasoneventconfig_list);


            ViewBag.event_id = objFilter.event_id.ToString();
            ViewBag.event_list = gen_event_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.event_title.ToString(),
                                                       Value = a.event_id.ToString(),
                                                       Selected = a.event_id == objFilter.event_id ? true : false

                                                   }
                                                   ).ToList();


            var style_item_type_list = JsonConvert.DeserializeObject<List<style_item_type_DTO>>(objFlter.styleitemtype_list);

            ViewBag.item_type_list = style_item_type_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_item_type_name.ToString(),
                                                       Value = a.style_item_type_id.ToString()
                                                   }
                                                   ).ToList();


            var product_type_list = JsonConvert.DeserializeObject<List<style_product_type_DTO>>(objFlter.styleproducttype_list);

            ViewBag.product_type_list = product_type_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_product_type_name.ToString(),
                                                       Value = a.style_product_type_id.ToString()
                                                   }
                                                   ).ToList();


            var gender_list = JsonConvert.DeserializeObject<List<style_gender_DTO>>(objFlter.stylegender_list);

            ViewBag.gender_list = gender_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_gender_name.ToString(),
                                                       Value = a.style_gender_id.ToString()
                                                   }
                                                   ).ToList();

            var origin_list = JsonConvert.DeserializeObject<List<style_item_origin_DTO>>(objFlter.styleitemorigin_list);

            ViewBag.origin_list = origin_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_item_origin_name.ToString(),
                                                       Value = a.style_item_origin_id.ToString()

                                                   }
                                                   ).ToList();

            return View("~/Views/DesignMgt/DesignReview/DesignReviewApprovalLanding.cshtml", model);

        }


        #region OldGridMethodForApproval

        //[HttpPost]
        //public async Task<IActionResult> GetDesignReviewApprovalData(Filter_RangePlan_DataTable request)
        //{
        //    request.fiscal_year_id = Fiscal_Year_ID;
        //    request.event_id = Event_ID;

        //    var designer_list = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

        //    List<rpc_sp_get_data_for_designer_review_DTO> records =  await _rpc_db_service.GetAllsp_get_data_for_designer_Approval_reviewAsync(
        //        request);

        //    var index = request.Start+1;
        //    var data = (from t in records
        //                select new
        //                {
        //                    t.tran_designer_review_id,
        //                    t.tran_pre_costing_id,
        //                    t.tran_sample_order_id,
        //                    t.tran_sample_order_number,
        //                    t.order_date,
        //                    t.delivery_date,
        //                    t.unit_name,
        //                    t.order_quantity,
        //                    t.style_quantity,
        //                    t.style_code,
        //                    t.approved_style,
        //                    sample_photos_html = GetHtmlSampleOrderTableHTML(t),
        //                    //t.bp_yearly_gross_sales,
        //                    //t.style_product_size_group_id,
        //                    row_index = index++,
        //                    //t.event_gross_sales,
        //                    //t.range_plan_detail_id,
        //                    //t.range_plan_id,
        //                    //t.tran_bp_event_id,
        //                    //t.tran_bp_year_id,
        //                    //t.style_item_product_id,
        //                    //t.total_rangeplan_mrp_value,
        //                    //t.total_rangeplan_cpu_value,
        //                    //t.total_rangeplan_quantity,
        //                    //t.range_plan_quantity,
        //                    //t.cpu_per_pc_value,
        //                    //t.mrp_per_pc_value,
        //                    //t.mrp_value,
        //                    //t.cpu_value,
        //                    t.tran_va_plan_id,
        //                    t.tran_va_plan_detl_id,
        //                    t.tran_va_plan_event_id,
        //                    // t.remarks,
        //                    t.style_master_category_id,
        //                    t.is_separate_price,
        //                    //t.tran_range_plan_event_id,
        //                    //t.is_finalized,
        //                    //t.priority_id,
        //                    t.no_of_style,
        //                    t.style_code_gen,
        //                    // t.is_approved,
        //                    totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
        //                    totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),
        //                    t.master_category_name,
        //                    product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
        //                    "-" + t.style_gender_name + "-" + t.master_category_name,
        //                    t.style_item_product_name,

        //                    strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",

        //                    btnViewDesignerPreview = t.is_approved != true ?
        //                    "<input style='background-color:darkcyan;' class='btn btn-success' style_quantity='" + t.style_quantity.ToString()
        //                    + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString()
        //                    + "' type='button' value='Approve Design' onclick='ViewDesignReviewForApproval(this)'>" :
        //                    "<input  class='btn btn-warning' style_quantity='" + t.style_quantity.ToString()
        //                    + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString()
        //                    + "' type='button' value='View Design Review' onclick='ViewDesignReview(this)'>",
        //                    t.style_product_type_id

        //                }).ToList();

        //    var ret_obj = new AjaxResponse(data.Count()>0? records[0].total_rows.Value:0, data);

        //    return Json(ret_obj);

        //}
        #endregion




        [HttpPost]
        public async Task<IActionResult> GetDesignReviewApprovalData(Filter_RangePlan_DataTable request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var actionType = 2;
            var designer_list = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

            List<rpc_sp_get_data_for_designer_review_DTO> records = await _TranDesignerReviewService.GetAllsp_get_data_for_designer_review_dataAsync(
                 request, actionType);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            t.tran_designer_review_id,
                            t.tran_pre_costing_id,
                            t.tran_sample_order_id,
                            t.tran_sample_order_number,
                            t.order_date,
                            t.delivery_date,
                            t.unit_name,
                            t.order_quantity,
                            t.style_quantity,
                            t.style_code,
                            t.approved_style,
                            sample_photos_html = GetHtmlSampleOrderTableHTML(t),
                            //t.bp_yearly_gross_sales,
                            //t.style_product_size_group_id,
                            row_index = index++,
                            //t.event_gross_sales,
                            //t.range_plan_detail_id,
                            //t.range_plan_id,
                            //t.tran_bp_event_id,
                            //t.tran_bp_year_id,
                            //t.style_item_product_id,
                            //t.total_rangeplan_mrp_value,
                            //t.total_rangeplan_cpu_value,
                            //t.total_rangeplan_quantity,
                            //t.range_plan_quantity,
                            //t.cpu_per_pc_value,
                            //t.mrp_per_pc_value,
                            //t.mrp_value,
                            //t.cpu_value,
                            t.tran_va_plan_id,
                            t.tran_va_plan_detl_id,
                            t.tran_va_plan_event_id,
                            // t.remarks,
                            t.style_master_category_id,
                            t.is_separate_price,
                            //t.tran_range_plan_event_id,
                            //t.is_finalized,
                            //t.priority_id,
                            t.no_of_style,
                            t.style_code_gen,
                            // t.is_approved,
                            totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
                            totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,

                            strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",

                            btnViewDesignerPreview = 
                            "<input style='background-color:darkcyan;' class='btn btn-warning' style_quantity='" + t.style_quantity.ToString()
                            + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString()
                            + "' type='button' value='Approve Design' onclick='ViewDesignReviewForApproval(this)'>",

                             t.style_product_type_id


                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);

            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetDesignReviewApprovedData(Filter_RangePlan_DataTable request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var actionType = 3;
            var designer_list = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

            List<rpc_sp_get_data_for_designer_review_DTO> records = await _TranDesignerReviewService.GetAllsp_get_data_for_designer_review_dataAsync(
                 request, actionType);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            t.tran_designer_review_id,
                            t.tran_pre_costing_id,
                            t.tran_sample_order_id,
                            t.tran_sample_order_number,
                            t.order_date,
                            t.delivery_date,
                            t.unit_name,
                            t.order_quantity,
                            t.style_quantity,
                            t.style_code,
                            t.approved_style,
                            sample_photos_html = GetHtmlSampleOrderTableHTML(t),
                            //t.bp_yearly_gross_sales,
                            //t.style_product_size_group_id,
                            row_index = index++,
                            //t.event_gross_sales,
                            //t.range_plan_detail_id,
                            //t.range_plan_id,
                            //t.tran_bp_event_id,
                            //t.tran_bp_year_id,
                            //t.style_item_product_id,
                            //t.total_rangeplan_mrp_value,
                            //t.total_rangeplan_cpu_value,
                            //t.total_rangeplan_quantity,
                            //t.range_plan_quantity,
                            //t.cpu_per_pc_value,
                            //t.mrp_per_pc_value,
                            //t.mrp_value,
                            //t.cpu_value,
                            t.tran_va_plan_id,
                            t.tran_va_plan_detl_id,
                            t.tran_va_plan_event_id,
                            // t.remarks,
                            t.style_master_category_id,
                            t.is_separate_price,
                            //t.tran_range_plan_event_id,
                            //t.is_finalized,
                            //t.priority_id,
                            t.no_of_style,
                            t.style_code_gen,
                            // t.is_approved,
                            totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
                            totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,

                            strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",

                            btnViewDesignerPreview = 
                            
                            "<input  class='btn btn-warning' style_quantity='" + t.style_quantity.ToString()
                            + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString()
                            + "' type='button' value='View Design Review' onclick='ViewDesignReview(this)'>",
                            t.style_product_type_id

                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);

            return Json(ret_obj);

        }



        [HttpGet]
        public async Task<IActionResult> DesignApprovalReviewforApprove(string input)
        {
            
            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            var objDbModel = await _TransampleorderService.GetSingleWithImageByIdAsync(temp_obj.tran_sample_order_id.Value);

            tran_designer_review_DTO model = new tran_designer_review_DTO();
            model.List_base64String_File = objDbModel.List_base64String_File;
            model.style_product = temp_obj.style_product;
            model.tran_designer_review_id = temp_obj.tran_designer_review_id;
            var tdr = await _TranDesignerReviewService.GetFullAsyncDesinerData(temp_obj.tran_designer_review_id ?? 0);

            model.style_product_details = temp_obj.style_product_detail;
            model.style_quantity = temp_obj.style_quantity;
            model.style_code = temp_obj.style_code;
            model.tran_sample_order_number = objDbModel.tran_sample_order_number;
            model.no_review = tdr.no_review;
            model.no_confirmation_with_modification = tdr.no_confirmation_with_modification;
            model.no_confirmation = tdr.no_confirmation;
            model.no_of_not_confirmed = tdr.no_of_not_confirmed;
            model.order_date = objDbModel.order_date;
            model.date_added = tdr.date_added;
            model.remarks = tdr.remarks;
            model.List_Files = tdr.List_Files;
            model.tran_pre_costing_id = temp_obj.tran_pre_costing_id;

            var designer_list = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

            model.designer = designer_list.Where(p => p.userid == sec_object.userid).FirstOrDefault().name;
            model.order_quantity = objDbModel.order_quantity;



            return PartialView("~/Views/DesignMgt/DesignReview/_DesignApprovalReview.cshtml", model);

        }

        [HttpPost]
        public async Task<ActionResult> ApproveDesign([FromBody] tran_designer_review_DTO model)
        {

            tran_designer_review_entity entity = new tran_designer_review_entity();
            entity = await _TranDesignerReviewService.GetAsync(model.tran_designer_review_id ?? 0);
            entity.approved_by = sec_object.userid;
            entity.approve_date = DateTime.Now;
            entity.approve_remarks = model.approve_remarks;
            entity.is_approved = model.is_approved;
            entity.tran_designer_review_id = model.tran_designer_review_id;

            var ret = await _TranDesignerReviewService.ApproveAsync(entity);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Saving Failed"
            });

        }



    }
}
