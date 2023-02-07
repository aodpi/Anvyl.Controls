using System.ComponentModel;

namespace Anvyl.Controls
{
	public partial class Loader
    {
		public static readonly DependencyProperty TickCountProperty =
			DependencyProperty.Register(nameof(TickCount), typeof(int), typeof(Loader), new FrameworkPropertyMetadata(12, FrameworkPropertyMetadataOptions.AffectsRender));

		public static readonly DependencyProperty ForegroundProperty =
			DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(Loader), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

		public static readonly DependencyProperty TickStartStyleProperty =
			DependencyProperty.Register(nameof(TickStartStyle), typeof(PenLineCap), typeof(Loader), new FrameworkPropertyMetadata(PenLineCap.Square, FrameworkPropertyMetadataOptions.AffectsRender));

		public static readonly DependencyProperty TickEndStyleProperty =
			DependencyProperty.Register(nameof(TickEndStyle), typeof(PenLineCap), typeof(Loader), new FrameworkPropertyMetadata(PenLineCap.Square, FrameworkPropertyMetadataOptions.AffectsRender));

		public static readonly DependencyProperty TickDirectionProperty =
			DependencyProperty.Register(nameof(TickDirection), typeof(SweepDirection), typeof(Loader), new FrameworkPropertyMetadata(SweepDirection.Clockwise, FrameworkPropertyMetadataOptions.AffectsRender));

		public static readonly DependencyProperty TickWidthProperty =
			DependencyProperty.Register(nameof(TickWidth), typeof(double), typeof(Loader), new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsRender));


		/// <summary>
		/// Width of each tick (effectively it's thickness)
		/// </summary>
		[Category("Common")]
		public double TickWidth
		{
			get { return (double)GetValue(TickWidthProperty); }
			set { SetValue(TickWidthProperty, value); }
		}

		/// <summary>
		/// Foreground color of the ticks
		/// </summary>
		[Category("Brush")]
		public Brush Foreground
		{
			get { return (Brush)GetValue(ForegroundProperty); }
			set { SetValue(ForegroundProperty, value); }
		}

		/// <summary>
		/// Total number of ticks.
		/// </summary>
		/// <remarks>It should be a number between 0 and 200</remarks>
		[Category("Common")]
		public int TickCount
		{
			get { return (int)GetValue(TickCountProperty); }
			set { SetValue(TickCountProperty, value); }
		}

		/// <summary>
		/// Style of the beggining of the ticks (from center)
		/// </summary>
		[Category("Common")]
		public PenLineCap TickEndStyle
		{
			get { return (PenLineCap)GetValue(TickEndStyleProperty); }
			set { SetValue(TickEndStyleProperty, value); }
		}

		/// <summary>
		/// Style of the ticks at the end of the ticks (from center).
		/// </summary>
		[Category("Common")]
		public PenLineCap TickStartStyle
		{
			get { return (PenLineCap)GetValue(TickStartStyleProperty); }
			set { SetValue(TickStartStyleProperty, value); }
		}

		/// <summary>
		/// Rotation direction of the ticks.
		/// </summary>
		[Category("Common")]
		public SweepDirection TickDirection
		{
			get { return (SweepDirection)GetValue(TickDirectionProperty); }
			set { SetValue(TickDirectionProperty, value); }
		}
	}
}
