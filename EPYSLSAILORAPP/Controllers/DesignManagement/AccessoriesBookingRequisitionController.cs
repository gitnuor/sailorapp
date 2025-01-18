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
    public class AccessoriesBookingRequisitionController : ExtendedBaseController
    {
        private readonly ILogger<AccessoriesBookingRequisitionController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITranPurchaseRequisitionService _AccessoriesBookingRequisitionService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly ApplicationUserManager<owin_user_DTO> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenUnitService _GenUnitService;
       
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private readonly IGenItemMasterService _GenItemMasterService;
        private readonly IGenItemStructureGroupService _GenItemStructureGroupService;
        private readonly IGenItemStructureGroupSubService _IGenItemStructureGroupSubService;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside AccessoriesBookingRequisitionController !");
            return View();
        }

        public AccessoriesBookingRequisitionController(ApplicationUserManager<owin_user_DTO> userManager, IGenSupplierInformationService GenSupplierInformationService,
           IMapper mapper, ILogger<AccessoriesBookingRequisitionController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration, IGenUnitService GenUnitService,
            IGenItemMasterService GenItemMasterService, IGenItemStructureGroupService GenItemStructureGroupService,
            IGenItemStructureGroupSubService IGenItemStructureGroupSubService,
            ITranPurchaseRequisitionService AccessoriesBookingRequisitionService, IGenSeasonEventConfigurationService genseasoneventconfigurationservice,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _GenItemMasterService = GenItemMasterService;
            _IGenItemStructureGroupSubService = IGenItemStructureGroupSubService;
            _GenItemStructureGroupService = GenItemStructureGroupService;
            _userManager = userManager;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _AccessoriesBookingRequisitionService = AccessoriesBookingRequisitionService;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            
            _GenUnitService = GenUnitService;
            _contextAccessor = contextAccessor;
            _GenSupplierInformationService = GenSupplierInformationService;
        }

        [HttpGet]
        public async Task<IActionResult> AccessoriesBookingRequisitionLanding()
        {

            return View("~/Views/DesignMgt/AccessoriesBookingRequisition/AccessoriesBookingRequisitionLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> AccessoriesBookingRequisitionNew()
        {

            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();
            model.event_id = objFilter.event_id;
            model.item_structure_group_id = 1;
            model.designer_id = sec_object.userid;

            model.designer_name = sec_object.userfullname;
            model.pr_date = DateTime.Now;
            List<gen_item_structure_group_sub_DTO> SubCategorySewingList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(2);
            List<gen_item_structure_group_DTO> GroupList = await _GenItemStructureGroupService.GetAllAccessoriesGroups();
            List<gen_item_structure_group_sub_DTO> SubCategoryFinishinList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(3);
            var unit_list = await _GenUnitService.GetAllAsync();
            ViewBag.SubCategorySewingList = SubCategorySewingList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();
            ViewBag.SubCategoryFinishinList = SubCategoryFinishinList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();
            ViewBag.GroupList = GroupList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.structure_group_name.ToString(),
                        Value = a.item_structure_group_id.ToString(),
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
            return PartialView("~/Views/DesignMgt/AccessoriesBookingRequisition/_AccessoriesBookingRequisitionNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> AccessoriesBookingRequisitionEdit(string pr_id)
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

            model = await _AccessoriesBookingRequisitionService.GetSingleRequisitionWithoutTechpackAsync(Convert.ToInt64(decoded_pr_id));
            model.event_id = objFilter.event_id;
            model.designer_name = model.merchandiser_name;
          
			var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));

            List<gen_item_structure_group_sub_DTO> SubCategorySewingList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(2);
            List<gen_item_structure_group_DTO> GroupList = await _GenItemStructureGroupService.GetAllAccessoriesGroups();

            List<gen_item_structure_group_sub_DTO> SubCategoryFinishinList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(3);
            ViewBag.SubCategorySewingList = SubCategorySewingList.ToList()
          .Select(a =>
                   new SelectListItem
                   {
                       Text = a.sub_group_name.ToString(),
                       Value = a.gen_item_structure_group_sub_id.ToString(),
                   }
                   ).ToList();

            ViewBag.SubCategoryFinishinList = SubCategoryFinishinList.ToList()
          .Select(a =>
                   new SelectListItem
                   {
                       Text = a.sub_group_name.ToString(),
                       Value = a.gen_item_structure_group_sub_id.ToString(),
                   }
                   ).ToList();
            ViewBag.GroupList = GroupList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.structure_group_name.ToString(),
                        Value = a.item_structure_group_id.ToString(),
                    }
                    ).ToList();

            model.ddlsupplier_info = new dropdown_entity()
            {
                id = model.supplier_id.ToString(),
                text = sup.name.ToString()
            };
            return PartialView("~/Views/DesignMgt/AccessoriesBookingRequisition/_AccessoriesBookingRequisitionEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> AccessoriesBookingRequisitionView(string pr_id)
        {

            string decoded_pr_id = clsUtil.DecodeString(pr_id);

            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();

            model = await _AccessoriesBookingRequisitionService.GetSingleRequisitionWithoutTechpackAsync(Convert.ToInt64(decoded_pr_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            var du = await _GenUnitService.GetAsync(Convert.ToInt64(model.delivery_unit_id));
            model.supplier_name = sup.name.ToString();
            model.delivery_unit_name = du.FirstOrDefault().unit_name;
			model.designer_name = model.merchandiser_name;
			return PartialView("~/Views/DesignMgt/AccessoriesBookingRequisition/_AccessoriesBookingRequisitionView.cshtml", model);

        }



        [HttpGet]
        public async Task<IActionResult> AccessoriesBookingRequisitionSubmittedView(string pr_id)
        {

            string decoded_pr_id = clsUtil.DecodeString(pr_id);
            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();

            model = await _AccessoriesBookingRequisitionService.GetSingleRequisitionWithoutTechpackAsync(Convert.ToInt64(decoded_pr_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            var du = await _GenUnitService.GetAsync(Convert.ToInt64(model.delivery_unit_id));
            model.supplier_name = sup.name.ToString();
            model.delivery_unit_name = du.FirstOrDefault().unit_name;
			model.designer_name = model.merchandiser_name;
			return PartialView("~/Views/DesignMgt/AccessoriesBookingRequisition/AccessoriesBookingRequisitionSubmittedView.cshtml", model);

        }



        [HttpPost]
        public async Task<IActionResult> GetAccessoriesBookingRequisitionData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _AccessoriesBookingRequisitionService.GetAllTranPurchaseRequisitionAccessoriesAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, 0,request.Search.Value);

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
                            "<button type='button' onclick='EditAccessoriesBookingRequisition(this)'  class='btn btn-primary btnEdit'  pr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='ViewAccessoriesBookingRequisition(this)'  class='btn btn-warning btnView'  pr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='DeleteAccessoriesBookingRequisition(\"" + clsUtil.EncodeString(t.pr_id.ToString()) + "\")' class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                            "</div>"
                        }).ToList();
            //var ret_obj = new AjaxResponse(records.Count, data);
            var ret_obj = (records != null && records.Count > 0) ? new AjaxResponse(records[0].total_rows, data) : new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetAccessoriesRequisitionSubmittedData(DtParameters request)
        {
            try
            {

                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;

                var records = await _AccessoriesBookingRequisitionService.GetAllTranPurchaseRequisitionAccessoriesAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, 1,request.Search.Value);

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

                                "<button type='button' onclick='ViewAccessoriesBookingRequisitionSubmitted(this)'  class='btn btn-success btnView'  pr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-check' aria-hidden='true'></i></button>" +

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
        public async Task<IActionResult> SaveAccessoriesBookingRequisition([FromBody] tran_purchase_requisition_DTO request)
        {


            var ret = true;
            if (request.pr_id == null)
            {
                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;
            }



            try
            {
                //var model = JsonConvert.DeserializeObject<tran_purchase_requisition_entity>(JsonConvert.SerializeObject(request));
                List<tran_purchase_requisition_dtl_entity> detail = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_entity>>(JsonConvert.SerializeObject(request.details));
                request.gen_item_structure_group_id = request.item_structure_group_id;

                //request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;

                ret = await _AccessoriesBookingRequisitionService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateAccessoriesBookingRequisition([FromBody] tran_purchase_requisition_DTO request)
        {
            var ret = true;


            request.updated_by = sec_object.userid.Value;
            request.date_updated = DateTime.Now;

            request.event_id = Event_ID;

            try
            {
                //var model = JsonConvert.DeserializeObject<tran_purchase_requisition_entity>(JsonConvert.SerializeObject(request));
                List<tran_purchase_requisition_dtl_entity> detail = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_entity>>(JsonConvert.SerializeObject(request.details));

                ret = await _AccessoriesBookingRequisitionService.UpdateOpenPRAsync(request, request.DeleteList);

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
        public async Task<IActionResult> DeleteAccessoriesBookingRequisition([FromBody] tran_purchase_requisition_DTO request)
        {

            var ret = true;

            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;



            try
            {

                string decoded_pr_id = clsUtil.DecodeString(request.strMasterID);

                request.pr_id = Convert.ToInt64(decoded_pr_id);

                ret = await _AccessoriesBookingRequisitionService.DeleteAsync(request.pr_id.Value);

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
        public async Task<IActionResult> AddNewAccessories(string item_structure_group_id, string gen_item_structure_group_sub_id, string measurement_unit_id)
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


            return PartialView("~/Views/DesignMgt/AccessoriesBookingRequisition/_AddAccessoriesDetails.cshtml", model);

        }

        [HttpGet]
        public async Task<JsonResult> AddAccessoriesDetailsMaster(string input)
        {

            var AccessoriesDetetail = JsonConvert.DeserializeObject<List<gen_item_master_DTO>>(input);
            long InsertedID = 0;

            InsertedID = await _GenItemMasterService.CheckAndSaveItemMasterAsync(AccessoriesDetetail.FirstOrDefault(), sec_object.userid);


            return Json(InsertedID);


        }

    }
}

