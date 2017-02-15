using System;

namespace Powell.Vehicles.Mvc.Manufacturer
{
    [Serializable]
    public class ManufacturerViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
