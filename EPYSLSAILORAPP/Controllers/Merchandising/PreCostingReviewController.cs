using AutoMapper;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using EPYSLSAILORAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using DnsClient.Protocol;
using EPYSLSAILORAPP.Application.DTO;
using ServiceStack.Messaging;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using EPYSLSAILORAPP.Application.Interface;
using System.Globalization;
using BDO.Core.Base;
using EPYSLSAILORAPP.Infrastructure.Services.GenServices;
using System.Text;
using Web.Core.Frame.Helpers;
using System.Runtime.InteropServices;
using EPYSLSAILORAPP.Domain.Entity.Tran_Tables;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.Interface.Common;
using Elasticsearch.Net;
using EPYSLSAILORAPP.SystemServices;
using EPYSLSAILORAPP.Infrastructure.Services.BusinessPlanning;
using ServiceStack;
using System.Drawing.Imaging;
using System.Collections.Generic;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Memory;
using Org.BouncyCastle.Ocsp;
using System.Web.Razor;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Application.Interface.Security;

namespace EPYSLSAILORAPP.Controllers.DesignManagement
{
    [RequestFormLimits(MultipartBodyLengthLimit = 2000000000)]
    [RequestSizeLimit(2000000000)]

    public class PreCostingReviewController : ExtendedBaseController
    {
        private readonly ILogger<PreCostingReviewController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBusinessPlanService _BP_service;
        private readonly IGenProcessMasterService _GenProcessMasterService;
        private readonly ITranPreCostingReviewService _TranPreCostingReviewService;
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

        private readonly IGenItemStructureGroupSubSegmentMappingService _GenItemStructureGroupSubSegmentMappingService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;

       // private readonly IStyleembellishmentService _StyleembellishmentService;
        private readonly IStyleproductsizegroupdetailsService _StyleproductsizegroupdetailsService;

        
       
        
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

