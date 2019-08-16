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

using CameraData;           // DeleteObject() для освобождения Bitmap


namespace DisplayControlWpf
{
    // Точки крепления маркеров к рамке выделения
    public enum AnchorID
    {
        None,           // если нет активного якоря
        TopLeft,
        TopCenter,
        TopRight,
        MiddleRight, 
        BottomRight, 
        BottomCenter, 
        BottomLeft, 
        MiddleLeft, 
        MiddleCenter
    }

    // Класс обслуживает рамку выделения
    public class RubberBandManager
    {
        private Shape m_rubberBand;             // прямоугольная рамка выделения
        private Point m_originTopLeftCorner;    // исходное положение углов рамки
        private Point m_originBottomRightCorner;// в момент захвата мыши
        private Point m_currentTopLeftCorner;   // текущее положение углов рамки
        private Point m_currentBottomRightCorner;
        private Size m_bounds;        // допустимые пределы координат углов рамки

        // Метод кооректирует координаты точки, чтобы они попадали в заданные пределы
        private Point Bound(Point pt0)
        {
            Point pt = new Point();
            pt.X = Math.Min(pt0.X, m_bounds.Width - 1);
            pt.X = Math.Max(pt.X, 0);
            pt.Y = Math.Min(pt0.Y, m_bounds.Height - 1);
            pt.Y = Math.Max(pt.Y, 0);
            return pt;
        }

        // Методы для целостной обработки модифицируемых полей объекта
        public Point originTopLeftCorner      
        {
            get
            {
                return m_originTopLeftCorner;
            }
            set
            {
                m_originTopLeftCorner = Bound(value);
            }
        }

        public Point originBottomRightCorner
        {
            get
            {
                return m_originBottomRightCorner;
            }
            set
            {
                m_originBottomRightCorner = Bound(value);
            }
        }

        public Point currentTopLeftCorner     
        {
            get
            {
                return m_currentTopLeftCorner;
            }
            set
            {
                m_currentTopLeftCorner = Bound(value);
                Update();
            }
        }

        public Point currentBottomRightCorner
        {
            get
            {
                return m_currentBottomRightCorner;
            }
            set
            {
                m_currentBottomRightCorner = Bound(value);
                Update();
            }
        }

        // Обновление координат рамки после изменения координат углов рамки
        private void Update()
        {
            double width = Math.Abs(currentTopLeftCorner.X - currentBottomRightCorner.X);
            double height = Math.Abs(currentTopLeftCorner.Y - currentBottomRightCorner.Y);
            double left = Math.Min(currentTopLeftCorner.X, currentBottomRightCorner.X);
            double top = Math.Min(currentTopLeftCorner.Y, currentBottomRightCorner.Y);

            m_rubberBand.Width = width;
            m_rubberBand.Height = height;
            Canvas.SetLeft(m_rubberBand, left);
            Canvas.SetTop(m_rubberBand, top);
            //m_rubberBand.BringIntoView();
        }

        // Создание графической примитива прямоугольника
        public RubberBandManager()
        {
            m_rubberBand = new Rectangle();
            //if (cropperStyle != null)
            //    rubberBand.Style = cropperStyle;
            //rubberBand.Style = new Style(typeof(Shape)); //, Style.BasedOn); 
            // Style.BasedOn;// DefaultStyleKey;
            m_rubberBand.Stroke = new SolidColorBrush(Colors.Black);
            m_rubberBand.StrokeThickness = 2;
            //rubberBand.Fill = new SolidColorBrush(Colors.Black);
        }

        // Регистрация графического примитива в элементе визуализации
        public void GuiBind(Canvas canvas)
        {
            canvas.Children.Add(m_rubberBand);
        }

        // Инциализация исходных координат рамки выделения
        public void Init(System.Drawing.Rectangle clip, Size bounds)
        {
            m_bounds = bounds;

            originTopLeftCorner = new Point(clip.Left, clip.Top);
            originBottomRightCorner = new Point(clip.Right, clip.Bottom);

            currentTopLeftCorner = originTopLeftCorner;
            currentBottomRightCorner = originBottomRightCorner;

            Update();
        }

