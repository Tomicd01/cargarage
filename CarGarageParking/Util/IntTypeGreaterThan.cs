using System.ComponentModel.DataAnnotations;

namespace CarGarageParking.Util
{
    public class IntTypeGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public IntTypeGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var currentValue = (int)value;
            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (comparisonProperty == null)
            {
                return new ValidationResult($"Property with name {_comparisonProperty} not found.");
            }

            var comparisonValue = (int)comparisonProperty.GetValue(validationContext.ObjectInstance);

            if (value == null || comparisonValue == null)
            {
                return ValidationResult.Success;
            }

            if (value != null && comparisonValue != null && currentValue > comparisonValue)
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater than {_comparisonProperty}");
            }

            return ValidationResult.Success;
        }
    }
}
