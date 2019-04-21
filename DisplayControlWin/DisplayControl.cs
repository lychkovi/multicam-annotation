using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Windows.Shapes;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Xaml;

using DisplayControlWpf;    // элемент управления для отображения вида

namespace DisplayControlWin
{
    public partial class DisplayControl : UserControl
    {
        private List<Panel> viewPanels;  // массив панелей отображения видов
        private List<UserCanvasControl> viewCanvases;  
            // Массив элементов управления WPF для отображения видов

        private void canvasCallbackFcn(
            int getControlID, EventTypeID eventID, System.Drawing.Rectangle bounds)
        { 
            string messageText = 
                "X = " + bounds.X.ToString() + 
                "; Y = " + bounds.Y.ToString() +
                "; W = " + bounds.Width.ToString() + 
                "; H = " + bounds.Height.ToString();
            MessageBox.Show(messageText, 
                "Canvas " + getControlID.ToString() + " raised an event!");
        }

        public DisplayControl()
        {
            InitializeComponent();

            // Заполняем массив панелей отображения видов
            viewPanels = new List<Panel>();
            viewPanels.Add(panel1);
            viewPanels.Add(panel2);
            viewPanels.Add(panel3);
            viewPanels.Add(panel4);

            // Заполняем массив элементов WPF отображения видов
            viewCanvases = new List<UserCanvasControl>();
            int icontrol = 0;
            foreach (Panel panel in viewPanels)
            {
                ElementHost ctrlHost = new ElementHost();
                ctrlHost.Dock = DockStyle.Fill;
                panel.Controls.Add(ctrlHost);
                DisplayControlWpf.UserCanvasControl wpfControl = 
                    new DisplayControlWpf.UserCanvasControl();
                wpfControl.InitializeComponent();
                ctrlHost.Child = wpfControl;
                wpfControl.setEventCallback(icontrol, canvasCallbackFcn);
                viewCanvases.Add(wpfControl);
                icontrol++;
            }
        }
    }
}
