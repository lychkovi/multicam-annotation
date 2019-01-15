// Проект представляет интерфейс класса для хранения переменных состояния
// слоя Trace, который должен реализовать элемент управления DisplayControl
// для автоматической перерисовки элемента управления при изменении состояния
// слоя Trace. Также проект содержит релизацию класса-контейнера переменных 
// состояния по умолчанию, реализующего данный интерфейс на случай отсутствия
// элемента управления DisplayControl. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TraceData
{
    public interface ITraceStateHolder
    {
        void StateReset();

        bool IsTraceSelected
        { // траектория выбрана?
            get; 
            set;
        }

        int TraceSelectedID
        { // индекс выбранной траектории
            get;
            set;
        }

        bool IsStateCreate
        { // инициировали создание
            get;
            set;
        }

        bool IsStateCreateMarker
        {
            get;
            set;
        }

        bool IsStateCreateStretch
        { // мышкой указали первый угол рамки, тянем второй
            get;
            set;
        }

        bool IsStateUpdate
        { // инициировали изменение
            get;
            set;
        }

        bool IsStateUpdateStretch
        { // мышкой указали первый угол рамки, тянем второй
            get;
            set;
        }

        bool IsStateUpdateAppend
        { // признак изменения траектории в режиме Append
            get;
            set;
        }
    }

    public class TraceStateHolderDummy : ITraceStateHolder
    {
        private bool m_IsTraceSelected; // траектория выбрана?
        private int m_TraceSelectedID;  // индекс выбранной траектории
        private bool m_IsStateCreate;   // инициировали создание
        private bool m_IsStateCreateMarker; // создание маркера
        private bool m_IsStateCreateStretch; // указали первый угол рамки
        private bool m_IsStateUpdate; // инициировали изменение
        private bool m_IsStateUpdateStretch; // указали первый угол рамки
        private bool m_IsStateUpdateAppend; // признак изменения траектории

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

        public TraceStateHolderDummy()
        {
            StateReset();
        }
    }
}
