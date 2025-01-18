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
    public class GenColorController : ExtendedBaseController
    {
        private readonly ILogger<GenColorController> _logger;

        private readonly IGenColorService _GenColorService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        //private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GenColorController !");
            return View();
        }

        public GenColorController(
           IMapper mapper, ILogger<GenColorController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            IGenColorService GenColorService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenColorService = GenColorService;
            //_configuration = configuration;
            

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> GenColorLanding()
        {

            return View("~/Views/Setup/GenColor/GenColorLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GenColorNew()
        {
            gen_color_DTO model = new gen_color_DTO();

            return PartialView("~/Views/Setup/GenColor/_GenColorNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenColorEdit(string gen_color_id)
        {

            string decoded_gen_color_id = clsUtil.DecodeString(gen_color_id);

            gen_color_DTO model = new gen_color_DTO();

            var objmodel = await _GenColorService.GetSingleAsync(Convert.ToInt64(decoded_gen_color_id));

            model = JsonConvert.DeserializeObject<gen_color_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Setup/GenColor/_GenColorEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenColorView(string gen_color_id)
        {

            string decoded_gen_color_id = clsUtil.DecodeString(gen_color_id);

            gen_color_DTO model = new gen_color_DTO();

            var objmodel = await _GenColorService.GetSingleAsync(Convert.ToInt64(decoded_gen_color_id));

            model = JsonConvert.DeserializeObject<gen_color_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Setup/GenColor/_GenColorView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetGenColorData(DtParameters request)
        {

            var records = await _GenColorService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.gen_color_id,
                            t.color_name,
                            t.color_code,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenColor(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  gen_color_id='" + clsUtil.EncodeString(t.gen_color_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewGenColor(this)' style='width: 120px;' class='btn btn-secondary btnView'  gen_color_id='" + clsUtil.EncodeString(t.gen_color_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteGenColor(\"" + clsUtil.EncodeString(t.gen_color_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }


        [HttpPost]
        public async Task<IActionResult> SaveGenColor([FromBody] gen_color_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;




            try
            {
                var model = JsonConvert.DeserializeObject<gen_color_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenColorService.SaveAsync(model);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successfull" : "Data Saving Failed")
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
        public async Task<IActionResult> UpdateGenColor([FromBody] gen_color_DTO request)
        {
            var ret = true;



            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;


            try
            {
                var model = JsonConvert.DeserializeObject<gen_color_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenColorService.UpdateAsync(model);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successfull" : "Data Saving Failed")
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
        public async Task<IActionResult> DeleteGenColor([FromBody] gen_color_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;



            try
            {

                string decoded_gen_color_id = clsUtil.DecodeString(request.strMasterID);

                request.gen_color_id = Convert.ToInt64(decoded_gen_color_id);

                ret = await _GenColorService.DeleteAsync(request.gen_color_id);

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

