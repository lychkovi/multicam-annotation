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
    enum appState
    {
        NOVID,                            // видео не загружено
        ISVID_NOTRAJ,                     // видео загружено, но траектория не выбрана
        ISVID_NOTRAJ_OPCREATE,            // видео загружено, выбрана операция создания траектории
        ISVID_NOTRAJ_OPCREATE_STRETCH,    // тянем рамку для новой траектории
        ISVIS_ISTRAJ_OPVIEW               // видео загружено, траектория выбрана, просмотр траектории
    }

    public partial class Form2 : Form
    {
        bool isVideo;           // признак успешно открытого видеофайла
        int videoHandle;
        int videoFramesTotal;
        double videoFps;
        double videoDurationMs;
        int ixFrameCurr;        // индекс текущего кадра видео
        Image frameCurr;        // текущий кадр видео
        appState appstate;      // состояние приложения
        int mouseStartX;        // координаты рамки выделения мышью
        int mouseStartY;
        int mouseStopX;
        int mouseStopY;

        // Коллекция содержит траектории всех объектов на видео
        TrackList tracks;

        // Метод смены состояния приложения
        private void enterAppState(appState newstate)
        {
            appstate = newstate;
        }

        // Операция создания новой траектории с началом на текущем кадре
        private void actionCreateTrack()
        {
            // Проверка условий применимости действия
            switch (appstate)
            {
                case appState.NOVID:
                    throw new Exception("actionCreateTrack: No video loaded!");
                    
                default:
                    // ... Дополнительные проверки ...
                    break;
            }

            // Изменение пользовательских данных в памяти приложения

            // Отображение изменений в графическом интерфейсе

            // Изменение состояния приложения
            enterAppState(appState.ISVID_NOTRAJ_OPCREATE);
        }

        // Метод перерисовывает кадр видео в поле PicturteBox на форме
        private void redrawPicture()
        {
            Graphics g;
            Point origin = new Point(0, 0);
            Pen pen;
            Rectangle rect = new Rectangle();

            switch (appstate)
            {
                case appState.NOVID:
                    break;
                case appState.ISVID_NOTRAJ_OPCREATE_STRETCH:
                case appState.ISVIS_ISTRAJ_OPVIEW:
                    // Отображаем поверх кадра текущую рамку выделения
                    g = pictureBox1.CreateGraphics();
                    g.DrawImage(frameCurr, origin);
                    pen = new Pen(DefaultForeColor);
                    rect.X = Math.Min(mouseStartX, mouseStopX);
                    rect.Y = Math.Min(mouseStartY, mouseStopY);
                    rect.Width = Math.Abs(mouseStopX - mouseStartX) + 1;
                    rect.Height = Math.Abs(mouseStopY - mouseStartY) + 1;
                    g.DrawRectangle(pen, rect);
                    break;
                default:
                    // Отображаем кадр видео без рамок выделения
                    g = pictureBox1.CreateGraphics();
                    g.DrawImage(frameCurr, origin);
                    break;
            }
        }

        public Form2()
        {
            InitializeComponent();
            isVideo = false;
            videoHandle = -1;
            appstate = appState.NOVID;

            // Инициализируем коллекцию траекторий
            tracks = new TrackList();
            tracks.videoFilePath = "none";
            tracks.Add(1, new Track());
            tracks[1].Add(1, new TrackNode());
            tracks[1].Add(2, new TrackNode());
            tracks.Add(2, new Track());
            tracks[2].Add(1, new TrackNode());
            tracks[2].Add(2, new TrackNode());

            // Выполняем сериализацию списка траекторий
            XmlTextWriter writer = new XmlTextWriter("tracks.xml", Encoding.Unicode);
            tracks.WriteXml(writer);
            writer.Close();

            // Выполняем десериализацию списка траекторий
            XmlTextReader reader = new XmlTextReader("tracks.xml");
            TrackList tracks2 = new TrackList();
            tracks2.ReadXml(reader);

            // Выполняем повторную сериализацию (для контроля)
            XmlTextWriter writer2 = new XmlTextWriter("tracks2.xml", Encoding.Unicode);
            tracks2.WriteXml(writer2);
            writer2.Close();
        }

        // Метод перематывает видео до нужной позиции
        private void GoToPosition(double video_time_ms)
        {
            // Перематываем видеофайл
            IntPtr hBitmap;
            int iframe;
            int errcode;
            errcode = OcvWrapper.VideoSeek(videoHandle, video_time_ms, out hBitmap, out iframe);
            if (errcode != 0)
            {
                MessageBox.Show("Unable to seek position in video!", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Сохраняем информацию о текущей позиции видео в полях класса
            ixFrameCurr = iframe;
            frameCurr = Image.FromHbitmap(hBitmap);

            // Отображаем информацию о текущей позиции видео на форме
            txtCurrentFrame.Text = ixFrameCurr.ToString();
            pictureBox1.Image = frameCurr;
        }

        private void CloseVideo()
        {
            if (isVideo)
            {
                OcvWrapper.VideoClose(videoHandle);
                pictureBox1.Image = null;
                txtVideoFilePath.Text = "";
                txtVideoFramesTotal.Text = "";
                txtVideoFps.Text = "";
                txtVideoDuration.Text = "";
                txtCurrentFrame.Text = "";
                txtCurrentPosition.Text = "";
                isVideo = false;
                enterAppState(appState.NOVID);
            }
        }

        private void btnOpenVideo_Click(object sender, EventArgs e)
        {
            // Открываем диалог выбора пути к видеофайлу
            DialogResult result = openVideoDlg.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
            
            // Открываем видеофайл
            IntPtr hBitmap;
            int errcode = OcvWrapper.VideoOpen(openVideoDlg.FileName, 
                out videoHandle, out hBitmap, 
                out videoFramesTotal, out videoFps);
            if (errcode != 0)
            {
                MessageBox.Show("Unable to open input video!", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Сохраняем информацию о видеофайле в полях класса
            ixFrameCurr = 0;
            frameCurr = Image.FromHbitmap(hBitmap);
            videoDurationMs = 1000.0 * videoFramesTotal / videoFps;
            isVideo = true;
            enterAppState(appState.ISVID_NOTRAJ);

            // Отображаем информацию о видеофайле на форме
            pictureBox1.Image = frameCurr;
            txtVideoFilePath.Text = openVideoDlg.FileName;
            txtVideoFramesTotal.Text = videoFramesTotal.ToString();
            txtVideoFps.Text = videoFps.ToString();
            txtVideoDuration.Text = videoDurationMs.ToString();
            trackBar1.Maximum = videoFramesTotal - 1;
            //trackBar1.LargeChange = videoFramesTotal - 1;
        }

        private void btnCloseVideo_Click(object sender, EventArgs e)
        {
            CloseVideo();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseVideo();
        }

        private void btnGoToPosition_Click(object sender, EventArgs e)
        {
            double video_time_ms = 200.0;
            GoToPosition(video_time_ms);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            double video_time_ms = trackBar1.Value * videoDurationMs / trackBar1.Maximum;
            GoToPosition(video_time_ms);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (appstate)
            {
                case appState.ISVID_NOTRAJ_OPCREATE:
                    mouseStartX = e.X;
                    mouseStartY = e.Y;
                    enterAppState(appState.ISVID_NOTRAJ_OPCREATE_STRETCH);
                    break;
                default:
                    break;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            switch (appstate)
            {
                case appState.ISVID_NOTRAJ_OPCREATE_STRETCH:
                    mouseStopX = e.X;
                    mouseStopY = e.Y;
                    redrawPicture();
                    break;
                default:
                    break;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (appstate)
            {
                case appState.ISVID_NOTRAJ_OPCREATE_STRETCH:
                    enterAppState(appState.ISVIS_ISTRAJ_OPVIEW);
                    break;
                default:
                    break;
            }
        }

        private void btnCreateTrack_Click(object sender, EventArgs e)
        {
            actionCreateTrack();
        }

    }
}

/* Работа со словарём
using System;
using System.Collections.Generic;

public class Example
{
    public static void Main()
    {
        // Create a new dictionary of strings, with string keys.
        //
        Dictionary<string, string> openWith = 
            new Dictionary<string, string>();

        // Add some elements to the dictionary. There are no 
        // duplicate keys, but some of the values are duplicates.
        openWith.Add("txt", "notepad.exe");
        openWith.Add("bmp", "paint.exe");
        openWith.Add("dib", "paint.exe");
        openWith.Add("rtf", "wordpad.exe");

        // The Add method throws an exception if the new key is 
        // already in the dictionary.
        try
        {
            openWith.Add("txt", "winword.exe");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("An element with Key = \"txt\" already exists.");
        }

        // The Item property is another name for the indexer, so you 
        // can omit its name when accessing elements. 
        Console.WriteLine("For key = \"rtf\", value = {0}.", 
            openWith["rtf"]);

        // The indexer can be used to change the value associated
        // with a key.
        openWith["rtf"] = "winword.exe";
        Console.WriteLine("For key = \"rtf\", value = {0}.", 
            openWith["rtf"]);

        // If a key does not exist, setting the indexer for that key
        // adds a new key/value pair.
        openWith["doc"] = "winword.exe";

        // The indexer throws an exception if the requested key is
        // not in the dictionary.
        try
        {
            Console.WriteLine("For key = \"tif\", value = {0}.", 
                openWith["tif"]);
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("Key = \"tif\" is not found.");
        }

        // When a program often has to try keys that turn out not to
        // be in the dictionary, TryGetValue can be a more efficient 
        // way to retrieve values.
        string value = "";
        if (openWith.TryGetValue("tif", out value))
        {
            Console.WriteLine("For key = \"tif\", value = {0}.", value);
        }
        else
        {
            Console.WriteLine("Key = \"tif\" is not found.");
        }

        // ContainsKey can be used to test keys before inserting 
        // them.
        if (!openWith.ContainsKey("ht"))
        {
            openWith.Add("ht", "hypertrm.exe");
            Console.WriteLine("Value added for key = \"ht\": {0}", 
                openWith["ht"]);
        }

        // When you use foreach to enumerate dictionary elements,
        // the elements are retrieved as KeyValuePair objects.
        Console.WriteLine();
        foreach( KeyValuePair<string, string> kvp in openWith )
        {
            Console.WriteLine("Key = {0}, Value = {1}", 
                kvp.Key, kvp.Value);
        }

        // To get the values alone, use the Values property.
        Dictionary<string, string>.ValueCollection valueColl =
            openWith.Values;

        // The elements of the ValueCollection are strongly typed
        // with the type that was specified for dictionary values.
        Console.WriteLine();
        foreach( string s in valueColl )
        {
            Console.WriteLine("Value = {0}", s);
        }

        // To get the keys alone, use the Keys property.
        Dictionary<string, string>.KeyCollection keyColl =
            openWith.Keys;

        // The elements of the KeyCollection are strongly typed
        // with the type that was specified for dictionary keys.
        Console.WriteLine();
        foreach( string s in keyColl )
        {
            Console.WriteLine("Key = {0}", s);
        }

        // Use the Remove method to remove a key/value pair.
        Console.WriteLine("\nRemove(\"doc\")");
        openWith.Remove("doc");

        if (!openWith.ContainsKey("doc"))
        {
            Console.WriteLine("Key \"doc\" is not found.");
        }
    }
}

*/