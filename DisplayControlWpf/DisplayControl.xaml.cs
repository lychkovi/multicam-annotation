// DLL-бибилотека пользовательского элемента управления DisplayControl

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using TraceData;            // Интерфейс ITraceStateHolder


namespace DisplayControlWpf
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class DisplayControl : UserControl, ITraceStateHolder
    {
        private bool m_IsTraceSelected; // траектория выбрана?
        private int m_TraceSelectedID;  // индекс выбранной траектории
        private bool m_IsStateCreate;   // инициировали создание
        private bool m_IsStateCreateMarker; // создание маркера
        private bool m_IsStateCreateStretch; // указали первый угол рамки
        private bool m_IsStateUpdate; // инициировали изменение
        private bool m_IsStateUpdateStretch; // указали первый угол рамки
        private bool m_IsStateUpdateAppend; // признак изменения траектории

        public DisplayControl()
        {
            InitializeComponent();
            StateReset();
        }

        // Реализация методов интерфейса ITraceStateHolder
        public void StateReset()
        {
            // Выбор траектории
            m_IsTraceSelected = false;
            m_TraceSelectedID = -1;

            // Создание траектории
            m_IsStateCreate = false;
            m_IsStateCreateStretch = false;
            m_IsStateCreateMarker = false;

            // Редактирование траектории
            m_IsStateUpdate = false;
            m_IsStateUpdateStretch = false;
            m_IsStateUpdateAppend = false;
        }

        public bool IsTraceSelected
        { // траектория выбрана?
            get { return m_IsTraceSelected; }

            set
            {
                m_IsTraceSelected = value;
            }
        }

        public int TraceSelectedID
        { // индекс выбранной траектории
            get { return m_TraceSelectedID; }

            set
            {
                m_TraceSelectedID = value;
            }
        }

        public bool IsStateCreate
        { // инициировали создание
            get { return m_IsStateCreate; }

            set
            {
                m_IsStateCreate = value;
            }
        }

        public bool IsStateCreateMarker
        {
            get { return m_IsStateCreateMarker; }

            set
            {
                m_IsStateCreateMarker = value;
            }
        }

        public bool IsStateCreateStretch
        { // мышкой указали первый угол рамки, тянем второй
            get { return m_IsStateCreateStretch; }

            set
            {
                m_IsStateCreateStretch = value;
            }
        }

        public bool IsStateUpdate
        { // инициировали изменение
            get { return m_IsStateUpdate; }

            set
            {
                m_IsStateUpdate = value;
            }
        }

        public bool IsStateUpdateStretch
        { // мышкой указали первый угол рамки, тянем второй
            get { return m_IsStateUpdateStretch; }

            set
            {
                m_IsStateUpdateStretch = value;
            }
        }

        public bool IsStateUpdateAppend
        { // признак изменения траектории в режиме Append
            get { return m_IsStateUpdateAppend; }

            set
            {
                m_IsStateUpdateAppend = value;
            }
        }

        // Метод обновляет размеры дочерних элементов в случае изменения 
        // размеров самого элемента управления NewWidth и NewHeight.
        public void OnResize(double NewWidth, double NewHeight)
        {
            canvasControl.Width = NewWidth;
            canvasControl.Height = NewHeight - 44.0;

            rectangleClip.Width = NewWidth;
            rectangleClip.Height = NewHeight;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.HeightChanged || e.WidthChanged)
            {
                OnResize(e.NewSize.Width, e.NewSize.Height);
            }
        }
    }
}