        // Отображение графического примитива
        public void Show()
        {
            m_rubberBand.Visibility = Visibility.Visible;
        }

        // Скрывание графического примитива
        public void Hide()
        {
            m_rubberBand.Visibility = Visibility.Hidden;
        }

        // Запрос границ прямоугольной рамки выделения
        public System.Drawing.Rectangle GetClientRect()
        {
            System.Drawing.Rectangle clip = new System.Drawing.Rectangle();
            clip.X = (int)Canvas.GetLeft(m_rubberBand);
            clip.Y = (int)Canvas.GetTop(m_rubberBand);
            clip.Width = (int)m_rubberBand.Width;
            clip.Height = (int)m_rubberBand.Height;
            return clip;
        }

        // Переинициализация рамки выделения в текущих границах
        public void ReInit()
        {
            System.Drawing.Rectangle clip = GetClientRect();
            Init(clip, m_bounds);
        }
    }


    public class AnchorManager
    {
        private Dictionary<AnchorID, Shape> m_anchors;  // якоря для редактирования рамки
        private AnchorID m_activeAnchorID;  // индекс активного якоря
        private double m_radius;    // радиус якоря
        private Color m_normalColor;    // цвет маркера по умолчанию

        private void SetAnchorCenter(AnchorID anchorID, Point center)
        {
            double width = 2 * m_radius;
            double height = 2 * m_radius;
            double left = center.X - m_radius;
            double top = center.Y - m_radius;

            m_anchors[anchorID].Width = width;
            m_anchors[anchorID].Height = height;
            Canvas.SetLeft(m_anchors[anchorID], left);
            Canvas.SetTop(m_anchors[anchorID], top);
            //m_anchors[anchorID].BringIntoView();
        }

        public AnchorManager()
        {
            // Инициализируем вспомогательные поля объекта
            m_activeAnchorID = AnchorID.None;
            m_normalColor = Colors.Green;
            m_radius = 3.0;

            // Создаём девять якорей
            m_anchors = new Dictionary<AnchorID, Shape>();
            Point center = new Point(0, 0);
            AnchorID[] ids = { 
                AnchorID.TopLeft, AnchorID.TopCenter, AnchorID.TopRight, 
                AnchorID.MiddleLeft, AnchorID.MiddleCenter, AnchorID.MiddleRight, 
                AnchorID.BottomLeft, AnchorID.BottomCenter, AnchorID.BottomRight
            };
            for (int i = 0; i < ids.Length; i++)
            {
                Shape anchor = new System.Windows.Shapes.Rectangle();
                anchor.Stroke = new SolidColorBrush(m_normalColor);
                anchor.StrokeThickness = 2;
                m_anchors.Add(ids[i], anchor);
                SetAnchorCenter(ids[i], center);
            }
        }

        // Метод помещает якоря на элемент визуализации пользовательского интерфейса
        public void GuiBind(Canvas canvas)
        {
            foreach (KeyValuePair<AnchorID, Shape> record in m_anchors)
                canvas.Children.Add(record.Value);
        }

        // Метод отображает якоря на пользовательском элементе управления
        public void Show()
        {
            foreach (KeyValuePair<AnchorID, Shape> record in m_anchors)
                record.Value.Visibility = Visibility.Visible;
        }

        // Метод скрывает якоря на пользовательском элементе управления
        public void Hide()
        {
            foreach (KeyValuePair<AnchorID, Shape> record in m_anchors)
                record.Value.Visibility = Visibility.Hidden;
        }

        // Метод возвращает идентификатор текущего активного якоря
        public AnchorID activeAnchorID
        {
            get { return m_activeAnchorID; }
        }

        // Метод возвращает координаты центра текущего активного якоря
        public Point GetAnchorCenter(AnchorID anchorID)
        {
            Point pt = new Point();
            if (anchorID != AnchorID.None)
            {
                pt.X = Canvas.GetLeft(m_anchors[anchorID]) + m_radius;
                pt.Y = Canvas.GetTop(m_anchors[anchorID]) + m_radius;
            }
            return pt;
        }

