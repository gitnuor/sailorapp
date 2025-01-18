
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

namespace EPYSLSAILORAPP.Controllers.ShopFloor
{
    public class SubContractRequestController : ExtendedBaseController
    {
        private readonly ILogger<SubContractRequestController> _logger;

        private readonly ISubContractRequestService _subContractRequestService;

        private readonly IGenUnitService _GenUnitService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside SubContractRequestController !");
            return View();
        }

        public SubContractRequestController(
           IMapper mapper, ILogger<SubContractRequestController> logger, IHttpContextAccessor contextAccessor,
            ISubContractRequestService subContractRequestService, IGenUnitService GenUnitService,
            IRPCDbService rpc_db_service, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _subContractRequestService = subContractRequestService;
            _GenUnitService = GenUnitService;
            
            _configuration = configuration;
            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> SubContractRequestLanding()
        {

            return View("~/Views/ShopFloor/SubContractRequest/SubContractRequestLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> SubContractRequestAdd(string tran_production_process_id, DtParameters request)
        {
            long p_tran_production_process_id = Convert.ToInt64(clsUtil.DecodeString(tran_production_process_id));
            var length = 10;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            rpc_tran_sub_contract_request_DTO model = await _subContractRequestService.GetTechPackInfoForSubContractRequest(request.Start, length, p_tran_production_process_id, ActionType.getById.ToString(), request.fiscal_year_id, request.event_id, request.delivery_unit_id);

            var supplier_name = await _subContractRequestService.GetAllSupplier(p_tran_production_process_id);

            ViewBag.supplier_name = supplier_name.ToList()
            .Select(a => new SelectListItem
            {
                Text = a.name,
                Value = a.supplier_id.ToString(),
            }).ToList();

            ViewBag.contactPerson = supplier_name.FirstOrDefault().contact_person;
            ViewBag.supplieraddress = supplier_name.FirstOrDefault().factory_address;
     
            return PartialView("~/Views/ShopFloor/SubContractRequest/_SubContractRequestAdd.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetSubContractPendingData(DtParameters request)
        {
            Int64 techpack_id = 0;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _subContractRequestService.GetSubContractRequestPendingListAsync(request.Start, request.Length, techpack_id, ActionType.getAll.ToString(), request.fiscal_year_id, request.event_id, request.delivery_unit_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_production_process_id,
                            t.tran_techpack_id,
                            t.techpack_number,
                            t.tran_production_process_date,
                            t.style_item_product_id,
                            t.style_item_product_category,
                            t.merchandiser_name,
                            t.designer_name,
                            t.styleQuantity,
                            t.emb_Sub_process_type,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='SubContractRequestAdd(this)' style='width:150px' class='btn btn-success btnEdit'  tran_production_process_id='" + clsUtil.EncodeString(t.tran_production_process_id.ToString()) + "'>Add Sub Contract</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }


        [HttpPost]
        public async Task<IActionResult> GetSubContractDraftData(DtParameters request)
        {
            Int64 actionType = 1;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _subContractRequestService.GetSubContract_DraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_sub_contract_request_id,
                            t.tran_sub_contract_request_date,
                            t.style_item_product_category,
                            t.techpack_number,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewSubContractDetails(this)' style='width:140px' class='btn btn-success btnEdit'  tran_sub_contract_request_id='" + clsUtil.EncodeString(t.tran_sub_contract_request_id.ToString()) + "'>Propose For Approval</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetSubContractProposedData(DtParameters request)
        {
            Int64 actionType = 2;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _subContractRequestService.GetSubContract_DraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_sub_contract_request_id,
                            t.tran_sub_contract_request_date,
                            t.style_item_product_category,
                            t.techpack_number,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewSubContractDetails(this)' style='width:140px' class='btn btn-success btnEdit'  tran_sub_contract_request_id='" + clsUtil.EncodeString(t.tran_sub_contract_request_id.ToString()) + "'>Approve</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }


        [HttpPost]
        public async Task<IActionResult> GetSubContractApprovedData(DtParameters request)
        {
            Int64 actionType = 3;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _subContractRequestService.GetSubContract_DraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_sub_contract_request_id,
                            t.tran_sub_contract_request_date,
                            t.style_item_product_category,
                            t.techpack_number,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewSubContractDetails(this)' style='width:140px' class='btn btn-success btnEdit'  tran_sub_contract_request_id='" + clsUtil.EncodeString(t.tran_sub_contract_request_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }


        [HttpGet]
        public async Task<ActionResult> GetSubcontractColorRateWiseDetail(long tran_production_process_id)
        {
            rpc_tran_sub_contract_request_DTO model = new rpc_tran_sub_contract_request_DTO();
           
            ViewBag.Colors = _subContractRequestService.GetAll_subcontract_color_Async(tran_production_process_id)
                     .Select(a => a.color_code.ToString())
                     .Distinct()
                     .Select(colorCode => new SelectListItem
                     {
                         Text = colorCode,
                         Value = colorCode
                     })
                     .ToList();


            return PartialView("~/Views/ShopFloor/SubContractRequest/_SubContractRequestDetails.cshtml", model);
        }

        [HttpGet]
        public async Task<JsonResult> GetSubContractDetailAll(long techpack_id, string colorCode)
        {

            var data = await _subContractRequestService.GetAll_subcontract_techpack_color_wiseAsync(techpack_id, colorCode);

            return Json(new { data = data });
        }

        [HttpPost]
        public async Task<IActionResult> SaveSubContractRequest([FromBody] tran_sub_contract_request_DTO request)
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
                //  request.tran_sub_contract_request_details = JArray.Parse(JsonConvert.SerializeObject(request.details)).ToString();
                request.tran_sub_contract_request_details = JsonConvert.SerializeObject(request.details);//.ToString();//JArray.Parse(JsonConvert.SerializeObject(request.details)).ToString();

                //foreach (var process in request.tran_production_process_details)
                //{
                //    if (process is JObject processObject)
                //    {
                //        var processNameDetail = processObject["process_name_detail"];
                //        string jsonString = JsonConvert.SerializeObject(processNameDetail);
                //        processObject["production_process_name"] = JArray.Parse(jsonString);
                //    }
                //}

                ret = await _subContractRequestService.tran_sub_contract_request_insert_sp(request);

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
        public async Task<IActionResult> SubContactRequestView(string id)
        {

            string decoded_po_id = clsUtil.DecodeString(id);

            tran_sub_contract_request_DTO model = new tran_sub_contract_request_DTO();

            model = await _subContractRequestService.GetSingleAsync(Convert.ToInt64(decoded_po_id));




            model.details = JsonConvert.DeserializeObject<List<tran_sub_contract_request_details_DTO>>(model.tran_sub_contract_request_details);

           




            return PartialView("~/Views/ShopFloor/SubContractRequest/_SubContactRequestDetailsView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> ProposedForApprovalOrApprove([FromBody] tran_sub_contract_request_DTO request)
        {

            var ret = true;

            {

                request.updated_by = sec_object.userid.Value;
                request.date_added = DateTime.Now;
            }

            if (request.is_submitted == 1)
            {
                ret = await _subContractRequestService.ApprovalProposedAsync(request);
                return Json(new ResultEntity
                {
                    Status_Code = ret == true ? "200" : "400",
                    Status_Result = ret == true ? "Proposed for approval successfully." : "Proposed Failed."
                });
            }
            else if (request.is_submitted == 2)
            {
                ret = await _subContractRequestService.ApprovedAsync(request);
                return Json(new ResultEntity
                {
                    Status_Code = ret == true ? "200" : "400",
                    Status_Result = ret == true ? "Approved successfully" : "Approved Failed"
                });
            }
            else
            {
                return Json(new ResultEntity
                {
                    Status_Code = "400",
                    Status_Result = "Operation Failed"
                });
            }

        }
    }
}

