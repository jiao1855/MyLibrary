using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HSBC.INS.Utils.Wpf
{
    public class DateTimeFormatValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string date = value.ToString().Trim();
            string[] formats = { "yyyyMMdd", "yyyyMd", "yyyy/MM/dd", "yyyy/M/d", "yyyy MM dd", "yyyy M d", "yyyy,MM,dd", "yyyy,M,d", "yyyy.MM.dd", "yyyy.M.d" };
            DateTime dt;
            if (DateTime.TryParseExact(date, formats, cultureInfo, System.Globalization.DateTimeStyles.None, out dt))
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "Date format incorrect");
        }
    }
}
