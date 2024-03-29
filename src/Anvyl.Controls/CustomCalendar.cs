﻿using Anvyl.Controls.Models.CustomCalendar;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Anvyl.Controls
{
    public partial class CustomCalendar : FrameworkElement
    {
        private const int MAX_DAYS_WEEK = 7;
        private const int ROWS = 6;
        private const int COLS = 7;
        private List<CalendarDay> calendarDays = new List<CalendarDay>();

        static CustomCalendar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomCalendar), new FrameworkPropertyMetadata(typeof(CustomCalendar)));
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            calendarDays.Clear();

            var days = GetDaysArray(DateTime);

            var cellWidth = (RenderSize.Width - 20) / COLS;
            var cellHeight = (RenderSize.Height - 20) / ROWS;

            double pixelsPerDip = VisualTreeHelper.GetDpi(this).PixelsPerDip;
            Typeface typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
            Pen dayCellPen = new Pen(Brushes.Black, 1);

            Rect backgroundRect = new Rect(20, 20, RenderSize.Width - 20, RenderSize.Height - 20);

            drawingContext.DrawRectangle(Background, null, backgroundRect);

            int index = 0;
            DrawDayNamesAndWeekNumbers(drawingContext, cellWidth, cellHeight, pixelsPerDip, typeface);

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    var currentDay = days[index];

                    DrawDayBorders(drawingContext, ref currentDay, cellWidth, cellHeight, dayCellPen, i, j);

                    DrawDayText(drawingContext, cellWidth, cellHeight, pixelsPerDip, typeface, i, j, currentDay);

                    index++;
                }
            }
        }

        private void DrawDayNamesAndWeekNumbers(DrawingContext drawingContext, double cellWidth, double cellHeight, double pixelsPerDip, Typeface typeface)
        {
            var firstDayOfWeek = GetCurrentFirstDayOfWeek();

            for (int i = 0; i < 7; i++)
            {
                var date = DateTime.MinValue.CreateDayOfWeek(firstDayOfWeek++);
                FormattedText dayText = new FormattedText(date.ToString("ddd"), CultureInfo.CurrentCulture, FlowDirection, typeface, 15, Brushes.Black, pixelsPerDip);
                drawingContext.DrawText(dayText, new Point(i * cellWidth + 20 + ((cellWidth - dayText.Width) / 2), 0));
            }

            // Week numbers
            for (int i = 0; i < 6; i++)
            {
                DateTime dt = new DateTime(DateTime.Year, DateTime.Month, 1).AddDays(i * 7);
                var weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

                var weekDayText = new FormattedText(weekNumber.ToString(), CultureInfo.CurrentCulture, FlowDirection, typeface, 12, Brushes.Black, pixelsPerDip);
                var textY = (i * cellHeight) + 20 + ((cellHeight - weekDayText.Height) / 2);
                drawingContext.DrawText(weekDayText, new Point(0, textY));
            }
        }

        private void DrawDayBorders(DrawingContext drawingContext, ref CalendarDay currentDay, double cellWidth, double cellHeight, Pen dayCellPen, int i, int j)
        {
            Rect cellBorderRect = new Rect(new Point((j * cellWidth) + 20, (i * cellHeight) + 20), new Size(cellWidth, cellHeight));

            if (j < (COLS - 1))
                drawingContext.DrawLine(dayCellPen, cellBorderRect.TopRight, cellBorderRect.BottomRight);

            if (i < ROWS - 1)
                drawingContext.DrawLine(dayCellPen, cellBorderRect.BottomLeft, cellBorderRect.BottomRight);

            currentDay.TargetRect = cellBorderRect;

            if (currentDay.DateTime == new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
            {
                drawingContext.DrawRectangle(CurrentDayBackground, null, Rect.Inflate(cellBorderRect, -.5, -.5));
            }

            if (currentDay.IsPreviousMonth)
            {
                drawingContext.DrawRectangle(PreviousMonthBackground, null, Rect.Inflate(cellBorderRect, -.5, -.5));
            }


            if (currentDay.IsNextMonth)
            {
                drawingContext.DrawRectangle(NextMonthBackground, null, Rect.Inflate(cellBorderRect, -.5, -.5));
            }

            calendarDays.Add(currentDay);
        }

        private void DrawDayText(DrawingContext drawingContext, double cellWidth, double cellHeight, double pixelsPerDip, Typeface typeface, int i, int j, CalendarDay currentDay)
        {
            Brush dayBrush = currentDay.IsInCurrentMonth ? Foreground : Brushes.DarkGray;

            if (currentDay.DateTime == new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
            {
                dayBrush = CurrentDayForeground;
            }

            if (currentDay.IsPreviousMonth)
                dayBrush = PreviousMonthForeground;

            if (currentDay.IsNextMonth)
                dayBrush = NextMonthForeground;

            var text = new FormattedText(currentDay.ToString(), CultureInfo.CurrentCulture, FlowDirection, typeface, FontSize, dayBrush, pixelsPerDip);
            var tX = (j * cellWidth) + 30;
            var tY = (i * cellHeight) + 30;

            Point textOrigin = new Point(tX, tY);
            drawingContext.DrawText(text, textOrigin);
        }

        private static CalendarDay[] GetDaysArray(DateTime date)
        {
            var calendar = CultureInfo.CurrentCulture.Calendar;

            CalendarDay[] days = new CalendarDay[42];

            var firstDay = date.FirstDayInMonth();

            var dayOfWeek = (int)calendar.GetDayOfWeek(firstDay);

            var maximumDays = calendar.GetMaxDaysInMonth(firstDay);

            int previousMonthDayCount = GetPreviousMountDayCount(dayOfWeek);

            int index = 0;

            if (previousMonthDayCount > 0)
            {
                var previousMonth = firstDay.AddMonths(-1);

                var previousMax = calendar.GetMaxDaysInMonth(previousMonth);

                for (int i = previousMonthDayCount; i > 0; i--)
                {
                    int day = previousMax - i + 1;

                    days[index] = new CalendarDay(false, previousMonth.Year, previousMonth.Month, day, true, false);
                    index++;
                }
            }

            for (int i = 0; i < maximumDays; i++)
            {
                days[index] = new CalendarDay(true, firstDay.Year, firstDay.Month, i + 1, false, false);
                firstDay = firstDay.AddDays(1);
                index++;
            }

            int nextMonthDay = 1;

            for (int i = index; i < days.Length; i++)
            {
                days[index] = new CalendarDay(false, firstDay.Year, firstDay.Month, nextMonthDay, false, true);
                nextMonthDay++;
                index++;
                firstDay = firstDay.AddDays(1);
            }

            return days;
        }

        private static int GetPreviousMountDayCount(int dayOfWeek)
        {
            return (dayOfWeek - GetCurrentFirstDayOfWeek() + MAX_DAYS_WEEK) % MAX_DAYS_WEEK;
        }

        private static int GetCurrentFirstDayOfWeek()
        {
            return (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            var position = e.GetPosition(this);

            var hit = calendarDays.Where(x => x.TargetRect.Contains(position)).FirstOrDefault();

            if (hit is not null)
            {
                DateTime = hit.DateTime;
            }
        }
    }
}

