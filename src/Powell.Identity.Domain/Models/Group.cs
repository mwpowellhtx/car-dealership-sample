using System.Collections.Generic;
using System.Linq;

namespace Powell.Identity.Domain
{
    using Collections.Generic;

    public class Group : CredentialBase
    {
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets an <see cref="IList{Membership}"/> for internal use.
        /// </summary>
        protected internal override IList<Membership> InternalMemberOf => MemberOf.ToBidirectionalList(
            a =>
            {
                a.Member = this;
                var group = a.Group;
                if (group == null || group.Members.Any(a.Equals)) return;
                group.InternalMembers.Add(a);
            },
            r =>
            {
                r.Member = null;
                var group = r.Group;
                if (group == null || !group.Members.Any(r.Equals)) return;
                group.InternalMembers.Remove(r);
            });

        /// <summary>
        /// Members backing field.
        /// </summary>
        private IList<Membership> _members;

        /// <summary>
        /// Gets or sets the Members.
        /// </summary>
        public virtual IList<Membership> Members
        {
            get { return _members; }
            set { _members = value ?? new List<Membership>(); }
        }

        /// <summary>
        /// Gets an <see cref="IList{Membership}"/> for internal use.
        /// </summary>
        protected internal virtual IList<Membership> InternalMembers => Members.ToBidirectionalList(
            a =>
            {
                a.Group = this;
                var member = a.Member;
                if (member == null || member.MemberOf.Any(a.Equals)) return;
                member.InternalMemberOf.Add(a);
            },
            r =>
            {
                r.Group = null;
                var member = r.Member;
                if (member == null || !member.MemberOf.Any(r.Equals)) return;
                member.InternalMemberOf.Remove(r);
            });

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Group()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        private void Initialize()
        {
            Members = null;
            Description = string.Empty;
        }

        /// <summary>
        /// Adds a <see cref="member"/> to the Group Members.
        /// </summary>
        /// <typeparam name="TCredential"></typeparam>
        /// <param name="member"></param>
        protected internal virtual void AddMember<TCredential>(TCredential member)
            where TCredential : CredentialBase, new()
        {
            if (Members.Any(
                x => ReferenceEquals(x.Member, member)
                     || x.Member.Id == member.Id))
            {
                return;
            }

            InternalMembers.Add(new Membership {Member = member});
        }

        /// <summary>
        /// Adds a <see cref="member"/> to the Group Members.
        /// </summary>
        /// <typeparam name="TCredential"></typeparam>
        /// <param name="member"></param>
        protected internal virtual bool RemoveMember<TCredential>(TCredential member)
            where TCredential : CredentialBase, new()
        {
            var m = Members.FirstOrDefault(x => x.Member.Equals(member));
            return m != null && InternalMembers.Remove(m);
        }
    }
}
