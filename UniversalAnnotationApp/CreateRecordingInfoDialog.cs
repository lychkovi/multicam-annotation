/* CreateRecordingInfoDialog: Форма, которая должна вызываться в качестве
 * диалогового окна при создании XML-файла описания видеозаписи.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace UniversalAnnotationApp
{
    public partial class CreateRecordingInfoDialog : Form
    {
        private TextBox[] TextBoxArray;
        private Button[] ButtonArray;

        // Количество камер в видеозаписи
        public int ViewsCount
        {
            get
            {
                return Math.Min(cmbNumberOfViews.SelectedIndex + 1,
                    TextBoxArray.Length);
            }
        }

        // Список путей к видеофайлам отдельных камер
        public string[] FilePathArray
        {
            get
            {
                string[] Array = new string[ViewsCount];
                for (int i = 0; i < ViewsCount; i++)
                    Array[i] = TextBoxArray[i].Text;
                return Array;
            }
        }

        // Комментарий к видеозаписи
        public string Comment
        {
            get
            {
                return txtComment.Text;
            }
        }

        public CreateRecordingInfoDialog()
        {
            InitializeComponent();

            // Сохраняем ссылки на элементы управления формы в массивы
            TextBoxArray = new TextBox[] { textBox1, textBox2, textBox3, 
                textBox4, textBox5, textBox6, textBox7, textBox8 };
            ButtonArray = new Button[] { button1, button2, button3, 
                button4, button5, button6, button7, button8 };

            // Выбираем вариант видеозапись из одного вида
            cmbNumberOfViews.SelectedIndex = 0;
        }

        /* Общий обработчик нажатия на кнопки во всех строках таблицы. */
        private void buttonXX_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;

                // Ищем в массиве кнопку, вызвавшую событие
                bool found = false;
                int index = 0;
                while (!found && index < ButtonArray.Length)
                {
                    if (button.Equals(ButtonArray[index]))
                        found = true;
                    else
                        ++index;
                }

                if (found)
                {
                    // Открываем диалог открытия файла
                    DialogResult result = dlgOpenFile.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        TextBoxArray[index].Text = dlgOpenFile.FileName;
                    }
                }
            }
        }

        /* Обработчик изменения состояния выпадающего списка для выбора 
         * количества видов видеозаписи. */
        private void cmbNumberOfViews_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            // Соответственно активируем необходимое количество строк таблицы
            for (int i = 0; i < ViewsCount; i++)
            {
                TextBoxArray[i].Visible = true;
                ButtonArray[i].Enabled = true;
            }

            // Остальные строки таблицы деактивируем
            for (int i = ViewsCount; i < TextBoxArray.Length; i++)
            {
                TextBoxArray[i].Text = "";
                TextBoxArray[i].Visible = false;
                ButtonArray[i].Enabled = false;
            }
        }

        /* Обработчик закрытия формы при нажатии на кнопку OK окна. */
        private void CreateRecordingInfoDialog_FormClosing(
            object sender, FormClosingEventArgs e)
        {
            // Если пользователь нажзал кнопку Отмена, данные окна не
            // учитываются в основной программе. 
            if (DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            // Проверяем, что указаны пути ко всем необходимым видеофайлам
            bool isValid = true;
            for (int i = 0; i < ViewsCount; i++)
            {
                if (TextBoxArray[i].Text == "")
                    isValid = false;
            }
            if (!isValid)
            {
                MessageBox.Show("Please, specify file paths for all views!",
                    "ERROR!", MessageBoxButtons.OK);
                e.Cancel = true;
            }
        }
    }
}
