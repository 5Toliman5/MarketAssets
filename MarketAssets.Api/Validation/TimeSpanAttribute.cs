using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MarketAssets.Api.Validation
{
    public class TimeSpanAttribute : ValidationAttribute
    {
        private const string TimeSpanFormat = @"d\.hh\:mm\:ss";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string timeSpanString)
            {
                if (TimeSpan.TryParseExact(timeSpanString, TimeSpanFormat, CultureInfo.InvariantCulture, out _))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult($"The field {validationContext.DisplayName} must be a valid time span in the format {TimeSpanFormat}.");
            }

            return new ValidationResult($"The field {validationContext.DisplayName} must be a string.");
        }
    }
}
