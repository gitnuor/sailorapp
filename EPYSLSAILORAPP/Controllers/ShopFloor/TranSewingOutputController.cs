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

using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;

namespace EPYSLSAILORAPP.Controllers
{
    public class TranSewingOutputController : ExtendedBaseController
    {
        private readonly ILogger<TranSewingOutputController> _logger;

        private readonly ITranSewingOutputService _TranSewingOutputService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside TranSewingOutputController !");
            return View();
        }

        public TranSewingOutputController(
           IMapper mapper, ILogger<TranSewingOutputController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            ITranSewingOutputService TranSewingOutputService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranSewingOutputService = TranSewingOutputService;

            

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> TranSewingOutputLanding()
        {

            return View("~/Views/ShopFloor/TranSewingOutput/TranSewingOutputLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranSewingOutputNew(string tran_sewing_input_id)
        {
            tran_sewing_output_DTO model = new tran_sewing_output_DTO();
            long input_id = Convert.ToInt64(clsUtil.DecodeString(tran_sewing_input_id));


            rpc_sewing_input_data_for_output_DTO dataset = await _TranSewingOutputService.GetSewingOutputDataByInputId(input_id);

            model.tran_sewing_allocation_plan_id = dataset.tran_sewing_allocation_plan_id;
            model.tran_sewing_input_id = input_id;
            model.techpack_id = dataset.techpack_id;
            model.techpack_number = dataset.techpack_number;
            model.merchandiser_name = dataset.merchandiser_name;
            model.style_item_product_category = dataset.style_item_product_category;
            ViewBag.LineNumber = JsonConvert.DeserializeObject<List<gen_production_line_entity>>(dataset.ddl_line).Select(a =>
           new SelectListItem
           {
               Text = a.line_name.ToString(),
               Value = a.production_line_id.ToString()
           }
           ).ToList();
            ViewBag.HourOutput = await _TranSewingOutputService.GetAllHourList();

            return PartialView("~/Views/ShopFloor/TranSewingOutput/_TranSewingOutputNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranSewingOutputEdit(string tran_sewing_output_id)
        {

            string decoded_tran_sewing_output_id = clsUtil.DecodeString(tran_sewing_output_id);

            tran_sewing_output_DTO model = new tran_sewing_output_DTO();

            var objmodel = await _TranSewingOutputService.GetSingleAsync(Convert.ToInt64(decoded_tran_sewing_output_id));

            model = JsonConvert.DeserializeObject<tran_sewing_output_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/ShopFloor/TranSewingOutput/_TranSewingOutputEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranSewingOutputView(string tran_sewing_output_id)
        {

            string decoded_tran_sewing_output_id = clsUtil.DecodeString(tran_sewing_output_id);

            tran_sewing_output_DTO model = new tran_sewing_output_DTO();

            var objmodel = await _TranSewingOutputService.GetSingleAsync(Convert.ToInt64(decoded_tran_sewing_output_id));

            model = JsonConvert.DeserializeObject<tran_sewing_output_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/ShopFloor/TranSewingOutput/_TranSewingOutputView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetTranSewingOutputtedData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranSewingOutputService.GetTranSewingOutputtedData(request.Start, request.Length, request.fiscal_year_id, request.event_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_sewing_output_id,
                            t.tran_sewing_input_id,
                            t.tran_sewing_allocation_plan_id,
                            t.techpack_id,
                            t.output_date,
                            t.techpack_number,
                            t.hour_output,
                            t.fiscal_year_id,
                            t.event_id,
                            t.sewing_allocation_number,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditTranSewingOutput(this)'  class='btn btn-warning btnEdit'  tran_sewing_output_id='" + clsUtil.EncodeString(t.tran_sewing_output_id.ToString()) + "'><i class=\"fa fa-eye\" aria-hidden=\"true\"></i></button>" +

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }




        [HttpPost]
        public async Task<IActionResult> SaveTranSewingOutput([FromBody] tran_sewing_output_DTO request)
        {
            
           

            
            var ret = true;

            
            {
               


                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;
                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;
            }

            try
            {
               

                ret = await _TranSewingOutputService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateTranSewingOutput([FromBody] tran_sewing_output_DTO request)
        {
            var ret = true;

            

            
            {
                

                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

                request.updated_by = sec_object.userid.Value;

                request.date_updated = DateTime.Now;
                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;
            }

            try
            {
                var model = JsonConvert.DeserializeObject<tran_sewing_output_entity>(JsonConvert.SerializeObject(request));

                ret = await _TranSewingOutputService.UpdateAsync(model);

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
        public async Task<IActionResult> DeleteTranSewingOutput([FromBody] tran_sewing_output_DTO request)
        {
           

            var ret = true;

           
            {
               
                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

            }

            try
            {

                string decoded_tran_sewing_output_id = clsUtil.DecodeString(request.strMasterID);

                request.tran_sewing_output_id = Convert.ToInt64(decoded_tran_sewing_output_id);

                ret = await _TranSewingOutputService.DeleteAsync(request.tran_sewing_output_id);

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


        [HttpPost]
        public async Task<IActionResult> GetAllSewingOutputPending(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranSewingOutputService.GetAllSewingOutputPendingAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.sewing_allocation_number,
                            t.techpack_number,
                            t.merchandiser_name,
                            t.sewing_allocation_date,
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='AddTranSewingOutput(this)'  class='btn btn-success btnView'  tran_sewing_input_id='" + clsUtil.EncodeString(t.tran_sewing_input_id.ToString()) + "'><i class='fa fa-puzzle-piece' aria-hidden='true'></i></button>" +

                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }
        [HttpGet]
        public async Task<JsonResult> AddLine(long tran_sewing_input_id, long production_line_id)
        {
            var pr = await _TranSewingOutputService.AddLine(tran_sewing_input_id, production_line_id);

            return Json(new { data = pr });
        }

        [HttpGet]
        public async Task<IActionResult> QCFailedDetails()
        {
            List<qc_failed_details_DTO> model = new List<qc_failed_details_DTO>();


            model = await _TranSewingOutputService.GetAllQCParam();




            return PartialView("~/Views/ShopFloor/TranSewingOutput/_QCFailedDetails.cshtml", model);

        }


    }
}

