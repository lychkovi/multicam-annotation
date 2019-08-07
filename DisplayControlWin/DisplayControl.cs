using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Windows.Shapes;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Xaml;

using DisplayControlWpf;    // элемент управления для отображения вида

namespace DisplayControlWin
{
    public partial class DisplayControl : UserControl
    {
        // Mассив панелей отображения видов
        private List<Panel> viewerPanels;  

        // Массив элементов управления WPF для отображения видов
        private List<UserCanvasControl> viewerCanvases;

        // Обработчики событий от отдельных полей вывода
        public event UserCanvasControl.canvasEventHandler RunCanvasEvent;
        public event UserCanvasControl.listEventHandler RunListEvent;
        private void OnViewerCanvasEvent(object sender, DisplayCanvasEventArgs e)
        {
            RunCanvasEvent(sender, e);
        }
        private void OnViewerListEvent(object sender, DisplayListEventArgs e)
        {
            RunListEvent(sender, e);
        }

        public DisplayControl()
        {
            InitializeComponent();

            // Заполняем массив панелей отображения видов
            viewerPanels = new List<Panel>();
            viewerPanels.Add(panel1);
            viewerPanels.Add(panel2);
            viewerPanels.Add(panel3);
            viewerPanels.Add(panel4);

            // Заполняем массив элементов WPF отображения видов
            viewerCanvases = new List<UserCanvasControl>();
            int icontrol = 0;
            foreach (Panel panel in viewerPanels)
            {
                ElementHost ctrlHost = new ElementHost();
                ctrlHost.Dock = DockStyle.Fill;
                panel.Controls.Add(ctrlHost);
                DisplayControlWpf.UserCanvasControl wpfControl =
                    new DisplayControlWpf.UserCanvasControl(icontrol);
                wpfControl.InitializeComponent();
                ctrlHost.Child = wpfControl;
                wpfControl.RunCanvasEvent += new
                    UserCanvasControl.canvasEventHandler(OnViewerCanvasEvent);
                wpfControl.RunListEvent += new
                    UserCanvasControl.listEventHandler(OnViewerListEvent);
                viewerCanvases.Add(wpfControl);
                icontrol++;
            }
        }

        // Метод возвращает количество полей для вывода изображений
        public int GetViewersCount()
        {
            return viewerCanvases.Count;
        }

        // Метод возвращает фактические размеры поля вывода изображения на форме
        public void GetViewerClientSize(int nviewer, out int width, out int height)
        {
            width = 0;
            height = 0;
        }

        // Метод задаёт содержание выпадающего списка для выбора конкретного 
        // вида для визуализации на отдельном поле вывода изображения, а также
        // индекс активного элемента списка
        public void SetViewerListOfViews(
            int nviewer, List<string> viewCaptions, int currViewIndex)
        {
            viewerCanvases[nviewer].SetListOfViews(viewCaptions, currViewIndex);
        }

        // Метод задаёт содержание выпадающего списка для выбора конкретного 
        // зума при визуализации на отдельном поле вывода изображения
        public void SetViewerListOfZooms(
            int nviewer, List<string> zoomCaptions, int currZoomIndex)
        {
            viewerCanvases[nviewer].SetListOfZooms(zoomCaptions, currZoomIndex);
        }

        // Метод задаёт изображение для отдельного поля вывода
        public void SetViewerImage(int nviewer, System.Drawing.Image newImage)
        {
            viewerCanvases[nviewer].SetImage(newImage);
        }

        // Метод удаляет изображение из отдельного поля вывода и переводит его
        // в неактивный режим
        public void DelViewerImage(int nviewer)
        {
            viewerCanvases[nviewer].DelImage();
        }

        // Метод задаём режим взаимодействия с мышкой для одного поля вывода
        // изображения, а также рамку выделения (при необходимости)
        public void SetViewerMode(
            int nviewer, 
            DisplayCanvasModeID mode,
            System.Drawing.Rectangle clip = new System.Drawing.Rectangle())
        {
            viewerCanvases[nviewer].SetMode(mode, clip);
        }

        // Метод задаём режим взаимодействия с мышкой для всех полей вывода
        // изображения, а также рамку выделения (при необходимости)
        public void SetViewersMode(
            DisplayCanvasModeID mode, 
            System.Drawing.Rectangle clip = new System.Drawing.Rectangle())
        {
            for (int i = 0; i < viewerCanvases.Count; i++)
                viewerCanvases[i].SetMode(mode, clip);
        }
    }
}
