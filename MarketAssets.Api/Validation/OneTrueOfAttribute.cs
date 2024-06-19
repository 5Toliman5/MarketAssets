using System.ComponentModel.DataAnnotations;

namespace MarketAssets.Api.Validation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class OneTrueOfAttribute : ValidationAttribute
    {
        private readonly string[] _propertyNames;

        public OneTrueOfAttribute(params string[] propertyNames)
        {
            _propertyNames = propertyNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = validationContext.ObjectType.GetProperties()
                .Where(p => _propertyNames.Contains(p.Name))
                .ToList();

            var trueCount = properties.Count(p => (bool)p.GetValue(validationContext.ObjectInstance));

            if (trueCount == 1)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"Only one of the following properties can be true: {string.Join(", ", _propertyNames)}");
        }
    }
}