        // Метод возвращает координаты центра текущего активного якоря
        public Point GetActiveAnchorCenter()
        {
            return GetAnchorCenter(m_activeAnchorID);
        }

        // Метод обновляет состояние якорей по заданным координатам 
        // рамки выделения rubber и параметрам мышки e. Следует вызывать
        // из обработчика события мыши OnMouseMove(). 
        public void Update(RubberBandManager rubber, Point mousePoint, 
            bool IsMouseCaptured)
        {
            // Определяем радиус якорей
            System.Drawing.Rectangle clip = rubber.GetClientRect();
            if (clip.Width < 15 || clip.Height < 15)
                m_radius = 2.0;
            else
                m_radius = 3.0;

            // Определяем координаты всех якорей
            Point topleft = rubber.currentTopLeftCorner;
            Point bottomright = rubber.currentBottomRightCorner;
            double widthStep = 0.5 * (bottomright.X - topleft.X);
            double heightStep = 0.5 * (bottomright.Y - topleft.Y);
            Point center = topleft;
            SetAnchorCenter(AnchorID.TopLeft, center);
            center.X = topleft.X + widthStep;
            center.Y = topleft.Y;
            SetAnchorCenter(AnchorID.TopCenter, center);
            center.X = topleft.X + 2 * widthStep;
            center.Y = topleft.Y;
            SetAnchorCenter(AnchorID.TopRight, center);
            center.X = topleft.X;
            center.Y = topleft.Y + heightStep;
            SetAnchorCenter(AnchorID.MiddleLeft, center);
            center.X = topleft.X + widthStep;
            center.Y = topleft.Y + heightStep;
            SetAnchorCenter(AnchorID.MiddleCenter, center);
            center.X = topleft.X + 2 * widthStep;
            center.Y = topleft.Y + heightStep;
            SetAnchorCenter(AnchorID.MiddleRight, center);
            center.X = topleft.X;
            center.Y = topleft.Y + 2 * heightStep;
            SetAnchorCenter(AnchorID.BottomLeft, center);
            center.X = topleft.X + widthStep;
            center.Y = topleft.Y + 2 * heightStep;
            SetAnchorCenter(AnchorID.BottomCenter, center);
            center = bottomright;
            SetAnchorCenter(AnchorID.BottomRight, center);

            // При необходимости обновляем индекс активного якоря
            if (!IsMouseCaptured)
            {
                // Отыскиваем ближайший к указателю мыши якорь
                AnchorID nearestID = AnchorID.None;
                double nearestDist = 1e6;
                foreach (KeyValuePair<AnchorID, Shape> record in m_anchors)
                {
                    center = GetAnchorCenter(record.Key);
                    double distX = Math.Abs(center.X - mousePoint.X);
                    double distY = Math.Abs(center.Y - mousePoint.Y);
                    double dist = Math.Sqrt(distX * distX + distY * distY);
                    if (dist < nearestDist)
                    {
                        nearestDist = dist;
                        nearestID = record.Key;
                    }
                }

                // Сравниваем расстояние до ближайшего якоря с порогом
                if (nearestDist < 10)
                    m_activeAnchorID = nearestID;
                else
                    m_activeAnchorID = AnchorID.None;
            }

            // Обновляем цвета якорей в зависимости от их состояния
            foreach (KeyValuePair<AnchorID, Shape> record in m_anchors)
            {
                Color color;
                if (record.Key == m_activeAnchorID)
                {
                    // Активный якорь
                    color = IsMouseCaptured ? Colors.Blue : Colors.Red;
                }
                else
                {
                    // Неактивный якорь
                    color = m_normalColor;
                }
                record.Value.Stroke = new SolidColorBrush(color);
            }
        }
    }


