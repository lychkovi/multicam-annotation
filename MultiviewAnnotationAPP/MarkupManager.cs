// MarkeupManager: Класс реализует функции для работы с разметкой видеозаписи
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiviewAnnotationAPP
{
    // MarkupStatusPublic: Структура представляет публичное состояние разметки
    public struct MarkupStatusPublic
    {
        public bool isMarkupUnsaved;       // несохраненные изменения в разметке

        public void Reset()
        {
            isMarkupUnsaved = false;
        }
    }

    // MarkupStatus: Структура представляет полное состояние разметки
    struct MarkupStatus
    {
        public MarkupStatusPublic pub;     // публичное состояние разметки
        public bool isMarkupFile;          // связана ли разметка с файлом на диске
        public string MarkupFilePath;      // путь к файлу разметки

        public void Reset()
        {
            pub.Reset();
            isMarkupFile = false;
            MarkupFilePath = "";
        }
    }

    // MarkupControls: Структура представляет ссылки на элементы управления формы
    // для отображения состояния разметки
    struct MarkupControls
    {
        public TextBox txtMarkupFilePath;
        public ToolStripStatusLabel statusMarkup;
    }

    // MarkupManager: Класс реализует функции для управления разметкой
    class MarkupManager
    {
        private MarkupStatus status;
        private MarkupControls controls;

        // UpdateControls: Метод обновляет элементы управления по текущему статусу
        private void UpdateControls()
        {
            if (status.isMarkupFile)
            {
                controls.txtMarkupFilePath.Text = status.MarkupFilePath;
                if (status.pub.isMarkupUnsaved)
                {
                    controls.statusMarkup.Text = "Markup Unsaved";
                }
                else
                {
                    controls.statusMarkup.Text = "";
                }
            }
            else
            {
                controls.txtMarkupFilePath.Text = "";
                controls.statusMarkup.Text = "Markup Unsaved";
            }
        }

        // Метод сбрасывает состояние и элементы управления формы в начало,
        // его можно вызывать только после инициализации поля controls
        private void Reset()
        {
            status.Reset();
            UpdateControls();
        }

        // Метод сохраняет ссылки на элементы управления формы в поле controls
        public void InitializeComponent(MarkupControls markupControls)
        {
            controls = markupControls;
            Reset();
        }

        // Метод возвращает публичное состояние источника видео
        public MarkupStatusPublic GetStatus()
        {
            return status.pub;
        }
    }
}
