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

			// Rotate transform with animation.
			RotateTransform rotate = GetRotateTransform(center);

			drawingContext.PushTransform(rotate);

            for (int i = 0; i < ticks.Length; i++)
			{
				var brush = Foreground.Clone();

				brush.Opacity = alpha;

				var pen = new Pen(brush, TickWidth)
				{
					StartLineCap = TickStartStyle,
					EndLineCap = TickEndStyle,
				};

				var tick = ticks[i];

				drawingContext.DrawLine(pen, tick.Start, tick.End);

				if (TickDirection == SweepDirection.Clockwise)
					alpha += alphaChange;
				else
					alpha -= alphaChange;
			}
			
			drawingContext.Pop();
		}

		private RotateTransform GetRotateTransform(Point center)
		{
			RotateTransform rotate = new RotateTransform(0, center.X, center.Y);

			DoubleAnimationUsingKeyFrames rotateAnim = new DoubleAnimationUsingKeyFrames
			{
				Duration = TimeSpan.FromMilliseconds(TickCount * 60),
				RepeatBehavior = RepeatBehavior.Forever
			};

			var step = 360d / TickCount;

			for (double i = step; i < 361d; i += step)
			{
				var stepIncrement = i;

				if (TickDirection == SweepDirection.Counterclockwise)
					stepIncrement = -i;

				var keyFrame = new DiscreteDoubleKeyFrame
				{
					Value = stepIncrement,
				};

				rotateAnim.KeyFrames.Add(keyFrame);
			}

			rotate.ApplyAnimationClock(RotateTransform.AngleProperty, rotateAnim.CreateClock());
			return rotate;
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