        public PreCostingReviewController(IBusinessPlanService BusinessPlanService,
            IGenFiscalYearService gen_fiscal_year_Service, IGenOutletService gen_outlet_entity_service, ITran_BP_YearService tran_bp_yearservice,
            ITran_BP_EventMonthService tran_bp_eventmonthservice, ITran_BP_EventMonthOutletService tran_bp_eventmonthoutletservice,
            IMapper mapper, ILogger<PreCostingReviewController> logger, IHttpContextAccessor contextAccessor,
            IStylecategoryService StylecategoryService,
            IStylegenderService StylegenderService,
            IStyleitemoriginService StyleitemoriginService,
            IStyleitemproductService StyleitemproductService,
            IStyleitemtypeService StyleitemtypeService,
            
           
            
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
             ITranPreCostingReviewService TranPreCostingReviewService,
            IGenItemStructureGroupSubSegmentMappingService GenItemStructureGroupSubSegmentMappingService,
            IStyleproductsizegroupdetailsService StyleproductsizegroupdetailsService,
            ITransampleorderembellishmentService TransampleorderembellishmentService, 
            IStyleproducttypeService StyleproducttypeService, ITranrangeplanService tran_rangeplanservice
            , IGenSeasonEventConfigurationService genseasoneventconfigurationservice, IOwinUserService OwinUserService
            , IRPCDbService rpc_db_service, IGenPriorityService gen_priority_service, IConfiguration configuration,
            ITransampleorderService transampleorderService, IGenItemStructureGroupSubService IGenItemStructureGroupSubService, IGenUnitService GenUnitService) : base(contextAccessor, configuration)
        {
            _BP_service = BusinessPlanService;
            _gen_fiscal_year_Service = gen_fiscal_year_Service;
            _gen_outlet_entity_service = gen_outlet_entity_service;
            _tran_bp_yearservice = tran_bp_yearservice;
            _mapper = mapper;
            _logger = logger;
            _tran_rangeplanservice = tran_rangeplanservice;
            _genseasoneventconfigurationservice = genseasoneventconfigurationservice;

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

            
           
           
            _TranPlanAllocateService = TranPlanAllocateService;
            _TranPreCostingReviewService = TranPreCostingReviewService;
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
        public async Task<IActionResult> PreCostingReviewLanding()//(string fiscal_year_id, string eventid, string range_plan_id, string tran_va_plan_event_id)
        {
            tran_va_plan_DTO model = new tran_va_plan_DTO();
           
            

            

           
            //if (sec_object.gen_team_group_id != Convert.ToInt64(Team_Category.Merchandiser) &&
            //    sec_object.roleid != Convert.ToInt64(Enum_Role.Admin))
            //{
            //    ViewBag.IsNotDesigner = "This page is only for the Merchandiser";
            //}

            //GenSeasonEventConfigurationDTO objFilter = new GenSeasonEventConfigurationDTO();

            


            //ViewBag.encoded_fiscal_year = objFilter.fiscal_year_id.ToString();

            //ViewBag.tran_va_plan_event_id = objFilter.tran_va_plan_event_id.ToString();

            //model.tran_range_plan_id = objFilter.range_plan_id;

            //var objFlter = await _rpc_db_service.GetAllproc_get_filter_itemsAsync(objFilter.fiscal_year_id);

            //var fiscal_year_list = JsonConvert.DeserializeObject<List<gen_fiscal_year_dto>>(objFlter.genfiscalyear_list);//await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            //ViewBag.fiscal_year_id = objFilter.fiscal_year_id.ToString();

            //ViewBag.fiscal_year_list = fiscal_year_list.ToList()
            //    .Select(a =>
            //                                       new SelectListItem
            //                                       {
            //                                           Text = a.year_name.ToString(),
            //                                           Value = a.fiscal_year_id.ToString(),
            //                                           Selected = a.fiscal_year_id == objFilter.fiscal_year_id ? true : false
            //                                       }
            //                                       ).ToList();


            //if (!String.IsNullOrEmpty(objFlter.genseasoneventconfig_list))
            //{
            //    var gen_event_list = JsonConvert.DeserializeObject<List<gen_season_event_config>>(objFlter.genseasoneventconfig_list);


            //    ViewBag.event_id = objFilter.event_id.ToString();
            //    ViewBag.event_list = gen_event_list
            //        .Select(a =>
            //                                           new SelectListItem
            //                                           {
            //                                               Text = a.event_title.ToString(),
            //                                               Value = a.event_id.ToString(),
            //                                               Selected = a.event_id == objFilter.event_id ? true : false

            //                                           }
            //                                           ).ToList();
            //}


            //var style_item_type_list = JsonConvert.DeserializeObject<List<style_item_type_DTO>>(objFlter.styleitemtype_list);

            //ViewBag.item_type_list = style_item_type_list
            //    .Select(a =>
            //                                       new SelectListItem
            //                                       {
            //                                           Text = a.style_item_type_name.ToString(),
            //                                           Value = a.style_item_type_id.ToString()
            //                                       }
            //                                       ).ToList();


            //var product_type_list = JsonConvert.DeserializeObject<List<style_product_type_DTO>>(objFlter.styleproducttype_list);

            //ViewBag.product_type_list = product_type_list
            //    .Select(a =>
            //                                       new SelectListItem
            //                                       {
            //                                           Text = a.style_product_type_name.ToString(),
            //                                           Value = a.style_product_type_id.ToString()
            //                                       }
            //                                       ).ToList();


            //var gender_list = JsonConvert.DeserializeObject<List<style_gender_DTO>>(objFlter.stylegender_list);

            //ViewBag.gender_list = gender_list
            //    .Select(a =>
            //                                       new SelectListItem
            //                                       {
            //                                           Text = a.style_gender_name.ToString(),
            //                                           Value = a.style_gender_id.ToString()
            //                                       }
            //                                       ).ToList();

            //var origin_list = JsonConvert.DeserializeObject<List<style_item_origin_DTO>>(objFlter.styleitemorigin_list);

            //ViewBag.origin_list = origin_list
            //    .Select(a =>
            //                                       new SelectListItem
            //                                       {
            //                                           Text = a.style_item_origin_name.ToString(),
            //                                           Value = a.style_item_origin_id.ToString()

            //                                       }
            //                                       ).ToList();

            return View("~/Views/MerchandiserMgt/PreCostingReview/PreCostingReviewLanding.cshtml", model);
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
        [HttpPost]
        public async Task<IActionResult> GetTranPreCostingData(tran_va_plan_Filter_DTO request)
        {
           
            List<rpc_sp_get_data_for_pre_costing_review_DTO> records = new List<rpc_sp_get_data_for_pre_costing_review_DTO>();

            var saved_records = await _rpc_db_service.GetAllsp_get_data_for_pre_costing_reviewAsync(
                request.fiscal_year_id.Value,request.event_id.Value, null, sec_object.userid.Value, request.data_filter_type, request.search.Value );

            records = _mapper.Map<List<rpc_sp_get_data_for_pre_costing_review_DTO>>(saved_records);

            var index = 1;

            var data = (from t in records
                        select new
                        {
                            t.tran_pre_costing_review_id,
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
                            t.techpack_number,
                            totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
                            totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),
                            
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,
                            strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",
                            btnAddEditPreCosting =t.tran_pre_costing_review_id==null? "<input style='width:150px;background-color:darkcyan;' class='btn btn-primary'  style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' type='button' value='Review ' onclick='EditPreCosting(this)'>":
                             "<input style='width:150px;background-color:darkcyan;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_pre_costing_review_id='" + t.tran_pre_costing_review_id.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' type='button' value='Review' onclick='PreCosting_DesignerApproved(this)'>",
                            btnAcknoledgedPreCosting = "<input style='width:150px;background-color:#617575;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_review_id='" + t.tran_pre_costing_review_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' type='button' value='View' onclick='PreCosting_Acknoledged(this,0)'>",
                            btnRequestForReviewPreCosting = "<input style='width:150px;background-color:#617575;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_review_id='" + t.tran_pre_costing_review_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' type='button' value='View' onclick='PreCosting_RequestedForReview(this,0)'>",
                            btnReviewedByDesignerPreCosting = "<input style='width:150px;background-color:darkcyan;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_pre_costing_review_id='" + t.tran_pre_costing_review_id.ToString() + "' tran_pre_costing_id='" + t.tran_pre_costing_id.ToString() + "' type='button' value='Review ' onclick='PreCosting_ReviewedByDesigner(this,2)'>",

                            t.style_product_type_id

                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count(), data);

            return Json(ret_obj);

        }

        private static List<tran_plan_allocate_style_DTO> GenerateStyleList(tran_va_plan_Filter_DTO? temp_obj, List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>? list_color_size)
        {
            var ListStyle = list_color_size.
                //.Where(p => p.tran_va_plan_detl_style_id == temp_obj.tran_va_plan_detl_style_id).
                GroupBy(p =>
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
        public async Task<IActionResult> PreCostingEdit(string input)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
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

            model.style_product = temp_obj.style_product;
            model.style_product_details = temp_obj.style_product_detail;
            model.style_quantity = temp_obj.style_quantity;
            model.style_code = temp_obj.style_code;
            model.order_quantity = temp_obj.order_quantity;
            model.sample_order.List_base64String_File = proc_sp_get_pre_costing_New.List_SampleOrderFiles;

            var mapping_structure_List = JsonConvert.DeserializeObject<List<rpc_sp_get_mapped_item_structure_DTO>>(proc_sp_get_pre_costing_New.mapping_structure_list);

            var objstructure_List_DB= JsonConvert.DeserializeObject<List<rpc_sp_get_sampleorder_subgroup_det_DTO>>(proc_sp_get_pre_costing_New.sampleorder_subgroup_list);

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

            var list= JsonConvert.DeserializeObject<List<gen_process_master_DTO>>(proc_sp_get_pre_costing_New.gen_process_master_list);

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

                foreach(var objsingle in model.List_PreCostingDet_rpc)
                {
                    objsingle.List_placement_info= objsingle.placement_info!=null? objsingle.placement_info.ToObject<List<style_placement_information_DTO>>():new List<style_placement_information_DTO>();
                    objsingle.placement_info = null;
                }
            }

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.pre_costing_subcontract_list))
                model.List_SubContractDetl = JsonConvert.DeserializeObject<List<tran_pre_costing_item_subcontract_detail_DTO>>(proc_sp_get_pre_costing_New.pre_costing_subcontract_list);
            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.pre_costing_embellishment_list))
                model.List_EmbellishmentDetl = JsonConvert.DeserializeObject<List<tran_pre_costing_item_embellishment_detail_DTO>>(proc_sp_get_pre_costing_New.pre_costing_embellishment_list);

