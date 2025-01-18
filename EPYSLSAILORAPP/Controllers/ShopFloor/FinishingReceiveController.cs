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
using Npgsql;
using Dapper;


namespace EPYSLSAILORAPP.Controllers.ShopFloor
{
    public class FinishingReceiveController  : ExtendedBaseController
    {
        private readonly ILogger<FinishingReceiveController> _logger;

        private readonly ITranFinishingReceiveService _TranFinishingReceiveService;
        private readonly ITranServiceWorkOrderService _TranServiceWorkOrderService;
        private readonly ITranEmbellishReceiveService _TranEmbellishReceiveService;

        private readonly IGenUnitService _GenunitService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside TranServiceWorkOrderController !");
            return View();
        }

        public FinishingReceiveController(
           IMapper mapper, ILogger<FinishingReceiveController> logger, IHttpContextAccessor contextAccessor,
            ITranServiceWorkOrderService TranServiceWorkOrderService, IGenUnitService genunitService,
            IRPCDbService rpc_db_service, ITranFinishingReceiveService TranFinishingReceiveService, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenunitService = genunitService;
            _TranServiceWorkOrderService = TranServiceWorkOrderService;
            _TranFinishingReceiveService = TranFinishingReceiveService;
            // _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);

            
            _configuration = configuration;
            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> FinishingReceiveLanding()
        {

            return View("~/Views/ShopFloor/FinishingReceive/FinishingReceiveLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranFinishingReceiveAdd(string techpack_id, DtParameters request)
        {

            string tran_techpack_id = clsUtil.DecodeString(techpack_id);
            var start = request.Start;
            var size = 10;
            rpc_tran_finishing_receive_DTO model = new rpc_tran_finishing_receive_DTO();
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            model = await _TranFinishingReceiveService.GetnFinishingReceiveListByIdAsync(start, size, Convert.ToInt64(tran_techpack_id), ActionType.getById.ToString(), request.fiscal_year_id, request.event_id);

            var finishing_process_name =  _TranFinishingReceiveService.GetAllFinishingProcessType();

            ViewBag.finishing_process_name = finishing_process_name.ToList()
            .Select(a => new SelectListItem
            {
                Text = a.finishing_process_name,
                Value = a.gen_finishing_process_id.ToString(),
            }).ToList();

            return PartialView("~/Views/ShopFloor/FinishingReceive/_TranFinishingReceiveAdd.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetFinishingReceivePendingData(DtParameters request)
        {
            Int64 finishing_receive_id = 0;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranFinishingReceiveService.GetnFinishingReceivePendingListAsync(request.Start, request.Length, finishing_receive_id, ActionType.getAll.ToString(), request.fiscal_year_id, request.event_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                           
                            t.tran_sewing_output_id,
                            t.techpack_id,
                            t.output_date,
                            t.merchandiser_name,
                            t.techpack_number,
                            t.style_item_product_id,
                            t.style_item_product_category,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='TranFinishingReceiveAdd(this)' style='width:140px' class='btn btn-success btnEdit'  techpack_id='" + clsUtil.EncodeString(t.techpack_id.ToString()) + "'>Create Finish Receive</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }


        [HttpPost]
        public async Task<IActionResult> GetFinishingReceiveDraftData(DtParameters request)
        {
            Int64 actionType = 1;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranFinishingReceiveService.GetFinishingReceive_DraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_finish_receive_id,
                            t.tran_finish_receive_no,
                            t.tran_finish_receive_date,
                            t.techpack_number,
                            t.style_item_product_category,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewFinishingReceiveDetails(this)' style='width:140px' class='btn btn-success btnEdit'  tran_finish_receive_id='" + clsUtil.EncodeString(t.tran_finish_receive_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }




        [HttpGet]
        public async Task<ActionResult> AddreceiveDetailNew(long techpack_id)
        {
            rpc_tran_finishing_receive_DTO model = new rpc_tran_finishing_receive_DTO();
            //ViewBag.LineNumber = _GenProductionLineService.GetSingleLineByAsync(2).Select(a =>
            //     new SelectListItem
            //     {
            //         Text = a.line_name.ToString(),
            //         Value = a.production_line_id.ToString()
            //     }
            //     ).ToList();

            ViewBag.Colors = _TranFinishingReceiveService.GetAllproc_sp_get_colors_by_sewing_output_Id(techpack_id)
                     .Select(a => a.color_code.ToString())
                     .Distinct()
                     .Select(colorCode => new SelectListItem
                     {
                         Text = colorCode,
                         Value = colorCode
                     })
                     .ToList();


            return PartialView("~/Views/ShopFloor/FinishingReceive/_FinishingReceiveDetails.cshtml", model);
        }

        [HttpGet]
        public async Task<JsonResult> GetDetaiilsReceive(long techpack_id, string colorCode)
        {

            var data = await _TranFinishingReceiveService.GetAll_finishing_receive_techpack_wiseAsync(techpack_id,colorCode);

            return Json(new { data = data });
        }

        [HttpPost]
        public async Task<IActionResult> SaveFinishingReceive([FromBody] tran_finishing_receive_DTO request)
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
                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;
                request.tran_finish_receive_details = JArray.Parse(JsonConvert.SerializeObject(request.details)).ToString();
                request.finishing_process_name = JArray.Parse(JsonConvert.SerializeObject(request.finishingProcessdetails)).ToString();

                ret = await _TranFinishingReceiveService.tran_finishing_receive_insert_sp(request);

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


        [HttpGet]
        public async Task<IActionResult> FinishingProcessView(string id)
        {

            string decoded_po_id = clsUtil.DecodeString(id);

            tran_finishing_receive_DTO model = new tran_finishing_receive_DTO();

            model = await _TranFinishingReceiveService.GetSingleAsync(Convert.ToInt64(decoded_po_id));

            model.finishingProcessdetails = JsonConvert.DeserializeObject<List<tran_finishing_process_DTO>>(model.finishing_process_name);

            model.details = JsonConvert.DeserializeObject<List<tran_finishing_receive_details_DTO>>(model.finish_receive_details);

            return PartialView("~/Views/ShopFloor/FinishingReceive/_FinishingReceiveDetailsView.cshtml", model);

        }





    }
}
