using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
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
    public class OpenPOApprovalController : ExtendedBaseController
    {
        private readonly ILogger<POApprovalController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITranScmPoService _ITranScmPoService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside Open PO Controller !");
            return View();
        }

        public OpenPOApprovalController(
           IMapper mapper, ILogger<POApprovalController> logger, IHttpContextAccessor contextAccessor, IGenSupplierInformationService GenSupplierInformationService,
            ITranScmPoService TranScmPoService,
            IConfiguration configuration,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _ITranScmPoService = TranScmPoService;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            
            _GenSupplierInformationService = GenSupplierInformationService;
            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> OpenPOApprovalLanding()
        {


            return View("~/Views/SCM/OpenPOApproval/OpenPOApprovalLanding.cshtml");
        }



        [HttpGet]
        public async Task<IActionResult> OpenPOApprovalView(string po_id)
        {

            string decoded_po_id = clsUtil.DecodeString(po_id);

            tran_scm_po_DTO model = new tran_scm_po_DTO();

            model = await _ITranScmPoService.GetSingleAsync(Convert.ToInt64(decoded_po_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            model.selected_supplier_name = sup.name;
            return PartialView("~/Views/SCM/OpenPOApproval/_OpenPOApprovalView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetPORejectData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _ITranScmPoService.GetOpenPORejectData(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, request.Search.Value); ;

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.po_id,
                            t.po_no,
                            t.po_date,
                            t.supplier_name,
                            t.unit_name,
                            t.pr_no,
                            t.event_title,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +


                            "<button type='button' onclick='ViewOpenPOReject(this)'  class='btn btn-danger btnView'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }


        [HttpPost]
        public async Task<IActionResult> GetOpenPOApprovedData(DtParameters request)
        {


            var records = await _ITranScmPoService.GetOpenPOApprovedData(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id,request.Search.Value);


            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.po_id,
                            t.po_no,
                            t.po_date,
                            t.supplier_name,
                            t.unit_name,

                            t.event_title,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +


                            "<button type='button' onclick='ViewPOApproval(this)'  class='btn btn-success btnView'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }



        [HttpPost]
        public async Task<IActionResult> GetOpenPOPendingApprovalData(DtParameters request)
        {


            var records = await _ITranScmPoService.GetAllOpenPoPendingApprovalAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id,request.Search.Value);


            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.po_id,
                            t.po_no,
                            t.po_date,
                            t.supplier_name,
                            t.unit_name,
                            t.event_title,
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewPOApproval(this)'  class='btn btn-warning btnView'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }


        [HttpPost]
        public async Task<IActionResult> UpdateOpenPOApproval([FromBody] tran_scm_po_DTO request)
        {
            var ret = true;

            request.added_by = sec_object.userid.Value;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

            try
            {



                if (request.is_approved == 1)
                {
                    ret = await _ITranScmPoService.UpdateApproval(Convert.ToInt64(request.po_id), request.status_remarks);

                    return Json(new ResultEntity
                    {
                        Status_Code = (ret == true ? "200" : "400"),
                        Status_Result = (ret == true ? "Successful" : "Data Approved Failed")
                    });
                }
                else
                {
                    ret = await _ITranScmPoService.UpdateRejectApproval(Convert.ToInt64(request.po_id), request.status_remarks);

                    return Json(new ResultEntity
                    {
                        Status_Code = (ret == true ? "200" : "400"),
                        Status_Result = (ret == true ? "Successful" : "Data Reject Failed")
                    });
                }

                //ret = await _ITranScmPoService.ApprovedAsync(request);

                //return Json(new ResultEntity
                //{
                //    Status_Code = (ret == true ? "200" : "400"),
                //    Status_Result = (ret == true ? "Successful" : "Data Saving Failed")
                //});
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

