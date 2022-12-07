using System.Net.Http.Headers;
using System.Windows.Media.Animation;

namespace Anvyl.Controls
{
    public class CustomProgressBar : FrameworkElement
    {
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(CustomProgressBar), new PropertyMetadata(default));

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(CustomProgressBar), new PropertyMetadata(default));

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(CustomProgressBar), new PropertyMetadata(default));

        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register(nameof(IsIndeterminate), typeof(bool), typeof(CustomProgressBar), new PropertyMetadata(false));

        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(CustomProgressBar), new PropertyMetadata(Brushes.DarkGray));

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(CustomProgressBar), new PropertyMetadata(Brushes.DarkGreen));




        public Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register(nameof(BorderBrush), typeof(Brush), typeof(CustomProgressBar), new PropertyMetadata(Brushes.Transparent));





        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(CustomProgressBar), new PropertyMetadata(default));


        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BrushThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(nameof(BorderThickness), typeof(Thickness), typeof(CustomProgressBar), new PropertyMetadata(new Thickness()));


        static CustomProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomProgressBar), new FrameworkPropertyMetadata(typeof(CustomProgressBar)));
        }


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var refRect = new Rect(0, 0, RenderSize.Width, RenderSize.Height);

            drawingContext.DrawRoundedRectangle(Background, null, refRect, CornerRadius, CornerRadius);

            DrawBorders(drawingContext, refRect);

            if (IsIndeterminate)
            {
                drawingContext.DrawRoundedRectangle(Foreground, null, new Rect(), GetIndeterminateAnimation().CreateClock(), CornerRadius, null, CornerRadius, null);
                return;
            }

            var width = Math.Clamp(RenderSize.Width * (Value / Maximum), 0, RenderSize.Width);

            drawingContext.DrawRoundedRectangle(Foreground, null, new Rect(0, 0, width, RenderSize.Height), CornerRadius, CornerRadius);
        }

        private void DrawBorders(DrawingContext drawingContext, Rect refRect)
        {
            var borderPen = new Pen(BorderBrush, BorderThickness.Left);

            var leftBPoint1 = new Point(refRect.TopLeft.X + (BorderThickness.Left / 2), refRect.TopLeft.Y);
            var leftBPoint2 = new Point(refRect.BottomLeft.X + (BorderThickness.Left / 2), refRect.BottomLeft.Y);

            drawingContext.DrawLine(borderPen, leftBPoint1, leftBPoint2);

            borderPen = new Pen(BorderBrush, BorderThickness.Top);

            var topBPoint1 = new Point(refRect.TopLeft.X, refRect.TopLeft.Y + (BorderThickness.Top / 2));
            var topBPoint2 = new Point(refRect.TopRight.X, refRect.TopRight.Y + (BorderThickness.Top / 2));

            drawingContext.DrawLine(borderPen, topBPoint1, topBPoint2);

            borderPen = new Pen(BorderBrush, BorderThickness.Right);

            var rightBPoint1 = new Point(refRect.TopRight.X - (BorderThickness.Right / 2), refRect.TopRight.Y);
            var rightBPoint2 = new Point(refRect.BottomRight.X - (BorderThickness.Right / 2), refRect.BottomRight.Y);

            drawingContext.DrawLine(borderPen, rightBPoint1, rightBPoint2);

            borderPen = new Pen(BorderBrush, BorderThickness.Bottom);

            var bottomBPoint1 = new Point(refRect.BottomRight.X, refRect.BottomRight.Y - (BorderThickness.Bottom / 2));
            var bottomBPoint2 = new Point(refRect.BottomLeft.X, refRect.BottomLeft.Y - (BorderThickness.Bottom / 2));

            drawingContext.DrawLine(borderPen, bottomBPoint1, bottomBPoint2);
        }

        private RectAnimationBase GetIndeterminateAnimation()
        {
            return new RectAnimationUsingKeyFrames
            {
                Duration = TimeSpan.FromSeconds(2.5),
                RepeatBehavior = RepeatBehavior.Forever,
                KeyFrames =
                {
                    new EasingRectKeyFrame(new Rect(0,0,0, RenderSize.Height), KeyTime.FromTimeSpan(TimeSpan.Zero)),
                    new EasingRectKeyFrame(new Rect(0,0,RenderSize.Width, RenderSize.Height), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))),
                    new EasingRectKeyFrame(new Rect(RenderSize.Width, 0, 0, RenderSize.Height), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2)))
                }
            };
        }
    }
}