            var row_index = 0;

            foreach ( var item in model.List_PreCostingDet_rpc) 
            {
                item.row_index = row_index++;
                item.current_state = 2;
            }
            
            var list_color_size= JsonConvert.DeserializeObject<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>>(proc_sp_get_pre_costing_New.color_detl_size_list);

            List<tran_plan_allocate_style_DTO> ListStyle = GenerateStyleList(temp_obj, list_color_size);

            model.List_plan_detl_style = ListStyle;
            
            model.List_detl_style_color =JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(model.color_wise_size_quantity);

            return PartialView("~/Views/MerchandiserMgt/PreCostingReview/_PreCostingReview.cshtml", model);

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
                var jsonstring =clsUtil.CleanJsonString( proc_sp_get_pre_costing_Review.pre_costing_detail_list);

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

                proc_sp_get_pre_costing_Review.List_detl_style_color = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>> (jsonstring);
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

            return PartialView("~/Views/MerchandiserMgt/PreCostingReview/_PreCostingReviewVersons.cshtml", proc_sp_get_pre_costing_Review);

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

            var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(measurement_unit_id));

            ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            {
                Text = p.unit_detail_display,
                Value = p.gen_measurement_unit_detail_id.ToString(),
                Selected = p.gen_measurement_unit_detail_id.ToString() == default_measurement_unit_detail_id ? true : false,
            }).ToList();

            var list_placement = JsonConvert.DeserializeObject<List<style_placement_information_DTO>>(list[0].styleplacementinformation_list);

            ViewBag.list_placement = list_placement.Select(p => new SelectListItem
            {
                Text = p.placement,
                Value = p.style_placement_information_id.ToString(),
            }).ToList();

            ViewBag.gen_item_structure_group_sub_id = Convert.ToInt32(gen_item_structure_group_sub_id);

            ViewBag.item_structure_group_id = Convert.ToInt32(item_structure_group_id);

            return PartialView("~/Views/MerchandiserMgt/PreCostingReview/_AddFabricDetails.cshtml", list);

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

            return PartialView("~/Views/MerchandiserMgt/PreCostingReview/_AddSubContractDetails.cshtml");

        }

        [HttpGet]
        public async Task<IActionResult> AddNewEmbellishmentDetails(string embellishment_id, string embellishment)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {
            

            ViewBag.embellishment_id = Convert.ToInt32(embellishment_id);
            ViewBag.embellishment = embellishment;

            return PartialView("~/Views/MerchandiserMgt/PreCostingReview/_AddEmbellishmentDetails.cshtml");

        }


        [HttpPost]
        public async Task<ActionResult> UpdatePreCostingReview([FromBody] tran_pre_costing_DTO model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {
            
            model.added_by = sec_object.userid;
            
            model.pre_costing_review.added_by = sec_object.userid;
            model.pre_costing_review.merchant_ack_by=sec_object.userid;
            model.pre_costing_review.date_added = DateTime.Now;

            var ret = await _TranpreconstingService.PreCostingReviewAdd(model);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Updating Failed"
            });

        }

        [HttpPost]
        public async Task<ActionResult> MerchantAckPreCosting([FromBody] tran_pre_costing_review_DTO model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {
           
            model.added_by = sec_object.userid;

            model.is_ack_by_merchant = 1;
            model.merchant_ack_by = sec_object.userid;
            model.merchant_ack_date = DateTime.Now;

            var ret = await _TranPreCostingReviewService.UpdateReviewAck(model);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Updating Failed"
            });

        }


    }
}
