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
            FileManagerControls fileControls;
            fileControls.dlgMarkupOpen = dlgMarkupOpen;
            fileControls.dlgMarkupSave = dlgMarkupSave;
            fileControls.dlgRecordingOpen = dlgRecordingOpen;
            fileControls.dlgRecordingSave = dlgRecordingSave;
            engine.FileGuiBind(fileControls);
        }

        private void menuRecordingCreate_Click(object sender, EventArgs e)
        {
            engine.FileOnRecordingCreate();
        }
    }
}
