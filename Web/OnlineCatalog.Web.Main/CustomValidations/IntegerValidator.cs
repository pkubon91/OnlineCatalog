using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineCatalog.Web.Main.CustomValidations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IntegerValidator : ValidationAttribute
    {
        private readonly int _minValue;
        private readonly int _maxValue;

        public IntegerValidator(int minValue, int maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Value is not provided");
            
            int parsedIntValue;
            if (!int.TryParse(value.ToString(), out parsedIntValue)) return new ValidationResult("Provided value is not integer");
            
            if(parsedIntValue < _minValue) return new ValidationResult($"Provided value is less than {_minValue}");
            if(parsedIntValue > _maxValue) return new ValidationResult($"Provided value is greater than {_maxValue}");
            return ValidationResult.Success;
        }
    }
}