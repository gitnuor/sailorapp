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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Context;
using ServiceStack;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers.ShopFloor
{
    public class EmbellishmentDeliveryChallanController : ExtendedBaseController
    {
        private readonly ILogger<EmbellishmentDeliveryChallanController> _logger;

        private readonly IEmbellishmentDeliveryChallanService _EmbellishmentDeliveryChallanService;

        private readonly IGenUnitService _GenUnitService;
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

        public EmbellishmentDeliveryChallanController(
           IMapper mapper, ILogger<EmbellishmentDeliveryChallanController> logger, IHttpContextAccessor contextAccessor,
            IEmbellishmentDeliveryChallanService EmbellishmentDeliveryChallanService, IGenUnitService GenUnitService,
            IRPCDbService rpc_db_service, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _EmbellishmentDeliveryChallanService = EmbellishmentDeliveryChallanService;
            _GenUnitService = GenUnitService;
            // _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);

            
            _configuration = configuration;
            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> EmbellishmentDeliveryChallanLanding()
        {

            return View("~/Views/ShopFloor/EmbellishmentDeliveryChallan/EmbellishmentDeliveryChallanLanding.cshtml");
        }


        [HttpGet]
        public async Task<IActionResult> TranDeliveryWorkOrderAdd(string service_work_order_id, DtParameters request)
        {

            string service_work_orderid = clsUtil.DecodeString(service_work_order_id);
            var start = request.Start;
            var size = 10;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            List<tran_service_work_order_DTO> model = new List<tran_service_work_order_DTO>();
            model = await _EmbellishmentDeliveryChallanService.GetDeliveryChalan_PendingListAsync(start, size, Convert.ToInt64(service_work_orderid), ActionType.getById.ToString(), request.fiscal_year_id, request.event_id, request.delivery_unit_id);

            return PartialView("~/Views/ShopFloor/EmbellishmentDeliveryChallan/_TranDeliveryChalanAdd.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> EmbDeliveryChallanDetails(string embellish_delivery_challan_id, DtParameters request)
        {
            try
            {
                string decoded_embellish_delivery_challan_id = clsUtil.DecodeString(embellish_delivery_challan_id);

                var data = await _EmbellishmentDeliveryChallanService.Get_master_detail_tran_emb_delivery_challan_Async(Convert.ToInt64(decoded_embellish_delivery_challan_id));
                tran_embellish_delivery_challan_DTO model = JsonConvert.DeserializeObject<tran_embellish_delivery_challan_DTO>(JsonConvert.SerializeObject(data));

                model.TranEmbellishDeliveryChallanDetail_List = JsonConvert.DeserializeObject<List<tran_embellish_delivery_challan_detail_DTO>>(data.tran_embellish_delivery_challan_detail_list);

                return PartialView("~/Views/ShopFloor/EmbellishmentDeliveryChallan/_EmbDeliveryChallanDetailsView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        [HttpGet]
        public async Task<IActionResult> EmbDeliveryChallanApproveDetails(string embellish_delivery_challan_id, DtParameters request)
        {
            try
            {
                string decoded_embellish_delivery_challan_id = clsUtil.DecodeString(embellish_delivery_challan_id);

                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;

                var data = await _EmbellishmentDeliveryChallanService.Get_master_detail_tran_emb_delivery_challan_Async(Convert.ToInt64(decoded_embellish_delivery_challan_id));
                tran_embellish_delivery_challan_DTO model = JsonConvert.DeserializeObject<tran_embellish_delivery_challan_DTO>(JsonConvert.SerializeObject(data));

                model.TranEmbellishDeliveryChallanDetail_List = JsonConvert.DeserializeObject<List<tran_embellish_delivery_challan_detail_DTO>>(data.tran_embellish_delivery_challan_detail_list);

                return PartialView("~/Views/ShopFloor/EmbellishmentDeliveryChallan/_EmbDeliveryChallanApproveDetailsView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        [HttpGet]
        public async Task<IActionResult> EmbDeliveryChallanDetailsForProposed(string embellish_delivery_challan_id, DtParameters request)
        {
            try
            {
                string decoded_embellish_delivery_challan_id = clsUtil.DecodeString(embellish_delivery_challan_id);

                var data = await _EmbellishmentDeliveryChallanService.Get_master_detail_tran_emb_delivery_challan_Async(Convert.ToInt64(decoded_embellish_delivery_challan_id));
                tran_embellish_delivery_challan_DTO model = JsonConvert.DeserializeObject<tran_embellish_delivery_challan_DTO>(JsonConvert.SerializeObject(data));

                model.TranEmbellishDeliveryChallanDetail_List = JsonConvert.DeserializeObject<List<tran_embellish_delivery_challan_detail_DTO>>(data.tran_embellish_delivery_challan_detail_list);

                return PartialView("~/Views/ShopFloor/EmbellishmentDeliveryChallan/_EmbDeliveryChallanApproveDetailsProposedView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        public async Task<IActionResult> EmbDeliveryChallanApprovedDetails(string embellish_delivery_challan_id, DtParameters request)
        {
            try
            {
                string decoded_embellish_delivery_challan_id = clsUtil.DecodeString(embellish_delivery_challan_id);

                var data = await _EmbellishmentDeliveryChallanService.Get_master_detail_tran_emb_delivery_challan_Async(Convert.ToInt64(decoded_embellish_delivery_challan_id));
                tran_embellish_delivery_challan_DTO model = JsonConvert.DeserializeObject<tran_embellish_delivery_challan_DTO>(JsonConvert.SerializeObject(data));

                model.TranEmbellishDeliveryChallanDetail_List = JsonConvert.DeserializeObject<List<tran_embellish_delivery_challan_detail_DTO>>(data.tran_embellish_delivery_challan_detail_list);

                return PartialView("~/Views/ShopFloor/EmbellishmentDeliveryChallan/_EmbDeliveryChallanApprovedDetailsView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }





        [HttpGet]
        public async Task<IActionResult> GetBatchDetaiils(long techpack_id)
        {

            List<tran_cutting_batch_wise_DTO> model = await _EmbellishmentDeliveryChallanService.GetAll_batch_workOrder_wiseAsync(techpack_id);

            return PartialView("~/Views/ShopFloor/EmbellishmentDeliveryChallan/_BatchDetails.cshtml", model);

        }

        [HttpGet]
        public async Task<JsonResult> GetBatchWiseBundleDetaiils(long batch_id)
        {

            var data = await _EmbellishmentDeliveryChallanService.GetAll_bundle_batch_wiseAsync(batch_id);

            return Json(new { data = data });
        }


        [HttpPost]
        public async Task<IActionResult> GetEmbellishmentDeliveryChallanPendingData(DtParameters request)
        {
            Int64 workOrderId = 0;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _EmbellishmentDeliveryChallanService.GetDeliveryChalan_PendingListAsync(request.Start, request.Length, workOrderId, ActionType.getAll.ToString(), request.fiscal_year_id, request.event_id, request.delivery_unit_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.service_work_order_id,
                            t.techpack_number,
                            t.service_work_order_no,
                            t.service_work_date,
                            t.unit_name,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='TranDeliveryWorkOrderAdd(this)' style='width:150px' class='btn btn-success btnEdit'  service_work_order_id='" + clsUtil.EncodeString(t.service_work_order_id.ToString()) + "'>Create Delivery Chalan</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetDeliveryChallanDraftData(DtParameters request)
        {
            Int64 actionType = 1;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _EmbellishmentDeliveryChallanService.GetTranDeliveryChallanListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.embellish_delivery_challan_id,
                            t.embellish_delivery_challan_no,
                            t.embellish_delivery_challan_date,
                            t.techpack_number,
                            t.unit_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewEmbDeliveryChallanDetails(this)' style='width:140px' class='btn btn-success btnEdit'  embellish_delivery_challan_id='" + clsUtil.EncodeString(t.embellish_delivery_challan_id.ToString()) + "'>Proposed For Approval</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetDeliveryChallanProposedData(DtParameters request)
        {
            Int64 actionType = 2;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _EmbellishmentDeliveryChallanService.GetTranDeliveryChallanListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.embellish_delivery_challan_id,
                            t.embellish_delivery_challan_no,
                            t.embellish_delivery_challan_date,
                            t.techpack_number,
                            t.unit_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewEmbDeliveryChallanDetailsForProposed(this)' style='width:120px' class='btn btn-success btnEdit'  embellish_delivery_challan_id='" + clsUtil.EncodeString(t.embellish_delivery_challan_id.ToString()) + "'>Approve</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetDeliveryChallanApprovedData(DtParameters request)
        {

            Int64 actionType = 3;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;


            var records = await _EmbellishmentDeliveryChallanService.GetTranDeliveryChallanListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.embellish_delivery_challan_id,
                            t.embellish_delivery_challan_no,
                            t.embellish_delivery_challan_date,
                            t.techpack_number,
                            t.unit_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewEmbDeliveryChallanApprovedDetails(this)' style='width:120px' class='btn btn-success btnEdit'  embellish_delivery_challan_id='" + clsUtil.EncodeString(t.embellish_delivery_challan_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmbDeliveryChallan([FromBody] tran_embellish_delivery_challan_DTO request)
        {




            var ret = true;


            {

                request.added_by = sec_object.userid.Value;
                request.date_added = DateTime.Now;
            }

            try
            {
                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;
                request.embellish_delivery_challan_detail_list = JArray.Parse(JsonConvert.SerializeObject(request.TranEmbellishDeliveryChallanDetail_List));

                ret = await _EmbellishmentDeliveryChallanService.tran_embellish_delivery_challan_insert_sp(request);

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
        public async Task<IActionResult> ProposedForApprovalOrApprove([FromBody] tran_embellish_delivery_challan_DTO request)
        {

            var ret = true;

            {

                request.updated_by = sec_object.userid.Value;
                request.date_added = DateTime.Now;
            }

            if (request.is_submitted == 1)
            {
                ret = await _EmbellishmentDeliveryChallanService.ApprovalProposedAsync(request);
                return Json(new ResultEntity
                {
                    Status_Code = ret == true ? "200" : "400",
                    Status_Result = ret == true ? "Proposed for approval successfully." : "Proposed Failed."
                });
            }
            else if (request.is_submitted == 2)
            {
                ret = await _EmbellishmentDeliveryChallanService.ApprovedAsync(request);
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
