using AutoMapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class FloorReturnAcknowledgementController : ExtendedBaseController
    {
        private readonly ILogger<FloorReturnAcknowledgementController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IShopFloorReturnService _ShopFloorReturnService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside FloorReturnAcknowledgementController !");
            return View();
        }

        public FloorReturnAcknowledgementController(
           IMapper mapper, ILogger<FloorReturnAcknowledgementController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            IShopFloorReturnService ShopFloorReturnService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor; _configuration = configuration;
            _ShopFloorReturnService = ShopFloorReturnService; _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> FloorReturnAcknowledgementLanding()
        {

            return View("~/Views/MCD/FloorReturnAcknowledgement/FloorReturnAcknowledgementLanding.cshtml");
        }



        [HttpGet]
        public async Task<IActionResult> FloorReturnView(string tran_shop_floor_return_id)
        {

            string decoded_tran_shop_floor_return_id = clsUtil.DecodeString(tran_shop_floor_return_id);

            tran_shop_floor_return_DTO model = new tran_shop_floor_return_DTO();

            model = await _ShopFloorReturnService.GetSingleAsyncByReturnId(Convert.ToInt64(decoded_tran_shop_floor_return_id));
            model.tran_shop_floor_return_id = Convert.ToInt64(decoded_tran_shop_floor_return_id);
            model.details = JsonConvert.DeserializeObject<List<tran_shop_floor_return_details_DTO>>(model.tran_return_detail_list);
            return PartialView("~/Views/MCD/FloorReturnAcknowledgement/_FloorReturnAcknowledgementView.cshtml", model);

        }
        [HttpGet]
        public async Task<IActionResult> FloorReturnEdit(string tran_shop_floor_return_id)
        {

            string decoded_tran_shop_floor_return_id = clsUtil.DecodeString(tran_shop_floor_return_id);

            tran_shop_floor_return_DTO model = new tran_shop_floor_return_DTO();

            model = await _ShopFloorReturnService.GetSingleAsyncByReturnId(Convert.ToInt64(decoded_tran_shop_floor_return_id));
            model.tran_shop_floor_return_id = Convert.ToInt64(decoded_tran_shop_floor_return_id);
            model.details = JsonConvert.DeserializeObject<List<tran_shop_floor_return_details_DTO>>(model.tran_return_detail_list);
            return PartialView("~/Views/MCD/FloorReturnAcknowledgement/_FloorReturnAcknowledgementEdit.cshtml", model);

        }





        [HttpPost]
        public async Task<IActionResult> GetApprovedShopFloorReturnData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _ShopFloorReturnService.GetApprovedShopFloorReturnData(request.Start, request.Length, request.fiscal_year_id, request.event_id,request.Search.Value);
            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_shop_floor_return_id,
                            t.return_no,
                            t.requisition_slip_no,
                            t.issue_no,
                            t.techpack_number,
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewShopFloorReturn(this)'  class='btn btn-warning btnView'  tran_shop_floor_return_id='" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetSubmittedShopFloorReturnData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _ShopFloorReturnService.GetSubmittedShopFloorReturnData(request.Start, request.Length, request.fiscal_year_id, request.event_id,request.Search.Value);
            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_shop_floor_return_id,
                            t.return_no,
                            t.requisition_slip_no,
                            t.issue_no,
                            t.techpack_number,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewShopFloorReturn(this)'  class='btn btn-warning btnView'  tran_shop_floor_return_id='" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }



        [HttpPost]
        public async Task<IActionResult> UpdateShopFloorReturn([FromBody] tran_shop_floor_return_DTO request)
        {
            var ret = true;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;


            try
            {
                var model = JsonConvert.DeserializeObject<tran_shop_floor_return_entity>(JsonConvert.SerializeObject(request));
                List<tran_shop_floor_return_details_entity> details = JsonConvert.DeserializeObject<List<tran_shop_floor_return_details_entity>>(JsonConvert.SerializeObject(request.details));

                model.acknowledged_date = request.date_updated;
                model.acknowledged_by = request.updated_by;
                model.is_acknowledged = request.is_acknowledged;
                if (model.is_acknowledged == 2)
                {
                    model.is_submitted = 1;

                }

                ret = await _ShopFloorReturnService.AckonwledgementUpdate(model);

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
        public async Task<IActionResult> Acknowledge([FromBody] tran_shop_floor_return_DTO request)
        {
            var ret = true;
            request.updated_by = sec_object.userid.Value;
            request.date_updated = DateTime.Now;
            try
            {

                request.acknowledged_date = request.date_updated;
                request.acknowledged_by = request.updated_by;
                request.is_acknowledged = request.is_acknowledged;

                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;


                ret = await _ShopFloorReturnService.Ackonwledge(request);

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

