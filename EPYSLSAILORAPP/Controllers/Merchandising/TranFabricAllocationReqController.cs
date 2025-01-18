using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Application.DTO;
using System.Security.Claims;
using EPYSLSAILORAPP.Application.Interface;
using BDO.Core.Base;
using Web.Core.Frame.Helpers;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.SystemServices;
using EPYSLSAILORAPP.Domain.DTO;
using ServiceStack;
using Serilog.Context;

using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Infrastructure.Services;


namespace EPYSLSAILORAPP.Controllers
{
    public class TranFabricAllocationReqController : ExtendedBaseController
    {
        private readonly ILogger<TranFabricAllocationReqController> _logger;

        private readonly IConfiguration _configuration;

        private readonly ITranFabricAllocationReqService _TranFabricAllocationReqService;
        private readonly IGenItemMasterService _GenItemMasterService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside TranFabricAllocationReqController !");
            return View();
        }

        public TranFabricAllocationReqController(
           IMapper mapper, ILogger<TranFabricAllocationReqController> logger, IHttpContextAccessor contextAccessor,
            ITranFabricAllocationReqService TranFabricAllocationReqService,
            IRPCDbService rpc_db_service, IGenItemMasterService genItemMasterService, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranFabricAllocationReqService = TranFabricAllocationReqService;

            

            _contextAccessor = contextAccessor;
            _GenItemMasterService = genItemMasterService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> TranFabricAllocationReqLanding()
        {
            return View("~/Views/MerchandiserMgt/TranFabricAllocationReq/TranFabricAllocationReqLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranFabricAllocationReqAppovalLanding()
        {
            return View("~/Views/MerchandiserMgt/TranFabricAllocationReq/TranFabricAllocationReqAppovalLanding.cshtml");
        }


        [HttpGet]
        public async Task<IActionResult> TranFabricAllocationReqNew()
        {
            tran_fabric_allocation_req_DTO model = new tran_fabric_allocation_req_DTO();
            model.allocation_date = DateTime.Now;

            var list = await _rpc_db_service.GetAllsp_get_mapped_segment_by_gen_item_structure_group_sub_idAsync(Convert.ToInt32(Enum_gen_item_structure_group_sub.Fabric));

            var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(Enum_measurement_unit.Weight));

            ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            {
                Text = p.unit_detail_display,
                Value = p.gen_measurement_unit_detail_id.ToString()
            }).ToList();

            model.ListCombination = list;

            return PartialView("~/Views/MerchandiserMgt/TranFabricAllocationReq/_TranFabricAllocationReqNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranFabricAllocationTrasnferReqNew()
        {
            tran_fabric_allocation_req_DTO model = new tran_fabric_allocation_req_DTO();
            model.allocation_date = DateTime.Now;

            var list = await _rpc_db_service.GetAllsp_get_mapped_segment_by_gen_item_structure_group_sub_idAsync(Convert.ToInt32(Enum_gen_item_structure_group_sub.Fabric));

            var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(Enum_measurement_unit.Weight));

            ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            {
                Text = p.unit_detail_display,
                Value = p.gen_measurement_unit_detail_id.ToString()
            }).ToList();

            model.ListCombination = list;

            return PartialView("~/Views/MerchandiserMgt/TranFabricAllocationReq/_TranFabricAllocationTransferReqNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranFabricAllocationReqApprove(string tran_fabric_allocation_req_id)
        {

            string decoded_tran_fabric_allocation_req_id = clsUtil.DecodeString(tran_fabric_allocation_req_id);

            tran_fabric_allocation_req_DTO model = new tran_fabric_allocation_req_DTO();

            model = await _TranFabricAllocationReqService.GetSingleAsync(Convert.ToInt64(decoded_tran_fabric_allocation_req_id));

            var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(Enum_measurement_unit.Weight));

            ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            {
                Text = p.unit_detail_display,
                Value = p.gen_measurement_unit_detail_id.ToString()
            }).ToList();


            return PartialView("~/Views/MerchandiserMgt/TranFabricAllocationReq/_TranFabricAllocationReqApprove.cshtml", model);

        }



        [HttpGet]
        public async Task<IActionResult> TranFabricTransferReqEdit(string tran_fabric_allocation_req_id)
        {

            string decoded_tran_fabric_allocation_req_id = clsUtil.DecodeString(tran_fabric_allocation_req_id);

            tran_fabric_allocation_req_DTO model = new tran_fabric_allocation_req_DTO();

            model = await _TranFabricAllocationReqService.GetSingleAsync(Convert.ToInt64(decoded_tran_fabric_allocation_req_id));

            var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(Enum_measurement_unit.Weight));

            ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            {
                Text = p.unit_detail_display,
                Value = p.gen_measurement_unit_detail_id.ToString()
            }).ToList();


