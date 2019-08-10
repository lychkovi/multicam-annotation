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
    public enum DisplayCanvasModeID
    {
        Disabled,        // изображение отсутствует
        Passive,         // события мыши не обрабатываются
        FocusPoint,      // ожидаем ввод координат интересующего объекта
        BoxCreate,       // ожидаем ввод координат рамки
        BoxUpdate,       // ожидаем изменение координат рамки
        MarkerCreate,    // ожидаем ввод координат маркера
        MarkerUpdate     // ожидаем изменение координат маркера
    }

    // Перечисление задает типы событий, которые может вырабатывать
    // пользовательский элемент управления при манипуляциях с 
    // полем ввода и редактирования разметки кадра
    public enum DisplayCanvasEventID
    {
        NodeCreated,     // создали новую рамку или маркер
        NodeUpdated,     // изменили рамку или маркер
        FocusPointed         // указали точку интереса (для выбора др. объекта)
    }

    // Параметры события при манипуляциях с полем ввода и редактирования 
    // разметки кадра
    public class DisplayCanvasEventArgs
    {
        public readonly DisplayCanvasEventID EventID;
        public int ControlID; // идентификатор элемента управления
        public int ViewID;    // идентификатор вида (задается DisplayManager)
        public System.Drawing.Rectangle clip;
        public bool HasBox;   // true для Box, false для Marker

        public DisplayCanvasEventArgs(DisplayCanvasEventID eventID)
        {
            EventID = eventID;
            ControlID = -1;
            ViewID = -1;
            clip = new System.Drawing.Rectangle();
            HasBox = false;
        }
    }

    // Перечисление задает типы событий, которые может вырабатывать
    // пользовательский элемент управления при изменении состояния
    // выпадающих списков
    public enum DisplayListEventID
    {
        ZoomChanged,        // изменение режима зуммирования
        ViewChanged         // изменение вида для отображения
    }

    // Параметры события выпадающего списка
    public class DisplayListEventArgs
    {
        public readonly DisplayListEventID EventID;
        public int ControlID;   // идентификатор элемента управления
        public int ListItemID;  // индекс элемента выпадающего списка

        public DisplayListEventArgs(DisplayListEventID eventID)
        {
            EventID = eventID;
            ControlID = -1;
            ListItemID = -1;
        }
    }

    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserCanvasControl : UserControl
    {
        private SelectionCanvas canvas;
        private int m_controlID;     // идентификатор элемента в массиве

        // Обработка событий от элемента управления
        public delegate void canvasEventHandler(
            object sender, DisplayCanvasEventArgs args);
        public event canvasEventHandler RunCanvasEvent;

        public delegate void listEventHandler(
            object sender, DisplayListEventArgs args);
        public event listEventHandler RunListEvent;

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
        /// <summary>
        /// Raised by the <span class="code-SummaryComment"><see cref="selectionCanvas">selectionCanvas</see></span>
        /// when the new crop shape (rectangle) has been drawn. This event
        /// then replaces the current selectionCanvas with a
        //        <see cref="DragCanvas">DragCanvas</see>
        /// which can then be used to drag the crop area around
        /// within a Canvas
        /// </summary>
        private void OnCanvasEvent(object sender, DisplayCanvasEventArgs args)
        {
            // Вызываем внешний обработчик события
            args.ControlID = m_controlID;
            RunCanvasEvent(this, args);
        }

        public UserCanvasControl(int controlID)
        {
            InitializeComponent();
            m_controlID = controlID;
            createSelectionCanvas();
            canvas.RunEvent +=
                new SelectionCanvas.CanvasEventHandler(OnCanvasEvent);
            SetMode(DisplayCanvasModeID.Disabled);
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
        public void SetMode(DisplayCanvasModeID mode,
            System.Drawing.Rectangle clip = new System.Drawing.Rectangle())
        {
            if (mode == DisplayCanvasModeID.Disabled)
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
                DisplayListEventArgs args = new DisplayListEventArgs(
                    DisplayListEventID.ViewChanged);
                args.ControlID = m_controlID;
                args.ListItemID = cmbView.SelectedIndex;
                RunListEvent(this, args);
            }
        }

        private void cmbZoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Изменился текущий выбранный зум для отображения
            if (cmbView.SelectedIndex != -1 && cmbZoom.SelectedIndex != -1)
            {
                DisplayListEventArgs args = new DisplayListEventArgs(
                    DisplayListEventID.ZoomChanged);
                args.ControlID = m_controlID;
                args.ListItemID = cmbZoom.SelectedIndex;
                RunListEvent(this, args);
            }
        }
    }
}
