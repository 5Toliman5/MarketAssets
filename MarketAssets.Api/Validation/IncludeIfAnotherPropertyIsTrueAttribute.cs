using System.ComponentModel.DataAnnotations;

namespace MarketAssets.Api.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IncludeIfAnotherPropertyIsTrueAttribute : ValidationAttribute
    {
        public string DependentPropertyName { get; }

        public IncludeIfAnotherPropertyIsTrueAttribute(string dependentPropertyName)
        {
            DependentPropertyName = dependentPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dependentPropertyInfo = validationContext.ObjectType.GetProperty(DependentPropertyName);
            if (dependentPropertyInfo == null)
            {
                return new ValidationResult($"Unknown property: {DependentPropertyName}");
            }

            var dependentPropertyValue = (bool)dependentPropertyInfo.GetValue(validationContext.ObjectInstance);
            if (dependentPropertyValue)
            {
                var propertyInfo = validationContext.ObjectType.GetProperty(validationContext.MemberName);
                var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance);

                if (propertyValue == null)
                {
                    return new ValidationResult($"The field {validationContext.DisplayName} is required when {DependentPropertyName} is true.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
