using AutoMapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using Web.Core.Frame.Helpers;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
namespace EPYSLSAILORAPP.Controllers
{
    public class TranFinishProductReceiveController : ExtendedBaseController
    {
        private readonly ILogger<TranFinishProductReceiveController> _logger;

        private readonly ITranFinishProductReceiveService _TranFinishProductReceiveService;
        private readonly IGenTranTransportService _genTranTransportService;
        private readonly ITranPackingListService _TranPackingListService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside TranFinishProductReceiveController !");
            return View();
        }

        public TranFinishProductReceiveController(
           IMapper mapper, ILogger<TranFinishProductReceiveController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration, ITranPackingListService TranPackingListService,
            ITranFinishProductReceiveService TranFinishProductReceiveService, IGenTranTransportService GenTranTransportService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _genTranTransportService = GenTranTransportService;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranFinishProductReceiveService = TranFinishProductReceiveService;
            _configuration = configuration;
            
            _TranPackingListService = TranPackingListService;


        }

        [HttpGet]
        public async Task<IActionResult> TranFinishProductReceiveLanding()
        {

            return View("~/Views/PMC/TranFinishProductReceive/TranFinishProductReceiveLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranFinishProductReceiveNew()
        {
            tran_finish_product_receive_DTO model = new tran_finish_product_receive_DTO();
            List<gen_tran_transport_entity> transportTypeList = await _genTranTransportService.GetAllAsync();

            ViewBag.transportTypeList =
                transportTypeList.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.transport_type.ToString(),
                        Value = a.transport_id.ToString()
                    }).ToList();

            return PartialView("~/Views/PMC/TranFinishProductReceive/_TranFinishProductReceiveNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranFinishProductReceiveEdit(string tran_finish_product_receive_id)
        {

            string decoded_tran_finish_product_receive_id = clsUtil.DecodeString(tran_finish_product_receive_id);

            tran_finish_product_receive_DTO model = new tran_finish_product_receive_DTO();

            var objmodel = await _TranFinishProductReceiveService.GetSingleAsync(Convert.ToInt64(decoded_tran_finish_product_receive_id));

            model = JsonConvert.DeserializeObject<tran_finish_product_receive_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/PMC/TranFinishProductReceive/_TranFinishProductReceiveEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranFinishProductReceiveView(string tran_finish_product_receive_id)
        {

            string decoded_tran_finish_product_receive_id = clsUtil.DecodeString(tran_finish_product_receive_id);

            tran_finish_product_receive_DTO model = new tran_finish_product_receive_DTO();

            model = await _TranFinishProductReceiveService.GetSingleAsync(Convert.ToInt64(decoded_tran_finish_product_receive_id));

            model.TranFinishProductReceiveDetails_List = JsonConvert.DeserializeObject<List<tran_finish_product_receive_details_DTO>>(model.finish_details);

            return PartialView("~/Views/PMC/TranFinishProductReceive/_TranFinishProductReceiveView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetTranFinishProductReceiveData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranFinishProductReceiveService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_finish_product_receive_id,
                      
                            t.finish_product_receive_no,
                            t.finish_product_receive_date,
                    
                            t.vehicle_number,
                            t.driver_name,
                           


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditTranFinishProductReceive(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  tran_finish_product_receive_id='" + clsUtil.EncodeString(t.tran_finish_product_receive_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewTranFinishProductReceive(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_finish_product_receive_id='" + clsUtil.EncodeString(t.tran_finish_product_receive_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteTranFinishProductReceive(\"" + clsUtil.EncodeString(t.tran_finish_product_receive_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetJoinedTranFinishProductReceiveData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranFinishProductReceiveService.GetAllJoined_TranFinishProductReceiveAsync(request.Start, request.Length,request.fiscal_year_id,request.event_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.finish_product_receive_no,
                            t.vehicle_type,
                            t.vehicle_number,
                            t.driver_name,
                            t.driver_contact_no,
                            t.note,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditTranFinishProductReceive(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  tran_finish_product_receive_id='" + clsUtil.EncodeString(t.tran_finish_product_receive_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewTranFinishProductReceive(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_finish_product_receive_id='" + clsUtil.EncodeString(t.tran_finish_product_receive_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteTranFinishProductReceive(\"" + clsUtil.EncodeString(t.tran_finish_product_receive_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }




        [HttpPost]
        public async Task<IActionResult> SaveTranFinishProductReceive([FromBody] tran_finish_product_receive_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.fiscal_year_id = objFilter.fiscal_year_id;
            request.event_id= objFilter.event_id;


            try
            {

                request.tran_finish_product_receive_details_json = JsonConvert.SerializeObject(request.TranFinishProductReceiveDetails_List);
                ret = await _TranFinishProductReceiveService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateTranFinishProductReceive([FromBody] tran_finish_product_receive_DTO request) 
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


                ret = await _TranFinishProductReceiveService.UpdateAsync(request);

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
        public async Task<IActionResult> DeleteTranFinishProductReceive([FromBody] tran_finish_product_receive_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;



            try
            {

                string decoded_tran_finish_product_receive_id = clsUtil.DecodeString(request.strMasterID);

                request.tran_finish_product_receive_id = Convert.ToInt64(decoded_tran_finish_product_receive_id);

                ret = await _TranFinishProductReceiveService.DeleteAsync(request.tran_finish_product_receive_id);

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
        public async Task<JsonResult> GetPackingDetails(long tran_packing_list_id)
        {


            rpc_proc_sp_get_data_tran_packing_list_by_id_DTO objmodel = await _TranPackingListService.GetPackingListForPMC(tran_packing_list_id);

            var details = JsonConvert.DeserializeObject<List<tran_packing_list_details_DTO>>(objmodel.packing_details);
            return Json(new { data = details });
        }
    }
}

