using System.ComponentModel.DataAnnotations;
using CarGarageParking.Util;

namespace CarGarageParking.Models
{
    public class Garage
    {
        public int GarageId { get; set; }
        [Required(ErrorMessage = "Name is mandatory!")]
        [StringLength(50, ErrorMessage = "Must have less than 50 characters!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Location is mandatory!")]
        [StringLength(150, ErrorMessage = "Must have less than 150 characters!")]
        public string Location { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Current occupancy must be greater than or equal to 0")]
        [IntTypeGreaterThan("Capacity", ErrorMessage = "Capacity must be greater than occupancy.")]
        public int CurrentOccupancy { get; set; }

        public int AvailableSpots
        {
            get
            {
                return Capacity - CurrentOccupancy;
            }
        }

        public ICollection<VehicleInGarage> VehicleInGarage { get; set; } = new List<VehicleInGarage>();

        public bool IsFull
        {
            get 
            {
                return CurrentOccupancy >= Capacity;
            }
        }

    }
}
