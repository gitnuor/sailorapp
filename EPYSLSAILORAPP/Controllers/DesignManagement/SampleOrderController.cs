using EPYSLSAILORAPP.Application.DTO;
using AutoMapper;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.Security;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack;
using Web.Core.Frame.Helpers;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;

namespace EPYSLSAILORAPP.Controllers.DesignManagement
{
    [RequestFormLimits(MultipartBodyLengthLimit = 2000000000)]
    [RequestSizeLimit(2000000000)]

    public class SampleOrderController : ExtendedBaseController
    {
        private readonly ILogger<SampleOrderController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBusinessPlanService _BP_service;
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
        private readonly IStyleproductsizegroupdetailsService _StyleproductsizegroupdetailsService;  
        private readonly ITranPlanAllocateService _TranPlanAllocateService;
        private readonly ITransampleorderService _TransampleorderService;
        private readonly ITransampleorderembellishmentService _TransampleorderembellishmentService;        
        private readonly ITransampleorderdetlService _TransampleorderdetlService;
        private readonly IGenOutletService _gen_outlet_entity_service;
        private readonly IGenUnitService _GenUnitService;
        private readonly IGenItemStructureGroupSubService _IGenItemStructureGroupSubService;
        private readonly IGenPriorityService _gen_priority_service;
        private readonly ITran_BP_YearService _tran_bp_yearservice;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
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

        public SampleOrderController( Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,    IGenFiscalYearService gen_fiscal_year_Service, IMapper mapper,

         ILogger<SampleOrderController> logger, IHttpContextAccessor contextAccessor,        
            ITransampleorderdetlService TransampleorderdetlService,     
            ITransampleorderembellishmentService TransampleorderembellishmentService, 
           IRPCDbService rpc_db_service,  IConfiguration configuration,
            ITransampleorderService transampleorderService,  IGenUnitService GenUnitService) : base(contextAccessor, configuration)
        {       
            _gen_fiscal_year_Service = gen_fiscal_year_Service;
            _mapper = mapper;
            _logger = logger;                
            _hostingEnvironment = hostingEnvironment;       
            _rpc_db_service = rpc_db_service; 
            _TransampleorderdetlService = TransampleorderdetlService;
              _logger.LogInformation("Hello from inside SampleOrder !");        
            _configuration = configuration;
            _redisRangePlanService = new RedisService<rpc_range_plan_getfor_createupdate_DTO>(_configuration);
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);           
            _TransampleorderService = transampleorderService;           
            _TransampleorderembellishmentService = TransampleorderembellishmentService;          
            _GenUnitService = GenUnitService;
        }


