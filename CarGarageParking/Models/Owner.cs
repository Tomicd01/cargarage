using System.ComponentModel.DataAnnotations;

namespace CarGarageParking.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }
        [Required(ErrorMessage = "First name is mandatory!")]
        [StringLength(50, ErrorMessage = "Must have less than 50 characters!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is mandatory!")]
        [StringLength(50, ErrorMessage = "Must have less than 50 characters!")]
        public string LastName { get; set; }
  
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public ICollection<VehicleInGarage> VehicleInGarages { get; set; } = new List<VehicleInGarage>();

    }
}
