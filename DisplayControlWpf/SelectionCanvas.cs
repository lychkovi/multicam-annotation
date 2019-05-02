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

namespace DisplayControlWpf
{
    // Перечень поддерживаемых режимов 
    public enum CanvasModeID
    {
        Idle,               // неактивный режим
        PointSelect,        // ожидаем ввод координат точки
        RubberCreate,       // ожидаем ввод координат рамки
        RubberUpdate        // ожидаем изменение координат рамки
    }

    public enum CanvasEventID
    {
        PointSelected,      // указали точку
        RubberCreated,      // создали новую рамку
        RubberUpdated,      // изменили рамку
        PointLost           // за пределы рамки нажали
    }

    public class CanvasEventArgs : EventArgs
    {
        public readonly CanvasEventID msg;
        public System.Drawing.Rectangle clip;

        public CanvasEventArgs(CanvasEventID message)
        {
            msg = message;
            clip = new System.Drawing.Rectangle();
        }
    }

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
        private string ImgUrl = "D:\\Igorek\\Disser\\YanaNewNew\\MyVideoAnno\\Debug\\Koala.jpg";
        private System.Windows.Media.Imaging.BitmapImage imgSource;
        private System.Windows.Controls.Image img;
        private Shape rubberBand;
        public delegate void CanvasEventHandler(
            object sender, CanvasEventArgs e);
        public event CanvasEventHandler RunEvent;
        #endregion

        #region Events
        /// <span class="code-SummaryComment"><summary></span>
        /// Raised when the user has drawn a selection area
        /// <span class="code-SummaryComment"></summary></span>
        #endregion

        #region Ctor
        /// <span class="code-SummaryComment"><summary></span>
        /// Constructs a new SelectionCanvas, and registers the
        /// CropImage event
        /// <span class="code-SummaryComment"></summary></span>
        public SelectionCanvas()
        {
            // Создаем элемент просмотра изображения
            imgSource = new BitmapImage(new Uri(ImgUrl));
            img = new System.Windows.Controls.Image();
            img.Source = imgSource;
            img.RenderTransform = new ScaleTransform(1.0, 1.0, 0, 0);
            this.Width = imgSource.Width;
            this.Height = imgSource.Height;
            this.Children.Add(img);

            // Создаем элемент прямоугольную рамку
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
                CanvasEventArgs args = 
                    new CanvasEventArgs(CanvasEventID.RubberCreated);
                args.clip.X = (int)Canvas.GetLeft(rubberBand);
                args.clip.Y = (int)Canvas.GetTop(rubberBand);
                args.clip.Width = (int)rubberBand.Width;
                args.clip.Height = (int)rubberBand.Height;
                RunEvent(this, args);
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

                double width = Math.Abs(mouseLeftDownPoint.X - currentPoint.X);
                double height = Math.Abs(mouseLeftDownPoint.Y - currentPoint.Y);
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
