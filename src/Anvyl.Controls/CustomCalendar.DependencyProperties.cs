using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Anvyl.Controls
{
    public partial class CustomCalendar
    {
        public static readonly DependencyProperty DateTimeProperty =
           DependencyProperty.Register(nameof(DateTime), typeof(DateTime), typeof(CustomCalendar), new FrameworkPropertyMetadata(DateTime.Now, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty CurrentDayForegroundProperty =
            DependencyProperty.Register(nameof(CurrentDayForeground), typeof(Brush), typeof(CustomCalendar), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty CurrentDayBackgroundProperty =
            DependencyProperty.Register(nameof(CurrentDayBackground), typeof(Brush), typeof(CustomCalendar), new FrameworkPropertyMetadata(Brushes.Beige, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty PreviousMonthForegroundProperty =
            DependencyProperty.Register(nameof(PreviousMonthForeground), typeof(Brush), typeof(CustomCalendar), new FrameworkPropertyMetadata(Brushes.DarkGray, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty PreviousMonthBackgroundProperty =
            DependencyProperty.Register(nameof(PreviousMonthBackground), typeof(Brush), typeof(CustomCalendar), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty NextMonthBackgroundProperty =
            DependencyProperty.Register(nameof(NextMonthBackground), typeof(Brush), typeof(CustomCalendar), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty NextMonthForegroundProperty =
            DependencyProperty.Register(nameof(NextMonthForeground), typeof(Brush), typeof(CustomCalendar), new FrameworkPropertyMetadata(Brushes.DarkGray, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FontFamilyProperty =
            TextElement.FontFamilyProperty.AddOwner(typeof(CustomCalendar), new FrameworkPropertyMetadata(SystemFonts.MessageFontFamily, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FontSizeProperty =
            TextElement.FontSizeProperty.AddOwner(typeof(CustomCalendar), new FrameworkPropertyMetadata(SystemFonts.MessageFontSize, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FontStretchProperty =
            TextElement.FontStretchProperty.AddOwner(typeof(CustomCalendar), new FrameworkPropertyMetadata(TextElement.FontStretchProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FontStyleProperty =
            TextElement.FontStyleProperty.AddOwner(typeof(CustomCalendar), new FrameworkPropertyMetadata(SystemFonts.MessageFontStyle, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FontWeightProperty =
            TextElement.FontWeightProperty.AddOwner(typeof(CustomCalendar), new FrameworkPropertyMetadata(SystemFonts.MessageFontWeight, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty BackgroundProperty =
            Panel.BackgroundProperty.AddOwner(typeof(CustomCalendar), new FrameworkPropertyMetadata(Panel.BackgroundProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ForegroundProperty =
            TextElement.ForegroundProperty.AddOwner(typeof(CustomCalendar), new FrameworkPropertyMetadata(SystemColors.ControlTextBrush, FrameworkPropertyMetadataOptions.Inherits));

        [Bindable(true), Category("Appearance")]
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        [Bindable(true), Category("Appearance")]
        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        [Bindable(true), Category("Appearance")]
        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value);}
        }

        [Bindable(true), Category("Appearance")]
        public FontStyle FontStyle
        {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        [Bindable(true), Category("Appearance")]
        public FontStretch FontStretch
        {
            get { return (FontStretch)GetValue(FontStretchProperty); }
            set { SetValue(FontStretchProperty, value); }
        }

        [Bindable(true), Category("Text")]
        [TypeConverter(typeof(FontSizeConverter))]
        [Localizability(LocalizationCategory.Font)]
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        [Bindable(true), Category("Text")]
        [Localizability(LocalizationCategory.Font)]
        public FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        [Category("Brush")]
        public Brush NextMonthForeground
        {
            get { return (Brush)GetValue(NextMonthForegroundProperty); }
            set { SetValue(NextMonthForegroundProperty, value); }
        }

        [Category("Brush")]
        public Brush NextMonthBackground
        {
            get { return (Brush)GetValue(NextMonthBackgroundProperty); }
            set { SetValue(NextMonthBackgroundProperty, value); }
        }

        [Category("Brush")]
        public Brush PreviousMonthBackground
        {
            get { return (Brush)GetValue(PreviousMonthBackgroundProperty); }
            set { SetValue(PreviousMonthBackgroundProperty, value); }
        }

        [Category("Brush")]
        public Brush PreviousMonthForeground
        {
            get { return (Brush)GetValue(PreviousMonthForegroundProperty); }
            set { SetValue(PreviousMonthForegroundProperty, value); }
        }

        [Category("Common")]
        public DateTime DateTime
        {
            get { return (DateTime)GetValue(DateTimeProperty); }
            set { SetValue(DateTimeProperty, value); }
        }

        [Category("Brush")]
        public Brush CurrentDayForeground
        {
            get { return (Brush)GetValue(CurrentDayForegroundProperty); }
            set { SetValue(CurrentDayForegroundProperty, value); }
        }

        [Category("Brush")]
        public Brush CurrentDayBackground
        {
            get { return (Brush)GetValue(CurrentDayBackgroundProperty); }
            set { SetValue(CurrentDayBackgroundProperty, value); }
        }
    }
}

