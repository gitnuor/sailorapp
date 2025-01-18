using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Merchandiser;
using EPYSLSAILORAPP.Application.Interface.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Enum;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services;

using EPYSLSAILORAPP.Models.MCD;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Collections.Generic;
using System.Security.Claims;
//using System.Web.WebPages.Html;
using Web.Core.Frame.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using IFabricRequisitionService = EPYSLSAILORAPP.Application.Interface.Merchandiser.IFabricRequisitionService;
using Dapper;
using Npgsql;
using NpgsqlTypes;

namespace EPYSLSAILORAPP.Controllers.MCD
{
    public class TranMcdPurchaseReturnController : ExtendedBaseController
    {
        private readonly ILogger<TranMcdPurchaseReturnController> _logger;

        private readonly ITranMcdRequisitionSlipService _TranMcdRequisitionSlipService;


        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private readonly IGenUnitService _GenUnitService;
        private readonly ITranMcdPurchaseReturnService _TranMcdPurchaseReturnService;

        private readonly ITranMcdReceiveService _TranMcdReceiveService;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside FabricRequisitionSlipController !");
            return View();
        }

        public TranMcdPurchaseReturnController(
           IMapper mapper, ILogger<TranMcdPurchaseReturnController> logger, IHttpContextAccessor contextAccessor,
            ITranMcdRequisitionSlipService TranMcdRequisitionSlipService, IGenSupplierInformationService GenSupplierInformationService, IGenUnitService GenUnitService, ITranMcdReceiveService TranMcdReceiveService,
            IRPCDbService rpc_db_service, ITranMcdPurchaseReturnService tranMcdPurchaseReturnService, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranMcdRequisitionSlipService = TranMcdRequisitionSlipService;
            _GenSupplierInformationService = GenSupplierInformationService;
            _GenUnitService = GenUnitService;
            _configuration = configuration;
            

            _contextAccessor = contextAccessor;
            _TranMcdPurchaseReturnService = tranMcdPurchaseReturnService;
            _TranMcdReceiveService = TranMcdReceiveService;
        }

