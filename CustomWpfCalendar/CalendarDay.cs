using System;
using System.Collections.Generic;
using System.Windows;

namespace CustomWpfCalendar
{
    internal struct CalendarDay : IEquatable<CalendarDay>
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

        public bool IsPreviousMonth { get; private set; }

        public bool IsNextMonth { get; private set; }

        public override bool Equals(object? obj)
        {
            return obj is CalendarDay day && Equals(day);
        }

        public bool Equals(CalendarDay other)
        {
            return IsInCurrentMonth == other.IsInCurrentMonth &&
                   Day == other.Day &&
                   Year == other.Year &&
                   Month == other.Month &&
                   EqualityComparer<Rect>.Default.Equals(TargetRect, other.TargetRect);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsInCurrentMonth, Day, TargetRect);
        }

        public override string ToString()
        {
            return Day.ToString();
        }

        public static bool operator ==(CalendarDay left, CalendarDay right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CalendarDay left, CalendarDay right)
        {
            return !(left == right);
        }
    }
}

