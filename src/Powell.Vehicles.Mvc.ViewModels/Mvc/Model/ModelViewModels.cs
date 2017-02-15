using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Powell.Vehicles.Mvc.Model
{
    public class RequestModelsRequestModel
    {
        public Guid? ManufacturerId { get; set; }
    }

    public class AvailableYearsRequestModel
    {
        public Guid ModelId { get; set; }
    }

    [Serializable]
    public class AvailableYearsResponseModel
    {
        public Guid ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public Guid ModelId { get; set; }

        public string ModelName { get; set; }

        public List<int> Years { get; set; }

        public AvailableYearsResponseModel()
        {
            Years = new List<int>();
        }
    }

    public class ModelResponseModel
    {
        public Guid ManufacturerId { get; set; }

        [Display(Name = "Manufacturer")]
        public string ManufacturerName { get; set; }

        public Guid ModelId { get; set; }

        [Display(Name = "Model")]
        public string ModelName { get; set; }
    }
}
