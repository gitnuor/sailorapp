//using System;
//using System.Reflection;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Localization;
//using Microsoft.Extensions.Logging;
//using CLL.Localization;
//using Web.Core.Frame.Interfaces.UseCases;
//using Web.Core.Frame.RequestResponse.UseCaseRequests;
//using Web.Core.Frame.Presenters;
//using Microsoft.Extensions.Configuration;
//using BDO.Core.DataAccessObjects.Models;
//using BDO.Core.DataAccessObjects.CommonEntities;
//using WebAdmin.Providers;
//using Newtonsoft.Json;
//using BDO.Core.DataAccessObjects.SecurityModels;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Linq;

//namespace WebAdmin.Controllers
//{
//    [Authorize]
//    [AutoValidateAntiforgeryToken]
//    public class RolePermissionController : BaseController
//    {
//        private readonly IOwin_RolePermissionUseCase _owin_RolePermissionUseCase;
//        private readonly Owin_RolePermissionPresenter _owin_RolePermissionPresenter;
//        private readonly ICacheProvider _cacheProvider;


//        private readonly IOwin_FormActionUseCase _owin_FormActionUseCase;
//        private readonly Owin_FormActionPresenter _owin_FormActionPresenter;

//        private readonly IOwin_RoleUseCase _owin_RoleUseCase;
//        private readonly Owin_RolePresenter _owin_RolePresenter;

//        private readonly ILogger<RolePermissionController> _logger;
//        private readonly IStringLocalizer _sharedLocalizer;
//        private readonly IAuthenticationSchemeProvider _schemeProvider;


//        //to Enable SignalR Inj
//        //private readonly IHubContext<HubUserContext> _hubuserContext;
//        //private readonly IUserConnectionManager _userConnectionManager;

//        /// <summary>
//        /// _configuration
//        /// </summary>
//        public IConfiguration _configuration { get; }

//        /// <summary>
//        /// Owin_RolePermissionController
//        /// </summary>
//        /// <param name="owin_RolePermissionUseCase"></param>
//        /// <param name="owin_RolePermissionPresenter"></param>
//        /// <param name="loggerFactory"></param>
//        /// <param name="factory"></param>
//        /// <param name="schemeProvider"></param>        
//        /// <param name="hubuserContext"></param>
//        /// <param name="userConnectionManager"></param>
//        /// <param name="cacheProvider"></param>
//        /// <param name="owin_FormActionUseCase"></param>
//        /// <param name="owin_FormActionPresenter"></param>
//        /// <param name="owin_RoleUseCase"></param>
//        /// <param name="owin_RolePresenter"></param>

//        public RolePermissionController(
//            IOwin_RolePermissionUseCase owin_RolePermissionUseCase,
//            Owin_RolePermissionPresenter owin_RolePermissionPresenter,
//            ILoggerFactory loggerFactory,
//            IStringLocalizerFactory factory,
//            IAuthenticationSchemeProvider schemeProvider
//            //,IHubContext<HubUserContext> hubuserContext
//            //,IUserConnectionManager userConnectionManager
//            , ICacheProvider cacheProvider

//             , IOwin_FormActionUseCase owin_FormActionUseCase
//            , Owin_FormActionPresenter owin_FormActionPresenter

//             , IOwin_RoleUseCase owin_RoleUseCase
//            , Owin_RolePresenter owin_RolePresenter
//            )
//        {
//            _owin_RolePermissionUseCase = owin_RolePermissionUseCase;
//            _owin_RolePermissionPresenter = owin_RolePermissionPresenter;
//            _logger = loggerFactory.CreateLogger<RolePermissionController>();
//            _schemeProvider = schemeProvider;


//            _owin_FormActionUseCase = owin_FormActionUseCase;
//            _owin_FormActionPresenter = owin_FormActionPresenter;

//            _owin_RoleUseCase = owin_RoleUseCase;
//            _owin_RolePresenter = owin_RolePresenter;

//            //_hubuserContext = hubuserContext;
//            //_userConnectionManager = userConnectionManager;
//            _cacheProvider = cacheProvider;

//            var type = typeof(SharedResource);
//            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
//            _sharedLocalizer = factory.Create("SharedResource", assemblyName.Name);
//        }


//        /// <summary>
//        /// LandingOwin_RolePermission
//        /// </summary>
//        /// <param name="returnUrl"></param>
//        /// <returns></returns>
//        [HttpGet]
//        public async Task<IActionResult> LandingOwin_RolePermission(string returnUrl)
//        {
//            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Account", "Login"); }

//            //await _owin_RoleUseCase.GetAll(new Owin_RoleRequest(new owin_roleEntity { }), _owin_RolePresenter);
//            //List<owin_roleEntity> rolelist = _owin_RolePresenter.Result as List<owin_roleEntity>;

