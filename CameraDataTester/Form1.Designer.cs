namespace CameraDataTester
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
            this.btnVideoOpen = new System.Windows.Forms.Button();
            this.lblVideoFilePath = new System.Windows.Forms.Label();
            this.btnVideoGoToFrame = new System.Windows.Forms.Button();
            this.tbxVideoGoToFrame = new System.Windows.Forms.TextBox();
            this.lblVideoCurrentFrame = new System.Windows.Forms.Label();
            this.tbxVideoCurrentFrame = new System.Windows.Forms.TextBox();
            this.radVideoPlay = new System.Windows.Forms.RadioButton();
            this.radVideoStop = new System.Windows.Forms.RadioButton();
            this.btnVideoClose = new System.Windows.Forms.Button();
            this.picVideo = new System.Windows.Forms.PictureBox();
            this.pbrVideo = new System.Windows.Forms.ProgressBar();
            this.grpVideo = new System.Windows.Forms.GroupBox();
            this.lblVideoFramesCount = new System.Windows.Forms.Label();
            this.tbxVideoTotalFrames = new System.Windows.Forms.TextBox();
            this.lblVideoFps = new System.Windows.Forms.Label();
            this.tbxVideoFps = new System.Windows.Forms.TextBox();
            this.grpImages = new System.Windows.Forms.GroupBox();
            this.dlgVideoOpen = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picVideo)).BeginInit();
            this.grpVideo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnVideoOpen
            // 
            this.btnVideoOpen.Location = new System.Drawing.Point(19, 21);
            this.btnVideoOpen.Name = "btnVideoOpen";
            this.btnVideoOpen.Size = new System.Drawing.Size(102, 23);
            this.btnVideoOpen.TabIndex = 0;
            this.btnVideoOpen.Text = "Open Video";
            this.btnVideoOpen.UseVisualStyleBackColor = true;
            this.btnVideoOpen.Click += new System.EventHandler(this.btnVideoOpen_Click);
            // 
            // lblVideoFilePath
            // 
            this.lblVideoFilePath.AutoSize = true;
            this.lblVideoFilePath.Location = new System.Drawing.Point(142, 26);
            this.lblVideoFilePath.Name = "lblVideoFilePath";
            this.lblVideoFilePath.Size = new System.Drawing.Size(92, 13);
            this.lblVideoFilePath.TabIndex = 1;
            this.lblVideoFilePath.Text = "No Video Opened";
            // 
            // btnVideoGoToFrame
            // 
            this.btnVideoGoToFrame.Location = new System.Drawing.Point(19, 50);
            this.btnVideoGoToFrame.Name = "btnVideoGoToFrame";
            this.btnVideoGoToFrame.Size = new System.Drawing.Size(102, 23);
            this.btnVideoGoToFrame.TabIndex = 2;
            this.btnVideoGoToFrame.Text = "GoTo Frame";
            this.btnVideoGoToFrame.UseVisualStyleBackColor = true;
            this.btnVideoGoToFrame.Click += new System.EventHandler(this.btnVideoGoToFrame_Click);
            // 
            // tbxVideoGoToFrame
            // 
            this.tbxVideoGoToFrame.Location = new System.Drawing.Point(145, 52);
            this.tbxVideoGoToFrame.Name = "tbxVideoGoToFrame";
            this.tbxVideoGoToFrame.Size = new System.Drawing.Size(100, 20);
            this.tbxVideoGoToFrame.TabIndex = 3;
            // 
            // lblVideoCurrentFrame
            // 
            this.lblVideoCurrentFrame.AutoSize = true;
            this.lblVideoCurrentFrame.Location = new System.Drawing.Point(27, 81);
            this.lblVideoCurrentFrame.Name = "lblVideoCurrentFrame";
            this.lblVideoCurrentFrame.Size = new System.Drawing.Size(73, 13);
            this.lblVideoCurrentFrame.TabIndex = 4;
            this.lblVideoCurrentFrame.Text = "Current Frame";
            // 
            // tbxVideoCurrentFrame
            // 
            this.tbxVideoCurrentFrame.Location = new System.Drawing.Point(145, 78);
            this.tbxVideoCurrentFrame.Name = "tbxVideoCurrentFrame";
            this.tbxVideoCurrentFrame.ReadOnly = true;
            this.tbxVideoCurrentFrame.Size = new System.Drawing.Size(100, 20);
            this.tbxVideoCurrentFrame.TabIndex = 5;
            // 
            // radVideoPlay
            // 
            this.radVideoPlay.AutoSize = true;
            this.radVideoPlay.Location = new System.Drawing.Point(30, 166);
            this.radVideoPlay.Name = "radVideoPlay";
            this.radVideoPlay.Size = new System.Drawing.Size(45, 17);
            this.radVideoPlay.TabIndex = 6;
            this.radVideoPlay.Text = "Play";
            this.radVideoPlay.UseVisualStyleBackColor = true;
            // 
            // radVideoStop
            // 
            this.radVideoStop.AutoSize = true;
            this.radVideoStop.Checked = true;
            this.radVideoStop.Location = new System.Drawing.Point(95, 166);
            this.radVideoStop.Name = "radVideoStop";
            this.radVideoStop.Size = new System.Drawing.Size(47, 17);
            this.radVideoStop.TabIndex = 7;
            this.radVideoStop.TabStop = true;
            this.radVideoStop.Text = "Stop";
            this.radVideoStop.UseVisualStyleBackColor = true;
            // 
            // btnVideoClose
            // 
            this.btnVideoClose.Location = new System.Drawing.Point(19, 207);
            this.btnVideoClose.Name = "btnVideoClose";
            this.btnVideoClose.Size = new System.Drawing.Size(102, 23);
            this.btnVideoClose.TabIndex = 8;
            this.btnVideoClose.Text = "Close Video";
            this.btnVideoClose.UseVisualStyleBackColor = true;
            this.btnVideoClose.Click += new System.EventHandler(this.btnVideoClose_Click);
            // 
            // picVideo
            // 
            this.picVideo.Location = new System.Drawing.Point(316, 46);
            this.picVideo.Name = "picVideo";
            this.picVideo.Size = new System.Drawing.Size(392, 184);
            this.picVideo.TabIndex = 9;
            this.picVideo.TabStop = false;
            // 
            // pbrVideo
            // 
            this.pbrVideo.Location = new System.Drawing.Point(316, 26);
            this.pbrVideo.Name = "pbrVideo";
            this.pbrVideo.Size = new System.Drawing.Size(392, 13);
            this.pbrVideo.TabIndex = 10;
            // 
            // grpVideo
            // 
            this.grpVideo.Controls.Add(this.tbxVideoFps);
            this.grpVideo.Controls.Add(this.lblVideoFps);
            this.grpVideo.Controls.Add(this.tbxVideoTotalFrames);
            this.grpVideo.Controls.Add(this.lblVideoFramesCount);
            this.grpVideo.Controls.Add(this.btnVideoOpen);
            this.grpVideo.Controls.Add(this.pbrVideo);
            this.grpVideo.Controls.Add(this.lblVideoFilePath);
            this.grpVideo.Controls.Add(this.picVideo);
            this.grpVideo.Controls.Add(this.btnVideoGoToFrame);
            this.grpVideo.Controls.Add(this.btnVideoClose);
            this.grpVideo.Controls.Add(this.tbxVideoGoToFrame);
            this.grpVideo.Controls.Add(this.radVideoStop);
            this.grpVideo.Controls.Add(this.lblVideoCurrentFrame);
            this.grpVideo.Controls.Add(this.radVideoPlay);
            this.grpVideo.Controls.Add(this.tbxVideoCurrentFrame);
            this.grpVideo.Location = new System.Drawing.Point(12, 12);
            this.grpVideo.Name = "grpVideo";
            this.grpVideo.Size = new System.Drawing.Size(727, 249);
            this.grpVideo.TabIndex = 11;
            this.grpVideo.TabStop = false;
            this.grpVideo.Text = "Video";
            // 
            // lblVideoFramesCount
            // 
            this.lblVideoFramesCount.AutoSize = true;
            this.lblVideoFramesCount.Location = new System.Drawing.Point(27, 106);
            this.lblVideoFramesCount.Name = "lblVideoFramesCount";
            this.lblVideoFramesCount.Size = new System.Drawing.Size(68, 13);
            this.lblVideoFramesCount.TabIndex = 11;
            this.lblVideoFramesCount.Text = "Total Frames";
            // 
            // tbxVideoTotalFrames
            // 
            this.tbxVideoTotalFrames.Location = new System.Drawing.Point(145, 103);
            this.tbxVideoTotalFrames.Name = "tbxVideoTotalFrames";
            this.tbxVideoTotalFrames.ReadOnly = true;
            this.tbxVideoTotalFrames.Size = new System.Drawing.Size(100, 20);
            this.tbxVideoTotalFrames.TabIndex = 12;
            // 
            // lblVideoFps
            // 
            this.lblVideoFps.AutoSize = true;
            this.lblVideoFps.Location = new System.Drawing.Point(27, 132);
            this.lblVideoFps.Name = "lblVideoFps";
            this.lblVideoFps.Size = new System.Drawing.Size(54, 13);
            this.lblVideoFps.TabIndex = 13;
            this.lblVideoFps.Text = "Video Fps";
            // 
            // tbxVideoFps
            // 
            this.tbxVideoFps.Location = new System.Drawing.Point(145, 129);
            this.tbxVideoFps.Name = "tbxVideoFps";
            this.tbxVideoFps.ReadOnly = true;
            this.tbxVideoFps.Size = new System.Drawing.Size(100, 20);
            this.tbxVideoFps.TabIndex = 14;
            // 
            // grpImages
            // 
            this.grpImages.Location = new System.Drawing.Point(13, 285);
            this.grpImages.Name = "grpImages";
            this.grpImages.Size = new System.Drawing.Size(727, 249);
            this.grpImages.TabIndex = 12;
            this.grpImages.TabStop = false;
            this.grpImages.Text = "Images";
            // 
            // dlgVideoOpen
            // 
            this.dlgVideoOpen.Filter = "AVI|*.avi|MP4|*.mp4|MPEG|*.mpg";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 552);
            this.Controls.Add(this.grpImages);
            this.Controls.Add(this.grpVideo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.picVideo)).EndInit();
            this.grpVideo.ResumeLayout(false);
            this.grpVideo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVideoOpen;
        private System.Windows.Forms.Label lblVideoFilePath;
        private System.Windows.Forms.Button btnVideoGoToFrame;
        private System.Windows.Forms.TextBox tbxVideoGoToFrame;
        private System.Windows.Forms.Label lblVideoCurrentFrame;
        private System.Windows.Forms.TextBox tbxVideoCurrentFrame;
        private System.Windows.Forms.RadioButton radVideoPlay;
        private System.Windows.Forms.RadioButton radVideoStop;
        private System.Windows.Forms.Button btnVideoClose;
        private System.Windows.Forms.PictureBox picVideo;
        private System.Windows.Forms.ProgressBar pbrVideo;
        private System.Windows.Forms.GroupBox grpVideo;
        private System.Windows.Forms.TextBox tbxVideoFps;
        private System.Windows.Forms.Label lblVideoFps;
        private System.Windows.Forms.TextBox tbxVideoTotalFrames;
        private System.Windows.Forms.Label lblVideoFramesCount;
        private System.Windows.Forms.GroupBox grpImages;
        private System.Windows.Forms.OpenFileDialog dlgVideoOpen;
    }
}

