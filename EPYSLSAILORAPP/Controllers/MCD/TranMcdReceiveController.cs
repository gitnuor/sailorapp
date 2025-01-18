using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.Merchandiser;
using EPYSLSAILORAPP.Application.Interface.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Enum;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Models.MCD;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Context;
using ServiceStack;
using System.Security.Claims;
//using System.Web.WebPages.Html;
using Web.Core.Frame.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace EPYSLSAILORAPP.Controllers
{
    public class TranMcdReceiveController : ExtendedBaseController//BaseController
    {
        private readonly ILogger<TranMcdReceiveController> _logger;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;

        private readonly ITranScmPoService _transcmpo;

        private readonly IConfiguration _configuration;
        private readonly ITranMcdReceiveService _TranMcdReceiveService;


        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private readonly IGenUnitService _GenUnitService;
        private readonly IGenWarehouseService _GenWarehouseService;
        private readonly IGenWarehouseFloorService _GenWarehouseFloorService;
        private readonly IGenWarehouseFloorRackService _GenWarehouseFloorRackService;
        private readonly IGenItemStructureGroupService _GenItemStructureGroupService;
        private readonly IGenItemStructureGroupSubService _IGenItemStructureGroupSubService;
        private readonly IGenItemMasterService _GenItemMasterService;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside TranMcdReceiveController !");
            return View();
        }

        public TranMcdReceiveController(IGenWarehouseService GenWarehouseService,
            IGenWarehouseFloorRackService GenWarehouseFloorRackService,
             IGenItemStructureGroupSubService GenItemStructureGroupSubService,
            IGenItemStructureGroupService GenItemStructureGroupService,
            IGenWarehouseFloorService GenWarehouseFloorService, IGenItemMasterService GenItemMasterService,
           IMapper mapper, ILogger<TranMcdReceiveController> logger, IHttpContextAccessor contextAccessor,
            ITranMcdReceiveService TranMcdReceiveService, IGenSupplierInformationService GenSupplierInformationService, IGenUnitService GenUnitService, IConfiguration configuration,
            IRPCDbService rpc_db_service, ITranScmPoService transcmpo) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _GenWarehouseService = GenWarehouseService;
            _GenWarehouseFloorService = GenWarehouseFloorService;
            _GenWarehouseFloorRackService = GenWarehouseFloorRackService;
            _GenItemStructureGroupService = GenItemStructureGroupService;
            _IGenItemStructureGroupSubService = GenItemStructureGroupSubService;

            _transcmpo = transcmpo;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranMcdReceiveService = TranMcdReceiveService;

            _GenSupplierInformationService = GenSupplierInformationService;
            _GenUnitService = GenUnitService;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);

            _GenItemMasterService = GenItemMasterService;
            _contextAccessor = contextAccessor;
            _transcmpo = transcmpo;
        }

        [HttpGet]
        public async Task<IActionResult> TranMcdReceiveLanding()
        {

            return View("~/Views/MCD/TranMcdReceive/TranMcdReceiveLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranMcdReceiveNew(long poid)
        {

            //string poid = clsUtil.DecodeString(id);

            var data = await _transcmpo.Get_data_tran_scm_po_Async(Convert.ToInt64(poid));
            rpc_tran_scm_po_detail_DTO model = JsonConvert.DeserializeObject<rpc_tran_scm_po_detail_DTO>(JsonConvert.SerializeObject(data));
            model.List_po_details = JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(data.list_po_details);

            //tran_mcd_receive_DTO model = new tran_mcd_receive_DTO();
            // model.item_structure_group_id = 1;

            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(1);
            ViewBag.SubCategoryList = SubCategoryList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();

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

            List<gen_warehouse_floor_DTO> WarehousefloorName = await _GenWarehouseFloorService.GetAllAsync();
            ViewBag.WarehousefloorName = WarehousefloorName.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.floor_name.ToString(),
                        Value = a.gen_warehouse_floor_id.ToString(),
                    }
                    ).ToList();

            // List<gen_warehouse_floor_rack_DTO> WarehouseRackName = await _TranMcdReceiveService.GetWarehouseRackName();
            // ViewBag.WarehouseRackName = WarehouseRackName.ToList()
            //.Select(a =>
            //         new SelectListItem
            //         {
            //             Text = a.rack_name.ToString(),
            //             Value = a.gen_warehouse_floor_id.ToString(),
            //         }
            //         ).ToList();

            var list_item_details = JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(data.list_po_details);
            var itemIds= list_item_details.Select(a=>a.item_id).ToList();
            ViewBag.Item_id = itemIds;


            return PartialView("~/Views/MCD/TranMcdReceive/_TranMcdReceiveNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranMcdUndoItem(long poid)
        {

            //string poid = clsUtil.DecodeString(id);

            var data = await _transcmpo.Get_data_tran_scm_po_Async(Convert.ToInt64(poid));
            rpc_tran_scm_po_detail_DTO model = JsonConvert.DeserializeObject<rpc_tran_scm_po_detail_DTO>(JsonConvert.SerializeObject(data));
            model.List_po_details = JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(data.list_po_details);

            //tran_mcd_receive_DTO model = new tran_mcd_receive_DTO();
            // model.item_structure_group_id = 1;

            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(1);
            ViewBag.SubCategoryList = SubCategoryList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();

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

            List<gen_warehouse_floor_DTO> WarehousefloorName = await _GenWarehouseFloorService.GetAllAsync();
            ViewBag.WarehousefloorName = WarehousefloorName.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.floor_name.ToString(),
                        Value = a.gen_warehouse_floor_id.ToString(),
                    }
                    ).ToList();

            // List<gen_warehouse_floor_rack_DTO> WarehouseRackName = await _TranMcdReceiveService.GetWarehouseRackName();
            // ViewBag.WarehouseRackName = WarehouseRackName.ToList()
            //.Select(a =>
            //         new SelectListItem
            //         {
            //             Text = a.rack_name.ToString(),
            //             Value = a.gen_warehouse_floor_id.ToString(),
            //         }
            //         ).ToList();




            return PartialView("~/Views/MCD/TranMcdReceive/_ItemUndoDetailsList.cshtml", model);

        }

        [HttpGet]
        public async Task<JsonResult> GetPODetails(long po_id)
        {

            var po = await _transcmpo.GetPurchaseOrder(po_id);
            ViewBag.item_structure_group_id = po.item_structure_group_id;

            return Json(new { po = po, data = po.List_po_details });
        }

        [HttpGet]
        public async Task<IActionResult> TranScmPoDetailList1(int loadSize, int pageNumber, string searchTerm = "", long group = 0)
        {
            try
            {
                var filter = new DtParameters();

                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;

                var ListData = await _TranMcdReceiveService.GetDropDownData(filter, group);//GetDropDownData(filter, group);

                var data = (from t in ListData
                            select new
                            {
                                Id = t.po_id,
                                Text = t.po_no

                            }).ToList();

                var total = data.Count();
                data = data.Skip(loadSize * (pageNumber - 1)).Take(loadSize).ToList();
                return Json(new { data = data, status = "success", Total = total });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetRackName(long selectedValue)
        {
            List<gen_warehouse_floor_rack_DTO> WarehouseRackName = await _GenWarehouseFloorRackService.GetByFloorID(selectedValue);
            ViewBag.WarehouseRackName = WarehouseRackName.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.rack_name.ToString(),
                        Value = a.gen_warehouse_floor_id.ToString(),
                    }
                    ).ToList();
            return Json(new { data = WarehouseRackName, status = "success" });
        }

        [HttpGet]
        public async Task<IActionResult> TranMcdReceiveEdit(string received_id)
        {

            string decoded_received_id = clsUtil.DecodeString(received_id);

            tran_mcd_receive_DTO model = new tran_mcd_receive_DTO();

            model = await _TranMcdReceiveService.GetSingleAsync(Convert.ToInt64(decoded_received_id));

            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(1);
            ViewBag.SubCategoryList = SubCategoryList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();

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

            List<gen_warehouse_floor_DTO> WarehousefloorName = await _GenWarehouseFloorService.GetAllAsync();
            ViewBag.WarehousefloorName = WarehousefloorName.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.floor_name.ToString(),
                        Value = a.gen_warehouse_floor_id.ToString(),
                    }
                    ).ToList();

            List<tran_mcd_receive_transport_DTO> transportTypeList = await _TranMcdReceiveService.GetAllTransportType();

            ViewBag.transportTypeList = new SelectList(
               transportTypeList.Select(t => new SelectListItem
               {
                   Text = t.transport_type.ToString(),
                   Value = t.transport_id.ToString(),
               }),
               "Value", "Text", model.transport_type);


            List<gen_transaction_mode_DTO> transactionMode = await _TranMcdReceiveService.GetAllTransactionMode();

            ViewBag.transactionTypeList = new SelectList(
               transactionMode.Select(t => new SelectListItem
               {
                   Value = t.transaction_mode_id.ToString(),
                   Text = t.transaction_mode_type.ToString(),
               }),
               "Value", "Text", model.tran_mode);

            List<gen_delivery_status_DTO> deliveryStatus = await _TranMcdReceiveService.GetAllDeliveryStatus();

            ViewBag.deliveryStatus = new SelectList(
               deliveryStatus.Select(t => new SelectListItem
               {
                   Value = t.delivery_status_id.ToString(),
                   Text = t.delivery_status_type.ToString(),
               }),
               "Value", "Text", model.delevary_status); ;





            return PartialView("~/Views/MCD/TranMcdReceive/_TranMcdReceiveEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranMcdDraftReceiveView(string received_id, DtParameters request)
        {

            string decoded_received_id = clsUtil.DecodeString(received_id);
            var start = request.Start;
            var size = 10;
            rpc_tran_mcd_receive_DTO masterData = new rpc_tran_mcd_receive_DTO();
            request.fiscal_year_id = objFilter.fiscal_year_id;
            request.event_id = objFilter.event_id;
            List<rpc_tran_mcd_receive_detail_DTO> detailData = new List<rpc_tran_mcd_receive_detail_DTO>();

            masterData = await _rpc_db_service.GetAllJoined_TranMcdReceiveAsync(start, size, ActionType.getById.ToString(), Convert.ToInt64(decoded_received_id), 1, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, request.Search);
            detailData = await _rpc_db_service.GetAllJoined_TranMcdReceiveDetailAsync(Convert.ToInt64(decoded_received_id));

            ReceiveViewModel viewModel = new ReceiveViewModel
            {
                MasterData = masterData,
                DetailData = detailData
            };

            return PartialView("~/Views/MCD/TranMcdReceive/_TranMcdReceiveView.cshtml", viewModel);

        }

        [HttpGet]
        public async Task<IActionResult> TranMcdReceiveView(string received_id, DtParameters request)
        {

            string decoded_received_id = clsUtil.DecodeString(received_id);
            var start = request.Start;
            var size = 10;
            rpc_tran_mcd_receive_DTO masterData = new rpc_tran_mcd_receive_DTO();
            request.fiscal_year_id = objFilter.fiscal_year_id;
            request.event_id = objFilter.event_id;
            List<rpc_tran_mcd_receive_detail_DTO> detailData = new List<rpc_tran_mcd_receive_detail_DTO>();
            masterData = await _rpc_db_service.GetAllJoined_TranMcdReceiveAsync(start, size, ActionType.getById.ToString(), Convert.ToInt64(decoded_received_id), 2, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, request.Search);
            detailData = await _rpc_db_service.GetAllJoined_TranMcdReceiveDetailAsync(Convert.ToInt64(decoded_received_id));
            ReceiveViewModel viewModel = new ReceiveViewModel
            {
                MasterData = masterData,
                DetailData = detailData
            };

            return PartialView("~/Views/MCD/TranMcdReceive/_TranMcdReceiveApprovalView.cshtml", viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> GetPOApprovalData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            

            var records = await _transcmpo.GetAllPoApprovalAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id,request.Search);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.po_id,
                            t.po_no,
                            t.po_date,
                            t.supplier_name,
                            t.unit_name,
                            t.pr_no,
                            t.event_title,
                            

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +


                            "<button type='button' onclick='AddItemReceive(this);'  class='btn btn-success btnAdd'  po_id='" + t.po_id.ToString() + "'>Add Item</button>" +

                            "</div>"
                        }).Skip(request.Start).Take(request.Length).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }


        [HttpPost]
        public async Task<IActionResult> GetTranMcdDraftReceiveData(DtParameters request)
        {
            Int64 receivedID = 0;
            Int64 actionType = 1;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetAllJoined_TranMcdReceiveListAsync(request.Start, request.Length, ActionType.getAll.ToString(), receivedID, actionType, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id,request.Search);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.received_id,
                            t.receive_no,
                            t.arrival_date,
                            t.delevary_status,
                            t.documents,
                            t.po_status,
                            t.po_no,
                            t.supplier_name,
                            t.office_address,
                            t.factory_address,
                            t.contact_person,
                            t.unit_name,
                            t.unit_address,
                            t.delivery_status_type,
                            t.transport_type,
                            t.transaction_mode_type,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='EditTranMcdReceive(this)' style='width: 50px;font-weight:600px;border: none;' class='btn btn-primary btnEdit'  received_id='" + clsUtil.EncodeString(t.received_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +

                            "<button type='button' onclick='ViewTranDraftMcdReceive(this)' style='width: 50px;font-weight:600px;border: none;' class='btn btn-warning btnView'  received_id='" + clsUtil.EncodeString(t.received_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();

            var totalRows = records?.FirstOrDefault()?.total_rows ?? 0;

            var ret_obj = new AjaxResponse(totalRows, data);

            return Json(ret_obj);

        }


        [HttpPost]
        public async Task<IActionResult> GetTranMcdReceiveData(DtParameters request)
        {
            Int64 receivedID = 0;
            Int64 actionType = 2;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetAllJoined_TranMcdReceiveListAsync(request.Start, request.Length, ActionType.getAll.ToString(), receivedID, actionType, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id,request.Search);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.received_id,
                            t.receive_no,
                            t.arrival_date,
                            t.delevary_status,
                            t.documents,
                            t.po_status,
                            t.po_no,
                            t.supplier_name,
                            t.office_address,
                            t.factory_address,
                            t.contact_person,
                            t.unit_name,
                            t.unit_address,
                            t.delivery_status_type,
                            t.transport_type,
                            t.transaction_mode_type,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewTranMcdReceive(this)' style='width: 50px;font-weight:600px;border: none;' class='btn btn-warning btnView'  received_id='" + clsUtil.EncodeString(t.received_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();

            //  var totalRows = records?.FirstOrDefault()?.total_rows ?? 0;
            var totalRows = records?.Count() ?? 0;

            var ret_obj = new AjaxResponse(totalRows, data);

            return Json(ret_obj);
        }




        [HttpPost]
        public async Task<IActionResult> SaveTranMcdReceive([FromBody] tran_mcd_receive_DTO request)
        {
            #region
            //GenSeasonEventConfigurationDTO objFilter = new GenSeasonEventConfigurationDTO();

            //if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            //{
            //    objFilter = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

            //    if (objFilter == null)
            //    {
            //        return RedirectToAction("Index", "Dashboard", new { load_filter = "1" });
            //    }
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Dashboard", new { load_filter = "1" });
            //}


            // var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            // List<Claim> listClaims = (List<Claim>)claim.Claims;
            #endregion

            var ret = true;
            try
            {

                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;

                if (request.received_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;
                   
                }

                request.po_status = request.gen_item_structure_group_id == 1 ? "A" : "D";

                var model = JsonConvert.DeserializeObject<tran_mcd_receive_entity>(JsonConvert.SerializeObject(request));

                model.event_id = objFilter.event_id;
                model.fiscal_year_id = objFilter.fiscal_year_id;


                var details = JsonConvert.DeserializeObject<List<tran_mcd_receive_detail_entity>>(JsonConvert.SerializeObject(request.details));

                details.ForEach(n =>
                    {
                        n.tran_type = 1;
                        n.added_by = sec_object.userid.Value;
                        n.date_added = DateTime.Now;
                    });
                

                ret = await _TranMcdReceiveService.SaveAsync(model, details, request.List_Files);


                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = ret == true ? (model.is_submitted == 1 ? "Successful" : "Confirm Received") : "Data Saving Failed"
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
        public async Task<IActionResult> AddNewItem(string gen_item_master_id, string gen_item_structure_group_sub_id)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {
            var list = await _TranMcdReceiveService.GetAllsp_get_techPack_by_gen_item_structure_group_sub_idAsync(Convert.ToInt32(gen_item_master_id), Convert.ToInt32(gen_item_structure_group_sub_id));

            // var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(measurement_unit_id));

            //ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            //{
            //    Text = p.unit_detail_display,
            //    Value = p.gen_measurement_unit_detail_id.ToString(),
            //    Selected = p.gen_measurement_unit_detail_id.ToString() == default_measurement_unit_detail_id ? true : false,
            //}).ToList();

            // var list_placement = JsonConvert.DeserializeObject<List<style_placement_information_DTO>>(list[0].styleplacementinformation_list);

            //ViewBag.list_placement = list_placement.Select(p => new SelectListItem
            //{
            //    Text = p.placement,
            //    Value = p.style_placement_information_id.ToString(),
            //}).ToList();

            ViewBag.gen_item_structure_group_sub_id = Convert.ToInt32(gen_item_structure_group_sub_id);

            // ViewBag.item_structure_group_id = Convert.ToInt32(item_structure_group_id);

            return PartialView("~/Views/MCD/TranMcdReceive/_ItemDetail.cshtml", list);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateTranMcdReceive([FromBody] tran_mcd_receive_DTO request)
        {

            var ret = true;

            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            

            try
            {
                var model = JsonConvert.DeserializeObject<tran_mcd_receive_entity>(JsonConvert.SerializeObject(request));
                model.event_id = objFilter.event_id;
                model.fiscal_year_id = objFilter.fiscal_year_id;
                model.is_submitted = request.is_submitted;

                var details = JsonConvert.DeserializeObject<List<tran_mcd_receive_detail_entity>>(JsonConvert.SerializeObject(request.details));

                ret = await _TranMcdReceiveService.UpdateAsync(model, details, request.List_Files);

                if (model.is_submitted == 1)
                {
                    return Json(new ResultEntity
                    {
                        Status_Code = (ret == true ? "200" : "400"),
                        Status_Result = (ret == true ? "Data Updated Successfully" : "Data Updated Failed")

                    });
                }
                else
                {
                    return Json(new ResultEntity
                    {
                        Status_Code = (ret == true ? "200" : "400"),
                        Status_Result = (ret == true ? "Confirm Received Successfully" : "Data Proposed Failed")

                    });

                }

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
        public async Task<IActionResult> DeleteTranMcdReceive([FromBody] tran_mcd_receive_DTO request)
        {
            //var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            //List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            //if (listClaims.Exists(c => c.Type == "secobject"))
            //{
            //    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

            //    SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            //  }

            try
            {

                string decoded_received_id = clsUtil.DecodeString(request.strMasterID);

                request.received_id = Convert.ToInt64(decoded_received_id);

                ret = await _TranMcdReceiveService.DeleteAsync(request.received_id.Value);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),

                    Status_Result = (ret == true ? "Successfully" : "Data Deletion Failed")

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


            return PartialView("~/Views/MCD/TranMcdReceive/_AddFabricDetails.cshtml", model);

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

            return PartialView("~/Views/MCD/TranMcdReceive/_AddAccessoriesDetails.cshtml", model);

        }

        [HttpGet]
        public async Task<JsonResult> AddAccessoriesDetailsMaster(string input)
        {


            var AccessoriesDetetail = JsonConvert.DeserializeObject<List<gen_item_master_DTO>>(input);
            long InsertedID = 0;

            InsertedID = await _GenItemMasterService.CheckAndSaveItemMasterAsync(AccessoriesDetetail.FirstOrDefault(), sec_object.userid);


            return Json(InsertedID);


        }

        [HttpGet]
        public async Task<JsonResult> AddFabricDetailsMaster(string input)
        {

            var FabricDetetail = JsonConvert.DeserializeObject<List<gen_item_master_DTO>>(input);
            long InsertedID = 0;

            InsertedID = await _GenItemMasterService.CheckAndSaveItemMasterAsync(FabricDetetail.FirstOrDefault(), sec_object.userid);


            return Json(InsertedID);


        }

        [HttpPost]
        public async Task<IActionResult> SaveTranRevise([FromBody] tran_mcd_receive_DTO request)
        {
           // tran_mcd_receive_DTO bb = new tran_mcd_receive_DTO();
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

               // request.added_by = sec_object.userid.Value;
               // request.date_added = DateTime.Now;
            }

            try
            {
                //request.fiscal_year_id = Fiscal_Year_ID;
                //request.event_id = Event_ID;
                // request.tran_revise_po_details = JArray.Parse(JsonConvert.SerializeObject(request.unmatchingDetails)).ToString();

                request.is_submitted = Convert.ToInt32(StatusFalg.is_revised);
                
                request.tran_revise_po_details = JsonConvert.SerializeObject(request.unmatchingDetails);
                
                ret = await _transcmpo.SaveRevisePoAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : "Data Saving Failed")
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


  
    }

}

