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
    public enum DisplayEventID
    {
        PointSelected,      // указали точку
        RubberCreated,      // создали новую рамку
        RubberUpdated,      // изменили рамку
        PointLost,          // за пределы рамки нажали
        ZoomChanged,        // изменение режима зуммирования
        ViewChanged         // изменение вида для отображения
    }

    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserCanvasControl : UserControl
    {
        private SelectionCanvas selectCanvForImg;

        // Обработка события от элемента управления
        private int controlID;     // идентификатор элемента в массиве
        public delegate void eventCallback(
            int getControlID, DisplayEventID eventID, 
            System.Drawing.Rectangle bounds);
        eventCallback eventCallbackFcn = null;

        // Регистрация обработчика события
        public void setEventCallback(
            int setControlID, eventCallback callbackFcn)
        {
            controlID = setControlID;
            eventCallbackFcn = callbackFcn;
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// creates the selection canvas, where user can draw
        /// selection rectangle
        /// <span class="code-SummaryComment"></summary></span>
        public void createSelectionCanvas()
        {
            selectCanvForImg = new SelectionCanvas();

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
        private void selectCanvForImg_CropImage(object sender, CanvasEventArgs e)
        {
            //rubberBand = (Shape)selectCanvForImg.Children[1];
            //createDragCanvas();

            // Вызываем внешний обработчик события
            if (eventCallbackFcn != null)
            {
                eventCallbackFcn(
                    controlID, DisplayEventID.RubberCreated, e.clip);
            }
        }

        public UserCanvasControl()
        {
            InitializeComponent();


            createSelectionCanvas();
            selectCanvForImg.RunEvent +=
                new SelectionCanvas.CanvasEventHandler(selectCanvForImg_CropImage);
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
