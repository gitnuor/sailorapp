using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
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
    public class TranPackingListController : ExtendedBaseController
    {
        private readonly ILogger<TranPackingListController> _logger;

        private readonly ITranPackingListService _TranPackingListService;
        private readonly ITrantechpackService _TechpackService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside Packing List Controller !");
            return View();
        }

        public TranPackingListController(
           IMapper mapper, ILogger<TranPackingListController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration, ITrantechpackService trantechpackService,
            ITranPackingListService TranPackingListService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _TechpackService = trantechpackService;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranPackingListService = TranPackingListService;
            _configuration = configuration;
            



        }

        [HttpGet]
        public async Task<IActionResult> TranPackingListLanding()
        {

            return View("~/Views/ShopFloor/TranPackingList/TranPackingListLanding.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> ApprovalTranPackingListLanding()
        {

            return View("~/Views/ShopFloor/TranPackingList/ApprovalTranPackingListLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranPackingListNew()
        {
            tran_packing_list_DTO model = new tran_packing_list_DTO();

            return PartialView("~/Views/ShopFloor/TranPackingList/_TranPackingListNew.cshtml", model);

        }
        [HttpGet]
        public async Task<IActionResult> AddStyle()
        {


            return PartialView("~/Views/ShopFloor/TranPackingList/_AddTechpack.cshtml");

        }

        [HttpGet]
        public async Task<IActionResult> TranPackingListEdit(string tran_packing_list_id)
        {

            string decoded_tran_packing_list_id = clsUtil.DecodeString(tran_packing_list_id);

            tran_packing_list_DTO model = new tran_packing_list_DTO();

            rpc_proc_sp_get_data_tran_packing_list_by_id_DTO objmodel = await _TranPackingListService.GetSingleAsync(Convert.ToInt64(decoded_tran_packing_list_id));

            model.packing_list_date = objmodel.packing_list_date;
            model.note = objmodel.note;
            model.packing_list_no = objmodel.packing_list_no;
            model.tran_packing_list_details = JsonConvert.DeserializeObject<List<tran_packing_list_details_DTO>>(objmodel.packing_details);
            model.tran_packing_list_id = Convert.ToInt64(decoded_tran_packing_list_id);
            return PartialView("~/Views/ShopFloor/TranPackingList/_TranPackingListEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranPackingListView(string tran_packing_list_id)
        {

            string decoded_tran_packing_list_id = clsUtil.DecodeString(tran_packing_list_id);

            tran_packing_list_DTO model = new tran_packing_list_DTO();

            rpc_proc_sp_get_data_tran_packing_list_by_id_DTO objmodel = await _TranPackingListService.GetSingleAsync(Convert.ToInt64(decoded_tran_packing_list_id));

            model.packing_list_date = objmodel.packing_list_date;
            model.note = objmodel.note;
            model.packing_list_no = objmodel.packing_list_no;
            model.is_draft = objmodel.is_draft;
            model.is_submitted = objmodel.is_submitted;
            model.is_approved = objmodel.is_approved;
            model.tran_packing_list_details = JsonConvert.DeserializeObject<List<tran_packing_list_details_DTO>>(objmodel.packing_details);
            model.tran_packing_list_id = Convert.ToInt64(decoded_tran_packing_list_id);
            return PartialView("~/Views/ShopFloor/TranPackingList/_TranPackingListView.cshtml", model);

        }
        [HttpGet]
        public async Task<IActionResult> ApproveTranPackingListView(string tran_packing_list_id)
        {

            string decoded_tran_packing_list_id = clsUtil.DecodeString(tran_packing_list_id);

            tran_packing_list_DTO model = new tran_packing_list_DTO();

            rpc_proc_sp_get_data_tran_packing_list_by_id_DTO objmodel = await _TranPackingListService.GetSingleAsync(Convert.ToInt64(decoded_tran_packing_list_id));

            model.packing_list_date = objmodel.packing_list_date;
            model.note = objmodel.note;
            model.packing_list_no = objmodel.packing_list_no;
            model.is_draft = objmodel.is_draft;
            model.is_submitted = objmodel.is_submitted;
            model.is_approved = objmodel.is_approved;
            model.tran_packing_list_details = JsonConvert.DeserializeObject<List<tran_packing_list_details_DTO>>(objmodel.packing_details);
            model.tran_packing_list_id = Convert.ToInt64(decoded_tran_packing_list_id);
            return PartialView("~/Views/ShopFloor/TranPackingList/_ApproveTranPackingListView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetTranPackingListData(DtParameters request)
        {

            request.fiscal_year_id = Fiscal_Year_ID;

            request.event_id = Event_ID;

            var records = await _TranPackingListService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_packing_list_id,
                            t.packing_list_no,
                            t.packing_list_date,
                            t.note,
                            t.is_submitted,
                            t.is_approved,
                            t.is_draft,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewTranPackingList(this)'  class='btn btn-warning btnView'  tran_packing_list_id='" + clsUtil.EncodeString(t.tran_packing_list_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='DeleteTranPackingList(\"" + clsUtil.EncodeString(t.tran_packing_list_id.ToString()) + "\")' class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetAllSubmittedDataAsync(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranPackingListService.GetAllSubmittedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_packing_list_id,
                            t.packing_list_no,
                            t.packing_list_date,
                            t.note,
                            t.is_submitted,
                            t.is_approved,
                            t.is_draft,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewTranPackingList(this)'  class='btn btn-warning btnView'  tran_packing_list_id='" + clsUtil.EncodeString(t.tran_packing_list_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }
        [HttpPost]
        public async Task<IActionResult> GetAllPendingDataAsync(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranPackingListService.GetAllSubmittedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_packing_list_id,
                            t.packing_list_no,
                            t.packing_list_date,
                            t.note,
                            t.is_submitted,
                            t.is_approved,
                            t.is_draft,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewTranPackingList(this)'  class='btn btn-primary btnView'  tran_packing_list_id='" + clsUtil.EncodeString(t.tran_packing_list_id.ToString()) + "'><i class='fa fa-check' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }
        [HttpPost]
        public async Task<IActionResult> GetAllApprovedDataAsync(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranPackingListService.GetAllApprovedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_packing_list_id,
                            t.packing_list_no,
                            t.packing_list_date,
                            t.note,
                            t.is_submitted,
                            t.is_approved,
                            t.is_draft,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewTranPackingList(this)'  class='btn btn-warning btnView'  tran_packing_list_id='" + clsUtil.EncodeString(t.tran_packing_list_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetJoinedTranPackingListData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _TranPackingListService.GetAllJoined_TranPackingListAsync(request.Start, request.Length,request.fiscal_year_id,request.event_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.packing_list_no,
                            t.note,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditTranPackingList(this)'  class='btn btn-warning btnEdit'  tran_packing_list_id='" + clsUtil.EncodeString(t.tran_packing_list_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewTranPackingList(this)'  class='btn btn-secondary btnView'  tran_packing_list_id='" + clsUtil.EncodeString(t.tran_packing_list_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteTranPackingList(\"" + clsUtil.EncodeString(t.tran_packing_list_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }




        [HttpPost]
        public async Task<IActionResult> SaveTranPackingList([FromBody] tran_packing_list_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;



            try
            {


                ret = await _TranPackingListService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateTranPackingList([FromBody] tran_packing_list_DTO request)
        {
            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;


            try
            {
                var model = JsonConvert.DeserializeObject<tran_packing_list_entity>(JsonConvert.SerializeObject(request));

                ret = await _TranPackingListService.UpdateAsync(model);

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
        public async Task<IActionResult> DeleteTranPackingList([FromBody] tran_packing_list_DTO request)
        {


            var ret = true;



            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;



            try
            {

                string decoded_tran_packing_list_id = clsUtil.DecodeString(request.strMasterID);

                request.tran_packing_list_id = Convert.ToInt64(decoded_tran_packing_list_id);

                ret = await _TranPackingListService.DeleteAsync(request.tran_packing_list_id);

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


        [HttpPost]
        public async Task<IActionResult> SendForApproval([FromBody] tran_packing_list_DTO request)
        {


            var ret = true;



            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;



            try
            {



                ret = await _TranPackingListService.SendForApproval(request.tran_packing_list_id);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Data Approved Successful" : "Data Approved Failed")
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
        public async Task<IActionResult> Approve([FromBody] tran_packing_list_DTO request)
        {


            var ret = true;



            request.approved_by = sec_object.userid.Value;

            request.approved_date = DateTime.Now;



            try
            {



                ret = await _TranPackingListService.Approve(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Data Approved Successful" : "Data Approved Failed")
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


            var pr = _TechpackService.GetAllproc_sp_get_techpack_data_for_packing_list(techpack_id);
            return Json(new { data = pr });
        }

    }
}

