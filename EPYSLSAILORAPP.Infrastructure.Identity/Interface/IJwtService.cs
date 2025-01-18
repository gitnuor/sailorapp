using EPYSLSAILORAPP.Domain.Security;
using System.Security.Claims;

namespace EPYSLSAILORAPP.Infrastructure.Identity.Interface
{
    public interface IJwtService
    {
       Task<string>  GenerateAsync(owin_user_entity user);
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        //Task<AccessToken> RefreshToken(Guid refreshTokenId);
    }
}