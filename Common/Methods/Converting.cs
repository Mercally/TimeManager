using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Common.Methods
{
    public class Converting
    {
        public static bool StringTryParse(object value, out string obj)
        {
            string _sqlTime = null;
            try
            {
                _sqlTime = Convert.ToString(value);

                obj = _sqlTime;
                return true;
            }
            catch (Exception)
            {
                obj = _sqlTime;
                return false;
            }
        }

        public static TimeSpan SqlTimeToTimeSpan(object sqlTime)
        {
            if (StringTryParse(sqlTime, out string _sqlTime))
            {
                if (TimeSpan.TryParse(_sqlTime, out TimeSpan Result))
                {
                    return Result;
                }
            }

            return TimeSpan.MinValue;
        }

        public static string DateTimeToSqlString(DateTime date, int sumDays = 0)
        {
            if(sumDays > 0)
            {
                date.AddDays(sumDays);
            }
            return $"{date.Year}{date.ToString("MM")}{date.ToString("dd")}";
        }
    }
}
