using System;
using System.ComponentModel.DataAnnotations;

namespace Powell.Vehicles.Mvc.Vehicle
{
    public class VehicleViewModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// For purposes of this view model, we want to closest match. It is entirely probably
        /// that the client customized their Color, so it will not be an exact match. It just
        /// needs to inform the dialog what a closest starting point might have been in order
        /// to edit.
        /// </summary>
        public Guid ModelYearColorId { get; set; }

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

    public class UpdateVehicleRequestModel
    {
        public Guid VehicleId { get; set; }

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
