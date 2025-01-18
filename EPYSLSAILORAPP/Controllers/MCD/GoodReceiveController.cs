using AutoMapper;
using BDO.Core.Base;
using Dapper;
using DnsClient;
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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using Serilog.Context;
using ServiceStack;
using System.Collections.Immutable;
using System.Drawing.Printing;
using System.Security.Claims;
//using System.Web.WebPages.Html;
using Web.Core.Frame.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using IFabricRequisitionService = EPYSLSAILORAPP.Application.Interface.Merchandiser.IFabricRequisitionService;

namespace EPYSLSAILORAPP.Controllers.MCD
{
    public class GoodReceiveController : ExtendedBaseController
    {
        private readonly ILogger<GoodReceiveController> _logger;

        private readonly ITranMcdReceiveService _TranMcdReceiveService;


        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private readonly IGenUnitService _GenUnitService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;

        private readonly IConfiguration _configuration;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GoodReceiveController !");
            return View();
        }

        public GoodReceiveController(
           IMapper mapper, ILogger<GoodReceiveController> logger, IHttpContextAccessor contextAccessor,
            ITranMcdReceiveService TranMcdReceiveService, IGenSupplierInformationService GenSupplierInformationService, IGenUnitService GenUnitService, IConfiguration configuration,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranMcdReceiveService = TranMcdReceiveService;


            _GenSupplierInformationService = GenSupplierInformationService;
            _GenUnitService = GenUnitService;

            

            _contextAccessor = contextAccessor;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);

        }

        [HttpGet]
        public async Task<IActionResult> GoodReceiveLanding()
        {

            return View("~/Views/MCD/GoodReceive/GoodReceiveLanding.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> GoodReceiveView(string received_id, DtParameters request)
        {



            string decoded_received_id = clsUtil.DecodeString(received_id);
            var start = request.Start;
            var size = 10;
            request.fiscal_year_id = objFilter.fiscal_year_id;
            request.event_id = objFilter.event_id;
            rpc_tran_mcd_receive_DTO masterData = new rpc_tran_mcd_receive_DTO();
            List<rpc_tran_mcd_receive_detail_DTO> detailData = new List<rpc_tran_mcd_receive_detail_DTO>();

            masterData = await _rpc_db_service.GetAllJoined_TranMcdReceiveAsync(start, size, ActionType.getById.ToString(), Convert.ToInt64(decoded_received_id), 2, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, request.Search);
            detailData = await _rpc_db_service.GetAllJoined_TranMcdReceiveDetailAsync(Convert.ToInt64(decoded_received_id));

            ReceiveViewModel viewModel = new ReceiveViewModel
            {
                MasterData = masterData,
                DetailData = detailData
            };

            return PartialView("~/Views/MCD/GoodReceive/_GoodReceiveView.cshtml", viewModel);

        }


        public async Task<IActionResult> GoodReceiveAcknowledgeView(string received_id, DtParameters request)
        {



            string decoded_received_id = clsUtil.DecodeString(received_id);
            var start = request.Start;
            var size = 10;
            request.fiscal_year_id = objFilter.fiscal_year_id;
            request.event_id = objFilter.event_id;
            rpc_tran_mcd_receive_DTO masterData = new rpc_tran_mcd_receive_DTO();
            List<rpc_tran_mcd_receive_detail_DTO> detailData = new List<rpc_tran_mcd_receive_detail_DTO>();

            masterData = await _rpc_db_service.GetAllJoined_TranMcdReceiveAsync(start, size, ActionType.getById.ToString(), Convert.ToInt64(decoded_received_id), 3, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, request.Search);
            detailData = await _rpc_db_service.GetAllJoined_TranMcdReceiveDetailAsync(Convert.ToInt64(decoded_received_id));

            ReceiveViewModel viewModel = new ReceiveViewModel
            {
                MasterData = masterData,
                DetailData = detailData
            };

            return PartialView("~/Views/MCD/GoodReceive/_GoodReceiveViewAcknowledge.cshtml", viewModel);

        }


        [HttpPost]
        public async Task<IActionResult> GetReceivedData(DtParameters request)
        {
            Int64 receivedID = 0;
            Int64 actionType = 2;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetAllJoined_TranMcdReceiveListAsync(request.Start, request.Length, ActionType.getAll.ToString(), receivedID, actionType, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id , request.Search);

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
                            receive_status_message = Convert.ToInt32(t.receive_status) switch
                            {
                                1 => "Fresh Receive",
                                2 => "Waiting to PO Revise",
                                3 => "Waiting to Modify Receive",
                                _ => "Other Status"
                            },


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewReceiveData(this)' style='width: 97px;font-weight:600px;' class='btn btn-success btnView' style='width: 83px;background: darkgreen;' received_id='" + clsUtil.EncodeString(t.received_id.ToString()) + "'>Acknowledge</button>" +
                            "</div>"
                        }).ToList();


        
           // var totalRows = records.Count;

            var ret_obj = new AjaxResponse(records.Count, data);

            return Json(ret_obj);


        }



        public async Task<IActionResult> GetReceivedAcknowledgementData(DtParameters request, Int64 receivedID)
        {


            Int64 actionType = 3;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetAllJoined_TranMcdReceiveListAsync(request.Start, request.Length, ActionType.getAll.ToString(), receivedID, actionType, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, request.Search);

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
                            "<button type='button' onclick='ViewReceiveDataAcknowledge(this)' style='width: 50px;font-weight:600px;' class='btn btn-secondary btnView'  received_id='" + clsUtil.EncodeString(t.received_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateTranMcdReceive([FromBody] tran_mcd_receive_DTO request)
        {

            var ret = true;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;


            try
            {
                var model = JsonConvert.DeserializeObject<tran_mcd_receive_entity>(JsonConvert.SerializeObject(request));
                request.fiscal_year_id = objFilter.fiscal_year_id;
                request.event_id = objFilter.event_id;
                request.received_id = model.received_id;
                request.is_acknowledged = model.is_acknowledged;

                ret = await _TranMcdReceiveService.AcKnowledgeAsync(model);

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
