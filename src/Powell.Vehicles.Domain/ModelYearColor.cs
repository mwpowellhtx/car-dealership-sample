namespace Powell.Vehicles
{
    public class ModelYearColor : DomainObject
    {
        public virtual ModelYear ModelYear { get; set; }

        public virtual Paint Color { get; set; }

        public ModelYearColor()
        {
            Initialize();
        }

        private void Initialize()
        {
        }
    }
}
