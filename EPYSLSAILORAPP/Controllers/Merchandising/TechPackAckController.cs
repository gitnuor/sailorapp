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
using EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt;
using EPYSLSAILORAPP.Infrastructure.Services.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using Microsoft.EntityFrameworkCore.Infrastructure;
using EPYSLSAILORAPP.Application.Interface.Security;

namespace EPYSLSAILORAPP.Controllers.DesignManagement
{
    [RequestFormLimits(MultipartBodyLengthLimit = 2000000000)]
    [RequestSizeLimit(2000000000)]

    public class TechPackAckController : ExtendedBaseController
    {
        private readonly ILogger<TechPackAckController> _logger;
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


        public TechPackAckController(IBusinessPlanService BusinessPlanService, ITranDesignerReviewService TranDesignerReviewService,
            IGenFiscalYearService gen_fiscal_year_Service, IGenOutletService gen_outlet_entity_service, ITran_BP_YearService tran_bp_yearservice,
            ITran_BP_EventMonthService tran_bp_eventmonthservice, ITran_BP_EventMonthOutletService tran_bp_eventmonthoutletservice,
            IMapper mapper, ILogger<TechPackAckController> logger, IHttpContextAccessor contextAccessor,
            IStylecategoryService StylecategoryService,
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
            ITransampleorderService transampleorderService, IGenItemStructureGroupSubService IGenItemStructureGroupSubService, IGenUnitService GenUnitService) : base(contextAccessor, configuration)
        {

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
        public async Task<IActionResult> TechPackAckLanding()
        {
            Int64 fiscal_year_id = 0, eventid = 0, range_plan_id = 0, tran_va_plan_event_id = 0;
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            tran_va_plan_DTO model = new tran_va_plan_DTO();

            #region commentSectionForFilteringController
            //SecurityCapsule sec_object = new SecurityCapsule();

            //List<Claim> listClaims = (List<Claim>)claim.Claims;

            //if (listClaims.Count > 0)
            //{
            //    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

            //    sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);
            //}

            //if (!listClaims.Exists(c => c.Type == "secobject"))
            //{
            //    Response.Redirect("/account/LogOff");
            //}
            //else if (sec_object.gen_team_group_id != Convert.ToInt64(Team_Category.Merchandiser) &&
            //    sec_object.roleid != Convert.ToInt64(Enum_Role.Admin))
            //{
            //    ViewBag.IsNotDesigner = "This page is only for the Merchandiser";
            //}
            #endregion


            return View("~/Views/MerchandiserMgt/TechPackAck/TechPackAckLanding.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetTechPackAckData(Filter_RangePlan_DataTable request)
        {
            request.userid = sec_object.userid;

            var records = await _rpc_db_service.GetAllsp_get_data_for_techpack_ackAsync(
                request);

            var index = 1;
            var data = (from t in records
                        select new
                        {
                            t.is_ack,
                            t.ack_date,

                            strack_date = (t.ack_date.HasValue ? t.ack_date.Value.ToString("dd-MMM-yyyy") : ""),
                            t.merchant_name,
                            t.techpack_number,
                            t.designer_name,
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
                            // t.bp_yearly_gross_sales,
                            t.style_product_size_group_id,
                            row_index = index++,

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
                            btnViewTechpack = "<input style='width:150px;background-color:darkcyan;' class='btn btn-primary' is_ack='" + t.is_ack.ToString() + "' style_quantity='" + t.style_quantity.ToString() + "'  tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_techpack_id='" + t.tran_techpack_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='View Tech Pack' onclick='ViewTechPack(this)'>",

                            btnAddEditTechpack = "<input style='width:150px;background-color:darkcyan;' class='btn btn-primary' is_ack='" + t.is_ack.ToString() + "' style_quantity='" + t.style_quantity.ToString() + "'  tran_sample_order_id='" + t.tran_sample_order_id.ToString() + "' tran_techpack_id='" + t.tran_techpack_id.ToString() + "' tran_designer_review_id='" + t.tran_designer_review_id.ToString() + "' type='button' value='Tech Pack Ack' onclick='AckTechPackAck(this)'>",
                            t.style_product_type_id

                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);

            return Json(ret_obj);

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
        public async Task<IActionResult> TechPackAck(string input)
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

            // var unit_list = await _GenUnitService.GetAllAsync();


            var list_unitsdto = JsonConvert.DeserializeObject<List<gen_unit_DTO>>(proc_sp_get_pre_costing_New.gen_unit_list);

            // var list_unitsdto = _mapper.Map<List<gen_unit_DTO>>(unit_list);
            //var objList_SizeDetl = await _StyleproductsizegroupdetailsService.GetAsync(temp_obj.style_product_size_group_id);

            var List_SizeGroupDetl = JsonConvert.DeserializeObject<List<style_product_size_group_details_DTO>>(proc_sp_get_pre_costing_New.style_product_sizegroupdetails_list);

            // var mapping_structure_List = await _rpc_db_service.GetAllsp_get_mapped_item_structureAsync(temp_obj.style_master_category_id.Value);

            //var objstructure_List_DB = await _rpc_db_service.GetAllsp_get_sampleorder_subgroup_detAsync(
            //  Convert.ToInt64(temp_obj.tran_sample_order_id.Value));

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

            //var List_ColorInfo = await _tranvaplandetlstylecolorService.GetAsync(temp_obj.tran_va_plan_detl_style_id.Value);
            var List_VAStyle_ColorInfo = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(proc_sp_get_pre_costing_New.style_color_size_details);

            // tran_pre_costing_DTO tran_pre_costing_DTO = await _TranpreconstingService.GetAsync(temp_obj.tran_pre_costing_id.Value);

            tran_tech_pack_DTO model = _mapper.Map<tran_tech_pack_DTO>(proc_sp_get_pre_costing_New);

            // model.costing_smv =!string.IsNullOrEmpty( model_PreCosting.smv)? Convert.ToDecimal(model_PreCosting.smv):0;

            //////////////////////
            ///
            model.TechPack_ColorList = JsonConvert.DeserializeObject<List<tran_tech_pack_color_DTO>>(proc_sp_get_pre_costing_New.color_wise_size_quantity);


            //var TechPackAck_EmbellishmentList = await _connectionSupabse.From<tran_tech_pack_embellishment_info_entity>()
            //    .Where(p => p.tran_tech_pack_id == Id).Select("*").Get();

            //var objTechPackAck_EmbellishmentList = JsonConvert.DeserializeObject<List<tran_tech_pack_embellishment_info_entity>>(proc_sp_get_pre_costing_New.tran_techpack_embellishmentinfo_list);

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.tran_techpack_embellishmentinfo_list))
            {
                model.TechPack_EmbellishmentList = JsonConvert.DeserializeObject<List<tran_tech_pack_embellishment_info_DTO>>(proc_sp_get_pre_costing_New.tran_techpack_embellishmentinfo_list);
                //  model.TechPackAck_EmbellishmentList = _mapper.Map<List<tran_tech_pack_embellishment_info_DTO>>(objTechPackAck_EmbellishmentList);
            }
            var TechPackAck_EmbellishmentListIndex = 0;


            if (model.TechPack_EmbellishmentList != null)
            {
                foreach (var singleembellish in model.TechPack_EmbellishmentList)
                {
                    singleembellish.EmbelshmentDet_List = JsonConvert.DeserializeObject<List<tran_tech_pack_embellishment_det_DTO>>(clsUtil.CleanJsonString(singleembellish.embellishment_details));

                    string unescapedJsonString = model.TechPack_EmbellishmentList[TechPackAck_EmbellishmentListIndex].supplier_info.Replace("\\\"", "\"").Trim('"');

                    singleembellish.ddlsupplier_info = JsonConvert.DeserializeObject<dropdown_entity>(unescapedJsonString);

                    //All_Embellishments += singleembellish.em + ",";

                    TechPackAck_EmbellishmentListIndex++;
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

            model.team_member_marketing_name = proc_sp_get_pre_costing_New.team_member_marketing_name;

            //  var detl = await _TransampleorderdetlService.GetAsync(temp_obj.tran_sample_order_id.Value);

            // model.List_Detl = _mapper.Map<List<tran_sample_order_detl_DTO>>(detl);
            model.List_Detl = JsonConvert.DeserializeObject<List<tran_sample_order_detl_DTO>>(proc_sp_get_pre_costing_New.sample_order_detaillist);

            var List_Unit = JsonConvert.DeserializeObject<List<style_product_unit_DTO>>(proc_sp_get_pre_costing_New.style_product_unit_list);

            //var objembellishment_db = await _rpc_db_service.GetAllsp_get_sampleorder_embellishment_detAsync(Convert.ToInt64(temp_obj.tran_sample_order_id.Value));
            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.sample_order_embellishmentlist))
            {
                var objembellishment_db = JsonConvert.DeserializeObject<List<rpc_sp_get_sampleorder_embellishment_det_DTO>>(proc_sp_get_pre_costing_New.sample_order_embellishmentlist);

                ViewBag.embellishment = objembellishment_db.ToList();

                var All_Embellishments = "";

                foreach (var item in objembellishment_db)
                {
                    All_Embellishments += item.style_embellishment_name + ",";
                }
                ViewBag.All_Embellishments = All_Embellishments;
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

            //var designer_list = await _OwinUserService.GetAllAsync();
            var designer_list = JsonConvert.DeserializeObject<List<owin_user_DTO>>(proc_sp_get_pre_costing_New.designer_list);

            model.designer = proc_sp_get_pre_costing_New.name;
            model.order_quantity = model_SampleOrder.order_quantity;
            model.tran_designer_review_id = temp_obj.tran_designer_review_id;
            model.delivery_unit_id = temp_obj.delivery_unit_id;
            model.delivery_date = temp_obj.delivery_date;

            //var objgarment_part_List = await _GenGarmentPartService.GetAllAsync();

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

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.sample_order_detaillist))
            {
                proc_sp_get_pre_costing_New.List_sample_order_detl = JsonConvert.DeserializeObject<List<tran_sample_order_detl_DTO>>(proc_sp_get_pre_costing_New.sample_order_detaillist);

                var allSampleSize = "";

                foreach (var singleSize in proc_sp_get_pre_costing_New.List_sample_order_detl)
                {
                    allSampleSize += singleSize.style_product_size + ",";
                }

                ViewBag.allSampleSize = allSampleSize;

            }

            model.rpc_proc_sp_get_tech_pack_data_edit_DTO = proc_sp_get_pre_costing_New;

            if (!string.IsNullOrEmpty(proc_sp_get_pre_costing_New.pre_costing_detail_list))
            {
                var pre_costing_detl = JsonConvert.DeserializeObject<List<tran_pre_costing_item_detail_DTO>>(proc_sp_get_pre_costing_New.pre_costing_detail_list);

                model.List_PreCostingDet = pre_costing_detl;

                foreach (var objdet in model.List_PreCostingDet)
                {
                    if (!string.IsNullOrEmpty(objdet.str_placement_info))
                    {
                        objdet.List_placement_info = JsonConvert.DeserializeObject<List<style_placement_information_DTO>>(objdet.str_placement_info);

                    }
                }

                if (pre_costing_detl.Where(p => p.gen_item_structure_group_sub_id == Convert.ToInt64(Enum_gen_item_structure_group_sub.Fabric)).FirstOrDefault() != null)
                {
                    var objFabric = pre_costing_detl.Where(p => p.gen_item_structure_group_sub_id == Convert.ToInt64(Enum_gen_item_structure_group_sub.Fabric)
                    && p.construction_id == Convert.ToInt64(Enum_gen_segment.Construction).ToString()).ToList();

                    var all_fabrics = "";

                    foreach (var item in objFabric)
                    {
                        all_fabrics += item.segment_det1_text + ",";
                    }

                    ViewBag.Fabrication = all_fabrics;

                    var objFabricComposition = pre_costing_detl.Where(p => p.gen_item_structure_group_sub_id == Convert.ToInt64(Enum_gen_item_structure_group_sub.Fabric)
                    && p.composition_id == Convert.ToInt64(Enum_gen_segment.Composition).ToString()).ToList(); ;

                    var all_fabrics_composition = "";

                    foreach (var item in objFabricComposition)
                    {
                        all_fabrics_composition += item.segment_det2_text + ",";
                    }

                    ViewBag.FabricComposition = all_fabrics_composition;
                }
            }

            return PartialView("~/Views/MerchandiserMgt/TechPackAck/_TechPackAckView.cshtml", model);

        }


        [HttpPost]
        public async Task<ActionResult> UpdateTechPackAck([FromBody] tran_tech_pack_DTO model)//(IFormFile file, [FromBody] Base64FileAttachment attachfile)
        {
           
            var ret = await _trantechpackService.UpdateAck(model.tran_techpack_id.Value, sec_object.userid.Value);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Techpack Acknowledged Successful" : "Techpack Acknowledge Failed"
            });

        }

    }
}
