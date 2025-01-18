using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace EPYSLSAILORAPP.Application.CustomAuthorization
{

    public class IsRoleActionReqHandler : AuthorizationHandler<IsRoleActionRequirement>
    {
        readonly IHttpContextAccessor _contextAccessor;

        public IsRoleActionReqHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IsRoleActionRequirement locrequirement)
        {
            if (context.User == null)
            {
                context.Fail();
            }

            if (await locrequirement.Pass(_contextAccessor, context))
            {
                context.Succeed(locrequirement);
            }
        }
    }
}
