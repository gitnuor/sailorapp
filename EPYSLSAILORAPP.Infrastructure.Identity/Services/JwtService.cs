using EPYSLSAILORAPP.Domain.Security;
using EPYSLSAILORAPP.Infrastructure.Identity.General;
using EPYSLSAILORAPP.Infrastructure.Identity.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EPYSLSAILORAPP.Infrastructure.Identity.Services
{
    public class JwtService : IJwtService
    {
        private  IdentitySettings _siteSetting;

        //private readonly AppUserClaimsPrincipleFactory claimsPrincipleFactory;

        public JwtService(IOptions<IdentitySettings> siteSetting)
        {
            _siteSetting = siteSetting.Value;
        }
        public async Task<string> GenerateAsync( owin_user_entity user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.SecretKey); // longer that 16 character
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(_siteSetting.Encryptkey); //must be 16 character
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);


            var claims = await SetClaims(user);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSetting.Issuer,
                Audience = _siteSetting.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(0),
                Expires = DateTime.Now.AddMinutes(_siteSetting.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);


            //var refreshToken = await _unitOfWork.UserRefreshTokenRepository.CreateToken(user.UserCode);
            //await _unitOfWork.CommitAsync();

            return securityToken.InnerToken.RawData.ToString();
        }

        public Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSetting.SecretKey)),
                ValidateLifetime = false,
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSetting.Encryptkey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return Task.FromResult(principal);
        }


        //public async Task<AccessToken> RefreshToken(Guid refreshTokenId)
        //{
        //    var refreshToken = await _unitOfWork.UserRefreshTokenRepository.GetTokenWithInvalidation(refreshTokenId);

        //    if (refreshToken is null)
        //        return null;

        //    refreshToken.IsValid = false;

        //    await _unitOfWork.CommitAsync();

        //    var user = await _unitOfWork.UserRefreshTokenRepository.GetUserByRefreshToken(refreshTokenId);

        //    if (user is null)
        //        return null;

        //    var result = await this.GenerateAsync(user);

        //    return result;
        //}

        private async Task<IEnumerable<Claim>> SetClaims(owin_user_entity user)
        {
            //var result = await _claimsPrincipal.CreateAsync(user);
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
            new Claim("UserID",user.userid.ToString()),
            new Claim("UserPassword",user.password)
        };
            await Task.Run(() =>
            {
                
            });
            return claims;
        }
    }
}
