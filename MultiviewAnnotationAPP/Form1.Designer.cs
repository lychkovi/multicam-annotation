namespace MultiviewAnnotationAPP
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
            this.grpVideoNavigator = new System.Windows.Forms.GroupBox();
            this.txtVideoPositionMs = new System.Windows.Forms.TextBox();
            this.lblVideoPositionMs = new System.Windows.Forms.Label();
            this.btnNextFrame = new System.Windows.Forms.Button();
            this.btnPreviousFrame = new System.Windows.Forms.Button();
            this.txtVideoFrameCurrent = new System.Windows.Forms.TextBox();
            this.lblCurrentFrame = new System.Windows.Forms.Label();
            this.txtGoToFrame = new System.Windows.Forms.TextBox();
            this.btnGoToFrame = new System.Windows.Forms.Button();
            this.txtVideoDurationMs = new System.Windows.Forms.TextBox();
            this.lblVideoDurationMs = new System.Windows.Forms.Label();
            this.txtVideoFps = new System.Windows.Forms.TextBox();
            this.lblVideoFps = new System.Windows.Forms.Label();
            this.txtVideoFramesTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusVideo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusMarkup = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVideoOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMarkupOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMarkupSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMarkupSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMarkupClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVideoClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTraceCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.tbrVideoSlider = new System.Windows.Forms.TrackBar();
            this.txtMarkupFilePath = new System.Windows.Forms.TextBox();
            this.lblMarkupFilePath = new System.Windows.Forms.Label();
            this.txtVideoFilePath = new System.Windows.Forms.TextBox();
            this.lblVideoFilePath = new System.Windows.Forms.Label();
            this.grpVideoNavigator.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrVideoSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // grpVideoNavigator
            // 
            this.grpVideoNavigator.Controls.Add(this.txtVideoPositionMs);
            this.grpVideoNavigator.Controls.Add(this.lblVideoPositionMs);
            this.grpVideoNavigator.Controls.Add(this.btnNextFrame);
            this.grpVideoNavigator.Controls.Add(this.btnPreviousFrame);
            this.grpVideoNavigator.Controls.Add(this.txtVideoFrameCurrent);
            this.grpVideoNavigator.Controls.Add(this.lblCurrentFrame);
            this.grpVideoNavigator.Controls.Add(this.txtGoToFrame);
            this.grpVideoNavigator.Controls.Add(this.btnGoToFrame);
            this.grpVideoNavigator.Controls.Add(this.txtVideoDurationMs);
            this.grpVideoNavigator.Controls.Add(this.lblVideoDurationMs);
            this.grpVideoNavigator.Controls.Add(this.txtVideoFps);
            this.grpVideoNavigator.Controls.Add(this.lblVideoFps);
            this.grpVideoNavigator.Controls.Add(this.txtVideoFramesTotal);
            this.grpVideoNavigator.Controls.Add(this.label1);
            this.grpVideoNavigator.Location = new System.Drawing.Point(12, 80);
            this.grpVideoNavigator.Name = "grpVideoNavigator";
            this.grpVideoNavigator.Size = new System.Drawing.Size(217, 231);
            this.grpVideoNavigator.TabIndex = 5;
            this.grpVideoNavigator.TabStop = false;
            this.grpVideoNavigator.Text = "Video";
            // 
            // txtVideoPositionMs
            // 
            this.txtVideoPositionMs.Location = new System.Drawing.Point(119, 103);
            this.txtVideoPositionMs.Name = "txtVideoPositionMs";
            this.txtVideoPositionMs.ReadOnly = true;
            this.txtVideoPositionMs.Size = new System.Drawing.Size(79, 20);
            this.txtVideoPositionMs.TabIndex = 16;
            // 
            // lblVideoPositionMs
            // 
            this.lblVideoPositionMs.AutoSize = true;
            this.lblVideoPositionMs.Location = new System.Drawing.Point(11, 106);
            this.lblVideoPositionMs.Name = "lblVideoPositionMs";
            this.lblVideoPositionMs.Size = new System.Drawing.Size(74, 13);
            this.lblVideoPositionMs.TabIndex = 15;
            this.lblVideoPositionMs.Text = "Video Position";
            // 
            // btnNextFrame
            // 
            this.btnNextFrame.Location = new System.Drawing.Point(113, 186);
            this.btnNextFrame.Name = "btnNextFrame";
            this.btnNextFrame.Size = new System.Drawing.Size(85, 23);
            this.btnNextFrame.TabIndex = 14;
            this.btnNextFrame.Text = "Next Frame";
            this.btnNextFrame.UseVisualStyleBackColor = true;
            // 
            // btnPreviousFrame
            // 
            this.btnPreviousFrame.Location = new System.Drawing.Point(9, 186);
            this.btnPreviousFrame.Name = "btnPreviousFrame";
            this.btnPreviousFrame.Size = new System.Drawing.Size(88, 23);
            this.btnPreviousFrame.TabIndex = 13;
            this.btnPreviousFrame.Text = "Previous Frame";
            this.btnPreviousFrame.UseVisualStyleBackColor = true;
            // 
            // txtVideoFrameCurrent
            // 
            this.txtVideoFrameCurrent.Location = new System.Drawing.Point(119, 160);
            this.txtVideoFrameCurrent.Name = "txtVideoFrameCurrent";
            this.txtVideoFrameCurrent.ReadOnly = true;
            this.txtVideoFrameCurrent.Size = new System.Drawing.Size(79, 20);
            this.txtVideoFrameCurrent.TabIndex = 12;
            // 
            // lblCurrentFrame
            // 
            this.lblCurrentFrame.AutoSize = true;
            this.lblCurrentFrame.Location = new System.Drawing.Point(11, 163);
            this.lblCurrentFrame.Name = "lblCurrentFrame";
            this.lblCurrentFrame.Size = new System.Drawing.Size(73, 13);
            this.lblCurrentFrame.TabIndex = 11;
            this.lblCurrentFrame.Text = "Current Frame";
            // 
            // txtGoToFrame
            // 
            this.txtGoToFrame.Location = new System.Drawing.Point(119, 132);
            this.txtGoToFrame.Name = "txtGoToFrame";
            this.txtGoToFrame.Size = new System.Drawing.Size(79, 20);
            this.txtGoToFrame.TabIndex = 10;
            // 
            // btnGoToFrame
            // 
            this.btnGoToFrame.Location = new System.Drawing.Point(9, 130);
            this.btnGoToFrame.Name = "btnGoToFrame";
            this.btnGoToFrame.Size = new System.Drawing.Size(75, 23);
            this.btnGoToFrame.TabIndex = 9;
            this.btnGoToFrame.Text = "GoTo Frame";
            this.btnGoToFrame.UseVisualStyleBackColor = true;
            this.btnGoToFrame.Click += new System.EventHandler(this.btnGoToFrame_Click);
            // 
            // txtVideoDurationMs
            // 
            this.txtVideoDurationMs.Location = new System.Drawing.Point(119, 76);
            this.txtVideoDurationMs.Name = "txtVideoDurationMs";
            this.txtVideoDurationMs.ReadOnly = true;
            this.txtVideoDurationMs.Size = new System.Drawing.Size(79, 20);
            this.txtVideoDurationMs.TabIndex = 8;
            // 
            // lblVideoDurationMs
            // 
            this.lblVideoDurationMs.AutoSize = true;
            this.lblVideoDurationMs.Location = new System.Drawing.Point(11, 79);
            this.lblVideoDurationMs.Name = "lblVideoDurationMs";
            this.lblVideoDurationMs.Size = new System.Drawing.Size(77, 13);
            this.lblVideoDurationMs.TabIndex = 7;
            this.lblVideoDurationMs.Text = "Video Duration";
            // 
            // txtVideoFps
            // 
            this.txtVideoFps.Location = new System.Drawing.Point(119, 50);
            this.txtVideoFps.Name = "txtVideoFps";
            this.txtVideoFps.ReadOnly = true;
            this.txtVideoFps.Size = new System.Drawing.Size(79, 20);
            this.txtVideoFps.TabIndex = 6;
            // 
            // lblVideoFps
            // 
            this.lblVideoFps.AutoSize = true;
            this.lblVideoFps.Location = new System.Drawing.Point(10, 53);
            this.lblVideoFps.Name = "lblVideoFps";
            this.lblVideoFps.Size = new System.Drawing.Size(57, 13);
            this.lblVideoFps.TabIndex = 5;
            this.lblVideoFps.Text = "Video FPS";
            // 
            // txtVideoFramesTotal
            // 
            this.txtVideoFramesTotal.Location = new System.Drawing.Point(119, 24);
            this.txtVideoFramesTotal.Name = "txtVideoFramesTotal";
            this.txtVideoFramesTotal.ReadOnly = true;
            this.txtVideoFramesTotal.Size = new System.Drawing.Size(79, 20);
            this.txtVideoFramesTotal.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Video Frames Total";
            // 
            // statusStrip
            // 
            this.statusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusVideo,
            this.statusMarkup,
            this.statusMode,
            this.statusAction});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 489);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(953, 22);
            this.statusStrip.TabIndex = 11;
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
            this.statusMode.Text = "Create Trace";
            // 
            // statusAction
            // 
            this.statusAction.AutoSize = false;
            this.statusAction.Name = "statusAction";
            this.statusAction.Size = new System.Drawing.Size(200, 17);
            this.statusAction.Text = "Enter First Corner";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(953, 24);
            this.menuStrip.TabIndex = 12;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuVideoOpen,
            this.menuMarkupOpen,
            this.menuMarkupSave,
            this.menuMarkupSaveAs,
            this.menuMarkupClose,
            this.menuVideoClose,
            this.menuQuit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuVideoOpen
            // 
            this.menuVideoOpen.Name = "menuVideoOpen";
            this.menuVideoOpen.Size = new System.Drawing.Size(163, 22);
            this.menuVideoOpen.Text = "Open Video...";
            // 
            // menuMarkupOpen
            // 
            this.menuMarkupOpen.Name = "menuMarkupOpen";
            this.menuMarkupOpen.Size = new System.Drawing.Size(163, 22);
            this.menuMarkupOpen.Text = "Open Markup...";
            // 
            // menuMarkupSave
            // 
            this.menuMarkupSave.Name = "menuMarkupSave";
            this.menuMarkupSave.Size = new System.Drawing.Size(163, 22);
            this.menuMarkupSave.Text = "Save Markup";
            // 
            // menuMarkupSaveAs
            // 
            this.menuMarkupSaveAs.Name = "menuMarkupSaveAs";
            this.menuMarkupSaveAs.Size = new System.Drawing.Size(163, 22);
            this.menuMarkupSaveAs.Text = "Save Markup As...";
            // 
            // menuMarkupClose
            // 
            this.menuMarkupClose.Name = "menuMarkupClose";
            this.menuMarkupClose.Size = new System.Drawing.Size(163, 22);
            this.menuMarkupClose.Text = "Close Markup";
            // 
            // menuVideoClose
            // 
            this.menuVideoClose.Name = "menuVideoClose";
            this.menuVideoClose.Size = new System.Drawing.Size(163, 22);
            this.menuVideoClose.Text = "Close Video";
            // 
            // menuQuit
            // 
            this.menuQuit.Name = "menuQuit";
            this.menuQuit.Size = new System.Drawing.Size(163, 22);
            this.menuQuit.Text = "Quit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTraceCreate});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // menuTraceCreate
            // 
            this.menuTraceCreate.Name = "menuTraceCreate";
            this.menuTraceCreate.Size = new System.Drawing.Size(137, 22);
            this.menuTraceCreate.Text = "Trace Create";
            // 
            // tbrVideoSlider
            // 
            this.tbrVideoSlider.Location = new System.Drawing.Point(12, 435);
            this.tbrVideoSlider.Name = "tbrVideoSlider";
            this.tbrVideoSlider.Size = new System.Drawing.Size(708, 42);
            this.tbrVideoSlider.TabIndex = 13;
            // 
            // txtMarkupFilePath
            // 
            this.txtMarkupFilePath.Location = new System.Drawing.Point(112, 55);
            this.txtMarkupFilePath.Name = "txtMarkupFilePath";
            this.txtMarkupFilePath.ReadOnly = true;
            this.txtMarkupFilePath.Size = new System.Drawing.Size(834, 20);
            this.txtMarkupFilePath.TabIndex = 17;
            // 
            // lblMarkupFilePath
            // 
            this.lblMarkupFilePath.AutoSize = true;
            this.lblMarkupFilePath.Location = new System.Drawing.Point(15, 58);
            this.lblMarkupFilePath.Name = "lblMarkupFilePath";
            this.lblMarkupFilePath.Size = new System.Drawing.Size(87, 13);
            this.lblMarkupFilePath.TabIndex = 16;
            this.lblMarkupFilePath.Text = "Markup File Path";
            // 
            // txtVideoFilePath
            // 
            this.txtVideoFilePath.Location = new System.Drawing.Point(112, 27);
            this.txtVideoFilePath.Name = "txtVideoFilePath";
            this.txtVideoFilePath.ReadOnly = true;
            this.txtVideoFilePath.Size = new System.Drawing.Size(834, 20);
            this.txtVideoFilePath.TabIndex = 15;
            // 
            // lblVideoFilePath
            // 
            this.lblVideoFilePath.AutoSize = true;
            this.lblVideoFilePath.Location = new System.Drawing.Point(15, 30);
            this.lblVideoFilePath.Name = "lblVideoFilePath";
            this.lblVideoFilePath.Size = new System.Drawing.Size(78, 13);
            this.lblVideoFilePath.TabIndex = 14;
            this.lblVideoFilePath.Text = "Video File Path";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 511);
            this.Controls.Add(this.txtMarkupFilePath);
            this.Controls.Add(this.lblMarkupFilePath);
            this.Controls.Add(this.txtVideoFilePath);
            this.Controls.Add(this.lblVideoFilePath);
            this.Controls.Add(this.tbrVideoSlider);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.grpVideoNavigator);
            this.Name = "Form1";
            this.Text = "Form1";
            this.grpVideoNavigator.ResumeLayout(false);
            this.grpVideoNavigator.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrVideoSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpVideoNavigator;
        private System.Windows.Forms.TextBox txtVideoPositionMs;
        private System.Windows.Forms.Label lblVideoPositionMs;
        private System.Windows.Forms.Button btnNextFrame;
        private System.Windows.Forms.Button btnPreviousFrame;
        private System.Windows.Forms.TextBox txtVideoFrameCurrent;
        private System.Windows.Forms.Label lblCurrentFrame;
        private System.Windows.Forms.TextBox txtGoToFrame;
        private System.Windows.Forms.Button btnGoToFrame;
        private System.Windows.Forms.TextBox txtVideoDurationMs;
        private System.Windows.Forms.Label lblVideoDurationMs;
        private System.Windows.Forms.TextBox txtVideoFps;
        private System.Windows.Forms.Label lblVideoFps;
        private System.Windows.Forms.TextBox txtVideoFramesTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusVideo;
        private System.Windows.Forms.ToolStripStatusLabel statusMarkup;
        private System.Windows.Forms.ToolStripStatusLabel statusMode;
        private System.Windows.Forms.ToolStripStatusLabel statusAction;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuVideoOpen;
        private System.Windows.Forms.ToolStripMenuItem menuMarkupOpen;
        private System.Windows.Forms.ToolStripMenuItem menuMarkupSave;
        private System.Windows.Forms.ToolStripMenuItem menuMarkupSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuMarkupClose;
        private System.Windows.Forms.ToolStripMenuItem menuVideoClose;
        private System.Windows.Forms.ToolStripMenuItem menuQuit;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuTraceCreate;
        private System.Windows.Forms.TrackBar tbrVideoSlider;
        private System.Windows.Forms.TextBox txtMarkupFilePath;
        private System.Windows.Forms.Label lblMarkupFilePath;
        private System.Windows.Forms.TextBox txtVideoFilePath;
        private System.Windows.Forms.Label lblVideoFilePath;
    }
}

