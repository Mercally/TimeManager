using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Common.Methods
{
    public class Formatting
    {
        public static string TimeSpanToSqlTime(TimeSpan timeSpan)
        {
            return string.Format("{0}:{1}:{2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        public static string SqlStringDateFirstDayCurrentMonth()
        {
            return $"{DateTime.Now.Year}{DateTime.Now.ToString("MM")}01";
        }

        public static string SqlStringDateLastDayCurrentMonth(int sumDays = 0)
        {
            DateTime LastDateInMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            if (sumDays > 0)
            {
                LastDateInMonth = LastDateInMonth.AddDays(sumDays);
            }
            return $"{LastDateInMonth.Year}{LastDateInMonth.ToString("MM")}{LastDateInMonth.ToString("dd")}";
        }
    }
}
