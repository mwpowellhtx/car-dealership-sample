using System;

namespace Powell.Identity.Domain
{
    [Flags]
    public enum Privilege : int
    {
        //TODO: then need to consider weakest/strongest heuristics
        /// <summary>
        /// NotSet (0)
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// Allow (0x1)
        /// </summary>
        Allow = 1 << 0,

        /// <summary>
        /// Deny (0x2)
        /// </summary>
        Deny = 1 << 1,

        /// <summary>
        /// Inherited (0x4)
        /// </summary>
        Inherited = 1 << 2
    }
}
