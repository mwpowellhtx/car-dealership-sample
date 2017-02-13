using System;
using System.Collections.Generic;
using System.Linq;

namespace Powell.Identity.Domain
{
    using Collections.Generic;
    using static Privilege;
    using static AccessControlStrategy;

    public abstract class CredentialBase : ExpiringDomainObject
    {
        /// <summary>
        /// Gets or sets the CredentialBase Name.
        /// </summary>
        /// <see cref="User.UserName"/>
        /// <see cref="Group.Name"/>
        public virtual string Name { get; set; }

        /// <summary>
        /// MemberOf backing field.
        /// </summary>
        private IList<Membership> _memberOf;

        /// <summary>
        /// Gets or sets the MemberOf.
        /// </summary>
        public virtual IList<Membership> MemberOf
        {
            get { return _memberOf; }
            set { _memberOf = value ?? new List<Membership>(); }
        }

        /// <summary>
        /// Gets an <see cref="IList{Membership}"/> for internal use.
        /// </summary>
        protected internal abstract IList<Membership> InternalMemberOf { get; }

        /// <summary>
        /// Permissions backing field.
        /// </summary>
        private IList<Permission> _permissions;

        /// <summary>
        /// Gets or sets the Permissions.
        /// </summary>
        public virtual IList<Permission> Permissions
        {
            get { return _permissions; }
            set { _permissions = value ?? new List<Permission>(); }
        }

        /// <summary>
        /// Gets an <see cref="IList{Permission}"/> for internal use.
        /// </summary>
        protected internal virtual IList<Permission> InternalPermissions => Permissions.ToBidirectionalList(
            a =>
            {
                a.Credential = this;
                var feature = a.Feature;
                if (feature == null || feature.Permissions.Any(a.Equals)) return;
                feature.InternalPermissions.Add(a);
            },
            r =>
            {
                r.Credential = null;
                var feature = r.Feature;
                if (feature == null || !feature.Permissions.Any(r.Equals)) return;
                feature.InternalPermissions.Remove(r);
            });

        /// <summary>
        /// Protected Default Constructor
        /// </summary>
        protected CredentialBase()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            MemberOf = null;
            Permissions = null;
        }

        /// <summary>
        /// Adds <see cref="Permission"/> to the <see cref="Feature"/> with the
        /// specified <see cref="Privilege"/>.
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="privilege"></param>
        /// <see cref="Feature.AddRole(CredentialBase,Privilege?)"/>
        protected internal virtual void AddRole(Feature feature, Privilege? privilege = null)
        {
            privilege = privilege ?? Inherited;

            // Will assume that the Feature Roles already contains him.
            var p = Permissions.SingleOrDefault(r => r.Credential.Equals(this) && r.Feature.Equals(feature));

            if (p != null)
            {
                // Touch the privilege on the way out.
                p.Privilege = privilege;
                return;
            }

            InternalPermissions.Add(new Permission
            {
                Feature = feature,
                Privilege = privilege
            });
        }

        /// <summary>
        /// Removes the role associated with this Credential and the
        /// <see cref="Feature"/> if it exists.
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public virtual bool RemoveRole(Feature feature)
        {
            var p = Permissions.SingleOrDefault(r => r.Credential.Equals(this) && r.Feature.Equals(feature));
            return p != null && InternalPermissions.Remove(p);
        }

        /// <summary>
        /// Returns whether this Credential has Privilege among his Roles for
        /// the Feature or whether any of his Memberships has the Privilege.
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="strategy"></param>
        /// <param name="until"></param>
        /// <returns></returns>
        public virtual bool? CanAccess(Feature feature, AccessControlStrategy strategy, DateTime? until = null)
        {
            //TODO: None? NotSet?
            //TODO: yes, this is roughly loosely analog to TFS 2013 security
            //TODO: also borrowing from a couple other role-based models...

            const bool opt = true; //opt-imistic
            const bool pes = false; //pes-simistic

            //Credentials having access is ruled out here.
            if (!Expiry.HasAccess(until)) return null;

            while (feature != null)
            {
                // Focus on just this feature and its branch (later on).
                var featuredRoles = Permissions.Where(r => r.Expiry.HasAccess(until) && r.Feature.Equals(feature)).ToArray();

                // Return the desired response when privilege is met.
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (strategy)
                {
                    case Optimistic:
                        if (featuredRoles.HasPermission(Allow)) return opt;
                        break;

                    case Pessimistic:
                        if (featuredRoles.HasPermission(Deny)) return pes;
                        break;
                }

                // See above re: ruling out Credentials HasAccess concerns.
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (strategy)
                {
                    case Optimistic:
                        if (MemberOf.Any(m => m.Group.CanAccess(feature, strategy, until) == opt))
                            return opt;
                        break;

                    case Pessimistic:
                        if (MemberOf.Any(m => m.Group.CanAccess(feature, strategy, until) == pes))
                            return pes;
                        break;
                }

                // Break when we are not talking about an inherited permission.
                if (featuredRoles.DoesNotHavePermission(Inherited)) break;

                // Get the next one or Null when there are no parents.
                feature = feature.Branch.Skip(1).FirstOrDefault();
            }

            return null;
        }
    }
}
