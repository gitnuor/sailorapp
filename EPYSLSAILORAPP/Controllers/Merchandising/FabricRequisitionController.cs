using AutoMapper;
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
using Org.BouncyCastle.Asn1.Ocsp;
using Serilog.Context;
using ServiceStack;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class FabricRequisitionController : ExtendedBaseController
    {
        private readonly ILogger<FabricRequisitionController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITranPurchaseRequisitionService _TranPurchaseRequisitionService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly ApplicationUserManager<owin_user_DTO> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenUnitService _GenUnitService;
        private readonly ITrantechpackService _trantechpackService;
        private HelperService objHelperService;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private readonly IGenItemMasterService _GenItemMasterService;
        private readonly IGenItemStructureGroupService _GenItemStructureGroupService;
        private readonly IGenItemStructureGroupSubService _IGenItemStructureGroupSubService;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside FabricRequisitionController !");
            return View();
        }

        public FabricRequisitionController(IGenItemStructureGroupSubService IGenItemStructureGroupSubService,
  IGenItemStructureGroupService GenItemStructureGroupService, ApplicationUserManager<owin_user_DTO> userManager, IGenSupplierInformationService GenSupplierInformationService,
           IMapper mapper, ILogger<FabricRequisitionController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration, IGenUnitService GenUnitService,
            IGenItemMasterService GenItemMasterService,
            ITranPurchaseRequisitionService TranPurchaseRequisitionService, IGenSeasonEventConfigurationService genseasoneventconfigurationservice, ITrantechpackService trantechpackService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _trantechpackService = trantechpackService;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranPurchaseRequisitionService = TranPurchaseRequisitionService;
            _GenItemStructureGroupService = GenItemStructureGroupService;
            _IGenItemStructureGroupSubService = IGenItemStructureGroupSubService;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            
            _GenUnitService = GenUnitService;
            _contextAccessor = contextAccessor;
            _GenSupplierInformationService = GenSupplierInformationService;
            _GenItemMasterService = GenItemMasterService;
        }

        [HttpGet]
        public async Task<IActionResult> FabricRequisitionLanding()
        {

            return View("~/Views/MerchandiserMgt/FabricRequisition/FabricRequisitionLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> FabricRequisitionNew()
        {

            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();
            model.event_id = objFilter.event_id;
            model.item_structure_group_id = 1;
            model.gen_item_structure_group_id = 1;

            //model.merchandiser_id = sec_object.userid;

            //model.merchandiser_name = sec_object.userfullname;
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
            return PartialView("~/Views/MerchandiserMgt/FabricRequisition/_FabricRequisitionNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> FabricRequisitionEdit(string pr_id)
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
            model = await _TranPurchaseRequisitionService.GetSingleRequisitionAsync(Convert.ToInt64(decoded_pr_id));
            model.merchandiser_name = sec_object.userfullname;
            model.event_id = objFilter.event_id;
            model.item_structure_group_id = 1;
            //model.List_Files = JsonConvert.DeserializeObject<List<file_upload>>(model.document_list);
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
            var techpack = await _trantechpackService.GetAsync(Convert.ToInt64(model.techpack_id));


            model.ddlsupplier_info = new dropdown_entity()
            {
                id = model.supplier_id.ToString(),
                text = sup.name.ToString()
            };
            model.ddlTechpack_info = new dropdown_TechpackEntity()
            {
                tran_techpack_id = model.techpack_id.ToString(),
                techpack_number = techpack.techpack_number.ToString()
            };

            return PartialView("~/Views/MerchandiserMgt/FabricRequisition/_FabricRequisitionEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> FabricRequisitionView(string pr_id)
        {

            string decoded_pr_id = clsUtil.DecodeString(pr_id);

            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();

            model = await _TranPurchaseRequisitionService.GetSingleRequisitionAsync(Convert.ToInt64(decoded_pr_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            var du = await _GenUnitService.GetAsync(Convert.ToInt64(model.delivery_unit_id));
            model.supplier_name = sup.name.ToString();
            model.delivery_unit_name = du.FirstOrDefault().unit_name;
            var techpack = await _trantechpackService.GetAsync(Convert.ToInt64(model.techpack_id));
            model.techpack_name = techpack.techpack_number;
            return PartialView("~/Views/MerchandiserMgt/FabricRequisition/_FabricRequisitionView.cshtml", model);

        }


        public async Task<IActionResult> FabricRequisitionSubmittedView(string pr_id)
        {

            string decoded_pr_id = clsUtil.DecodeString(pr_id);



            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();

            model = await _TranPurchaseRequisitionService.GetSingleRequisitionAsync(Convert.ToInt64(decoded_pr_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            var du = await _GenUnitService.GetAsync(Convert.ToInt64(model.delivery_unit_id));
            var techpack = await _trantechpackService.GetAsync(Convert.ToInt64(model.techpack_id));
            model.supplier_name = sup.name.ToString();
            model.delivery_unit_name = du.FirstOrDefault().unit_name;
            model.designer_name = sec_object.username;
            model.techpack_name = techpack.techpack_number;
            return PartialView("~/Views/MerchandiserMgt/FabricRequisition/_FabricRequisitionSubmittedView.cshtml", model);

        }





        [HttpPost]
        public async Task<IActionResult> GetFabricRequisitionData(DtParameters request)
        {



            var records = await _TranPurchaseRequisitionService.GetAllTranPurchaseRequisitionMerchandiseFabricAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, 0, request.Search.Value);

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
                            "<button type='button' onclick='EditFabricRequisition(this)'  class='btn btn-primary btnEdit'  pr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='ViewFabricRequisition(this)'  class='btn btn-warning btnView'  pr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='DeleteFabricRequisition(\"" + clsUtil.EncodeString(t.pr_id.ToString()) + "\")' class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }



        public async Task<IActionResult> GetFabricRequisitionSubmittedData(DtParameters request)
        {
            try
            {


                var records = await _TranPurchaseRequisitionService.GetAllTranPurchaseRequisitionMerchandiseFabricAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, 1, request.Search.Value);

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

                                "<button type='button' onclick='ViewFabricBookingRequisitionSubmitted(this)'  class='btn btn-success btnView'  pr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                                "</div>"
                            }).ToList();
                var ret_obj = new AjaxResponse(records.Count, data);
                return Json(ret_obj);

            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpPost]
        public async Task<IActionResult> SaveFabricRequisition([FromBody] tran_purchase_requisition_DTO request)
        {
            var ret = true;

            if (request.pr_id == null)
            {
                request.gen_item_structure_group_id = 1;
                request.added_by = sec_object.userid.Value;
                request.date_added = DateTime.Now;
            }

            try
            {
                // var model = JsonConvert.DeserializeObject<tran_purchase_requisition_entity>(JsonConvert.SerializeObject(request));
                List<tran_purchase_requisition_dtl_entity> detail = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_entity>>(JsonConvert.SerializeObject(request.details));


                ret = await _TranPurchaseRequisitionService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateFabricRequisition([FromBody] tran_purchase_requisition_DTO request)
        {
            var ret = true;


            request.updated_by = sec_object.userid.Value;
            request.date_updated = DateTime.Now;

            try
            {

                
                request.gen_item_structure_group_id = 1;
                ret = await _TranPurchaseRequisitionService.UpdateAsync(request, request.DeleteList);

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
        public async Task<IActionResult> DeleteFabricRequisition([FromBody] tran_purchase_requisition_DTO request)
        {

            var ret = true;
            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;



            try
            {

                string decoded_pr_id = clsUtil.DecodeString(request.strMasterID);

                request.pr_id = Convert.ToInt64(decoded_pr_id);

                ret = await _TranPurchaseRequisitionService.DeleteAsync(request.pr_id.Value);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : "Data Deletion Failed")
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


            return PartialView("~/Views/MerchandiserMgt/FabricRequisition/_AddFabricDetails.cshtml", model);

        }

        [HttpGet]
        public async Task<JsonResult> AddFabricDetailsMaster(string input)
        {

            var FabricDetetail = JsonConvert.DeserializeObject<List<gen_item_master_DTO>>(input);
            long InsertedID = 0;

            InsertedID = await _GenItemMasterService.CheckAndSaveItemMasterAsync(FabricDetetail.FirstOrDefault(), sec_object.userid);


            return Json(InsertedID);


        }


        public async Task<JsonResult> GetMerchandiserInfo(long techpack)
        {
            

            var model= await _TranPurchaseRequisitionService.GetMerchandiserByTechpack(techpack);
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetItemDetailsByTechpackforFabric(long p_techpack_id,long item_structure_group_id,long gen_item_structure_group_sub_id)
        {
            var model = await _TranPurchaseRequisitionService.GetItemDetailsByTechpackforFabric(p_techpack_id, gen_item_structure_group_sub_id);

            return PartialView("~/Views/MerchandiserMgt/FabricRequisition/_AddFabricDetails.cshtml", model);
        }

    }
}



