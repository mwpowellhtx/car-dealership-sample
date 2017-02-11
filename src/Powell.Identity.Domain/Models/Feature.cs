using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace Powell.Identity.Domain
{
    using Collections.Generic;
    using static Privilege;

    public class Feature : ExpiringDomainObject, IRole<Guid>
    {
        #region Role Members

        public virtual string Name { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the Parent.
        /// </summary>
        public virtual Feature Parent { get; set; }

        /// <summary>
        /// Children backing field.
        /// </summary>
        private IList<Feature> _children;

        /// <summary>
        /// Gets or sets the Children.
        /// </summary>
        public virtual IList<Feature> Children
        {
            get { return _children; }
            set { _children = value ?? new List<Feature>(); }
        }

        /// <summary>
        /// Gets an <see cref="IList{Feature}"/> for internal use.
        /// </summary>
        internal IList<Feature> InternalChildren => Children.ToBidirectionalList(
            a => a.Parent = this, r => r.Parent = null);

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets the Feature Branch starting with itself.
        /// </summary>
        internal IEnumerable<Feature> Branch => GetBranch(this);

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
        internal IList<Permission> InternalPermissions => Permissions.ToBidirectionalList(
            a =>
            {
                a.Feature = this;
                var credential = a.Credential;
                if (credential == null || credential.Permissions.Any(a.Equals)) return;
                credential.InternalPermissions.Add(a);
            },
            r =>
            {
                r.Feature = null;
                var credential = r.Credential;
                if (credential == null || !credential.Permissions.Any(r.Equals)) return;
                credential.InternalPermissions.Remove(r);
            });

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Feature()
        {
            Initialize();
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        private void Initialize()
        {
            Parent = null;
            Children = null;
            Permissions = null;
            Description = null;
        }

        /// <summary>
        /// Returns the feature branch starting with itself.
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        private static IEnumerable<Feature> GetBranch(Feature feature)
        {
            while (feature != null)
            {
                yield return feature;
                feature = feature.Parent;
            }
        }

        /// <summary>
        /// Adds <see cref="Permission"/> to the <see cref="credential"/> with
        /// the specified <see cref="Privilege"/>.
        /// </summary>
        /// <param name="credential"></param>
        /// <param name="privilege"></param>
        /// <see cref="CredentialBase.AddRole(Feature,Privilege?)"/>
        internal virtual void AddRole(CredentialBase credential, Privilege? privilege = null)
        {
            privilege = privilege ?? Inherited;

            //Will assume that the Credential Roles already contains him.
            var p = Permissions.SingleOrDefault(r => r.Feature.Equals(this) && r.Credential.Equals(credential));

            if (p != null)
            {
                //Touch the privilege on the way out.
                p.Privilege = privilege;
                return;
            }

            InternalPermissions.Add(new Permission
            {
                Credential = credential,
                Privilege = privilege
            });
        }

        /// <summary>
        /// Removes the role associated with this Feature and the
        /// <see cref="CredentialBase"/> if it exists.
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        public virtual bool RemoveRole(CredentialBase credential)
        {
            var p = Permissions.SingleOrDefault(r => r.Feature.Equals(this) && r.Credential.Equals(credential));
            return p != null && InternalPermissions.Remove(p);
        }
    }
}
