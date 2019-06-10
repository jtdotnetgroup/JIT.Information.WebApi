using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace JIT.InformationSystem.CommonClass
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field)]
    public sealed class EmailAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var text = value as string;
            if (string.IsNullOrEmpty(text))
                return true;
            else
            {
                var EmailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
                var regex = new Regex(EmailRegex);
                return regex.IsMatch(text);
            }
        }
    }

}