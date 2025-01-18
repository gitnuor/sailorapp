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
using ServiceStack;

namespace EPYSLSAILORAPP.Controllers.ShopFloor
{
    public class FinishingProductionProcessController : ExtendedBaseController
    {
        private readonly ILogger<FinishingProductionProcessController> _logger;

        private readonly ITranFinishingReceiveService _TranFinishingReceiveService;
        private readonly IFinishingProductionProcessService _TranFinishingProductionProcessService;
        
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

        public FinishingProductionProcessController(
           IMapper mapper, ILogger<FinishingProductionProcessController> logger, IHttpContextAccessor contextAccessor,
            IFinishingProductionProcessService TranFinishingProductionProcessService, IGenUnitService genunitService,
            IRPCDbService rpc_db_service, ITranFinishingReceiveService TranFinishingReceiveService, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenunitService = genunitService;
            _TranFinishingReceiveService = TranFinishingReceiveService;
            _TranFinishingProductionProcessService= TranFinishingProductionProcessService;
            // _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> FinishingProductionProcessLanding()
        {
            var techpackname = _TranFinishingProductionProcessService.GetAllTechpackList();

            var distinctTechpackList = techpackname
                                    .GroupBy(t => t.techpack_number)
                                    .Select(g => g.First()) // This ensures distinct items based on techpack_number
                                    .ToList();

            ViewBag.techpackname = distinctTechpackList.ToList().Distinct()
            .Select(a => new SelectListItem
            {
                Text = a.techpack_number,
                Value = a.techpack_id.ToString(),
            }).ToList();

            return View("~/Views/ShopFloor/FinishingProductionProcess/FinishingProductionProcessLanding.cshtml");
        }

        [HttpGet]
        public IActionResult GetTabContent(int tabIndex)
        {
            // Here, you would retrieve the specific content based on the tab index
            // For demonstration, let's just return some simple content
            if (tabIndex == 0)
            {
                return PartialView("_TabContent0");
            }
            else if (tabIndex == 1)
            {
                var data = "5";
                return PartialView("~/Views/ShopFloor/FinishingProductionProcess/_TabContent1.cshtml",data);
               // return PartialView("_TabContent1");
            }
            else
            {
                return PartialView("_TabContentDefault", tabIndex);
            }
        }


        [HttpGet]
        public async Task<IActionResult> TranFinishingProductionAdd(string techpack_id, DtParameters request)
        {

          //  string tran_techpack_id = clsUtil.DecodeString(techpack_id);
            string tran_techpack_id = techpack_id;
            var start = request.Start;
            var size = 10;
            rpc_tran_finishing_production_DTO model = new rpc_tran_finishing_production_DTO();

            model = await _TranFinishingProductionProcessService.GetnFinishingProductionListByIdAsync(start, size, Convert.ToInt64(tran_techpack_id), ActionType.getById.ToString(), request.fiscal_year_id, request.event_id);

            var finishing_process_name = _TranFinishingReceiveService.GetAllFinishingProcessType();

            ViewBag.finishing_process_name = finishing_process_name.ToList()
            .Select(a => new SelectListItem
            {
                Text = a.finishing_process_name,
                Value = a.gen_finishing_process_id.ToString(),
            }).ToList();

            return PartialView("~/Views/ShopFloor/FinishingProductionProcess/_TranFinishingProductionAdd.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> TranFinishingProductionAdd2(string techpack_id, DtParameters request,Int64 gen_finishing_process_id)
        {

            //  string tran_techpack_id = clsUtil.DecodeString(techpack_id);
            string tran_techpack_id = techpack_id;
            var start = request.Start;
            var size = 10;
            tran_finishing_production_process_DTO model = new tran_finishing_production_process_DTO();
             model=   await _TranFinishingProductionProcessService.GetnFinishingProductionReceiveListByTechpacAsync(request.Start, size, Convert.ToInt64(tran_techpack_id), ActionType.getById.ToString(), request.fiscal_year_id, request.event_id, gen_finishing_process_id);

            model.details = JsonConvert.DeserializeObject<List<tran_finishing_production_process_details_DTO>>(model.tran_finishing_production_process_details_list);
            

            //var finishing_process_name = _TranFinishingReceiveService.GetAllFinishingProcessType();

            //ViewBag.finishing_process_name = finishing_process_name.ToList()
            //.Select(a => new SelectListItem
            //{
            //    Text = a.finishing_process_name,
            //    Value = a.gen_finishing_process_id.ToString(),
            //}).ToList();

            return PartialView("~/Views/ShopFloor/FinishingProductionProcess/_TranFinishingProductionAddSecondStep.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> TranFinishingProductionView(string techpack_id, TechPack_DtParameters request,string activeTag)
        {

            string tran_techpack_id = techpack_id; //clsUtil.DecodeString(techpack_id);
            var start = request.Start;
            var size = 10;
            List<tran_finishing_production_process_DTO> model = new List<tran_finishing_production_process_DTO>();
            model= await _TranFinishingProductionProcessService.GetnFinishingProductionListView(Convert.ToInt64(tran_techpack_id), activeTag);
            //var data = await _TranFinishingProductionProcessService.GetnFinishingProductionListView(Convert.ToInt64(tran_techpack_id));
            // tran_finishing_production_process_DTO model = JsonConvert.DeserializeObject<tran_finishing_production_process_DTO>(JsonConvert.SerializeObject(data));
             //model.details = JsonConvert.DeserializeObject<List<tran_finishing_production_process_details_DTO>>(data.tran_finishing_production_process_details_list);

            return PartialView("~/Views/ShopFloor/FinishingProductionProcess/_TranFinishingProductionView.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> TranFinishingProductionProcessTab(string techpack_id, DtParameters request)      
        {

            rpc_tran_finishing_receive_DTO model = new rpc_tran_finishing_receive_DTO();

           var data  = await _TranFinishingProductionProcessService.GetnFinishingProductionProcessTab(Convert.ToInt64(techpack_id));
            //var serializedData = JsonConvert.SerializeObject(data.tran_finishing_production_process_type);

            model.details_process = JsonConvert.DeserializeObject<List<FinishingProcessWrapper>>(data.tran_finishing_production_process_type);

            return PartialView("~/Views/ShopFloor/FinishingProductionProcess/_TabDetails.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetFinishingProductionProcessPendingData_2nd_process(TechPack_DtParameters request)
        {
            Int64 finishing_receive_id = 0;

            //var records = await _TranFinishingProductionProcessService.GetnFinishingProductionPendingListAsync(request.Start, request.Length, finishing_receive_id, ActionType.getAll.ToString(), request.fiscal_year_id, request.event_id);

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;


            var records = await _TranFinishingProductionProcessService.GetnFinishingProductionPendingListByTechpackAsync(request.Start, request.Length, request.techpack_id,request.gen_finishing_process_id, ActionType.getById.ToString(), request.fiscal_year_id, request.event_id);



            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_finish_receive_id,
                            t.techpack_id,
                            t.tran_finish_receive_date,
                            t.techpack_number,
                            t.style_item_product_id,
                            t.style_item_product_category,
                            t.finishing_process_name,
                            t.production_process_date,
                            t.tran_finishing_production_process_id,




                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='viewdetailbtn1(this)' style='width:120px' class='btn btn-warning btnView'  tran_finishing_production_process_id='" + t.tran_finishing_production_process_id.ToString() + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpGet]
        public async Task<ActionResult> AddDetailColorWise(long techpack_id)
        {
            rpc_tran_finishing_production_DTO model = new rpc_tran_finishing_production_DTO();
           
            ViewBag.Colors = _TranFinishingProductionProcessService.GetAllproc_sp_get_colors_by_techpack_Id(techpack_id)
                     .Select(a => a.color_code.ToString())
                     .Distinct()
                     .Select(colorCode => new SelectListItem
                     {
                         Text = colorCode,
                         Value = colorCode
                     })
                     .ToList();


            return PartialView("~/Views/ShopFloor/FinishingProductionProcess/_FinishingProductionDetails.cshtml", model);
        }

        [HttpGet]
        public async Task<JsonResult> GetDetaiilsReceiveProduction(long techpack_id, string colorCode , Int64? finishingProcessId)
        {

            var data = await _TranFinishingProductionProcessService.GetAll_finishing_production_techpack_color_wiseAsync(techpack_id, colorCode , finishingProcessId);

            return Json(new { data = data });
        }

        [HttpGet]
        public async Task<JsonResult> GetDetaiilsProductionIdWise(long finishing_production_process_id)
        {

            var data = await _TranFinishingProductionProcessService.GetDetaiilsProductionIdWise(finishing_production_process_id);

            return Json(new { data = data });
        }

        [HttpPost]
        public async Task<IActionResult> SaveFinishingProduction([FromBody] tran_finishing_production_process_DTO request)
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
                request.tran_finishing_production_process_details = JArray.Parse(JsonConvert.SerializeObject(request.details));

                ret = await _TranFinishingProductionProcessService.tran_finishing_production_process_insert_sp(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Data Saved Successfully." : "Data Saving Failed")
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
