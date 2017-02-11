using System.Collections.Generic;
using Powell.Collections.Generic;

namespace Powell.Vehicles
{
    public class Manufacturer : DomainObject
    {
        public virtual string Name { get; set; }

        private IList<Model> _models;

        public virtual IList<Model> Models
        {
            get { return _models; }
            protected set { _models = value ?? new List<Model>(); }
        }

        /// <summary>
        /// Gets the <see cref="Models"/> <see cref="IList{Model}"/> for internal use.
        /// </summary>
        internal IList<Model> InternalModels => Models.ToBidirectionalList(a => a.Make = this, r => r.Make = null);

        public Manufacturer()
        {
            Initialize();
        }

        private void Initialize()
        {
            // Make sure that Models is properly initialized.
            Models = null;
        }
    }
}
