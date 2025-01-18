using AutoMapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Context;
using ServiceStack;
using System.Collections.Generic;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class TranReturnChallanReceivedController : ExtendedBaseController
    {
        private readonly ILogger<TranReturnChallanReceivedController> _logger;

        private readonly ITranReturnChallanReceivedService _TranReturnChallanReceivedService;
        private readonly ITranReturnChallanService _TranReturnChallanService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside Return Challan Received Controller !");
            return View();
        }

        public TranReturnChallanReceivedController(
           IMapper mapper, ILogger<TranReturnChallanReceivedController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            ITranReturnChallanReceivedService TranReturnChallanReceivedService, ITranReturnChallanService TranReturnChallanService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _TranReturnChallanService = TranReturnChallanService;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranReturnChallanReceivedService = TranReturnChallanReceivedService;
            _configuration = configuration;
            



        }

        [HttpGet]
        public async Task<IActionResult> TranReturnChallanReceivedLanding()
        {

            return View("~/Views/PMC/TranReturnChallanReceived/TranReturnChallanReceivedLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranReturnChallanReceivedNew(string tran_return_challan_id)
        {

            string decoded_tran_return_challan_id = clsUtil.DecodeString(tran_return_challan_id);

            tran_return_challan_received_DTO model = new tran_return_challan_received_DTO();

            model = await _TranReturnChallanReceivedService.GetReturnChallanData(Convert.ToInt64(decoded_tran_return_challan_id));

            model.TranReturnChallanDetails_List = JsonConvert.DeserializeObject<List<tran_return_challan_details_DTO>>(model.receive_details);

          


            

            return PartialView("~/Views/PMC/TranReturnChallanReceived/_TranReturnChallanReceivedNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranReturnChallanReceivedEdit(string tran_return_challan_received_id)
        {

            string decoded_tran_return_challan_received_id = clsUtil.DecodeString(tran_return_challan_received_id);

            tran_return_challan_received_DTO model = new tran_return_challan_received_DTO();

            var objmodel = await _TranReturnChallanReceivedService.GetSingleAsync(Convert.ToInt64(decoded_tran_return_challan_received_id));

            model = JsonConvert.DeserializeObject<tran_return_challan_received_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/PMC/TranReturnChallanReceived/_TranReturnChallanReceivedEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranReturnChallanReceivedView(string tran_return_challan_received_id)
        {

            string decoded_tran_return_challan_id = clsUtil.DecodeString(tran_return_challan_received_id);

            tran_return_challan_received_DTO model = new tran_return_challan_received_DTO();

            model = await _TranReturnChallanReceivedService.GetSingleAsync(Convert.ToInt64(decoded_tran_return_challan_id));

            model.TranReturnChallanDetails_List = JsonConvert.DeserializeObject<List<tran_return_challan_details_DTO>>(model.return_challan_receive_details_json);



            return PartialView("~/Views/PMC/TranReturnChallanReceived/_TranReturnChallanReceivedView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetTranReturnChallanReceivedData(DtParameters request)
        {

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranReturnChallanReceivedService.GetAllReturnChallanReceivedAsync(request.fiscal_year_id, request.event_id, request.Start, request.Length);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_return_challan_received_id,
                            t.tran_return_challan_id,
                            t.return_receive_no,
                            t.return_receive_date,
                            t.transport_type,
                            t.return_no,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,
                            t.fiscal_year_id,
                            t.event_id,
                            t.tran_return_challan_receive_details_json,
                            t.driver_name,
                            t.driver_contact_no,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewTranReturnChallanReceived(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_return_challan_received_id='" + clsUtil.EncodeString(t.tran_return_challan_received_id.ToString()) + "'>View</button>" +
                           
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }
        [HttpPost]
        public async Task<IActionResult> GetTranReturnChallanData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranReturnChallanReceivedService.GetAllPendingReturnChallanReceivedAsync(request.fiscal_year_id, request.event_id, request.Start, request.Length);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_return_challan_id,
                            t.return_no,
                            t.return_date,
                            t.transport_type,
                            t.driver_name,
                            t.driver_contact_no,

                            //datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            //"<button type='button' onclick='ViewTranReturnChallan(this)'  class='btn btn-primary btnView'  tran_return_challan_id='" + clsUtil.EncodeString(t.tran_return_challan_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></button>" +

                            //"</div>"


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='AddTranReturnChallanReceived(this)'  class='btn btn-primary '  tran_return_challan_id='" + clsUtil.EncodeString(t.tran_return_challan_id.ToString()) + "'>Receive</button>" +

                            "</div>"





                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }
        //[HttpPost]
        // public async Task<IActionResult> GetJoinedTranReturnChallanReceivedData(DtParameters request)
        // {

        //     var records = await _TranReturnChallanReceivedService.GetAllJoined_TranReturnChallanReceivedAsync( request.Start,request.Length);

        //     var index = request.Start + 1;
        //     var data = (from t in records
        //                 select new
        //                 {
        //                     row_index = index++,
        //                     			t.return_receive_no,


        //                   datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
        //                     "<button type='button' onclick='EditTranReturnChallanReceived(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  tran_return_challan_received_id='" + clsUtil.EncodeString(t.tran_return_challan_received_id.ToString()) + "'>Edit</button>" +
        //                     "<button type='button' onclick='ViewTranReturnChallanReceived(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_return_challan_received_id='" + clsUtil.EncodeString(t.tran_return_challan_received_id.ToString()) + "'>View</button>"+
        //"<button type='button' onclick='DeleteTranReturnChallanReceived(\""+ clsUtil.EncodeString(t.tran_return_challan_received_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
        //"</div>"

        //                 }).ToList();
        //     var ret_obj = new AjaxResponse(records.Count, data);
        //     return Json(ret_obj);

        // }




        [HttpPost]
        public async Task<IActionResult> SaveTranReturnChallanReceived([FromBody] tran_return_challan_received_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;


            try
            {
                var model = JsonConvert.DeserializeObject<tran_return_challan_received_DTO>(JsonConvert.SerializeObject(request));

               


                model.tran_return_challan_receive_details_json = JsonConvert.SerializeObject(model.TranReturnChallanReceiveDetails_List);

                ret = await _TranReturnChallanReceivedService.tran_return_challan_received_insert_sp(model);

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
        public async Task<IActionResult> UpdateTranReturnChallanReceived([FromBody] tran_return_challan_received_DTO request)
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
                var model = JsonConvert.DeserializeObject<tran_return_challan_received_DTO>(JsonConvert.SerializeObject(request));

                ret = await _TranReturnChallanReceivedService.UpdateAsync(model);

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
        public async Task<IActionResult> DeleteTranReturnChallanReceived([FromBody] tran_return_challan_received_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            try
            {

                string decoded_tran_return_challan_received_id = clsUtil.DecodeString(request.strMasterID);

                request.tran_return_challan_received_id = Convert.ToInt64(decoded_tran_return_challan_received_id);

                ret = await _TranReturnChallanReceivedService.DeleteAsync(request.tran_return_challan_received_id);

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

