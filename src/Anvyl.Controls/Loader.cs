using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media.Animation;

namespace Anvyl.Controls
{
    internal struct LoaderTick : IEquatable<LoaderTick>
    {
        public LoaderTick(Point start, Point end, double angle)
        {
            Start = start;
            End = end;
            Angle = angle;
        }

        /// <summary>
        /// Start point of this tick
        /// </summary>
        public Point Start { get; private set; }

        /// <summary>
        /// End point of this tick
        /// </summary>
        public Point End { get; private set; }

        public double Angle { get; private set; }

        public override bool Equals(object? obj)
        {
            return obj is LoaderTick tick && Equals(tick);
        }

        public bool Equals(LoaderTick other)
        {
            return EqualityComparer<Point>.Default.Equals(Start, other.Start) &&
                   EqualityComparer<Point>.Default.Equals(End, other.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        public static bool operator ==(LoaderTick left, LoaderTick right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LoaderTick left, LoaderTick right)
        {
            return !(left == right);
        }
    }

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

        protected override void OnRender(DrawingContext drawingContext)
        {
            var ticks = CalculateTicks();
            var alpha = 0.1d;
            var alphaChange = (1 / (double)TickCount);

            Point center = new Point(RenderSize.Width / 2, RenderSize.Height / 2);
           
            for (int i = 0; i < ticks.Length; i++)
            {
                var brush = new SolidColorBrush(Colors.Black);
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

            var keyFrames = pointAnim.KeyFrames;
            var step = 360 / TickCount;

            for (int i = step; i < 361; i += step)
            {
                PointKeyFrame keyframe = new DiscretePointKeyFrame
                {
                    Value = refPoint.Rotate(i, center),
                };

                keyFrames.Add(keyframe);
            }

            return pointAnim;
        }

        private LoaderTick[] CalculateTicks()
        {
            var ticks = new LoaderTick[TickCount];

            var angleIncrement = (360d / TickCount);

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
                start.Offset(RenderSize.Width / 2, RenderSize.Height / 2);

                var end = new Point(outerRadius * cos, outerRadius * sin);
                end.Offset(RenderSize.Width / 2, RenderSize.Height / 2);

                ticks[i] = new LoaderTick(start, end, angle);
                
                angle += angleIncrement;
            }

            return ticks;
        }
    }
}
