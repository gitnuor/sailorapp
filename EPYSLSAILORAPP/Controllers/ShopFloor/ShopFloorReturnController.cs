using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Context;
using ServiceStack;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class ShopFloorReturnController : ExtendedBaseController
    {
        private readonly ILogger<ShopFloorReturnController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IShopFloorReturnService _ShopFloorReturnService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside ShopFloorReturnController !");
            return View();
        }

        public ShopFloorReturnController(
           IMapper mapper, ILogger<ShopFloorReturnController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            IShopFloorReturnService ShopFloorReturnService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor; _configuration = configuration;
            _ShopFloorReturnService = ShopFloorReturnService; _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> ShopFloorReturnLanding()
        {

            return View("~/Views/ShopFloor/ShopFloorReturn/ShopFloorReturnLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> ShopFloorReturnNew()
        {

            tran_shop_floor_return_DTO model = new tran_shop_floor_return_DTO();



            {


                model.return_by = sec_object.username;


            }
            model.date_added = DateTime.Now;
            return PartialView("~/Views/ShopFloor/ShopFloorReturn/_ShopFloorReturnNew.cshtml", model);

        }

        [HttpGet]
        public async Task<JsonResult> GetRequisitionIssueData(long tran_mcd_requisition_issue_id)
        {


            var model = await _ShopFloorReturnService.Get_mcd_requisition_issueAsync(tran_mcd_requisition_issue_id);

            tran_mcd_requisition_issue_DTO data = JsonConvert.DeserializeObject<tran_mcd_requisition_issue_DTO>(JsonConvert.SerializeObject(model));
            data.details = JsonConvert.DeserializeObject<List<tran_mcd_requisition_issue_details_DTO>>(model.tran_mcd_requisition_issue_detail_list);

            return Json(new { data = data, details = data.details });


        }
        [HttpGet]
        public async Task<IActionResult> ShopFloorReturnEdit(string tran_shop_floor_return_id)
        {

            string decoded_tran_shop_floor_return_id = clsUtil.DecodeString(tran_shop_floor_return_id);

            tran_shop_floor_return_DTO model = new tran_shop_floor_return_DTO();

            model = await _ShopFloorReturnService.GetSingleAsyncByReturnId(Convert.ToInt64(decoded_tran_shop_floor_return_id));
            model.tran_shop_floor_return_id = Convert.ToInt64(decoded_tran_shop_floor_return_id);
            model.details = JsonConvert.DeserializeObject<List<tran_shop_floor_return_details_DTO>>(model.tran_return_detail_list);
            return PartialView("~/Views/ShopFloor/ShopFloorReturn/_ShopFloorReturnEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> ShopFloorReturnView(string tran_shop_floor_return_id)
        {

            string decoded_tran_shop_floor_return_id = clsUtil.DecodeString(tran_shop_floor_return_id);

            tran_shop_floor_return_DTO model = new tran_shop_floor_return_DTO();

            model = await _ShopFloorReturnService.GetSingleAsyncByReturnId(Convert.ToInt64(decoded_tran_shop_floor_return_id));
            model.tran_shop_floor_return_id = Convert.ToInt64(decoded_tran_shop_floor_return_id);
            model.details = JsonConvert.DeserializeObject<List<tran_shop_floor_return_details_DTO>>(model.tran_return_detail_list);

            return PartialView("~/Views/ShopFloor/ShopFloorReturn/_ShopFloorReturnView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetShopFloorReturnData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _ShopFloorReturnService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_shop_floor_return_id,
                            t.return_no,
                            t.requisition_slip_id,
                            t.requisition_slip_no,
                            t.tran_mcd_requisition_issue_id,
                            t.techpack_id,
                            t.remarks,
                            t.is_submitted,
                            t.is_acknowledged,
                            t.acknowledged_by,
                            t.acknowledged_date,
                            t.acknowledged_remarks,
                            t.gen_item_structure_group_id,
                            t.gen_item_structure_group_sub_id,
                            t.event_id,
                            t.fiscal_year_id,
                            t.added_by,
                            t.date_added,
                            t.updated_by,
                            t.date_updated,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditShopFloorReturn(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  tran_shop_floor_return_id='" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewShopFloorReturn(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_shop_floor_return_id='" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteShopFloorReturn(\"" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetJoinedShopFloorReturnData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _ShopFloorReturnService.GetAllJoined_ShopFloorReturnAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id);
            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_shop_floor_return_id,
                            t.return_no,
                            t.requisition_slip_no,
                            t.issue_no,
                            t.techpack_number,




                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditShopFloorReturn(this)'  class='btn btn-primary btnEdit'  tran_shop_floor_return_id='" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='ViewShopFloorReturn(this)'  class='btn btn-warning btnView'  tran_shop_floor_return_id='" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='DeleteShopFloorReturn(\"" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "\")'  class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetApprovedShopFloorReturnData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _ShopFloorReturnService.GetApprovedShopFloorReturnData(request.Start, request.Length, request.fiscal_year_id, request.event_id,request.Search.Value);
            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_shop_floor_return_id,
                            t.return_no,
                            t.requisition_slip_no,
                            t.issue_no,
                            t.techpack_number,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewShopFloorReturn(this)'  class='btn btn-warning btnView'  tran_shop_floor_return_id='" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetRejectedShopFloorReturnData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            var records = await _ShopFloorReturnService.GetRejectedShopFloorReturnData(request.Start, request.Length, request.fiscal_year_id, request.event_id);
            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_shop_floor_return_id,
                            t.return_no,
                            t.requisition_slip_no,
                            t.issue_no,
                            t.techpack_number,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditShopFloorReturn(this)'  class='btn btn-secondary btnEdit'  tran_shop_floor_return_id='" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewShopFloorReturn(this)'  class='btn btn-secondary btnView'  tran_shop_floor_return_id='" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteShopFloorReturn(\"" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "\")'  class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }
        [HttpPost]
        public async Task<IActionResult> GetSubmittedShopFloorReturnData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _ShopFloorReturnService.GetSubmittedShopFloorReturnData(request.Start, request.Length, request.fiscal_year_id, request.event_id,request.Search.Value);
            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_shop_floor_return_id,
                            t.return_no,
                            t.requisition_slip_no,
                            t.issue_no,
                            t.techpack_number,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewShopFloorReturn(this)'  class='btn btn-warning btnView'  tran_shop_floor_return_id='" + clsUtil.EncodeString(t.tran_shop_floor_return_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }




        [HttpPost]
        public async Task<IActionResult> SaveShopFloorReturn([FromBody] tran_shop_floor_return_DTO request)
        {


            var ret = true;
            request.added_by = sec_object.userid.Value;
            request.date_added = DateTime.Now;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            request.is_submitted = 1;


            try
            {

                request.shop_floor_return_details_json = JArray.Parse(JsonConvert.SerializeObject(request.details));


                ret = await _ShopFloorReturnService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateShopFloorReturn([FromBody] tran_shop_floor_return_DTO request)
        {
            var ret = true;
            request.updated_by = sec_object.userid.Value;
            request.date_updated = DateTime.Now;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            try
            {
                var model = JsonConvert.DeserializeObject<tran_shop_floor_return_entity>(JsonConvert.SerializeObject(request));
                List<tran_shop_floor_return_details_entity> details = JsonConvert.DeserializeObject<List<tran_shop_floor_return_details_entity>>(JsonConvert.SerializeObject(request.details));

                ret = await _ShopFloorReturnService.UpdateAsync(model, details);

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
        public async Task<IActionResult> SendForApproval(long tran_shop_floor_return_id)
        {
            var ret = true;

           
            try
            {
              
                ret = await _ShopFloorReturnService.SendForApproval(tran_shop_floor_return_id);

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

