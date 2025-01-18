using AutoMapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Context;
using ServiceStack;
using System.Collections.Generic;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class TranDistributionPlanController : ExtendedBaseController
    {
        private readonly ILogger<TranDistributionPlanController> _logger;

        private readonly ITranDistributionPlanService _TranDistributionPlanService;
        private readonly ITrantechpackService _TechpackService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;
        private readonly IGenOutletService _gen_outlet_entity_service;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside TranDistributionPlanController !");
            return View();
        }

        public TranDistributionPlanController(
           IMapper mapper, ILogger<TranDistributionPlanController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            ITranDistributionPlanService TranDistributionPlanService, ITrantechpackService trantechpackService, IGenOutletService gen_outlet_entity_service,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _TechpackService = trantechpackService;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranDistributionPlanService = TranDistributionPlanService;
            _configuration = configuration;
            
            _gen_outlet_entity_service = gen_outlet_entity_service;


        }

        [HttpGet]
        public async Task<IActionResult> TranDistributionPlanLanding()
        {

            return View("~/Views/PMC/TranDistributionPlan/TranDistributionPlanLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranDistributionPlanNew()
        {
            tran_distribution_plan_DTO model = new tran_distribution_plan_DTO();
            model.distributed_by_name = sec_object.userfullname;
            List<rpc_proc_sp_get_data_techpack_for_distribution_DTO> result = await _TechpackService.GetAllproc_sp_get_data_techpack_for_distributionAsync(objFilter.fiscal_year_id, objFilter.event_id);
            model.techpacks = result.Select(a =>
                    new SelectListItem
                    {
                        Text = a.techpack_number.ToString(),
                        Value = a.techpack_id.ToString()
                    }).ToList();

            return PartialView("~/Views/PMC/TranDistributionPlan/_TranDistributionPlanNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranDistributionPlanEdit(string tran_distribution_plan_id)
        {

            string decoded_tran_distribution_plan_id = clsUtil.DecodeString(tran_distribution_plan_id);

            tran_distribution_plan_DTO model = new tran_distribution_plan_DTO();

            var objmodel = await _TranDistributionPlanService.GetSingleAsync(Convert.ToInt64(decoded_tran_distribution_plan_id));

            model = JsonConvert.DeserializeObject<tran_distribution_plan_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/PMC/TranDistributionPlan/_TranDistributionPlanEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranDistributionPlanView(string tran_distribution_plan_id)
        {

            string decoded_tran_distribution_plan_id = clsUtil.DecodeString(tran_distribution_plan_id);

            tran_distribution_plan_DTO model = new tran_distribution_plan_DTO();

            var objmodel = await _TranDistributionPlanService.GetSingleAsync(Convert.ToInt64(decoded_tran_distribution_plan_id));

            model = JsonConvert.DeserializeObject<tran_distribution_plan_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/PMC/TranDistributionPlan/_TranDistributionPlanView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetTranDistributionPlanData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranDistributionPlanService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.distribution_no,t.distribution_date,
                            t.name,
                            t.event_title,
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewTranDistributionPlan(this)'  class='btn btn-secondary btnView'  tran_distribution_plan_id='" + clsUtil.EncodeString(t.tran_distribution_plan_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='DeleteTranDistributionPlan(\"" + clsUtil.EncodeString(t.tran_distribution_plan_id.ToString()) + "\")' class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);


         
        }

        [HttpPost]
        public async Task<IActionResult> SaveTranDistributionPlan([FromBody] tran_distribution_plan_DTO request)
        {
            var ret = true;
            request.added_by = sec_object.userid.Value;
            request.date_added = DateTime.Now;
            request.submitted_date = DateTime.Now;
            request.distributed_by = sec_object.userid.Value;
            request.event_id = objFilter.event_id;
            request.fiscal_year_id=objFilter.fiscal_year_id;
            request.is_submitted = 2;
            request.submitted_by = sec_object.userid.Value;

            foreach (tran_distribution_plan_details_DTO plan in request.TranDistributionPlanDetails_List)
            {
                plan.tran_distribution_plan_outlet_details =JArray.Parse(JsonConvert.SerializeObject(plan.TranDistributionPlanOutletDetails_List));
            }
            try
            {
                request.tran_distribution_plan_details_json=JArray.Parse(JsonConvert.SerializeObject(request.TranDistributionPlanDetails_List));

                ret = await _TranDistributionPlanService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateTranDistributionPlan([FromBody] tran_distribution_plan_DTO request)
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


                ret = await _TranDistributionPlanService.UpdateAsync(request);

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
        public async Task<IActionResult> DeleteTranDistributionPlan([FromBody] tran_distribution_plan_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;



            try
            {

                string decoded_tran_distribution_plan_id = clsUtil.DecodeString(request.strMasterID);

                request.tran_distribution_plan_id = Convert.ToInt64(decoded_tran_distribution_plan_id);

                ret = await _TranDistributionPlanService.DeleteAsync(request.tran_distribution_plan_id);

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
        public async Task<JsonResult> GetDistributionByTechpack(long p_techpack_id)
        {


            List<tran_distribution_plan_details_DTO> objmodel = await _TranDistributionPlanService.GetDistributionByTechpack(p_techpack_id);
            return Json(new { data = objmodel });
        }
        [HttpGet]
        public async Task<IActionResult> OutletDetails()
        {

            List<gen_outlet_DTO> outletList = await _gen_outlet_entity_service.GetAllAsync();

            List<tran_distribution_plan_outlet_details_DTO> outletDetailsList = outletList.Select(o => new tran_distribution_plan_outlet_details_DTO
            {
                outlet_id = o.outlet_id,
                outlet_name = o.outlet_name,
                received_quantity = 0
            }).ToList();
            return PartialView("~/Views/PMC/TranDistributionPlan/_OutleDetails.cshtml", outletDetailsList);

        }
    }
}

