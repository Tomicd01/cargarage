using System.ComponentModel.DataAnnotations;
using CarGarageParking.Util;

namespace CarGarageParking.Models
{
    public class VehicleInGarage
    {
        public int VehicleInGarageId { get; set; }
        [Required]
        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
        [Required]
        public int GarageId { get; set; }   
        
        public Garage Garage { get; set; }

        [Required(ErrorMessage = "Entry time is required")]
        [DataType(DataType.DateTime)]
        public DateTime EntryTime { get; set; }

        [IntTypeGreaterThan("EntryTime", ErrorMessage = "Exit time must be greater than entry time.")]
        [DataType(DataType.DateTime)]
        public DateTime? ExitTime { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Hourly rate must be greater than 0.")]
        public decimal HourlyRate { get; set; }

        public int? OwnerId { get; set; }

        public Owner? Owner { get; set; }

        public bool IsVehicleStillInGarage { get; set; } = true;
    }
}
