using System;

namespace Anvyl.Controls.Extensions
{
    static class CalendarExtensions
    {
        public static int GetMaxDaysInMonth(this System.Globalization.Calendar c, DateTime time)
        {
            return c.GetDaysInMonth(time.Year, time.Month, c.GetEra(time));
        }
    }
}

