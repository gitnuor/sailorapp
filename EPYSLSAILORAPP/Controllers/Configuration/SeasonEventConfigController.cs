using AutoMapper;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using EPYSLSAILORAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using DnsClient.Protocol;
using EPYSLSAILORAPP.Application.DTO;
using ServiceStack.Messaging;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using EPYSLSAILORAPP.Application.Interface;
using System.Globalization;
using BDO.Core.Base;
using EPYSLSAILORAPP.Infrastructure.Services.GenServices;
using System.Text;
using Web.Core.Frame.Helpers;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Controllers.Configuration
{
    public class SeasonEventConfigController : BaseController
    {
        private readonly ILogger<SeasonEventConfigController> _logger;
        private readonly IGenSeasonEventConfigurationService _seasoneventconfig_service;
        private readonly IGenSeasonService _gen_season_Service;
        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside SeasonEventConfigController !");
            return View();
        }

        public SeasonEventConfigController(IGenSeasonEventConfigurationService seasoneventconfig_service,
            IGenFiscalYearService gen_fiscal_year_Service, IGenSeasonService gen_season_Service
            , IMapper mapper, ILogger<SeasonEventConfigController> logger, IHttpContextAccessor contextAccessor)
        {

            _mapper = mapper;
            _logger = logger;
            _gen_season_Service = gen_season_Service;
            _seasoneventconfig_service = seasoneventconfig_service;
            _gen_fiscal_year_Service = gen_fiscal_year_Service;

            _contextAccessor = contextAccessor;
            _logger.LogInformation("Hello from inside SeasonEventConfigController !");
        }

        [HttpGet]
        public async Task<IActionResult> SeasonEventConfigLanding()
        {

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();


            ViewBag.fiscal_year_list = fiscal_year_list
                //Where(p => tran_bp_year.All(q => q.fiscal_year_id != p.fiscal_year_id)).ToList()
                //.Where(p=>p.is_used==true)
                .Select(a =>
                            new SelectListItem
                            {
                                Text = a.year_name.ToString(),
                                Value = a.fiscal_year_id.ToString(),
                                Selected = a.is_used == true ? true : false
                            }
                        ).ToList();

            return View("~/Views/Configuration/SeasonEventConfig/SeasonEventConfigLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> SeasonEventConfigNew([FromQuery] string input)
        {

            return ViewComponent("SeasonEventConfigNew", new { strID = input });

        }


        [HttpGet]
        public async Task<IActionResult> LoadfiscalyearDataForCopy([FromQuery] string input, string from_year, string to_year)
        {
            string decoded_fiscal_year_id = clsUtil.DecodeString(input);
            DtParameters dtparam = new DtParameters();
            // dtparam.event_id = Convert.ToInt64(decoded_event_id);
            dtparam.Start = 0;
            dtparam.Length = 100;
            dtparam.Search = new DtSearch();
            dtparam.fiscal_year_id = Convert.ToInt64(decoded_fiscal_year_id);


            var model = _seasoneventconfig_service.GetAllPagedData(dtparam).Result.FirstOrDefault();

            model.seasonEventConficList = _seasoneventconfig_service.GetAllPagedDataForCopy(dtparam).Result.ToList();

            foreach (var obj in model.seasonEventConficList)
            {
                var new_year_start = 0; var new_year_end = 0;

                if (obj.start_date.Value.Year.ToString() == from_year)
                {
                    new_year_start = Convert.ToInt32(to_year);
                }
                else
                {
                    new_year_start = Convert.ToInt32(to_year) + 1;
                }

                if (obj.end_date.Value.Year.ToString() == from_year)
                {
                    new_year_end = Convert.ToInt32(to_year);
                }
                else
                {
                    new_year_end = Convert.ToInt32(to_year) + 1;
                }

                obj.start_date = new DateTime(new_year_start, obj.start_date.Value.Month, obj.start_date.Value.Day);

                obj.end_date = new DateTime(new_year_end, obj.end_date.Value.Month, obj.end_date.Value.Day);

            }

            var season_list = _gen_season_Service.get_fiscal_season_GetAll().Result;

            ViewBag.season_list = season_list.OrderBy(p => p.sequence)
                .Select(a =>
                                new SelectListItem
                                {
                                    Text = a.season_name.ToString(),
                                    Value = a.season_id.ToString(),
                                    Selected = true

                                }
                        ).ToList();

            var fiscal_year_list = _gen_fiscal_year_Service.get_fiscal_year_GetAll().Result;

            ViewBag.fiscal_year_list = fiscal_year_list
                .Where(p => p.fiscal_year_id == model.fiscal_year_id).ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString(),
                                                       Selected = true
                                                   }
                                                   ).ToList();

            return PartialView("~/Views/Configuration/SeasonEventConfig/_CopySeasonEventConfig.cshtml", model);
        }


        [HttpGet]
        public async Task<IActionResult> SeasonEventConfigEdit([FromQuery] string input)
        {
            string decoded_event_id = clsUtil.DecodeString(input);

            DtParameters dtparam = new DtParameters();
            dtparam.event_id = Convert.ToInt64(decoded_event_id);
            dtparam.Start = 0;
            dtparam.Length = 100;
            dtparam.Search = new DtSearch();
            //dtparam.fiscal_year_id = null;

            var model = _seasoneventconfig_service.GetAllPagedData(dtparam).Result.FirstOrDefault();
            //model.seasonEventConficList = model

            var season_list = _gen_season_Service.get_fiscal_season_GetAll().Result;

            ViewBag.season_list = season_list.OrderBy(p => p.sequence)
                .Select(a =>
                                new SelectListItem
                                {
                                    Text = a.season_name.ToString(),
                                    Value = a.season_id.ToString(),

                                }
                        ).ToList();

            var fiscal_year_list = _gen_fiscal_year_Service.get_fiscal_year_GetAll().Result;

            ViewBag.fiscal_year_list = fiscal_year_list
                .Where(p => p.fiscal_year_id == model.fiscal_year_id).ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString(),
                                                       Selected = true
                                                   }
                                                   ).ToList();



            return PartialView("~/Views/Configuration/SeasonEventConfig/_SeasonEventConfigEdit.cshtml", model);

        }

        [HttpPost]
        public async Task<IActionResult> GetSeasonEventConfigData(DtParameters request)
        {
            var records = await _seasoneventconfig_service.GetAllPagedData(request);

            var data = (from t in records
                        select new
                        {
                            t.fiscal_year_id,
                            t.season_id,
                            t.season_name,
                            t.event_title,
                            t.event_id,
                            t.start_date,
                            t.event_sequence,
                            t.end_date,
                            str_startdate = t.start_date.Value.ToString("dd-MMM-yyyy"),
                            str_enddate = t.end_date.Value.ToString("dd-MMM-yyyy"),
                            t.start_month_id,
                            t.end_month_id,
                            str_start_month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(t.start_month_id)),
                            str_end_month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(t.end_month_id)),
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' style='width: 120px;' class='btn btn-secondary btnEdit'  event_id='" + clsUtil.EncodeString(t.event_id.ToString()) + "'>Edit</button></div>"
                        }).ToList();
            var ret_obj = new AjaxResponse((records.Count() > 0 ? records.FirstOrDefault().total_rows.Value : 0), data);
            return Json(ret_obj);

        }


        [HttpPost]
        public async Task<IActionResult> SaveSeasonEventConfig([FromBody] GenSeasonEventConfigurationDTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;
            }

            request.start_date = Convert.ToDateTime(request.start_date.Value.ToString("yyyy-MM-dd"));
            request.end_date = Convert.ToDateTime(request.end_date.Value.ToString("yyyy-MM-dd"));

            var ret = await _seasoneventconfig_service.SaveAsync(request);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Saving Failed"
            });

        }

        [HttpPost]
        public async Task<IActionResult> UpdateSeasonEventConfig([FromBody] GenSeasonEventConfigurationDTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                request.updated_by = sec_object.userid.Value;
                request.added_by = sec_object.userid.Value;

                request.date_updated = DateTime.Now;

                request.date_added = DateTime.Now;
            }

            request.start_date = Convert.ToDateTime(request.start_date.Value.ToString("yyyy-MM-dd"));
            request.end_date = Convert.ToDateTime(request.end_date.Value.ToString("yyyy-MM-dd"));

            var ret = await _seasoneventconfig_service.UpdateAsync(request);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Updating Failed"
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSeasonEventConfigCopy([FromBody] gen_season_event_config_ext request)
        {
            bool ret=false;
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;
            }

            // request.start_date = Convert.ToDateTime(request.start_date.Value.ToString("yyyy-MM-dd"));
            //request.end_date = Convert.ToDateTime(request.end_date.Value.ToString("yyyy-MM-dd"));

            gen_season_event_config model =new gen_season_event_config();
            foreach (var item in request.seasonEventConficList)
            {
                item.added_by=request.added_by;
                item.date_added = request.date_added;
                model= JsonConvert.DeserializeObject<gen_season_event_config> (JsonConvert.SerializeObject(item));

                ret = await _seasoneventconfig_service.SaveAsyncCopy(model);
            }

            

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Saving Failed"
            });

        }


    }
}
