using System;
using System.Collections.Generic;
using System.Windows;

namespace Anvyl.Controls.Models.CustomCalendar
{
    internal class CalendarDay : IEquatable<CalendarDay?>
    {
        public CalendarDay(bool isInCurrentMonth, int year, int month, int day, bool isPreviousMonth, bool isNextMonth)
        {
            IsInCurrentMonth = isInCurrentMonth;
            Day = day;
            Year = year;
            Month = month;
            IsPreviousMonth = isPreviousMonth;
            IsNextMonth = isNextMonth;
        }

        public bool IsInCurrentMonth { get; private set; }

        public int Day { get; private set; }

        public int Year { get; private set; }

        public int Month { get; private set; }

        public Rect TargetRect { get; set; }

        public DateTime DateTime
        {
            get
            {
                return new DateTime(Year, Month, Day);
            }
        }

        public override string ToString()
        {
            return Day.ToString();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as CalendarDay);
        }

        public bool Equals(CalendarDay? other)
        {
            return other is not null &&
                   Day == other.Day &&
                   Year == other.Year &&
                   Month == other.Month;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Day, Year, Month);
        }

        public bool IsPreviousMonth { get; private set; }

        public bool IsNextMonth { get; private set; }

        public static bool operator ==(CalendarDay? left, CalendarDay? right)
        {
            return EqualityComparer<CalendarDay>.Default.Equals(left, right);
        }

        public static bool operator !=(CalendarDay? left, CalendarDay? right)
        {
            return !(left == right);
        }
    }
}

