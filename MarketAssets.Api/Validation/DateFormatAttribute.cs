using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MarketAssets.Api.Validation
{
    public class DateFormatAttribute : ValidationAttribute
    {
        private const string DateFormat = "yyyy-MM-dd";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }
            if (value is string dateString)
            {
                if (DateTime.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult($"The field {validationContext.DisplayName} must be a valid date in the format {DateFormat}.");
            }

            return new ValidationResult($"The field {validationContext.DisplayName} must be a string.");
        }
    }
}
