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
    public class GenEmailTemplateCategoryController : ExtendedBaseController
    {
        private readonly ILogger<GenEmailTemplateCategoryController> _logger;

        private readonly IGenEmailTemplateCategoryService _GenEmailTemplateCategoryService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GenEmailTemplateCategoryController !");
            return View();
        }

        public GenEmailTemplateCategoryController(
           IMapper mapper, ILogger<GenEmailTemplateCategoryController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            IGenEmailTemplateCategoryService GenEmailTemplateCategoryService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenEmailTemplateCategoryService = GenEmailTemplateCategoryService;
            _configuration = configuration;




        }

        [HttpGet]
        public async Task<IActionResult> GenEmailTemplateCategoryLanding()
        {

            return View("~/Views/Setup/GenEmailTemplateCategory/GenEmailTemplateCategoryLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GenEmailTemplateCategoryNew()
        {
            gen_email_template_category_dto model = new gen_email_template_category_dto();

            return PartialView("~/Views/Setup/GenEmailTemplateCategory/_GenEmailTemplateCategoryNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenEmailTemplateCategoryEdit(string gen_email_template_category_id)
        {

            string decoded_gen_email_template_category_id = clsUtil.DecodeString(gen_email_template_category_id);

            gen_email_template_category_dto model = new gen_email_template_category_dto();

            var objmodel = await _GenEmailTemplateCategoryService.GetSingleAsync(Convert.ToInt64(decoded_gen_email_template_category_id));

            model = JsonConvert.DeserializeObject<gen_email_template_category_dto>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Setup/GenEmailTemplateCategory/_GenEmailTemplateCategoryEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenEmailTemplateCategoryView(string gen_email_template_category_id)
        {

            string decoded_gen_email_template_category_id = clsUtil.DecodeString(gen_email_template_category_id);

            gen_email_template_category_dto model = new gen_email_template_category_dto();

            var objmodel = await _GenEmailTemplateCategoryService.GetSingleAsync(Convert.ToInt64(decoded_gen_email_template_category_id));

            model = JsonConvert.DeserializeObject<gen_email_template_category_dto>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Setup/GenEmailTemplateCategory/_GenEmailTemplateCategoryView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetGenEmailTemplateCategoryData(DtParameters request)
        {

            var records = await _GenEmailTemplateCategoryService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.gen_email_template_category_id,
                            t.category_name,
                            t.added_by,
                            t.date_added,
                            t.updated_by,
                            t.date_updated,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenEmailTemplateCategory(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  gen_email_template_category_id='" + clsUtil.EncodeString(t.gen_email_template_category_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewGenEmailTemplateCategory(this)' style='width: 120px;' class='btn btn-secondary btnView'  gen_email_template_category_id='" + clsUtil.EncodeString(t.gen_email_template_category_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteGenEmailTemplateCategory(\"" + clsUtil.EncodeString(t.gen_email_template_category_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        




        [HttpPost]
        public async Task<IActionResult> SaveGenEmailTemplateCategory([FromBody] gen_email_template_category_dto request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;




            try
            {
                var model = JsonConvert.DeserializeObject<gen_email_template_category_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenEmailTemplateCategoryService.SaveAsync(model);

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
        public async Task<IActionResult> UpdateGenEmailTemplateCategory([FromBody] gen_email_template_category_dto request)
        {
            var ret = true;



            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;


            try
            {
                var model = JsonConvert.DeserializeObject<gen_email_template_category_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenEmailTemplateCategoryService.UpdateAsync(model);

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
        public async Task<IActionResult> DeleteGenEmailTemplateCategory([FromBody] gen_email_template_category_dto request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;



            try
            {

                string decoded_gen_email_template_category_id = clsUtil.DecodeString(request.strMasterID);

                request.gen_email_template_category_id = Convert.ToInt64(decoded_gen_email_template_category_id);

                ret = await _GenEmailTemplateCategoryService.DeleteAsync(request.gen_email_template_category_id);

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

