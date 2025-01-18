using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Application.DTO;
using System.Security.Claims;
using EPYSLSAILORAPP.Application.Interface;
using BDO.Core.Base;
using Web.Core.Frame.Helpers;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.SystemServices;
using EPYSLSAILORAPP.Domain.DTO;
using ServiceStack;
using Serilog.Context;

using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Controllers
{
    public class GenItemStockMasterController : BaseController
    {
        private readonly ILogger<GenItemStockMasterController> _logger;

        private readonly IGenItemStockMasterService _GenItemStockMasterService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GenItemStockMasterController !");
            return View();
        }

        public GenItemStockMasterController(
           IMapper mapper, ILogger<GenItemStockMasterController> logger, IHttpContextAccessor contextAccessor,
            IGenItemStockMasterService GenItemStockMasterService,
            IRPCDbService rpc_db_service)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenItemStockMasterService = GenItemStockMasterService;

            

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> GenItemStockMasterLanding()
        {

            return View("~/Views/Setup/GenItemStockMaster/GenItemStockMasterLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GenItemStockMasterNew()
        {
            gen_item_stock_master_DTO model = new gen_item_stock_master_DTO();

            return PartialView("~/Views/Setup/GenItemStockMaster/_GenItemStockMasterNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenItemStockMasterEdit(string item_stock_master_id)
        {

            string decoded_item_stock_master_id = clsUtil.DecodeString(item_stock_master_id);

            gen_item_stock_master_DTO model = new gen_item_stock_master_DTO();

            var objmodel = await _GenItemStockMasterService.GetSingleAsync(Convert.ToInt64(decoded_item_stock_master_id),null);

            model = JsonConvert.DeserializeObject<gen_item_stock_master_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Setup/GenItemStockMaster/_GenItemStockMasterEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenItemStockMasterView(string item_stock_master_id)
        {

            string decoded_item_stock_master_id = clsUtil.DecodeString(item_stock_master_id);

            gen_item_stock_master_DTO model = new gen_item_stock_master_DTO();

            var objmodel = await _GenItemStockMasterService.GetSingleAsync(Convert.ToInt64(decoded_item_stock_master_id),null);

            model = JsonConvert.DeserializeObject<gen_item_stock_master_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Setup/GenItemStockMaster/_GenItemStockMasterView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetGenItemStockMasterData(DtParameters request)
        {

            var records = await _GenItemStockMasterService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.item_stock_master_id,
                            t.item_master_id,
                            t.tran_techpack_id,
                            t.measurement_unit_detail_id,
                            t.opening_quantity,
                            t.total_received_quantity,
                            t.total_allocated_quantity,
                            t.total_issued_quantity,
                            t.total_failed_quantity,
                            t.total_floor_return_quantity,
                            t.total_quarantine_quantity,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,
                            t.available_stock_quantity,
                            t.total_stock_quantity,
                            t.item_name,
                            t.unit_detail_title,
                            t.tran_sample_order_number,
                            t.style_code,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenItemStockMaster(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  item_stock_master_id='" + clsUtil.EncodeString(t.item_stock_master_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewGenItemStockMaster(this)' style='width: 120px;' class='btn btn-secondary btnView'  item_stock_master_id='" + clsUtil.EncodeString(t.item_stock_master_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteGenItemStockMaster(\"" + clsUtil.EncodeString(t.item_stock_master_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.FirstOrDefault().total_rows.Value, data);
            return Json(ret_obj);

        }

       




        [HttpPost]
        public async Task<IActionResult> SaveGenItemStockMaster([FromBody] gen_item_stock_master_DTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                if (request.item_stock_master_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;
                }

            }

            try
            {
                var model = JsonConvert.DeserializeObject<gen_item_stock_master_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenItemStockMasterService.SaveAsync(model);

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
        public async Task<IActionResult> UpdateGenItemStockMaster([FromBody] gen_item_stock_master_DTO request)
        {
            var ret = true;

            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

                request.updated_by = sec_object.userid.Value;

                request.date_updated = DateTime.Now;
            }

            try
            {
                var model = JsonConvert.DeserializeObject<gen_item_stock_master_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenItemStockMasterService.UpdateAsync(model);

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

