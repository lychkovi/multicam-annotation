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
        private List<Panel> viewPanels;  // массив панелей отображения видов
        private List<UserCanvasControl> viewCanvases;
        // Массив элементов управления WPF для отображения видов

        private void canvasCallbackFcn(
            int getControlID, DisplayEventID eventID, System.Drawing.Rectangle bounds)
        {
            string messageText =
                "X = " + bounds.X.ToString() +
                "; Y = " + bounds.Y.ToString() +
                "; W = " + bounds.Width.ToString() +
                "; H = " + bounds.Height.ToString();
            MessageBox.Show(messageText,
                "Canvas " + getControlID.ToString() + " raised an event!");
        }

        public DisplayControl()
        {
            InitializeComponent();

            // Заполняем массив панелей отображения видов
            viewPanels = new List<Panel>();
            viewPanels.Add(panel1);
            viewPanels.Add(panel2);
            viewPanels.Add(panel3);
            viewPanels.Add(panel4);

            // Заполняем массив элементов WPF отображения видов
            viewCanvases = new List<UserCanvasControl>();
            int icontrol = 0;
            foreach (Panel panel in viewPanels)
            {
                ElementHost ctrlHost = new ElementHost();
                ctrlHost.Dock = DockStyle.Fill;
                panel.Controls.Add(ctrlHost);
                DisplayControlWpf.UserCanvasControl wpfControl =
                    new DisplayControlWpf.UserCanvasControl();
                wpfControl.InitializeComponent();
                ctrlHost.Child = wpfControl;
                wpfControl.setEventCallback(icontrol, canvasCallbackFcn);
                viewCanvases.Add(wpfControl);
                icontrol++;
            }
        }

        // Метод возвращает количество полей для вывода изображений
        public int GetViewersCount()
        {
            return viewCanvases.Count;
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
            viewCanvases[nviewer].SetListOfViews(viewCaptions, currViewIndex);
        }

        // Метод задаёт содержание выпадающего списка для выбора конкретного 
        // зума при визуализации на отдельном поле вывода изображения
        public void SetViewerListOfZooms(
            int nviewer, List<string> zoomCaptions, int currZoomIndex)
        {
            viewCanvases[nviewer].SetListOfZooms(zoomCaptions, currZoomIndex);
        }

        // Метод задаёт изображение для отдельного поля вывода
        public void SetViewerImage(int nviewer, System.Drawing.Image newImage)
        {
            viewCanvases[nviewer].SetImage(newImage);
        }

        // Метод задаём режим взаимодействия с мышкой для всех полей вывода
        // изображения, а также рамку выделения (при необходимости)
        public void SetViewersMode(
            CanvasModeID mode, 
            System.Drawing.Rectangle clip = new System.Drawing.Rectangle())
        {
            for (int i = 0; i < viewCanvases.Count; i++)
                viewCanvases[i].SetMode(mode, clip);
        }
    }
}
