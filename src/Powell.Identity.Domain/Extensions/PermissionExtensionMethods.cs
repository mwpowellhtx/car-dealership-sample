using System.Collections.Generic;
using System.Linq;

namespace Powell.Identity.Domain
{
    internal static class PermissionExtensionMethods
    {
        /// <summary>
        /// Returns whether <see cref="values"/> HasPermission <see cref="privilege"/>.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="privilege"></param>
        /// <returns></returns>
        internal static bool HasPermission(this IEnumerable<Permission> values, Privilege privilege)
        {
            return values.Any(r => (r.Privilege & privilege) == privilege);
        }

        /// <summary>
        /// Returns whether <see cref="values"/> DoesNotHavePermission <see cref="privilege"/>.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="privilege"></param>
        /// <returns></returns>
        internal static bool DoesNotHavePermission(this IEnumerable<Permission> values, Privilege privilege)
        {
            return values.All(r => (r.Privilege & privilege) != privilege);
        }
    }
}
