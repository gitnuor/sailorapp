using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.RPC;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Enum;
using EPYSLSAILORAPP.Domain.RPC;
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
    public class BillSubmissionController : ExtendedBaseController
    {
        private readonly ILogger<BillSubmissionController> _logger;


        private readonly ITranScmBillSubmissionService _tranScmBillSubmissionService;

        private readonly ITranScmPoService _tramscmpo;
        private readonly IConfiguration _configuration;

        private readonly ITranScmPoService _TranScmPoService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside BillSubmissionController !");
            return View();
        }

        public BillSubmissionController(
           IMapper mapper, ILogger<BillSubmissionController> logger, IHttpContextAccessor contextAccessor, ITranScmPoService TranScmPoService, IGenSupplierInformationService GenSupplierInformationService,
            IRPCDbService rpc_db_service, ITranScmBillSubmissionService tranScmBillSubmissionService, ITranScmPoService tramscmpo, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _tranScmBillSubmissionService = tranScmBillSubmissionService;
            _tramscmpo = tramscmpo;

            _TranScmPoService = TranScmPoService;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenSupplierInformationService = GenSupplierInformationService;
            

            _contextAccessor = contextAccessor;
            _tramscmpo = tramscmpo;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> BillSubmissionLanding()
        {

            return View("~/Views/SCM/BillSubmission/BillSubmissionLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> BillSubmissionNew(string po_id)
        {
            string id = clsUtil.DecodeString(po_id);
            tran_scm_bill_submission_DTO model = new tran_scm_bill_submission_DTO();
            tran_scm_po_DTO po_model = new tran_scm_po_DTO();
            po_model = await _TranScmPoService.GetSingleAsync(Convert.ToInt64(id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(po_model.supplier_id));
            model.po_id = po_model.po_id;
            model.supplier_id = po_model.supplier_id;
            model.supplier_name = sup.name;
            model.List_Files = po_model.List_Files;
            model.po_details = po_model.List_po_details;
            model.po_date = po_model.po_date;
            model.po_no = po_model.po_no;
            model.po_approved_date = po_model.approved_date;
            model.bill_date=DateTime.Now;
            model.total_po_amount = model.po_details.Sum(item => item.total_price);
       
            List<rpc_proc_sp_get_data_tran_scm_po_DTO> model1 = new List<rpc_proc_sp_get_data_tran_scm_po_DTO>();
            model1 = await _TranScmPoService.GetBillChallanDetailAsync(Convert.ToInt64(id));

            model.detChallan = model1.ToList();

            return PartialView("~/Views/SCM/BillSubmission/_BillSubmissionNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> BillSubmissionEdit(string bill_submission_id)
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
            return PartialView("~/Views/SCM/BillSubmission/_BillSubmissionEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> BillSubmissionView(string bill_submission_id)
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

            return PartialView("~/Views/SCM/BillSubmission/_BillSubmissionView.cshtml", model);

        }

        //[HttpGet]
        //public async Task<IActionResult> BillChallanItemdetail(string poid, string recivedID)
        //{
        //    tran_scm_bill_submission_DTO model = new tran_scm_bill_submission_DTO();

        //    List<rpc_tran_mcd_receive_detail_DTO> modelDet= new List<rpc_tran_mcd_receive_detail_DTO>();
        //    modelDet = await _TranScmPoService.GetBillChallanItemDetailAsync(Convert.ToInt64(poid) , Convert.ToInt64(recivedID));
        //    model.detItem = modelDet.ToList();

        //    return PartialView("~/Views/SCM/BillSubmission/_billItemDetail.cshtml", model);
        //}

        [HttpGet]
        public async Task<JsonResult> BillChallanItemdetail(string poid, string recivedID)
        {

            tran_scm_bill_submission_DTO data = new tran_scm_bill_submission_DTO();

            List<tran_scm_po_details_DTO> modelDet = new List<tran_scm_po_details_DTO>();
            modelDet = await _TranScmPoService.GetBillChallanItemDetailAsync(Convert.ToInt64(poid), Convert.ToInt64(recivedID));
            data.po_details = modelDet.ToList();

            return Json(new { data = data });
        }


        [HttpPost]
        public async Task<IActionResult> GetPOApprovalData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            Int64 actionType = 3;
            Int64 receivedID = 0;

            //var records = await _tranScmBillSubmissionService.GetAll_Pending_Bill_Submission_Async(request.fiscal_year_id,request.event_id,request.Start,request.Length,request.Search.Value);
            var records = await _rpc_db_service.GetAllJoined_TranMcdReceiveListAsync(request.Start, request.Length, ActionType.getAll.ToString(), receivedID, actionType, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, request.Search);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.po_id,
                            t.po_no,
                            t.po_date,
                            t.del_chalan_date,
                            t.supplier_name,
                            t.unit_name,                          
                            t.is_submitted,
                           
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +


                            "<button type='button' onclick='AddBillSubmission(this)'  class='btn btn-success btnView'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'>Submit Bill</button>" +

                            "</div>"
                        }).ToList();
           // var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }


        //[HttpPost]
        //public async Task<IActionResult> GetBillSubmittedData(DtParameters request)
        //{
        //    request.fiscal_year_id = Fiscal_Year_ID;
        //    request.event_id = Event_ID;
        //    var action_type = 1;
        //    var records = await _tranScmBillSubmissionService.GetAll_Bill_Submission_Async(request.fiscal_year_id, request.event_id, action_type, request.Start, request.Length,request.Search.Value);

        //    var index = request.Start + 1;
        //    var data = (from t in records
        //                select new
        //                {
        //                    row_index = index++,
        //                    t.bill_submission_id,
        //                    t.po_id,
        //                    t.po_no,
        //                    t.supplier_id,
        //                    t.supplier_name,
        //                    t.bill_no,
        //                    t.challan_no,
        //                    t.challan_date,
        //                    t.bill_date,
        //                    t.total_po_amount,
        //                    t.loading_cost_in_percentage,
        //                    t.loading_cost,
        //                    t.transport_cost_in_percentage,
        //                    t.transport_cost,
        //                    t.discount_in_percentage,
        //                    t.discount_amount,
        //                    t.vat_in_percentage,
        //                    t.vat_amount,
        //                    t.total_value,
        //                    t.is_submitted,
        //                    t.submitted_date,
        //                    t.submitted_by,
        //                    t.is_approved,
        //                    t.approved_date,
        //                    t.approved_by,
        //                    t.status,
        //                    t.documents,
        //                    t.terms_conditions,
        //                    t.remarks,
        //                    t.added_by,
        //                    t.updated_by,
        //                    t.date_added,
        //                    t.date_updated,
        //                    t.delivery_unit_name,


        //                    datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
        //                    "<button type='button' onclick='EditBillSubmission(this)'  class='btn btn-primary btnEdit'  bill_submission_id='" + clsUtil.EncodeString(t.bill_submission_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +
        //                    "<button type='button' onclick='ViewBillSubmission(this)'  class='btn btn-warning btnView'  bill_submission_id='" + clsUtil.EncodeString(t.bill_submission_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
        //                    "<button type='button' onclick='DeleteBillSubmission(\"" + clsUtil.EncodeString(t.bill_submission_id.ToString()) + "\")'  class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
        //                    "</div>"
        //                }).ToList();
        //    var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
        //    return Json(ret_obj);

        //}


        [HttpPost]
        public async Task<IActionResult> SaveBillSubmission([FromBody] tran_scm_bill_submission_DTO request)
        {
            var ret = true;



            request.updated_by = sec_object.userid.Value;
            request.date_updated = DateTime.Now;


            try
            {
                var model = JsonConvert.DeserializeObject<tran_scm_bill_submission_DTO>(JsonConvert.SerializeObject(request));
                model.challan_date = model.bill_date;
                model.challan_no = model.bill_no;
                model.submitted_date = DateTime.Now;
                model.submitted_by = request.added_by;
                model.is_send_for_approval = request.is_send_for_approval;
                
                model.fiscal_year_id=Fiscal_Year_ID;
                model.event_id = Event_ID;
                ret = await _tranScmBillSubmissionService.tran_scm_bill_submission_insert_sp(model);

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
        public async Task<IActionResult> UpdateBillSubmission([FromBody] tran_scm_bill_submission_DTO request)
        {
            var ret = true;

            request.updated_by = sec_object.userid.Value;
            request.date_updated = DateTime.Now;

            try
            {
                var model = JsonConvert.DeserializeObject<tran_scm_bill_submission_DTO>(JsonConvert.SerializeObject(request));
                model.challan_date = model.bill_date;
                model.challan_no = model.bill_no;
                model.submitted_date = DateTime.Now;
                model.submitted_by = request.added_by;
                model.is_send_for_approval = request.is_send_for_approval;
               
                ret = await _tranScmBillSubmissionService.tran_scm_bill_submission_update_sp(model);

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