    /// <span class="code-SummaryComment"><summary></span>
    /// Provides a Canvas where a rectangle will be drawn
    /// that matches the selection area that the user drew
    /// on the canvas using the mouse
    /// <span class="code-SummaryComment"></summary></span>
    public partial class SelectionCanvas : Canvas
    {
        #region Instance fields
        private bool m_IsOpened;          // признак наличия изображения
        private DisplayCanvasModeID m_mode;
        //private string ImgUrl = "D:\\Igorek\\Disser\\YanaNewNew\\MyVideoAnno\\Debug\\Koala.jpg";
        //private System.Windows.Media.Imaging.BitmapImage imgSource;
        private System.Windows.Media.ImageSource imgSource;
        private System.Windows.Controls.Image img;
        private RubberBandManager rubber;   // менеджер для рамки выделения
        private AnchorManager anchors;      // менеджер для якорей рамки
        private Point mouseLeftDownPoint;   


        public delegate void CanvasEventHandler(
            object sender, DisplayCanvasEventArgs e);
        public event CanvasEventHandler RunEvent;
        #endregion

        #region Events
        /// <span class="code-SummaryComment"><summary></span>
        /// Raised when the user has drawn a selection area
        /// <span class="code-SummaryComment"></summary></span>
        #endregion

        #region Ctor
        /// <span class="code-SummaryComment"><summary></span>
        /// Constructs a new SelectionCanvas, and registers the
        /// CropImage event
        /// <span class="code-SummaryComment"></summary></span>
        public SelectionCanvas()
        {
            // Создаем элемент просмотра изображения
            //imgSource = new BitmapImage(new Uri(ImgUrl));
            img = new System.Windows.Controls.Image();
            //img.Source = imgSource;
            //img.RenderTransform = new ScaleTransform(1.0, 1.0, 0, 0);
            //this.Width = imgSource.Width;
            //this.Height = imgSource.Height;
            this.Children.Add(img);

            // Создаем элемент управления прямоугольной рамкой выделения
            rubber = new RubberBandManager();
            rubber.GuiBind(this);

            // Создаём элемент управления якорями рамки
            anchors = new AnchorManager();
            anchors.GuiBind(this);

            // Инциализируем остальные поля
            m_IsOpened = false;

            // Переводим поле вывода в неактивный режим
            SetMode(DisplayCanvasModeID.Disabled);
        }
        #endregion

        #region Public Properties
        #endregion

        #region Overrides

