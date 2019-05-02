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
        private SelectionCanvas canvas;

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
            canvas = new SelectionCanvas();

            // Вариант 1. Если нужно отмасштабированное изображение 
            // (но без контроля выхода выеления за пределы изображения):
            //vbForImg.Child = selectCanvForImg;
            //vbForImg.Stretch = Stretch.Uniform;

            // Вариант 2. Если нужно изображение в оригинальных размерах 
            // (с прокруткой)
            svForImg.Content = canvas;

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
        private void OnCanvasEvent(object sender, CanvasEventArgs e)
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
            canvas.RunEvent +=
                new SelectionCanvas.CanvasEventHandler(OnCanvasEvent);
            SetMode(CanvasModeID.Disabled);
        }

        // Метод возвращает фактические размеры поля вывода изображения на форме
        public void GetClientSize(out int width, out int height)
        {
            width = 0;
            height = 0;
        }

        // Метод задаёт содержание выпадающего списка для выбора конкретного 
        // вида для визуализации на отдельном поле вывода изображения, а также
        // индекс активного элемента списка
        public void SetListOfViews(List<string> viewCaptions, int currViewIndex)
        {
            cmbView.Items.Clear();
            for (int i = 0; i < viewCaptions.Count; i++)
                cmbView.Items.Add(viewCaptions[i]);
            cmbView.SelectedIndex = currViewIndex;
        }

        // Метод задаёт содержание выпадающего списка для выбора конкретного 
        // зума при визуализации на отдельном поле вывода изображения
        public void SetListOfZooms(List<string> zoomCaptions, int currZoomIndex)
        {
            cmbZoom.Items.Clear();
            for (int i = 0; i < zoomCaptions.Count; i++)
                cmbZoom.Items.Add(zoomCaptions[i]);
            cmbZoom.SelectedIndex = currZoomIndex;
        }

        // Метод задаёт изображение для отдельного поля вывода
        public void SetImage(System.Drawing.Image newImage)
        {
            canvas.SetImage(newImage);
        }

        // Метод задаём режим взаимодействия с мышкой для всех полей вывода
        // изображения, а также рамку выделения (при необходимости)
        public void SetMode(CanvasModeID mode,
            System.Drawing.Rectangle clip = new System.Drawing.Rectangle())
        {
            if (mode == CanvasModeID.Disabled)
                this.IsEnabled = false;
            else
                this.IsEnabled = true;
            canvas.SetMode(mode, clip);
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