            return PartialView("~/Views/MerchandiserMgt/TranFabricAllocationReq/_TranFabricAllocationTransferReqEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranFabricAllocationReqEdit(string tran_fabric_allocation_req_id)
        {

            string decoded_tran_fabric_allocation_req_id = clsUtil.DecodeString(tran_fabric_allocation_req_id);

            tran_fabric_allocation_req_DTO model = new tran_fabric_allocation_req_DTO();

            model = await _TranFabricAllocationReqService.GetSingleAsync(Convert.ToInt64(decoded_tran_fabric_allocation_req_id));

            var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(Enum_measurement_unit.Weight));

            ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            {
                Text = p.unit_detail_display,
                Value = p.gen_measurement_unit_detail_id.ToString()
            }).ToList();


            return PartialView("~/Views/MerchandiserMgt/TranFabricAllocationReq/_TranFabricAllocationReqEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranFabricAllocationReqView(string tran_fabric_allocation_req_id)
        {

            string decoded_tran_fabric_allocation_req_id = clsUtil.DecodeString(tran_fabric_allocation_req_id);

            tran_fabric_allocation_req_DTO model = new tran_fabric_allocation_req_DTO();

            model = await _TranFabricAllocationReqService.GetSingleAsync(Convert.ToInt64(decoded_tran_fabric_allocation_req_id));

            var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(Enum_measurement_unit.Weight));

            ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            {
                Text = p.unit_detail_display,
                Value = p.gen_measurement_unit_detail_id.ToString()
            }).ToList();

            return PartialView("~/Views/MerchandiserMgt/TranFabricAllocationReq/_TranFabricAllocationReqView.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranFabricTransferReqView(string tran_fabric_allocation_req_id)
        {

            string decoded_tran_fabric_allocation_req_id = clsUtil.DecodeString(tran_fabric_allocation_req_id);

            tran_fabric_allocation_req_DTO model = new tran_fabric_allocation_req_DTO();

            model = await _TranFabricAllocationReqService.GetSingleAsync(Convert.ToInt64(decoded_tran_fabric_allocation_req_id));

            var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(Enum_measurement_unit.Weight));

            ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            {
                Text = p.unit_detail_display,
                Value = p.gen_measurement_unit_detail_id.ToString()
            }).ToList();

            return PartialView("~/Views/MerchandiserMgt/TranFabricAllocationReq/_TranFabricAllocationTransferReqView.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranFabricAllocationReqApprovalView(string tran_fabric_allocation_req_id)
        {

            string decoded_tran_fabric_allocation_req_id = clsUtil.DecodeString(tran_fabric_allocation_req_id);

            tran_fabric_allocation_req_DTO model = new tran_fabric_allocation_req_DTO();

            model = await _TranFabricAllocationReqService.GetSingleAsync(Convert.ToInt64(decoded_tran_fabric_allocation_req_id));

            var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(Enum_measurement_unit.Weight));

            ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            {
                Text = p.unit_detail_display,
                Value = p.gen_measurement_unit_detail_id.ToString()
            }).ToList();

            return PartialView("~/Views/MerchandiserMgt/TranFabricAllocationReq/_TranFabricAllocationReqApprovalView.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranFabricTransferReqApprovalView(string tran_fabric_allocation_req_id)
        {

            string decoded_tran_fabric_allocation_req_id = clsUtil.DecodeString(tran_fabric_allocation_req_id);

            tran_fabric_allocation_req_DTO model = new tran_fabric_allocation_req_DTO();

            model = await _TranFabricAllocationReqService.GetSingleAsync(Convert.ToInt64(decoded_tran_fabric_allocation_req_id));

            var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(Enum_measurement_unit.Weight));

            ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            {
                Text = p.unit_detail_display,
                Value = p.gen_measurement_unit_detail_id.ToString()
            }).ToList();

