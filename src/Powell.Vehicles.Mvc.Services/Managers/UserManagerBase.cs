using System;
using Microsoft.AspNet.Identity;

namespace Powell.Vehicles.Managers
{
    using Identity.Domain;
    using Mvc.Identity;

    public abstract class UserManagerBase<TUser> : UserManager<TUser, Guid>
        where TUser : User, new()
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        protected UserManagerBase(IUserRepository<TUser> repository)
            : base(repository)
        {
        }
    }
}