        [HttpGet]
        public async Task<IActionResult> PurchaseReturnLanding()
        {

            return View("~/Views/MCD/TranMcdPurchaseReturn/PurchaseReturnLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranMcdReturnAdd(string received_id, DtParameters request)
        {

            string decoded_received_id = clsUtil.DecodeString(received_id);
            var start = request.Start;
            var size = 10;
            request.fiscal_year_id = objFilter.fiscal_year_id;
            request.event_id = objFilter.event_id;
            rpc_tran_mcd_receive_DTO masterData = new rpc_tran_mcd_receive_DTO();
            List<rpc_tran_mcd_receive_detail_DTO> detailData = new List<rpc_tran_mcd_receive_detail_DTO>();

            masterData = await _rpc_db_service.GetAllJoined_TranMcdReceiveAsync(start, size, ActionType.getById.ToString(), Convert.ToInt64(decoded_received_id), 3, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id,request.Search);
            detailData = await _rpc_db_service.GetAllJoined_TranMcdReceiveDetailFailAsync(Convert.ToInt64(decoded_received_id));

            masterData.return_date = DateTime.Now;

            ReceiveViewModel viewModel = new ReceiveViewModel
            {
                MasterData = masterData,
                DetailData = detailData
            };

            return PartialView("~/Views/MCD/TranMcdPurchaseReturn/_TranMcdReturnAdd.cshtml", viewModel);

        }

        [HttpGet]
        public async Task<IActionResult> TranMcdReturnView(string purchase_return_id, DtParameters request)
        {
            try
            {
                string decoded_purchase_return_id = clsUtil.DecodeString(purchase_return_id);

                //var data = _rpc_db_service.Get_master_detail_tran_purchase_return_slipAsync(Convert.ToInt64(decoded_purchase_return_id));
                tran_mcd_purchase_return_DTO model = await _TranMcdPurchaseReturnService.GetSingleAsync(Convert.ToInt64(decoded_purchase_return_id)); //JsonConvert.DeserializeObject<tran_mcd_purchase_return_DTO>(JsonConvert.SerializeObject(data));
                //model.details = JsonConvert.DeserializeObject<List<tran_mcd_purchase_return_detail_DTO>>(model.tran_mcd_purchase_return_detail_list);
                model.details = JsonConvert.DeserializeObject<List<tran_mcd_purchase_return_detail_DTO>>(model.purchase_return_detail);

                return PartialView("~/Views/MCD/TranMcdPurchaseReturn/_TranMcdReturnView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public async Task<IActionResult> TranMcdReturnEdit(string purchase_return_id)
        {
            string decoded_purchase_return_id = clsUtil.DecodeString(purchase_return_id);

            tran_mcd_purchase_return_DTO model = await _TranMcdPurchaseReturnService.GetSingleAsync(Convert.ToInt64(decoded_purchase_return_id));

            model.details = JsonConvert.DeserializeObject<List<tran_mcd_purchase_return_detail_DTO>>(model.purchase_return_detail);

            List<tran_mcd_receive_transport_DTO> transportTypeList = await _TranMcdReceiveService.GetAllTransportType();

            ViewBag.transportTypeList = new SelectList(
               transportTypeList.Select(t => new SelectListItem
               {
                   Text = t.transport_type.ToString(),
                   Value = t.transport_id.ToString(),
               }),
               "Value", "Text", model.transport_type);

            return PartialView("~/Views/MCD/TranMcdPurchaseReturn/_TranMcdPurchaseReturnEdit.cshtml", model);

        }



        [HttpPost]
        public async Task<IActionResult> GetFurchaseReturnPendingData(DtParameters request)
        {
            Int64 receivedID = 0;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetTranMcdRejectListAsync(request.Start, request.Length, ActionType.getAll.ToString(), receivedID, request.fiscal_year_id, request.event_id, request.supplier_id,request.Search.Value);

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


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='TranMcdReturnAdd(this)' style='width:120px' class='btn btn-success btnEdit'  received_id='" + clsUtil.EncodeString(t.received_id.ToString()) + "'>Return Item</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetFurchaseReturnDraftData(DtParameters request)
        {
            Int64 actionType = 1;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetTranMcdRejectDraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id,request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,

                            t.purchase_return_id,
                            t.purchase_return_no,
                            t.receive_no,
                            t.return_date,
                            t.name,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='EditMcdReturn(this)' class='btn btn-primary btnEdit'  purchase_return_id='" + clsUtil.EncodeString(t.purchase_return_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +

                            "<button type='button' onclick='ViewTranMcdReturn(this)' style='width:40px' class='btn btn-info btnEdit'  purchase_return_id='" + clsUtil.EncodeString(t.purchase_return_id.ToString()) + "'><i class=\"fa-regular fa-eye\"></i></button>" +

                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetFurchaseReturnProposedData(DtParameters request)
        {
            Int64 actionType = 2;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetTranMcdRejectDraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id,request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,

                            t.purchase_return_id,
                            t.purchase_return_no,
                            t.receive_no,
                            t.return_date,
                            t.name,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewTranMcdReturn(this)' style='width:120px' class='btn btn-success btnEdit'  purchase_return_id='" + clsUtil.EncodeString(t.purchase_return_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetFurchaseReturnApprovedData(DtParameters request)
        {

            Int64 actionType = 3;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetTranMcdRejectDraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id, request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,

                            t.purchase_return_id,
                            t.purchase_return_no,
                            t.receive_no,
                            t.return_date,
                            t.name,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewTranMcdReturn(this)' style='width:120px' class='btn btn-success btnEdit'  purchase_return_id='" + clsUtil.EncodeString(t.purchase_return_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);
        }

        public async Task<IActionResult> SaveTranMCDReturn([FromBody] tran_mcd_purchase_return_DTO request)
        {


            //var ret = true;
            (bool success, long purchase_return_id) ret = (true, 0);

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            try
            {

                if (request.purchase_return_id == null || request.purchase_return_id == 0)
                {

                    request.added_by = sec_object.userid.Value;
                    request.date_added = DateTime.Now;
                }

                var model = JsonConvert.DeserializeObject<tran_mcd_purchase_return_entity>(JsonConvert.SerializeObject(request));
                
                model.event_id = objFilter.event_id;
                model.fiscal_year_id = objFilter.fiscal_year_id;


                var details = JsonConvert.DeserializeObject<List<tran_mcd_purchase_return_detail_entity>>(JsonConvert.SerializeObject(request.details));





                details.ForEach(n =>
                {
                    // n.tran_type = 1;
                    n.added_by = sec_object.userid.Value;
                    n.date_added = DateTime.Now;
                });



                ret = await _TranMcdPurchaseReturnService.tran_mcd_purchase_return_insert_sp(model, details);
                // ret = await _TranMcdPurchaseReturnService.SaveAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret.success ? "200" : "400"),
                    data = Convert.ToString(ret.purchase_return_id)
                    //Status_Result = (ret == true ? "Successful" : "Data Saving Failed")
                });
            }
            catch (Exception ex)
            {
                //ret = false;

                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    _logger.LogError(ex.ToString());
                }

                return Json(new ResultEntity
                {
                    Status_Code = "400", // Since the operation failed due to an exception, set the status code to 400
                    Status_Result = "Operation Failed: " + ex.Message //
                });
            }

        }


        public async Task<IActionResult> UpdateTranMCDReturn([FromBody] tran_mcd_purchase_return_DTO request)
        {
            (bool success, long purchase_return_id) ret = (true, 0);
            try
            {
                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;

                if (request.purchase_return_id != null)
                {

                    request.added_by = sec_object.userid.Value;
                    request.date_added = DateTime.Now;
                }

                var model = JsonConvert.DeserializeObject<tran_mcd_purchase_return_entity>(JsonConvert.SerializeObject(request));
                model.event_id = objFilter.event_id;
                model.fiscal_year_id = objFilter.fiscal_year_id;


                var details = JsonConvert.DeserializeObject<List<tran_mcd_purchase_return_detail_entity>>(JsonConvert.SerializeObject(request.details));

                details.ForEach(n =>
                {

                    n.added_by = sec_object.userid.Value;
                    n.date_added = DateTime.Now;
                    n.purchase_return_id = model.purchase_return_id;
                });



                ret = await _TranMcdPurchaseReturnService.tran_mcd_purchase_return_update_sp(model, details);


                return Json(new ResultEntity
                {
                    Status_Code = (ret.success ? "200" : "400"),
                    data = Convert.ToString(ret.purchase_return_id)
                    //Status_Result = (ret == true ? "Successful" : "Data Saving Failed")
                });
            }
            catch (Exception ex)
            {
                //ret = false;

                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    _logger.LogError(ex.ToString());
                }

                return Json(new ResultEntity
                {
                    Status_Code = "400", // Since the operation failed due to an exception, set the status code to 400
                    Status_Result = "Operation Failed: " + ex.Message //
                });
            }
        }


        [HttpPost]
        public async Task<IActionResult> ProposeForApprovalAndApprove(Int64 Id)
        {
            try
            {
                var ret = true;
                tran_mcd_purchase_return_DTO model = await _TranMcdPurchaseReturnService.GetSingleAsync(Id);
                //model.purchase_return_id = Id;

                if (model.is_submitted == 1)
                {
                    ret = await _TranMcdPurchaseReturnService.ProposedAsync(model);
                }
                if (model.is_submitted == 2)
                {
                    ret = await _TranMcdPurchaseReturnService.ApproveAsync(model);
                }


                return Json(new ResultEntity
                {
                    Status_Code = "200",
                });
            }
            catch (Exception ex)
            {
                return Json(new { Status_Code = "500", Status_Result = "Failed to propose for approval" });
            }
        }

        #region Purchase Return Approve

        [HttpGet]
        public async Task<IActionResult> PurchaseReturnApproveLanding()
        {

            return View("~/Views/MCD/TranMcdPurchaseReturn/PurchaseReturnApproveLanding.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> GetPurchaseReturnProposedDataApprove(DtParameters request)
        {
            Int64 actionType = 2;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetTranMcdRejectDraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id, request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,

                            t.purchase_return_id,
                            t.purchase_return_no,
                            t.receive_no,
                            t.return_date,
                            t.name,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewTranMcdReturnApprove(this)' style='width:120px' class='btn btn-success btnEdit'  purchase_return_id='" + clsUtil.EncodeString(t.purchase_return_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetPurchaseReturnApprovedData(DtParameters request)
        {

            Int64 actionType = 3;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetTranMcdRejectDraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id, request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,

                            t.purchase_return_id,
                            t.purchase_return_no,
                            t.receive_no,
                            t.return_date,
                            t.name,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewTranMcdReturnApprove(this)' style='width:120px' class='btn btn-success btnEdit'  purchase_return_id='" + clsUtil.EncodeString(t.purchase_return_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);
        }

        [HttpGet]
        public async Task<IActionResult> TranMcdReturnViewApprove(string purchase_return_id, DtParameters request)
        {
            try
            {
                string decoded_purchase_return_id = clsUtil.DecodeString(purchase_return_id);

                var data = await _rpc_db_service.Get_master_detail_tran_purchase_return_slipAsync(Convert.ToInt64(decoded_purchase_return_id));
                tran_mcd_purchase_return_DTO model = JsonConvert.DeserializeObject<tran_mcd_purchase_return_DTO>(JsonConvert.SerializeObject(data));
                model.details = JsonConvert.DeserializeObject<List<tran_mcd_purchase_return_detail_DTO>>(data.tran_mcd_purchase_return_detail_list);

                return PartialView("~/Views/MCD/TranMcdPurchaseReturn/_TranPurchaseReturnApproveView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        [HttpPost]
        public async Task<IActionResult> UpdateForApproval([FromBody] tran_mcd_purchase_return_DTO request)
        {
            var ret = true;

            request.added_by = sec_object.userid.Value;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            try
            {

                ret = await _TranMcdPurchaseReturnService.ApproveAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Data Approved Successful" : "Data Saving Failed")
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

        #endregion

    }
}
