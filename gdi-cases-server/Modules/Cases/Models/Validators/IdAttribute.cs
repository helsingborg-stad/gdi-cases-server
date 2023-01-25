using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace gdi_cases_server.Modules.Cases.Models.Validators
{
    public class IdAttribute : ValidationAttribute
    {
        // id should be dense, i.e. no whitespace
        private static Regex matchContent = new Regex("^[\\S]+$", RegexOptions.Multiline);

        public override string FormatErrorMessage(string name) => String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);

        public override bool IsValid(object? value)
        {
            return (value is string) && matchContent.IsMatch(value as string);
        }
    }
}
