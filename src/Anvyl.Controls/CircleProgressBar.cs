using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register(nameof(StrokeThickness), typeof(double), typeof(CircleProgressBar), new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.AffectsRender));



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

            return new Size(width - StrokeThickness, height - StrokeThickness);
        }

        private double ComputeNormalizedRange()
        {
            var range = Maximum - Minimum;
            var delta = Value - Minimum;
            var output = range == 0.0 ? 0.0 : delta / range;
            output = Math.Min(Math.Max(0.0, output), 0.999999);
            return output;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var size = ComputeEllipseSize();
            var range = ComputeNormalizedRange();

            drawingContext.PushTransform(new TranslateTransform(StrokeThickness, StrokeThickness));

            drawingContext.DrawEllipse(null, new Pen(Background, StrokeThickness), new Point(size.Width, size.Height), size.Width, size.Height);
            DrawProgress(drawingContext, size, range);

            drawingContext.Pop();
        }

        private void DrawProgress(DrawingContext drawingContext, Size size, double range)
        {
            var angle = 360d * range; // angle in degrees based on percentage
            Point startPoint = new Point(size.Width, 0);

            Point p = startPoint.Rotate(angle, new Point(size.Width, size.Height));

            ArcSegment arcSegment = new ArcSegment(p, size, 0, angle >= 180d, SweepDirection.Clockwise, true);

            PathGeometry geo = new PathGeometry
            {
                Figures =
                {
                    new PathFigure
                    {
                        IsClosed = false,
                        StartPoint = startPoint,
                        Segments =
                        {
                            arcSegment
                        },
                    }
                }
            };

            drawingContext.DrawGeometry(null, new Pen(Foreground, StrokeThickness), geo);

            DrawPercentageText(drawingContext, size, range);
        }

        private void DrawPercentageText(DrawingContext drawingContext, Size size, double range)
        {
            Typeface tf = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);

            var dpi = VisualTreeHelper.GetDpi(this);
            var textToFormat = string.Format(CultureInfo.InvariantCulture, "{0:P0}", range);
            var text = new FormattedText(textToFormat, CultureInfo.InvariantCulture, FlowDirection, tf, FontSize, Foreground, dpi.PixelsPerDip);

            Point textPoint = new Point(size.Width - (text.Width / 2), size.Height - (text.Height / 2));

            drawingContext.DrawText(text, textPoint);
        }
    }
}
