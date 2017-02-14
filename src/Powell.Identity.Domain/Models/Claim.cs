namespace Powell.Identity.Domain
{
    // TODO: TBD: transfer over the claims extension methods as well...
    using ClaimType = System.Security.Claims.Claim;

    //TODO: we may need/want System.Security.Claims.ClaimValueTypes as well, or some variation of those from:
    // https://msdn.microsoft.com/en-us/library/microsoft.identitymodel.claims.claimvaluetypes.aspx
    // https://msdn.microsoft.com/en-us/library/system.security.claims.claimvaluetypes.aspx
    // TODO: there may be other, better, or more comprehensive resources from which to draw such types

    // TODO: may want to install our own self-signed, or authority-signed, X.509 (?) certificate
    // Key information: "Issuer", in this case, for internal use, of course we "trust" the Issuer

    public class Claim : ExpiringDomainObject
    {
        /// <summary>
        /// Gets or sets the TypeUri for the Claim. The claim type or the type of claim that is
        /// represented is sort of a misnomer. I do not know the specific origins of what claim
        /// type means, but apparently this is universally represented as a Uri. The package of
        /// which claims is referred to as a Token.
        /// </summary>
        public virtual string TypeUri { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.String"/> representation of the Claim.
        /// </summary>
        public virtual string Value { get; protected internal set; }

        /// <summary>
        /// Gets or sets the ValueType.
        /// </summary>
        public virtual string ValueType { get; protected internal set; }

        public virtual string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the User for in whose behalf the Claim is acquired.
        /// </summary>
        public virtual User User { get; set; }

        public Claim()
        {
            Initialize();
        }

        private void Initialize()
        {
            TypeUri = null;
            Value = null;
            ValueType = null;
            User = null;
            Expiry = null;
        }

        public static implicit operator ClaimType(Claim claim)
        {
            return new ClaimType(claim.TypeUri, claim.Value, claim.ValueType, claim.Issuer);
        }
    }
}
