using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Bookify.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CompareDateWithAttribute : ValidationAttribute
    {
        private readonly bool _compareWithCurrent;

        public CompareDateWithAttribute(string other, bool isBiggerThanOther)
        {
            ArgumentNullException.ThrowIfNull(other);
            Other = other;
            IsBiggerThanOther = isBiggerThanOther;
        }

        public CompareDateWithAttribute(bool isBiggerThanCurrent)
        {
            _compareWithCurrent = true;
            IsBiggerThanOther = isBiggerThanCurrent;
        }

        public string Other { get; }
        public bool IsBiggerThanOther { get; }

        //For comparing two property dates
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime attributePropertyDate;
            DateTime otherDate;

            if (_compareWithCurrent && value != null)
                return CompareWithCurrent(value, validationContext);

            var otherPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(Other);

            if (otherPropertyInfo == null || value == null)
                return ValidationResult.Success;

            var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

            if (otherValue == null)
                return ValidationResult.Success;

            otherDate = otherValue is DateOnly ? ((DateOnly)otherValue).ToDateTime(TimeOnly.MinValue) : (DateTime)otherValue;
            attributePropertyDate = value is DateOnly ? ((DateOnly)value).ToDateTime(TimeOnly.MinValue) : (DateTime)value;

            var compareResult = attributePropertyDate.CompareTo(otherDate);
            if (compareResult != 0 && compareResult > 0 == IsBiggerThanOther)
                return ValidationResult.Success;
            else
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        //For comparing with current date
        public ValidationResult CompareWithCurrent(object value, ValidationContext validationContext)
        {
            var attributeProperty = value is DateOnly ? ((DateOnly)value).ToDateTime(TimeOnly.MinValue) : 
                (DateTime)value;

            var compareResult = attributeProperty.CompareTo(DateTime.Today);
            if (compareResult != 0 && compareResult > 0 == IsBiggerThanOther)
                return ValidationResult.Success;
            else
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}