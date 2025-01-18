using AutoMapper;
using BDO.Core.Base;
using DnsClient;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Enum;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Context;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Claims;
using Web.Core.Frame.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EPYSLSAILORAPP.Controllers.ShopFloor
{
    public class EmbellishmentReceiveController : ExtendedBaseController
    {
        private readonly ILogger<EmbellishmentReceiveController> _logger;

        private readonly IEmbellishmentDeliveryChallanService _EmbellishmentDeliveryChallanService;
        private readonly ITranServiceWorkOrderService _TranServiceWorkOrderService;
        private readonly ITranEmbellishReceiveService _TranEmbellishReceiveService;

        private readonly IGenUnitService _GenunitService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside TranServiceWorkOrderController !");
            return View();
        }

        public EmbellishmentReceiveController(
           IMapper mapper, ILogger<EmbellishmentReceiveController> logger, IHttpContextAccessor contextAccessor,
            IEmbellishmentDeliveryChallanService EmbellishmentDeliveryChallanService, ITranServiceWorkOrderService TranServiceWorkOrderService, IGenUnitService genunitService,
            IRPCDbService rpc_db_service, ITranEmbellishReceiveService TranEmbellishReceiveService, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _EmbellishmentDeliveryChallanService = EmbellishmentDeliveryChallanService;
            _GenunitService = genunitService;
            _TranServiceWorkOrderService = TranServiceWorkOrderService;
            _TranEmbellishReceiveService = TranEmbellishReceiveService;
            // _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);

            
            _configuration = configuration;
            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> EmbellishmentReceiveLanding()
        {
            return View("~/Views/ShopFloor/EmbellishmentReceive/EmbellishmentReceiveLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranDeliveryReceiveAdd(string service_work_order_id, DtParameters request)
        {

            string service_work_orderid = clsUtil.DecodeString(service_work_order_id);
            var start = request.Start;
            var size = 10;
            tran_service_work_order_DTO model = new tran_service_work_order_DTO();

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            model = await _EmbellishmentDeliveryChallanService.GetnDeliveryChallanReceiveAddAsync(start, size, Convert.ToInt64(service_work_orderid), ActionType.getById.ToString(), request.fiscal_year_id, request.event_id, request.delivery_unit_id);

            //model = await _EmbellishmentDeliveryChallanService.GetDeliveryChalan_PendingListAsync(start + 1, size, Convert.ToInt64(service_work_orderid), ActionType.getById.ToString(), request.fiscal_year_id, request.event_id, request.delivery_unit_id);

            return PartialView("~/Views/ShopFloor/EmbellishmentReceive/_TranEmblishmentReceiveAdd.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetEmbellishmentReceivePendingData(DtParameters request)
        {
            Int64 workOrderId = 0;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _EmbellishmentDeliveryChallanService.GetnDeliveryChallanReceivePendingAsync(request.Start, request.Length, workOrderId, ActionType.getAll.ToString(), request.fiscal_year_id, request.event_id, request.delivery_unit_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.embellish_delivery_challan_id,
                            t.service_work_order_id,
                            t.techpack_number,
                            t.service_work_order_no,
                            t.service_work_date,
                            t.unit_name,
                            t.name,
                            t.process_name,
                            t.embellish_delivery_challan_no,
                            t.order_quantity,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='TranDeliveryReceiveAdd(this)' style='width:180px' class='btn btn-success btnEdit'  service_work_order_id='" + clsUtil.EncodeString(t.service_work_order_id.ToString()) + "'>Emblishment Receive Data</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetReceiveChallanDraftData(DtParameters request)
        {
            Int64 actionType = 1;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranEmbellishReceiveService.GetTranReceivedChallanAllListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.embellish_receive_id,
                            t.embellish_receive_no,
                            t.embellish_receive_date,
                            t.name,
                            t.techpack_number,
                            t.service_work_order_no,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewEmbDeliveryChallanReceiveDetails(this)' style='width:140px' class='btn btn-success btnEdit'  embellish_receive_id='" + clsUtil.EncodeString(t.embellish_receive_id.ToString()) + "'>Proposed For Approval</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetReceiveChallanProposedData(DtParameters request)
        {
            Int64 actionType = 2;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranEmbellishReceiveService.GetTranReceivedChallanAllListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.embellish_receive_id,
                            t.embellish_receive_no,
                            t.embellish_receive_date,
                            t.name,
                            t.techpack_number,
                            t.service_work_order_no,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewEmbDeliveryChallanProposedDetails(this)' style='width:120px' class='btn btn-success btnEdit'  embellish_receive_id='" + clsUtil.EncodeString(t.embellish_receive_id.ToString()) + "'>Approve</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetReceiveChallanApprovedData(DtParameters request)
        {

            Int64 actionType = 3;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranEmbellishReceiveService.GetTranReceivedChallanAllListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.embellish_receive_id,
                            t.embellish_receive_no,
                            t.embellish_receive_date,
                            t.name,
                            t.techpack_number,
                            t.service_work_order_no,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewEmbDeliveryChallanReceiveApproveDetails(this)' style='width:120px' class='btn btn-success btnEdit'  embellish_receive_id='" + clsUtil.EncodeString(t.embellish_receive_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }


        [HttpGet]
        public async Task<ActionResult> AddreceiveDetailNew(long service_work_order_id)
        {
            tran_service_work_order_DTO model = new tran_service_work_order_DTO();
            //ViewBag.LineNumber = _GenProductionLineService.GetSingleLineByAsync(2).Select(a =>
            //     new SelectListItem
            //     {
            //         Text = a.line_name.ToString(),
            //         Value = a.production_line_id.ToString()
            //     }
            //     ).ToList();

            ViewBag.Colors = _TranServiceWorkOrderService.GetAllproc_sp_get_colors_by_work_order(service_work_order_id)
                     .Select(a => a.color_code.ToString())
                     .Distinct()
                     .Select(colorCode => new SelectListItem
                     {
                         Text = colorCode,
                         Value = colorCode
                     })
                     .ToList();


            return PartialView("~/Views/ShopFloor/EmbellishmentReceive/_EmbReceiveDetails.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetColorWisePart(long service_work_order_id, string p_color_code)
        {

            List<tran_emb_part_dropdown> records = await _EmbellishmentDeliveryChallanService.GetAllproc_sp_get_color_wise_part_async(service_work_order_id, p_color_code);
            ViewBag.Gen_Part_Id = records
                     .Select(a => new SelectListItem
                     {
                         Text = a.garment_part_name,
                         Value = a.gen_garment_part_id.ToString()
                     })
                     .ToList();
            return Json(records);
        }

        [HttpGet]
        public async Task<IActionResult> GetPartWiseBatch(long workOrderId, string colorCode, long genGarmentPartId)
        {

            List<tran_emb_part_dropdown> records = await _EmbellishmentDeliveryChallanService.GetAllproc_sp_get_part_wise_batch_async(workOrderId, colorCode, genGarmentPartId);

            return Json(records);
        }

        [HttpGet]
        public async Task<JsonResult> GetBatchWiseBundleDetaiilsReceive(long batch_id, string colorCode, long genGarmentPartId)
        {

            var data = await _EmbellishmentDeliveryChallanService.GetAll_bundle_batch_wiseAsync(batch_id, colorCode, genGarmentPartId);

            return Json(new { data = data });
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmbDeliveryReceive([FromBody] tran_embellish_receive_DTO request)
        {


            var ret = true;

            request.added_by = sec_object.userid.Value;
            request.date_added = DateTime.Now;

            try
            {
                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;
                request.embellish_receive_detail_list = JArray.Parse(JsonConvert.SerializeObject(request.details));

                ret = await _TranEmbellishReceiveService.tran_embellish_receive_insert_sp(request);

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

        [HttpGet]
        public async Task<IActionResult> EmbDeliveryChallanReceiveDetails(string embellish_receive_id, DtParameters request)
        {
            try
            {
                string decoded_embellish_receive_id = clsUtil.DecodeString(embellish_receive_id);

                var data = await _TranEmbellishReceiveService.Get_master_detail_tran_emb_delivery_challan_Receive_Async(Convert.ToInt64(decoded_embellish_receive_id));
                tran_embellish_receive_DTO model = JsonConvert.DeserializeObject<tran_embellish_receive_DTO>(JsonConvert.SerializeObject(data));

                model.details = JsonConvert.DeserializeObject<List<tran_embellish_receive_detail_DTO>>(data.tran_embellish_receive_detail_list);

                return PartialView("~/Views/ShopFloor/EmbellishmentReceive/_EmbDeliveryChallanReceiveDetailsView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        [HttpPost]
        public async Task<IActionResult> ProposedForApprovalOrApprove([FromBody] tran_embellish_receive_DTO request)
        {

            var ret = true;

            {

                request.updated_by = sec_object.userid.Value;
                request.date_added = DateTime.Now;
            }

            if (request.is_submitted == 1)
            {
                ret = await _TranEmbellishReceiveService.ApprovalProposedAsync(request);
                return Json(new ResultEntity
                {
                    Status_Code = ret == true ? "200" : "400",
                    Status_Result = ret == true ? "Proposed for approval successfully." : "Proposed Failed."
                });
            }
            else if (request.is_submitted == 2)
            {
                ret = await _TranEmbellishReceiveService.ApprovedAsync(request);
                return Json(new ResultEntity
                {
                    Status_Code = ret == true ? "200" : "400",
                    Status_Result = ret == true ? "Approved successfully" : "Approved Failed"
                });
            }
            else
            {
                return Json(new ResultEntity
                {
                    Status_Code = "400",
                    Status_Result = "Operation Failed"
                });
            }

        }



    }
}








