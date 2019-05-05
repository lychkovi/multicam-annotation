using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MarkupData;


namespace UniversalAnnotationApp
{
    // Ссылки на все элементы управления формы, за которые отвечает слой
    public struct FileManagerControls
    {
        public SaveFileDialog dlgRecordingSave;
        public OpenFileDialog dlgRecordingOpen;
        public SaveFileDialog dlgMarkupSave;
        public OpenFileDialog dlgMarkupOpen;
        public TextBox txtRecordingFile;    // открытый файл видеозаписи
        public TextBox txtMarkupFile;       // открытый файл разметки
    }

    // Элемент управления состоянием слоя с автоматическим обновлением
    // инйормации на форме
    public class FileManagerState
    {
        FileManagerControls m_Controls;
        string m_RecordingFile; // путь к открытому XML-файлу видеозаписи
        string m_MarkupFile;    // путь к открытому XML-файлу разметки

        /* Метод обновляет информацию на элементах управления формы, которые
         * относятся к данному слою, в зависимости от состояния модели. */
        private void m_ControlsUpdate()
        {
            if (m_Controls.txtRecordingFile != null)
                m_Controls.txtRecordingFile.Text = m_RecordingFile;

            if (m_Controls.txtMarkupFile != null)
                m_Controls.txtMarkupFile.Text = m_MarkupFile;
        }

        public void GuiBind(FileManagerControls controls)
        {
            m_Controls = controls;
            m_ControlsUpdate();
        }

        public string RecordingFile
        {
            get { return m_RecordingFile; }

            set
            {
                m_RecordingFile = value;
                m_ControlsUpdate();
            }
        }

        public string MarkupFile
        {
            get { return m_MarkupFile; }

            set
            {
                m_MarkupFile = value;
                m_ControlsUpdate();
            }
        }

        public SaveFileDialog dlgRecordingSave
        {
            get { return m_Controls.dlgRecordingSave; }
        }

        public OpenFileDialog dlgRecordingOpen
        {
            get { return m_Controls.dlgRecordingOpen; }
        }

        public SaveFileDialog dlgMarkupSave
        {
            get { return m_Controls.dlgMarkupSave; }
        }

        public OpenFileDialog dlgMarkupOpen
        {
            get { return m_Controls.dlgMarkupOpen; }
        }

        public FileManagerState()
        {
            m_RecordingFile = "";
            m_MarkupFile = "";
        }
    }

    public interface IFile
    {
        void FileGuiBind(FileManagerControls controls);
        bool FileOnFormClosing();        // вызов при выходе из приложения

        void FileOnRecordingCreate();   // обработчики событий меню
        void FileOnRecordingOpen();
        void FileOnRecordingClose();

        void FileOnMarkupSave();
        void FileOnMarkupSaveAs();
        void FileOnMarkupOpen();
        void FileOnMarkupClose();
    }

    abstract class FileManagerBase : TraceManager, IFile
    {
        abstract public void FileGuiBind(FileManagerControls controls);
        abstract public bool FileOnFormClosing(); // при выходе из приложения

        // Эти четыре метода должны быть у всех слоев приложения
        abstract protected bool FileCameraOpen(RecordingInfo rec);
        abstract protected void FileCameraClose();
        abstract protected bool FileMarkupOpen(string MarkupFilePath);
        abstract protected bool FileMarkupClose();

        // Обработчики событий меню
        abstract public void FileOnRecordingCreate();   
        abstract public void FileOnRecordingOpen();
        abstract public void FileOnRecordingClose();

        abstract public void FileOnMarkupSave();
        abstract public void FileOnMarkupSaveAs();
        abstract public void FileOnMarkupOpen();
        abstract public void FileOnMarkupClose();
    }

    class FileManager: FileManagerBase
    {
        // Ссылки на элементы управления формы, за которые отвечает элемент
        FileManagerState m_gui;

        override public void FileGuiBind(FileManagerControls controls)
        {
            m_gui.GuiBind(controls);
        }

        override protected bool FileCameraOpen(RecordingInfo rec)
        {
            return TraceCameraOpen(rec);
        }

        override protected void FileCameraClose()
        {
            if (CameraIsOpened)
            {
                if (MarkupIsOpened)
                {
                    if (!FileMarkupClose())
                        return;
                }

                TraceCameraClose();
            }
        }

        override protected bool FileMarkupOpen(string MarkupFilePath)
        {
            if (MarkupIsOpened)
                if (!FileMarkupClose()) return false;

            return TraceMarkupOpen(MarkupFilePath);
        }

        override protected bool FileMarkupClose()
        {
            if (!MarkupIsOpened)
                return true;

            if (!MarkupIsSaved)
            {
                /* Спросить о необходимости сохранения изменений. */
                DialogResult result = MessageBox.Show("Save markup changes?",
                    "WARNING!", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        FileOnMarkupSave();
                        break;
                    case DialogResult.No:
                    default:
                        break;
                    case DialogResult.Cancel:
                        return false;
                }
            }
            TraceMarkupClose();
            return true;
        }

