namespace UniversalAnnotationApp
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRecordingOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRecordingCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRecordingClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMarkupOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMarkupSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMarkupSaveAd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMarkupClose = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusVideo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusMarkup = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.dlgRecordingSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgRecordingOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgMarkupSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgMarkupOpen = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(745, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRecordingOpen,
            this.menuRecordingCreate,
            this.menuRecordingClose,
            this.menuMarkupOpen,
            this.menuMarkupSave,
            this.menuMarkupSaveAd,
            this.menuMarkupClose});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuRecordingOpen
            // 
            this.menuRecordingOpen.Name = "menuRecordingOpen";
            this.menuRecordingOpen.Size = new System.Drawing.Size(158, 22);
            this.menuRecordingOpen.Text = "Recording Open";
            this.menuRecordingOpen.Click += new System.EventHandler(this.menuRecordingOpen_Click);
            // 
            // menuRecordingCreate
            // 
            this.menuRecordingCreate.Name = "menuRecordingCreate";
            this.menuRecordingCreate.Size = new System.Drawing.Size(158, 22);
            this.menuRecordingCreate.Text = "Recording Create";
            this.menuRecordingCreate.Click += new System.EventHandler(this.menuRecordingCreate_Click);
            // 
            // menuRecordingClose
            // 
            this.menuRecordingClose.Name = "menuRecordingClose";
            this.menuRecordingClose.Size = new System.Drawing.Size(158, 22);
            this.menuRecordingClose.Text = "Recording Close";
            this.menuRecordingClose.Click += new System.EventHandler(this.menuRecordingClose_Click);
            // 
            // menuMarkupOpen
            // 
            this.menuMarkupOpen.Name = "menuMarkupOpen";
            this.menuMarkupOpen.Size = new System.Drawing.Size(158, 22);
            this.menuMarkupOpen.Text = "Markup Open";
            // 
            // menuMarkupSave
            // 
            this.menuMarkupSave.Name = "menuMarkupSave";
            this.menuMarkupSave.Size = new System.Drawing.Size(158, 22);
            this.menuMarkupSave.Text = "Markup Save";
            // 
            // menuMarkupSaveAd
            // 
            this.menuMarkupSaveAd.Name = "menuMarkupSaveAd";
            this.menuMarkupSaveAd.Size = new System.Drawing.Size(158, 22);
            this.menuMarkupSaveAd.Text = "Markup Save As";
            // 
            // menuMarkupClose
            // 
            this.menuMarkupClose.Name = "menuMarkupClose";
            this.menuMarkupClose.Size = new System.Drawing.Size(158, 22);
            this.menuMarkupClose.Text = "Markup Close";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusVideo,
            this.statusMarkup,
            this.statusMode,
            this.statusAction});
            this.statusStrip.Location = new System.Drawing.Point(0, 463);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(745, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusVideo
            // 
            this.statusVideo.AutoSize = false;
            this.statusVideo.Name = "statusVideo";
            this.statusVideo.Size = new System.Drawing.Size(200, 17);
            this.statusVideo.Text = "No Video";
            // 
            // statusMarkup
            // 
            this.statusMarkup.AutoSize = false;
            this.statusMarkup.Name = "statusMarkup";
            this.statusMarkup.Size = new System.Drawing.Size(200, 17);
            this.statusMarkup.Text = "Markup Unsaved";
            // 
            // statusMode
            // 
            this.statusMode.AutoSize = false;
            this.statusMode.Name = "statusMode";
            this.statusMode.Size = new System.Drawing.Size(200, 17);
            this.statusMode.Text = "Trace Create";
            // 
            // statusAction
            // 
            this.statusAction.AutoSize = false;
            this.statusAction.Name = "statusAction";
            this.statusAction.Size = new System.Drawing.Size(109, 17);
            this.statusAction.Text = "Enter First Corner";
            // 
            // dlgRecordingSave
            // 
            this.dlgRecordingSave.Filter = "XML files|*.xml";
            // 
            // dlgRecordingOpen
            // 
            this.dlgRecordingOpen.FileName = "openFileDialog1";
            this.dlgRecordingOpen.Filter = "XML files|*.xml";
            // 
            // dlgMarkupSave
            // 
            this.dlgMarkupSave.Filter = "XML files|*.xml";
            // 
            // dlgMarkupOpen
            // 
            this.dlgMarkupOpen.FileName = "openFileDialog1";
            this.dlgMarkupOpen.Filter = "XML files|*.xml";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 485);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusVideo;
        private System.Windows.Forms.ToolStripStatusLabel statusMarkup;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuRecordingOpen;
        private System.Windows.Forms.ToolStripMenuItem menuMarkupOpen;
        private System.Windows.Forms.ToolStripMenuItem menuMarkupSave;
        private System.Windows.Forms.ToolStripMenuItem menuMarkupSaveAd;
        private System.Windows.Forms.ToolStripMenuItem menuMarkupClose;
        private System.Windows.Forms.ToolStripStatusLabel statusMode;
        private System.Windows.Forms.ToolStripStatusLabel statusAction;
        private System.Windows.Forms.ToolStripMenuItem menuRecordingCreate;
        private System.Windows.Forms.ToolStripMenuItem menuRecordingClose;
        private System.Windows.Forms.SaveFileDialog dlgRecordingSave;
        private System.Windows.Forms.OpenFileDialog dlgRecordingOpen;
        private System.Windows.Forms.SaveFileDialog dlgMarkupSave;
        private System.Windows.Forms.OpenFileDialog dlgMarkupOpen;
    }
}