        /// <span class="code-SummaryComment"><summary></span>
        /// Captures the mouse
        /// <span class="code-SummaryComment"></summary></span>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (!this.IsMouseCaptured)
            {
                mouseLeftDownPoint = e.GetPosition(this);
                this.CaptureMouse();

                if (m_mode == DisplayCanvasModeID.BoxCreate)
                {
                    rubber.currentTopLeftCorner = mouseLeftDownPoint;
                    rubber.currentBottomRightCorner = mouseLeftDownPoint;
                    rubber.Show();
                }
            }
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Releases the mouse, and raises the CropImage Event
        /// <span class="code-SummaryComment"></summary></span>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (this.IsMouseCaptured)
            {
                DisplayCanvasEventArgs args;
                switch (m_mode)
                {
                    case DisplayCanvasModeID.FocusPoint:
                        args = new DisplayCanvasEventArgs(DisplayCanvasEventID.FocusPointed);
                        args.clip.X = (int) e.GetPosition(this).X;
                        args.clip.Y = (int) e.GetPosition(this).Y;
                        RunEvent(this, args);
                        break;
                    case DisplayCanvasModeID.BoxCreate:
                        args = new DisplayCanvasEventArgs(DisplayCanvasEventID.NodeCreated);
                        args.clip = rubber.GetClientRect();
                        args.HasBox = true;
                        RunEvent(this, args);
                        break;
                    case DisplayCanvasModeID.BoxUpdate:
                        if (anchors.activeAnchorID != AnchorID.None)
                        {
                            args = new DisplayCanvasEventArgs(DisplayCanvasEventID.NodeUpdated);
                            args.clip = rubber.GetClientRect();
                            args.HasBox = true;
                            rubber.ReInit(); // Фиксируем изменения рамки
                        }
                        else
                        {
                            // Нажали мимо рамки - потеря фокуса
                            args = new DisplayCanvasEventArgs(DisplayCanvasEventID.FocusPointed);
                            args.clip.X = (int) e.GetPosition(this).X;
                            args.clip.Y = (int) e.GetPosition(this).Y;
                            args.HasBox = false;
                        }
                        RunEvent(this, args);
                        break;
                    case DisplayCanvasModeID.Passive:
                    case DisplayCanvasModeID.Disabled:
                        break;
                    default:
                        throw new Exception("Unsupported canvas mode detected!");
                }
                this.ReleaseMouseCapture();
            }
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Creates a child control
        /// <span class="code-SummaryComment"><see cref="System.Windows.Shapes.Rectangle">
        /// Rectangle</see></span>
        /// and adds it to this controls children collection
        /// at the co-ordinates the user
        /// drew with the mouse
        /// <span class="code-SummaryComment"></summary></span>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Point currentPoint = e.GetPosition(this);
            if (this.IsMouseCaptured)
            {
                switch (m_mode)
                {
                    case DisplayCanvasModeID.BoxCreate:
                        rubber.currentTopLeftCorner = mouseLeftDownPoint;
                        rubber.currentBottomRightCorner = currentPoint;
                        break;
                    case DisplayCanvasModeID.BoxUpdate:
                        if (anchors.activeAnchorID != AnchorID.None)
                        {
                            // Обновляем положение рамки выделения
                            double deltaX = currentPoint.X - mouseLeftDownPoint.X;
                            double deltaY = currentPoint.Y - mouseLeftDownPoint.Y;
                            Point corner = new Point();
                            switch (anchors.activeAnchorID)
                            {
                                case AnchorID.TopLeft:
                                    corner.X = rubber.originTopLeftCorner.X + deltaX;
                                    corner.Y = rubber.originTopLeftCorner.Y + deltaY;
                                    rubber.currentTopLeftCorner = corner;
                                    break;
                                case AnchorID.TopCenter:
                                    corner.X = rubber.originTopLeftCorner.X;
                                    corner.Y = rubber.originTopLeftCorner.Y + deltaY;
                                    rubber.currentTopLeftCorner = corner;
                                    break;
                                case AnchorID.TopRight:
                                    corner.X = rubber.originTopLeftCorner.X;
                                    corner.Y = rubber.originTopLeftCorner.Y + deltaY;
                                    rubber.currentTopLeftCorner = corner;
                                    corner.X = rubber.originBottomRightCorner.X + deltaX;
                                    corner.Y = rubber.originBottomRightCorner.Y;
                                    rubber.currentBottomRightCorner = corner;
                                    break;
                                case AnchorID.MiddleLeft:
                                    corner.X = rubber.originTopLeftCorner.X + deltaX;
                                    corner.Y = rubber.originTopLeftCorner.Y;
                                    rubber.currentTopLeftCorner = corner;
                                    break;
                                case AnchorID.MiddleCenter:
                                    corner.X = rubber.originTopLeftCorner.X + deltaX;
                                    corner.Y = rubber.originTopLeftCorner.Y + deltaY;
                                    rubber.currentTopLeftCorner = corner;
                                    corner.X = rubber.originBottomRightCorner.X + deltaX;
                                    corner.Y = rubber.originBottomRightCorner.Y + deltaY;
                                    rubber.currentBottomRightCorner = corner;
                                    break;
                                case AnchorID.MiddleRight:
                                    corner.X = rubber.originBottomRightCorner.X + deltaX;
                                    corner.Y = rubber.originBottomRightCorner.Y;
                                    rubber.currentBottomRightCorner = corner;
                                    break;
                                case AnchorID.BottomLeft:
                                    corner.X = rubber.originTopLeftCorner.X + deltaX;
                                    corner.Y = rubber.originTopLeftCorner.Y;
                                    rubber.currentTopLeftCorner = corner;
                                    corner.X = rubber.originBottomRightCorner.X;
                                    corner.Y = rubber.originBottomRightCorner.Y + deltaY;
                                    rubber.currentBottomRightCorner = corner;
                                    break;
                                case AnchorID.BottomCenter:
                                    corner.X = rubber.originBottomRightCorner.X;
                                    corner.Y = rubber.originBottomRightCorner.Y + deltaY;
                                    rubber.currentBottomRightCorner = corner;
                                    break;
                                case AnchorID.BottomRight:
                                    corner.X = rubber.originBottomRightCorner.X + deltaX;
                                    corner.Y = rubber.originBottomRightCorner.Y + deltaY;
                                    rubber.currentBottomRightCorner = corner;
                                    break;
                                default:
                                    break;
                            }
                            // Обновляем положение якорей рамки
                            anchors.Update(rubber, currentPoint, true);
                        }
                        break;
                    case DisplayCanvasModeID.FocusPoint:
                    case DisplayCanvasModeID.Passive:
                    case DisplayCanvasModeID.Disabled:
                    default:
                        break;
                }
            }
            else
            {
                // В режиме редактирования рамки выделяем активный якорь
                if (m_mode == DisplayCanvasModeID.BoxUpdate)
                    anchors.Update(rubber, currentPoint, false);
            }
        }
        #endregion

