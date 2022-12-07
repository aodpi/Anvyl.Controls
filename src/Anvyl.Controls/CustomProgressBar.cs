namespace Anvyl.Controls
{
    public class CustomProgressBar : FrameworkElement
    {
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
            var width = RenderSize.Width;
            var height = RenderSize.Height;

            drawingContext.DrawRectangle(Brushes.Transparent, new Pen
            {
                Brush = Brushes.Black,
                Thickness = 1
            }, new Rect(0, 0, width, height));
        }
    }
}
