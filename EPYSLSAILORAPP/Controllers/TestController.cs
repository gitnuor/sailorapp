using Microsoft.AspNetCore.Mvc;

namespace EPYSLSAILORAPP.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
