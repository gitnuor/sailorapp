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
    public class GenOutletController : ExtendedBaseController
    {
        private readonly ILogger<GenOutletController> _logger;

        private readonly IGenOutletService _GenOutletService;
     

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GenOutletController !");
            return View();
        }

        public GenOutletController(
           IMapper mapper, ILogger<GenOutletController> logger, IHttpContextAccessor contextAccessor,
            IGenOutletService GenOutletService,
            IRPCDbService rpc_db_service, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenOutletService = GenOutletService;

            

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> GenOutletLanding()
        {

            return View("~/Views/Setup/GenOutlet/GenOutletLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GenOutletNew()
        {
            gen_outlet_DTO model = new gen_outlet_DTO();

            return PartialView("~/Views/Setup/GenOutlet/_GenOutletNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenOutletEdit(string outlet_id)
        {

            string decoded_outlet_id = clsUtil.DecodeString(outlet_id);

            gen_outlet_DTO model = new gen_outlet_DTO();

            model = await _GenOutletService.GetSingleAsync(Convert.ToInt64(decoded_outlet_id));

            return PartialView("~/Views/Setup/GenOutlet/_GenOutletEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenOutletView(string outlet_id)
        {

            string decoded_outlet_id = clsUtil.DecodeString(outlet_id);

            gen_outlet_DTO model = new gen_outlet_DTO();

            model = await _GenOutletService.GetSingleAsync(Convert.ToInt64(decoded_outlet_id));

            return PartialView("~/Views/Setup/GenOutlet/_GenOutletView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetGenOutletData(DtParameters request)
        {

            var records = await _GenOutletService.GetAllAsync();

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                string searchValue = request.Search.Value.ToLower();
                records = records.Where(t =>
                    t.outlet_name.ToLower().Contains(searchValue)
                    
                 
                ).ToList();
            }

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.outlet_id,
                            t.outlet_code,
                            t.outlet_name,
                            t.outlet_description,
                            t.district_id,
                            t.division_id,
                            t.outlet_address,
                            t.is_active,
                            t.outlet_logo_path,
                            t.sequence_no,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenOutlet(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  outlet_id='" + clsUtil.EncodeString(t.outlet_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewGenOutlet(this)' style='width: 120px;' class='btn btn-secondary btnView'  outlet_id='" + clsUtil.EncodeString(t.outlet_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteGenOutlet(\"" + clsUtil.EncodeString(t.outlet_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }



        [HttpPost]
        public async Task<IActionResult> SaveGenOutlet([FromBody] gen_outlet_DTO request)
        {
            request.added_by = sec_object.userid.Value;
            request.date_added = DateTime.Now;
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                if (request.outlet_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;
                }

            }

            try
            {
                var model = JsonConvert.DeserializeObject<gen_outlet_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenOutletService.SaveAsync(model);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Data Updated Successfully" : "Data Saving Failed")
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
        public async Task<IActionResult> UpdateGenOutlet([FromBody] gen_outlet_DTO request)
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
                var model = JsonConvert.DeserializeObject<gen_outlet_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenOutletService.UpdateAsync(model);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Data Updated Successfully" : "Data Saving Failed")
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
        public async Task<IActionResult> DeleteGenOutlet([FromBody] gen_outlet_DTO request)
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

                string decoded_outlet_id = clsUtil.DecodeString(request.strMasterID);

                request.outlet_id = Convert.ToInt64(decoded_outlet_id);

                ret = await _GenOutletService.DeleteAsync(request.outlet_id.Value);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Data Deleted Successfully" : "Data Deletion Failed")
                });
            }
            catch (Exception ex)
            {
                ret = false;

                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    {
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
    }
}

