using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace InsuranceAPI.AnnotationsConfig;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class RequiredIfAttribute : ValidationAttribute
{
    private readonly string _dependentPropertyName;
    private readonly object _targetValue;

    public RequiredIfAttribute(string dependentPropertyName, object targetValue)
    {
        _dependentPropertyName = dependentPropertyName;
        _targetValue = targetValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var dependentProperty = validationContext.ObjectInstance.GetType().GetProperty(_dependentPropertyName);
        if (dependentProperty == null)
        {
            return new ValidationResult($"Propriedade dependente '{_dependentPropertyName}' não encontrada.");
        }

        var dependentPropertyValue = dependentProperty.GetValue(validationContext.ObjectInstance);
        if (dependentPropertyValue != null && dependentPropertyValue.Equals(_targetValue))
        {
            if (value == null || (value is string str && string.IsNullOrEmpty(str)))
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}
