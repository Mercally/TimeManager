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
    }
}
