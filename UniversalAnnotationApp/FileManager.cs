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
    }

    public interface IFile
    {
        void FileGuiBind(FileManagerControls controls);
        bool FileOnFormClosing();        // вызов при выходе из приложения

        void FileOnRecordingCreate();   // обработчики событий меню
        void FileOnRecordingOpen();
        void FileOnRecordingClose();
    }

    abstract class FileManagerBase : DisplayManager, IFile
    {
        abstract public void FileGuiBind(FileManagerControls controls);
        abstract public bool FileOnFormClosing(); // при выходе из приложения

        abstract protected bool FileCameraOpen(RecordingInfo rec);
        abstract protected void FileCameraClose();
        abstract protected bool FileMarkupOpen(string MarkupFilePath);
        abstract protected bool FileMarkupClose();

        // Обработчики событий меню
        abstract public void FileOnRecordingCreate();   
        abstract public void FileOnRecordingOpen();
        abstract public void FileOnRecordingClose();
    }

    class FileManager: FileManagerBase
    {
        // Ссылки на элементы управления формы, за которые отвечает элемент
        FileManagerControls m_Controls;

        override public void FileGuiBind(FileManagerControls controls)
        {
            m_Controls = controls;
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

        /* Метод обновляет информацию на элементах управления формы, которые
         * относятся к данному слою, в зависимости от состояния модели. */
        private void m_ControlsUpdate()
        {
            if (CameraIsOpened)
            {
                /* ... */
            }

            if (MarkupIsOpened)
            {
                /* ... */
            }
        }

        override protected bool FileCameraOpen(RecordingInfo rec)
        {
            if (DisplayCameraOpen(rec))
            {
                m_ControlsUpdate();
                return true;
            }
            else
                return false;
        }

        override protected void FileCameraClose()
        {
            DisplayCameraClose();
            m_ControlsUpdate();
        }

        override protected bool FileMarkupOpen(string MarkupFilePath)
        {
            if (MarkupIsOpened)
                if (!FileMarkupClose()) return false;

            if (DisplayMarkupOpen(MarkupFilePath))
            {
                m_ControlsUpdate();
                return true;
            }
            else
                return false;
        }

        override protected bool FileMarkupClose()
        {
            if (!MarkupIsSaved)
            {
                /* Спросить о необходимости сохранения изменений. */
                /* ... */
            }
            DisplayMarkupClose();
            m_ControlsUpdate();
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
                        result = m_Controls.dlgRecordingSave.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            // Готовим структуру данных для записи в XML
                            MarkupProvider xml = new MarkupProviderADO();
                            xml.InitHeader(rec);
                            xml.Save(m_Controls.dlgRecordingSave.FileName);
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
            DialogResult result = m_Controls.dlgRecordingOpen.ShowDialog();
            if (result != DialogResult.OK)
                return;

            // Если видеозапись открыта, её сначала нужно закрыть
            if (CameraIsOpened)
                FileOnRecordingClose();

            // Затем открываем новую видеозапись
            //try
            //{
                MarkupProvider xml = new MarkupProviderADO();
                if (!xml.Open(m_Controls.dlgRecordingOpen.FileName))
                {
                    MessageBox.Show("Unable to open video recording XML!",
                        "ERROR!", MessageBoxButtons.OK);
                    return;
                }
                RecordingInfo rec = xml.GetHeader();

                if (!DisplayCameraOpen(rec))
                {
                    MessageBox.Show("Unable to open video recording!",
                        "ERROR!", MessageBoxButtons.OK);
                    return;
                }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message, "ERROR!", MessageBoxButtons.OK);
            //}
        }

        override public void FileOnRecordingClose()
        {
            if (CameraIsOpened)
                DisplayCameraClose();
        }
    }
}
