namespace Powell.Vehicles
{
    public class Vehicle : DomainObject, IPaintable
    {
        private ModelYear _modelYear;

        /// <summary>
        /// Gets or sets the ModelYear.
        /// </summary>
        public virtual ModelYear ModelYear
        {
            get { return _modelYear; }
            set
            {
                _modelYear = value;
                Paint = _modelYear.Paint;
                // And if after market paint is desired, then set the Paint freely after that.
            }
        }

        // TODO: TBD: potential joining table: Model-Paint, also Vehicle-Paint is entirely possible
        /// <summary>
        /// Gets or sets the Vehicle Paint.
        /// </summary>
        public virtual Paint Paint { get; set; }

        public Vehicle()
        {
            Initialize();
        }

        private void Initialize()
        {
            // Paint is set upon setting the Model.
            ModelYear = new ModelYear();
        }
    }
}
