using System.Collections.Generic;
using Powell.Collections.Generic;

namespace Powell.Vehicles
{
    public class Model : DomainObject
    {
        public virtual Manufacturer Make { get; set; }

        public virtual string Name { get; set; }

        private IList<ModelYear> _modelYears;

        public virtual IList<ModelYear> ModelYears
        {
            get { return _modelYears; }
            protected set { _modelYears = value ?? new List<ModelYear>(); }
        }

        /// <summary>
        /// Gets the <see cref="ModelYears"/> <see cref="IList{ModelYear}"/> for internal use.
        /// </summary>
        protected internal virtual IList<ModelYear> InternalModelYears => ModelYears.ToBidirectionalList(
            a => a.Model = this, r => r.Model = null);

        public Model()
        {
            Initialize();
        }

        private void Initialize()
        {
            Make = new Manufacturer();
            // Make sure that Years is properly initialized.
            ModelYears = null;
        }
    }
}