//            //ViewBag.RoleList = (List<SelectListItem>)rolelist.ToList().OrderBy(p => p.roleid).Select(a =>
//            //    new SelectListItem
//            //    {
//            //        Text = a.rolename,
//            //        Value = a.roleid.ToString()
//            //    }
//            //).ToList();
//            return View("../Security/RolePermission/LandingOwin_RolePermission", new owin_rolepermissionEntity());
//        }       

//        /// <summary>
//        /// ListOwin_RolePermission
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [AutoValidateAntiforgeryToken]
//        public async Task<IActionResult> ListOwin_RolePermission(DtParameters request)
//        {
//            try
//            {
//                var draw = request.Draw;
//                owin_rolepermissionEntity objrequest = new owin_rolepermissionEntity();
//                objrequest.BaseSecurityParam = new BDO.Core.Base.SecurityCapsule();
//                objrequest.BaseSecurityParam = request.BaseSecurityParam;
//                objrequest.CurrentPage = request.Start == 0 ? 1 : request.Start / request.Length + 1;
//                objrequest.PageSize = request.Length;
//                objrequest.SortExpression = request.SortOrder + " " + request.Order[0].Dir;
//                objrequest.strCommonSerachParam = request.Search.Value;
//                objrequest.ControllerName = "Owin_RolePermission";
//                await _owin_RolePermissionUseCase.GetListView(new Owin_RolePermissionRequest(objrequest), _owin_RolePermissionPresenter);
//                return Json(_owin_RolePermissionPresenter.Result);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        /// <summary>
//        /// Get View AddOwin_RolePermission
//        /// </summary>
//        /// <param name="returnUrl"></param>
//        /// <returns></returns>
//        [HttpGet]
//        [Authorize]
//        public async Task<IActionResult> AddOwin_RolePermission(string returnUrl)
//        {
//            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Account", "Login"); }
//            return View("../General/Owin_RolePermission/AddOwin_RolePermission", new owin_rolepermissionEntity());
//        }

//        /// <summary>
//        /// POST AddOwin_RolePermission
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> AddOwin_RolePermission([FromBody] owin_rolepermissionEntity request)
//        {
//            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Account", "Login"); }
//            if (!ModelState.IsValid) { return BadRequest(ModelState); }
//            await _owin_RolePermissionUseCase.Save(new Owin_RolePermissionRequest(request), _owin_RolePermissionPresenter);
//            return _owin_RolePermissionPresenter.ContentResult;
//        }

//        /// <summary>
//        /// Get View EditOwin_RolePermission
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [HttpGet]
//        [AutoValidateAntiforgeryToken]
//        public async Task<IActionResult> EditOwin_RolePermission([FromQuery(Name = "params")] string input)
//        {
//            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Account", "Login"); }
//            owin_rolepermissionEntity objEntity = new owin_rolepermissionEntity();
//            objEntity.rolepremissionid = long.Parse(objClsPrivate.DecodeUrlParamsWithoutURI("rolepremissionid", input).ToString());
//            await _owin_RolePermissionUseCase.GetSingle(new Owin_RolePermissionRequest(objEntity), _owin_RolePermissionPresenter);
//            objEntity = _owin_RolePermissionPresenter.Result as owin_rolepermissionEntity;


//            owin_formactionEntity objowin_formactionEntity = new owin_formactionEntity();
//            objowin_formactionEntity.formactionid = objEntity.formactionid;
//            await _owin_FormActionUseCase.GetSingle(new Owin_FormActionRequest(objowin_formactionEntity), _owin_FormActionPresenter);
//            objowin_formactionEntity = ((owin_formactionEntity)_owin_FormActionPresenter.Result);
//            var dataowin_formaction = (new { Id = objowin_formactionEntity.formactionid, Text = objowin_formactionEntity.formactionid });
//            ViewBag.preloadedDataowin_formaction = JsonConvert.SerializeObject(dataowin_formaction);




//            return View("../General/Owin_RolePermission/EditOwin_RolePermission", _owin_RolePermissionPresenter.Result);
//        }

//        /// <summary>
//        /// Post EditOwin_RolePermission
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> EditOwin_RolePermission([FromBody] owin_rolepermissionEntity request)
//        {
//            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Account", "Login"); }
//            if (!ModelState.IsValid) { return BadRequest(ModelState); }
//            await _owin_RolePermissionUseCase.Update(new Owin_RolePermissionRequest(request), _owin_RolePermissionPresenter);
//            return _owin_RolePermissionPresenter.ContentResult;
//        }

