using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class TranSewingInputController : ExtendedBaseController
    {
        private readonly ILogger<TranSewingInputController> _logger;

        private readonly ITranSewingInputService _TranSewingInputService;
        private readonly ISewingAllocationPlanService _SewingAllocationPlanService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside Sewing Input Controller !");
            return View();
        }

        public TranSewingInputController(
           IMapper mapper, ILogger<TranSewingInputController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            ITranSewingInputService TranSewingInputService, ISewingAllocationPlanService SewingAllocationPlanService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranSewingInputService = TranSewingInputService;
            _SewingAllocationPlanService = SewingAllocationPlanService;
            
              
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
        }

        [HttpGet]
        public async Task<IActionResult> TranSewingInputLanding()
        {

            return View("~/Views/ShopFloor/TranSewingInput/TranSewingInputLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranSewingInputNew(string tran_sewing_allocation_plan_id)
        {
           long sap_id = Convert.ToInt64(clsUtil.DecodeString(tran_sewing_allocation_plan_id));

           tran_sewing_input_DTO model = new tran_sewing_input_DTO();
            rpc_sewing_allocation_data_DTO dataset =await  _SewingAllocationPlanService.GetSewingInputDataByAllocationId(sap_id);

            model.tran_sewing_allocation_plan_id = dataset.tran_sewing_allocation_plan_id;
            model.sewing_allocation_number = dataset.sewing_allocation_number;
            model.sewing_allocation_date = dataset.sewing_allocation_date;
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

            return PartialView("~/Views/ShopFloor/TranSewingInput/_TranSewingInputNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranSewingInputEdit(string tran_sewing_input_id)
        {

            string decoded_tran_sewing_input_id = clsUtil.DecodeString(tran_sewing_input_id);

            tran_sewing_input_DTO model = new tran_sewing_input_DTO();

            var objmodel = await _TranSewingInputService.GetSingleAsync(Convert.ToInt64(decoded_tran_sewing_input_id));

            model = JsonConvert.DeserializeObject<tran_sewing_input_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/ShopFloor/TranSewingInput/_TranSewingInputEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranSewingInputView(string tran_sewing_input_id)
        {

            string decoded_tran_sewing_input_id = clsUtil.DecodeString(tran_sewing_input_id);

            tran_sewing_input_DTO model = new tran_sewing_input_DTO();

            var objmodel = await _TranSewingInputService.GetSingleAsync(Convert.ToInt64(decoded_tran_sewing_input_id));

            model = JsonConvert.DeserializeObject<tran_sewing_input_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/ShopFloor/TranSewingInput/_TranSewingInputView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetTranSewingInputData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranSewingInputService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_sewing_input_id,
                            t.tran_sewing_allocation_plan_id,
                            t.techpack_id,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditTranSewingInput(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  tran_sewing_input_id='" + clsUtil.EncodeString(t.tran_sewing_input_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewTranSewingInput(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_sewing_input_id='" + clsUtil.EncodeString(t.tran_sewing_input_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteTranSewingInput(\"" + clsUtil.EncodeString(t.tran_sewing_input_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetAllPendingSewingInput(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranSewingInputService.GetAllPendingSewingInputAsync(request.Start, request.Length,request.fiscal_year_id,request.event_id);

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
                            "<button type='button' onclick='AddTranSewingInput(this)' class='btn btn-success'  tran_sewing_allocation_plan_id='" + clsUtil.EncodeString(t.tran_sewing_allocation_plan_id.ToString()) + "'><i class='fa fa-scissors' aria-hidden='true'></i></button>" +
                         
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }
        [HttpPost]
        public async Task<IActionResult> GetAllSewingInputted(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranSewingInputService.GetAllSewingInputtedAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id);

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
                           
                            "<button type='button' onclick='ViewTranSewingInput(this)'  class='btn btn-warning btnView'  tran_sewing_input_id='" + clsUtil.EncodeString(t.tran_sewing_input_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }





        [HttpPost]
        public async Task<IActionResult> SaveTranSewingInput([FromBody] tran_sewing_input_DTO request)
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
                ret = await _TranSewingInputService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateTranSewingInput([FromBody] tran_sewing_input_DTO request)
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
                var model = JsonConvert.DeserializeObject<tran_sewing_input_entity>(JsonConvert.SerializeObject(request));

                ret = await _TranSewingInputService.UpdateAsync(model);

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
        public async Task<IActionResult> DeleteTranSewingInput([FromBody] tran_sewing_input_DTO request)
        {
            

            var ret = true;

           
            {
                

                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

            }

            try
            {

                string decoded_tran_sewing_input_id = clsUtil.DecodeString(request.strMasterID);

                request.tran_sewing_input_id = Convert.ToInt64(decoded_tran_sewing_input_id);

                ret = await _TranSewingInputService.DeleteAsync(request.tran_sewing_input_id);

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
        public async Task<JsonResult> AddLine(long tran_sewing_allocation_plan_id,long production_line_id)
        {
            var pr = await _SewingAllocationPlanService.AddLine(tran_sewing_allocation_plan_id, production_line_id);

            return Json(new { data = pr });
        }

    }
}

