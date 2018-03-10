namespace VideoAnnotationAPP
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNumber1 = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtNumber2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubtract = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.lstInputValues = new System.Windows.Forms.ListBox();
            this.lstOutputValues = new System.Windows.Forms.ListBox();
            this.btnProcessList = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNewValue = new System.Windows.Forms.TextBox();
            this.btnAppendList = new System.Windows.Forms.Button();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNumber1
            // 
            this.txtNumber1.Location = new System.Drawing.Point(125, 34);
            this.txtNumber1.Name = "txtNumber1";
            this.txtNumber1.Size = new System.Drawing.Size(100, 20);
            this.txtNumber1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(38, 113);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtNumber2
            // 
            this.txtNumber2.Location = new System.Drawing.Point(125, 72);
            this.txtNumber2.Name = "txtNumber2";
            this.txtNumber2.Size = new System.Drawing.Size(100, 20);
            this.txtNumber2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Number 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Number 2";
            // 
            // btnSubtract
            // 
            this.btnSubtract.Location = new System.Drawing.Point(150, 112);
            this.btnSubtract.Name = "btnSubtract";
            this.btnSubtract.Size = new System.Drawing.Size(75, 23);
            this.btnSubtract.TabIndex = 5;
            this.btnSubtract.Text = "Subtract";
            this.btnSubtract.UseVisualStyleBackColor = true;
            this.btnSubtract.Click += new System.EventHandler(this.btnSubtract_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.Location = new System.Drawing.Point(289, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(424, 411);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(173, 377);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(52, 23);
            this.btnLoadImage.TabIndex = 7;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 382);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Input File";
            // 
            // txtInputFile
            // 
            this.txtInputFile.Location = new System.Drawing.Point(100, 379);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(60, 20);
            this.txtInputFile.TabIndex = 9;
            this.txtInputFile.Text = "lena.jpg";
            // 
            // lstInputValues
            // 
            this.lstInputValues.FormattingEnabled = true;
            this.lstInputValues.Location = new System.Drawing.Point(38, 211);
            this.lstInputValues.Name = "lstInputValues";
            this.lstInputValues.Size = new System.Drawing.Size(72, 108);
            this.lstInputValues.TabIndex = 10;
            // 
            // lstOutputValues
            // 
            this.lstOutputValues.FormattingEnabled = true;
            this.lstOutputValues.Location = new System.Drawing.Point(149, 211);
            this.lstOutputValues.Name = "lstOutputValues";
            this.lstOutputValues.Size = new System.Drawing.Size(76, 108);
            this.lstOutputValues.TabIndex = 11;
            // 
            // btnProcessList
            // 
            this.btnProcessList.Location = new System.Drawing.Point(150, 338);
            this.btnProcessList.Name = "btnProcessList";
            this.btnProcessList.Size = new System.Drawing.Size(75, 23);
            this.btnProcessList.TabIndex = 12;
            this.btnProcessList.Text = "Process List";
            this.btnProcessList.UseVisualStyleBackColor = true;
            this.btnProcessList.Click += new System.EventHandler(this.btnProcessList_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Input Values";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(151, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Output Values";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "New Value";
            // 
            // txtNewValue
            // 
            this.txtNewValue.Location = new System.Drawing.Point(100, 159);
            this.txtNewValue.Name = "txtNewValue";
            this.txtNewValue.Size = new System.Drawing.Size(60, 20);
            this.txtNewValue.TabIndex = 16;
            this.txtNewValue.Text = "0";
            // 
            // btnAppendList
            // 
            this.btnAppendList.Location = new System.Drawing.Point(173, 157);
            this.btnAppendList.Name = "btnAppendList";
            this.btnAppendList.Size = new System.Drawing.Size(52, 23);
            this.btnAppendList.TabIndex = 17;
            this.btnAppendList.Text = "Append";
            this.btnAppendList.UseVisualStyleBackColor = true;
            this.btnAppendList.Click += new System.EventHandler(this.btnAppendList_Click);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(173, 417);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(52, 23);
            this.btnSaveImage.TabIndex = 18;
            this.btnSaveImage.Text = "Save";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 422);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "OutputFile";
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(100, 419);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(60, 20);
            this.txtOutputFile.TabIndex = 20;
            this.txtOutputFile.Text = "lena.png";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 492);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSaveImage);
            this.Controls.Add(this.btnAppendList);
            this.Controls.Add(this.txtNewValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnProcessList);
            this.Controls.Add(this.lstOutputValues);
            this.Controls.Add(this.lstInputValues);
            this.Controls.Add(this.txtInputFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSubtract);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumber2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtNumber1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNumber1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtNumber2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSubtract;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.ListBox lstInputValues;
        private System.Windows.Forms.ListBox lstOutputValues;
        private System.Windows.Forms.Button btnProcessList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNewValue;
        private System.Windows.Forms.Button btnAppendList;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOutputFile;
    }
}