            return PartialView("~/Views/MerchandiserMgt/TranFabricAllocationReq/_TranFabricTransferReqApprovalView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetTranFabricAllocationReqData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranFabricAllocationReqService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_fabric_allocation_req_id,
                            t.allocation_number,
                            t.allocation_date,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,
                            str_allocation_date = t.allocation_date.Value.ToString("dd-MMM-yyyy"),

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            ((t.is_submitted == null || t.is_submitted == 1) ? "<button type='button' onclick='EditTranFabricAllocationReq(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  tran_fabric_allocation_req_id='" + clsUtil.EncodeString(t.tran_fabric_allocation_req_id.ToString()) + "'>Edit</button>" : "") +
                            "<button type='button' onclick='ViewTranFabricAllocationReq(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_fabric_allocation_req_id='" + clsUtil.EncodeString(t.tran_fabric_allocation_req_id.ToString()) + "'>View</button>" +
                            //"<button type='button' onclick='DeleteTranFabricAllocationReq(\"" + clsUtil.EncodeString(t.tran_fabric_allocation_req_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>",
                            datatable_transfer_buttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            ((t.is_submitted == null || t.is_submitted == 1) ? "<button type='button' onclick='EditTranFabricTransferReq(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  tran_fabric_allocation_req_id='" + clsUtil.EncodeString(t.tran_fabric_allocation_req_id.ToString()) + "'>Edit</button>" : "") +
                            "<button type='button' onclick='ViewTranFabricTransferReq(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_fabric_allocation_req_id='" + clsUtil.EncodeString(t.tran_fabric_allocation_req_id.ToString()) + "'>View</button>" +
                            //"<button type='button' onclick='DeleteTranFabricAllocationReq(\"" + clsUtil.EncodeString(t.tran_fabric_allocation_req_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>",
                            approvalbuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            ((t.is_submitted == 2 && t.is_approved == null) ? "<button type='button' onclick='TranFabricAllocationReqApprovalView(this)' style='width: 120px;' class='btn btn-secondary btnApprove'  tran_fabric_allocation_req_id='" + clsUtil.EncodeString(t.tran_fabric_allocation_req_id.ToString()) + "'>Approve / Reject</button>" : "") +
                            "<button type='button' onclick='ViewTranFabricAllocationReq(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_fabric_allocation_req_id='" + clsUtil.EncodeString(t.tran_fabric_allocation_req_id.ToString()) + "'>View</button>" +
                            //"<button type='button' onclick='DeleteTranFabricAllocationReq(\"" + clsUtil.EncodeString(t.tran_fabric_allocation_req_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>",
                            approval_transfer_buttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            ((t.is_submitted == 2 && t.is_approved == null) ? "<button type='button' onclick='TranFabricTransferReqApprovalView(this)' style='width: 120px;' class='btn btn-secondary btnApprove'  tran_fabric_allocation_req_id='" + clsUtil.EncodeString(t.tran_fabric_allocation_req_id.ToString()) + "'>Approve / Reject</button>" : "") +
                            "<button type='button' onclick='ViewTranFabricAllocationReq(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_fabric_allocation_req_id='" + clsUtil.EncodeString(t.tran_fabric_allocation_req_id.ToString()) + "'>View</button>" +
                            //"<button type='button' onclick='DeleteTranFabricAllocationReq(\"" + clsUtil.EncodeString(t.tran_fabric_allocation_req_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }



        [HttpPost]
        public async Task<IActionResult> SaveTranFabricAllocationReq([FromBody] tran_fabric_allocation_req_DTO request)
        {

            var ret = true;

            if (request.tran_fabric_allocation_req_id == null)
            {
                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;
            }

            try
            {

                request.allocation_date = DateTime.Now;

                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;

                ret = await _TranFabricAllocationReqService.SaveAsync(request);

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

        [HttpPost]
        public async Task<IActionResult> UpdateTranFabricAllocationReq([FromBody] tran_fabric_allocation_req_DTO request)
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
                ret = await _TranFabricAllocationReqService.UpdateAsync(request);

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

        [HttpPost]
        public async Task<IActionResult> ApproveRejectTranFabricAllocationReq([FromBody] tran_fabric_allocation_req_DTO request)
        {
            var ret = true;

            request.approved_by = sec_object.userid.Value;

            request.approved_date = DateTime.Now;

            request.fiscal_year_id = Fiscal_Year_ID;

            request.event_id = Event_ID;

            if (request.is_approved == 2) //reject
            {
                request.is_submitted = null;
            }
            else
            {
                request.is_submitted = 2;
            }


            try
            {
                ret = await _TranFabricAllocationReqService.ApproveRejectAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Approved Successfully" : "Approval Failed")
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
        public async Task<IActionResult> DeleteTranFabricAllocationReq([FromBody] tran_fabric_allocation_req_DTO request)
        {


            var ret = true;


            {


                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

            }

            try
            {

                string decoded_tran_fabric_allocation_req_id = clsUtil.DecodeString(request.strMasterID);

                request.tran_fabric_allocation_req_id = Convert.ToInt64(decoded_tran_fabric_allocation_req_id);

                ret = await _TranFabricAllocationReqService.DeleteAsync(request.tran_fabric_allocation_req_id.Value);

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


    }
}

