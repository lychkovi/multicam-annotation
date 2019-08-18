using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Интеграция с Windows Presentation Forms
using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Xaml;

namespace UniversalAnnotationApp
{
    public partial class Form1 : Form
    {
        private ElementHost ctrlHost;
        private DisplayControlWin.DisplayControl DisplayCtrl;
        private FileManager engine;

        public Form1()
        {
            InitializeComponent();

            // Добавляем элемент управления DisplayControlWpf
            DisplayCtrl = new DisplayControlWin.DisplayControl();
            DisplayCtrl.Dock = DockStyle.Fill;
            pnlDisplay.Controls.Add(DisplayCtrl);

            // Создаем объект бизнес-логики приложения
            engine = new FileManager();

            // Регистрируем элементы управления в отдельных слоях объекта
            CameraManagerControls cameraControls;
            cameraControls.statusVideo = statusVideo;
            engine.CameraGuiBind(cameraControls);

            MarkupManagerControls markupControls;
            markupControls.statusMarkup = statusMarkup;
            engine.MarkupGuiBind(markupControls);

            DisplayManagerControls displayControls;
            displayControls.Ctrl = DisplayCtrl;
            engine.DisplayGuiBind(displayControls);

            TraceManagerControls traceControls;
            traceControls = new TraceManagerControls();
            // Понель воспроизведения
            traceControls.grpNavigation = grpNavigation;
            traceControls.btnNaviGotoFrame = btnNaviGotoFrame;
            traceControls.btnNaviNext = btnNaviNext;
            traceControls.btnNaviPlayStop = btnNaviPlayStop;
            traceControls.btnNaviPrevious = btnNaviPrevious;
            traceControls.chkNaviPlayReverse = chkNaviPlayReverse;
            traceControls.cmbNaviPlaySpeed = cmbNaviPlaySpeed;
            traceControls.radNaviBoxMajor = radNaviBoxMajor;
            traceControls.radNaviMarkerMajor = radNaviMarkerMajor;
            traceControls.txtNaviCurrFrame = txtNaviCurrFrame;
            traceControls.txtNaviGotoFrame = txtNaviGotoFrame;
            traceControls.txtNaviTotalFrames = txtNaviTotalFrames;
            traceControls.trbNaviSlider = trbNaviSlider;
            traceControls.tmrPlayTimer = tmrPlayTimer;
            // Панель отслеживания объекта
            traceControls.grpTracking = grpTracking;
            traceControls.cmbTrackingMethod = cmbTrackingMethod;
            traceControls.chkTrackingReverse = chkTrackingReverse;
            traceControls.chkTrackingIsOccluded = chkTrackingIsOccluded;
            traceControls.chkTrackingIsShaded = chkTrackingIsShaded;
            traceControls.btnTrackingSeekExtent = btnTrackingSeekExtent;
            traceControls.btnTrackingTruncate = btnTrackingTruncate;
            traceControls.btnTrackingTrack = btnTrackingTrack;
            // Панель категории
            traceControls.grpCategory = grpCategory;
            traceControls.cmbCategoryID = cmbCategoryID;
            traceControls.txtCategoryName = txtCategoryName;
            traceControls.btnCategoryNew = btnCategoryNew;
            traceControls.btnCategoryDelete = btnCategoryDelete;
            traceControls.btnCategoryEditSave = btnCategoryEditSave;
            // Регистрируем все элементы управления в слое TraceManager
            engine.TraceGuiBind(traceControls);

            FileManagerControls fileControls;
            fileControls.dlgMarkupOpen = dlgMarkupOpen;
            fileControls.dlgMarkupSave = dlgMarkupSave;
            fileControls.dlgRecordingOpen = dlgRecordingOpen;
            fileControls.dlgRecordingSave = dlgRecordingSave;
            fileControls.txtRecordingFile = txtRecordingFile;
            fileControls.txtMarkupFile = txtMarkupFile;
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

        private void menuMarkupOpen_Click(object sender, EventArgs e)
        {
            engine.FileOnMarkupOpen();
        }

        private void menuMarkupSave_Click(object sender, EventArgs e)
        {
            engine.FileOnMarkupSave();
        }

        private void menuMarkupSaveAd_Click(object sender, EventArgs e)
        {
            engine.FileOnMarkupSaveAs();
        }

        private void menuMarkupClose_Click(object sender, EventArgs e)
        {
            engine.FileOnMarkupClose();
        }

        private void menuTraceCreate_Click(object sender, EventArgs e)
        {
            engine.TraceTraceCreate();
        }

        private void menuTraceDelete_Click(object sender, EventArgs e)
        {
            engine.TraceTraceDelete();
        }
    }
}
