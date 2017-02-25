using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Powell.Vehicles.Managers
{
    using Identity.Domain;
    using static DefaultAuthenticationTypes;

    public static class ApplicationUserExtensionMethods
    {
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync<TUser>(this TUser user,
            UserManagerBase<TUser> manager)
            where TUser : User, new()
            => await manager.CreateIdentityAsync(user, ApplicationCookie);
    }
}
