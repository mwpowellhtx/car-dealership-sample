namespace Powell.Vehicles
{
    public class Vehicle : DomainObject
    {
        /// <summary>
        /// Gets or sets the ModelYear.
        /// </summary>
        public virtual ModelYear ModelYear { get; set; }

        /// <summary>
        /// Gets or sets the Vehicle Paint.
        /// </summary>
        public virtual Paint Color { get; set; }

        /// <summary>
        /// Gets or sets the Vehicle Mileage.
        /// </summary>
        /// <remarks>We shall not worry about odometer roll-over for the moment.</remarks>
        public virtual double Mileage { get; set; }

        private string _description;

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public virtual string Description
        {
            get { return _description; }
            set { _description = value ?? string.Empty; }
        }

        public Vehicle()
        {
            Initialize();
        }

        private void Initialize()
        {
            /* Model layer does not have sufficient perspective; leave to service/controller layer to
             * connect the dots between ModelYear, ModelYearColor (default Color source), and Color. */

            Mileage = default(double);
            // Make sure collections and strings are set accordingly.
            Description = null;
        }
    }
}
