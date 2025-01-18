using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EPYSLSAILORAPP.FilterAndAttributes
{
   
    public sealed class AuthorizeActionFilter : AuthorizeAttribute, IAuthorizationFilter//Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new RedirectResult("/account/LogOff");
                }

            }
        }

    }
}
