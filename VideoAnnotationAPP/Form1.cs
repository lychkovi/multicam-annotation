// Form1.cs: Главная форма приложения для разметки движущихся объектов в кадрах видеопотока.
// https://www.c-sharpcorner.com/uploadfile/tanmayit08/unmanaged-cpp-dll-call-from-managed-C-Sharp-application/
//
// Создаем решение из двух проектов: 
// 1) C++ Win32 Console Application (при создании проекта ставим галки DLL, empty project)
// 2) C# Windows Forms Application
//
// В настройках проекта C# 
// 1) На вкладке Построение указываем Путь выхода: 
// ..\Debug\ (чтобы исполняемый файл находился в той же директории, что и DLL)
// 2) На вкладке Отладка ставим галку Разрешить отладку неуправляемого кода
//
// После этого можно дедлать пошаговую отладку приложения с заходом в DLL.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;       // для DllImport native C++


namespace VideoAnnotationAPP
{
    public partial class Form1 : Form
    {
        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int subtract(int a, int b);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int add(int a, int b);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 load_image(out IntPtr ret);

        [DllImport("OcvWrapperMfcDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 release_image();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            int y = Convert.ToInt32(textBox2.Text);
            int z = add(x, y);
            MessageBox.Show("Required Answer is " + Convert.ToString(z), "Answer",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            int y = Convert.ToInt32(textBox2.Text);
            int z = subtract(x, y);
            MessageBox.Show("Required Answer is " + Convert.ToString(z), "Answer", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            IntPtr ret;
            Int32 result = load_image(out ret);
            Bitmap b = Image.FromHbitmap(ret);
            pictureBox1.Image = b;
            //b.Dispose();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            release_image();
            //Marshal.FreeHGlobal(ret);
            //Marshal.FreeCoTaskMem(ret);
            
        }
    }
}
