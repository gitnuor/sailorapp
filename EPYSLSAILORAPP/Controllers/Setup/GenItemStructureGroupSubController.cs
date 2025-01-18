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
    public class GenItemStructureGroupSubController : BaseController
    {
        private readonly ILogger<GenItemStructureGroupSubController> _logger;

        private readonly IGenItemStructureGroupSubService _IGenItemStructureGroupSubService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GenItemStructureGroupSubController !");
            return View();
        }

        public GenItemStructureGroupSubController(
           IMapper mapper, ILogger<GenItemStructureGroupSubController> logger, IHttpContextAccessor contextAccessor,
            IGenItemStructureGroupSubService IGenItemStructureGroupSubService,
            IRPCDbService rpc_db_service)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _IGenItemStructureGroupSubService = IGenItemStructureGroupSubService;

            

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> GenItemStructureGroupSubLanding()
        {

            return View("~/Views/Setup/GenItemStructureGroupSub/GenItemStructureGroupSubLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GenItemStructureGroupSubNew()
        {
            gen_item_structure_group_sub_DTO model = new gen_item_structure_group_sub_DTO();

            var objRet = await _rpc_db_service.GetAllproc_gen_item_structure_group_sub_newAsync();

            model.List_Item_Structure_Group = JsonConvert.DeserializeObject<List<gen_item_structure_group_DTO>>(objRet.gen_item_structure_group_list);
            model.List_Measurement_Unit = JsonConvert.DeserializeObject<List<gen_measurement_unit_DTO>>(objRet.gen_measurement_unit_list);
            model.List_Measurement_Unit_Detail = JsonConvert.DeserializeObject<List<gen_measurement_unit_detail_DTO>>(objRet.gen_measurement_unit_detail_list);

            return PartialView("~/Views/Setup/GenItemStructureGroupSub/_GenItemStructureGroupSubNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenItemStructureGroupSubEdit(string gen_item_structure_group_sub_id)
        {

            string decoded_gen_item_structure_group_sub_id = clsUtil.DecodeString(gen_item_structure_group_sub_id);

            var model = await _rpc_db_service.GetAllproc_gen_item_structure_group_sub_editAsync(Convert.ToInt64(decoded_gen_item_structure_group_sub_id));

            return PartialView("~/Views/Setup/GenItemStructureGroupSub/_GenItemStructureGroupSubEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenItemStructureGroupSubView(string gen_item_structure_group_sub_id)
        {

            string decoded_gen_item_structure_group_sub_id = clsUtil.DecodeString(gen_item_structure_group_sub_id);

            var model = await _rpc_db_service.GetAllproc_gen_item_structure_group_sub_editAsync(Convert.ToInt64(decoded_gen_item_structure_group_sub_id));

            return PartialView("~/Views/Setup/GenItemStructureGroupSub/_GenItemStructureGroupSubView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetGenItemStructureGroupSubData(DtParameters request)
        {

            var records = await _IGenItemStructureGroupSubService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.gen_item_structure_group_sub_id,
                            t.item_structure_group_id,
                            t.sub_group_name,
                            t.added_by,
                            t.date_added,
                            t.updated_by,
                            t.date_updated,
                            t.measurement_unit_id,
                            t.default_measurement_unit_detail_id,
                            t.secondary_measurement_unit_id,
                            t.secondary_measurement_unit_detail_id,
                            t.measurement_unit,
                            t.default_measurement_unit_detail,
                            t.secondary_measurement_unit,
                            t.secondary_measurement_unit_detail,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenItemStructureGroupSub(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  gen_item_structure_group_sub_id='" + clsUtil.EncodeString(t.gen_item_structure_group_sub_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewGenItemStructureGroupSub(this)' style='width: 120px;' class='btn btn-secondary btnView'  gen_item_structure_group_sub_id='" + clsUtil.EncodeString(t.gen_item_structure_group_sub_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteGenItemStructureGroupSub(\"" + clsUtil.EncodeString(t.gen_item_structure_group_sub_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }



        [HttpPost]
        public async Task<IActionResult> SaveGenItemStructureGroupSub([FromBody] gen_item_structure_group_sub_DTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                if (request.gen_item_structure_group_sub_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;
                }

            }

            try
            {
                //var model = JsonConvert.DeserializeObject<gen_item_structure_group_sub_entity>(JsonConvert.SerializeObject(request));

                ret = await _IGenItemStructureGroupSubService.SaveUpdateAsync(request);

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
        public async Task<IActionResult> UpdateGenItemStructureGroupSub([FromBody] gen_item_structure_group_sub_DTO request)
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
               // var model = JsonConvert.DeserializeObject<gen_item_structure_group_sub_entity>(JsonConvert.SerializeObject(request));

                ret = await _IGenItemStructureGroupSubService.SaveUpdateAsync(request);

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
        public async Task<IActionResult> DeleteGenItemStructureGroupSub([FromBody] gen_item_structure_group_sub_DTO request)
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

                string decoded_gen_item_structure_group_sub_id = clsUtil.DecodeString(request.strMasterID);

                request.gen_item_structure_group_sub_id = Convert.ToInt64(decoded_gen_item_structure_group_sub_id);

                ret = await _IGenItemStructureGroupSubService.DeleteAsync(request.gen_item_structure_group_sub_id.Value);

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

