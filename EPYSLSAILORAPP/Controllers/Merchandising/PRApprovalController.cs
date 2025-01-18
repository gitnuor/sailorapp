using AutoMapper;
using EPYSLSAILORAPP.Application.CustomIdentityManagers;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using ServiceStack;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class PRApprovalController : ExtendedBaseController
    {
        private readonly ILogger<PRApprovalController> _logger;
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
            _logger.LogInformation("Hello from inside PRApprovalController !");
            return View();
        }

        public PRApprovalController(ApplicationUserManager<owin_user_DTO> userManager, IGenSupplierInformationService GenSupplierInformationService,
           IMapper mapper, ILogger<PRApprovalController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration, IGenUnitService GenUnitService,
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
        public async Task<IActionResult> PRApprovalLanding()
        {

            return View("~/Views/MerchandiserMgt/PRApproval/PRApprovalLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> PRApprovalView(string pr_id)
        {

            string decoded_pr_id = clsUtil.DecodeString(pr_id);

            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();

            model = await _TranPurchaseRequisitionService.GetSingleRequisitionAsync(Convert.ToInt64(decoded_pr_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            var du = await _GenUnitService.GetAsync(Convert.ToInt64(model.delivery_unit_id));
            model.supplier_name = sup.name.ToString();
            model.delivery_unit_name = du.FirstOrDefault().unit_name;

            return PartialView("~/Views/MerchandiserMgt/PRApproval/_PRApprovalView.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> DraftPRApprovalView(string dpr_id)
        {

            string decoded_pr_id = clsUtil.DecodeString(dpr_id);

            tran_purchase_requisition_DTO model = new tran_purchase_requisition_DTO();

            model = await _TranPurchaseRequisitionService.GetSingleRequisitionAsync(Convert.ToInt64(decoded_pr_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            var du = await _GenUnitService.GetAsync(Convert.ToInt64(model.delivery_unit_id));
            model.supplier_name = sup.name.ToString();
            model.delivery_unit_name = du.FirstOrDefault().unit_name;

            return PartialView("~/Views/MerchandiserMgt/PRApproval/_DraftPRApprovalView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetPRApprovalData(DtParameters request)
        {




            var records = await _TranPurchaseRequisitionService.GetAllTranPurchaseRequisitionMerchantPendingApprovalAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, request.Search.Value);

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

                            "<button type='button' onclick='ViewPRApproval(this)'  class='btn btn-success btnView'  pr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-check' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }


        [HttpPost]
        public async Task<IActionResult> GetDraftPRApprovalData(DtParameters request)
        {


            var records = await _TranPurchaseRequisitionService.GetAllJoined_TranPurchaseRequisitionMerchandise_Approved(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, request.Search.Value);

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

                            "<button type='button' onclick='ViewDraftPRApproval(this)'  class='btn btn-success btnView'  dpr_id='" + clsUtil.EncodeString(t.pr_id.ToString()) + "'><i class='fa fa-check' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }


        [HttpPost]
        public async Task<IActionResult> ApproveDraftPR([FromBody] tran_draft_purchase_requisition_DTO request)
        {


            var ret = true;


            {




                if (request.dpr_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;
                }

            }

            try
            {


                ret = await _TranPurchaseRequisitionService.ExecutePrApprovalAsync(Convert.ToInt64(request.dpr_id));

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Success" : "Data Saving Failed")
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
        public async Task<IActionResult> SavePRApproval([FromBody] tran_purchase_requisition_DTO request)
        {

            var ret = true;

            if (request.pr_id == null)
            {
                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;
            }

            try
            {

                ret = await _TranPurchaseRequisitionService.ExecutePrApprovalAsync(Convert.ToInt64(request.pr_id));

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Success" : "Data Saving Failed")
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

