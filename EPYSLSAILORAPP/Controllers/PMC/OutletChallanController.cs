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
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using System.Drawing.Imaging;


namespace EPYSLSAILORAPP.Controllers.PMC
{
    public class OutletChallanController : ExtendedBaseController
    {
        private readonly ILogger<OutletChallanController> _logger;

        private readonly ISubContractRequestService _subContractRequestService;
        private readonly ITranChallanOutletService _TranChallanOutletService;

        private readonly ITranServiceWorkOrderService _TranServiceWorkOrderService;

        private readonly IGenUnitService _GenUnitService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;
        private readonly IGenOutletService _gen_outlet_entity_service;
        private readonly IGenTranTransportService _genTranTransportService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside SubContractRequestController !");
            return View();
        }

        public OutletChallanController(
           IMapper mapper, ILogger<OutletChallanController> logger, IHttpContextAccessor contextAccessor,
            ISubContractRequestService subContractRequestService, IGenUnitService GenUnitService,
            IRPCDbService rpc_db_service, IConfiguration configuration, ITranChallanOutletService TranChallanOutletService, IGenTranTransportService GenTranTransportService, IGenOutletService gen_outlet_entity_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _subContractRequestService = subContractRequestService;
            _GenUnitService = GenUnitService;
            
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _gen_outlet_entity_service = gen_outlet_entity_service;
            _genTranTransportService = GenTranTransportService;
            _TranChallanOutletService = TranChallanOutletService;

        }

