using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DisplayWpf
{
    /// <span class="code-SummaryComment"><summary></span>
    /// Provides a Canvas where a rectangle will be drawn
    /// that matches the selection area that the user drew
    /// on the canvas using the mouse
    /// <span class="code-SummaryComment"></summary></span>
    public partial class SelectionCanvas : Canvas
    {
        #region Instance fields
        private Point mouseLeftDownPoint;
        private Style cropperStyle;
        public Shape rubberBand = null;
        public readonly RoutedEvent CropImageEvent;
        #endregion

        #region Events
        /// <span class="code-SummaryComment"><summary></span>
        /// Raised when the user has drawn a selection area
        /// <span class="code-SummaryComment"></summary></span>
        public event RoutedEventHandler CropImage
        {
            add { AddHandler(this.CropImageEvent, value); }
            remove { RemoveHandler(this.CropImageEvent, value); }
        }
        #endregion

        #region Ctor
        /// <span class="code-SummaryComment"><summary></span>
        /// Constructs a new SelectionCanvas, and registers the
        /// CropImage event
        /// <span class="code-SummaryComment"></summary></span>
        public SelectionCanvas()
        {
            this.CropImageEvent = EventManager.RegisterRoutedEvent
        ("CropImage", RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(SelectionCanvas));
        }
        #endregion

        #region Public Properties
        public Style CropperStyle
        {
            get { return cropperStyle; }
            set { cropperStyle = value; }
        }
        #endregion

        #region Overrides

        /// <span class="code-SummaryComment"><summary></span>
        /// Captures the mouse
        /// <span class="code-SummaryComment"></summary></span>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (!this.IsMouseCaptured)
            {
                mouseLeftDownPoint = e.GetPosition(this);
                this.CaptureMouse();
            }
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Releases the mouse, and raises the CropImage Event
        /// <span class="code-SummaryComment"></summary></span>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (this.IsMouseCaptured && rubberBand != null)
            {
                this.ReleaseMouseCapture();

                RaiseEvent(new RoutedEventArgs(this.CropImageEvent, this));
            }
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Creates a child control
        /// <span class="code-SummaryComment"><see cref="System.Windows.Shapes.Rectangle">Rectangle</see></span>
        /// and adds it to this controls children collection
        /// at the co-ordinates the user
        /// drew with the mouse
        /// <span class="code-SummaryComment"></summary></span>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.IsMouseCaptured)
            {
                Point currentPoint = e.GetPosition(this);

                if (rubberBand == null)
                {
                    rubberBand = new Rectangle();
                    //if (cropperStyle != null)
                    //    rubberBand.Style = cropperStyle;
                    //rubberBand.Style = new Style(typeof(Shape)); //, Style.BasedOn); // Style.BasedOn;// DefaultStyleKey;
                    //rubberBand.Style.
                    rubberBand.Stroke = new SolidColorBrush(Colors.Black);
                    rubberBand.StrokeThickness = 2;
                    //rubberBand.Fill = new SolidColorBrush(Colors.Black);
                    this.Children.Add(rubberBand);
                }

                double width = Math.Abs(mouseLeftDownPoint.X - currentPoint.X);
                double height =
            Math.Abs(mouseLeftDownPoint.Y - currentPoint.Y);
                double left = Math.Min(mouseLeftDownPoint.X, currentPoint.X);
                double top = Math.Min(mouseLeftDownPoint.Y, currentPoint.Y);

                rubberBand.Width = width;
                rubberBand.Height = height;
                Canvas.SetLeft(rubberBand, left);
                Canvas.SetTop(rubberBand, top);
                rubberBand.BringIntoView();
            }
        }
        #endregion
    }
}
