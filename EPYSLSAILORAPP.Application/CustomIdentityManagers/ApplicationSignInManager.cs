using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using EPYSLSAILORAPP.Application.DTO;
using Microsoft.Extensions.Configuration;
using EPYSLSAILORAPP.Domain.DTO;

namespace EPYSLSAILORAPP.Application.CustomIdentityManagers
{
    public class ApplicationSignInManager<TUser> : SignInManager<owin_user_DTO> where TUser : class
    {

        private readonly ApplicationUserManager<owin_user_DTO> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private HttpContext _context;
        private IAuthenticationSchemeProvider _schemes;
        public virtual Microsoft.Extensions.Logging.ILogger Logger { get; set; }
       // public ApplicationUserManager<owin_user_DTO> UserManager { get; set; }
        public IUserClaimsPrincipalFactory<owin_user_DTO> ClaimsFactory { get; set; }
        public IdentityOptions Options { get; set; }

        /// <summary>
        /// Context
        /// </summary>
        public HttpContext Context
        {
            get
            {
                var context = _context ?? _contextAccessor?.HttpContext;
                if (context == null)
                {
                    throw new InvalidOperationException("HttpContext must not be null.");
                }
                return context;
            }
            set
            {
                _context = value;
            }
        }

        private readonly IConfiguration _config;
       
        public ApplicationSignInManager(

            ApplicationUserManager<owin_user_DTO> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<owin_user_DTO> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<owin_user_DTO>> logger,
            IAuthenticationSchemeProvider schemes
            ,IUserConfirmation<owin_user_DTO> confirmation
            , IConfiguration config
        )
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes
              , confirmation
              )
        {
            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }
            if (contextAccessor == null)
            {
                throw new ArgumentNullException(nameof(contextAccessor));
            }
            if (claimsFactory == null)
            {
                throw new ArgumentNullException(nameof(claimsFactory));
            }

            _userManager = userManager;
            _contextAccessor = contextAccessor;
            ClaimsFactory = claimsFactory;
            Options = optionsAccessor?.Value ?? new IdentityOptions();
            Logger = logger;
            _schemes = schemes;
            _config = config;
            
        }

        public override async Task<ClaimsPrincipal> CreateUserPrincipalAsync(owin_user_DTO user)
        {
            try
            {
                return await ClaimsFactory.CreateAsync(user);
            }
            catch (Exception x)
            {
                throw x;
            }

        }

        public override async Task<SignInResult> PasswordSignInAsync(string username, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }


        public override async Task<SignInResult> PasswordSignInAsync(owin_user_DTO user, string password,
           bool isPersistent, bool lockoutOnFailure)
        {
            try
            {
                //var attempt = await CheckPasswordSignInAsync(user, password, lockoutOnFailure);
                //return attempt.Succeeded
                //    ? await SignInOrTwoFactorAsync(user, isPersistent)
                //    : attempt;
                // return  SignInAsync(user, new AuthenticationProperties { IsPersistent = isPersistent }, "forms");
                return SignInResult.Success;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// CheckPasswordSignInAsync
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="lockoutOnFailure"></param>
        /// <returns></returns>
        public override async Task<SignInResult> CheckPasswordSignInAsync(owin_user_DTO user, string password, bool lockoutOnFailure)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var error = await PreSignInCheck(user);
            if (error != null)
            {
                return error;
            }

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var alwaysLockout = AppContext.TryGetSwitch("Microsoft.AspNetCore.Identity.CheckPasswordSignInAlwaysResetLockoutOnSuccess", out var enabled) && enabled;
                // Only reset the lockout when TFA is not enabled when not in quirks mode
                if (alwaysLockout || !await IsTfaEnabled(user))
                {
                    await ResetLockout(user);
                }

                return SignInResult.Success;
            }
            Logger.LogWarning(2, "User {userId} failed to provide the correct password.", await _userManager.GetUserIdAsync(user));

            if (_userManager.SupportsUserLockout && lockoutOnFailure)
            {
                // If lockout is requested, increment access failed count which might lock out the user
                await _userManager.AccessFailedAsync(user);
                if (await _userManager.IsLockedOutAsync(user))
                {
                    return await LockedOut(user);
                }
            }
            return SignInResult.Failed;
        }

        private async Task<bool> IsTfaEnabled(owin_user_DTO user)
            => _userManager.SupportsUserTwoFactor &&
            await _userManager.GetTwoFactorEnabledAsync(user) &&
            (await _userManager.GetValidTwoFactorProvidersAsync(user)).Count > 0;


        public override Task SignInAsync(owin_user_DTO user, bool isPersistent, string authenticationMethod = null)
        {
            return SignInAsync(user, new AuthenticationProperties { IsPersistent = isPersistent }, authenticationMethod);
        }

        public async Task SignInAsync(owin_user_DTO user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        {
            //claims
            var userPrincipal = await CreateUserPrincipalAsync(user);
            // Review: should we guard against CreateUserPrincipal returning null?
            if (authenticationMethod != null)
            {
                userPrincipal.Identities.First().AddClaim(new Claim(ClaimTypes.AuthenticationMethod, authenticationMethod));
            }
            await Context.SignInAsync(IdentityConstants.ApplicationScheme,
                userPrincipal,
                authenticationProperties ?? new AuthenticationProperties());
        }

        public override bool IsSignedIn(ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            return principal?.Identities != null &&
                principal.Identities.Any(i => i.AuthenticationType == IdentityConstants.ApplicationScheme);
        }

        public override async Task<bool> CanSignInAsync(owin_user_DTO user)
        {
            if (Options.SignIn.RequireConfirmedEmail && !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                Logger.LogWarning(0, "User {userId} cannot sign in without a confirmed email.", await _userManager.GetUserIdAsync(user));
                return false;
            }
            if (Options.SignIn.RequireConfirmedPhoneNumber && !(await _userManager.IsPhoneNumberConfirmedAsync(user)))
            {
                Logger.LogWarning(1, "User {userId} cannot sign in without a confirmed phone number.", await _userManager.GetUserIdAsync(user));
                return false;
            }

            return true;
        }
    }
}
