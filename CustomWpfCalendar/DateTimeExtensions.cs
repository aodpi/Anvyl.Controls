using System;

namespace CustomWpfCalendar
{
    static class DateTimeExtensions
    {
        public static DateTime FirstDayInMonth(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, 1);
        }
    }
}

