using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Anvyl.Controls
{
    public class CircleProgressBar : Control
    {
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(CircleProgressBar), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));



        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(CircleProgressBar), new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsRender));

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Minimum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(CircleProgressBar), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));

        static CircleProgressBar()
        {
            BackgroundProperty.OverrideMetadata(typeof(CircleProgressBar), new FrameworkPropertyMetadata(Brushes.DarkGray, FrameworkPropertyMetadataOptions.AffectsRender));
            ForegroundProperty.OverrideMetadata(typeof(CircleProgressBar), new FrameworkPropertyMetadata(Brushes.AliceBlue, FrameworkPropertyMetadataOptions.AffectsRender));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleProgressBar), new FrameworkPropertyMetadata(typeof(CircleProgressBar)));
        }

        private Size ComputeEllipseSize()
        {
            var width = Math.Max(RenderSize.Width / 2, 0);
            var height = Math.Max(RenderSize.Height / 2, 0);

            return new Size(width, height);
        }

        private double ComputeNormalizedRange()
        {
            var range = Maximum - Minimum;
            var delta = Value - Minimum;
            var output = range == 0.0 ? 0.0 : delta / range;
            output = Math.Min(Math.Max(0.0, output), 0.9999);
            return output;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var size = ComputeEllipseSize();
            var range = ComputeNormalizedRange();

            drawingContext.DrawEllipse(null, new Pen(Background, 5), new Point(size.Width, size.Height), size.Width, size.Height);
            DrawProgress(drawingContext, size, range);
        }

        private void DrawProgress(DrawingContext drawingContext, Size size, double range)
        {
            if (range == 0.9999)
            {
                drawingContext.DrawEllipse(null, new Pen(Foreground, 5), new Point(size.Width, size.Height), size.Width, size.Height);
                DrawPercentageText(drawingContext, size, 1);
                return;
            }

            var angle = 2 * Math.PI * range;
            double x = (Math.Sin(angle) * size.Width) + size.Width;
            double y = ((Math.Cos(angle) * size.Height) - size.Height) * -1;

            PathGeometry geo = new PathGeometry
            {
                Figures =
                {
                    new PathFigure
                    {
                        IsClosed = false,
                        StartPoint = new Point(size.Width, 0),
                        Segments =
                        {
                            new ArcSegment(new Point(x,y), size, 0, angle >= Math.PI , SweepDirection.Clockwise, true)
                        }
                    }
                }
            };

            drawingContext.DrawGeometry(null, new Pen(Foreground, 5), geo);

            DrawPercentageText(drawingContext, size, range);
        }

        private void DrawPercentageText(DrawingContext drawingContext, Size size, double range)
        {
            Typeface tf = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);

            var dpi = VisualTreeHelper.GetDpi(this);
            var text = new FormattedText(string.Format("{0} %", range * 100), CultureInfo.CurrentCulture, FlowDirection, tf, FontSize, Foreground, dpi.PixelsPerDip);

            drawingContext.DrawText(text, new Point(size.Width - (text.Width / 2), size.Height - (text.Height / 2)));
        }
    }
}
