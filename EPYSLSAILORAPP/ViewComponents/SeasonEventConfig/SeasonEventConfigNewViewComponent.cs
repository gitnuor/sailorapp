
using AutoMapper.Configuration;
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

namespace EPYSLSAILORAPP.ViewComponents.SeasonEventConfig
{
    /// <summary>
    /// SideBarMenu
    /// </summary>
    public class SeasonEventConfigNewViewComponent : ViewComponent
    {
        //private IPostRepository repository;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private readonly IGenSeasonService _gen_season_Service;
        private readonly IGenSeasonEventConfigurationService _seasoneventconfig_service;
        /// <summary>
        /// AboutUsViewComponent
        /// </summary>
        public SeasonEventConfigNewViewComponent(IHttpContextAccessor contextAccessor,
          IGenFiscalYearService gen_fiscal_year_Service,
          IGenSeasonEventConfigurationService seasoneventconfig_service, IGenSeasonService gen_season_Service,
           IConfiguration config)
        {
            _config = config;
            _contextAccessor = contextAccessor;
            _gen_fiscal_year_Service = gen_fiscal_year_Service;
            _gen_season_Service = gen_season_Service;
            _seasoneventconfig_service = seasoneventconfig_service;
        }

        public IViewComponentResult Invoke(string strID)
        {
            string decoded_fiscal_yearid = clsUtil.DecodeString(strID);

            GenSeasonEventConfigurationDTO model = new GenSeasonEventConfigurationDTO();

            model.start_date = DateTime.Now;

            //var list = _seasoneventconfig_service.GetAllData(null,  Convert.ToInt64(decoded_fiscal_yearid) ).Result;

            //if (list.Count > 0)
            //{
            //    var objLastEntry = list.OrderByDescending(p => p.end_date).FirstOrDefault();
            //    model.start_date = objLastEntry.end_date.Value.AddDays(1);
            //}

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
                .Where(p => p.fiscal_year_id == Convert.ToInt64(decoded_fiscal_yearid)).ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString(),
                                                       Selected = true
                                                   }
                                                   ).ToList();

           

            return View("~/Views/Configuration/SeasonEventConfig/_SeasonEventConfigNew.cshtml", model);

        }

    }
}
