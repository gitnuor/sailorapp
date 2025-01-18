using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

using EPYSLSAILORAPP.Domain.DTO;

namespace Web.Core.Frame.CustomStores
{
    public class CustomUserStore :
                                IUserStore<owin_user_DTO>,
                                IUserEmailStore<owin_user_DTO>,
                                IUserPhoneNumberStore<owin_user_DTO>,
                                IUserTwoFactorStore<owin_user_DTO>,
                                IUserPasswordStore<owin_user_DTO>,
                                IUserRoleStore<owin_user_DTO>,
                                IUserClaimStore<owin_user_DTO>,
                                IUserLockoutStore<owin_user_DTO>,
                                IUserAuthenticationTokenStore<owin_user_DTO>,
                                IUserAuthenticatorKeyStore<owin_user_DTO>,
                                IUserTwoFactorRecoveryCodeStore<owin_user_DTO>
    {
        private readonly IHttpContextAccessor _contextAccessor;
     

        private bool _disposed;
        public void Dispose() { }
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
        public CustomUserStore(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
          
        }


        #region FIND FUNCTION FROM DB
        public async Task<owin_user_DTO> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            //    var user = await BFC.Core.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).GetUserByParams(new owin_user_DTO()
            //    {
            //        emailaddress = normalizedEmail
            //    }, cancellationToken);
            //    return user;
            //if (user != null)
            //    return user;
            //else
            //    throw new InvalidCredentialException("Oops!!!");
            throw new NotImplementedException();
        }

