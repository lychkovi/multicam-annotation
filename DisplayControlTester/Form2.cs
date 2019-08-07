// Form2: Форма тестирует элемент управления DisplayControlWin (Windows Forms)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DisplayControlWin;
using DisplayControlWpf;
using CameraData;

namespace DisplayControlTester
{
    public partial class Form2 : Form
    {
        private DisplayControl control;
        private CameraProvider cam;
        private bool isVideoOpened;
        private int VideoHandle;

        // Метод перевода в начальное состояние
        private void ControlsReset()
        {
            isVideoOpened = false;
            control.DelViewerImage(0);
            control.DelViewerImage(1);
            control.DelViewerImage(2);
            control.DelViewerImage(3);
        }

        // Обработчик события от полей вывода разметки
        private void OnDisplayCanvasEvent(
            object sender, DisplayCanvasEventArgs e)
        {
            string eventName;
            switch (e.eventID)
            {
                case DisplayCanvasEventID.FocusPointed:
                default:
                    eventName = "Focus Pointed";
                    break;
                case DisplayCanvasEventID.NodeCreated:
                    if (e.hasBox)
                        eventName = "Box Created";
                    else
                        eventName = "Marker Created";
                    break;
                case DisplayCanvasEventID.NodeUpdated:
                    if (e.hasBox)
                        eventName = "Box Updated";
                    else
                        eventName = "Marker Updated";
                    break;
            }
            string eventArgs =
                "X = " + e.clip.X.ToString() +
                "; Y = " + e.clip.Y.ToString();
            if (e.hasBox)
            {
                eventArgs +=
                    "; W = " + e.clip.Width.ToString() +
                    "; H = " + e.clip.Height.ToString();
            }
            string messageText = eventName + ": " + eventArgs;
                
            MessageBox.Show(messageText,
                "Viewer " + e.controlID.ToString() + " raised an event!");
        }

        // Обработчик события от пользовательского элемента управления
        private void OnDisplayListEvent(
            object sender, DisplayListEventArgs e)
        {
            string eventName;
            switch (e.eventID)
            {
                case DisplayListEventID.ViewChanged:
                default:
                    eventName = "View Changed";
                    break;
                case DisplayListEventID.ZoomChanged:
                    eventName = "Zoom Changed";
                    break;
            }
            string eventArgs =
                "Selected Item Index = " + e.listItemID.ToString();
            string messageText = eventName + ": " + eventArgs;

            MessageBox.Show(messageText,
                "Viewer " + e.controlID.ToString() + " raised an event!");
        }

        public Form2()
        {
            InitializeComponent();

            // Создаем интерактивный элемент отображения видео
            control = new DisplayControl();
            control.RunCanvasEvent += new UserCanvasControl.canvasEventHandler(
                OnDisplayCanvasEvent);
            control.RunListEvent += new UserCanvasControl.listEventHandler(
                OnDisplayListEvent);
            control.Dock = DockStyle.Fill;
            panel1.Controls.Add(control);

            // Создаем источник видео
            cam = new CameraProviderVideo();
            ControlsReset();

            //string ImgUrl = "D:\\Igorek\\Disser\\YanaNewNew\\MyVideoAnno\\Debug\\Koala.jpg";
            //Image image;
            //control.SetViewerImage(0, image);
        }

        private void menuVideoClose_Click(object sender, EventArgs e)
        {
            if (isVideoOpened)
            {
                cam.Close(VideoHandle);
                ControlsReset();
            }
        }

        private void menuVideoOpen_Click(object sender, EventArgs e)
        {
            Image InitialFrame;
            int FramesCount;
            double Fps;

            // 1. Ввод пути к файлу с помощью стандартного диалога
            DialogResult dlgResult = dlgVideoOpen.ShowDialog(this);
            if (dlgResult != DialogResult.OK)
                return;

            // 2. На всякий случай закрываем текущий открытый файл
            menuVideoClose_Click(sender, e);

            // 3. Пытаемся открыть новый файл
            bool success = cam.Open(dlgVideoOpen.FileName, out VideoHandle,
                out InitialFrame, out FramesCount, out Fps);
            if (!success)
            {
                MessageBox.Show("Unable to open video file!", "ERROR",
                    MessageBoxButtons.OK);
                return;
            }

            // 4. Инициализируем элементы отображения кадров
            List<string> viewCaptions = new List<string>();
            viewCaptions.Add("View1");
            viewCaptions.Add("View2");
            control.SetViewerListOfViews(0, viewCaptions, 0);
            List<string> zoomCaptions = new List<string>();
            zoomCaptions.Add("1x");
            zoomCaptions.Add("2x");
            control.SetViewerListOfZooms(0, zoomCaptions, 0);
            control.SetViewerImage(0, InitialFrame);
            control.SetViewerImage(1, InitialFrame);
            control.SetViewerImage(2, InitialFrame);
            control.SetViewerImage(3, InitialFrame);
            control.SetViewerMode(0, DisplayCanvasModeID.BoxUpdate, 
                new Rectangle(20, 20, 40, 40));
            control.SetViewerMode(1, DisplayCanvasModeID.BoxCreate);
            control.SetViewerMode(2, DisplayCanvasModeID.FocusPoint);
            isVideoOpened = true;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Закрываем видеофайл, если он был открыт
            menuVideoClose_Click(sender, e);
        }
    }
}
