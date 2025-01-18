using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class BillApprovalController : ExtendedBaseController
    {
        private readonly ILogger<BillApprovalController> _logger;


        private readonly ITranScmBillSubmissionService _tranScmBillSubmissionService;

        private readonly IConfiguration _configuration;
        private readonly IBillApprovalService _BillApprovalService;
        private readonly ITranScmPoService _TranScmPoService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside BillApprovalController !");
            return View();
        }

        public BillApprovalController(
           IMapper mapper, ILogger<BillApprovalController> logger, IHttpContextAccessor contextAccessor,
           ITranScmPoService TranScmPoService, IGenSupplierInformationService GenSupplierInformationService,
            IBillApprovalService BillApprovalService,
            IRPCDbService rpc_db_service, ITranScmBillSubmissionService tranScmBillSubmissionService, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _tranScmBillSubmissionService = tranScmBillSubmissionService;

            _TranScmPoService = TranScmPoService;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _BillApprovalService = BillApprovalService;
            _GenSupplierInformationService = GenSupplierInformationService;
            

            _contextAccessor = contextAccessor;
            _tranScmBillSubmissionService = tranScmBillSubmissionService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> BillApprovalLanding()
        {

            return View("~/Views/SCM/BillApproval/BillApprovalLanding.cshtml");
        }


        [HttpGet]
        public async Task<IActionResult> BillApprovalView(string bill_submission_id)
        {

            string decoded_bill_submission_id = clsUtil.DecodeString(bill_submission_id);

            tran_scm_bill_submission_DTO model = new tran_scm_bill_submission_DTO();
            tran_scm_po_DTO po_model = new tran_scm_po_DTO();
            model = await _tranScmBillSubmissionService.GetSingleAsync(Convert.ToInt64(decoded_bill_submission_id));
            po_model = await _TranScmPoService.GetSingleAsync(Convert.ToInt64(model.po_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            model.supplier_name = sup.name;
            model.List_Files = po_model.List_Files;
            model.po_details = po_model.List_po_details;
            model.po_date = po_model.po_date;
            model.po_no = po_model.po_no;
            model.po_approved_date = po_model.approved_date;

            return PartialView("~/Views/SCM/BillApproval/_BillApprovalView.cshtml", model);

        }
        [HttpPost]
        public async Task<IActionResult> GetPendingBillData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var action_type = 1;

            var records = await _tranScmBillSubmissionService.GetAll_Bill_Submission_Async(request.fiscal_year_id, request.event_id, action_type, request.Start, request.Length,request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.bill_submission_id,
                            t.po_id,
                            t.po_no,
                            t.supplier_id,
                            t.supplier_name,
                            t.bill_no,
                            t.challan_no,
                            t.challan_date,
                            t.bill_date,
                            t.total_po_amount,
                            t.loading_cost_in_percentage,
                            t.loading_cost,
                            t.transport_cost_in_percentage,
                            t.transport_cost,
                            t.discount_in_percentage,
                            t.discount_amount,
                            t.vat_in_percentage,
                            t.vat_amount,
                            t.total_value,
                            t.is_submitted,
                            t.submitted_date,
                            t.submitted_by,
                            t.is_approved,
                            t.approved_date,
                            t.approved_by,
                            t.status,
                            t.documents,
                            t.terms_conditions,
                            t.remarks,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,
                            t.delivery_unit_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewBillApproval(this)'  class='btn btn-warning btnView'  bill_submission_id='" + clsUtil.EncodeString(t.bill_submission_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" 
                            
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }


        public async Task<IActionResult> GetApprovalBillData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var action_type = 2;

            var records = await _tranScmBillSubmissionService.GetAll_Bill_Submission_Async(request.fiscal_year_id, request.event_id, action_type, request.Start, request.Length, request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.bill_submission_id,
                            t.po_id,
                            t.po_no,
                            t.supplier_id,
                            t.supplier_name,
                            t.bill_no,
                            t.challan_no,
                            t.challan_date,
                            t.bill_date,
                            t.total_po_amount,
                            t.loading_cost_in_percentage,
                            t.loading_cost,
                            t.transport_cost_in_percentage,
                            t.transport_cost,
                            t.discount_in_percentage,
                            t.discount_amount,
                            t.vat_in_percentage,
                            t.vat_amount,
                            t.total_value,
                            t.is_submitted,
                            t.submitted_date,
                            t.submitted_by,
                            t.is_approved,
                            t.approved_date,
                            t.approved_by,
                            t.status,
                            t.documents,
                            t.terms_conditions,
                            t.remarks,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,
                            t.delivery_unit_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewBillApproval(this)'  class='btn btn-success btnView'  bill_submission_id='" + clsUtil.EncodeString(t.bill_submission_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }


        [HttpPost]
        public async Task<IActionResult> UpdateBillApproval([FromBody] tran_scm_bill_submission_DTO request)
        {
            var ret = true;


            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

            request.approved_by = sec_object.userid.Value;
            request.approved_date = DateTime.Now;

            
            try
            {
                var model = JsonConvert.DeserializeObject<tran_scm_bill_submission_entity>(JsonConvert.SerializeObject(request));


                ret = await _tranScmBillSubmissionService.Update_Bill_Approval_Async(model);

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
        public async Task<IActionResult> DeleteBillApproval([FromBody] tran_scm_bill_submission_DTO request)
        {

            var ret = true;


            {


                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

            }

            try
            {

                string decoded_bill_submission_id = clsUtil.DecodeString(request.strMasterID);

                request.bill_submission_id = Convert.ToInt64(decoded_bill_submission_id);

                ret = await _tranScmBillSubmissionService.DeleteAsync(request.bill_submission_id.Value);

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

