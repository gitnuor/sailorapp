using EPYSLSAILORAPP.Models;
using EPYSLSAILORAPP.SystemServices;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using System;
using System.Diagnostics;

namespace EPYSLSAILORAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
      
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Home/Error")]
        public IActionResult Error()
        {
            ViewBag.errormsg=Request.Query["err"];

            var exceptionHandlerPathFeature = _contextAccessor.HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null)
            {
                // Get the exception details
                var exception = exceptionHandlerPathFeature.Error;

                ViewBag.ExceptionMessage = exception.Message;
               // ViewBag.ExceptionStackTrace = exception.StackTrace;

                var objError = new ErrorDetails()
                {
                    StatusCode = _contextAccessor.HttpContext.Response.StatusCode,
                    Message = "Internal Server Error from the custom middleware.",
                    ActualError = exception.ToString()
                };

                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    _logger.LogError(exception.ToString());
                }
            }
           

            return View();
        }
    }
}