        [HttpPost]
        public async Task<IActionResult> GetSampleOrderSummaryData(tran_va_plan_DTO request)
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
                            strBtns = "<button type='button' style='width: 130px;'  onclick=\"location.href='/SampleOrder/SampleOrderCreateLanding?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "&va_plan_id=" + clsUtil.EncodeString(t.tran_va_plan_id != null ? t.tran_va_plan_id.ToString() : "0") + "'\"   class='btn btn-secondary BtnSize'>Event Wise Data</button>"

                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count(), data);

            return Json(ret_obj);

        }

        [HttpGet]
        public async Task<IActionResult> SampleOrderLanding(string fiscal_year_id, string range_plan_id, string va_plan_id)
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
                        return RedirectToAction("SampleOrderCreate", "SampleOrder", new
                        {
                            fiscal_year_id = clsUtil.EncodeString(obj.fiscal_year_id.ToString()),
                            eventid = clsUtil.EncodeString(obj.event_id.ToString()),
                            range_plan_id = clsUtil.EncodeString(retFilter.FirstOrDefault().range_plan_id.ToString()),
                           
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
        [HttpPost]
        public async Task<IActionResult> GetSampleOrderEventData(Filter_RangePlan_DataTable request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

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
                            strBtns = (t.is_finalised != true ? "<button type='button' style='width: 110px;'  onclick=\"location.href='/SampleOrder/SampleOrderCreate?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&eventid=" + clsUtil.EncodeString(t.event_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "&tran_va_plan_event_id=" + clsUtil.EncodeString(t.tran_va_plan_event_id != null ? t.tran_va_plan_event_id.ToString() : "0") + "'\"  class='btn btn-secondary btnRangePlanAddModify'>Sample Order</button>" : ""),
                           
                            strBtnPreCosting = (t.is_finalised != true ? "<button type='button' style='width: 110px;'  onclick=\"location.href='/PreCosting/PreCostingCreate?fiscal_year_id=" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "&eventid=" + clsUtil.EncodeString(t.event_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "&range_plan_id=" + clsUtil.EncodeString(t.range_plan_id.ToString()) + "&tran_va_plan_event_id=" + clsUtil.EncodeString(t.tran_va_plan_event_id != null ? t.tran_va_plan_event_id.ToString() : "0") + "'\"  class='btn btn-secondary btnPreCosting'>Pre-Costing</button>" : "")

                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count(), data);

            return Json(ret_obj);

        }
       
        [HttpGet]
        public async Task<IActionResult> SampleOrderCreate()
        {
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

            ViewBag.encoded_fiscal_year = objFilter.str_fiscal_year_id;

            ViewBag.tran_va_plan_event_id = objFilter.tran_va_plan_event_id.ToString();

            model.tran_range_plan_id = objFilter.range_plan_id;

            var objFlter = await _rpc_db_service.GetAllproc_get_filter_itemsAsync(objFilter.fiscal_year_id);

            var fiscal_year_list = JsonConvert.DeserializeObject<List<gen_fiscal_year_dto>>(objFlter.genfiscalyear_list);

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


            var product_type_list = JsonConvert.DeserializeObject<List<style_product_type_DTO>>(objFlter.styleproducttype_list); ;

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

            return View("~/Views/DesignMgt/SampleOrder/SampleOrderCreate.cshtml", model);
        }

        private string GetHtmlSampleOrderTableHTML(rpc_tran_sample_order_getfor_create_DTO obj)
        {
            string strhtml = "<div class='row'>";

            try
            { 

                var sample_order = JsonConvert.DeserializeObject<List<rpc_sp_get_data_for_sampleorder_DTO>>(obj.sample_order_json);

                if (sample_order != null)
                {
                    foreach (var sample in sample_order)
                    {
                        if (!string.IsNullOrEmpty(sample.sample_photos))
                        {
                            var temparray = JArray.Parse(sample.sample_photos);
                            sample.sample_photos_List = JArray.Parse(sample.sample_photos).ToObject<List<file_upload>>();

                            strhtml += "<div class='col-lg-3'><div class='form-group'><img class='grid_img' src=\"../Images/loading-icon-file.gif\"  tran_sample_order_id='" + sample.tran_sample_order_id + "'  onclick='ShowImage(this);'  style='width: 70%;;height:40px;' /><a class='anch_link' onclick='EditSampleOrder(this," + sample.tran_sample_order_id.ToString() + ")' style='font-size:12px;'>" + sample.tran_sample_order_number + "</a></div></div>";

                        }
                    }

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
        public async Task<IActionResult> GetTranSampleOrderData(Filter_RangePlan_DataTable request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            List<rpc_tran_sample_order_getfor_create_DTO> records = new List<rpc_tran_sample_order_getfor_create_DTO>();

            if (sec_object.roleid == Convert.ToInt64(Enum_Role.Admin))
            {
                request.userid = null;        

            }
            else
            {
                request.userid = sec_object.userid.Value;
            }
            records = await _rpc_db_service.GetAlltran_sample_order_getfor_createAsync(
                          request);


            var index = request.Start+1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.style_item_type_name,
                            t.style_product_type_name,
                            t.style_item_origin_name,
                            t.style_gender_name,
                            t.master_category_name,
                            t.style_item_product_name,
                            t.style_code,
                            style_code_qty = t.style_code + " (" + t.style_quantity.ToString() + ")",
                            t.style_quantity,
                            t.tran_va_plan_detl_style_id,
                            t.style_master_category_id,
                            t.tran_va_plan_detl_id,
                            t.style_item_product_id,
                            str_sameple_order_dt_html = GetHtmlSampleOrderTableHTML(t),                         
                            totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
                            totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),
                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name + "<br/>" + t.style_item_product_name,

                            strtxt_style_code_gen = "<input style='width: 80px;text-align:center;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",
                            btnSampleOrderBOMAddUpdate = "<button type='button' style='width:170px;text-align:center;' onclick='BtnSampleOrderBOMAddUpdate(this)'  tran_va_plan_detl_style_id='" + t.tran_va_plan_detl_style_id.ToString() + "' style_master_category_id='" + t.style_master_category_id.ToString() + "' tran_va_plan_detl_id='" + t.tran_va_plan_detl_id.ToString() + "'  style_item_product_id='" + t.style_item_product_id.ToString() + "'  class='btn btn-secondary BtnSampleOrderAndBOM'>Add Sample Order & BOM</button>",
                          
                            t.style_product_type_id

                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value:0, data);

            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetSampleOrderTradingData(Filter_RangePlan_DataTable request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            List<rpc_tran_sample_order_getfor_create_DTO> records = new List<rpc_tran_sample_order_getfor_create_DTO>();

            if (sec_object.roleid == Convert.ToInt64(Enum_Role.Admin))
            {
                request.userid = null;          

            }
            else
            {
                request.userid = sec_object.userid.Value;
            

            }

            records = await _rpc_db_service.GetAlltran_sample_order_getfor_create_with_trading(
                             request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.style_item_type_name,
                            t.style_product_type_name,
                            t.style_item_origin_name,
                            t.style_gender_name,
                            t.master_category_name,
                            t.style_item_product_name,
                            t.style_code,
                            style_code_qty = t.style_code + " (" + t.style_quantity.ToString() + ")",
                            t.style_quantity,
                            t.trading_type,
                            t.tran_va_plan_detl_style_id,
                            t.style_master_category_id,
                            t.tran_va_plan_detl_id,
                            t.style_item_product_id,

                            str_sameple_order_dt_html = GetHtmlSampleOrderTableHTML(t),

                            totaladded = records.Where(p => p.tran_va_plan_detl_id != null).Count(),
                            totalnotadded = records.Where(p => p.tran_va_plan_detl_id == null).Count(),

                            product_details = t.style_item_type_name + "-" + t.style_product_type_name + "-" + t.style_item_origin_name +
                            "-" + t.style_gender_name + "-" + t.master_category_name + "<br/>" + t.style_item_product_name,

                            strtxt_style_code_gen = "<input style='width: 80px;text-align:center;' min='0' type='text' class='border-gray-200 rounded-sm txt_style_code_gen' value='" + (!string.IsNullOrEmpty(t.style_code_gen) ? t.style_code_gen : clsUtil.GenStyleCode(t.style_item_product_name)) + "' />",
                            btnSampleOrderBOMAddUpdate = "<button type='button' style='width:170px;text-align:center;' onclick='BtnSampleOrderBOMAddUpdate(this)'  tran_va_plan_detl_style_id='" + t.tran_va_plan_detl_style_id.ToString() + "' style_master_category_id='" + t.style_master_category_id.ToString() + "' tran_va_plan_detl_id='" + t.tran_va_plan_detl_id.ToString() + "'  style_item_product_id='" + t.style_item_product_id.ToString() + "'  class='btn btn-secondary BtnSampleOrderAndBOM'>Add Sample Order & BOM</button>",

                            t.style_product_type_id

                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);

            return Json(ret_obj);


        }

        private string GetNumberOfStyleHTML(long? no_of_style, bool? isEnable)
        {

            string str = " <select " + (isEnable == false ? "disabled" : "") + " style='width:100%;' class='form-control ddlNoOfStyle'>" +
                                   " <option value='0'>Select</option>";

            for (int i = 1; i <= 10; i++)
            {
                var selected = no_of_style.HasValue && no_of_style.Value == i ? "selected" : "";
                str += "<option value='" + i.ToString() + "' " + selected + ">" + i.ToString() + "</option>";
            }

            str += "</select>";

            return str;
        }
        //


        [HttpGet]
        public async Task<IActionResult> SampleOrderBOMAdd(string input)
        {


            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            tran_sample_order_DTO model = new tran_sample_order_DTO();
            model = _mapper.Map<tran_sample_order_DTO>(temp_obj);

            model.style_product = temp_obj.style_product;
            model.style_product_details = temp_obj.style_product_detail;
            model.tran_va_plan_detl_style_id = temp_obj.tran_va_plan_detl_style_id;
            model.style_code = temp_obj.style_code;
            model.order_quantity = temp_obj.style_quantity;


            var proc_sameple_order_data = await _rpc_db_service.GetAllproc_sp_get_sample_order_dataAsync
                (Fiscal_Year_ID,
                temp_obj.style_item_product_id.Value,
                Convert.ToInt64(Team_Category.Designer), temp_obj.tran_va_plan_detl_id.Value);

            var List_Color_detl_size = JsonConvert.DeserializeObject<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>>(proc_sameple_order_data.colordetlsize_list);

            model.List_Color_detl_size = List_Color_detl_size.Where(p => p.tran_va_plan_detl_style_id == temp_obj.tran_va_plan_detl_style_id).ToList();

            var List_Color = model.List_Color_detl_size
               .GroupBy(p => new { p.color_code, p.style_embellishment_ids })
               .Select(p => new rpc_sp_get_color_detl_size_by_vaplandetlid_DTO
               {
                   color_code = p.Key.color_code,
                   style_embellishment_ids = p.Key.style_embellishment_ids

               }).ToList();

            var mapping_structure_List = JsonConvert.DeserializeObject<List<mapped_rpc_item_structure>>(proc_sameple_order_data.mapping_structure_list);

            var structure_group_List = mapping_structure_List
                .GroupBy(p => new { p.item_structure_group_id, p.structure_group_name })
                .Select(p => new rpc_sp_get_mapped_item_structure_DTO
                {
                    item_structure_group_id = p.Key.item_structure_group_id,
                    structure_group_name = p.Key.structure_group_name
                }).ToList();

            foreach (var singlegroup in structure_group_List)
            {
                singlegroup.List = mapping_structure_List.
                    Where(p => p.item_structure_group_id == singlegroup.item_structure_group_id).ToList();
            }

            model.List_Mapped_Structure = structure_group_List;

            model.List_Structure_det = JsonConvert.DeserializeObject<List<gen_item_structure_group_sub_DTO>>(proc_sameple_order_data.gen_itemstructure_groupsub_list);

            var list_unitsdto = JsonConvert.DeserializeObject<List<gen_unit_DTO>>(proc_sameple_order_data.gen_unit_list);

            model.List_Unit = list_unitsdto.Select(p => new SelectListItem
            {
                Text = p.unit_name,
                Value = p.gen_unit_id.ToString()
            }).ToList();

            var designer_list = JsonConvert.DeserializeObject<List<owin_user_DTO>>(proc_sameple_order_data.designer_list);

            if (designer_list.Where(p => p.userid == sec_object.userid).FirstOrDefault() != null)
            {
                model.designer = designer_list.Where(p => p.userid == sec_object.userid).FirstOrDefault().name;
                model.designer_member_id = designer_list.Where(p => p.userid == sec_object.userid).FirstOrDefault().userid;
            }
            var List_Unit = JsonConvert.DeserializeObject<List<style_product_unit_DTO>>(proc_sameple_order_data.style_product_unit_list);

            var List_SizeGroupDetl = JsonConvert.DeserializeObject<List<style_product_size_group_details_DTO>>(proc_sameple_order_data.style_product_sizegroupdetails_list);

            model.List_Detl = new List<tran_sample_order_detl_DTO>();

            var color_index = 1;

            model.List_Detl.Add(
                new tran_sample_order_detl_DTO
                {
                    RowNumber = color_index++,
                    txtcolor_code = "",
                    txtorder_quantity = "<input type='number' style='width:80px;' min='0' value='0' onchange='calc_total();'  class='border-gray-200 rounded-sm txtorder_quantity' />",
                    strddlSizeHTML = GetDDLProductSizeHTML(List_SizeGroupDetl, null),
                    strddlUnitHTML = GetDDLUnitHTML(List_Unit, null),
                    btnAddDeleteRowHTML = "<button type='button' style='width: 70px;' onclick='btnAddDeleteRow(this)' class='btn btn-secondary btnAddDeleteRow '>Add</button>"
                });

            if (List_Color.Count > 0)
            {
                model.style_embellishment_ids = "'" + List_Color[0].style_embellishment_ids + "'";

                foreach (var item in List_Color)
                {


                    model.List_Detl.Add(
                      new tran_sample_order_detl_DTO
                      {
                          RowNumber = color_index++,
                          txtcolor_code = item.color_code,
                          txtorder_quantity = "<input type='number' style='width:80px;' min='0' value='0' onchange='calc_total();'  class='border-gray-200 rounded-sm txtorder_quantity' />",
                          strddlSizeHTML = GetDDLProductSizeHTML(List_SizeGroupDetl, null),
                          strddlUnitHTML = GetDDLUnitHTML(List_Unit, null),
                          btnAddDeleteRowHTML = "<button type='button' style='width: 70px;' onclick='btnAddDeleteRow(this)' class='btn btn-secondary btnAddDeleteRow '>Add</button>"
                      });
                }
            }

            model.delivery_date = DateTime.Now;
            model.order_date = DateTime.Now;

            ViewBag.style_pattern_list = proc_sameple_order_data.obj_style_pattern_list.ToList()
               .Select(a =>
                                                  new SelectListItem
                                                  {
                                                      Text = a.pattern_info.ToString(),
                                                      Value = a.style_pattern_id.ToString(),
                                                      Selected = model.obj_pattern != null && model.obj_pattern.style_pattern_id == a.style_pattern_id ? true : false
                                                  }
                                                  ).ToList();

            ViewBag.style_fit_list = proc_sameple_order_data.obj_style_fit_info_list.ToList()
             .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.fit_info.ToString(),
                                                    Value = a.style_fit_info_id.ToString(),
                                                    Selected = model.obj_fit_name != null && model.obj_fit_name.style_fit_info_id == a.style_fit_info_id ? true : false

                                                }
                                                ).ToList();

            model.fiscal_year_id = Fiscal_Year_ID;
            model.event_id = Event_ID;

            return PartialView("~/Views/DesignMgt/SampleOrder/_SampleOrderBOMNew.cshtml", model);

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

        [HttpPost]
        public async Task<ActionResult> SaveSamepleOrderAndBOM([FromBody] tran_sample_order_DTO model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {

            model.added_by = sec_object.userid;
            model.date_added = DateTime.Now;
            model.order_date=DateTime.Now;
            model.designer_member_id = sec_object.userid;

            model.fiscal_year_id = Fiscal_Year_ID;
            model.event_id = Event_ID;

            var ret = await _TransampleorderService.SaveAsync(model);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Saving Failed"
            });

        }



        [HttpGet]
        public async Task<IActionResult> SampleOrderBOMEdit(string input)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {
           
            var temp_obj = JsonConvert.DeserializeObject<tran_va_plan_Filter_DTO>(input);

            var proc_sameple_order_data = await _rpc_db_service.GetAllproc_sp_get_sample_order_data_editAsync
                (
                    temp_obj.tran_sample_order_id.Value,
                    temp_obj.fiscal_year_id.Value, temp_obj.style_item_product_id.Value,
                    Convert.ToInt64(Team_Category.Designer)
                );

            tran_sample_order_DTO model = _mapper.Map<tran_sample_order_DTO>(proc_sameple_order_data);

            model.style_product = temp_obj.style_product;
            model.style_product_details = temp_obj.style_product_detail;
            model.style_code = temp_obj.style_code;
            model.List_base64String_File = proc_sameple_order_data.List_SampleOrderFiles;
            model.obj_fit_name = proc_sameple_order_data.obj_style_fit_info;
            model.obj_pattern = proc_sameple_order_data.obj_style_pattern;

            var mapping_structure_List = JsonConvert.DeserializeObject<List<mapped_rpc_item_structure>>(proc_sameple_order_data.mapping_structure_list);

            var structure_List_DB = JsonConvert.DeserializeObject<List<rpc_sp_get_sampleorder_subgroup_det_DTO>>(proc_sameple_order_data.sampleorder_subgroup_list);

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

            model.List_Structure_det = JsonConvert.DeserializeObject<List<gen_item_structure_group_sub_DTO>>(proc_sameple_order_data.gen_itemstructure_groupsub_list);

            //var unit_list = await _GenUnitService.GetAllAsync();

            var list_unitsdto = JsonConvert.DeserializeObject<List<gen_unit_DTO>>(proc_sameple_order_data.gen_unit_list);

            model.List_Unit = list_unitsdto.Select(p => new SelectListItem
            {
                Text = p.unit_name,
                Value = p.gen_unit_id.ToString()
            }).ToList();

            var designer_list = JsonConvert.DeserializeObject<List<owin_user_DTO>>(proc_sameple_order_data.designer_list);


            if (designer_list.Where(p => p.userid == sec_object.userid).FirstOrDefault() != null)
            {
                model.designer = designer_list.Where(p => p.userid == sec_object.userid).FirstOrDefault().name;
                model.designer_member_id = designer_list.Where(p => p.userid == sec_object.userid).FirstOrDefault().userid;
            }

             if (!string.IsNullOrEmpty(proc_sameple_order_data.sample_order_embellishmentlist))
            {
                var objembellishment_db = JsonConvert.DeserializeObject<List<rpc_proc_sp_get_sample_order_embellishment_DTO>>(proc_sameple_order_data.sample_order_embellishmentlist);

                ViewBag.embellishment = "'" + JsonConvert.SerializeObject(objembellishment_db.ToList().Select(p => new ddlEntity
                {
                    id = p.embellishment_id.ToString(),
                    text = p.process_name,
                    secondary_id = p.tran_sample_order_embellishment_id.ToString(),
                    current_state = 2
                })) + "'";
            }

            ViewBag.List_base64String_File = JsonConvert.SerializeObject(model.List_base64String_File);

          
            var List_Unit = JsonConvert.DeserializeObject<List<style_product_unit_DTO>>(proc_sameple_order_data.style_product_unit_list);

           
            var List_SizeGroupDetl = JsonConvert.DeserializeObject<List<style_product_size_group_details_DTO>>(proc_sameple_order_data.style_product_sizegroupdetails_list);

            ////need to optimize
            //var detl = await _TransampleorderdetlService.GetAsync(model.tran_sample_order_id.Value);

            model.List_Detl = JsonConvert.DeserializeObject<List<tran_sample_order_detl_DTO>>(proc_sameple_order_data.sample_order_detaillist);

            foreach (var obj in model.List_Detl)
            {
                obj.txtcolor_code = obj.color_code;

                obj.txtorder_quantity = "<input type='number' style='width:80px;' min='0' value='" + obj.order_quantity.ToString() + "' onchange='calc_total();'  class='border-gray-200 rounded-sm txtorder_quantity' />";
                obj.strddlSizeHTML = GetDDLProductSizeHTML(List_SizeGroupDetl, obj.style_product_size_group_detid);
                obj.strddlUnitHTML = GetDDLUnitHTML(List_Unit, obj.style_product_unit_id);
                obj.btnAddDeleteRowHTML = "<button tran_sample_order_detl_id='" + obj.tran_sample_order_detl_id + "' type='button' style='width: 70px;background-color:#df7373' onclick='btnAddDeleteRow(this)' class='btn btn-secondary btnAddDeleteRow '>Remove</button>";

            }

            model.List_Detl.Insert(0,
            new tran_sample_order_detl_DTO
            {
                RowNumber = 0,
                txtcolor_code = "",
                txtorder_quantity = "<input type='number' style='width:80px;' min='0' value='0' onchange='calc_total();'  class='border-gray-200 rounded-sm txtorder_quantity' />",
                strddlSizeHTML = GetDDLProductSizeHTML(List_SizeGroupDetl, null),
                strddlUnitHTML = GetDDLUnitHTML(List_Unit, null),
                btnAddDeleteRowHTML = "<button type='button' style='width: 70px;' onclick='btnAddDeleteRow(this)' class='btn btn-secondary btnAddDeleteRow '>Add</button>"
            });

            ViewBag.style_pattern_list = proc_sameple_order_data.obj_style_pattern_list.ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.pattern_info.ToString(),
                                                       Value = a.style_pattern_id.ToString(),
                                                       Selected = model.obj_pattern != null && model.obj_pattern.style_pattern_id == a.style_pattern_id ? true : false
                                                   }
                                                   ).ToList();

            ViewBag.style_fit_list = proc_sameple_order_data.obj_style_fit_info_list.ToList()
             .Select(a =>
                                                new SelectListItem
                                                {
                                                    Text = a.fit_info.ToString(),
                                                    Value = a.style_fit_info_id.ToString(),
                                                    Selected = model.obj_fit_name != null && model.obj_fit_name.style_fit_info_id == a.style_fit_info_id ? true : false

                                                }
                                                ).ToList();

            model.fiscal_year_id = Fiscal_Year_ID;
            model.event_id = Event_ID;

            return PartialView("~/Views/DesignMgt/SampleOrder/_SampleOrderBOMEdit.cshtml", model);

        }


        [HttpPost]
        public async Task<ActionResult> UpdateSamepleOrderAndBOM([FromBody] tran_sample_order_DTO model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {
       

            model.added_by = sec_object.userid;
            model.date_added = DateTime.Now;

            model.fiscal_year_id = Fiscal_Year_ID;
            model.event_id = Event_ID;

            var ret = await _TransampleorderService.UpdateAsync(model);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Saving Failed"
            });

        }


    }
}
