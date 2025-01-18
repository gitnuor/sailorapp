using AutoMapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Context;
using ServiceStack;
using System.Data.Entity.Infrastructure;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class TranReturnChallanController : ExtendedBaseController
    {
        private readonly ILogger<TranReturnChallanController> _logger;
        private readonly IGenTranTransportService _genTranTransportService;
        private readonly ITranReturnChallanService _TranReturnChallanService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;
        private readonly IGenOutletService _gen_outlet_entity_service;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside Return Challan Controller !");
            return View();
        }

        public TranReturnChallanController(
           IMapper mapper, ILogger<TranReturnChallanController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration, IGenTranTransportService GenTranTransportService,
            ITranReturnChallanService TranReturnChallanService, IGenOutletService gen_outlet_entity_service,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _genTranTransportService = GenTranTransportService;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranReturnChallanService = TranReturnChallanService;
            _configuration = configuration;
            

            _gen_outlet_entity_service = gen_outlet_entity_service;

        }

        [HttpGet]
        public async Task<IActionResult> TranReturnChallanLanding()
        {

            return View("~/Views/Outlet/TranReturnChallan/TranReturnChallanLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranReturnChallanNew(string tran_outlet_receive_note_id)
        {
            string decode_tran_outlet_receive_note_id = clsUtil.DecodeString(tran_outlet_receive_note_id);

            tran_return_challan_DTO model = new tran_return_challan_DTO();
            model=await _TranReturnChallanService.GetOutletReceiveData(Convert.ToInt64(decode_tran_outlet_receive_note_id));
            model.return_to_name = "Sailor Factory";
            model.return_to = 1;
            List<gen_tran_transport_entity> transportTypeList = await _genTranTransportService.GetAllAsync();
            List<gen_outlet_DTO> outletList = await _gen_outlet_entity_service.GetAllAsync();
            model.TranReturnChallanDetails_List = JsonConvert.DeserializeObject<List<tran_return_challan_details_DTO>>(model.receive_details);
            ViewBag.outletList =
                outletList.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.outlet_name.ToString(),
                        Value = a.outlet_id.ToString()
                    }).ToList();
            ViewBag.transportTypeList =
                transportTypeList.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.transport_type.ToString(),
                        Value = a.transport_id.ToString()
                    }).ToList();
            return PartialView("~/Views/Outlet/TranReturnChallan/_TranReturnChallanNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranReturnChallanEdit(string tran_return_challan_id)
        {

            string decoded_tran_return_challan_id = clsUtil.DecodeString(tran_return_challan_id);

            tran_return_challan_DTO model = new tran_return_challan_DTO();

            var objmodel = await _TranReturnChallanService.GetOutletChallanReturnData(Convert.ToInt64(decoded_tran_return_challan_id));

            model = JsonConvert.DeserializeObject<tran_return_challan_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Outlet/TranReturnChallan/_TranReturnChallanEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranReturnChallanView(string tran_return_challan_id)
        {

            string decoded_tran_return_challan_id = clsUtil.DecodeString(tran_return_challan_id);

            tran_return_challan_DTO model = new tran_return_challan_DTO();

             model = await _TranReturnChallanService.GetOutletChallanReturnData(Convert.ToInt64(decoded_tran_return_challan_id));

            model.TranReturnChallanDetails_List = JsonConvert.DeserializeObject<List<tran_return_challan_details_DTO>>(model.receive_details);

            return PartialView("~/Views/Outlet/TranReturnChallan/_TranReturnChallanView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetTranReturnChallanData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranReturnChallanService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,                         
                            t.return_no,
                            t.return_date,                         
                            t.transport_type_name,
                            t.driver_name,
                            t.driver_contact_no,                          
                            t.tran_return_challan_details_json,
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +                           
                            "<button type='button' onclick='ViewTranReturnChallan(this)'  class='btn btn-primary btnView'  tran_return_challan_id='" + clsUtil.EncodeString(t.tran_return_challan_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></button>" +
                         
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetPrendingReturnData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranReturnChallanService.GetPrendingReturnData(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_outlet_receive_note_id,
                            t.outlet_receive_no,
                            t.driver_contact,
                            t.transport_type_name,
                            t.outlet_receive_date,
                            t.driver_name,
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='AddTranReturnChallan(this)'  class='btn btn-success btnView'  tran_outlet_receive_note_id='" + clsUtil.EncodeString(t.tran_outlet_receive_note_id.ToString()) + "'><i class='fa fa-undo' aria-hidden='true'></i></button>" +
                          
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }



        [HttpPost]
        public async Task<IActionResult> SaveTranReturnChallan([FromBody] tran_return_challan_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;
            request.event_id = objFilter.event_id;
            request.fiscal_year_id = objFilter.fiscal_year_id;



            try
            {
                request.tran_return_challan_details_json = JArray.Parse(JsonConvert.SerializeObject(request.TranReturnChallanDetails_List));


                ret = await _TranReturnChallanService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateTranReturnChallan([FromBody] tran_return_challan_DTO request)
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
                var model = JsonConvert.DeserializeObject<tran_return_challan_DTO>(JsonConvert.SerializeObject(request));

                ret = await _TranReturnChallanService.UpdateAsync(model);

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
        public async Task<IActionResult> DeleteTranReturnChallan([FromBody] tran_return_challan_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;



            try
            {

                string decoded_tran_return_challan_id = clsUtil.DecodeString(request.strMasterID);

                request.tran_return_challan_id = Convert.ToInt64(decoded_tran_return_challan_id);

                ret = await _TranReturnChallanService.DeleteAsync(request.tran_return_challan_id);

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

