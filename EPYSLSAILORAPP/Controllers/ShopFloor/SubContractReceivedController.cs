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
    public class SubContractReceivedController : ExtendedBaseController
    {
        private readonly ILogger<SubContractReceivedController> _logger;

        private readonly ISubContractReceivedService _subContractReceivedService;

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

        public SubContractReceivedController(
           IMapper mapper, ILogger<SubContractReceivedController> logger, IHttpContextAccessor contextAccessor,
            ISubContractReceivedService subContractReceivedService, IGenUnitService GenUnitService,
            IRPCDbService rpc_db_service, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _subContractReceivedService = subContractReceivedService;
            _GenUnitService = GenUnitService;
            
            _configuration = configuration;
            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> SubContractReceivedLanding()
        {

            return View("~/Views/ShopFloor/SubContractReceived/SubContractReceivedLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> SubContractReceivedAdd(string tran_sub_contract_request_id, DtParameters request)
        {
            long p_tran_sub_contract_request_id = Convert.ToInt64(clsUtil.DecodeString(tran_sub_contract_request_id));
            var length = 10;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            rpc_tran_sub_contract_received_DTO model = await _subContractReceivedService.GetTechPackInfoForSubContractReceived(request.Start, length, p_tran_sub_contract_request_id, ActionType.getById.ToString(), request.fiscal_year_id, request.event_id, request.delivery_unit_id);

            model.tran_sub_contract_received_date=DateTime.Now;
            var supplier_name = await _subContractReceivedService.GetAllSupplier(p_tran_sub_contract_request_id);

            ViewBag.supplier_name = supplier_name.ToList()
            .Select(a => new SelectListItem
            {
                Text = a.name,
                Value = a.supplier_id.ToString(),
            }).ToList();

            ViewBag.contactPerson = supplier_name.FirstOrDefault().contact_person;
            ViewBag.supplieraddress = supplier_name.FirstOrDefault().factory_address;

            ViewBag.Colors = _subContractReceivedService.GetAll_subcontract_color_Async(p_tran_sub_contract_request_id)
                     .Select(a => a.color_code.ToString())
                     .Distinct()
                     .Select(colorCode => new SelectListItem
                     {
                         Text = colorCode,
                         Value = colorCode
                     })
                     .ToList();

            return PartialView("~/Views/ShopFloor/SubContractReceived/_SubContractReceivedAdd.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetSubContractReceivedPendingData(DtParameters request)
        {
            Int64 techpack_id = 0;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _subContractReceivedService.GetSubContractReceivedPendingListAsync(request.Start, request.Length, techpack_id, ActionType.getAll.ToString(), request.fiscal_year_id, request.event_id, request.delivery_unit_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_sub_contract_request_id,
                            t.tran_techpack_id,
                            t.techpack_number,
                            t.tran_sub_contract_request_date,
                            t.style_item_product_id,
                            t.style_item_product_category,
                            t.merchandiser_name,
                            t.designer_name,
                            t.styleQuantity,
                            t.emb_Sub_process_type,
                            
                            

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='SubContractReceivedAdd(this)' style='width:150px' class='btn btn-success btnEdit'  tran_sub_contract_request_id='" + clsUtil.EncodeString(t.tran_sub_contract_request_id.ToString()) + "'>Sub Contract Received</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetSubContractReceivedDraftData(DtParameters request)
        {
            Int64 actionType = 1;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _subContractReceivedService.GetSubContractReceived_DraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_sub_contract_received_id,
                            t.tran_sub_contract_received_date,
                            t.style_item_product_category,
                            t.techpack_number,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewSubContractReceivedDetails(this)' style='width:140px' class='btn btn-success btnEdit'  tran_sub_contract_received_id='" + clsUtil.EncodeString(t.tran_sub_contract_received_id.ToString()) + "'>Propose For Approval</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetSubContractReceivedProposedData(DtParameters request)
        {
            Int64 actionType = 2;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _subContractReceivedService.GetSubContractReceived_DraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_sub_contract_received_id,
                            t.tran_sub_contract_received_date,
                            t.style_item_product_category,
                            t.techpack_number,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewSubContractReceivedDetails(this)' style='width:140px' class='btn btn-success btnEdit'  tran_sub_contract_received_id='" + clsUtil.EncodeString(t.tran_sub_contract_received_id.ToString()) + "'>Approve</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }


        [HttpPost]
        public async Task<IActionResult> GetSubContractReceivedApprovedData(DtParameters request)
        {
            Int64 actionType = 3;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _subContractReceivedService.GetSubContractReceived_DraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_sub_contract_received_id,
                            t.tran_sub_contract_received_date,
                            t.style_item_product_category,
                            t.techpack_number,
                            t.supplier_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewSubContractReceivedDetails(this)' style='width:140px' class='btn btn-success btnEdit'  tran_sub_contract_received_id='" + clsUtil.EncodeString(t.tran_sub_contract_received_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpGet]
        public async Task<JsonResult> GetSubContractReceivedAll(long techpack_id, string colorCode)
        {

            var data = await _subContractReceivedService.GetAll_subcontract_techpack_color_wiseAsync(techpack_id, colorCode);

            return Json(new { data = data });
        }

        [HttpPost]
        public async Task<IActionResult> SaveSubContractReceived([FromBody] tran_sub_contract_received_DTO request)
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
                request.tran_sub_contract_received_details = JArray.Parse(JsonConvert.SerializeObject(request.details)).ToString();

                ret = await _subContractReceivedService.tran_sub_contract_received_insert_sp(request);

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
        public async Task<IActionResult> SubContactReceivedView(string id)
        {

            string decoded_po_id = clsUtil.DecodeString(id);

            tran_sub_contract_received_DTO model = new tran_sub_contract_received_DTO();

            model = await _subContractReceivedService.GetSingleAsync(Convert.ToInt64(decoded_po_id));


            model.details = JsonConvert.DeserializeObject<List<tran_sub_contract_received_details_DTO>>(model.sub_contract_received_details);

            return PartialView("~/Views/ShopFloor/SubContractReceived/_SubContractReceivedDetailsView.cshtml", model);

        }

        [HttpPost]
        public async Task<IActionResult> ProposedForApprovalOrApprove([FromBody] tran_sub_contract_received_DTO request)
        {

            var ret = true;

            {

                request.updated_by = sec_object.userid.Value;
                request.date_added = DateTime.Now;
            }

            if (request.is_submitted == 1)
            {
                ret = await _subContractReceivedService.ApprovalProposedAsync(request);
                return Json(new ResultEntity
                {
                    Status_Code = ret == true ? "200" : "400",
                    Status_Result = ret == true ? "Proposed for approval successfully." : "Proposed Failed."
                });
            }
            else if (request.is_submitted == 2)
            {
                ret = await _subContractReceivedService.ApprovedAsync(request);
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
