
using AutoMapper.Configuration;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.ViewComponents
{
    /// <summary>
    /// SideBarMenu
    /// </summary>
    public class SeasonEventConfigEditViewComponent : ViewComponent
    {
        //private IPostRepository repository;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private readonly IGenSeasonService _gen_season_Service;
        private readonly IGenSeasonEventConfigurationService _seasoneventconfig_service;
        /// <summary>
        /// AboutUsViewComponent
        /// </summary>
        public SeasonEventConfigEditViewComponent(IHttpContextAccessor contextAccessor,
          IGenFiscalYearService gen_fiscal_year_Service,
          IGenSeasonEventConfigurationService seasoneventconfig_service, IGenSeasonService gen_season_Service,
           Microsoft.Extensions.Configuration.IConfiguration config)
        {
            _config = config;
            _contextAccessor = contextAccessor;
            _gen_fiscal_year_Service = gen_fiscal_year_Service;
            _gen_season_Service = gen_season_Service;
            _seasoneventconfig_service = seasoneventconfig_service;
        }

        public IViewComponentResult Invoke(string strID)
        {
            string decoded_event_id = clsUtil.DecodeString(strID);

            DtParameters dtparam = new DtParameters();
            dtparam.event_id = Convert.ToInt64(decoded_event_id);
            dtparam.Start = 0;
            dtparam.Length = 100;
            dtparam.Search = new DtSearch();
            //dtparam.fiscal_year_id = null;

            var model = _seasoneventconfig_service.GetAllPagedData(dtparam).Result.FirstOrDefault();

            var season_list = _gen_season_Service.get_fiscal_season_GetAll().Result;

            ViewBag.season_list = season_list.OrderBy(p=>p.sequence)
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

        

            return View("~/Views/Configuration/SeasonEventConfig/_SeasonEventConfigEdit.cshtml", model);


        }

    }
}
