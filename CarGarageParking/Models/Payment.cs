using System.ComponentModel.DataAnnotations;
using CarGarageParking.Util;

namespace CarGarageParking.Models
{
    public class Payment : IValidatableObject
    {
        public int PaymentId { get; set; }

        [Range(0.01,double.MaxValue, ErrorMessage = "Total charge must be greater than 0")]
        [IntTypeGreaterThan("VehicleHourlyRate", ErrorMessage = "TotalCharge must be greater than vehicle hourly rate.")]
        public decimal TotalCharge { get; set; }

        public bool IsPaid { get; set; }

        [Required(ErrorMessage = "Payment time is required")]
        public DateTime PaymentTime { get; set; }

        [DateGreaterThan("EntryTime", ErrorMessage = "Expiration must be greater than payment time.")]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationTime { get; set; }

        [Required]
        public int VehicleInGarageId { get; set; }

        public decimal VehicleHourlyRate { get; set; }

        public VehicleInGarage VehicleInGarage { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsPaid)
            {
                yield return new ValidationResult("Payment must be paid.", new[] { nameof(IsPaid) });
            }

            if ((DateTime.Now - PaymentTime).TotalMinutes > 15)
            {
                VehicleInGarage.EntryTime = ExpirationTime;
                yield return new ValidationResult("You have exceeded time to leave a garage, new cycle has started.", new[] { nameof(ExpirationTime) });

            }

            var totalHours = Math.Ceiling((PaymentTime - VehicleInGarage.EntryTime).TotalHours);
            var requiredCharge = (decimal)totalHours * VehicleInGarage.HourlyRate;

            if (TotalCharge < requiredCharge)
            {
                yield return new ValidationResult($"Total charge must be greater than or equal to {requiredCharge}.", new[] { nameof(TotalCharge) });
            }
        }
    }
}
