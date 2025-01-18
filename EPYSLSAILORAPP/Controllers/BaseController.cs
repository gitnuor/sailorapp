using EPYSLSAILORAPP.Domain.Security;
using Microsoft.AspNetCore.Mvc;

namespace EPYSLSAILORAPP.Controllers
{
    public abstract class BaseController : Controller
    {
        private static owin_user_entity _user;

        protected BaseController()
        {

            if (_user != null)
            {
                //if(!objuser.Identity.IsAuthenticated)
                //RedirectToAction(actionName: "login", controllerName: "account");
            }
        }

        protected int UserId=0;
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

        protected string UserIp= "";
        protected string BaseUrl="";
    }
}