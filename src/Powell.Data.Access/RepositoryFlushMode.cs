namespace Powell.Data
{
    public enum RepositoryFlushMode
    {
        /// <summary>
        /// -1
        /// </summary>
        Unspecified = -1,

        /// <summary>
        /// 0
        /// </summary>
        Never = 0,

        /// <summary>
        /// 5
        /// </summary>
        Commit = 5,
        
        /// <summary>
        /// 10
        /// </summary>
        Auto = 10,

        /// <summary>
        /// 20
        /// </summary>
        Always = 20
    }
}
