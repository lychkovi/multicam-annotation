// Form4.cs: Программа проверяет возможность доступа к элементам управления формы
// через вспомогательные классы (по ссылкам на элементы управления). 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VideoAnnotationAPP
{
    public partial class Form4 : Form
    {
        Proxy proxy;

        public Form4()
        {
            InitializeComponent();
            proxy = new Proxy(textBox1, label1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            proxy.Update();
        }
    }


    public class Proxy
    {
        protected TextBox textBox;
        protected Label label;

        public Proxy(TextBox textBoxIn, Label labelIn)
        {
            textBox = textBoxIn;
            label = labelIn;
        }

        public void Update()
        {
            label.Text = textBox.Text;
        }
    }
}
