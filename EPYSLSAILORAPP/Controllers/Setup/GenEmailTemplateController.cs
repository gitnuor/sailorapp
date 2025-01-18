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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EPYSLSAILORAPP.Controllers
{
    public class GenEmailTemplateController : ExtendedBaseController
    {
        private readonly ILogger<GenEmailTemplateController> _logger;

        private readonly IGenEmailTemplateService _GenEmailTemplateService;
        private readonly IGenEmailTemplateCategoryService _GenEmailTemplateCategoryService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GenEmailTemplateController !");
            return View();
        }

        public GenEmailTemplateController(
           IMapper mapper, ILogger<GenEmailTemplateController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            IGenEmailTemplateService GenEmailTemplateService, IGenEmailTemplateCategoryService GenEmailTemplateCategoryService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenEmailTemplateService = GenEmailTemplateService;
            _GenEmailTemplateCategoryService = GenEmailTemplateCategoryService;
            _configuration = configuration;




        }

        [HttpGet]
        public async Task<IActionResult> GenEmailTemplateLanding()
        {

            return View("~/Views/Setup/GenEmailTemplate/GenEmailTemplateLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GenEmailTemplateNew()
        {
            gen_email_template_DTO model = new gen_email_template_DTO();

            var emailCategory_list = await _GenEmailTemplateCategoryService.GetAllAsync();

            model.GenEmailTemplateCategory_List = (List<SelectListItem>)emailCategory_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.category_name.ToString(),
                                                       Value = a.gen_email_template_category_id.ToString()

                                                   }
                                                   ).ToList();



            return PartialView("~/Views/Setup/GenEmailTemplate/_GenEmailTemplateNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenEmailTemplateEdit(string gen_email_template_id)
        {

            string decoded_gen_email_template_id = clsUtil.DecodeString(gen_email_template_id);

            gen_email_template_DTO model = new gen_email_template_DTO();

            var objmodel = await _GenEmailTemplateService.GetSingleAsync(Convert.ToInt64(decoded_gen_email_template_id));

            model = JsonConvert.DeserializeObject<gen_email_template_DTO>(JsonConvert.SerializeObject(objmodel));

            //var emailCategory_list = await _GenEmailTemplateCategoryService.GetAllAsync();

            //model.GenEmailTemplateCategory_List = (List<SelectListItem>)emailCategory_list
            //    .Select(a =>
            //                                       new SelectListItem
            //                                       {
            //                                           Text = a.category_name.ToString(),
            //                                           Value = a.gen_email_template_category_id.ToString()

            //                                       }
            //                                       ).ToList();

            return PartialView("~/Views/Setup/GenEmailTemplate/_GenEmailTemplateEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenEmailTemplateView(string gen_email_template_id)
        {

            string decoded_gen_email_template_id = clsUtil.DecodeString(gen_email_template_id);

            gen_email_template_DTO model = new gen_email_template_DTO();

            var objmodel = await _GenEmailTemplateService.GetSingleAsync(Convert.ToInt64(decoded_gen_email_template_id));

            model = JsonConvert.DeserializeObject<gen_email_template_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Setup/GenEmailTemplate/_GenEmailTemplateView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetGenEmailTemplateData(DtParameters request)
        {

            var records = await _GenEmailTemplateService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.gen_email_template_id,
                            t.email_template_name,
                            t.email_template_html,
                            t.added_by,
                            t.date_added,
                            t.updated_by,
                            t.date_updated,
                            t.gen_email_template_category_id,
                            t.category_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenEmailTemplate(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  gen_email_template_id='" + clsUtil.EncodeString(t.gen_email_template_id.ToString()) + "'>Edit</button>" 
                            //"<button type='button' onclick='ViewGenEmailTemplate(this)' style='width: 120px;' class='btn btn-secondary btnView'  gen_email_template_id='" + clsUtil.EncodeString(t.gen_email_template_id.ToString()) + "'>View</button>" +
                            //"<button type='button' onclick='DeleteGenEmailTemplate(\"" + clsUtil.EncodeString(t.gen_email_template_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            //"</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetJoinedGenEmailTemplateData(DtParameters request)
        {

            var records = await _GenEmailTemplateService.GetAllJoined_GenEmailTemplateAsync(request.Start, request.Length);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.email_template_html,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenEmailTemplate(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  gen_email_template_id='" + clsUtil.EncodeString(t.gen_email_template_id.ToString()) + "'>Edit</button>" 
                            //"<button type='button' onclick='ViewGenEmailTemplate(this)' style='width: 120px;' class='btn btn-secondary btnView'  gen_email_template_id='" + clsUtil.EncodeString(t.gen_email_template_id.ToString()) + "'>View</button>" +
                            //"<button type='button' onclick='DeleteGenEmailTemplate(\"" + clsUtil.EncodeString(t.gen_email_template_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            //"</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }




        [HttpPost]
        public async Task<IActionResult> SaveGenEmailTemplate([FromBody] gen_email_template_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;




            try
            {
                var model = JsonConvert.DeserializeObject<gen_email_template_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenEmailTemplateService.SaveAsync(model);

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
        public async Task<IActionResult> UpdateGenEmailTemplate([FromBody] gen_email_template_DTO request)
        {
            var ret = true;



            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;


            try
            {
                var model = JsonConvert.DeserializeObject<gen_email_template_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenEmailTemplateService.UpdateAsync(model);

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
        public async Task<IActionResult> DeleteGenEmailTemplate([FromBody] gen_email_template_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;



            try
            {

                string decoded_gen_email_template_id = clsUtil.DecodeString(request.strMasterID);

                request.gen_email_template_id = Convert.ToInt64(decoded_gen_email_template_id);

                ret = await _GenEmailTemplateService.DeleteAsync(request.gen_email_template_id);

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

