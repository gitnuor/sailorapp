using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.CustomIdentityManagers;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.SCM;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class PRAcknowledgementController : ExtendedBaseController
    {
        private readonly ILogger<PRAcknowledgementController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITranPurchaseRequisitionService _TranPurchaseRequisitionService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly ApplicationUserManager<owin_user_DTO> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenUnitService _GenUnitService;
        private HelperService objHelperService;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside PRAcknowledgementController !");
            return View();
        }

        public PRAcknowledgementController(ApplicationUserManager<owin_user_DTO> userManager, IGenSupplierInformationService GenSupplierInformationService,
           IMapper mapper, ILogger<PRAcknowledgementController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration, IGenUnitService GenUnitService,
            ITranPurchaseRequisitionService TranPurchaseRequisitionService, IGenSeasonEventConfigurationService genseasoneventconfigurationservice,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranPurchaseRequisitionService = TranPurchaseRequisitionService;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            
            _GenUnitService = GenUnitService;
            _contextAccessor = contextAccessor;
            _GenSupplierInformationService = GenSupplierInformationService;
        }

        [HttpGet]
        public async Task<IActionResult> PRAcknowledgementLanding()
        {

            return View("~/Views/SCM/PRAcknowledgement/PRAcknowledgementLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> PRAcknowledgementView(string pr_id)
        {

            string decoded_pr_id = clsUtil.DecodeString(pr_id);

            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();

            model = await _TranPurchaseRequisitionService.GetSingleRequisitionAsync(Convert.ToInt64(decoded_pr_id));
           // var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            //var du = await _GenUnitService.GetAsync(Convert.ToInt64(model.delivery_unit_id));
           // model.supplier_name = sup.name.ToString();
           // model.delivery_unit_name = du.FirstOrDefault().unit_name;

            return PartialView("~/Views/SCM/PRAcknowledgement/_PRAcknowledgementView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetPRAcknowledgementPendingData(DtParameters request)
        {

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranPurchaseRequisitionService.GetAllJoined_TranPurchaseRequisitionPendingAckAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.pr_id,
                            t.pr_no,
                            t.pr_date,
                            t.delivery_date,
                            t.event_title,
                            t.designer_name,
                            t.unit_name,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewPRAcknowledgement(this)'  class='btn btn-warning btnView'  pr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }

        public async Task<IActionResult> GetPRAcknowledgementData(DtParameters request)
        {

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranPurchaseRequisitionService.GetAllJoined_TranPurchaseRequisitionAckAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id,request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.pr_id,
                            t.pr_no,
                            t.pr_date,
                            t.delivery_date,
                            t.event_title,
                            t.designer_name,
                            t.unit_name,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewPRAcknowledgement(this)'  class='btn btn-success btnView'  pr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }



        [HttpPost]
        public async Task<IActionResult> SavePRAcknowledgement([FromBody] tran_purchase_requisition_DTO request)
        {
            var ret = true;
            request.added_by = sec_object.userid.Value;
            try
            {
                //request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;

                ret = await _TranPurchaseRequisitionService.ExecuteAcknoledgeAsync(Convert.ToInt64(request.pr_id), Convert.ToInt64(request.added_by));

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

