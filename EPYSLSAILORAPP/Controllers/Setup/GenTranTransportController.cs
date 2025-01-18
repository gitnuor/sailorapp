using AutoMapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class GenTranTransportController : ExtendedBaseController
    {
        private readonly ILogger<GenTranTransportController> _logger;

        private readonly IGenTranTransportService _GenTranTransportService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GenTranTransportController !");
            return View();
        }

        public GenTranTransportController(
           IMapper mapper, ILogger<GenTranTransportController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            IGenTranTransportService GenTranTransportService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenTranTransportService = GenTranTransportService;
            _configuration = configuration;
            



        }

        [HttpGet]
        public async Task<IActionResult> GenTranTransportLanding()
        {

            return View("~/Views/Setup/GenTranTransport/GenTranTransportLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GenTranTransportNew()
        {
            gen_tran_transport_DTO model = new gen_tran_transport_DTO();

            return PartialView("~/Views/Setup/GenTranTransport/_GenTranTransportNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenTranTransportEdit(string transport_id)
        {

            string decoded_transport_id = clsUtil.DecodeString(transport_id);

            gen_tran_transport_DTO model = new gen_tran_transport_DTO();

            var objmodel = await _GenTranTransportService.GetSingleAsync(Convert.ToInt64(decoded_transport_id));

            model = JsonConvert.DeserializeObject<gen_tran_transport_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Setup/GenTranTransport/_GenTranTransportEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenTranTransportView(string transport_id)
        {

            string decoded_transport_id = clsUtil.DecodeString(transport_id);

            gen_tran_transport_DTO model = new gen_tran_transport_DTO();

            var objmodel = await _GenTranTransportService.GetSingleAsync(Convert.ToInt64(decoded_transport_id));

            model = JsonConvert.DeserializeObject<gen_tran_transport_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Setup/GenTranTransport/_GenTranTransportView.cshtml", model);

        }


        //[HttpPost]
        //public async Task<IActionResult> GetGenTranTransportData(DtParameters request)
        //{

        //    var records = await _GenTranTransportService.GetAllPagedDataAsync(request);

        //    var index = request.Start + 1;
        //    var data = (from t in records
        //                select new
        //                {
        //                    row_index = index++,
        //                    t.transport_id,
        //                    t.transport_type,


        //                    datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
        //                    "<button type='button' onclick='EditGenTranTransport(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  transport_id='" + clsUtil.EncodeString(t.transport_id.ToString()) + "'>Edit</button>" +
        //                    "<button type='button' onclick='ViewGenTranTransport(this)' style='width: 120px;' class='btn btn-secondary btnView'  transport_id='" + clsUtil.EncodeString(t.transport_id.ToString()) + "'>View</button>" +
        //                    "<button type='button' onclick='DeleteGenTranTransport(\"" + clsUtil.EncodeString(t.transport_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
        //                    "</div>"
        //                }).ToList();
        //    var ret_obj = new AjaxResponse(records.Count, data);
        //    return Json(ret_obj);

        //}





        [HttpPost]
        public async Task<IActionResult> SaveGenTranTransport([FromBody] gen_tran_transport_DTO request)
        {


            var ret = true;


     




            try
            {
                var model = JsonConvert.DeserializeObject<gen_tran_transport_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenTranTransportService.SaveAsync(model);

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
        public async Task<IActionResult> UpdateGenTranTransport([FromBody] gen_tran_transport_DTO request)
        {
            var ret = true;





            try
            {
                var model = JsonConvert.DeserializeObject<gen_tran_transport_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenTranTransportService.UpdateAsync(model);

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
        public async Task<IActionResult> DeleteGenTranTransport([FromBody] gen_tran_transport_DTO request)
        {


            var ret = true;


         


            try
            {

                string decoded_transport_id = clsUtil.DecodeString(request.strMasterID);

                request.transport_id = Convert.ToInt64(decoded_transport_id);

                ret = await _GenTranTransportService.DeleteAsync(request.transport_id);

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

