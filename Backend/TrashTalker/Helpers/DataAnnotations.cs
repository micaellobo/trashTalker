using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TrashTalker.Helpers
{
    public class DateFromNow : RangeAttribute
    {
        public DateFromNow(double daysToAddBeginFromNow)
            : base(typeof(DateTime), DateTime.Now.AddDays(daysToAddBeginFromNow).ToString(), DateTime.Now.AddYears(10).ToString())
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Data deve ser superior a {this.Minimum:dd/MM/yyyy HH:mm:ss}";
        }
    }


    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class NotNull : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return value is null ? new ValidationResult(ErrorMessage) : null;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    internal class NonEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value is Guid guid) && Guid.Empty == guid)
                return new ValidationResult(ErrorMessage);

            return null;
        }
    }
}