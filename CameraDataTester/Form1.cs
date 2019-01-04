using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CameraData;               // тестируемые классы поставщиков видео

namespace CameraDataTester
{
    public partial class Form1 : Form
    {
        private CameraProvider cam;
        private bool isVideoOpened;
        private int VideoHandle;

        // Метод перевода в начальное состояние
        private void ControlsReset()
        {
            pbrVideo.Value = 0;
            picVideo.Image = null;
            isVideoOpened = false;
            lblVideoFilePath.Text = "No Video Opened";
            tbxVideoCurrentFrame.Text = "";
            tbxVideoFps.Text = "";
            tbxVideoTotalFrames.Text = "";
        }

        public Form1()
        {
            InitializeComponent();
            cam = new CameraProviderVideo();
            ControlsReset();
        }

        private void btnVideoClose_Click(object sender, EventArgs e)
        {
            if (isVideoOpened)
            {
                cam.Close(VideoHandle);
                ControlsReset();
            }
        }

        private void btnVideoOpen_Click(object sender, EventArgs e)
        {
            Image InitialFrame;
            int FramesCount;
            double Fps;

            // 1. Ввод пути к файлу с помощью стандартного диалога
            DialogResult dlgResult = dlgVideoOpen.ShowDialog(this);
            if (dlgResult != DialogResult.OK)
                return;

            // 2. На всякий случай закрываем текущий открытый файл
            btnVideoClose_Click(sender, e);

            // 3. Пытаемся открыть новый файл
            bool success = cam.Open(dlgVideoOpen.FileName, out VideoHandle, 
                out InitialFrame, out FramesCount, out Fps);
            if (!success)
            {
                MessageBox.Show("Unable to open video file!", "ERROR",
                    MessageBoxButtons.OK);
                return;
            }

            // 4. Инициализируем элементы управления на форме
            pbrVideo.Value = 0;
            picVideo.Image = InitialFrame;
            isVideoOpened = true;
            lblVideoFilePath.Text = dlgVideoOpen.FileName;
            tbxVideoCurrentFrame.Text = "0";
            tbxVideoFps.Text = Fps.ToString();
            tbxVideoTotalFrames.Text = FramesCount.ToString();
        }

        private void btnVideoGoToFrame_Click(object sender, EventArgs e)
        {
            int FrameID;
            int FramesCount;

            if (int.TryParse(tbxVideoGoToFrame.Text, out FrameID))
            {
                picVideo.Image = cam.GetFrame(VideoHandle, FrameID);
                tbxVideoCurrentFrame.Text = FrameID.ToString();
                FramesCount = int.Parse(tbxVideoTotalFrames.Text);
                if (FramesCount >= 2)
                {
                    pbrVideo.Value = 100 * FrameID / (FramesCount - 1);
                }
                else
                {
                    pbrVideo.Value = 100;
                }   
            }
            else
            {
                MessageBox.Show("Wrong frame number!", "ERROR!", 
                    MessageBoxButtons.OK);
                tbxVideoGoToFrame.Text = "";
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Закрываем видеофайл, если он был открыт
            btnVideoClose_Click(sender, e);
        }

    }
}
