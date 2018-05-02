using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace VideoAnnotationAPP
{
    public partial class Form3 : Form
    {
        GuiContext context;     // состояние графического интерфейса
        TrackList markup;       // разметка текущего видео
        Image frameCurr;        // текущий кадр видео

        public Form3()
        {
            InitializeComponent();
            context.Reset();
            markup = new TrackList();
            markup.Reset();

            StatusBarUpdate();
            VideoNavigatorUpdate();
            PictureRedraw();
        }

        // ******************************************************************
        // Общие операции, вызываемые из обработчиков действий, инициируемых 
        // пользователем из графического интерфейса
        private bool MarkupSaveToPath(string FilePath)
        {
            // Выполняем сериализацию списка траекторий
            XmlTextWriter writer = new XmlTextWriter(
                FilePath, Encoding.Unicode);
            markup.WriteXml(writer);

            // Изменяем состояние контекста графического интерфейса
            context.isMarkupFile = true;
            context.MarkupFilePath = FilePath;
            context.isMarkupUnsaved = false;

            // Обновляем индикацию на форме
            txtMarkupFilePath.Text = context.MarkupFilePath;
            StatusBarUpdate();
            return true;
        }

        private void VideoNavigatorUpdate()
        {
            if (context.isVideo)
            {
                txtVideoFilePath.Text = context.VideoFilePath;
                txtVideoFramesTotal.Text = context.VideoFramesCount.ToString();
                txtVideoFps.Text = context.VideoFps.ToString("####.##");
                int videoDurationMsInt = (int) context.VideoDurationMs;
                txtVideoDurationMs.Text = videoDurationMsInt.ToString() + " ms";
                double videoPositionMs = context.VideoFrameCurrent * 1000.0 / context.VideoFps;
                int videoPositionMsInt = (int) videoPositionMs;
                txtVideoPositionMs.Text = videoPositionMsInt.ToString() + " ms";
                txtVideoFrameCurrent.Text = context.VideoFrameCurrent.ToString();

                // Управляем ползунком
                tbrVideoSlider.Enabled = (context.modemajor == EditModeMajor.VideoView);
            }
            else
            {
                // Видеофайл не загружен
                txtVideoFilePath.Text = "";
                txtVideoFramesTotal.Text = "";
                txtVideoFps.Text = "";
                txtVideoDurationMs.Text = "";
                txtVideoPositionMs.Text = "";
                txtVideoFrameCurrent.Text = "";
                tbrVideoSlider.Enabled = false;
            }
        }

        // Обновление надписей в строке состояния
        private void StatusBarUpdate()
        {
            if (context.isVideo)
                statusVideo.Text = "Video Loaded";
            else
                statusVideo.Text = "No Video";

            if (context.isMarkupUnsaved)
                statusMarkup.Text = "Markup Unsaved";
            else
                statusMarkup.Text = "";

            switch (context.modemajor)
            {
                case EditModeMajor.VideoNone:
                default:
                    statusMode.Text = "";
                    break;
                case EditModeMajor.VideoView:
                    statusMode.Text = "View Video";
                    break;
                case EditModeMajor.TraceCreate:
                    statusMode.Text = "Create Trace";
                    break;
                case EditModeMajor.TraceAppend:
                case EditModeMajor.TraceEdit:
                    if (context.modemajor == EditModeMajor.TraceAppend)
                        statusMode.Text = "Append Trace";
                    else
                        statusMode.Text = "Edit Trace";
                    switch (context.modedir)
                    {
                        case EditModeDir.Forward:
                            statusMode.Text += " Forward";
                            break;
                        case EditModeDir.Backward:
                            statusMode.Text += " Backward";
                            break;
                        default:
                            break;
                    }
                    break;
            }

            switch (context.modeminor)
            {
                case EditModeMinor.None:
                default:
                    statusAction.Text = "";
                    break;
                case EditModeMinor.SpecifyFirstCorner:
                    statusAction.Text = "Specify First Corner";
                    break;
                case EditModeMinor.SpecifySecondCorner:
                    statusAction.Text = "Specify Second Corner";
                    break;
                case EditModeMinor.AddPoint:
                    statusAction.Text = "Specify Feature Point";
                    break;
                case EditModeMinor.SelectPoint:
                    statusAction.Text = "Select Feature Point";
                    break;
            }
        }

        // Метод перерисовывает изображение кадра frameCurr с нанесенной 
        // разметкой траекторий. 
        private void PictureRedraw()
        {
            if (context.isVideo)
            {
                picFrameView.Image = frameCurr;
            }
            else
            {
                picFrameView.Image = null;
            }
        }

        // Метод выполняет достраивание или перезаписывание одного узла 
        // текущей редактируемой траектории в соответствии с режимом
        // с переходом к следующему кадру видеофайла
        private void TraceChangeProceed()
        {

        }

        // Метод перематывает видео к нужному кадру и соответственно
        // обновляет состояние интерфейса на форме
        private bool VideoMoveTo(int iframe)
        {
            if (!context.isVideo)
                throw new Exception("No video loaded!");

            if (iframe < 0 || iframe >= context.VideoFramesCount)
                return false;

            double positionMs = iframe * 
                context.VideoDurationMs / context.VideoFramesCount;

            // Перематываем видеофайл
            IntPtr hBitmap;
            int newframe;
            int errcode;
            errcode = OcvWrapper.VideoSeek(positionMs, out hBitmap, out newframe);
            if (errcode != 0)
            {
                MessageBox.Show("Unable to seek position in video!", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Сохраняем информацию о текущей позиции видео в полях класса
            context.VideoFrameCurrent = newframe - 1;
            frameCurr = Image.FromHbitmap(hBitmap);

            // Отображаем информацию о текущей позиции видео на форме
            VideoNavigatorUpdate();
            PictureRedraw();
            return true;
        }

        // ******************************************************************
        // Операции, инициируемые пользователем из графического интерфейса
        private bool MarkupSaveAs()
        {
            // Открываем диалог выбора пути к файлу разметки
            DialogResult dlgResult = saveMarkupFileDlg.ShowDialog(this);
            if (dlgResult != DialogResult.OK)
                return false;

            // Выполняем сериализацию, обновляем состояние контекста
            return MarkupSaveToPath(saveMarkupFileDlg.FileName);
        }

        private bool MarkupSave()
        {
            if (context.isMarkupFile)
                return MarkupSaveToPath(context.MarkupFilePath);
            else
                return MarkupSaveAs();
        }

        private bool MarkupClose()
        {
            if (context.isMarkupUnsaved)
            {
                // Спрашиваем пользователя о необходимости сохранения
                DialogResult dlgResult = MessageBox.Show(
                    "Would you like to save markup before closing?", "Attention!",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dlgResult)
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        MarkupSave();
                        break;
                    case System.Windows.Forms.DialogResult.No:
                    default:
                        break;
                    case System.Windows.Forms.DialogResult.Cancel:
                        return false;
                }
            }

            // Сбрасываем разметку траекторий
            markup.Reset();

            // Изменяняем состояние контекста графического интефейса
            context.isMarkupFile = false;
            context.MarkupFilePath = "";
            context.isMarkupUnsaved = false;
            if (context.isVideo)
                context.modemajor = EditModeMajor.VideoView;
            else
                context.modemajor = EditModeMajor.VideoNone;

            // Обновляем состояние индикации на форме
            txtMarkupFilePath.Text = context.MarkupFilePath;
            StatusBarUpdate();

            return true;
        }

        private void MarkupOpen()
        {
            bool isSuccess;

            // При необходимости выполняем сохранение разметки в файл
            if (context.isMarkupUnsaved || context.isMarkupFile)
            {
                isSuccess = MarkupClose();
                if (!isSuccess) return;
            }

            // Открываем диалог для выбора файла
            DialogResult dlgResult = openMarkupFileDlg.ShowDialog(this);
            if (dlgResult != DialogResult.OK)
                return;

            // Выполняем десериализацию списка траекторий
            XmlTextReader reader = 
                new XmlTextReader(openMarkupFileDlg.FileName);
            try
            {
                markup.ReadXml(reader);
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Problem occurred during XML parsing: " + e.Message, 
                    "Error!", MessageBoxButtons.OK);
                markup.Reset();
                return; 
            }

            // Изменяем состояние контекста графического интерфейса
            context.isMarkupFile = true;
            context.MarkupFilePath = openMarkupFileDlg.FileName;
            context.isMarkupUnsaved = false;

            // Обновляем индикацию на форме
            txtMarkupFilePath.Text = context.MarkupFilePath;
            StatusBarUpdate();
            statusMarkup.Text = "Markup Loaded";
        }

        private void VideoClose()
        {
            if (!context.isVideo)
                return;

            if (!MarkupClose())
                throw new Exception("VideoClose(): Unable to close markup!");

            // Закрываем объект декодирования видео
            int errcode = OcvWrapper.VideoClose();
            if (errcode != 0)
                throw new Exception("VideoClose(): Unable to close video with ffmpeg!");

            // Обновляем состояние контекста графического интерфейса
            context.Reset();

            // Обновляем индикацию на форме
            VideoNavigatorUpdate();
            StatusBarUpdate();
            PictureRedraw();
        }

        private void VideoOpen()
        {
            if (context.isVideo)
                VideoClose();

            DialogResult dlgResult = openVideoFileDlg.ShowDialog(this);
            if (dlgResult == DialogResult.OK)
            {
                // Открываем видеофайл
                IntPtr hBitmap;
                int videoFramesCount;
                double videoFps;
                int errcode = OcvWrapper.VideoOpen(openVideoFileDlg.FileName, 
                    out hBitmap, out videoFramesCount, out videoFps);
                if (errcode != 0)
                {
                    MessageBox.Show("Unable to open input video!", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (videoFramesCount == 0)
                {
                    MessageBox.Show("Input video is empty!", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Сохраняем информацию о видеофайле в полях класса
                frameCurr = Image.FromHbitmap(hBitmap);
                context.VideoFramesCount = videoFramesCount;
                context.VideoFps = videoFps;
                context.VideoFrameCurrent = 0;
                context.VideoDurationMs = 1000.0 * videoFramesCount / videoFps;
                context.isVideo = true;
                context.VideoFilePath = openVideoFileDlg.FileName;
                context.modemajor = EditModeMajor.VideoView;
                context.modeminor = EditModeMinor.None;
                context.modedir = EditModeDir.None;

                // Обновляем индикацию на форме
                VideoNavigatorUpdate();
                StatusBarUpdate();
                PictureRedraw();
                tbrVideoSlider.Minimum = 0;
                tbrVideoSlider.Maximum = context.VideoFramesCount - 1;
                int largeChange = context.VideoFramesCount / 20;
                if (largeChange > 1)
                    tbrVideoSlider.LargeChange = context.VideoFramesCount / 20;
                else
                    tbrVideoSlider.LargeChange = 1;
            }
        }

        // 
        void VideoMoveTo()
        {

        }

        // ******************************************************************
        // Обработчики событий элементов управления
        private void menuVideoOpen_Click(object sender, EventArgs e)
        {
            VideoOpen();
        }

        private void menuMarkupOpen_Click(object sender, EventArgs e)
        {
            MarkupOpen();
        }

        private void menuMarkupSave_Click(object sender, EventArgs e)
        {
            MarkupSave();
        }

        private void menuMarkupSaveAs_Click(object sender, EventArgs e)
        {
            MarkupSaveAs();
        }

        private void menuVideoClose_Click(object sender, EventArgs e)
        {
            VideoClose();
        }

        private void menuMarkupClose_Click(object sender, EventArgs e)
        {
            MarkupClose();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            VideoClose();
        }

        private void menuQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbrVideoSlider_ValueChanged(object sender, EventArgs e)
        {
            if (context.isVideo && context.modemajor == EditModeMajor.VideoView)
            {
                // Выполняем переход к указанной позиции видео
                VideoMoveTo(tbrVideoSlider.Value);
            }
        }

        private void btnGoToFrame_Click(object sender, EventArgs e)
        {
            int iframe; 
                
            if (context.isVideo && context.modemajor == EditModeMajor.VideoView)
            {
                // Выполняем переход к указанной позиции видео
                if (System.Int32.TryParse(txtGoToFrame.Text, out iframe) && 
                    iframe >= 0 && iframe < context.VideoFramesCount)
                    tbrVideoSlider.Value = iframe;
                else
                    MessageBox.Show("Wrong frame number entered!", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPreviousFrame_Click(object sender, EventArgs e)
        {
            if (context.isVideo && context.VideoFrameCurrent >= 1) 
            switch (context.modemajor)
            {
                case EditModeMajor.VideoView:
                    // Выполняем переход к указанной позиции видео
                    tbrVideoSlider.Value -= 1;
                    break;
                case EditModeMajor.TraceEdit:
                case EditModeMajor.TraceAppend:
                    // Выполняем добавление нового узла траектории
                    if (context.modedir == EditModeDir.Backward)
                        TraceChangeProceed();
                    break;
                default:
                    // Ничего не делаем
                    break;
            }
        }

        private void btnNextFrame_Click(object sender, EventArgs e)
        {
            if (context.isVideo && 
                context.VideoFrameCurrent < context.VideoFramesCount - 1)
            switch (context.modemajor)
            {
                case EditModeMajor.VideoView:
                    // Выполняем переход к указанной позиции видео
                    tbrVideoSlider.Value += 1;
                    break;
                case EditModeMajor.TraceEdit:
                case EditModeMajor.TraceAppend:
                    // Выполняем добавление нового узла траектории
                    if (context.modedir == EditModeDir.Forward)
                        TraceChangeProceed();
                    break;
                default:
                    // Ничего не делаем
                    break;
            }
        }
    }
}
