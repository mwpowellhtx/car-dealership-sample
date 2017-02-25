namespace Powell.Data
{
    public interface IRepositorySessionProvider<out TSession>
    {
        /// <summary>
        /// Gets the Session.
        /// </summary>
        TSession Session { get; }
    }
}