        // Метод преобразует изображение к объекту, пригодному для отображения
        // в среде Windows Presentation Forms. 
        public static BitmapSource GetImageStream(System.Drawing.Image myImage)
        {
            BitmapSource bitmapSource;
            using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(myImage))
            {
                IntPtr bmpPt = bitmap.GetHbitmap();
                bitmapSource =
                    System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        bmpPt,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());

                //freeze bitmapSource and clear memory to avoid memory leaks
                bitmapSource.Freeze();
                CameraProviderVideo.ReleaseBitmap(bmpPt);
            }
            return bitmapSource;
        }

        // Метод возвращает фактические размеры поля вывода изображения на форме
        public void GetClientSize(out int width, out int height)
        {
            width = (int) this.Width;
            height = (int) this.Height;
        }

        // Метод задаёт изображение для отдельного поля вывода
        public void SetImage(System.Drawing.Image newImage)
        {
            ImageSource imgSourceOld = img.Source;
            imgSource = GetImageStream(newImage);
            img.Source = imgSource;
            img.RenderTransform = new ScaleTransform(1.0, 1.0, 0, 0);
            this.Width = imgSource.Width;
            this.Height = imgSource.Height;
            rubber.Init(
                rubber.GetClientRect(), 
                new Size(this.Width, this.Height));
            m_IsOpened = true;

            // Ждем освобождения всей памяти
            GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();

            // Выходим из неактивного режима
            if (m_mode == DisplayCanvasModeID.Disabled)
                SetMode(DisplayCanvasModeID.Passive);
        }

        // Метод удаляет изображение из поля вывода и переводит его
        // в неактивный режим
        public void DelImage()
        {
            img.Source = null;
            this.Width = 0;
            this.Height = 0;
            m_IsOpened = false;

            // Входим в неактивный режим
            SetMode(DisplayCanvasModeID.Disabled);
        }

        // Метод задаём режим взаимодействия с мышкой для всех полей вывода
        // изображения, а также рамку выделения (при необходимости)
        public void SetMode(DisplayCanvasModeID mode, 
            System.Drawing.Rectangle clip = new System.Drawing.Rectangle())
        {
            if (mode == DisplayCanvasModeID.Disabled)
            {
                rubber.Hide();
                anchors.Hide();
                img.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                if (m_mode == DisplayCanvasModeID.Disabled && !m_IsOpened)
                {
                    throw new ApplicationException(
                        "Cannot enable element without image loaded!");
                }

                switch (mode)
                {
                    case DisplayCanvasModeID.BoxUpdate:
                        rubber.Init(clip, new Size(this.Width, this.Height));
                        rubber.Show();
                        anchors.Update(rubber, new Point(1e6, 1e6), false);
                        anchors.Show();
                        break;
                    default:
                        rubber.Hide();
                        anchors.Hide();
                        break;
                }

                img.Visibility = System.Windows.Visibility.Visible;
            }
            m_mode = mode;
        }
    }
}
