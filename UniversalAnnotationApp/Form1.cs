using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UniversalAnnotationApp
{
    public partial class Form1 : Form
    {
        private FileManager engine;

        public Form1()
        {
            InitializeComponent();

            engine = new FileManager();

            // Регистрируем элементы управления в отдельных слоях
            CameraManagerControls cameraControls;
            cameraControls.statusVideo = statusVideo;
            engine.CameraGuiBind(cameraControls);

            FileManagerControls fileControls;
            fileControls.dlgMarkupOpen = dlgMarkupOpen;
            fileControls.dlgMarkupSave = dlgMarkupSave;
            fileControls.dlgRecordingOpen = dlgRecordingOpen;
            fileControls.dlgRecordingSave = dlgRecordingSave;
            engine.FileGuiBind(fileControls);
        }

        // Событие вызывается непосредственно перед закрытием формы, может 
        // отменить закрытие.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!engine.FileOnFormClosing())
                e.Cancel = true;
        }

        private void menuRecordingCreate_Click(object sender, EventArgs e)
        {
            engine.FileOnRecordingCreate();
        }

        private void menuRecordingOpen_Click(object sender, EventArgs e)
        {
            engine.FileOnRecordingOpen();
        }

        private void menuRecordingClose_Click(object sender, EventArgs e)
        {
            engine.FileOnRecordingClose();
        }
    }
}
