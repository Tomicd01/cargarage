using System.ComponentModel.DataAnnotations;

namespace CarGarageParking.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "License plate is required")]
        [StringLength(15, ErrorMessage = "License plate can't exceed 15 characters!")]
        public string LicensePlate { get; set; }

        public int? OwnerId { get; set; }
        
        public Owner? Owner { get; set; }

    }
}
