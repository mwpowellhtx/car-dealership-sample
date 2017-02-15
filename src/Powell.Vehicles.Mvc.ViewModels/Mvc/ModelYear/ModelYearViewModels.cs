using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Powell.Vehicles.Mvc.ModelYear
{
    public class ModelYearViewModel
    {
        public Guid Id { get; set; }

        public Guid ManufacturerId { get; set; }

        [Display(Name = "Manufactuter")]
        public string ManufacturerName { get; set; }

        public Guid ModelId { get; set; }

        [Display(Name = "Model")]
        public string ModelName { get; set; }

        public int Year { get; set; }

        public Guid ColorId { get; set; }

        [Display(Name = "Color")]
        public string ColorName { get; set; }

        public string ColorValue { get; set; }
    }

    public class RequestModelYearsRequestModel
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

    public class AddModelYearRequestModel
    {
        public Guid ModelId { get; set; }

        public Guid ColorId { get; set; }

        public int Year { get; set; }
    }
}
