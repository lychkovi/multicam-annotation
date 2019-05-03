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

        public Form2()
        {
            InitializeComponent();

            // Создаем интерактивный элемент отображения видео
            control = new DisplayControl();
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
            control.SetViewerListOfViews(0, viewCaptions, 0);
            List<string> zoomCaptions = new List<string>();
            zoomCaptions.Add("1x");
            control.SetViewerListOfZooms(0, zoomCaptions, 0);
            control.SetViewerImage(0, InitialFrame);
            control.SetViewerImage(1, InitialFrame);
            control.SetViewerImage(2, InitialFrame);
            control.SetViewerImage(3, InitialFrame);
            control.SetViewerMode(0, CanvasModeID.RubberUpdate, 
                new Rectangle(20, 20, 40, 40));
            control.SetViewerMode(1, CanvasModeID.RubberCreate);
            control.SetViewerMode(2, CanvasModeID.PointSelect);
            isVideoOpened = true;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Закрываем видеофайл, если он был открыт
            menuVideoClose_Click(sender, e);
        }
    }
}
