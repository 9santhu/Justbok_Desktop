using System;
using System.Globalization;
using System.Windows.Data;

namespace JustbokApplication.Convertors
{
    public class BoolToStringConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (System.Convert.ToBoolean(value))
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value.ToString().Trim().ToUpper().Equals("YES"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.LogError(ex, ErrorLog.LogSeverity.Error);
            }
            return false;
        }
    }
}
