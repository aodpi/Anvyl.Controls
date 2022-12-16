using System;

namespace Anvyl.Controls.Extensions
{
    internal static class DateTimeExtensions
    {
        public static DateTime FirstDayInMonth(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, 1);
        }

        public static DateTime CreateDayOfWeek(this DateTime date, int dayOfWeek)
        {
            DateTime dt = date;

            int daysUntilDay = (dayOfWeek - (int)dt.DayOfWeek + 7) % 7;
            dt = dt.AddDays(daysUntilDay);
            return dt;
        }
    }
}

