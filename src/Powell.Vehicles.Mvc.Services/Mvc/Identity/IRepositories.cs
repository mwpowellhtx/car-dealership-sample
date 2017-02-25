using System;
using Microsoft.AspNet.Identity;

namespace Powell.Vehicles.Mvc.Identity
{
    using Powell.Identity.Domain;

    public interface IUserRepository<TUser>
        : IIdentityRepository
            , IUserStore<TUser, Guid>
        where TUser : User, new()
    {
    }

    public interface IUserPasswordRepository<TUser>
        : IIdentityRepository
            , IUserPasswordStore<TUser, Guid>
        where TUser : User, new()
    {
    }

    public interface IUserEmailRepository<TUser>
        : IIdentityRepository
            , IUserEmailStore<TUser, Guid>
        where TUser : User, new()
    {
    }

    public interface IUserTwoFactorRepository<TUser>
        : IIdentityRepository
            , IUserTwoFactorStore<TUser, Guid>
        where TUser : User, new()
    {
    }

    public interface IUserLockoutRepository<TUser>
        : IIdentityRepository
            , IUserLockoutStore<TUser, Guid>
        where TUser : User, new()
    {
    }

    public interface IUserLoginRepository<TUser>
        : IIdentityRepository
            , IUserLoginStore<TUser, Guid>
        where TUser : User, new()
    {
    }

    public interface IUserSecurityStampRepository<TUser>
        : IIdentityRepository
            , IUserSecurityStampStore<TUser, Guid>
        where TUser : User, new()
    {
    }

    public interface ICompositeUserRepository<TUser>
        : IUserRepository<TUser>
            , IUserPasswordRepository<TUser>
            , IUserEmailRepository<TUser>
            , IUserTwoFactorRepository<TUser>
            , IUserLockoutRepository<TUser>
            , IUserLoginRepository<TUser>
            , IUserSecurityStampRepository<TUser>
        where TUser : User, new()
    {
    }

    public interface ICompositeUserRepository
        : ICompositeUserRepository<User>
    {
    }
}
