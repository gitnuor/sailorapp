using BDO.Core.Base;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Security;
using IdentityModel;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Claims;

namespace EPYSLSAILORAPP.SystemServices
{
    public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<owin_user_DTO, IdentityRole>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserClaimsPrincipalFactory(
            UserManager<owin_user_DTO> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor
            , IHttpContextAccessor contextAccessor
            )
                : base(userManager, roleManager, optionsAccessor)
        {
            _contextAccessor= contextAccessor;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(owin_user_DTO user)
        {

            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;
            DateTime dt = DateTime.Now;

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Role, "dataEventRecords"),
                new Claim(JwtClaimTypes.Role, "dataEventRecords.user")
            };

            SecurityCapsule _securityCapsule = new SecurityCapsule();
           
            _securityCapsule.updatedbyusername = user.user_name;

            _securityCapsule.updateddate = dt;
            _securityCapsule.createdbyusername = user.user_name;
            _securityCapsule.createddate = dt;
            _securityCapsule.transid = "NEWTRANSID";
            _securityCapsule.userid = user.userid;
            _securityCapsule.email = user.email;
            _securityCapsule.username = user.user_name;
            _securityCapsule.isauthenticated = true;

            //added by noru 26/12/2023
            _securityCapsule.roleid = user.role_id;
            //end


            // one time 
            //transactioncodeGen objTranIDGen = new transactioncodeGen();
            _securityCapsule.sessionid = _contextAccessor.HttpContext.Session.Id;
           // _securityCapsule.transid = objTranIDGen.GetRandomAlphaNumericStringForTransactionActivity("TRAN", dt);
            _securityCapsule.usertoken = _securityCapsule.transid;

            //_securityCapsule.actioname = actionName;
            //_securityCapsule.controllername = controllerName;
            _securityCapsule.pageurl = _contextAccessor.HttpContext.Request.GetDisplayUrl();
            _securityCapsule.ipaddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            string strserialize = JsonConvert.SerializeObject(_securityCapsule);

           
            claims.Add(new Claim("secobject", strserialize));

            claims.Add(new Claim("userid", user.userid.ToString()));
            claims.Add(new Claim("username", user.user_name));

            claims.Add(new Claim("roleid", user.role_id.ToString()));

            claims.Add(new Claim("secobject", strserialize));
            claims.Add(new Claim("emp_pic", _securityCapsule.emp_pic));

         
            claims.Add(new Claim("gen_team_group_id", user.gen_team_group_id != null ? user.gen_team_group_id.ToString() : "0"));
            claims.Add(new Claim("team_group_name", user.team_group_name != null ? user.team_group_name : ""));


            claims.Add(new Claim(JwtClaimTypes.Role, "user"));
            
            identity.AddClaims(claims);
            return principal;
        }
    }
}
