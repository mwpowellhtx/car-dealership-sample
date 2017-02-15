using System;
using System.ComponentModel.DataAnnotations;

namespace Powell.Vehicles.Mvc.Vehicle
{
    public class VehicleViewModel
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

        [DisplayFormat(DataFormatString = "{0:#,#0.0###}", ApplyFormatInEditMode = true)]
        public double Mileage { get; set; }

        public string Description { get; set; }
    }

    public class AddVehicleRequestModel
    {
        public Guid ModelYearColorId { get; set; }

        public Guid ColorId { get; set; }

        public double Mileage { get; set; }

        public string Description { get; set; }
    }

    public class DeleteVehicleRequestModel
    {
        public Guid VehicleId { get; set; }
    }
}
