namespace Powell.Domain
{
    public class ExpiringTimeStampModelComponentMap : TimeStampModelComponentMapBase<ExpiringTimeStampModel>
    {
        public ExpiringTimeStampModelComponentMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            Map(x => x.ExpiresOn).Nullable();
        }
    }
}
