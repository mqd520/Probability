using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// DateTime
    /// </summary>
    public static class DateTimeTool
    {
        public static int GetTimeStamp(this DateTime datetime)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)((datetime - startTime).TotalSeconds);
        }

        public static long GetMillTimeStamp(this DateTime datetime)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)((datetime - startTime).TotalMilliseconds);
        }

        public static DateTime GetTodayEndTime(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, datetime.Day, 23, 59, 59);
        }
    }
}