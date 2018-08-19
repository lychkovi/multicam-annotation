using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiviewAnnotationAPP
{
    public partial class Form1 : Form
    {
        private Engine engine;

        public Form1()
        {
            InitializeComponent();

            engine = new Engine();

            VideoControls videoControls;
            videoControls.txtVideoFilePath = txtVideoFilePath;
            videoControls.txtVideoFramesTotal = txtVideoFramesTotal;
            videoControls.txtVideoFps = txtVideoFps;
            videoControls.txtVideoDurationMs = txtVideoDurationMs;
            videoControls.txtVideoPositionMs = txtVideoPositionMs;
            videoControls.txtGoToFrame = txtVideoFrameCurrent;
            videoControls.txtVideoFrameCurrent = txtVideoFrameCurrent;
            videoControls.tbrVideoSlider = tbrVideoSlider;
            videoControls.statusVideo = statusVideo;
            engine.InitVideoManager(videoControls);

            MarkupControls markupControls;
            markupControls.txtMarkupFilePath = txtMarkupFilePath;
            markupControls.statusMarkup = statusMarkup;
            engine.InitMarkupManager(markupControls);

            PictureControls pictureControls;
            pictureControls.statusMode = statusMode;
            pictureControls.statusAction = statusAction;
            engine.InitPictureManager(pictureControls);
        }

        private void btnGoToFrame_Click(object sender, EventArgs e)
        {
            engine.VideoMoveTo(20);
        }
    }
}
