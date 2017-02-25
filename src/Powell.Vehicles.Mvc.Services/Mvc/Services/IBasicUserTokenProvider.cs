using System;
using Microsoft.AspNet.Identity;
using Powell.Identity.Domain;

namespace Powell.Vehicles.Mvc.Services
{
    using IUserTokenProviderType = IUserTokenProvider<User, Guid>;

    public interface IBasicUserTokenProvider : IUserTokenProviderType
    {
    }
}
