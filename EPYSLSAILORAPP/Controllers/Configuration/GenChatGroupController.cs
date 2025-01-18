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
using static System.Runtime.InteropServices.JavaScript.JSType;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EPYSLSAILORAPP.Controllers
{
    public class GenChatGroupController : ExtendedBaseController
    {
        private readonly ILogger<GenChatGroupController> _logger;

        private readonly IGenChatGroupService _GenChatGroupService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GenChatGroupController !");
            return View();
        }

        public GenChatGroupController(IHostingEnvironment hostingEnvironment,
           IMapper mapper, ILogger<GenChatGroupController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            IGenChatGroupService GenChatGroupService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration,true)
        {
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenChatGroupService = GenChatGroupService;
            _configuration = configuration;
            

        }

        [HttpGet]
        public async Task<IActionResult> GenChatGroupLanding()
        {

            return View("~/Views/Configuration/GenChatGroup/GenChatGroupLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GenChatGroupNew()
        {
            gen_chat_group_DTO model = new gen_chat_group_DTO();

            return PartialView("~/Views/Configuration/GenChatGroup/_GenChatGroupNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenChatGroupEdit(string chat_group_id)
        {

            string decoded_chat_group_id = clsUtil.DecodeString(chat_group_id);

            gen_chat_group_DTO model = new gen_chat_group_DTO();

            var objmodel = await _GenChatGroupService.GetSingleAsync(Convert.ToInt64(decoded_chat_group_id));

            model = JsonConvert.DeserializeObject<gen_chat_group_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Configuration/GenChatGroup/_GenChatGroupEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenChatGroupView(string chat_group_id)
        {

            string decoded_chat_group_id = clsUtil.DecodeString(chat_group_id);

            gen_chat_group_DTO model = new gen_chat_group_DTO();

            var objmodel = await _GenChatGroupService.GetSingleAsync(Convert.ToInt64(decoded_chat_group_id));

            model = JsonConvert.DeserializeObject<gen_chat_group_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Configuration/GenChatGroup/_GenChatGroupView.cshtml", model);

        }


        private async Task<string> getImageHtml(gen_chat_group_entity ent)
        {
            var str = "<div class='row'>";

            if (!string.IsNullOrEmpty(ent.group_users))
            {
                var List = JsonConvert.DeserializeObject<List<gen_chat_user>>(ent.group_users);

                foreach (var file in List)
                {

                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", "emp_pic") + "/" + file.emp_pic;

                    if (System.IO.File.Exists(Path.Combine(filePath)))
                    {
                        var bytes = System.IO.File.ReadAllBytes(filePath);

                        var base64string = Convert.ToBase64String(bytes);

                        var file_extension = Path.GetExtension(file.emp_pic).Trim('.');

                        var image_source = $"data:image/{file_extension};base64,{base64string}";

                        str+= @"<div class='col-md-2'><span class='single_employee' style='width:100%;text-align:center;display:block'  emp_pic='" + file.emp_pic.ToString() + "' emp_name='" + file.name.ToString() + "' emp_id='" + file.user_name.ToString() + "'><img style='display: inline-block;height:80px;width:90px;' src=" + image_source + " /><br/> " + file.name + "(" + file.user_name.ToString() + ")</span></div>";

                    }
                }

                str += "</div>";
            }
           
            return str;
            
        }

        [HttpPost]
        public async Task<IActionResult> GetGenChatGroupData(DtParameters request)
        {

            var records = await _GenChatGroupService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.chat_group_id,
                            t.chat_group_name,
                            //t.group_users,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,
                            userimage= getImageHtml(t).Result,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenChatGroup(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  chat_group_id='" + clsUtil.EncodeString(t.chat_group_id.ToString()) + "'>Edit</button>" +
                            
                            "<button type='button' onclick='DeleteGenChatGroup(\"" + clsUtil.EncodeString(t.chat_group_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }




        [HttpPost]
        public async Task<IActionResult> SaveGenChatGroup([FromBody] gen_chat_group_DTO request)
        {


            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            try
            {
               // var model = JsonConvert.DeserializeObject<gen_chat_group_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenChatGroupService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateGenChatGroup([FromBody] gen_chat_group_DTO request)
        {
            var ret = true;



            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;


            try
            {
                //var model = JsonConvert.DeserializeObject<gen_chat_group_entity>(JsonConvert.SerializeObject(request));

                ret = await _GenChatGroupService.UpdateAsync(request);

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
        public async Task<IActionResult> DeleteGenChatGroup([FromBody] gen_chat_group_DTO request)
        {


            var ret = true;

            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            try
            {

                string decoded_chat_group_id = clsUtil.DecodeString(request.strMasterID);

                request.chat_group_id = Convert.ToInt64(decoded_chat_group_id);

                ret = await _GenChatGroupService.DeleteAsync(request.chat_group_id);

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

