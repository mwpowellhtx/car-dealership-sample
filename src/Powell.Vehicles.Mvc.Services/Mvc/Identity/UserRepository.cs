using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Powell.Vehicles.Mvc.Identity
{
    using Data;
    using Powell.Identity.Domain;
    using static DateTime;
    using static StringComparison;

    public class UserRepository : Repository, ICompositeUserRepository
    {
        public UserRepository(IRepositorySessionProvider provider)
            : base(provider)
        {
        }

        #region IUserStore Members

        public Task CreateAsync(User user)
        {
            return Task.Run(() => Transact(r => r.SaveOrUpdate(user)));
        }

        public Task DeleteAsync(User user)
        {
            return Task.Run(() => Transact(r => r.Delete(user)));
        }

        public Task<User> FindByIdAsync(Guid userId)
        {
            return Task.Run(() => Transact(r => r.Get<User>(userId)));
        }

        public Task<User> FindByNameAsync(string userNameOrEmailAddress)
        {
            // User (or in this case, CredentialBase) Name should be a Unique Constraint.
            return Task.Run(() => Transact(r => r.Query<User>(
                x => x.EmailAddress.Equals(userNameOrEmailAddress, InvariantCultureIgnoreCase)
                     || x.Name.Equals(userNameOrEmailAddress, InvariantCultureIgnoreCase)).SingleOrDefault()));
        }

        public Task UpdateAsync(User user)
        {
            return Task.Run(() => Transact(r => r.SaveOrUpdate(user)));
        }

        #endregion

        #region IUserPasswordStore Members

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            return Task.Run(() =>
            {
                user.PasswordHash = passwordHash;
                Transact(r => r.SaveOrUpdate(user));
            });
        }

        #endregion


        #region IUserEmailStore Members    

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.Run(() => user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.Run(() => !string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetEmailAsync(User user, string email)
        {
            return Task.Run(() =>
            {
                user.EmailAddress = email;
                Transact(r => r.SaveOrUpdate(user));
            });
        }

        public Task<string> GetEmailAsync(User user)
        {
            return Task.Run(() => user.EmailAddress);
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            return Task.Run(() => user.IsEmailAddressConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            return Task.Run(() =>
            {
                user.IsEmailAddressConfirmed = confirmed;
                Transact(r => r.SaveOrUpdate(user));
            });
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return Task.Run(() =>
            {
                return Transact(r => r.Query<User>(
                    x => x.EmailAddress.Equals(email,
                        InvariantCultureIgnoreCase)).SingleOrDefault());
            });
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            // TODO: this is probably the best thing to do: return the value ?? UtcNow to avoid inadvertent lockouts
            return Task.Run(() => new DateTimeOffset(user.LockoutExpiryUtc ?? UtcNow));
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            //TODO: this one is critical: set is happening when locked out? not otherwise?
            return Task.Run(() =>
            {
                user.LockoutExpiryUtc = lockoutEnd.DateTime;
                Transact(r => r.Save(user));
            });
        }

        /// <summary>
        /// Sets the <see cref="User.AccessFailedCount"/> to some value determined by
        /// <paramref name="getter"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="getter"></param>
        /// <returns></returns>
        private Task<int> SetAccessFailedCount(User user, Func<User, int> getter)
        {
            return Task.Run(() =>
            {
                var count = user.AccessFailedCount = getter(user);
                Transact(r => r.SaveOrUpdate(user));
                return count;
            });
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return SetAccessFailedCount(user, x => x.AccessFailedCount + 1);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            return SetAccessFailedCount(user, x => default(int));
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.Run(() => user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.Run(() => user.IsLockedOut);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            // TODO: See comments in SetLockoutEndDateAsync: so when we "disable" the lock, then this ought to "clear" the end date?
            return Task.Run(() =>
            {
                user.IsLockedOut = enabled;
                Transact(r => r.SaveOrUpdate(user));
            });
        }

        #endregion

        #region IUserTwoFactorStore Members

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            return Task.Run(() =>
            {
                user.IsTwoFactorEnabled = enabled;
                Transact(r => r.SaveOrUpdate(user));
            });
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.Run(() => user.IsTwoFactorEnabled);
        }

        #endregion

        #region IUserLoginStore Members

        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            return Task.Run(() =>
            {
                user.InternalLogins.Add(login);
                Transact(r => r.SaveOrUpdate(user.Logins.Where(x => x.IsTransient).ToArray()));
            });
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            return Task.Run(() =>
            {
                var loginInfo = user.Logins.SingleOrDefault(
                    x => x.Provider == login.LoginProvider
                         && x.ProviderKey == login.ProviderKey);

                if (loginInfo == null) return;

                // Make sure internal state reflects removal.
                user.InternalLogins.Remove(loginInfo);

                // Then delete from data store.
                Transact(r => Delete(loginInfo));
            });
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            // The conversion operator is implicit, but this will work under these circumstances as well.
            return Task.Run<IList<UserLoginInfo>>(() => user.Logins.Select(x => (UserLoginInfo)x).ToList());
        }

        public Task<User> FindAsync(UserLoginInfo login)
        {
            return Task.Run(() =>
            {
                var loginInfo = Transact(r =>
                    r.Query<LoginInfo>().SingleOrDefault(
                        x => x.Provider == login.LoginProvider
                             && x.ProviderKey == login.ProviderKey));

                return loginInfo?.User;
            });
        }

        #endregion

        #region IUserSecurityStampStore Members

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            return Task.Run(() =>
            {
                user.SecurityStamp = Guid.Parse(stamp);
                Transact(r => r.SaveOrUpdate(user));
            });
        }

        public Task<string> GetSecurityStampAsync(User user)
        {
            return Task.Run(() => user.SecurityStamp.ToString("D"));
        }

        #endregion
    }
}
