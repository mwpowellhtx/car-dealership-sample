using System;
using Microsoft.AspNet.Identity;

namespace Powell.Vehicles.Mvc.Identity
{
    using Powell.Identity.Domain;

    public interface IIdentityUserStore<TUser> : IUserStore<TUser, Guid>
        where TUser : User, new()
    {
    }
}
