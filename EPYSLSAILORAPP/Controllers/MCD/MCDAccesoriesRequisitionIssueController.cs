using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.SupplyChain;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class MCDAccesoriesRequisitionIssueController : ExtendedBaseController
    {
        private readonly ILogger<MCDAccesoriesRequisitionIssueController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITranMcdRequisitionIssueService _MCDRequisitionIssueService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside MCDRequisitionIssueController !");
            return View();
        }

        public MCDAccesoriesRequisitionIssueController(
           IMapper mapper, ILogger<MCDAccesoriesRequisitionIssueController> logger, IHttpContextAccessor contextAccessor,
            ITranMcdRequisitionIssueService MCDRequisitionIssueService, IConfiguration configuration,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _MCDRequisitionIssueService = MCDRequisitionIssueService;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> MCDRequisitionIssueLanding()
        {


            return View("~/Views/MCD/MCDAccesoriesRequisitionIssue/MCDRequisitionIssueLanding.cshtml");
        }


        [HttpGet]
        public async Task<IActionResult> MCDRequisitionIssueLandingApproval()
        {


            return View("~/Views/MCD/MCDAccesoriesRequisitionIssue/MCDRequisitionIssueLandingApproval.cshtml");
        }


        [HttpGet]
        public async Task<IActionResult> MCDRequisitionIssueNew(string tran_mcd_requisition_slip_id)
        {

            string decoded_requisition_slip_id = clsUtil.DecodeString(tran_mcd_requisition_slip_id);

            var ret = true;

            var data = await _rpc_db_service.Get_master_detail_fabric_requisition_slipAsync(Convert.ToInt64(decoded_requisition_slip_id));
            tran_mcd_requisition_slip_DTO model = JsonConvert.DeserializeObject<tran_mcd_requisition_slip_DTO>(JsonConvert.SerializeObject(data));
            model.details = JsonConvert.DeserializeObject<List<tran_mcd_requisition_slip_detail_DTO>>(data.tran_mcd_requisition_slip_detail_list);

            model.issuer_name = sec_object.username;


            model.date_added = DateTime.Now.Date;
            ViewBag.SubCategoryList = JsonConvert.DeserializeObject<List<dropdown_entity>>(data.sub_group_dropdown_list).ToList()
            .Select(a =>
                     new SelectListItem
                     {
                         Text = a.text.ToString(),
                         Value = a.id.ToString(),
                     }
                     ).ToList();

            return PartialView("~/Views/MCD/MCDAccesoriesRequisitionIssue/_MCDRequisitionIssueNew.cshtml", model);

        }


        [HttpGet]
        public async Task<IActionResult> AddNewAccesories(string techPackId, string gen_item_structure_group_sub_id)
        {


            var list = await _rpc_db_service.GetAllsp_get_techPack_by_gen_item_structure_group_sub_idAsync(Convert.ToInt32(techPackId), Convert.ToInt32(gen_item_structure_group_sub_id));


            //}).ToList();

            ViewBag.gen_item_structure_group_sub_id = Convert.ToInt32(gen_item_structure_group_sub_id);


            return PartialView("~/Views/MCD/MCDAccesoriesRequisitionIssue/_AccesoriesDetailsList.cshtml", list);

        }

        [HttpPost]
        public async Task<IActionResult> GetMCDRequisitionIssueData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _MCDRequisitionIssueService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_mcd_requisition_issue_id,
                            t.issue_no,
                            t.requisition_slip_id,
                            t.techpack_id,
                            t.remarks,
                            t.is_submitted,
                            t.is_full_issued,
                            t.is_closed,
                            t.is_approved,
                            t.approved_by,
                            t.approve_date,
                            t.approve_remarks,
                            t.added_by,
                            t.date_added,
                            t.updated_by,
                            t.date_updated,
                            t.gen_item_structure_group_id,
                            t.gen_item_structure_group_sub_id,
                            t.event_id,
                            t.fiscal_year_id,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditMCDRequisitionIssue(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  tran_mcd_requisition_issue_id='" + clsUtil.EncodeString(t.tran_mcd_requisition_issue_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewMCDRequisitionIssue(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_mcd_requisition_issue_id='" + clsUtil.EncodeString(t.tran_mcd_requisition_issue_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteMCDRequisitionIssue(\"" + clsUtil.EncodeString(t.tran_mcd_requisition_issue_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetJoinedMCDRequisitionSlipData(MCD_DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _MCDRequisitionIssueService.GetAllMCDRequisitionSlipAccAsync(request.Start, request.Length,
                request.fiscal_year_id, request.event_id,
                                    request.group_id, request.sub_group_id, request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.requisition_slip_id,
                            t.requisition_slip_no,
                            t.requisition_date,
                            t.requisition_by,
                            t.techpack_number,
                            t.name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='McdRequisitionIssue(this)' style='width: 120px;' class='btn btn-success btnEdit'  requisition_slip_id='" + clsUtil.EncodeString(t.requisition_slip_id.ToString()) + "'>Issue</button>" +

                            "</div>"

                        }).ToList();
         
                var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);
            

           


        }

        [HttpPost]
        public async Task<IActionResult> GetDraftRequisitionSlipAcceData(MCD_DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _MCDRequisitionIssueService.GetDraftRequisitionSlipAcceData(request.Start, request.Length,
                request.fiscal_year_id, request.event_id,
                                    request.group_id, request.sub_group_id,request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.issue_no,
                            t.tran_mcd_requisition_issue_id,
                            t.requisition_slip_no,
                            t.date_added,
                            t.techpack_number,
                            t.name,
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='MCDRequisitionIssueDraft(this)' style='width: 120px;' class='btn btn-success btnEdit'  requisition_issue_id='" + clsUtil.EncodeString(t.tran_mcd_requisition_issue_id.ToString()) + "'>Issue</button>" +

                            "</div>"

                        }).ToList();
           
                var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
                return Json(ret_obj);
          


        }


        [HttpPost]
        public async Task<IActionResult> GetSubmittedRequisitionSlipData(MCD_DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _MCDRequisitionIssueService.GetSubmittedRequisitionAccData(request.Start, request.Length,
                request.fiscal_year_id, request.event_id,
                                    request.group_id, request.sub_group_id, request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.issue_no,
                            t.requisition_slip_no,
                            t.date_added,
                            t.techpack_number,
                            t.name,
                            t.tran_mcd_requisition_issue_id,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='MCDRequisitionIssueView(this)' style='width: 120px;' class='btn btn-success btnEdit'  requisition_issue_id='" + clsUtil.EncodeString(t.tran_mcd_requisition_issue_id.ToString()) + "'>View</button>" +

                            "</div>"

                        }).ToList();

         
                var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
                return Json(ret_obj);
          


        }

        [HttpPost]
        public async Task<IActionResult> GetSubmittedRequisitionSlipDataforApproval(MCD_DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _MCDRequisitionIssueService.GetSubmittedRequisitionAccData(request.Start, request.Length,
                request.fiscal_year_id, request.event_id,
                                    request.group_id, request.sub_group_id,request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.issue_no,
                            t.requisition_slip_no,
                            t.date_added,
                            t.techpack_number,
                            t.name,
                            t.tran_mcd_requisition_issue_id,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='MCDRequisitionIssueView(this)' style='width: 120px;' class='btn btn-success btnEdit'  requisition_issue_id='" + clsUtil.EncodeString(t.tran_mcd_requisition_issue_id.ToString()) + "'>View</button>" +

                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);


        }

        [HttpPost]
        public async Task<IActionResult> GetAprrovedRequisitionSlipData(MCD_DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _MCDRequisitionIssueService.GetAprrovedRequisitionSlipDataAccesories(request.Start, request.Length,
                request.fiscal_year_id, request.event_id,
                                    request.group_id, request.sub_group_id,request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.issue_no,
                            t.requisition_slip_no,
                            t.date_added,
                            t.techpack_number,
                            t.name,
                            t.tran_mcd_requisition_issue_id,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='MCDRequisitionIssueView(this)' style='width: 120px;' class='btn btn-success btnEdit'  requisition_issue_id='" + clsUtil.EncodeString(t.tran_mcd_requisition_issue_id.ToString()) + "'>View</button>" +

                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);


        }

        [HttpGet]
        public async Task<IActionResult> MCDRequisitionIssueEdit(string tran_mcd_requisition_issue_id)
        {

            string decoded_tran_mcd_requisition_issue_id = clsUtil.DecodeString(tran_mcd_requisition_issue_id);

            tran_mcd_requisition_issue_DTO data = new tran_mcd_requisition_issue_DTO();

            var model = await _MCDRequisitionIssueService.Get_mcd_requisition_issueAsync(Convert.ToInt64(decoded_tran_mcd_requisition_issue_id));

            data = JsonConvert.DeserializeObject<tran_mcd_requisition_issue_DTO>(JsonConvert.SerializeObject(model));

            data.details = JsonConvert.DeserializeObject<List<tran_mcd_requisition_issue_details_DTO>>(model.tran_mcd_requisition_issue_detail_list);

            ViewBag.SubCategoryList = JsonConvert.DeserializeObject<List<dropdown_entity>>(model.sub_group_dropdown_list).ToList()
          .Select(a =>
                   new SelectListItem
                   {
                       Text = a.text.ToString(),
                       Value = a.id.ToString(),
                   }
                   ).ToList();
            return PartialView("~/Views/MCD/MCDAccesoriesRequisitionIssue/_MCDRequisitionIssueEdit.cshtml", data);

        }

        [HttpGet]
        public async Task<IActionResult> MCDRequisitionIssueView(string tran_mcd_requisition_issue_id)
        {

            string decoded_tran_mcd_requisition_issue_id = clsUtil.DecodeString(tran_mcd_requisition_issue_id);

            tran_mcd_requisition_issue_DTO data = new tran_mcd_requisition_issue_DTO();

            var model = await _MCDRequisitionIssueService.Get_mcd_requisition_issueAsync(Convert.ToInt64(decoded_tran_mcd_requisition_issue_id));

            data = JsonConvert.DeserializeObject<tran_mcd_requisition_issue_DTO>(JsonConvert.SerializeObject(model));
            data.details = JsonConvert.DeserializeObject<List<tran_mcd_requisition_issue_details_DTO>>(model.tran_mcd_requisition_issue_detail_list);
            ViewBag.SubCategoryList = JsonConvert.DeserializeObject<List<dropdown_entity>>(model.sub_group_dropdown_list).ToList()
          .Select(a =>
                   new SelectListItem
                   {
                       Text = a.text.ToString(),
                       Value = a.id.ToString(),
                   }
                   ).ToList();
            return PartialView("~/Views/MCD/MCDAccesoriesRequisitionIssue/_MCDRequisitionIssueView.cshtml", data);

        }

        [HttpGet]
        public async Task<IActionResult> MCDRequisitionIssueViewforApproval(string tran_mcd_requisition_issue_id)
        {

            string decoded_tran_mcd_requisition_issue_id = clsUtil.DecodeString(tran_mcd_requisition_issue_id);

            tran_mcd_requisition_issue_DTO data = new tran_mcd_requisition_issue_DTO();

            var model = await _MCDRequisitionIssueService.Get_mcd_requisition_issueAsync(Convert.ToInt64(decoded_tran_mcd_requisition_issue_id));

            data = JsonConvert.DeserializeObject<tran_mcd_requisition_issue_DTO>(JsonConvert.SerializeObject(model));
            data.details = JsonConvert.DeserializeObject<List<tran_mcd_requisition_issue_details_DTO>>(model.tran_mcd_requisition_issue_detail_list);
            ViewBag.SubCategoryList = JsonConvert.DeserializeObject<List<dropdown_entity>>(model.sub_group_dropdown_list).ToList()
          .Select(a =>
                   new SelectListItem
                   {
                       Text = a.text.ToString(),
                       Value = a.id.ToString(),
                   }
                   ).ToList();
            return PartialView("~/Views/MCD/MCDAccesoriesRequisitionIssue/_MCDRequisitionIssueApprovalView.cshtml", data);

        }


        [HttpPost]
        public async Task<IActionResult> SaveMCDRequisitionIssue([FromBody] tran_mcd_requisition_issue_DTO request)
        {


            var ret = true;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            if (request.tran_mcd_requisition_issue_id == 0)
            {
                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;
            }


            try
            {
                var model = JsonConvert.DeserializeObject<tran_mcd_requisition_issue_entity>(JsonConvert.SerializeObject(request));

                model.event_id = objFilter.event_id;
                model.fiscal_year_id=objFilter.fiscal_year_id;
                model.is_submitted = model.is_submitted;
                model.is_approved = false;
                List<tran_mcd_requisition_issue_details_entity> detail = JsonConvert.DeserializeObject<List<tran_mcd_requisition_issue_details_entity>>(JsonConvert.SerializeObject(request.details));


                ret = await _MCDRequisitionIssueService.SaveAsync(model, detail);

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
        public async Task<IActionResult> UpdateMCDRequisitionIssue([FromBody] tran_mcd_requisition_issue_DTO request)
        {
            var ret = true;


            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            try
            {
                var model = JsonConvert.DeserializeObject<tran_mcd_requisition_issue_entity>(JsonConvert.SerializeObject(request));
                List<tran_mcd_requisition_issue_details_entity> detail = JsonConvert.DeserializeObject<List<tran_mcd_requisition_issue_details_entity>>(JsonConvert.SerializeObject(request.details));


                ret = await _MCDRequisitionIssueService.UpdateAsync(model, detail);

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

