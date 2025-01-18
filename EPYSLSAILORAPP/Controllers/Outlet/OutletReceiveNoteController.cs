using AutoMapper;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using EPYSLSAILORAPP.Application.DTO;
using Web.Core.Frame.Helpers;
using EPYSLSAILORAPP.Domain.Enum;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Application.DTO.RPC;
using Microsoft.AspNetCore.Mvc.Rendering;
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Serilog.Context;
using System.Security.Claims;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using System.Drawing.Imaging;

namespace EPYSLSAILORAPP.Controllers.Outlet
{
    public class OutletReceiveNoteController : ExtendedBaseController
    {
        private readonly ILogger<OutletReceiveNoteController> _logger;

        private readonly ISubContractRequestService _subContractRequestService;
        private readonly ITranChallanOutletService _TranChallanOutletService;

        private readonly ITranOutletReceiveNoteService _TranOutletReceiveNoteService;

        private readonly IGenUnitService _GenUnitService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;
        private readonly IGenOutletService _gen_outlet_entity_service;
        private readonly IGenTranTransportService _genTranTransportService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside SubContractRequestController !");
            return View();
        }

        public OutletReceiveNoteController(
           IMapper mapper, ILogger<OutletReceiveNoteController> logger, IHttpContextAccessor contextAccessor,
            ISubContractRequestService subContractRequestService, IGenUnitService GenUnitService,
            IRPCDbService rpc_db_service, IConfiguration configuration, ITranChallanOutletService TranChallanOutletService, ITranOutletReceiveNoteService TranOutletReceiveNoteService, IGenTranTransportService GenTranTransportService, IGenOutletService gen_outlet_entity_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _subContractRequestService = subContractRequestService;
            _GenUnitService = GenUnitService;
            
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _gen_outlet_entity_service = gen_outlet_entity_service;
            _genTranTransportService = GenTranTransportService;
            _TranOutletReceiveNoteService = TranOutletReceiveNoteService;
            _TranChallanOutletService = TranChallanOutletService;

        }

        [HttpGet]
        public async Task<IActionResult> OutletChallanReceiveLanding()
        {

            return View("~/Views/Outlet/OutletReceiveNote/OutletChallanReceiveLanding.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> GetTranOutletChallanPendingData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranChallanOutletService.GetAllPagedDataPendingAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_delivery_outlet_challan_id,
                            t.delivery_outlet_challan_date,
                            t.transport_number,
                            t.driver_name,
                            t.delivery_outlet_challan_no,
                            t.outlet_name,
                            t.delivery_from,
                            t.delivery_address,

                            datatablebuttonscode = "<div style='text-align:center;width: 160px;' role='toolbar' >" +
                            "<button type='button' onclick='AddOutletChallanReceive(this)' style='width: 120px;' class='btn btn-primary btnEdit'  tran_delivery_outlet_challan_id='" + clsUtil.EncodeString(t.tran_delivery_outlet_challan_id.ToString()) + "'>create receive note </button>" +
                            

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpGet]
        public async Task<IActionResult> TranOutletChallanReceiveAdd(string tran_delivery_outlet_challan_id, DtParameters request)
        {
            try
            {
                string decoded_delivery_outlet_challan_id = clsUtil.DecodeString(tran_delivery_outlet_challan_id);

                var data = await _TranChallanOutletService.Get_data_tran_delivery_outlet_challan_Async(Convert.ToInt64(decoded_delivery_outlet_challan_id));
                tran_delivery_outlet_challan_DTO model = JsonConvert.DeserializeObject<tran_delivery_outlet_challan_DTO>(JsonConvert.SerializeObject(data));
                model.details = JsonConvert.DeserializeObject<List<tran_delivery_outlet_challan_details_DTO>>(data.tran_delivery_outlet_challan_details_list);

                model.tran_delivery_outlet_challan_id = Convert.ToInt64(decoded_delivery_outlet_challan_id);
                return PartialView("~/Views/Outlet/OutletReceiveNote/_TranOutletChallanReceiveNew.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        [HttpPost]
        public async Task<IActionResult> SaveOutletChallanReceive([FromBody] tran_outlet_receive_note_DTO request)
        {

            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                request.added_by = sec_object.userid.Value;
                request.date_added = DateTime.Now;
            }

            try
            {
                request.event_id = objFilter.event_id;
                request.fiscal_year_id = objFilter.fiscal_year_id;
                request.tran_outlet_receive_note_detail_json = JArray.Parse(JsonConvert.SerializeObject(request.details));

                ret = await _TranOutletReceiveNoteService.tran_outlet_receive_note_insert_sp(request);

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
        public async Task<IActionResult> GetTranOutletChallanReceiveDraftData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranOutletReceiveNoteService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_outlet_receive_note_id,
                            t.outlet_receive_date,
                            t.transport_number,
                            t.driver_name,
                            t.outlet_receive_no,
                            t.outlet_name,
                            t.delivery_from,
                            t.delivery_address,

                            datatablebuttonscode = "<div style='text-align:center;width: 160px;' role='toolbar' >" +
                            "<button type='button' onclick='ViewOutletChallanReceive(this)' style='width: 120px;' class='btn btn-primary btnEdit'  tran_outlet_receive_note_id='" + clsUtil.EncodeString(t.tran_outlet_receive_note_id.ToString()) + "'>View </button>" +


                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpGet]
        public async Task<IActionResult> TranOutletChallanReceiveView(string tran_outlet_receive_note_id, DtParameters request)
        {
            try
            {
                string decoded_tran_outlet_receive_note_id = clsUtil.DecodeString(tran_outlet_receive_note_id);

                var data = await _TranOutletReceiveNoteService.GetAllJoined_TranOutletReceiveNoteAsync(Convert.ToInt64(decoded_tran_outlet_receive_note_id));
                tran_outlet_receive_note_DTO model = JsonConvert.DeserializeObject<tran_outlet_receive_note_DTO>(JsonConvert.SerializeObject(data));
                model.details = JsonConvert.DeserializeObject<List<tran_outlet_receive_note_details_DTO>>(data.tran_outlet_receive_note_details_list);

                model.tran_outlet_receive_note_id = Convert.ToInt64(decoded_tran_outlet_receive_note_id);
                return PartialView("~/Views/Outlet/OutletReceiveNote/_TranOutletChallanReceiveView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }



    }
}
