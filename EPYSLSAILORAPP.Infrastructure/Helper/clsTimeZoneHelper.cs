using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Infrastructure.Helper
{
    public static class TimeZoneHelper
    {
        private static readonly TimeZoneInfo DhakaTimezone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Dhaka");

        public static DateTime ConvertFromUtc(DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, DhakaTimezone);
        }
    }
}
