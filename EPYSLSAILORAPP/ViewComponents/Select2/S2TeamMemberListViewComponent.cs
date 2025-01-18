
using AutoMapper.Configuration;
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

    public class S2TeamMemberListViewComponent : ViewComponent
    {
       
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;

      
        public S2TeamMemberListViewComponent(IHttpContextAccessor contextAccessor, 
          
           Microsoft.Extensions.Configuration.IConfiguration config)
        {
            _config = config;
            _contextAccessor = contextAccessor;
          
        }


        public IViewComponentResult Invoke(string team_group_id, string ddlId, string selectedvalue="", string cssclass = "",
            bool isRequired= false, bool isReadOnly = false, bool isMultiple = false, string placeholder="",string preloaded="",string model_parentid="")
        {
            
            ViewBag.ddlTitle = "";
            ViewBag.data = !string.IsNullOrEmpty(preloaded) ? preloaded : null;
            ViewBag.selectid = ddlId;
            ViewBag.ddlcss = cssclass;
            ViewBag.selectedvalue = selectedvalue;
            ViewBag.isRequired = isRequired;
            ViewBag.isMultiple = isMultiple;
            ViewBag.placeholder = placeholder;
            ViewBag.isReadOnly = isReadOnly;
            ViewBag.model_parentid = model_parentid;
            ViewBag.team_group_id = team_group_id;

            return View("~/Views/Shared/Components/Select2/S2TeamMemberList.cshtml");
        }

    }
}

