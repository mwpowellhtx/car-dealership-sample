using System;
using System.Collections.Generic;
using Powell.Collections.Generic;

namespace Powell.Vehicles
{
    public class Year : DomainObject
    {
        public virtual DateTime Value { get; set; }

        private IList<ModelYear> _modelYears;

        public virtual IList<ModelYear> ModelYears
        {
            get { return _modelYears; }
            protected set { _modelYears = value ?? new List<ModelYear>(); }
        }

        /// <summary>
        /// Gets the <see cref="ModelYears"/> <see cref="IList{ModelYear}"/> for internal use.
        /// </summary>
        internal IList<ModelYear> InternalModelYears => ModelYears.ToBidirectionalList(a => a.Year = this, r => r.Year = null);

        public Year()
        {
            Initialize();
        }

        private void Initialize()
        {
            Value = new DateTime(DateTime.Now.Year, 1, 1);
            // Make sure collection is properly initialized.
            ModelYears = null;
        }
    }
}
