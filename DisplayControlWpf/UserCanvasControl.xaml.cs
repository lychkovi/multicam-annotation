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
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserCanvasControl : UserControl
    {
        private SelectionCanvas canvas;

        // Обработка события от элемента управления
        private int m_controlID;     // идентификатор элемента в массиве
        public delegate void controlEventHandler(
            object sender, DisplayControlEventArgs args);
        public event controlEventHandler RunEvent;

        /// <span class="code-SummaryComment"><summary></span>
        /// creates the selection canvas, where user can draw
        /// selection rectangle
        /// <span class="code-SummaryComment"></summary></span>
        public void createSelectionCanvas()
        {
            canvas = new SelectionCanvas();

            // Вариант 1. Если нужно отмасштабированное изображение 
            // (но без контроля выхода выделения за пределы изображения),
            // используем ViewBox:
            //vbForImg.Child = selectCanvForImg;
            //vbForImg.Stretch = Stretch.Uniform;

            // Вариант 2. Если нужно изображение в оригинальных размерах 
            // (с прокруткой), используем ScrollViewer:
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
            // Вызываем внешний обработчик события
            DisplayControlEventID eventID;
            switch (e.msg)
            {
                case CanvasEventID.PointLost:
                default:
                    eventID = DisplayControlEventID.PointLost;
                    break;
                case CanvasEventID.PointSelected:
                    eventID = DisplayControlEventID.PointSelected;
                    break;
                case CanvasEventID.RubberCreated:
                    eventID = DisplayControlEventID.RubberCreated;
                    break;
                case CanvasEventID.RubberUpdated:
                    eventID = DisplayControlEventID.RubberUpdated;
                    break;
            }
            DisplayControlEventArgs args = new DisplayControlEventArgs(eventID);
            args.controlID = m_controlID;
            args.clip = e.clip;
            RunEvent(this, args);
        }

        public UserCanvasControl(int controlID)
        {
            InitializeComponent();
            m_controlID = controlID;
            createSelectionCanvas();
            canvas.RunEvent +=
                new SelectionCanvas.CanvasEventHandler(OnCanvasEvent);
            SetMode(CanvasModeID.Disabled);
        }

        // Метод возвращает фактические размеры поля вывода изображения на форме
        public void GetClientSize(out int width, out int height)
        {
            canvas.GetClientSize(out width, out height);
        }

        // Метод задаёт содержание выпадающего списка для выбора конкретного 
        // вида для визуализации на отдельном поле вывода изображения, а также
        // индекс активного элемента списка
        public void SetListOfViews(List<string> viewCaptions, int currViewIndex)
        {
            cmbView.SelectionChanged -= cmbView_SelectionChanged;
            cmbView.Items.Clear();
            for (int i = 0; i < viewCaptions.Count; i++)
                cmbView.Items.Add(viewCaptions[i]);
            cmbView.SelectedIndex = currViewIndex;
            cmbView.SelectionChanged += cmbView_SelectionChanged;
        }

        // Метод задаёт содержание выпадающего списка для выбора конкретного 
        // зума при визуализации на отдельном поле вывода изображения
        public void SetListOfZooms(List<string> zoomCaptions, int currZoomIndex)
        {
            cmbZoom.SelectionChanged -= cmbZoom_SelectionChanged;
            cmbZoom.Items.Clear();
            for (int i = 0; i < zoomCaptions.Count; i++)
                cmbZoom.Items.Add(zoomCaptions[i]);
            cmbZoom.SelectedIndex = currZoomIndex;
            cmbZoom.SelectionChanged += cmbZoom_SelectionChanged;
        }

        // Метод задаёт изображение для отдельного поля вывода
        public void SetImage(System.Drawing.Image newImage)
        {
            canvas.SetImage(newImage);
            this.IsEnabled = true;
        }

        // Метод удаляет изображение из поля вывода и переводит его
        // в неактивный режим
        public void DelImage()
        {
            canvas.DelImage();
            this.IsEnabled = false;
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

        private void cmbView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Изменился текущий выбранный для отображения вид
            if (cmbView.SelectedIndex != -1 && cmbZoom.SelectedIndex != -1)
            {
                DisplayControlEventArgs args = new DisplayControlEventArgs(
                    DisplayControlEventID.ViewChanged);
                args.controlID = m_controlID;
                args.cmbItemID = cmbView.SelectedIndex;
                RunEvent(this, args);
            }
        }

        private void cmbZoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Изменился текущий выбранный зум для отображения
            if (cmbView.SelectedIndex != -1 && cmbZoom.SelectedIndex != -1)
            {
                DisplayControlEventArgs args = new DisplayControlEventArgs(
                    DisplayControlEventID.ZoomChanged);
                args.controlID = m_controlID;
                args.cmbItemID = cmbZoom.SelectedIndex;
                RunEvent(this, args);
            }
        }
    }

    // Перечисление задает типы событий, которые может вырабатывать
    // пользовательский элемент управления при изменении состояния
    // выпадающих списков
    public enum DisplayControlEventID
    {
        PointSelected,      // указали точку
        RubberCreated,      // создали новую рамку
        RubberUpdated,      // изменили рамку
        PointLost,          // за пределы рамки нажали
        ZoomChanged,        // изменение режима зуммирования
        ViewChanged         // изменение вида для отображения
    }

    // Параметры события выпадающего списка
    public class DisplayControlEventArgs
    {
        public readonly DisplayControlEventID msg;
        public int controlID;  // идентификатор элемента управления
        public System.Drawing.Rectangle clip;
        public int cmbItemID;  // индекс элемента выпадающего списка

        public DisplayControlEventArgs(DisplayControlEventID message)
        {
            msg = message;
            controlID = -1;
            clip = new System.Drawing.Rectangle();
            cmbItemID = -1;
        }
    }
}
