using System;
using System.ComponentModel.DataAnnotations;

namespace Powell.Vehicles.Mvc.Model
{
    public class RequestModelsRequestModel
    {
        public Guid? ManufacturerId { get; set; }
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
