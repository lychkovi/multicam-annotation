using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Xaml;

namespace DisplayControlTester
{
    public partial class Form1 : Form
    {
        private ElementHost ctrlHost;
        private DisplayControlWpf.DisplayControl wpfAddressCtrl;
        System.Windows.FontWeight initFontWeight;
        double initFontSize;
        System.Windows.FontStyle initFontStyle;
        System.Windows.Media.SolidColorBrush initBackBrush;
        System.Windows.Media.SolidColorBrush initForeBrush;
        System.Windows.Media.FontFamily initFontFamily;

        public Form1()
        {
            InitializeComponent();

            ctrlHost = new ElementHost();
            ctrlHost.Dock = DockStyle.Fill;
            panel1.Controls.Add(ctrlHost);
            wpfAddressCtrl = new DisplayControlWpf.DisplayControl();
            wpfAddressCtrl.InitializeComponent();
            ctrlHost.Child = wpfAddressCtrl;
            wpfAddressCtrl.OnResize(panel1.Width, panel1.Height);
        }
    }
}
