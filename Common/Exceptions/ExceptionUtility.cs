using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TimeManager.Common.Exceptions
{
    public class ExceptionUtility
    {
        internal const string Empty = "Empty";

        public static void LogError(Exception exception, string message = null, string method = null, string @class = null, string assembly = null)
        {
            Debug.WriteLine($"---------------------------------------------------------------------------------------");
            if (exception == null)
            {
                Debug.WriteLine($"{DateTime.Now.ToShortDateString()} -> Exception: {Empty}");
                return;
            }

            Debug.WriteLine($"{DateTime.Now.ToShortTimeString()} -> Exception.Message: {exception.Message ?? Empty}");
            Debug.WriteLine($"\t\t-> Message: {message ?? Empty}");
            Debug.WriteLine($"\t\t-> Method: {method ?? Empty}");
            Debug.WriteLine($"\t\t-> Class: {@class ?? Empty}");
            Debug.WriteLine($"\t\t-> Assembly: {assembly ?? Empty}");

            if (exception.InnerException != null)
            {
                Debug.WriteLine($"\t\t  -> Exception.InnerException.Message: {exception.InnerException.Message ?? Empty}");
            }
            Debug.WriteLine($"---------------------------------------------------------------------------------------");
        }
    }
}