        // Обработчик события меню создания нового XML-файла описания 
        // видеозаписи
        override public void FileOnRecordingCreate()
        {
            CreateRecordingInfoDialog CreateDialog = 
                new CreateRecordingInfoDialog();
            
            RecordingInfo rec = new RecordingInfo();
            bool isSuccess;
            do
            {
                isSuccess = true;
                DialogResult result = CreateDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Извлекаем информацию из видеофайлов, сохраняем
                    // в структуру RecordingInfo
                    try
                    {
                        rec = CameraCreate(CreateDialog.FilePathArray,
                            CreateDialog.Comment);

                        // Запрашиваем путь к файлу для сохранения XML
                        result = m_gui.dlgRecordingSave.ShowDialog();
                        string path = m_gui.dlgRecordingSave.FileName;
                        rec.FileNameXML = path;

                        if (result == DialogResult.OK)
                        {
                            // Готовим структуру данных для записи в XML
                            MarkupProvider xml = new MarkupProviderADO();
                            xml.InitHeader(rec);
                            xml.Save(path);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "ERROR!", 
                            MessageBoxButtons.OK);
                        isSuccess = false;
                    }
                }
            }
            while (!isSuccess);

            CreateDialog.Dispose();
        }

        override public void FileOnRecordingOpen()
        {
            // Запрашиваем путь к XML-файлу видеозаписи
            DialogResult result = m_gui.dlgRecordingOpen.ShowDialog();
            if (result != DialogResult.OK)
                return;

            // Если видеозапись открыта, её сначала нужно закрыть
            if (CameraIsOpened)
                FileOnRecordingClose();

            // Затем открываем новую видеозапись
            try
            {
                MarkupProvider xml = new MarkupProviderADO();
                if (!xml.Open(m_gui.dlgRecordingOpen.FileName))
                {
                    MessageBox.Show("Unable to open video recording XML!",
                        "ERROR!", MessageBoxButtons.OK);
                    return;
                }
                RecordingInfo rec = xml.GetHeader();

                if (!FileCameraOpen(rec))
                {
                    MessageBox.Show("Unable to open video recording!",
                        "ERROR!", MessageBoxButtons.OK);
                    return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR!", MessageBoxButtons.OK);
            }

            // Обновляем пути к файлам разметки и видеозаписи на форме
            m_gui.RecordingFile = m_gui.dlgRecordingOpen.FileName;
        }

        override public void FileOnRecordingClose()
        {
            if (CameraIsOpened)
                FileCameraClose();

            m_gui.RecordingFile = "";
            m_gui.MarkupFile = "";
        }

        /* Вспомогательный метод, вызывается из FileOnMarkupSaveAs и
         * FileOnMarkupSave. */
        private void m_MarkupSave(string MarkupFilePath)
        {
            if (MarkupSave(MarkupFilePath))
            {
                m_gui.MarkupFile = MarkupFilePath;
            }
            else
            {
                MessageBox.Show("Unable to save markup!", "ERROR!",
                    MessageBoxButtons.OK);
            }
        }

        override public void FileOnMarkupSaveAs()
        {
            if (!MarkupIsOpened)
            {
                MessageBox.Show("You should open markup first!",
                    "ERROR", MessageBoxButtons.OK);
                return;
            }

            // Запрашиваем путь для сохранения XML-файла разметки
            DialogResult result = m_gui.dlgMarkupSave.ShowDialog();
            if (result == DialogResult.OK)
            {
                m_MarkupSave(m_gui.dlgMarkupSave.FileName);
            }
        }

        override public void FileOnMarkupSave()
        {
            if (!MarkupIsOpened)
            {
                MessageBox.Show("You should open markup first!", 
                    "ERROR", MessageBoxButtons.OK);
                return;
            }

            if (m_gui.MarkupFile == "")
                FileOnMarkupSaveAs();
            else
                m_MarkupSave(m_gui.MarkupFile);
        }

        override public void FileOnMarkupOpen()
        {
            if (!CameraIsOpened)
            {
                MessageBox.Show("You should open recording first!",
                    "ERROR", MessageBoxButtons.OK);
                return;
            }

            if (MarkupIsOpened)
            {
                if (!FileMarkupClose())
                    return;
            }

            // Запрашиваем путь к XML-файлу разметки
            DialogResult result = m_gui.dlgMarkupOpen.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    if (FileMarkupOpen(m_gui.dlgMarkupOpen.FileName))
                        m_gui.MarkupFile = m_gui.dlgMarkupOpen.FileName;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "ERROR!",
                        MessageBoxButtons.OK);
                }
            }
        }

        override public void FileOnMarkupClose()
        {
            if (FileMarkupClose())
                m_gui.MarkupFile = "";
        }

        // При выходе из приложения нудно закрыть все открытые файлы
        override public bool FileOnFormClosing()
        {
            if (MarkupIsOpened)
            {
                // Если разметка не сохранена, предложим пользователю
                // сохранить разметку или отказаться от закрытия формы
                if (!FileMarkupClose())
                    return false;
            }

            if (CameraIsOpened)
            {
                FileCameraClose();
            }
            return true;
        }

        public FileManager()
        {
            m_gui = new FileManagerState();
        }
    }
}
