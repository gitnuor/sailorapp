using EPYSLSAILORAPP.Domain.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using EPYSLSAILORAPP.Models;
//using EPYSLSAILORAPP.Domain.Statics;
//using EPYSL.Encription;
using k8s.KubeConfigModels;
using EPYSLSAILORAPP.Application.Interface.Security;
using ServiceStack;

using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Domain.DTO;
using Microsoft.AspNetCore.Http;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Infrastructure.Services;
using static MongoDB.Libmongocrypt.CryptContext;
using Microsoft.Extensions.Caching.Memory;
using BDO.Core.Base;
using Newtonsoft.Json;

namespace EPYSLSAILORAPP.Controllers
{
    public class AccountController : BaseController
    {

        private readonly IOwinUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        private IRedisService<rpc_range_plan_getfor_createupdate_DTO> _redisRangePlanService;
        public AccountController(IOwinUserService userService, IConfiguration configuration, IMemoryCache cache)
        {
            _userService = userService;
            _configuration = configuration;
            _cache = cache;
            _redisRangePlanService = new RedisService<rpc_range_plan_getfor_createupdate_DTO>(_configuration);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction(actionName: "Index", controllerName: "Dashboard");

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(owin_user_DTO model)
        {
            var sec_object = new SecurityCapsule();

            List<SecurityCapsule> list = new List<SecurityCapsule>();

            if (_cache.TryGetValue("online_user_list", out list))
            {
                if (list.Where(p => p.username == model.user_name).ToList().Count > 0)
                {
                    return Json(new { statusCode = 400, statusResult = "User already logged In from another device." });
                }
            }

            _redisRangePlanService.ClearAllKeys();

            AppUser = null;

            var request = await _userService.GetSingleAsyncByROle(model.user_name);

            if (request.role_id != null)
            {
                var result = await _userService.CheckUserForLogin(model);

                if (result.Status_Code == "200")
                {
                    return Json(new { statusCode = result.Status_Code, statusResult = result.Status_Result, accessToken = "" });
                }
                else
                {
                    return Json(new { statusCode = result.Status_Code, statusResult = result.Status_Result });
                }
            }
            else
            {
                return Json(new { statusCode = 400, statusResult = "User is not Authorized" });
            }
            
           
        }

        [HttpPost]
        public ActionResult Register(RegisterBindingModel model)
        {
            AppUser = null;

            return Json(new { statusCode = HttpStatusCode.OK, accessToken = "" });
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Register(int menuId, string pageName)
        {
            pageName = string.Join("", pageName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            ViewBag.MenuId = menuId;
            ViewBag.PageName = pageName;
            return PartialView("~/Views/Account/_Register.cshtml");
        }

        [HttpGet, AllowAnonymous]
        public ActionResult SecurityRule(int menuId, string pageName)
        {
            pageName = string.Join("", pageName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            ViewBag.MenuId = menuId;
            ViewBag.PageName = pageName;
            return PartialView("~/Views/Account/_SecurityRule.cshtml");
        }
        [HttpGet, AllowAnonymous]
        public ActionResult GroupUser(int menuId, string pageName)
        {
            pageName = string.Join("", pageName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            ViewBag.MenuId = menuId;
            ViewBag.PageName = pageName;
            return PartialView("~/Views/Account/_GroupUser.cshtml");
        }

        [HttpGet, AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return PartialView("~/Views/Account/_ChangePasswordPartial.cshtml");
        }

        [HttpGet, AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> LogOff()
        {
            await _userService.UserLogOff();
            return RedirectToAction("Login", "Account");
        }
    }
}
