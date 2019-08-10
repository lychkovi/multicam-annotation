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
            this.lblRecordingFile = new System.Windows.Forms.Label();
            this.lblMarkupFile = new System.Windows.Forms.Label();
            this.txtRecordingFile = new System.Windows.Forms.TextBox();
            this.txtMarkupFile = new System.Windows.Forms.TextBox();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.pnlFilesOpened = new System.Windows.Forms.Panel();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.grpTracking = new System.Windows.Forms.GroupBox();
            this.grpNavigation = new System.Windows.Forms.GroupBox();
            this.pnlAttributes = new System.Windows.Forms.Panel();
            this.grpAttributes = new System.Windows.Forms.GroupBox();
            this.pnlTimeLine = new System.Windows.Forms.Panel();
            this.tbrTimeLine = new System.Windows.Forms.TrackBar();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTraceCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTraceDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.pnlFilesOpened.SuspendLayout();
            this.pnlTools.SuspendLayout();
            this.pnlAttributes.SuspendLayout();
            this.pnlTimeLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrTimeLine)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(746, 24);
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
            this.menuMarkupOpen.Click += new System.EventHandler(this.menuMarkupOpen_Click);
            // 
            // menuMarkupSave
            // 
            this.menuMarkupSave.Name = "menuMarkupSave";
            this.menuMarkupSave.Size = new System.Drawing.Size(158, 22);
            this.menuMarkupSave.Text = "Markup Save";
            this.menuMarkupSave.Click += new System.EventHandler(this.menuMarkupSave_Click);
            // 
            // menuMarkupSaveAd
            // 
            this.menuMarkupSaveAd.Name = "menuMarkupSaveAd";
            this.menuMarkupSaveAd.Size = new System.Drawing.Size(158, 22);
            this.menuMarkupSaveAd.Text = "Markup Save As";
            this.menuMarkupSaveAd.Click += new System.EventHandler(this.menuMarkupSaveAd_Click);
            // 
            // menuMarkupClose
            // 
            this.menuMarkupClose.Name = "menuMarkupClose";
            this.menuMarkupClose.Size = new System.Drawing.Size(158, 22);
            this.menuMarkupClose.Text = "Markup Close";
            this.menuMarkupClose.Click += new System.EventHandler(this.menuMarkupClose_Click);
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
            this.statusStrip.Size = new System.Drawing.Size(746, 22);
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
            // lblRecordingFile
            // 
            this.lblRecordingFile.AutoSize = true;
            this.lblRecordingFile.Location = new System.Drawing.Point(12, 15);
            this.lblRecordingFile.Name = "lblRecordingFile";
            this.lblRecordingFile.Size = new System.Drawing.Size(75, 13);
            this.lblRecordingFile.TabIndex = 2;
            this.lblRecordingFile.Text = "Recording File";
            // 
            // lblMarkupFile
            // 
            this.lblMarkupFile.AutoSize = true;
            this.lblMarkupFile.Location = new System.Drawing.Point(12, 41);
            this.lblMarkupFile.Name = "lblMarkupFile";
            this.lblMarkupFile.Size = new System.Drawing.Size(62, 13);
            this.lblMarkupFile.TabIndex = 3;
            this.lblMarkupFile.Text = "Markup File";
            // 
            // txtRecordingFile
            // 
            this.txtRecordingFile.Location = new System.Drawing.Point(93, 12);
            this.txtRecordingFile.Name = "txtRecordingFile";
            this.txtRecordingFile.ReadOnly = true;
            this.txtRecordingFile.Size = new System.Drawing.Size(640, 20);
            this.txtRecordingFile.TabIndex = 4;
            // 
            // txtMarkupFile
            // 
            this.txtMarkupFile.Location = new System.Drawing.Point(93, 38);
            this.txtMarkupFile.Name = "txtMarkupFile";
            this.txtMarkupFile.ReadOnly = true;
            this.txtMarkupFile.Size = new System.Drawing.Size(640, 20);
            this.txtMarkupFile.TabIndex = 5;
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDisplay.Location = new System.Drawing.Point(154, 94);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(438, 320);
            this.pnlDisplay.TabIndex = 6;
            // 
            // pnlFilesOpened
            // 
            this.pnlFilesOpened.Controls.Add(this.lblRecordingFile);
            this.pnlFilesOpened.Controls.Add(this.lblMarkupFile);
            this.pnlFilesOpened.Controls.Add(this.txtMarkupFile);
            this.pnlFilesOpened.Controls.Add(this.txtRecordingFile);
            this.pnlFilesOpened.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilesOpened.Location = new System.Drawing.Point(0, 24);
            this.pnlFilesOpened.Name = "pnlFilesOpened";
            this.pnlFilesOpened.Size = new System.Drawing.Size(746, 70);
            this.pnlFilesOpened.TabIndex = 7;
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.grpTracking);
            this.pnlTools.Controls.Add(this.grpNavigation);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTools.Location = new System.Drawing.Point(0, 94);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(154, 369);
            this.pnlTools.TabIndex = 8;
            // 
            // grpTracking
            // 
            this.grpTracking.Location = new System.Drawing.Point(4, 140);
            this.grpTracking.Name = "grpTracking";
            this.grpTracking.Size = new System.Drawing.Size(147, 226);
            this.grpTracking.TabIndex = 1;
            this.grpTracking.TabStop = false;
            this.grpTracking.Text = "Tracking";
            // 
            // grpNavigation
            // 
            this.grpNavigation.Location = new System.Drawing.Point(4, 7);
            this.grpNavigation.Name = "grpNavigation";
            this.grpNavigation.Size = new System.Drawing.Size(147, 127);
            this.grpNavigation.TabIndex = 0;
            this.grpNavigation.TabStop = false;
            this.grpNavigation.Text = "Navigation";
            // 
            // pnlAttributes
            // 
            this.pnlAttributes.Controls.Add(this.grpAttributes);
            this.pnlAttributes.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAttributes.Location = new System.Drawing.Point(592, 94);
            this.pnlAttributes.Name = "pnlAttributes";
            this.pnlAttributes.Size = new System.Drawing.Size(154, 369);
            this.pnlAttributes.TabIndex = 9;
            // 
            // grpAttributes
            // 
            this.grpAttributes.Location = new System.Drawing.Point(4, 4);
            this.grpAttributes.Name = "grpAttributes";
            this.grpAttributes.Size = new System.Drawing.Size(147, 362);
            this.grpAttributes.TabIndex = 0;
            this.grpAttributes.TabStop = false;
            this.grpAttributes.Text = "Attributes";
            // 
            // pnlTimeLine
            // 
            this.pnlTimeLine.Controls.Add(this.tbrTimeLine);
            this.pnlTimeLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTimeLine.Location = new System.Drawing.Point(154, 414);
            this.pnlTimeLine.Name = "pnlTimeLine";
            this.pnlTimeLine.Size = new System.Drawing.Size(438, 49);
            this.pnlTimeLine.TabIndex = 10;
            // 
            // tbrTimeLine
            // 
            this.tbrTimeLine.Location = new System.Drawing.Point(3, 4);
            this.tbrTimeLine.Name = "tbrTimeLine";
            this.tbrTimeLine.Size = new System.Drawing.Size(428, 42);
            this.tbrTimeLine.TabIndex = 0;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTraceCreate,
            this.menuTraceDelete});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // menuTraceCreate
            // 
            this.menuTraceCreate.Name = "menuTraceCreate";
            this.menuTraceCreate.Size = new System.Drawing.Size(152, 22);
            this.menuTraceCreate.Text = "Trace Create";
            this.menuTraceCreate.Click += new System.EventHandler(this.menuTraceCreate_Click);
            // 
            // menuTraceDelete
            // 
            this.menuTraceDelete.Name = "menuTraceDelete";
            this.menuTraceDelete.Size = new System.Drawing.Size(152, 22);
            this.menuTraceDelete.Text = "Trace Delete";
            this.menuTraceDelete.Click += new System.EventHandler(this.menuTraceDelete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 485);
            this.Controls.Add(this.pnlDisplay);
            this.Controls.Add(this.pnlTimeLine);
            this.Controls.Add(this.pnlAttributes);
            this.Controls.Add(this.pnlTools);
            this.Controls.Add(this.pnlFilesOpened);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(754, 512);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pnlFilesOpened.ResumeLayout(false);
            this.pnlFilesOpened.PerformLayout();
            this.pnlTools.ResumeLayout(false);
            this.pnlAttributes.ResumeLayout(false);
            this.pnlTimeLine.ResumeLayout(false);
            this.pnlTimeLine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrTimeLine)).EndInit();
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
        private System.Windows.Forms.Label lblRecordingFile;
        private System.Windows.Forms.Label lblMarkupFile;
        private System.Windows.Forms.TextBox txtRecordingFile;
        private System.Windows.Forms.TextBox txtMarkupFile;
        private System.Windows.Forms.Panel pnlDisplay;
        private System.Windows.Forms.Panel pnlFilesOpened;
        private System.Windows.Forms.Panel pnlTools;
        private System.Windows.Forms.GroupBox grpTracking;
        private System.Windows.Forms.GroupBox grpNavigation;
        private System.Windows.Forms.Panel pnlAttributes;
        private System.Windows.Forms.GroupBox grpAttributes;
        private System.Windows.Forms.Panel pnlTimeLine;
        private System.Windows.Forms.TrackBar tbrTimeLine;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuTraceCreate;
        private System.Windows.Forms.ToolStripMenuItem menuTraceDelete;
    }
}

