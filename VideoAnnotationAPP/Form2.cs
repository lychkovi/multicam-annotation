using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;       // для DllImport native C++

namespace VideoAnnotationAPP
{
    public partial class Form2 : Form
    {
        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int open_video(String filepath, out IntPtr hBitmap, 
            out int nframes, out double fps);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int close_video();

        bool isVideo;           // признак успешно открытого видеофайла
        int videoFramesTotal;
        double videoFps;
        double videoDurationMs;
        int ixFrameCurr;        // индекс текущего кадра видео
        Image frameCurr;          // текущий кадр видео

        public Form2()
        {
            InitializeComponent();
            isVideo = false;
        }

        private void CloseVideo()
        {
            if (isVideo)
            {
                close_video();
                pictureBox1.Image = null;
                txtVideoFilePath.Text = "";
                txtVideoFramesTotal.Text = "";
                txtVideoFps.Text = "";
                txtVideoDuration.Text = "";
                txtCurrentFrame.Text = "";
                txtCurrentPosition.Text = "";
                isVideo = false;
            }
        }

        private void btnOpenVideo_Click(object sender, EventArgs e)
        {
            // Открываем диалог выбора пути к видеофайлу
            DialogResult result = openVideoDlg.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
            
            // Открываем видеофайл
            IntPtr hBitmap;
            int errcode = open_video(openVideoDlg.FileName, out hBitmap, 
                out videoFramesTotal, out videoFps);
            if (errcode != 0)
            {
                MessageBox.Show("Unable to open input video!", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Сохраняем информацию о видеофайле в полях класса
            ixFrameCurr = 0;
            frameCurr = Image.FromHbitmap(hBitmap);
            videoDurationMs = videoFramesTotal / videoFps;
            isVideo = true;

            // Отображаем информацию о видеофайле на форме
            pictureBox1.Image = frameCurr;
            txtVideoFilePath.Text = openVideoDlg.FileName;
            txtVideoFramesTotal.Text = videoFramesTotal.ToString();
            txtVideoFps.Text = videoFps.ToString();
            txtVideoDuration.Text = videoDurationMs.ToString();
            trackBar1.Maximum = videoFramesTotal - 1;
            //trackBar1.LargeChange = videoFramesTotal - 1;
        }

        private void btnCloseVideo_Click(object sender, EventArgs e)
        {
            CloseVideo();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseVideo();
        }

    }
}
