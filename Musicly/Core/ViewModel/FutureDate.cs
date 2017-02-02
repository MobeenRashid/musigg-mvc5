using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Musicly.Core.ViewModel
{
    public class FutureDate : ValidationAttribute
    {
        public FutureDate()
        {
            ErrorMessage = "Please provide any Future date";
        }
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value), "dd MMM yyyy", CultureInfo.CurrentUICulture, DateTimeStyles.None, out dateTime);

            return (isValid && dateTime > DateTime.Now);
        }
    }
}