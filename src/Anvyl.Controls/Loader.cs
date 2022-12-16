using System.ComponentModel;
using System.Windows.Media.Animation;
using Anvyl.Controls.Models.Loader;

namespace Anvyl.Controls
{
    public partial class Loader : FrameworkElement
    {
        static Loader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Loader), new FrameworkPropertyMetadata(typeof(Loader)));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var ticks = CalculateTicks();
            var alpha = TickDirection == SweepDirection.Clockwise ? 0.1d : 1d;

            var alphaChange = 1 / (double)TickCount;

            Point center = new Point(RenderSize.Width / 2, RenderSize.Height / 2);

            for (int i = 0; i < ticks.Length; i++)
            {
                var brush = new SolidColorBrush(Foreground)
                {
                    Opacity = alpha
                };

                var pen = new Pen(brush, TickWidth)
                {
                    StartLineCap = TickStartStyle,
                    EndLineCap = TickEndStyle,
                };

                var tick = ticks[i];

                var p1 = CreateTickPointAnimation(tick.Start, center);
                var p2 = CreateTickPointAnimation(tick.End, center);

                drawingContext.DrawLine(pen, tick.Start, p1.CreateClock(), tick.End, p2.CreateClock());

                if (TickDirection == SweepDirection.Clockwise)
                    alpha += alphaChange;
                else
                    alpha -= alphaChange;
            }
        }

        private PointAnimationBase CreateTickPointAnimation(Point refPoint, Point center)
        {
            var pointAnim = new PointAnimationUsingKeyFrames
            {
                Duration = TimeSpan.FromMilliseconds(TickCount * 60),
                RepeatBehavior = RepeatBehavior.Forever,
            };

            var step = 360 / (double)TickCount;

            for (double i = step; i < 361d; i += step)
            {
                var stepIncrement = i;

                if (TickDirection == SweepDirection.Counterclockwise)
                    stepIncrement = -i;

                PointKeyFrame keyframe = new DiscretePointKeyFrame
                {
                    Value = refPoint.Rotate(stepIncrement, center),
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
