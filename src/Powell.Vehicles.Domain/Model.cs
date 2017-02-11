using System.Collections.Generic;
using Powell.Collections.Generic;

namespace Powell.Vehicles
{
    public class Model : DomainObject
    {
        public virtual Manufacturer Make { get; set; }

        public virtual string Name { get; set; }

        private IList<ModelYear> _years;

        public virtual IList<ModelYear> Years
        {
            get { return _years; }
            protected set { _years = value ?? new List<ModelYear>(); }
        }

        /// <summary>
        /// Gets the <see cref="Years"/> <see cref="IList{ModelYear}"/> for internal use.
        /// </summary>
        internal IList<ModelYear> InternalYears => Years.ToBidirectionalList(a => a.Model = this, r => r.Model = null);

        public Model()
        {
            Initialize();
        }

        private void Initialize()
        {
            Make = new Manufacturer();
            // Make sure that Years is properly initialized.
            Years = null;
        }
    }
}
