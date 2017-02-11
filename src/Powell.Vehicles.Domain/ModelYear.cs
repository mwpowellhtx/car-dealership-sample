using System;
using System.Collections.Generic;
using Powell.Collections.Generic;

namespace Powell.Vehicles
{
    public class ModelYear : DomainObject, IPaintable
    {
        public virtual int Year { get; set; }

        public virtual Model Model { get; set; }

        private IList<Vehicle> _vehicles;

        public virtual IList<Vehicle> Vehicles
        {
            get { return _vehicles; }
            internal set { _vehicles = value ?? new List<Vehicle>(); }
        }

        /// <summary>
        /// Gets the <see cref="Vehicles"/> <see cref="IList{Vehicle}"/> for internal use.
        /// </summary>
        internal IList<Vehicle> InternalVehicles
            => Vehicles.ToBidirectionalList(
                a => a.ModelYear = this, r => r.ModelYear = null);

        // TODO: TBD: potentially could normalize "paint" from even ModelYears ...
        /// <summary>
        /// Gets or sets the Paint which in which the Model is available.
        /// </summary>
        public virtual Paint Paint { get; set; }

        public ModelYear()
        {
            Initialize();
        }

        private void Initialize()
        {
            // Make sure that Vehicles is properly initialized.
            Vehicles = null;
            Year = DateTime.Now.Year;
            Model = new Model();
            Paint = new Paint();
        }
    }
}
