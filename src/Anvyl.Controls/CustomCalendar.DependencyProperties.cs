namespace Anvyl.Controls
{
    public partial class CustomCalendar
    {
        public static readonly DependencyProperty DateTimeProperty =
           DependencyProperty.Register(nameof(DateTime), typeof(DateTime), typeof(CustomCalendar), new PropertyMetadata(DateTime.Now));

        public static readonly DependencyProperty CurrentDayForegroundProperty =
            DependencyProperty.Register(nameof(CurrentDayForeground), typeof(Brush), typeof(CustomCalendar), new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty CurrentDayBackgroundProperty =
            DependencyProperty.Register(nameof(CurrentDayBackground), typeof(Brush), typeof(CustomCalendar), new PropertyMetadata(Brushes.Beige));

        public static readonly DependencyProperty PreviousMonthForegroundProperty =
            DependencyProperty.Register(nameof(PreviousMonthForeground), typeof(Brush), typeof(CustomCalendar), new PropertyMetadata(Brushes.DarkGray));

        public static readonly DependencyProperty PreviousMonthBackgroundProperty =
            DependencyProperty.Register(nameof(PreviousMonthBackground), typeof(Brush), typeof(CustomCalendar), new PropertyMetadata(Brushes.White));

        public static readonly DependencyProperty NextMonthBackgroundProperty =
            DependencyProperty.Register(nameof(NextMonthBackground), typeof(Brush), typeof(CustomCalendar), new PropertyMetadata(Brushes.White));

        public static readonly DependencyProperty NextMonthForegroundProperty =
            DependencyProperty.Register(nameof(NextMonthForeground), typeof(Brush), typeof(CustomCalendar), new PropertyMetadata(Brushes.DarkGray));

        public static readonly DependencyProperty FontStyleProperty =
            DependencyProperty.Register(nameof(FontStyle), typeof(FontStyle), typeof(CustomCalendar), new PropertyMetadata(FontStyles.Normal));

        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register(nameof(FontFamily), typeof(FontFamily), typeof(CustomCalendar), new PropertyMetadata(new FontFamily("Segoe UI")));

        public static readonly DependencyProperty FontWeightProperty =
            DependencyProperty.Register(nameof(FontWeight), typeof(FontWeight), typeof(CustomCalendar), new PropertyMetadata(FontWeights.Normal));

        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register(nameof(FontSize), typeof(double), typeof(CustomCalendar), new PropertyMetadata(15d));

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(CustomCalendar), new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty FontStretchProperty =
            DependencyProperty.Register(nameof(FontStretch), typeof(FontStretch), typeof(CustomCalendar), new PropertyMetadata(FontStretches.Normal));

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(CustomCalendar), new PropertyMetadata(Brushes.White));


        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public FontStyle FontStyle
        {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        public FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public FontStretch FontStretch
        {
            get { return (FontStretch)GetValue(FontStretchProperty); }
            set { SetValue(FontStretchProperty, value); }
        }

        public Brush NextMonthForeground
        {
            get { return (Brush)GetValue(NextMonthForegroundProperty); }
            set { SetValue(NextMonthForegroundProperty, value); }
        }

        public Brush NextMonthBackground
        {
            get { return (Brush)GetValue(NextMonthBackgroundProperty); }
            set { SetValue(NextMonthBackgroundProperty, value); }
        }

        public Brush PreviousMonthBackground
        {
            get { return (Brush)GetValue(PreviousMonthBackgroundProperty); }
            set { SetValue(PreviousMonthBackgroundProperty, value); }
        }

        public Brush PreviousMonthForeground
        {
            get { return (Brush)GetValue(PreviousMonthForegroundProperty); }
            set { SetValue(PreviousMonthForegroundProperty, value); }
        }

        public DateTime DateTime
        {
            get { return (DateTime)GetValue(DateTimeProperty); }
            set { SetValue(DateTimeProperty, value); }
        }

        public Brush CurrentDayForeground
        {
            get { return (Brush)GetValue(CurrentDayForegroundProperty); }
            set { SetValue(CurrentDayForegroundProperty, value); }
        }

        public Brush CurrentDayBackground
        {
            get { return (Brush)GetValue(CurrentDayBackgroundProperty); }
            set { SetValue(CurrentDayBackgroundProperty, value); }
        }
    }
}

