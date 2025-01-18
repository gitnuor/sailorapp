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
    public class GenArifController : BaseController
    {
        private readonly ILogger<GenArifController> _logger;

        private readonly IGenArifService _GenArifService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GenArifController !");
            return View();
        }

        public GenArifController(
           IMapper mapper, ILogger<GenArifController> logger, IHttpContextAccessor contextAccessor,
            IGenArifService GenArifService,
            IRPCDbService rpc_db_service)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenArifService = GenArifService;

            

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> GenArifLanding()
        {

            return View("~/Views/Business Plan/GenArif/GenArifLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GenArifNew()
        {
            gen_arif_DTO model = new gen_arif_DTO();

            return PartialView("~/Views/Business Plan/GenArif/_GenArifNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenArifEdit(string gen_arif_id)
        {

            string decoded_gen_arif_id = clsUtil.DecodeString(gen_arif_id);

            gen_arif_DTO model = new gen_arif_DTO();

            var objmodel = await _GenArifService.GetSingleAsync(Convert.ToInt64(decoded_gen_arif_id));

            model = JsonConvert.DeserializeObject<gen_arif_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Business Plan/GenArif/_GenArifEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenArifView(string gen_arif_id)
        {

            string decoded_gen_arif_id = clsUtil.DecodeString(gen_arif_id);

            gen_arif_DTO model = new gen_arif_DTO();

            var objmodel = await _GenArifService.GetSingleAsync(Convert.ToInt64(decoded_gen_arif_id));

            model = JsonConvert.DeserializeObject<gen_arif_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Business Plan/GenArif/_GenArifView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetGenArifData(DtParameters request)
        {

            var records = await _GenArifService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.gen_arif_id,
                            t.bank_name,
                            t.bank_short_name,
                            t.is_used,
                            t.is_local,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,
                            t.arif_details_1,
                            t.arif_details_2,
                            t.unit_id,
                            t.district_id,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenArif(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  gen_arif_id='" + clsUtil.EncodeString(t.gen_arif_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewGenArif(this)' style='width: 120px;' class='btn btn-secondary btnView'  gen_arif_id='" + clsUtil.EncodeString(t.gen_arif_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteGenArif(\"" + clsUtil.EncodeString(t.gen_arif_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetJoinedGenArifData(DtParameters request)
        {

            var records = await _GenArifService.GetAllJoined_GenArifAsync(request.Start, request.Length);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.bank_name,
                            t.bank_short_name,
                            t.is_used,
                            t.is_local,
                            t.arif_details_1,
                            t.arif_details_2,
                            t.unit_id,
                            t.district_id,
                            t.gen_arif_details1_id,
                            t.details1,
                            t.details2,
                            t.current_state,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenArif(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  gen_arif_id='" + clsUtil.EncodeString(t.gen_arif_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewGenArif(this)' style='width: 120px;' class='btn btn-secondary btnView'  gen_arif_id='" + clsUtil.EncodeString(t.gen_arif_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteGenArif(\"" + clsUtil.EncodeString(t.gen_arif_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }




        [HttpPost]
        public async Task<IActionResult> SaveGenArif([FromBody] gen_arif_DTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                if (request.gen_arif_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;
                }

            }

            try
            {
                var model = JsonConvert.DeserializeObject<gen_arif_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenArifService.SaveAsync(model);

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
        public async Task<IActionResult> UpdateGenArif([FromBody] gen_arif_DTO request)
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
                var model = JsonConvert.DeserializeObject<gen_arif_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenArifService.UpdateAsync(model);

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
        public async Task<IActionResult> DeleteGenArif([FromBody] gen_arif_DTO request)
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

                string decoded_gen_arif_id = clsUtil.DecodeString(request.strMasterID);

                request.gen_arif_id = Convert.ToInt64(decoded_gen_arif_id);

                ret = await _GenArifService.DeleteAsync(request.gen_arif_id.Value);

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


    }
}

