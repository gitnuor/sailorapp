using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.Security;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace EPYSLSAILORAPP.Controllers
{
    [AutoValidateAntiforgeryToken]
    public abstract class ExtendedBaseController : Controller
    {
        private static owin_user_entity _user;
        private readonly IHttpContextAccessor _contextAccessor;
        public SecurityCapsule sec_object = new SecurityCapsule();
        private readonly IConfiguration _configuration;
        public IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        public GenSeasonEventConfigurationDTO objFilter = new GenSeasonEventConfigurationDTO();
        public Int64 Fiscal_Year_ID { get; set; }
        public Int64 Event_ID { get; set; }

        protected ClaimsPrincipal CurrentUser => HttpContext?.User;

        protected ExtendedBaseController(IHttpContextAccessor contextAccessor, IConfiguration configuration, bool? isExclude = false)
        {

            _contextAccessor = contextAccessor;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);


            if (_contextAccessor != null)
            {
                var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

                List<Claim> listClaims = (List<Claim>)claim.Claims;

                if (listClaims.Count > 0)
                {
                    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                    sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);
                }

                if (listClaims.Count == 0 || !listClaims.Exists(c => c.Type == "secobject"))
                {
                    _contextAccessor.HttpContext.Response.Redirect("/account/LogOff", true);
                }
            }

            if (isExclude == false)
            {
                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {
                    objFilter = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                    if (objFilter == null)
                    {
                        _contextAccessor.HttpContext.Response.Redirect("/Dashboard/Index?load_filter=1");
                    }
                    else
                    {
                        Fiscal_Year_ID = objFilter.fiscal_year_id;
                        Event_ID = objFilter.event_id;
                    }

                    if(Fiscal_Year_ID==0)
                    {
                        _contextAccessor.HttpContext.Response.Redirect("/Dashboard/Index?load_filter=1");
                    }
                }
                else
                {
                    _contextAccessor.HttpContext.Response.Redirect("/Dashboard/Index?load_filter=1");
                }
            }

        }

        protected int UserId = 0;
        protected owin_user_entity AppUser
        {
            get
            {
                //_user = _userService.Find(UserId);
                //if (_user == null)
                //    throw new Exception("Can't not find logged in user.");

                //return _user;
                return new owin_user_entity();
            }
            set
            {
                _user = value;
            }
        }

        protected string UserIp = "";
        protected string BaseUrl = "";
    }
}