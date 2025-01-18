using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Numerics;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class SewingAllocationPlanController : ExtendedBaseController
    {
        private readonly ILogger<SewingAllocationPlanController> _logger;

        private readonly ISewingAllocationPlanService _SewingAllocationPlanService;
        private readonly ITrantechpackService _TechpackService;
        private readonly ICuttingService _CuttingService;

        private readonly IConfiguration _configuration;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenProductionLineService _GenProductionLineService;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside SewingAllocationPlanController !");
            return View();
        }

        public SewingAllocationPlanController(
           IMapper mapper, ILogger<SewingAllocationPlanController> logger, IHttpContextAccessor contextAccessor, ICuttingService CuttingService,
           ITrantechpackService trantechpackService, IGenProductionLineService GenProductionLineService,
            ISewingAllocationPlanService SewingAllocationPlanService,
            IRPCDbService rpc_db_service, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _SewingAllocationPlanService = SewingAllocationPlanService;
            _TechpackService = trantechpackService;
            _CuttingService = CuttingService;
            _GenProductionLineService = GenProductionLineService;
            

            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> SewingAllocationPlanLanding()
        {

            return View("~/Views/ShopFloor/SewingAllocationPlan/SewingAllocationPlanLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> SewingAllocationPlanNew()
        {
            tran_sewing_allocation_plan_DTO model = new tran_sewing_allocation_plan_DTO();


            return PartialView("~/Views/ShopFloor/SewingAllocationPlan/_SewingAllocationPlanNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> SewingAllocationPlanEdit(string tran_sewing_allocation_plan_id)
        {

            string decoded_tran_sewing_allocation_plan_id = clsUtil.DecodeString(tran_sewing_allocation_plan_id);

            tran_sewing_allocation_plan_DTO model = new tran_sewing_allocation_plan_DTO();

            var objmodel = await _SewingAllocationPlanService.GetSingleAsync(Convert.ToInt64(decoded_tran_sewing_allocation_plan_id));

            model = JsonConvert.DeserializeObject<tran_sewing_allocation_plan_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/ShopFloor/SewingAllocationPlan/_SewingAllocationPlanEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> SewingAllocationPlanView(string tran_sewing_allocation_plan_id)
        {

            string decoded_tran_sewing_allocation_plan_id = clsUtil.DecodeString(tran_sewing_allocation_plan_id);

            tran_sewing_allocation_plan_DTO model = new tran_sewing_allocation_plan_DTO();

            var objmodel = await _SewingAllocationPlanService.GetSingleAsync(Convert.ToInt64(decoded_tran_sewing_allocation_plan_id));

            model = JsonConvert.DeserializeObject<tran_sewing_allocation_plan_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/ShopFloor/SewingAllocationPlan/_SewingAllocationPlanView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetSewingAllocationPlanData(DtParameters request)
        {

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _SewingAllocationPlanService.GetAllPagedDataAsync(request.fiscal_year_id, request.event_id, request.Start, request.Length);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_sewing_allocation_plan_id,
                            t.sewing_allocation_number,
                            t.techpack_number,
                            t.sewing_allocation_date,
                            t.merchandiser_name,

                            t.style_item_product_category,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewSewingAllocationPlan(this)' class='btn btn-warning btnView'  tran_sewing_allocation_plan_id='" + clsUtil.EncodeString(t.tran_sewing_allocation_plan_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='DeleteSewingAllocationPlan(\"" + clsUtil.EncodeString(t.tran_sewing_allocation_plan_id.ToString()) + "\")'  class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }






        [HttpPost]
        public async Task<IActionResult> SaveSewingAllocationPlan([FromBody] sewing_plan_insert_dto request)
        {


            var ret = true;

            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.fiscal_year_id = objFilter.fiscal_year_id;
            request.event_id = objFilter.event_id;


            try
            {

                ret = await _SewingAllocationPlanService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateSewingAllocationPlan([FromBody] tran_sewing_allocation_plan_DTO request)
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
                var model = JsonConvert.DeserializeObject<tran_sewing_allocation_plan_entity>(JsonConvert.SerializeObject(request));

                ret = await _SewingAllocationPlanService.UpdateAsync(model);

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
        public async Task<IActionResult> DeleteSewingAllocationPlan([FromBody] tran_sewing_allocation_plan_DTO request)
        {


            var ret = true;


            {


                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

            }

            try
            {

                string decoded_tran_sewing_allocation_plan_id = clsUtil.DecodeString(request.strMasterID);

                request.tran_sewing_allocation_plan_id = Convert.ToInt64(decoded_tran_sewing_allocation_plan_id);

                ret = await _SewingAllocationPlanService.DeleteAsync(request.tran_sewing_allocation_plan_id);

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
        public async Task<JsonResult> GetTechpackDet(long techpack_id)
        {
            var pr = await _TechpackService.GetAllproc_sp_get_techapack_details_for_sewingAsync(techpack_id);

            return Json(new { data = pr });
        }
        [HttpGet]
        public async Task<ActionResult> AddPlanNew(long techpack_id)
        {
            tran_sewing_detail_plan_DTO model = new tran_sewing_detail_plan_DTO();
            ViewBag.LineNumber = _GenProductionLineService.GetSingleLineByAsync(2).Select(a =>
                 new SelectListItem
                 {
                     Text = a.line_name.ToString(),
                     Value = a.production_line_id.ToString()
                 }
                 ).ToList();

            ViewBag.Colors = _TechpackService.GetAllproc_sp_get_colors_by_techpackAsync(techpack_id).Select(a =>
                 new SelectListItem
                 {
                     Text = a.color_code.ToString(),
                     Value = a.color_code.ToString()
                 }
                 ).ToList();
            return PartialView("~/Views/ShopFloor/SewingAllocationPlan/_PlanDetails.cshtml", model);
        }


        [HttpGet]
        public async Task<IActionResult> GetColorWiseBatch(long techpack_id, string p_color_code)
        {

            List<tran_cutting_batch_dropdown> records = await _CuttingService.GetAllproc_sp_get_color_wise_batchAsync(techpack_id, p_color_code);


            foreach (tran_cutting_batch_dropdown bs in records)
            {
                bs.batch_summery = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_size_summery>>(bs.batch_summery_string);

            }

            return Json(records);
        }

    }
}

