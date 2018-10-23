using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HSBC.INS.Utils.Wpf
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DateTime? dt = (DateTime?)value;
                if(dt.HasValue)
                    return dt.Value.ToString("yyyy/MM/dd");
            }
            return string.Empty ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string date = value.ToString().Trim();
                string[] formats = { "yyyyMMdd", "yyyyMd", "yyyy/MM/dd", "yyyy/M/d", "yyyy MM dd", "yyyy M d", "yyyy,MM,dd", "yyyy,M,d", "yyyy.MM.dd", "yyyy.M.d" };
                DateTime dt;
                if (DateTime.TryParseExact(date, formats, culture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    return dt;
                }
            }
            return null;
        }
    }
}
