using System.Windows.Media.Animation;
using Anvyl.Controls.Models.Loader;

namespace Anvyl.Controls
{
    public class Loader : FrameworkElement
    {
        public int TickCount
        {
            get { return (int)GetValue(TickCountProperty); }
            set { SetValue(TickCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TickCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TickCountProperty =
            DependencyProperty.Register(nameof(TickCount), typeof(int), typeof(Loader), new PropertyMetadata(12));



        public Color Foreground
        {
            get { return (Color)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Foreground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register(nameof(Foreground), typeof(Color), typeof(Loader), new PropertyMetadata(Colors.Black));


        protected override void OnRender(DrawingContext drawingContext)
        {
            var ticks = CalculateTicks();
            var alpha = 0.1d;
            var alphaChange = 1 / (double)TickCount;

            Point center = new Point(RenderSize.Width / 2, RenderSize.Height / 2);

            for (int i = 0; i < ticks.Length; i++)
            {
                var brush = new SolidColorBrush(Foreground);
                brush.Opacity = alpha;

                var pen = new Pen(brush, 3)
                {
                    EndLineCap = PenLineCap.Round,
                    StartLineCap = PenLineCap.Round
                };

                var tick = ticks[i];

                var p1 = CreatePointAnimation(tick.Start, center);
                var p2 = CreatePointAnimation(tick.End, center);

                drawingContext.DrawLine(pen, tick.Start, p1.CreateClock(), tick.End, p2.CreateClock());

                alpha += alphaChange;
            }
        }

        private PointAnimationBase CreatePointAnimation(Point refPoint, Point center)
        {
            var pointAnim = new PointAnimationUsingKeyFrames
            {
                Duration = TimeSpan.FromMilliseconds(TickCount * 60),
                RepeatBehavior = RepeatBehavior.Forever,
            };

            var step = 360 / (double)TickCount;

            for (double i = step; i < 361d; i += step)
            {
                PointKeyFrame keyframe = new DiscretePointKeyFrame
                {
                    Value = refPoint.Rotate(i, center),
                };

                pointAnim.KeyFrames.Add(keyframe);
            }

            return pointAnim;
        }

        private LoaderTick[] CalculateTicks()
        {
            var ticks = new LoaderTick[TickCount];

            var angleIncrement = 360d / TickCount;

            var width = RenderSize.Width < RenderSize.Height ? RenderSize.Width : RenderSize.Height;
            var centerPoint = new Point(RenderSize.Width / 2, RenderSize.Height / 2);

            var innerRadius = (int)(width * 0.175);
            var outerRadius = (int)(width * 0.3125);

            double angle = 0;

            for (int i = 0; i < TickCount; i++)
            {
                var angleInRadians = angle.ToRadians();
                var cos = (float)Math.Cos(angleInRadians);
                var sin = (float)Math.Sin(angleInRadians);

                var start = new Point(innerRadius * cos, innerRadius * sin);
                start.Offset(centerPoint.X, centerPoint.Y);

                var end = new Point(outerRadius * cos, outerRadius * sin);
                end.Offset(centerPoint.X, centerPoint.Y);

                ticks[i] = new LoaderTick(start, end, angle);

                angle += angleIncrement;
            }

            return ticks;
        }
    }
}
