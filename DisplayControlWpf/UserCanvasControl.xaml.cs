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
    // Перечисление задает все типы событий, которые может вырабатывать
    // пользовательский элемент управления
    public enum EventTypeID
    {
        rubberCreated,
        rubberSelected,
        rubberModified
    }

    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserCanvasControl : UserControl
    {
        private string ImgUrl = "D:\\Igorek\\Disser\\YanaNewNew\\MyVideoAnno\\Debug\\Koala.jpg";
        private BitmapImage bmpSource = null;
        private SelectionCanvas selectCanvForImg = null;
        private System.Windows.Controls.Image img = null;
        private double zoomFactor = 1.0;

        // Обработка события от элемента управления
        private Shape rubberBand = null;
        private int controlID;     // идентификатор элемента в массиве
        public delegate void eventCallback(
            int getControlID, EventTypeID eventID, 
            System.Drawing.Rectangle bounds);
        eventCallback eventCallbackFcn = null;

        // Регистрация обработчика события
        public void setEventCallback(
            int setControlID, eventCallback callbackFcn)
        {
            controlID = setControlID;
            eventCallbackFcn = callbackFcn;
        }
        //    int setControlID, EventTypeID eventID, Shape rubberBand);

        /// <span class="code-SummaryComment"><summary></span>
        /// Creates the Image source for the current canvas
        /// <span class="code-SummaryComment"></summary></span>
        private void createImageSource()
        {
            bmpSource = new BitmapImage(new Uri(ImgUrl));
            img = new System.Windows.Controls.Image();
            img.Source = bmpSource;
            //if there was a Zoom Factor applied
            //img.RenderTransform = new ScaleTransform (0.8, 0.8, 0, 0);
            img.RenderTransform = new ScaleTransform(zoomFactor, zoomFactor, 0, 0);
            //Size imgRenderSize = new Size();
            //imgRenderSize.Width = 100;
            //imgRenderSize.Height = 100;
            //img.RenderSize = imgRenderSize;
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// creates the selection canvas, where user can draw
        /// selection rectangle
        /// <span class="code-SummaryComment"></summary></span>
        public void createSelectionCanvas()
        {
            createImageSource();
            selectCanvForImg = new SelectionCanvas();
            selectCanvForImg.Width = bmpSource.Width* zoomFactor;
            selectCanvForImg.Height = bmpSource.Height* zoomFactor;
            selectCanvForImg.Children.Clear();
            selectCanvForImg.rubberBand = null;
            selectCanvForImg.Children.Add(img);

            // Вариант 1. Если нужно отмасштабированное изображение 
            // (но без контроля выхода выеления за пределы изображения):
            //vbForImg.Child = selectCanvForImg;
            //vbForImg.Stretch = Stretch.Uniform;

            // Вариант 2. Если нужно изображение в оригинальных размерах 
            // (с прокруткой)
            svForImg.Content = selectCanvForImg;

            //createSelectionCanvasMenu();
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Raised by the <span class="code-SummaryComment"><see cref="selectionCanvas">selectionCanvas</see></span>
        /// when the new crop shape (rectangle) has been drawn. This event
        /// then replaces the current selectionCanvas with a
        //        <see cref="DragCanvas">DragCanvas</see>
        /// which can then be used to drag the crop area around
        /// within a Canvas
        /// </summary>
        private void selectCanvForImg_CropImage
                (object sender, RoutedEventArgs e)
        {
            rubberBand = (Shape)selectCanvForImg.Children[1];
            //createDragCanvas();

            // Вызываем внешний обработчик события
            if (eventCallbackFcn != null)
            {
                System.Drawing.Rectangle bounds = new System.Drawing.Rectangle();
                bounds.X = (int) Canvas.GetLeft(rubberBand);
                bounds.Y = (int) Canvas.GetTop(rubberBand);
                bounds.Width = (int)rubberBand.Width;
                bounds.Height = (int)rubberBand.Height;
                eventCallbackFcn(
                    controlID, EventTypeID.rubberCreated, bounds);
            }
        }

        public UserCanvasControl()
        {
            InitializeComponent();

            createSelectionCanvas();
            selectCanvForImg.CropImage +=
                new RoutedEventHandler(selectCanvForImg_CropImage);
        }

        // Метод обновляет размеры дочерних элементов в случае изменения 
        // размеров самого элемента управления NewWidth и NewHeight.
        public void OnResize(double NewWidth, double NewHeight)
        {
            svForImg.Width = NewWidth;
            svForImg.Height = NewHeight - 52.0;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.HeightChanged || e.WidthChanged)
            {
                OnResize(e.NewSize.Width, e.NewSize.Height);
            }
        }
    }
}
