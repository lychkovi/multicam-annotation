// Form2: Форма тестирует элемент управления DisplayControlWin (Windows Forms)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DisplayControlWin;

namespace DisplayControlTester
{
    public partial class Form2 : Form
    {
        DisplayControlWin.DisplayControl control;

        public Form2()
        {
            InitializeComponent();

            control = new DisplayControlWin.DisplayControl();
            control.Dock = DockStyle.Fill;
            panel1.Controls.Add(control);
        }
    }
}
