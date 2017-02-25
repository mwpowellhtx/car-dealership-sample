using System;
using System.Linq;

namespace Powell.Identity.Domain
{
    internal static class ClaimExtensionMethods
    {
        internal static Claim SetValue<T>(this Claim claim, T value)
        {
            claim.Value = value.ToString();
            //TODO: fill in the supported type here.
            claim.ValueType = null;
            return claim;
        }

        internal static Claim SetValue(this Claim claim, object value)
        {
            //TODO: this may work out: then filter by "types"
            return claim.SetValue(value as string)
                    .SetValue(value as int?)
                    .SetValue(value as DateTime?)
                ;
        }

        private static object GetValue<T>(this Claim claim, Func<string, T> getter)
        {
            return claim.ValueType.Equals(typeof(T).Name)
                ? getter(claim.Value)
                : (object) null;
        }

        internal static object GetValue(this Claim claim)
        {
            return claim.GetValue(x => x)
                   ?? claim.GetValue(long.Parse)
                   ?? claim.GetValue(int.Parse)
                   ?? claim.GetValue(DateTime.Parse)
                ;
        }

        /// <summary>
        /// Returns whether the any of the <paramref name="ids"/> is contained by the set of
        /// <see cref="Claim.Value"/> identifiers. Theoretically, this should work because
        /// <see cref="Guid"/> are designed to be unique, especially at the database primary
        /// key level.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool HasClaimedIds(this User user, params Guid[] ids)
        {
            // Given the nature of Guid, theoretically they should all be unique.
            var guidTypeFullName = typeof(Guid).FullName;
            return user.Claims
                .Where(x => x.ValueType == guidTypeFullName)
                .Select(x =>
                {
                    Guid parsed;
                    if (!Guid.TryParse(x.Value, out parsed))
                        parsed = Guid.Empty;
                    return new {ParsedId = parsed, Claim = x};
                })
                .Any(x => ids.Contains(x.ParsedId));
        }

        // TODO: TBD: consider where better to draw this from: AssemblyInfo?
        /// <summary>
        /// DefaultIssuer: "Kingdom Software"
        /// </summary>
        private const string DefaultIssuer = "Kingdom Software";

        public static bool HasClaims(this User user, string typeUri, Func<Claim, bool> check = null)
        {
            check = check ?? (x => true);
            return user.Claims.Any(x => x.TypeUri.Equals(typeUri) && check(x));
        }

        public static bool IsClaimCurrent(this User user, string typeUri,
            DateTime? effectiveTimeStamp = null)
        {
            // TODO: incorporate SystemTime instead for those times when we might want to simulate the environment ...
            effectiveTimeStamp = effectiveTimeStamp ?? DateTime.UtcNow;

            var current = user.Claims.SingleOrDefault(x => x.TypeUri.Equals(typeUri));

            return current != null && current.Expiry.HasExpired(effectiveTimeStamp);
        }

        public static User AddClaim<T>(this User user, string typeUri,
            T value, Func<T, string> formatter, DateTime? effectiveTimeStamp = null)
        {
            user.InternalClaims.Add(new Claim
            {
                Issuer = DefaultIssuer,
                TypeUri = typeUri,
                // When the Subscription actually started.
                Value = formatter(value),
                ValueType = typeof(T).FullName
            });

            return user;
        }
    }
}
