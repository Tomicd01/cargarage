using System.ComponentModel.DataAnnotations;
using CarGarageParking.Services;

namespace CarGarageParking.Models.ViewModel
{
    public class ApplicationRegistrationViewModel
    {
        [Required]
        public Owner Owner { get; set; }
        [Range(1, 10, ErrorMessage = "You can register between 1 and 10 vehicles.")]
        public int? NumberOfVehicles { get; set; }
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}
