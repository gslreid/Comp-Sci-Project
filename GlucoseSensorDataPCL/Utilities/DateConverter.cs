using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlucoseSensorDataPCL.Utilities
{
    class DateConverter
    {
        public static DateTime ToDateTime(long javaDate)
        {
            DateTime epochDate = new DateTime(1970, 1, 1);  // Start of Java Dates
            DateTime zeroDay = new DateTime(1, 1, 1); // Start of .NET Dates

            TimeSpan timeBeforeEpoch = new TimeSpan(epochDate.Ticks - zeroDay.Ticks);
            TimeSpan jsonDate = new TimeSpan(javaDate * 10000 + timeBeforeEpoch.Ticks);
            DateTime localDate = new DateTime(jsonDate.Ticks, DateTimeKind.Local);

            return localDate;
        }
    }
}
