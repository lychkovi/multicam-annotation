namespace VideoAnnotationAPP
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openVideoDlg = new System.Windows.Forms.OpenFileDialog();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOpenVideo = new System.Windows.Forms.Button();
            this.txtVideoFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVideoFramesTotal = new System.Windows.Forms.TextBox();
            this.txtVideoFps = new System.Windows.Forms.TextBox();
            this.txtVideoDuration = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCurrentPosition = new System.Windows.Forms.TextBox();
            this.btnGoToPosition = new System.Windows.Forms.Button();
            this.btnPlay10x = new System.Windows.Forms.Button();
            this.btnPlay05x = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay20x = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCurrentFrame = new System.Windows.Forms.TextBox();
            this.btnNextFrame = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCloseVideo = new System.Windows.Forms.Button();
            this.btnCreateTrack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 30;
            this.trackBar1.Location = new System.Drawing.Point(246, 424);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(628, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.Location = new System.Drawing.Point(246, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(628, 325);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // btnOpenVideo
            // 
            this.btnOpenVideo.Location = new System.Drawing.Point(26, 26);
            this.btnOpenVideo.Name = "btnOpenVideo";
            this.btnOpenVideo.Size = new System.Drawing.Size(94, 23);
            this.btnOpenVideo.TabIndex = 3;
            this.btnOpenVideo.Text = "Open Video";
            this.btnOpenVideo.UseVisualStyleBackColor = true;
            this.btnOpenVideo.Click += new System.EventHandler(this.btnOpenVideo_Click);
            // 
            // txtVideoFilePath
            // 
            this.txtVideoFilePath.Location = new System.Drawing.Point(126, 28);
            this.txtVideoFilePath.Name = "txtVideoFilePath";
            this.txtVideoFilePath.ReadOnly = true;
            this.txtVideoFilePath.Size = new System.Drawing.Size(748, 20);
            this.txtVideoFilePath.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Video Frames Total";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Video FPS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Video Duration";
            // 
            // txtVideoFramesTotal
            // 
            this.txtVideoFramesTotal.Location = new System.Drawing.Point(126, 67);
            this.txtVideoFramesTotal.Name = "txtVideoFramesTotal";
            this.txtVideoFramesTotal.ReadOnly = true;
            this.txtVideoFramesTotal.Size = new System.Drawing.Size(100, 20);
            this.txtVideoFramesTotal.TabIndex = 8;
            // 
            // txtVideoFps
            // 
            this.txtVideoFps.Location = new System.Drawing.Point(126, 96);
            this.txtVideoFps.Name = "txtVideoFps";
            this.txtVideoFps.ReadOnly = true;
            this.txtVideoFps.Size = new System.Drawing.Size(100, 20);
            this.txtVideoFps.TabIndex = 9;
            // 
            // txtVideoDuration
            // 
            this.txtVideoDuration.Location = new System.Drawing.Point(126, 128);
            this.txtVideoDuration.Name = "txtVideoDuration";
            this.txtVideoDuration.ReadOnly = true;
            this.txtVideoDuration.Size = new System.Drawing.Size(100, 20);
            this.txtVideoDuration.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Current Position";
            // 
            // txtCurrentPosition
            // 
            this.txtCurrentPosition.Location = new System.Drawing.Point(126, 160);
            this.txtCurrentPosition.Name = "txtCurrentPosition";
            this.txtCurrentPosition.ReadOnly = true;
            this.txtCurrentPosition.Size = new System.Drawing.Size(100, 20);
            this.txtCurrentPosition.TabIndex = 12;
            // 
            // btnGoToPosition
            // 
            this.btnGoToPosition.Location = new System.Drawing.Point(26, 192);
            this.btnGoToPosition.Name = "btnGoToPosition";
            this.btnGoToPosition.Size = new System.Drawing.Size(94, 23);
            this.btnGoToPosition.TabIndex = 13;
            this.btnGoToPosition.Text = "Go To Position";
            this.btnGoToPosition.UseVisualStyleBackColor = true;
            this.btnGoToPosition.Click += new System.EventHandler(this.btnGoToPosition_Click);
            // 
            // btnPlay10x
            // 
            this.btnPlay10x.Location = new System.Drawing.Point(16, 48);
            this.btnPlay10x.Name = "btnPlay10x";
            this.btnPlay10x.Size = new System.Drawing.Size(63, 23);
            this.btnPlay10x.TabIndex = 15;
            this.btnPlay10x.Text = "Play";
            this.btnPlay10x.UseVisualStyleBackColor = true;
            // 
            // btnPlay05x
            // 
            this.btnPlay05x.Location = new System.Drawing.Point(85, 19);
            this.btnPlay05x.Name = "btnPlay05x";
            this.btnPlay05x.Size = new System.Drawing.Size(65, 23);
            this.btnPlay05x.TabIndex = 16;
            this.btnPlay05x.Text = "Play 0.5x";
            this.btnPlay05x.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(85, 48);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(65, 23);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnPlay20x
            // 
            this.btnPlay20x.Location = new System.Drawing.Point(16, 19);
            this.btnPlay20x.Name = "btnPlay20x";
            this.btnPlay20x.Size = new System.Drawing.Size(63, 23);
            this.btnPlay20x.TabIndex = 18;
            this.btnPlay20x.Text = "Play 2x";
            this.btnPlay20x.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Current Frame";
            // 
            // txtCurrentFrame
            // 
            this.txtCurrentFrame.Location = new System.Drawing.Point(126, 229);
            this.txtCurrentFrame.Name = "txtCurrentFrame";
            this.txtCurrentFrame.ReadOnly = true;
            this.txtCurrentFrame.Size = new System.Drawing.Size(100, 20);
            this.txtCurrentFrame.TabIndex = 20;
            // 
            // btnNextFrame
            // 
            this.btnNextFrame.Location = new System.Drawing.Point(26, 271);
            this.btnNextFrame.Name = "btnNextFrame";
            this.btnNextFrame.Size = new System.Drawing.Size(75, 23);
            this.btnNextFrame.TabIndex = 21;
            this.btnNextFrame.Text = "Next Frame";
            this.btnNextFrame.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(127, 271);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Previous Frame";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(126, 195);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(100, 20);
            this.maskedTextBox1.TabIndex = 23;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPlay20x);
            this.groupBox1.Controls.Add(this.btnPlay10x);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnPlay05x);
            this.groupBox1.Location = new System.Drawing.Point(30, 310);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 82);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Playback";
            // 
            // btnCloseVideo
            // 
            this.btnCloseVideo.Location = new System.Drawing.Point(30, 427);
            this.btnCloseVideo.Name = "btnCloseVideo";
            this.btnCloseVideo.Size = new System.Drawing.Size(75, 23);
            this.btnCloseVideo.TabIndex = 25;
            this.btnCloseVideo.Text = "Close Video";
            this.btnCloseVideo.UseVisualStyleBackColor = true;
            this.btnCloseVideo.Click += new System.EventHandler(this.btnCloseVideo_Click);
            // 
            // btnCreateTrack
            // 
            this.btnCreateTrack.Location = new System.Drawing.Point(127, 426);
            this.btnCreateTrack.Name = "btnCreateTrack";
            this.btnCreateTrack.Size = new System.Drawing.Size(89, 23);
            this.btnCreateTrack.TabIndex = 26;
            this.btnCreateTrack.Text = "Create Track";
            this.btnCreateTrack.UseVisualStyleBackColor = true;
            this.btnCreateTrack.Click += new System.EventHandler(this.btnCreateTrack_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 481);
            this.Controls.Add(this.btnCreateTrack);
            this.Controls.Add(this.btnCloseVideo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnNextFrame);
            this.Controls.Add(this.txtCurrentFrame);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnGoToPosition);
            this.Controls.Add(this.txtCurrentPosition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtVideoDuration);
            this.Controls.Add(this.txtVideoFps);
            this.Controls.Add(this.txtVideoFramesTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtVideoFilePath);
            this.Controls.Add(this.btnOpenVideo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.trackBar1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openVideoDlg;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOpenVideo;
        private System.Windows.Forms.TextBox txtVideoFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVideoFramesTotal;
        private System.Windows.Forms.TextBox txtVideoFps;
        private System.Windows.Forms.TextBox txtVideoDuration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCurrentPosition;
        private System.Windows.Forms.Button btnGoToPosition;
        private System.Windows.Forms.Button btnPlay10x;
        private System.Windows.Forms.Button btnPlay05x;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlay20x;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCurrentFrame;
        private System.Windows.Forms.Button btnNextFrame;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCloseVideo;
        private System.Windows.Forms.Button btnCreateTrack;
    }
}