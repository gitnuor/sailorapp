using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Security.Claims;
using Web.Core.Frame.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
namespace EPYSLSAILORAPP.Controllers
{
    public class TranFinalInspectionController : ExtendedBaseController
    {
        private readonly ILogger<TranFinalInspectionController> _logger;
        private readonly ITrantechpackService _TechpackService;
        private readonly ITranFinalInspectionService _TranFinalInspectionService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside Final Inspection Controller !");
            return View();
        }

        public TranFinalInspectionController(
           IMapper mapper, ILogger<TranFinalInspectionController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            ITranFinalInspectionService TranFinalInspectionService, ITrantechpackService trantechpackService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranFinalInspectionService = TranFinalInspectionService;
            _TechpackService = trantechpackService;
            

        
            _configuration = configuration;

        }

        [HttpGet]
        public async Task<IActionResult> TranFinalInspectionLanding()
        {

            return View("~/Views/ShopFloor/TranFinalInspection/TranFinalInspectionLanding.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> FinalInspectionApprovalLanding()
        {

            return View("~/Views/ShopFloor/TranFinalInspection/ApprovalFinalInspectionLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranFinalInspectionNew()
        {
            tran_final_inspection_DTO model = new tran_final_inspection_DTO();

            return PartialView("~/Views/ShopFloor/TranFinalInspection/_TranFinalInspectionNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranFinalInspectionEdit(string tran_final_inspection_id)
        {

            string decoded_tran_final_inspection_id = clsUtil.DecodeString(tran_final_inspection_id);

            tran_final_inspection_DTO model = new tran_final_inspection_DTO();

            var objmodel = await _TranFinalInspectionService.GetSingleAsync(Convert.ToInt64(decoded_tran_final_inspection_id));

            objmodel.TranFinalInspectionDetails_List = JsonConvert.DeserializeObject<List<tran_final_inspection_details_DTO>>(objmodel.inspection_details);

           return PartialView("~/Views/ShopFloor/TranFinalInspection/_TranFinalInspectionEdit.cshtml", objmodel);

        }

        [HttpGet]
        public async Task<IActionResult> TranFinalInspectionView(string tran_final_inspection_id)
        {

            string decoded_tran_final_inspection_id = clsUtil.DecodeString(tran_final_inspection_id);

            tran_final_inspection_DTO model = new tran_final_inspection_DTO();

            var objmodel = await _TranFinalInspectionService.GetSingleAsync(Convert.ToInt64(decoded_tran_final_inspection_id));

            objmodel.TranFinalInspectionDetails_List = JsonConvert.DeserializeObject<List<tran_final_inspection_details_DTO>>(objmodel.inspection_details);

            return PartialView("~/Views/ShopFloor/TranFinalInspection/_TranFinalInspectionView.cshtml", objmodel);

        }


        [HttpPost]
        public async Task<IActionResult> GetTranFinalInspectionDraftedData(DtParameters request)
        {

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranFinalInspectionService.GetTranFinalInspectionDraftedData(request.Start,request.Length, request.fiscal_year_id,request.event_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_final_inspection_id,
                            t.final_inspection_no,
                            t.final_inspection_date,
                            t.finishing_production_process_id,
                            t.techpack_id,
                            t.note,t.techpack_number,
                            t.event_title,
                            t.event_id,
           


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditTranFinalInspection(this)' class='btn btn-primary btnEdit'  tran_final_inspection_id='" + clsUtil.EncodeString(t.tran_final_inspection_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +
                            
                            
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }
        [HttpPost]
        public async Task<IActionResult> GetTranFinalInspectionSubmittedData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranFinalInspectionService.GetTranFinalInspectionSubmittedData(request.Start, request.Length, request.fiscal_year_id, request.event_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_final_inspection_id,
                            t.final_inspection_no,
                            t.final_inspection_date,
                            t.finishing_production_process_id,
                            t.techpack_id,
                            t.note,
                            t.techpack_number,
                            t.event_title,
                            t.event_id,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                           
                            "<button type='button' onclick='ViewTranFinalInspection(this)' class='btn btn-warning btnView'  tran_final_inspection_id='" + clsUtil.EncodeString(t.tran_final_inspection_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }
        [HttpPost]
        public async Task<IActionResult> GetTranFinalInspectionApprovedData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranFinalInspectionService.GetTranFinalInspectionApprovedData(request.Start, request.Length, request.fiscal_year_id, request.event_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_final_inspection_id,
                            t.final_inspection_no,
                            t.final_inspection_date,
                            t.finishing_production_process_id,
                            t.techpack_id,
                            t.note,
                            t.techpack_number,
                            t.event_title,
                            t.event_id,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                           
                            "<button type='button' onclick='ViewTranFinalInspection(this)' class='btn btn-warning btnView'  tran_final_inspection_id='" + clsUtil.EncodeString(t.tran_final_inspection_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }
        [HttpPost]
        public async Task<IActionResult> GetJoinedTranFinalInspectionData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranFinalInspectionService.GetAllJoined_TranFinalInspectionAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.final_inspection_no,
                            t.note,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditTranFinalInspection(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  tran_final_inspection_id='" + clsUtil.EncodeString(t.tran_final_inspection_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewTranFinalInspection(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_final_inspection_id='" + clsUtil.EncodeString(t.tran_final_inspection_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteTranFinalInspection(\"" + clsUtil.EncodeString(t.tran_final_inspection_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }




        [HttpPost]
        public async Task<IActionResult> SaveTranFinalInspection([FromBody] tran_final_inspection_DTO request)
        {

            var ret = true;

            request.added_by = sec_object.userid.Value;
            request.date_added = DateTime.Now;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            request.tran_final_inspection_details = JArray.Parse(JsonConvert.SerializeObject(request.TranFinalInspectionDetails_List));


            try
            {


                ret = await _TranFinalInspectionService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateTranFinalInspection([FromBody] tran_final_inspection_DTO request)
        {
            var ret = true;

            

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;


            try
            {

                request.tran_final_inspection_details = JArray.Parse(JsonConvert.SerializeObject(request.TranFinalInspectionDetails_List));
                ret = await _TranFinalInspectionService.UpdateAsync(request);

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
        public async Task<IActionResult> SendForApproval([FromBody] tran_final_inspection_DTO request)
        {
            var ret = true;



            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;


            try
            {

                request.tran_final_inspection_details = JArray.Parse(JsonConvert.SerializeObject(request.TranFinalInspectionDetails_List));
                ret = await _TranFinalInspectionService.SendForApproval(request);

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
        public async Task<IActionResult> DeleteTranFinalInspection([FromBody] tran_final_inspection_DTO request)
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

                string decoded_tran_final_inspection_id = clsUtil.DecodeString(request.strMasterID);

                request.tran_final_inspection_id = Convert.ToInt64(decoded_tran_final_inspection_id);

                ret = await _TranFinalInspectionService.DeleteAsync(request.tran_final_inspection_id);

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

        [HttpGet]
        public async Task<JsonResult> GetTechpackDet(long techpack_id)
        {
            var pr = await _TechpackService.GetAllproc_sp_get_techapack_details_for_final_inspection(techpack_id);

            pr.colors = _TechpackService.GetAllproc_sp_get_colors_by_techpackAsync(techpack_id).Select(a =>
                 new SelectListItem
                 {
                     Text = a.color_code.ToString(),
                     Value = a.color_code.ToString()
                 }
                 ).ToList();
            return Json(new { data = pr });
        }


        [HttpGet]
        public async Task<JsonResult> GetProductionQuantityForFinalInspection(long techpack_id, string color_code)
        {
            var pr = await _TranFinalInspectionService.GetProductionQuantityForFinalInspection(techpack_id, color_code);


            return Json(new { data = pr });
        }



        [HttpPost]
        public async Task<IActionResult> GetTranFinalInspectionApprovalPendingData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranFinalInspectionService.GetTranFinalInspectionSubmittedData( request.Start, request.Length, request.fiscal_year_id, request.event_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_final_inspection_id,
                            t.final_inspection_no,
                            t.final_inspection_date,
                            t.finishing_production_process_id,
                            t.techpack_id,
                            t.note,
                            t.techpack_number,
                            t.event_title,
                            t.event_id,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                             "<button type='button' onclick='EditPendingFinalInspection(this)' class='btn btn-primary btnEdit'  tran_final_inspection_id='" + clsUtil.EncodeString(t.tran_final_inspection_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }


        [HttpGet]
        public async Task<IActionResult> EditPendingFinalInspection(string tran_final_inspection_id)
        {

            string decoded_tran_final_inspection_id = clsUtil.DecodeString(tran_final_inspection_id);

            tran_final_inspection_DTO model = new tran_final_inspection_DTO();

            var objmodel = await _TranFinalInspectionService.GetSingleAsync(Convert.ToInt64(decoded_tran_final_inspection_id));

            objmodel.TranFinalInspectionDetails_List = JsonConvert.DeserializeObject<List<tran_final_inspection_details_DTO>>(objmodel.inspection_details);

            return PartialView("~/Views/ShopFloor/TranFinalInspection/_ApprovalFinalInspectionEdit.cshtml", objmodel);

        }



        [HttpPost]
        public async Task<IActionResult> ApproveTranFinalInspection([FromBody] tran_final_inspection_DTO request)
        {
            var ret = true;



            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;


            try
            {

                request.tran_final_inspection_details = JArray.Parse(JsonConvert.SerializeObject(request.TranFinalInspectionDetails_List));
                ret = await _TranFinalInspectionService.ApproveInspection(request);

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

