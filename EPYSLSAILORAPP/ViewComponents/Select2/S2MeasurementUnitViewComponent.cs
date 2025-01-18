
using AutoMapper.Configuration;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace EPYSLSAILORAPP.ViewComponents
{
    /// <summary>
    /// SideBarMenu
    /// </summary>
    public class S2MeasurementUnitViewComponent : ViewComponent
    {
        //private IPostRepository repository;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRPCDbService _rpc_db_service;

        /// <summary>
        /// AboutUsViewComponent
        /// </summary>
        public S2MeasurementUnitViewComponent(IHttpContextAccessor contextAccessor,
          IRPCDbService rpc_db_service,
           Microsoft.Extensions.Configuration.IConfiguration config)
        {
            _config = config;
            _contextAccessor = contextAccessor;
            _rpc_db_service = rpc_db_service;


        }


        public IViewComponentResult Invoke(string ddlId,Int64? measurement_unit_id=0, string selectedvalue="", string cssclass = "",
            bool? isRequired= false, bool? isReadOnly = false, string placeholder="",string preloaded="",string model_parentid="")
        {
            
            ViewBag.ddlTitle = "";
            ViewBag.data = !string.IsNullOrEmpty(preloaded) ? preloaded : null;
            ViewBag.selectid = ddlId;
            ViewBag.ddlcss = cssclass;
            ViewBag.selectedvalue = selectedvalue;
            ViewBag.isRequired = isRequired;
            ViewBag.placeholder = placeholder;
            ViewBag.isReadOnly = isReadOnly;
            ViewBag.model_parentid = model_parentid;


            var list_measurement =  _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(measurement_unit_id)).Result;

            var objlist = list_measurement.Select(p => new SelectListItem
            {
                Text = p.unit_detail_display,
                Value = p.gen_measurement_unit_detail_id.ToString()
            }).ToList();

            ViewBag.list_measurement = objlist;

            return View("~/Views/Shared/Components/Select2/S2MeasurementUnit.cshtml");
        }

    }
}
