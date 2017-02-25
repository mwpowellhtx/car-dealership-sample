using System;
using Microsoft.AspNet.Identity.Owin;
using Powell.Identity.Domain;

namespace Powell.Vehicles.Mvc.Services
{
    using DataProtectorTokenProviderType = DataProtectorTokenProvider<User, Guid>;

    public class BasicDataProtectorTokenProvider : DataProtectorTokenProviderType
        , IBasicUserTokenProvider
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public BasicDataProtectorTokenProvider(IMachineKeyDataProtector protector)
            : base(protector)
        {
        }
    }
}
