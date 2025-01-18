using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.Security;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers.DesignManagement
{
    [RequestFormLimits(MultipartBodyLengthLimit = 2000000000)]
    [RequestSizeLimit(2000000000)]

    public class TechPackController : ExtendedBaseController
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        private readonly ILogger<TechPackController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IStylegenderService _StylegenderService;
        private readonly IGenGarmentPartService _GenGarmentPartService;
        private readonly IGenProcessMasterDetailService _GenProcessMasterDetailService;
        private readonly IStyleitemoriginService _StyleitemoriginService;
        private readonly IStyleitemtypeService _StyleitemtypeService;
        private readonly IStyleproducttypeService _StyleproducttypeService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly ITranPlanAllocateService _TranPlanAllocateService;
        private readonly ITransampleorderService _TransampleorderService;
        private readonly IGenUnitService _GenUnitService;
        private readonly ITran_BP_YearService _tran_bp_yearservice;
        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private readonly IOwinUserService _OwinUserService;
        private readonly ITranDesignerReviewService _TranDesignerReviewService;
        private readonly ITrantechpackService _trantechpackService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IGenSeasonEventConfigurationService _genseasoneventconfigurationservice;
        private readonly IStyleproductunitService _StyleproductunitService;
        private readonly ITransampleorderdetlService _TransampleorderdetlService;
        private readonly IStyleproductsizegroupdetailsService _StyleproductsizegroupdetailsService;
        private readonly ITranPlanAllocateStyleColorService _tranvaplandetlstylecolorService;
        private readonly IMapper _mapper;
        private readonly ITranpreconstingService _TranpreconstingService;
       
        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        private IRedisService<rpc_range_plan_getfor_createupdate_DTO> _redisRangePlanService;


        public TechPackController(IBusinessPlanService BusinessPlanService, ITranDesignerReviewService TranDesignerReviewService,
            IGenFiscalYearService gen_fiscal_year_Service, IGenOutletService gen_outlet_entity_service, ITran_BP_YearService tran_bp_yearservice,
            ITran_BP_EventMonthService tran_bp_eventmonthservice, ITran_BP_EventMonthOutletService tran_bp_eventmonthoutletservice,
            IMapper mapper, ILogger<TechPackController> logger, IHttpContextAccessor contextAccessor,
            IStylecategoryService StylecategoryService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IStylegenderService StylegenderService,
            IStyleitemoriginService StyleitemoriginService,
            IStyleitemproductService StyleitemproductService,
            IStyleitemtypeService StyleitemtypeService,

           
            
            IGenProcessMasterDetailService GenProcessMasterDetailService,
            ITranPlanAllocateService TranPlanAllocateService,
            IStyleproductunitService StyleproductunitService,
           //ITranvaproductembellishmentmappingService TranvaproductembellishmentmappingService,
           // IStyleembellishmentService StyleembellishmentService,
           ITrantechpackService trantechpackService,
            IStyleitemproductsubcategoryService IStyleitemproductsubcategoryService,
            IStylelabelService StylelabelService, ITranrangeplandetailssizeService tran_rangeplandetailssizeservice,
            ITranPlanAllocateStyleService tranvaplandetlstyleService,
            IStylemastercategoryService StylemastercategoryService,
            ITransampleorderdetlService TransampleorderdetlService,
             IGenProcessMasterService GenProcessMasterService,
             ITranpreconstingService TranpreconstingService,
             IGenGarmentPartService GenGarmentPartService,
             ITranPlanAllocateStyleColorService tranvaplandetlstylecolorService,
            IGenItemStructureGroupSubSegmentMappingService GenItemStructureGroupSubSegmentMappingService,
            IStyleproductsizegroupdetailsService StyleproductsizegroupdetailsService,
            ITransampleorderembellishmentService TransampleorderembellishmentService, 
            IStyleproducttypeService StyleproducttypeService, ITranrangeplanService tran_rangeplanservice
            , IGenSeasonEventConfigurationService genseasoneventconfigurationservice, IOwinUserService OwinUserService
            , IRPCDbService rpc_db_service, IGenPriorityService gen_priority_service, IConfiguration configuration,
            ITransampleorderService transampleorderService, IGenItemStructureGroupSubService IGenItemStructureGroupSubService,
            IGenUnitService GenUnitService) : base(contextAccessor, configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _gen_fiscal_year_Service = gen_fiscal_year_Service;
            _tran_bp_yearservice = tran_bp_yearservice;
            _mapper = mapper;
            _logger = logger;
            _trantechpackService = trantechpackService;
            _genseasoneventconfigurationservice = genseasoneventconfigurationservice;
            _rpc_db_service = rpc_db_service;
            _OwinUserService = OwinUserService;
            _contextAccessor = contextAccessor;
            _TranPlanAllocateService = TranPlanAllocateService;
            _StylegenderService = StylegenderService;
            _StyleitemoriginService = StyleitemoriginService;
            _StyleitemtypeService = StyleitemtypeService;
            _StyleproducttypeService = StyleproducttypeService;
            _StyleproductunitService = StyleproductunitService;
            _TransampleorderdetlService = TransampleorderdetlService;
            _logger.LogInformation("Hello from inside SampleOrder !");
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            _redisRangePlanService = new RedisService<rpc_range_plan_getfor_createupdate_DTO>(_configuration);
            _StyleproductsizegroupdetailsService = StyleproductsizegroupdetailsService;
            _tranvaplandetlstylecolorService = tranvaplandetlstylecolorService;
            _TranpreconstingService = TranpreconstingService;
            _GenGarmentPartService = GenGarmentPartService;
            _GenProcessMasterDetailService = GenProcessMasterDetailService;

            
            _TransampleorderService = transampleorderService;
            _TranDesignerReviewService = TranDesignerReviewService;
            _GenUnitService = GenUnitService;
        }


        [HttpGet]
        public async Task<IActionResult> TechPackLanding()
        {
            Int64 fiscal_year_id = 0, eventid = 0, range_plan_id = 0, tran_va_plan_event_id = 0;
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

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

            return View("~/Views/DesignMgt/TechPack/TechPackLanding.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> TechPackApprovalLanding()
        {
            Int64 fiscal_year_id = 0, eventid = 0, range_plan_id = 0, tran_va_plan_event_id = 0;
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

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

            return View("~/Views/DesignMgt/TechPack/TechPackApprovalLanding.cshtml", model);
        }

        private string GetHtmlSampleOrderTableHTML(rpc_sp_get_data_for_techpack_DTO sample_order)
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
        public async Task<IActionResult> GetTechPackData(Filter_RangePlan_DataTable request)
        {
            request.userid=sec_object.userid;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetAllsp_get_data_for_techpackAsync(
                request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            t.techpack_number,
                            strtechpack_date = (t.techpack_date.HasValue ? t.techpack_date.Value.ToString("dd-MMM-yyyy") : ""),
                            t.tran_techpack_id,
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
                            t.tran_va_plan_detl_style_id,
                            t.delivery_unit_id,
                            sample_photos_html = GetHtmlSampleOrderTableHTML(t),
                            t.style_product_size_group_id,
                            row_index = index++,
                            t.merchant_name,
                            t.style_item_product_id,

                            t.tran_va_plan_id,
                            t.tran_va_plan_detl_id,
                            t.tran_va_plan_event_id,
                            t.remarks,
                            t.style_master_category_id,
                            t.is_separate_price,
                            
                            t.no_of_style,
                            t.style_code_gen,

                            totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
                            totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,

                            strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",

                            btnAddEditTechpack = t.tran_techpack_id == null ?
                            "<input style='width:150px;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='Add Tech Pack' onclick='AddTechPack(this)'>"
                            : "<input style='width:150px;background-color:darkcyan;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "'  tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_techpack_id='" + t.tran_techpack_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='Modify ' onclick='EditTechPack(this)'>",
                            t.style_product_type_id,

                            btnView = "<input style='width:150px;background-color:darkcyan;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "'  tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_techpack_id='" + t.tran_techpack_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='View' onclick='ViewTechPack(this)'>",

                            btnApprove = "<input style='width:150px;background-color:darkcyan;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "'  tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_techpack_id='" + t.tran_techpack_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='Approve' onclick='LoadTechPackApprove(this)'>"

                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);

            return Json(ret_obj);

        }

        [HttpGet]
        public async Task<IActionResult> TechPackAdd(string input)
        {

            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            var proc_sp_get_pre_costing_New = await _rpc_db_service.GetAllproc_sp_get_tech_pack_dataAsync
              (
                  temp_obj.tran_pre_costing_id,
                  temp_obj.tran_sample_order_id.Value, temp_obj.style_item_product_id.Value,
                  temp_obj.fiscal_year_id.Value
              );

            tran_sample_order_DTO model_SampleOrder = _mapper.Map<tran_sample_order_DTO>(proc_sp_get_pre_costing_New);

            tran_pre_costing_DTO model_PreCosting = _mapper.Map<tran_pre_costing_DTO>(proc_sp_get_pre_costing_New);

            var list_unitsdto = JsonConvert.DeserializeObject<List<gen_unit_DTO>>(proc_sp_get_pre_costing_New.gen_unit_list);

            var List_SizeGroupDetl = JsonConvert.DeserializeObject<List<style_product_size_group_details_DTO>>(proc_sp_get_pre_costing_New.style_product_sizegroupdetails_list);

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
            var List_AllSizeGroupDet = JsonConvert.DeserializeObject<List<style_product_size_group_details_entity>>(proc_sp_get_pre_costing_New.style_product_sizegroupdetails_list);

            var List_VAStyle_ColorInfo = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(proc_sp_get_pre_costing_New.style_color_size_details);

            tran_tech_pack_DTO model = new tran_tech_pack_DTO();

            model.List_PreCostingDet_rpc = JsonConvert.DeserializeObject<List<rpc_sp_get_pre_costing_details_by_masterid_DTO>>(proc_sp_get_pre_costing_New.pre_costing_detail_list); ;

            model.Color_List = List_VAStyle_ColorInfo;

            model.Color_Group_Details = _mapper.Map<List<style_product_size_group_details_DTO>>(List_AllSizeGroupDet);

            model.List_detl_style_color = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(proc_sp_get_pre_costing_New.precosting_colorwisesize_quantity);

            model.style_code = temp_obj.style_code;

            model.pre_costing_quantity = model_PreCosting.pre_costing_quantity;
           
            model.smv = model_PreCosting.smv;

            model.style_item_product_id = temp_obj.style_item_product_id;

            model.style_item_product_name = temp_obj.style_product;

            model.List_Unit = list_unitsdto.Select(p => new SelectListItem
            {
                Text = p.unit_name,
                Value = p.gen_unit_id.ToString()
            }).ToList();

            model.List_Detl = JsonConvert.DeserializeObject<List<tran_sample_order_detl_DTO>>(proc_sp_get_pre_costing_New.sample_order_detaillist);

            var List_Unit = JsonConvert.DeserializeObject<List<style_product_unit_DTO>>(proc_sp_get_pre_costing_New.style_product_unit_list);

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.sample_order_embellishmentlist))
            {
                var objembellishment_db = JsonConvert.DeserializeObject<List<rpc_sp_get_sampleorder_embellishment_det_DTO>>(proc_sp_get_pre_costing_New.sample_order_embellishmentlist);

                ViewBag.embellishment = objembellishment_db.ToList();
            }

            foreach (var obj in model.List_Detl)
            {
                obj.txtcolor_code = "<input type='color' value='" + obj.color_code + "' class='border-gray-200 rounded-sm txtvalcolor_code' onchange='LoadColorFromBox(this)' style='width:30%;float:left;' />" +
                     "<input   pattern='^#+([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$' value='" + obj.color_code + "' type='text' class='border-gray-200 rounded-sm txtColorCode' style='width:65%;float:right;' /> ";

                obj.txtorder_quantity = "<input type='number' style='width:80px;' min='0' value='" + obj.order_quantity.ToString() + "' onchange='calc_total();'  class='border-gray-200 rounded-sm txtorder_quantity' />";
                obj.strddlSizeHTML = GetDDLProductSizeHTML(List_SizeGroupDetl, obj.style_product_size_group_detid);
                obj.strddlUnitHTML = GetDDLUnitHTML(List_Unit, obj.style_product_unit_id);
                obj.btnAddDeleteRowHTML = "<button tran_sample_order_detl_id='" + obj.tran_sample_order_detl_id + "' type='button' style='width: 70px;background-color:darkred' onclick='btnAddDeleteRow(this)' class='btn btn-secondary btnAddDeleteRow '>Remove</button>";
            }

            model.List_Mapped_Structure = structure_group_List;

            model.List_base64String_File = proc_sp_get_pre_costing_New.List_SampleOrderFiles;
            model.style_product = temp_obj.style_product;
            model.style_product_details = temp_obj.style_product_detail;
            model.style_quantity = temp_obj.style_quantity;
            model.teckpack_style_code = temp_obj.style_code;
            model.tran_sample_order_number = model_SampleOrder.tran_sample_order_number;
            model.order_date = model_SampleOrder.order_date;
            model.date_added = DateTime.Now.Date;
            model.tran_pre_costing_id = temp_obj.tran_pre_costing_id;
            model.techpack_date = DateTime.Now.Date;

            //var designer_list = JsonConvert.DeserializeObject<List<owin_user_DTO>>(proc_sp_get_pre_costing_New.designer_list);

            model.designer = proc_sp_get_pre_costing_New.name;
            model.order_quantity = model_SampleOrder.order_quantity;
            model.tran_designer_review_id = temp_obj.tran_designer_review_id;
            model.delivery_unit_id = temp_obj.delivery_unit_id;
            model.delivery_date = temp_obj.delivery_date;
            var objgarment_part_List = JsonConvert.DeserializeObject<List<gen_garment_part_DTO>>(proc_sp_get_pre_costing_New.gen_garment_partlist);

            ViewBag.garment_part_List = objgarment_part_List
                .Select(a =>
                            new SelectListItem
                            {
                                Text = a.garment_part_name,
                                Value = a.gen_garment_part_id.ToString()
                            }
                        ).ToList();

            //ViewBag.merchandiser_List = designer_list.Select(a =>
            //                new SelectListItem
            //                {
            //                    Text = a.name,
            //                    Value = a.userid.ToString()
            //                }
            //            ).ToList(); ;


            ViewBag.Genprocessmasterdetail_List = JsonConvert.DeserializeObject<List<gen_process_master_detail_entity>>(proc_sp_get_pre_costing_New.gen_process_master_detail_list);

            var list_color_size = JsonConvert.DeserializeObject<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>>(proc_sp_get_pre_costing_New.color_detl_size_list);

            List<tran_plan_allocate_style_DTO> ListStyle = GenerateListStyle(temp_obj, list_color_size);

            model.List_plan_detl_style = ListStyle;

            ViewBag.product_composition_List = proc_sp_get_pre_costing_New.List_style_product_composition
           .Select(a =>
                       new SelectListItem
                       {
                           Text = a.product_composition,
                           Value = a.style_product_composition_id.ToString()
                       }
                   ).ToList();

            ViewBag.style_sleeve_info_List = proc_sp_get_pre_costing_New.List_style_sleeve_info
              .Select(a =>
                          new SelectListItem
                          {
                              Text = a.sleeve_info,
                              Value = a.style_sleeve_info_id.ToString()
                          }
                      ).ToList();

            return PartialView("~/Views/DesignMgt/TechPack/_TechPackNew.cshtml", model);

        }

        private static List<tran_plan_allocate_style_DTO> GenerateListStyle(tran_va_plan_Filter_DTO? temp_obj, List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO> list_color_size)
        {
            var ListStyle = list_color_size.Where(p => p.tran_va_plan_detl_style_id == temp_obj.tran_va_plan_detl_style_id).GroupBy(p =>
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

        private string GetDDLUnitHTML(List<style_product_unit_DTO> List_ProductUnit, Int64? UnitID)
        {
            string dropdownHTML = "<select class='ddlProductUnit' style='line-height: 0.5rem;font-size:11px;'>";

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
            string dropdownHTML = "<select class='ddlProductSizeDet' style='line-height: 0.5rem;font-size:11px;'>";

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
        public async Task<IActionResult> TechPackEdit(string input)
        {

            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            var proc_sp_get_pre_costing_New = await _rpc_db_service.GetAllproc_sp_get_tech_pack_data_editAsync
             (
                 temp_obj.tran_techpack_id,
                 temp_obj.tran_pre_costing_id,
                 temp_obj.tran_sample_order_id.Value, temp_obj.style_item_product_id.Value,
                 temp_obj.fiscal_year_id.Value,
                 Convert.ToInt64(Team_Category.Merchandiser)
             );

            //var objTemp = await _trantechpackService.GetTechpackByID(temp_obj.tran_techpack_id.Value);

            tran_sample_order_DTO model_SampleOrder = _mapper.Map<tran_sample_order_DTO>(proc_sp_get_pre_costing_New);

            tran_pre_costing_DTO model_PreCosting = _mapper.Map<tran_pre_costing_DTO>(proc_sp_get_pre_costing_New);

            var list_unitsdto = JsonConvert.DeserializeObject<List<gen_unit_DTO>>(proc_sp_get_pre_costing_New.gen_unit_list);

            var List_SizeGroupDetl = JsonConvert.DeserializeObject<List<style_product_size_group_details_DTO>>(proc_sp_get_pre_costing_New.style_product_sizegroupdetails_list);

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

            var List_AllSizeGroupDet = JsonConvert.DeserializeObject<List<style_product_size_group_details_entity>>(proc_sp_get_pre_costing_New.style_product_sizegroupdetails_list);

            var List_VAStyle_ColorInfo = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(proc_sp_get_pre_costing_New.style_color_size_details);

            tran_tech_pack_DTO model = _mapper.Map<tran_tech_pack_DTO>(proc_sp_get_pre_costing_New);

            model.style_item_product_id = temp_obj.style_item_product_id;

            model.style_item_product_name = temp_obj.style_product;

            model.TechPack_ColorList = JsonConvert.DeserializeObject<List<tran_tech_pack_color_DTO>>(proc_sp_get_pre_costing_New.color_wise_size_quantity);

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.tran_techpack_embellishmentinfo_list))
            {
                model.TechPack_EmbellishmentList = JsonConvert.DeserializeObject<List<tran_tech_pack_embellishment_info_DTO>>(proc_sp_get_pre_costing_New.tran_techpack_embellishmentinfo_list);

                var TechPack_EmbellishmentListIndex = 0;

                foreach (var singleembellish in model.TechPack_EmbellishmentList)
                {
                    singleembellish.EmbelshmentDet_List = JsonConvert.DeserializeObject<List<tran_tech_pack_embellishment_det_DTO>>(clsUtil.CleanJsonString(singleembellish.embellishment_details));

                    string unescapedJsonString = model.TechPack_EmbellishmentList[TechPack_EmbellishmentListIndex].supplier_info.Replace("\\\"", "\"").Trim('"');

                    singleembellish.ddlsupplier_info = JsonConvert.DeserializeObject<dropdown_entity>(unescapedJsonString);

                    TechPack_EmbellishmentListIndex++;
                }
            }

            model.List_base64String_Techpack_File = proc_sp_get_pre_costing_New.List_TechPackFiles;


            model.List_base64String_File = proc_sp_get_pre_costing_New.List_SampleOrderFiles; ;

            model.List_PreCostingDet_rpc = JsonConvert.DeserializeObject<List<rpc_sp_get_pre_costing_details_by_masterid_DTO>>(proc_sp_get_pre_costing_New.pre_costing_detail_list);

            model.List_detl_style_color = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(proc_sp_get_pre_costing_New.style_color_details);


            model.Color_List = _mapper.Map<List<tran_plan_allocate_style_color_DTO>>(List_VAStyle_ColorInfo);

            model.Color_Group_Details = _mapper.Map<List<style_product_size_group_details_DTO>>(List_AllSizeGroupDet);

            model.List_Unit = list_unitsdto.Select(p => new SelectListItem
            {
                Text = p.unit_name,
                Value = p.gen_unit_id.ToString()
            }).ToList();
            model.List_Detl = JsonConvert.DeserializeObject<List<tran_sample_order_detl_DTO>>(proc_sp_get_pre_costing_New.sample_order_detaillist);

            var List_Unit = JsonConvert.DeserializeObject<List<style_product_unit_DTO>>(proc_sp_get_pre_costing_New.style_product_unit_list);
            
                //var objembellishment_db = JsonConvert.DeserializeObject<List<rpc_sp_get_sampleorder_embellishment_det_DTO>>(proc_sp_get_pre_costing_New.sample_order_embellishmentlist);

                //ViewBag.embellishment = objembellishment_db.ToList();

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.sample_order_embellishmentlist))
            {
                var objembellishment_db = JsonConvert.DeserializeObject<List<rpc_sp_get_sampleorder_embellishment_det_DTO>>(proc_sp_get_pre_costing_New.sample_order_embellishmentlist);

                ViewBag.embellishment = objembellishment_db.ToList();
            }

            foreach (var obj in model.List_Detl)
            {
                obj.txtcolor_code = "<input type='color' value='" + obj.color_code + "' class='border-gray-200 rounded-sm txtvalcolor_code' onchange='LoadColorFromBox(this)' style='width:30%;float:left;' />" +
                     "<input   pattern='^#+([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$' value='" + obj.color_code + "' type='text' class='border-gray-200 rounded-sm txtColorCode' style='width:65%;float:right;' /> ";

                obj.txtorder_quantity = "<input type='number' style='width:80px;' min='0' value='" + obj.order_quantity.ToString() + "' onchange='calc_total();'  class='border-gray-200 rounded-sm txtorder_quantity' />";
                obj.strddlSizeHTML = GetDDLProductSizeHTML(List_SizeGroupDetl, obj.style_product_size_group_detid);
                obj.strddlUnitHTML = GetDDLUnitHTML(List_Unit, obj.style_product_unit_id);
                obj.btnAddDeleteRowHTML = "<button tran_sample_order_detl_id='" + obj.tran_sample_order_detl_id + "' type='button' style='width: 70px;background-color:darkred' onclick='btnAddDeleteRow(this)' class='btn btn-secondary btnAddDeleteRow '>Remove</button>";
            }

            model.List_Mapped_Structure = structure_group_List;

            model.tran_techpack_id = temp_obj.tran_techpack_id;
            model.style_product = temp_obj.style_product;
            model.style_product_details = temp_obj.style_product_detail;
            model.style_quantity = temp_obj.style_quantity;
            model.teckpack_style_code = temp_obj.style_code;
            model.tran_sample_order_number = model_SampleOrder.tran_sample_order_number;
            model.order_date = model_SampleOrder.order_date;
            model.date_added = DateTime.Now.Date;
            model.tran_pre_costing_id = temp_obj.tran_pre_costing_id;

            var designer_list = JsonConvert.DeserializeObject<List<owin_user_DTO>>(proc_sp_get_pre_costing_New.designer_list);

            model.designer = proc_sp_get_pre_costing_New.team_member_marketing_name;
            model.order_quantity = model_SampleOrder.order_quantity;
            model.tran_designer_review_id = temp_obj.tran_designer_review_id;
            model.delivery_unit_id = temp_obj.delivery_unit_id;
            model.delivery_date = temp_obj.delivery_date;

            var objgarment_part_List = JsonConvert.DeserializeObject<List<gen_garment_part_DTO>>(proc_sp_get_pre_costing_New.gen_garment_partlist);

            ViewBag.garment_part_List = objgarment_part_List
                .Select(a =>
                            new SelectListItem
                            {
                                Text = a.garment_part_name,
                                Value = a.gen_garment_part_id.ToString()
                            }
                        ).ToList();

            ViewBag.merchandiser_List = designer_list.Select(a =>
                            new SelectListItem
                            {
                                Text = a.name,
                                Value = a.userid.ToString()
                            }
                        ).ToList();
            ViewBag.Genprocessmasterdetail_List = JsonConvert.DeserializeObject<List<gen_process_master_detail_entity>>(proc_sp_get_pre_costing_New.gen_process_master_detail_list);

            ViewBag.product_composition_List = proc_sp_get_pre_costing_New.List_style_product_composition
                .Select(a =>
                            new SelectListItem
                            {
                                Text = a.product_composition,
                                Value = a.style_product_composition_id.ToString()
                            }
                        ).ToList();

            ViewBag.style_sleeve_info_List = proc_sp_get_pre_costing_New.List_style_sleeve_info
              .Select(a =>
                          new SelectListItem
                          {
                              Text = a.sleeve_info,
                              Value = a.style_sleeve_info_id.ToString()
                          }
                      ).ToList();

            return PartialView("~/Views/DesignMgt/TechPack/_TechPackEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TechPackApprove(string input)
        {

            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            var proc_sp_get_pre_costing_New = await _rpc_db_service.GetAllproc_sp_get_tech_pack_data_editAsync
             (
                 temp_obj.tran_techpack_id,
                 temp_obj.tran_pre_costing_id,
                 temp_obj.tran_sample_order_id.Value, temp_obj.style_item_product_id.Value,
                 temp_obj.fiscal_year_id.Value,
                 Convert.ToInt64(Team_Category.Merchandiser)
             );

            tran_sample_order_DTO model_SampleOrder = _mapper.Map<tran_sample_order_DTO>(proc_sp_get_pre_costing_New);

            tran_pre_costing_DTO model_PreCosting = _mapper.Map<tran_pre_costing_DTO>(proc_sp_get_pre_costing_New);
            var list_unitsdto = JsonConvert.DeserializeObject<List<gen_unit_DTO>>(proc_sp_get_pre_costing_New.gen_unit_list);

            var List_SizeGroupDetl = JsonConvert.DeserializeObject<List<style_product_size_group_details_DTO>>(proc_sp_get_pre_costing_New.style_product_sizegroupdetails_list);

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

            var List_AllSizeGroupDet = JsonConvert.DeserializeObject<List<style_product_size_group_details_entity>>(proc_sp_get_pre_costing_New.style_product_sizegroupdetails_list);
            var List_VAStyle_ColorInfo = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(proc_sp_get_pre_costing_New.style_color_size_details);

            tran_tech_pack_DTO model = _mapper.Map<tran_tech_pack_DTO>(proc_sp_get_pre_costing_New);

            model.TechPack_ColorList = JsonConvert.DeserializeObject<List<tran_tech_pack_color_DTO>>(proc_sp_get_pre_costing_New.color_wise_size_quantity);

            model.TechPack_EmbellishmentList = JsonConvert.DeserializeObject<List<tran_tech_pack_embellishment_info_DTO>>(proc_sp_get_pre_costing_New.tran_techpack_embellishmentinfo_list);

            var TechPack_EmbellishmentListIndex = 0;
            foreach (var singleembellish in model.TechPack_EmbellishmentList)
            {
                singleembellish.EmbelshmentDet_List = JsonConvert.DeserializeObject<List<tran_tech_pack_embellishment_det_DTO>>(clsUtil.CleanJsonString( singleembellish.embellishment_details));

                string unescapedJsonString = model.TechPack_EmbellishmentList[TechPack_EmbellishmentListIndex].supplier_info.Replace("\\\"", "\"").Trim('"');

                singleembellish.ddlsupplier_info = JsonConvert.DeserializeObject<dropdown_entity>(unescapedJsonString);

                TechPack_EmbellishmentListIndex++;
            }

            model.List_base64String_Techpack_File = proc_sp_get_pre_costing_New.List_TechPackFiles;


            model.List_base64String_File = proc_sp_get_pre_costing_New.List_SampleOrderFiles; ;

            model.List_PreCostingDet_rpc = JsonConvert.DeserializeObject<List<rpc_sp_get_pre_costing_details_by_masterid_DTO>>(proc_sp_get_pre_costing_New.pre_costing_detail_list);

            model.List_detl_style_color = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(proc_sp_get_pre_costing_New.style_color_details);


            model.Color_List = _mapper.Map<List<tran_plan_allocate_style_color_DTO>>(List_VAStyle_ColorInfo);

            model.Color_Group_Details = _mapper.Map<List<style_product_size_group_details_DTO>>(List_AllSizeGroupDet);

            model.List_Unit = list_unitsdto.Select(p => new SelectListItem
            {
                Text = p.unit_name,
                Value = p.gen_unit_id.ToString()
            }).ToList();
            model.List_Detl = JsonConvert.DeserializeObject<List<tran_sample_order_detl_DTO>>(proc_sp_get_pre_costing_New.sample_order_detaillist);

            var List_Unit = JsonConvert.DeserializeObject<List<style_product_unit_DTO>>(proc_sp_get_pre_costing_New.style_product_unit_list);

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.sample_order_embellishmentlist))
            {
                var objembellishment_db = JsonConvert.DeserializeObject<List<rpc_sp_get_sampleorder_embellishment_det_DTO>>(proc_sp_get_pre_costing_New.sample_order_embellishmentlist);

                ViewBag.embellishment = objembellishment_db.ToList();
            }

            foreach (var obj in model.List_Detl)
            {
                obj.txtcolor_code = "<input type='color' value='" + obj.color_code + "' class='border-gray-200 rounded-sm txtvalcolor_code' onchange='LoadColorFromBox(this)' style='width:30%;float:left;' />" +
                     "<input   pattern='^#+([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$' value='" + obj.color_code + "' type='text' class='border-gray-200 rounded-sm txtColorCode' style='width:65%;float:right;' /> ";

                obj.txtorder_quantity = "<input type='number' style='width:80px;' min='0' value='" + obj.order_quantity.ToString() + "' onchange='calc_total();'  class='border-gray-200 rounded-sm txtorder_quantity' />";
                obj.strddlSizeHTML = GetDDLProductSizeHTML(List_SizeGroupDetl, obj.style_product_size_group_detid);
                obj.strddlUnitHTML = GetDDLUnitHTML(List_Unit, obj.style_product_unit_id);
                obj.btnAddDeleteRowHTML = "<button tran_sample_order_detl_id='" + obj.tran_sample_order_detl_id + "' type='button' style='width: 70px;background-color:darkred' onclick='btnAddDeleteRow(this)' class='btn btn-secondary btnAddDeleteRow '>Remove</button>";
            }

            model.List_Mapped_Structure = structure_group_List;

            model.tran_techpack_id = temp_obj.tran_techpack_id;
            model.style_product = temp_obj.style_product;
            model.style_product_details = temp_obj.style_product_detail;
            model.style_quantity = temp_obj.style_quantity;
            model.teckpack_style_code = temp_obj.style_code;
            model.tran_sample_order_number = model_SampleOrder.tran_sample_order_number;
            model.order_date = model_SampleOrder.order_date;
            model.date_added = DateTime.Now.Date;
            model.tran_pre_costing_id = temp_obj.tran_pre_costing_id;
            var designer_list = JsonConvert.DeserializeObject<List<owin_user_DTO>>(proc_sp_get_pre_costing_New.designer_list);

            model.designer = proc_sp_get_pre_costing_New.name;
            model.order_quantity = model_SampleOrder.order_quantity;
            model.tran_designer_review_id = temp_obj.tran_designer_review_id;
            model.delivery_unit_id = temp_obj.delivery_unit_id;
            model.delivery_date = temp_obj.delivery_date;

            var objgarment_part_List = JsonConvert.DeserializeObject<List<gen_garment_part_DTO>>(proc_sp_get_pre_costing_New.gen_garment_partlist);

            ViewBag.garment_part_List = objgarment_part_List
                .Select(a =>
                            new SelectListItem
                            {
                                Text = a.garment_part_name,
                                Value = a.gen_garment_part_id.ToString()
                            }
                        ).ToList();

            ViewBag.merchandiser_List = designer_list.Select(a =>
                            new SelectListItem
                            {
                                Text = a.name,
                                Value = a.userid.ToString()
                            }
                        ).ToList();
            ViewBag.Genprocessmasterdetail_List = JsonConvert.DeserializeObject<List<gen_process_master_detail_entity>>(proc_sp_get_pre_costing_New.gen_process_master_detail_list);

            ViewBag.product_composition_List = proc_sp_get_pre_costing_New.List_style_product_composition
                .Select(a =>
                            new SelectListItem
                            {
                                Text = a.product_composition,
                                Value = a.style_product_composition_id.ToString()
                            }
                        ).ToList();

            ViewBag.style_sleeve_info_List = proc_sp_get_pre_costing_New.List_style_sleeve_info
              .Select(a =>
                          new SelectListItem
                          {
                              Text = a.sleeve_info,
                              Value = a.style_sleeve_info_id.ToString()
                          }
                      ).ToList();

            return PartialView("~/Views/DesignMgt/TechPack/_TechPackApprove.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetTechPackApprovalData(Filter_RangePlan_DataTable request)
        {

            var designer_list = await _OwinUserService.GetMemberListByTeamID(Convert.ToInt64(Team_Category.Designer));

            List<rpc_sp_get_data_for_designer_review_DTO> records = new List<rpc_sp_get_data_for_designer_review_DTO>();
            
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var saved_records = await _rpc_db_service.GetAllsp_get_data_for_designer_Approval_reviewAsync(
                request);

            var index = 1;
            var data = (from t in saved_records
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
                            t.delivery_unit_id,
                            //t.bp_yearly_gross_sales,
                            //t.style_product_size_group_id,
                            row_index = index++,
                            //t.event_gross_sales,
                            //t.range_plan_detail_id,
                            //t.range_plan_id,
                            //t.tran_bp_event_id,
                            //t.tran_bp_year_id,
                            t.style_item_product_id,
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
                            t.merchant_name,
                            // totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
                            //   totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),
                            t.master_category_name,
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name,
                            t.style_item_product_name,

                            strtxt_style_code_gen = "<input style='width: 80px;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",

                            btnAddEditPreCosting = t.tran_designer_review_id == null ?
                            "<input style='width:110px;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' type='button' value='Add Design Review' onclick='AddTechPack(this)'>"
                            : "<input style='width:110px;background-color:darkcyan;' class='btn btn-primary' style_quantity='" + t.style_quantity.ToString() + "' tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='Edit Design Review' onclick='EditTechPack(this)'>",
                            t.style_product_type_id

                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? saved_records[0].total_rows.Value : 0, data);

            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<ActionResult> SaveTechPack([FromBody] tran_tech_pack_DTO model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {

            model.added_by = sec_object.userid;
            model.date_added = DateTime.Now;

            model.fiscal_year_id = Fiscal_Year_ID;
            model.event_id = Event_ID;

            if (model.is_submitted == 1)
            {
                model.submitted_by = model.added_by;
            }

            var ret = await _trantechpackService.SaveAsync(model);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Saving Failed"
            });

        }


        [HttpPost]
        public async Task<ActionResult> UpdateTechPack([FromBody] tran_tech_pack_DTO model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
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

            model.added_by = sec_object.userid;
            model.date_added = DateTime.Now;
            model.updated_by = sec_object.userid;
            model.date_updated = DateTime.Now;

            model.fiscal_year_id = Fiscal_Year_ID;
            model.event_id = Event_ID;

            if (model.is_submitted == 1)
            {
                model.submitted_by = model.added_by;
            }

            var ret = await _trantechpackService.UpdateAsync_Extended(model);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Update Failed"
            });

        }

        [HttpPost]
        public async Task<ActionResult> ApproveRejectTechPack([FromBody] tran_tech_pack_DTO model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {
            #region commentSectionForFilteringController
            //SecurityCapsule sec_object = new SecurityCapsule();

            //var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            //List<Claim> listClaims = (List<Claim>)claim.Claims;

            #endregion

            //if (listClaims.Exists(c => c.Type == "secobject"))
            {
                //var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                // sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                model.added_by = sec_object.userid;
            }

            model.approved_by = model.added_by;
            model.approve_date = DateTime.Now;

            var ret = await _trantechpackService.tran_tech_pack_approve_reject(model);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Updating Failed"
            });

        }

    }
}