        public async Task<owin_user_DTO> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            //var user = await BFC.Core.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).GetUserByParams(new owin_user_DTO()
            //{
            //    userid = new Guid(userId)
            //}, cancellationToken);
            //return user;
            //if (user != null)
            //    return user;
            //else
            //    throw new InvalidCredentialException("Oops!!!");
            throw new NotImplementedException();
        }

        public async Task<owin_user_DTO> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            //var user = await BFC.Core.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).GetUserByParams(new owin_user_DTO()
            //{
            //    username = normalizedUserName
            //}, cancellationToken);
            //return user;
            throw new NotImplementedException();
        }




        #endregion

        #region USER CREATE UPDATE DELETE FUNCTIONS 
        public async Task<IdentityResult> CreateAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            return IdentityResult.Success;
        }
        #endregion

        public Task AddToRoleAsync(owin_user_DTO user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<IdentityResult> UpdateEmailAsync(owin_user_DTO user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        #region GET CURRENT USER OBJECT

       
        public async Task<string> GetEmailAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user.email;
        }
        public async Task<bool> GetEmailConfirmedAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<string> GetNormalizedEmailAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user.email;
        }
        public async Task<string> GetNormalizedUserNameAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user.user_name;
        }
        public async Task<string> GetPasswordHashAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPhoneNumberAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> GetPhoneNumberConfirmedAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<IList<string>> GetRolesAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            //var Owin_Role = await BFC.Core.FacadeCreatorObjects.Security.owin_roleFCC.GetFacadeCreate(_contextAccessor).GetByUsrID(user, cancellationToken);
            //if (Owin_Role != null && Owin_Role.Count > 0)
            //{
            //    List<string> strlist = new List<string>();
            //    foreach (owin_roleEntity role in Owin_Role)
            //    {
            //        strlist.Add(role.rolename);
            //    }
            //    return strlist;
            //}
            return await Task.FromResult<IList<string>>(new List<string>());
        }

        public async Task<bool> GetTwoFactorEnabledAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
             throw new NotImplementedException();
        }
        public async Task<string> GetUserIdAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user.userid.ToString();
        }
        public async Task<string> GetUserNameAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user.user_name;
        }
        public Task<bool> HasPasswordAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region SET CURRENT USER OBJECT
        public async Task SetEmailAsync(owin_user_DTO user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task SetEmailConfirmedAsync(owin_user_DTO user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task SetNormalizedEmailAsync(owin_user_DTO user, string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task SetUserNameAsync(owin_user_DTO user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task SetNormalizedUserNameAsync(owin_user_DTO user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task SetTwoFactorEnabledAsync(owin_user_DTO user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SetPasswordHashAsync(owin_user_DTO user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SetPhoneNumberAsync(owin_user_DTO user, string phoneNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task SetPhoneNumberConfirmedAsync(owin_user_DTO user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion


        public async Task<IList<owin_user_DTO>> GetUsersInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (string.IsNullOrEmpty(normalizedRoleName))
            {
                throw new ArgumentNullException(nameof(normalizedRoleName));
            }

            //var Owin_Role = await BFC.Core.FacadeCreatorObjects.Security.owin_roleFCC.GetFacadeCreate(_contextAccessor).GetAll(new owin_roleEntity() { rolename = normalizedRoleName }, cancellationToken);
            //if (Owin_Role != null)
            //{
            //    var userlist = await BFC.Core.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).GetUsersInRoleAsync(new owin_user_DTO()
            //    {
            //        roleid = Owin_Role.FirstOrDefault().roleid
            //    }, cancellationToken);

            //    return userlist;
            //}
            return new List<owin_user_DTO>();
        }

        public async Task<bool> IsInRoleAsync(owin_user_DTO user, string roleName, CancellationToken cancellationToken)
        {
            //var _roles = await BFC.Core.FacadeCreatorObjects.Security.owin_roleFCC.GetFacadeCreate(_contextAccessor).GetAll(new owin_roleEntity() { rolename = roleName }, cancellationToken);
            //if (_roles != null && _roles.Count > 0)
            //{
            //    var userlist = await BFC.Core.FacadeCreatorObjects.Security.ExtendedPartial.FCCKAFUserSecurity.GetFacadeCreate(_contextAccessor).GetUsersInRoleAsync(new owin_user_DTO()
            //    {
            //        roleid = _roles.FirstOrDefault().roleid,
            //        userid = user.userid
            //    }, cancellationToken);

            //    if (userlist != null && userlist.Count > 0)
            //        return true;
            //    else
            //        return false;
            //}
            return false;
        }

        public async Task RemoveFromRoleAsync(owin_user_DTO user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

        }

        public async Task<IList<Claim>> GetClaimsAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            IList<Claim> result =new List<Claim>();

            //    BFC.Core.FacadeCreatorObjects.Security.owin_userclaimsFCC.GetFacadeCreate(_contextAccessor).GetAll(new owin_userclaimsEntity()
            //{
            //    userid = user.userid
            //}, cancellationToken).Result.ToList().Select(x => new Claim(x.claimtype, x.claimvalue)).ToList();

            return result;
        }

        public Task AddClaimsAsync(owin_user_DTO user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            //if (cancellationToken != null)
            //    cancellationToken.ThrowIfCancellationRequested();

            //if (user == null)
            //    throw new ArgumentNullException(nameof(user));

            //foreach (var claim in claims)
            //{
            //    user.Claims.Add(new owin_userclaimsEntity { claimtype = claim.Type, claimvalue = claim.Value, userid = user.userid });
            //}
            return Task.FromResult(0);
        }

        public Task ReplaceClaimAsync(owin_user_DTO user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(owin_user_DTO user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<owin_user_DTO>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SetLockoutEndDateAsync(owin_user_DTO user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GetLockoutEnabledAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(owin_user_DTO user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        //IUserAuthenticationTokenStore

        public Task SetTokenAsync(owin_user_DTO user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTokenAsync(owin_user_DTO user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTokenAsync(owin_user_DTO user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //IUserAuthenticatorKeyStore
        public Task SetAuthenticatorKeyAsync(owin_user_DTO user, string key, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAuthenticatorKeyAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        //IUserTwoFactorRecoveryCodeStore
        public Task ReplaceCodesAsync(owin_user_DTO user, IEnumerable<string> recoveryCodes, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RedeemCodeAsync(owin_user_DTO user, string code, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountCodesAsync(owin_user_DTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }



        public async Task<owin_user_DTO> createUserProfileAsync(owin_user_DTO objUserHRProfile, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

      
    }
}
