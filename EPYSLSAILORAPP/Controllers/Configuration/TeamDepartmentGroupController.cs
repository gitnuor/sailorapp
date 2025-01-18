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
    public class GenTeamDepartmentGroupController : ExtendedBaseController
    {
        private readonly ILogger<GenTeamDepartmentGroupController> _logger;

        private readonly IGenTeamDepartmentGroupService _GenTeamDepartmentGroupService;
        private readonly ICommonService _CommonService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GenTeamDepartmentGroupController !");
            return View();
        }

        public GenTeamDepartmentGroupController(ICommonService CommonService,
           IMapper mapper, ILogger<GenTeamDepartmentGroupController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            IGenTeamDepartmentGroupService GenTeamDepartmentGroupService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration, true)
        {
            _mapper = mapper;
            _CommonService = CommonService;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenTeamDepartmentGroupService = GenTeamDepartmentGroupService;
            _configuration = configuration;
            

        }

        [HttpGet]
        public async Task<IActionResult> GenTeamDepartmentGroupLanding()
        {

            return View("~/Views/Configuration/GenTeamDepartmentGroup/GenTeamDepartmentGroupLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GenTeamDepartmentGroupNew()
        {
            gen_team_department_group_DTO model = new gen_team_department_group_DTO();

            return PartialView("~/Views/Configuration/GenTeamDepartmentGroup/_GenTeamDepartmentGroupNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenTeamDepartmentGroupEdit(string gen_team_department_group_id)
        {

            string decoded_gen_team_department_group_id = clsUtil.DecodeString(gen_team_department_group_id);

            var model = await _GenTeamDepartmentGroupService.GetSingleAsync(Convert.ToInt64(decoded_gen_team_department_group_id));

            var objHead = new List<SelectListItem>();
            objHead.Add(new SelectListItem
            {
                Text = model.team_head_name,
                Value = model.team_head_user_id.ToString()
            });

            ViewBag.TeamHead = objHead;

            var objDepartment = new List<SelectListItem>();
            objDepartment.Add(new SelectListItem
            {
                Text = model.team_group,
                Value = model.team_group_id.ToString()
            });

            ViewBag.Department = objDepartment;

            foreach (var obj in model.GenTeamDepartmentGroupMembers_List)
            {

                var ret = await _CommonService.LoadAllEmployeePic(
                     new owin_user_DTO
                     {
                         emp_pic = obj.emp_pic,
                         designation_name = obj.designation_name,
                         employee_code = Convert.ToInt64(obj.employee_code),
                         name = obj.name
                     });

                obj.new_emp_pic = ret.new_emp_pic;

            }

            ViewBag.MemberList = (List<SelectListItemExtended>)model.GenTeamDepartmentGroupMembers_List
                .Where(p=>p.member_user_id!= model.team_head_user_id)
            
            .Select(a =>
                                               new SelectListItemExtended
                                               {
                                                   Text = a.member_name,
                                                   Value = a.member_user_id.ToString(),
                                                   Selected = (a.is_active == 1 ? true : false),
                                                   str_other_value=a.new_emp_pic,
                                                   str_other_value2=a.is_active.ToString()
                                               }
                    ).ToList();


            return PartialView("~/Views/Configuration/GenTeamDepartmentGroup/_GenTeamDepartmentGroupEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenTeamDepartmentGroupView(string gen_team_department_group_id)
        {

            string decoded_gen_team_department_group_id = clsUtil.DecodeString(gen_team_department_group_id);

            gen_team_department_group_DTO model = new gen_team_department_group_DTO();

            var objmodel = await _GenTeamDepartmentGroupService.GetSingleAsync(Convert.ToInt64(decoded_gen_team_department_group_id));

            model = JsonConvert.DeserializeObject<gen_team_department_group_DTO>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Configuration/GenTeamDepartmentGroup/_GenTeamDepartmentGroupView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetGenTeamDepartmentGroupData(DtParameters request)
        {

            var records = await _GenTeamDepartmentGroupService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,

                            t.group_id,
                            t.team_group_name,
                            t.team_head_name,
                            t.team_head_id_number,
                            t.team_head_user_id,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenTeamDepartmentGroup(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  gen_team_department_group_id='" + clsUtil.EncodeString(t.group_id.ToString()) + "'>Edit</button>" +
                           
                            "<button type='button' onclick='DeleteGenTeamDepartmentGroup(\"" + clsUtil.EncodeString(t.group_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }


        public async Task<IActionResult> CheckTeamMember(Int64 userid,Int64 groupid=0)
        {

            var ret = await _GenTeamDepartmentGroupService.CheckMemberByUserID(userid, groupid);

            return Json(ret);

        }

        [HttpPost]
        public async Task<IActionResult> GetJoinedGenTeamDepartmentGroupData(DtParameters request)
        {

            var records = await _GenTeamDepartmentGroupService.GetAllPagedDataAsync(request);

            var data = (from t in records
                        select new
                        {
                          
                            t.team_group_name,
                            t.team_head_name,
                            t.team_head_id_number,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenTeamDepartmentGroup(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  gen_team_department_group_id='" + clsUtil.EncodeString(t.group_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='DeleteGenTeamDepartmentGroup(\"" + clsUtil.EncodeString(t.group_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }




        [HttpPost]
        public async Task<IActionResult> SaveGenTeamDepartmentGroup([FromBody] gen_team_department_group_DTO request)
        {

            var ret = true;

            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            try
            {

                ret = await _GenTeamDepartmentGroupService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateGenTeamDepartmentGroup([FromBody] gen_team_department_group_DTO request)
        {
            var ret = true;



            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

            try
            {
                
                ret = await _GenTeamDepartmentGroupService.UpdateAsync(request);

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
        public async Task<IActionResult> DeleteGenTeamDepartmentGroup([FromBody] gen_team_department_group_DTO request)
        {

            var ret = true;

            request.added_by = sec_object.userid.Value;
            request.date_added = DateTime.Now;

            try
            {

                string decoded_gen_team_department_group_id = clsUtil.DecodeString(request.strMasterID);

                ret = await _GenTeamDepartmentGroupService.DeleteAsync(Convert.ToInt64(decoded_gen_team_department_group_id));

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

