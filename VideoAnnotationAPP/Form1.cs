// Form1.cs: Главная форма приложения для разметки движущихся объектов в кадрах видеопотока.
// https://www.c-sharpcorner.com/uploadfile/tanmayit08/unmanaged-cpp-dll-call-from-managed-C-Sharp-application/
//
// Создаем решение из двух проектов: 
// 1) Visual C++ >> MFC > Библиотека DLL MFC
// 2) C# Windows Forms Application
//
// В настройках проекта C# 
// 1) На вкладке Построение указываем Путь выхода: 
// ..\Debug\ (чтобы исполняемый файл находился в той же директории, что и DLL)
// 2) На вкладке Отладка ставим галку Разрешить отладку неуправляемого кода
//
// В настройках библиотеки DLL MFC На вкладке Общие
// 1) Использование MFC выставляем в Использовать MFC в статической библиотеке,
// иначе будут утечки памяти. 
// 2) Набор символов выставляем в Использовать много байтную кодировку, 
// инче будут проблемы с обработкой строк системными библиотеками.
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


namespace VideoAnnotationAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(txtNumber1.Text);
            int y = Convert.ToInt32(txtNumber2.Text);
            int z = OcvWrapper.TestAdd(x, y);
            MessageBox.Show("Required Answer is " + Convert.ToString(z), "Answer",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(txtNumber1.Text);
            int y = Convert.ToInt32(txtNumber2.Text);
            int z = OcvWrapper.TestSubtract(x, y);
            MessageBox.Show("Required Answer is " + Convert.ToString(z), "Answer", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void btnAppendList_Click(object sender, EventArgs e)
        {
            lstInputValues.Items.Add(txtNewValue.Text);
        }

        private void btnProcessList_Click(object sender, EventArgs e)
        {
            int inputValuesCount, outputValuesCount;
            double[] inputValues, outputValues;

            inputValuesCount = lstInputValues.Items.Count;
            inputValues = new double[inputValuesCount];
            for (int i = 0; i < inputValuesCount; i++)
            {
                inputValues[i] = Double.Parse(lstInputValues.Items[i].ToString());
            }

            OcvWrapper.TestAddArray(inputValues, inputValuesCount, out outputValues, out outputValuesCount);
            for (int i = 0; i < outputValuesCount; i++)
            {
                lstOutputValues.Items.Add(outputValues[i].ToString());
            }
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            IntPtr hBitmap;
            String filepath = "..\\Data\\" + txtInputFile.Text;
            int errcode = OcvWrapper.ImageLoad(filepath, out hBitmap);
            if (errcode == 0)
            {
                pictureBox1.Image = Image.FromHbitmap(hBitmap);
            }
            else
            {
                MessageBox.Show("Unable to load input image!", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            IntPtr hBitmap;
            Bitmap bmp;
            String filepath = "..\\Data\\" + txtOutputFile.Text;

            bmp = new Bitmap(pictureBox1.Image);
            hBitmap = bmp.GetHbitmap();
            int errcode = OcvWrapper.ImageSave(filepath, hBitmap);
        }
    }
}