//        /// <summary>
//        /// Get View GetSingleOwin_RolePermission
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [HttpGet]
//        [AutoValidateAntiforgeryToken]
//        public async Task<IActionResult> GetSingleOwin_RolePermission([FromQuery(Name = "params")] string input)
//        {
//            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Account", "Login"); }
//            owin_rolepermissionEntity objEntity = new owin_rolepermissionEntity();
//            objEntity.rolepremissionid = long.Parse(objClsPrivate.DecodeUrlParamsWithoutURI("rolepremissionid", input).ToString());
//            await _owin_RolePermissionUseCase.GetSingle(new Owin_RolePermissionRequest(objEntity), _owin_RolePermissionPresenter);
//            objEntity = _owin_RolePermissionPresenter.Result as owin_rolepermissionEntity;

//            owin_formactionEntity objowin_formactionEntity = new owin_formactionEntity();
//            objowin_formactionEntity.formactionid = objEntity.formactionid;
//            await _owin_FormActionUseCase.GetSingle(new Owin_FormActionRequest(objowin_formactionEntity), _owin_FormActionPresenter);
//            objowin_formactionEntity = ((owin_formactionEntity)_owin_FormActionPresenter.Result);
//            var dataowin_formaction = (new { Id = objowin_formactionEntity.formactionid, Text = objowin_formactionEntity.formactionid });
//            ViewBag.preloadedDataowin_formaction = JsonConvert.SerializeObject(dataowin_formaction);




//            return View("../General/Owin_RolePermission/GetSingleOwin_RolePermission", _owin_RolePermissionPresenter.Result);
//        }

//        /// <summary>
//        /// Get View DeleteOwin_RolePermission
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        [HttpGet]
//        [AutoValidateAntiforgeryToken]
//        public async Task<IActionResult> DeleteOwin_RolePermission([FromQuery(Name = "params")] string input)
//        {
//            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Account", "Login"); }
//            owin_rolepermissionEntity objEntity = new owin_rolepermissionEntity();
//            objEntity.rolepremissionid = long.Parse(objClsPrivate.DecodeUrlParamsWithoutURI("rolepremissionid", input).ToString());
//            await _owin_RolePermissionUseCase.GetSingle(new Owin_RolePermissionRequest(objEntity), _owin_RolePermissionPresenter);
//            objEntity = _owin_RolePermissionPresenter.Result as owin_rolepermissionEntity;

//            owin_formactionEntity objowin_formactionEntity = new owin_formactionEntity();
//            objowin_formactionEntity.formactionid = objEntity.formactionid;
//            await _owin_FormActionUseCase.GetSingle(new Owin_FormActionRequest(objowin_formactionEntity), _owin_FormActionPresenter);
//            objowin_formactionEntity = ((owin_formactionEntity)_owin_FormActionPresenter.Result);
//            var dataowin_formaction = (new { Id = objowin_formactionEntity.formactionid, Text = objowin_formactionEntity.formactionid });
//            ViewBag.preloadedDataowin_formaction = JsonConvert.SerializeObject(dataowin_formaction);




//            return View("../General/Owin_RolePermission/DeleteOwin_RolePermission", _owin_RolePermissionPresenter.Result);
//        }

//        /// <summary>
//        /// Post DeleteOwin_RolePermission
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [AutoValidateAntiforgeryToken]
//        public async Task<IActionResult> DeleteOwin_RolePermission([FromBody] owin_rolepermissionEntity request)
//        {
//            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Account", "Login"); }
//            /*				 ModelState.Remove("rolepremissionid");
//                  ModelState.Remove("roleid");
//                  ModelState.Remove("formactionid");
//                  ModelState.Remove("status");
// */
//            if (!ModelState.IsValid) { return BadRequest(ModelState); }
//            await _owin_RolePermissionUseCase.Delete(new Owin_RolePermissionRequest(request), _owin_RolePermissionPresenter);
//            return _owin_RolePermissionPresenter.ContentResult;
//        }


//        #region Custom Actions

//        /// <summary>
//        /// LandingOwin_RolePermission
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [AllowAnonymous]
//        public async Task<IActionResult> LoadRolePermission([FromBody] owin_formactionEntity request)
//        {
//            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Account", "Login"); }
//            //if (!ModelState.IsValid) { return BadRequest(ModelState); }
//            //await _owin_RolePermissionUseCase.Save(new Owin_RolePermissionRequest(request), _owin_RolePermissionPresenter);
//            await _owin_FormActionUseCase.GetFormActionByRole(new Owin_FormActionRequest(request), _owin_FormActionPresenter);
//            return Json(_owin_FormActionPresenter);
//        }


//        /// <summary>
//        /// Post EditOwin_RolePermission
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> AssignOwin_RolePermission([FromBody] owin_rolepermissionEntity request)
//        {
//            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Account", "Login"); }
//            //if (!ModelState.IsValid) { return BadRequest(ModelState); }
//            await _owin_FormActionUseCase.AssignRolePermission(new Owin_RolePermissionRequest(request), _owin_RolePermissionPresenter);
//            return _owin_RolePermissionPresenter.ContentResult;
//        }

//        #endregion Custom Actions
//    }
//}
