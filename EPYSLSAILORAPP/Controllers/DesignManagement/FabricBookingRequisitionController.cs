using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.CustomIdentityManagers;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity.SupplyChain;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Infrastructure.Services.Tran_DesignMgt;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class FabricBookingRequisitionController : ExtendedBaseController
    {
        private readonly ILogger<FabricBookingRequisitionController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITranPurchaseRequisitionService _FabricBookingRequisitionService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly ApplicationUserManager<owin_user_DTO> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenUnitService _GenUnitService;
        private HelperService objHelperService;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private readonly IGenItemMasterService _GenItemMasterService;
        private readonly IGenItemStructureGroupService _GenItemStructureGroupService;
        private readonly IGenItemStructureGroupSubService _IGenItemStructureGroupSubService;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside FabricBookingRequisitionController !");
            return View();
        }

        public FabricBookingRequisitionController(
              IGenItemStructureGroupSubService IGenItemStructureGroupSubService,
  IGenItemStructureGroupService GenItemStructureGroupService, ApplicationUserManager<owin_user_DTO> userManager, IGenSupplierInformationService GenSupplierInformationService,
           IGenItemMasterService GenItemMasterService, IMapper mapper, ILogger<FabricBookingRequisitionController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration, IGenUnitService GenUnitService,

            ITranPurchaseRequisitionService FabricBookingRequisitionService, IGenSeasonEventConfigurationService genseasoneventconfigurationservice,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _GenItemStructureGroupService = GenItemStructureGroupService;
            _IGenItemStructureGroupSubService = IGenItemStructureGroupSubService;
            _userManager = userManager;
            _GenItemMasterService = GenItemMasterService;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _FabricBookingRequisitionService = FabricBookingRequisitionService;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);

            _GenUnitService = GenUnitService;
            _contextAccessor = contextAccessor;
            _GenSupplierInformationService = GenSupplierInformationService;
        }

        [HttpGet]
        public async Task<IActionResult> FabricBookingRequisitionLanding()
        {

            return View("~/Views/DesignMgt/FabricBookingRequisition/FabricBookingRequisitionLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> FabricBookingRequisitionNew()
        {


            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();
            model.event_id = objFilter.event_id;
            model.item_structure_group_id = 1;
            model.designer_id = sec_object.userid;

            model.designer_name = sec_object.userfullname;
            model.pr_date = DateTime.Now;
            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(model.item_structure_group_id.Value);
            var unit_list = await _GenUnitService.GetAllAsync();
            ViewBag.SubCategoryList = SubCategoryList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();
            ViewBag.DeliveryUnits = unit_list.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.unit_name.ToString(),
                        Value = a.gen_unit_id.ToString(),
                    }
                    ).ToList();
            return PartialView("~/Views/DesignMgt/FabricBookingRequisition/_FabricBookingRequisitionNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> FabricBookingRequisitionEdit(string pr_id)
        {

            string decoded_pr_id = clsUtil.DecodeString(pr_id);
            var unit_list = await _GenUnitService.GetAllAsync();
            ViewBag.DeliveryUnits = unit_list.ToList()
               .Select(a =>
                        new SelectListItem
                        {
                            Text = a.unit_name.ToString(),
                            Value = a.gen_unit_id.ToString(),
                        }
                        ).ToList();
            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();



            model = await _FabricBookingRequisitionService.GetSingleRequisitionWithoutTechpackAsync(Convert.ToInt64(decoded_pr_id));
            model.designer_name = model.merchandiser_name;
            model.event_id = objFilter.event_id;
            model.item_structure_group_id = 1;
            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(model.item_structure_group_id.Value);

            ViewBag.SubCategoryList = SubCategoryList.ToList()
           .Select(a =>
                   new SelectListItem
                   {
                       Text = a.sub_group_name.ToString(),
                       Value = a.gen_item_structure_group_sub_id.ToString(),
                   }
                   ).ToList();
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));


            model.ddlsupplier_info = new dropdown_entity()
            {
                id = model.supplier_id.ToString(),
                text = sup.name.ToString()
            };

            return PartialView("~/Views/DesignMgt/FabricBookingRequisition/_FabricBookingRequisitionEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> FabricBookingRequisitionView(string pr_id)
        {

            string decoded_pr_id = clsUtil.DecodeString(pr_id);

            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();

            model = await _FabricBookingRequisitionService.GetSingleRequisitionWithoutTechpackAsync(Convert.ToInt64(decoded_pr_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            var du = await _GenUnitService.GetAsync(Convert.ToInt64(model.delivery_unit_id));
            model.supplier_name = sup.name.ToString();
            model.delivery_unit_name = du.FirstOrDefault().unit_name;
            model.designer_name = model.merchandiser_name;
            return PartialView("~/Views/DesignMgt/FabricBookingRequisition/_FabricBookingRequisitionView.cshtml", model);

        }


        [HttpGet]
        public async Task<IActionResult> FabricBookingRequisitionSubmittedView(string pr_id)
        {

            string decoded_pr_id = clsUtil.DecodeString(pr_id);

            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();

            model = await _FabricBookingRequisitionService.GetSingleRequisitionWithoutTechpackAsync(Convert.ToInt64(decoded_pr_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            var du = await _GenUnitService.GetAsync(Convert.ToInt64(model.delivery_unit_id));
            model.supplier_name = sup.name.ToString();
            model.delivery_unit_name = du.FirstOrDefault().unit_name;
            model.designer_name = model.merchandiser_name;
            return PartialView("~/Views/DesignMgt/FabricBookingRequisition/FabricBookingRequisitionSubmittedView.cshtml", model);

        }




        [HttpPost]
        public async Task<IActionResult> GetFabricBookingRequisitionData(DtParameters request)
        {
            try
            {
                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;

                var records = await _FabricBookingRequisitionService.GetAllTranPurchaseRequisitionAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, 0, request.Search.Value);

                var index = request.Start + 1;
                var data = (from t in records
                            select new
                            {
                                row_index = index++,
                                t.pr_id,
                                t.pr_no,
                                t.pr_date,
                                t.delivery_date,
                                t.event_title,
                                t.designer_name,
                                t.unit_name,
                                t.supplier_name,


                                datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                                "<button type='button' onclick='EditFabricBookingRequisition(this)' class='btn btn-primary btnEdit'  pr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +
                                "<button type='button' onclick='ViewFabricBookingRequisition(this)' class='btn btn-warning btnView'  pr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                                "<button type='button' onclick='DeleteFabricBookingRequisition(\"" + clsUtil.EncodeString(t.pr_id.ToString()) + "\")'  class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                                "</div>"
                            }).ToList();

                

                var ret_obj = (records != null && records.Count > 0) ? new AjaxResponse(records[0].total_rows, data): new AjaxResponse(records.Count, data);
                //  var ret_obj = new AjaxResponse(records[0].total_rows, data);
                return Json(ret_obj);
            }
            catch (Exception ex)
            {

                throw;
            }


        }


        [HttpPost]
        public async Task<IActionResult> GetFabricBookingRequisitionSubmittedData(DtParameters request)
        {
            try
            {
                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;

                var records = await _FabricBookingRequisitionService.GetAllTranPurchaseRequisitionAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, 1, request.Search.Value);

                var index = request.Start + 1;
                var data = (from t in records
                            select new
                            {
                                row_index = index++,
                                t.pr_id,
                                t.pr_no,
                                t.pr_date,
                                t.delivery_date,
                                t.event_title,
                                t.designer_name,
                                t.unit_name,
                                t.supplier_name,


                                datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                                "<button type='button' onclick='ViewFabricBookingRequisitionSubmitted(this)'  class='btn btn-success btnView'  pr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-check' aria-hidden='true'></i></button>" +

                                "</div>"
                            }).ToList();
                //var ret_obj = new AjaxResponse(records.Count, data);
                var ret_obj = (records != null && records.Count > 0) ? new AjaxResponse(records[0].total_rows, data) : new AjaxResponse(records.Count, data);
                return Json(ret_obj);

            }
            catch (Exception)
            {

                throw;
            }

        }



        [HttpPost]
        public async Task<IActionResult> SaveFabricBookingRequisition([FromBody] tran_purchase_requisition_DTO request)
        {

            var ret = true;

            //request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            if (request.pr_id == null)
            {
                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;
            }

            try
            {
                //var model = JsonConvert.DeserializeObject<tran_purchase_requisition_entity>(JsonConvert.SerializeObject(request));
                List<tran_purchase_requisition_dtl_entity> detail = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_entity>>(JsonConvert.SerializeObject(request.details));

                request.gen_item_structure_group_id = 1;

                //ret = await _FabricBookingRequisitionService.SaveAsync(request);

                for (int i = 0; i < 150000; i++)
                {
                    await _FabricBookingRequisitionService.SaveAsync(request);
                }


                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Success" : "Data Saving Failed")
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
                    Status_Result = (ret == true ? "Operation Failed" : ex.Message)
                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateFabricBookingRequisition([FromBody] tran_purchase_requisition_DTO request)
        {
            var ret = true;


            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

            //request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;


            try
            {
                //var model = JsonConvert.DeserializeObject<tran_purchase_requisition_entity>(JsonConvert.SerializeObject(request));
                List<tran_purchase_requisition_dtl_entity> detail = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_entity>>(JsonConvert.SerializeObject(request.details));
                request.gen_item_structure_group_id = 1;
                ret = await _FabricBookingRequisitionService.UpdateOpenPRAsync(request, request.DeleteList);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Success" : "Data Saving Failed")
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
                    Status_Result = (ret == true ? "Operation Failed" : ex.Message)
                });
            }


        }



        [HttpPost]
        public async Task<IActionResult> DeleteFabricBookingRequisition([FromBody] tran_purchase_requisition_DTO request)
        {


            var ret = true;



            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;



            try
            {

                string decoded_pr_id = clsUtil.DecodeString(request.strMasterID);

                request.pr_id = Convert.ToInt64(decoded_pr_id);

                ret = await _FabricBookingRequisitionService.DeleteAsync(request.pr_id.Value);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Success" : "Data Deletion Failed")
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
                    Status_Result = (ret == true ? "Operation Failed" : ex.Message)
                });
            }

        }


        [HttpGet]
        public async Task<IActionResult> AddNewFabric(string item_structure_group_id, string gen_item_structure_group_sub_id, string measurement_unit_id)
        {


            var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(measurement_unit_id));

            ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            {
                Text = p.unit_detail_display,
                Value = p.gen_measurement_unit_detail_id.ToString(),
                Selected = p.gen_measurement_unit_detail_id.ToString() == "86",
            }).ToList();





            fabric_mapping_segment_details_for_fabricbooking_dto model = new fabric_mapping_segment_details_for_fabricbooking_dto();
            model.gen_item_structure_group_id = Convert.ToInt32(item_structure_group_id);
            model.gen_item_structure_group_sub_id = Convert.ToInt32(gen_item_structure_group_sub_id);
            model.mapping_item = await _rpc_db_service.GetAllsp_get_mapped_segment_by_gen_item_structure_group_sub_idAsync(Convert.ToInt32(gen_item_structure_group_sub_id));


            return PartialView("~/Views/DesignMgt/FabricBookingRequisition/_AddFabricDetails.cshtml", model);

        }

        [HttpGet]
        public async Task<JsonResult> AddFabricDetailsMaster(string input)
        {


            var FabricDetetail = JsonConvert.DeserializeObject<List<gen_item_master_DTO>>(input);
            long InsertedID = 0;

            InsertedID = await _GenItemMasterService.CheckAndSaveItemMasterAsync(FabricDetetail.FirstOrDefault(), sec_object.userid);


            return Json(InsertedID);


        }

    }
}

