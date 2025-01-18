using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Configuration;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class TranBarcodeController : ExtendedBaseController
    {
        private readonly ILogger<TranBarcodeController> _logger;

        private readonly ITranBarcodeService _TranBarcodeService;
        private readonly IConfiguration _configuration;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITrantechpackService _TechpackService;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside TranBarcodeController !");
            return View();
        }

        public TranBarcodeController(
           IMapper mapper, ILogger<TranBarcodeController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration, ITrantechpackService trantechpackService,
            ITranBarcodeService TranBarcodeService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranBarcodeService = TranBarcodeService;
            _TechpackService = trantechpackService;
            

            _configuration = configuration;


        }

        [HttpGet]
        public async Task<IActionResult> TranBarcodeLanding()
        {

            return View("~/Views/ShopFloor/TranBarcode/TranBarcodeLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranBarcodeNew()
        {
            tran_barcode_DTO model = new tran_barcode_DTO();

            return PartialView("~/Views/ShopFloor/TranBarcode/_TranBarcodeNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranBarcodeEdit(string tran_barcode_id)
        {

            string decoded_tran_barcode_id = clsUtil.DecodeString(tran_barcode_id);

            tran_barcode_DTO model = new tran_barcode_DTO();

            var objmodel = await _TranBarcodeService.GetSingleAsync(Convert.ToInt64(decoded_tran_barcode_id));

            model = JsonConvert.DeserializeObject<tran_barcode_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/ShopFloor/TranBarcode/_TranBarcodeEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranBarcodeView(string tran_barcode_id)
        {

            string decoded_tran_barcode_id = clsUtil.DecodeString(tran_barcode_id);

            tran_barcode_DTO model = new tran_barcode_DTO();

            var objmodel = await _TranBarcodeService.GetSingleAsync(Convert.ToInt64(decoded_tran_barcode_id));

            model = JsonConvert.DeserializeObject<tran_barcode_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/ShopFloor/TranBarcode/_TranBarcodeView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetTranBarcodeData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranBarcodeService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_barcode_id,
                            t.techpack_id,
                            t.color_code,
                            t.style_product_size_group_detid,
                            t.barcode,
                            t.added_by,
                            t.date_added,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditTranBarcode(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  tran_barcode_id='" + clsUtil.EncodeString(t.tran_barcode_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewTranBarcode(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_barcode_id='" + clsUtil.EncodeString(t.tran_barcode_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteTranBarcode(\"" + clsUtil.EncodeString(t.tran_barcode_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetJoinedTranBarcodeData(DtParameters request)
        {
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranBarcodeService.GetAllJoined_TranBarcodeAsync( request.fiscal_year_id, request.event_id, request.Start, request.Length);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.techpack_number,
                            t.color_code,
                           
                            t.size,
                            t.barcode,
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                           
                            "<button type='button' onclick='ViewTranBarcode(this)' class='btn btn-warning btnView'  tran_barcode_id='" + clsUtil.EncodeString(t.tran_barcode_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='DeleteTranBarcode(\"" + clsUtil.EncodeString(t.tran_barcode_id.ToString()) + "\")' class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);
            return Json(ret_obj);

        }




        [HttpPost]
        public async Task<IActionResult> SaveTranBarcode([FromBody] tran_barcode_DTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            try
            {


                ret = await _TranBarcodeService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateTranBarcode([FromBody] tran_barcode_DTO request)
        {
            var ret = true;



            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            try
            {
                var model = JsonConvert.DeserializeObject<tran_barcode_entity>(JsonConvert.SerializeObject(request));

                ret = await _TranBarcodeService.UpdateAsync(model);

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
        public async Task<JsonResult> GetTechpackDet(long techpack_id)
        {


            var pr = _TechpackService.GetAllproc_sp_get_colors_by_techpackAsync(techpack_id).Select(a =>
                 new SelectListItem
                 {
                     Text = a.color_code.ToString(),
                     Value = a.color_code.ToString()
                 }
                 ).ToList();
            return Json(new { data = pr });
        }
        [HttpGet]
        public async Task<JsonResult> GetTechpackColorWiseSizeList(long techpack_id, string color_code)
        {


            var pr = _TechpackService.GetTechpackColorWiseSizeList(techpack_id, color_code);
            return Json(new { data = pr });
        }
    }
}