        [HttpGet]
        public async Task<IActionResult> OutletChallanLanding()
        {

            return View("~/Views/PMC/OutletChallan/OutletChallanLanding.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> GetTranOutletChallanDraftData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranChallanOutletService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_delivery_outlet_challan_id,
                            t.delivery_outlet_challan_date,
                            t.transport_number,
                            t.driver_name,
                            t.delivery_outlet_challan_no,
                            t.outlet_name,
                            t.delivery_from,
                            t.delivery_address,

                            datatablebuttonscode = "<div style='text-align:center;width: 210px;' role='toolbar' >" +
                            "<button type='button' onclick='EditOutletChallan(this)' style='width: 90px;display:none;' class='btn btn-primary btnEdit'  tran_delivery_outlet_challan_id='" + clsUtil.EncodeString(t.tran_delivery_outlet_challan_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewOutletChallanDraft(this)' style='width: 90px;' class='btn btn-primary btnView'  tran_delivery_outlet_challan_id='" + clsUtil.EncodeString(t.tran_delivery_outlet_challan_id.ToString()) + "'>View</button>" +
                           
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpGet]
        public async Task<IActionResult> TranOutletChallanNew()
        {
            rpc_tran_outlet_challan_request_DTO model = new rpc_tran_outlet_challan_request_DTO();
            List<gen_outlet_DTO> outletList = await _gen_outlet_entity_service.GetAllAsync();

            ViewBag.outletList =
                outletList.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.outlet_name.ToString(),
                        Value = a.outlet_id.ToString()
                    }).ToList();

            

            List<gen_tran_transport_entity> transportTypeList = await _genTranTransportService.GetAllAsync();

            ViewBag.transportTypeList =
                transportTypeList.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.transport_type.ToString(),
                        Value = a.transport_id.ToString()
                    }).ToList();

            var techpacklist = await _genTranTransportService.GetAllTechAsync();

            ViewBag.techpacklist =
                techpacklist.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.techpack_number.ToString(),
                        Value = a.techpack_id.ToString()
                    }).ToList();

            //var techpack = await _subContractRequestService.GetAllSupplier(p_tran_production_process_id);

            //ViewBag.supplier_name = techpack.ToList()
            //.Select(a => new SelectListItem
            //{
            //    Text = a.name,
            //    Value = a.supplier_id.ToString(),
            //}).ToList();

            return PartialView("~/Views/PMC/OutletChallan/_OutletChallanNew.cshtml", model);

        }

        [HttpGet]
        public async Task<JsonResult> GetOutletAddress(long outletid)
        {
            var result = await _gen_outlet_entity_service.GetSingleAsync(outletid);
            return Json(new { data = result.outlet_address });
        }

        [HttpGet]
        public async Task<JsonResult> GetOutletDetails(long techpack_id)
        {

            var objmodel = await _TranChallanOutletService.GetOutletDetailList(techpack_id);
            return Json(new { data = JsonConvert.DeserializeObject<List<rpc_tran_outlet_challan_request_DTO>>(objmodel.FirstOrDefault().challan_details) });
        }

        [HttpPost]
        public async Task<IActionResult> SaveOutletChallan([FromBody] tran_delivery_outlet_challan_DTO request)
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
                request.event_id = objFilter.event_id;
                request.fiscal_year_id = objFilter.fiscal_year_id;
                request.tran_delivery_outlet_challan_id_detail_json = JArray.Parse(JsonConvert.SerializeObject(request.details));

                ret = await _TranChallanOutletService.tran_delivery_outlet_challan_insert_sp(request);

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

        #region Outlet Challan Approve

        [HttpGet]
        public async Task<IActionResult> OutletChallanLandingApprove()
        {

            return View("~/Views/PMC/OutletChallan/OutletChallanLandingApprove.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> GetTranOutletChallanProposedApprovalData(DtParameters request)
        {
            Int64 actionType = 2;
            request.event_id = objFilter.event_id;
            request.fiscal_year_id = objFilter.fiscal_year_id;

            var records = await _TranChallanOutletService.GetTranServiceoutlet_challan_landing_approval_data(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id,request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_delivery_outlet_challan_id,
                            t.delivery_outlet_challan_date,
                            t.transport_number,
                            t.driver_name,
                            t.delivery_outlet_challan_no,
                            t.outlet_name,
                            t.delivery_from,
                            t.delivery_address,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewOutletChallanApprove(this)' style='width:120px' class='btn btn-success btnEdit'  tran_delivery_outlet_challan_id='" + clsUtil.EncodeString(t.tran_delivery_outlet_challan_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetTranOutletChallanApprovedData(DtParameters request)
        {

            Int64 actionType = 3;
            request.event_id = objFilter.event_id;
            request.fiscal_year_id = objFilter.fiscal_year_id;

            var records = await _TranChallanOutletService.GetTranServiceoutlet_challan_landing_approval_data(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_delivery_outlet_challan_id,
                            t.delivery_outlet_challan_date,
                            t.transport_number,
                            t.driver_name,
                            t.delivery_outlet_challan_no,
                            t.outlet_name,
                            t.delivery_from,
                            t.delivery_address,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewOutletChallanApprove(this)' style='width:120px' class='btn btn-success btnEdit'  tran_delivery_outlet_challan_id='" + clsUtil.EncodeString(t.tran_delivery_outlet_challan_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpGet]
        public async Task<IActionResult> TranOutletChallanViewApprove(string tran_delivery_outlet_challan_id, DtParameters request)
        {
            try
            {
                string decoded_delivery_outlet_challan_id = clsUtil.DecodeString(tran_delivery_outlet_challan_id);

                var data = await _TranChallanOutletService.Get_data_tran_delivery_outlet_challan_Async(Convert.ToInt64(decoded_delivery_outlet_challan_id));
                tran_delivery_outlet_challan_DTO model = JsonConvert.DeserializeObject<tran_delivery_outlet_challan_DTO>(JsonConvert.SerializeObject(data));
                model.details = JsonConvert.DeserializeObject<List<tran_delivery_outlet_challan_details_DTO>>(data.tran_delivery_outlet_challan_details_list);

                model.tran_delivery_outlet_challan_id = Convert.ToInt64(decoded_delivery_outlet_challan_id);
                return PartialView("~/Views/PMC/OutletChallan/_TranOutletChallanApproveView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        [HttpGet]
        public async Task<IActionResult> TranOutletChallanView(string tran_delivery_outlet_challan_id, DtParameters request)
        {
            try
            {
                string decoded_delivery_outlet_challan_id = clsUtil.DecodeString(tran_delivery_outlet_challan_id);

                var data = await _TranChallanOutletService.Get_data_tran_delivery_outlet_challan_Async(Convert.ToInt64(decoded_delivery_outlet_challan_id));
                tran_delivery_outlet_challan_DTO model = JsonConvert.DeserializeObject<tran_delivery_outlet_challan_DTO>(JsonConvert.SerializeObject(data));
                model.details = JsonConvert.DeserializeObject<List<tran_delivery_outlet_challan_details_DTO>>(data.tran_delivery_outlet_challan_details_list);

                model.tran_delivery_outlet_challan_id = Convert.ToInt64(decoded_delivery_outlet_challan_id);
                return PartialView("~/Views/PMC/OutletChallan/_TranOutletChallanView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        [HttpPost]
        public async Task<IActionResult> UpdateForApproval([FromBody] tran_delivery_outlet_challan_DTO request)
        {
            var ret = true;
            request.updated_by = sec_object.userid.Value;
            request.date_updated = DateTime.Now;
 
            try
            {

                ret = await _TranChallanOutletService.ApproveAsync(request);

               
                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Data Approved Successfully" : "Data Approved Failed")
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
        public async Task<IActionResult> ProposedForApproval([FromBody] tran_delivery_outlet_challan_DTO request)
        {
            var ret = true;
            request.updated_by = sec_object.userid.Value;
            request.date_updated = DateTime.Now;

            try
            {

                ret = await _TranChallanOutletService.ProposedAsync(request);


                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Proposed For Approval Successfully" : "Data Proposed Failed")
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

        #endregion

    }
}
