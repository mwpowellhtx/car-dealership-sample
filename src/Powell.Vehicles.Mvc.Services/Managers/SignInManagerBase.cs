using System;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Powell.Vehicles.Managers
{
    using Identity.Domain;

    public abstract class SignInManagerBase<TUser> : SignInManager<TUser, Guid>
        where TUser : User, new()
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        protected SignInManagerBase(UserManagerBase<TUser> userManager, IAuthenticationManager authManager)
            : base(userManager, authManager)
        {
        }
    }
}
