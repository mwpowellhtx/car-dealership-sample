using System;
using System.Collections.Generic;
using Powell.Collections.Generic;

namespace Powell.Vehicles
{
    public class ModelYear : DomainObject
    {
        public virtual Model Model { get; set; }

        private Year _year;

        /// <summary>
        /// Gets or sets the ModelYear Year. Set <see cref="ProducedOn"/> differently when other
#pragma warning disable 1584, 1711, 1572, 1581, 1580
        /// than the <see cref="Year.Value"/>.
#pragma warning restore 1584, 1711, 1572, 1581, 1580
        /// </summary>
        public virtual Year Year
        {
            get { return _year; }
            set
            {
                _year = value;
                // Change ProducedOn if different from Year.
                ProducedOn = _year.Value;
            }
        }

        public virtual DateTime ProducedOn { get; set; }

        private IList<Vehicle> _vehicles;

        /// <summary>
        /// Gets the Vehicles in the ModelYear.
        /// </summary>
        public virtual IList<Vehicle> Vehicles
        {
            get { return _vehicles; }
            protected internal set { _vehicles = value ?? new List<Vehicle>(); }
        }

        /// <summary>
        /// Gets the <see cref="Vehicles"/> <see cref="IList{Vehicle}"/> for internal use.
        /// </summary>
        protected internal virtual IList<Vehicle> InternalVehicles => Vehicles.ToBidirectionalList(
            a => a.ModelYear = this, r => r.ModelYear = null);

        // TODO: TBD: potentially could normalize "paint" from even ModelYears ...

        private IList<ModelYearColor> _colors;

        /// <summary>
        /// Gets the Colors in which the ModelYear is available.
        /// </summary>
        public virtual IList<ModelYearColor> Colors
        {
            get { return _colors; }
            protected set { _colors = value ?? new List<ModelYearColor>(); }
        }

        /// <summary>
        /// Gets the <see cref="Colors"/> <see cref="IList{ModelYearColor}"/> for internal use.
        /// </summary>
        protected internal virtual IList<ModelYearColor> InternalColors => Colors.ToBidirectionalList(
            a => a.ModelYear = this, r => r.ModelYear = null);

        public ModelYear()
        {
            Initialize();
        }

        private void Initialize()
        {
            Year = new Year();
            Model = new Model();
            // Make sure that collections are properly initialized.
            Vehicles = null;
            Colors = null;
        }
    }
}
