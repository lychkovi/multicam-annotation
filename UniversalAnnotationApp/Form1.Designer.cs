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
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRecordingOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRecordingCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRecordingClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMarkupOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMarkupSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMarkupSaveAd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMarkupClose = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTraceCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTraceDelete = new System.Windows.Forms.ToolStripMenuItem();
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
            this.btnTrackingTrack = new System.Windows.Forms.Button();
            this.chkTrackingIsShaded = new System.Windows.Forms.CheckBox();
            this.chkTrackingIsOccluded = new System.Windows.Forms.CheckBox();
            this.btnTrackingTruncate = new System.Windows.Forms.Button();
            this.btnTrackingSeekExtent = new System.Windows.Forms.Button();
            this.lblTrackingAttributes = new System.Windows.Forms.Label();
            this.chkTrackingReverse = new System.Windows.Forms.CheckBox();
            this.lblTrackingDir = new System.Windows.Forms.Label();
            this.lblTrackingMethod = new System.Windows.Forms.Label();
            this.cmbTrackingMethod = new System.Windows.Forms.ComboBox();
            this.grpNavigation = new System.Windows.Forms.GroupBox();
            this.btnNaviPlayStop = new System.Windows.Forms.Button();
            this.radNaviMarkerMajor = new System.Windows.Forms.RadioButton();
            this.radNaviBoxMajor = new System.Windows.Forms.RadioButton();
            this.cmbNaviPlaySpeed = new System.Windows.Forms.ComboBox();
            this.lblNaviPlaySpeed = new System.Windows.Forms.Label();
            this.chkNaviPlayReverse = new System.Windows.Forms.CheckBox();
            this.lblNaviPlayDir = new System.Windows.Forms.Label();
            this.btnNaviNext = new System.Windows.Forms.Button();
            this.btnNaviPrevious = new System.Windows.Forms.Button();
            this.txtNaviTotalFrames = new System.Windows.Forms.TextBox();
            this.lblNaviTotalFrames = new System.Windows.Forms.Label();
            this.txtNaviGotoFrame = new System.Windows.Forms.TextBox();
            this.btnNaviGotoFrame = new System.Windows.Forms.Button();
            this.txtNaviCurrFrame = new System.Windows.Forms.TextBox();
            this.lblNaviCurrFrame = new System.Windows.Forms.Label();
            this.pnlAttributes = new System.Windows.Forms.Panel();
            this.grpAttributes = new System.Windows.Forms.GroupBox();
            this.pnlTimeLine = new System.Windows.Forms.Panel();
            this.trbNaviSlider = new System.Windows.Forms.TrackBar();
            this.tmrPlayTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.pnlFilesOpened.SuspendLayout();
            this.pnlTools.SuspendLayout();
            this.grpTracking.SuspendLayout();
            this.grpNavigation.SuspendLayout();
            this.pnlAttributes.SuspendLayout();
            this.pnlTimeLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbNaviSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(793, 24);
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
            this.menuTraceCreate.Size = new System.Drawing.Size(137, 22);
            this.menuTraceCreate.Text = "Trace Create";
            this.menuTraceCreate.Click += new System.EventHandler(this.menuTraceCreate_Click);
            // 
            // menuTraceDelete
            // 
            this.menuTraceDelete.Name = "menuTraceDelete";
            this.menuTraceDelete.Size = new System.Drawing.Size(137, 22);
            this.menuTraceDelete.Text = "Trace Delete";
            this.menuTraceDelete.Click += new System.EventHandler(this.menuTraceDelete_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusVideo,
            this.statusMarkup,
            this.statusMode,
            this.statusAction});
            this.statusStrip.Location = new System.Drawing.Point(0, 550);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(793, 22);
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
            this.pnlDisplay.Size = new System.Drawing.Size(485, 407);
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
            this.pnlFilesOpened.Size = new System.Drawing.Size(793, 70);
            this.pnlFilesOpened.TabIndex = 7;
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.grpTracking);
            this.pnlTools.Controls.Add(this.grpNavigation);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTools.Location = new System.Drawing.Point(0, 94);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(154, 456);
            this.pnlTools.TabIndex = 8;
            // 
            // grpTracking
            // 
            this.grpTracking.Controls.Add(this.btnTrackingTrack);
            this.grpTracking.Controls.Add(this.chkTrackingIsShaded);
            this.grpTracking.Controls.Add(this.chkTrackingIsOccluded);
            this.grpTracking.Controls.Add(this.btnTrackingTruncate);
            this.grpTracking.Controls.Add(this.btnTrackingSeekExtent);
            this.grpTracking.Controls.Add(this.lblTrackingAttributes);
            this.grpTracking.Controls.Add(this.chkTrackingReverse);
            this.grpTracking.Controls.Add(this.lblTrackingDir);
            this.grpTracking.Controls.Add(this.lblTrackingMethod);
            this.grpTracking.Controls.Add(this.cmbTrackingMethod);
            this.grpTracking.Location = new System.Drawing.Point(4, 228);
            this.grpTracking.Name = "grpTracking";
            this.grpTracking.Size = new System.Drawing.Size(147, 225);
            this.grpTracking.TabIndex = 1;
            this.grpTracking.TabStop = false;
            this.grpTracking.Text = "Tracking";
            // 
            // btnTrackingTrack
            // 
            this.btnTrackingTrack.Location = new System.Drawing.Point(7, 193);
            this.btnTrackingTrack.Name = "btnTrackingTrack";
            this.btnTrackingTrack.Size = new System.Drawing.Size(63, 23);
            this.btnTrackingTrack.TabIndex = 9;
            this.btnTrackingTrack.Text = "Track";
            this.btnTrackingTrack.UseVisualStyleBackColor = true;
            // 
            // chkTrackingIsShaded
            // 
            this.chkTrackingIsShaded.AutoSize = true;
            this.chkTrackingIsShaded.Location = new System.Drawing.Point(10, 169);
            this.chkTrackingIsShaded.Name = "chkTrackingIsShaded";
            this.chkTrackingIsShaded.Size = new System.Drawing.Size(74, 17);
            this.chkTrackingIsShaded.TabIndex = 8;
            this.chkTrackingIsShaded.Text = "Is Shaded";
            this.chkTrackingIsShaded.UseVisualStyleBackColor = true;
            // 
            // chkTrackingIsOccluded
            // 
            this.chkTrackingIsOccluded.AutoSize = true;
            this.chkTrackingIsOccluded.Location = new System.Drawing.Point(10, 146);
            this.chkTrackingIsOccluded.Name = "chkTrackingIsOccluded";
            this.chkTrackingIsOccluded.Size = new System.Drawing.Size(83, 17);
            this.chkTrackingIsOccluded.TabIndex = 7;
            this.chkTrackingIsOccluded.Text = "Is Occluded";
            this.chkTrackingIsOccluded.UseVisualStyleBackColor = true;
            // 
            // btnTrackingTruncate
            // 
            this.btnTrackingTruncate.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackingTruncate.Location = new System.Drawing.Point(7, 98);
            this.btnTrackingTruncate.Name = "btnTrackingTruncate";
            this.btnTrackingTruncate.Size = new System.Drawing.Size(134, 23);
            this.btnTrackingTruncate.TabIndex = 6;
            this.btnTrackingTruncate.Text = "Truncate Trace End";
            this.btnTrackingTruncate.UseVisualStyleBackColor = false;
            // 
            // btnTrackingSeekExtent
            // 
            this.btnTrackingSeekExtent.Location = new System.Drawing.Point(7, 69);
            this.btnTrackingSeekExtent.Name = "btnTrackingSeekExtent";
            this.btnTrackingSeekExtent.Size = new System.Drawing.Size(107, 23);
            this.btnTrackingSeekExtent.TabIndex = 5;
            this.btnTrackingSeekExtent.Text = "Seek Trace End";
            this.btnTrackingSeekExtent.UseVisualStyleBackColor = true;
            // 
            // lblTrackingAttributes
            // 
            this.lblTrackingAttributes.AutoSize = true;
            this.lblTrackingAttributes.Location = new System.Drawing.Point(6, 129);
            this.lblTrackingAttributes.Name = "lblTrackingAttributes";
            this.lblTrackingAttributes.Size = new System.Drawing.Size(54, 13);
            this.lblTrackingAttributes.TabIndex = 4;
            this.lblTrackingAttributes.Text = "Attributes:";
            // 
            // chkTrackingReverse
            // 
            this.chkTrackingReverse.AutoSize = true;
            this.chkTrackingReverse.Location = new System.Drawing.Point(69, 46);
            this.chkTrackingReverse.Name = "chkTrackingReverse";
            this.chkTrackingReverse.Size = new System.Drawing.Size(72, 17);
            this.chkTrackingReverse.TabIndex = 3;
            this.chkTrackingReverse.Text = "Reversed";
            this.chkTrackingReverse.UseVisualStyleBackColor = true;
            // 
            // lblTrackingDir
            // 
            this.lblTrackingDir.AutoSize = true;
            this.lblTrackingDir.Location = new System.Drawing.Point(7, 47);
            this.lblTrackingDir.Name = "lblTrackingDir";
            this.lblTrackingDir.Size = new System.Drawing.Size(51, 13);
            this.lblTrackingDir.TabIndex = 2;
            this.lblTrackingDir.Text = "Track Dir";
            // 
            // lblTrackingMethod
            // 
            this.lblTrackingMethod.AutoSize = true;
            this.lblTrackingMethod.Location = new System.Drawing.Point(7, 20);
            this.lblTrackingMethod.Name = "lblTrackingMethod";
            this.lblTrackingMethod.Size = new System.Drawing.Size(43, 13);
            this.lblTrackingMethod.TabIndex = 1;
            this.lblTrackingMethod.Text = "Method";
            // 
            // cmbTrackingMethod
            // 
            this.cmbTrackingMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrackingMethod.FormattingEnabled = true;
            this.cmbTrackingMethod.Location = new System.Drawing.Point(56, 17);
            this.cmbTrackingMethod.Name = "cmbTrackingMethod";
            this.cmbTrackingMethod.Size = new System.Drawing.Size(85, 21);
            this.cmbTrackingMethod.TabIndex = 0;
            // 
            // grpNavigation
            // 
            this.grpNavigation.Controls.Add(this.btnNaviPlayStop);
            this.grpNavigation.Controls.Add(this.radNaviMarkerMajor);
            this.grpNavigation.Controls.Add(this.radNaviBoxMajor);
            this.grpNavigation.Controls.Add(this.cmbNaviPlaySpeed);
            this.grpNavigation.Controls.Add(this.lblNaviPlaySpeed);
            this.grpNavigation.Controls.Add(this.chkNaviPlayReverse);
            this.grpNavigation.Controls.Add(this.lblNaviPlayDir);
            this.grpNavigation.Controls.Add(this.btnNaviNext);
            this.grpNavigation.Controls.Add(this.btnNaviPrevious);
            this.grpNavigation.Controls.Add(this.txtNaviTotalFrames);
            this.grpNavigation.Controls.Add(this.lblNaviTotalFrames);
            this.grpNavigation.Controls.Add(this.txtNaviGotoFrame);
            this.grpNavigation.Controls.Add(this.btnNaviGotoFrame);
            this.grpNavigation.Controls.Add(this.txtNaviCurrFrame);
            this.grpNavigation.Controls.Add(this.lblNaviCurrFrame);
            this.grpNavigation.Location = new System.Drawing.Point(4, 7);
            this.grpNavigation.Name = "grpNavigation";
            this.grpNavigation.Size = new System.Drawing.Size(147, 215);
            this.grpNavigation.TabIndex = 0;
            this.grpNavigation.TabStop = false;
            this.grpNavigation.Text = "Navigation";
            // 
            // btnNaviPlayStop
            // 
            this.btnNaviPlayStop.Location = new System.Drawing.Point(7, 159);
            this.btnNaviPlayStop.Name = "btnNaviPlayStop";
            this.btnNaviPlayStop.Size = new System.Drawing.Size(63, 23);
            this.btnNaviPlayStop.TabIndex = 14;
            this.btnNaviPlayStop.Text = "Play";
            this.btnNaviPlayStop.UseVisualStyleBackColor = true;
            // 
            // radNaviMarkerMajor
            // 
            this.radNaviMarkerMajor.AutoSize = true;
            this.radNaviMarkerMajor.Location = new System.Drawing.Point(69, 191);
            this.radNaviMarkerMajor.Name = "radNaviMarkerMajor";
            this.radNaviMarkerMajor.Size = new System.Drawing.Size(58, 17);
            this.radNaviMarkerMajor.TabIndex = 13;
            this.radNaviMarkerMajor.TabStop = true;
            this.radNaviMarkerMajor.Text = "Marker";
            this.radNaviMarkerMajor.UseVisualStyleBackColor = true;
            // 
            // radNaviBoxMajor
            // 
            this.radNaviBoxMajor.AutoSize = true;
            this.radNaviBoxMajor.Location = new System.Drawing.Point(11, 191);
            this.radNaviBoxMajor.Name = "radNaviBoxMajor";
            this.radNaviBoxMajor.Size = new System.Drawing.Size(43, 17);
            this.radNaviBoxMajor.TabIndex = 12;
            this.radNaviBoxMajor.TabStop = true;
            this.radNaviBoxMajor.Text = "Box";
            this.radNaviBoxMajor.UseVisualStyleBackColor = true;
            // 
            // cmbNaviPlaySpeed
            // 
            this.cmbNaviPlaySpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNaviPlaySpeed.FormattingEnabled = true;
            this.cmbNaviPlaySpeed.Location = new System.Drawing.Point(86, 132);
            this.cmbNaviPlaySpeed.Name = "cmbNaviPlaySpeed";
            this.cmbNaviPlaySpeed.Size = new System.Drawing.Size(55, 21);
            this.cmbNaviPlaySpeed.TabIndex = 11;
            // 
            // lblNaviPlaySpeed
            // 
            this.lblNaviPlaySpeed.AutoSize = true;
            this.lblNaviPlaySpeed.Location = new System.Drawing.Point(7, 135);
            this.lblNaviPlaySpeed.Name = "lblNaviPlaySpeed";
            this.lblNaviPlaySpeed.Size = new System.Drawing.Size(61, 13);
            this.lblNaviPlaySpeed.TabIndex = 10;
            this.lblNaviPlaySpeed.Text = "Play Speed";
            // 
            // chkNaviPlayReverse
            // 
            this.chkNaviPlayReverse.AutoSize = true;
            this.chkNaviPlayReverse.Location = new System.Drawing.Point(69, 112);
            this.chkNaviPlayReverse.Name = "chkNaviPlayReverse";
            this.chkNaviPlayReverse.Size = new System.Drawing.Size(72, 17);
            this.chkNaviPlayReverse.TabIndex = 9;
            this.chkNaviPlayReverse.Text = "Reversed";
            this.chkNaviPlayReverse.UseVisualStyleBackColor = true;
            // 
            // lblNaviPlayDir
            // 
            this.lblNaviPlayDir.AutoSize = true;
            this.lblNaviPlayDir.Location = new System.Drawing.Point(7, 113);
            this.lblNaviPlayDir.Name = "lblNaviPlayDir";
            this.lblNaviPlayDir.Size = new System.Drawing.Size(43, 13);
            this.lblNaviPlayDir.TabIndex = 8;
            this.lblNaviPlayDir.Text = "Play Dir";
            // 
            // btnNaviNext
            // 
            this.btnNaviNext.Location = new System.Drawing.Point(77, 85);
            this.btnNaviNext.Name = "btnNaviNext";
            this.btnNaviNext.Size = new System.Drawing.Size(64, 23);
            this.btnNaviNext.TabIndex = 7;
            this.btnNaviNext.Text = "Next";
            this.btnNaviNext.UseVisualStyleBackColor = true;
            // 
            // btnNaviPrevious
            // 
            this.btnNaviPrevious.Location = new System.Drawing.Point(7, 85);
            this.btnNaviPrevious.Name = "btnNaviPrevious";
            this.btnNaviPrevious.Size = new System.Drawing.Size(63, 23);
            this.btnNaviPrevious.TabIndex = 6;
            this.btnNaviPrevious.Text = "Previous";
            this.btnNaviPrevious.UseVisualStyleBackColor = true;
            // 
            // txtNaviTotalFrames
            // 
            this.txtNaviTotalFrames.Location = new System.Drawing.Point(86, 61);
            this.txtNaviTotalFrames.Name = "txtNaviTotalFrames";
            this.txtNaviTotalFrames.ReadOnly = true;
            this.txtNaviTotalFrames.Size = new System.Drawing.Size(55, 20);
            this.txtNaviTotalFrames.TabIndex = 5;
            // 
            // lblNaviTotalFrames
            // 
            this.lblNaviTotalFrames.AutoSize = true;
            this.lblNaviTotalFrames.Location = new System.Drawing.Point(7, 65);
            this.lblNaviTotalFrames.Name = "lblNaviTotalFrames";
            this.lblNaviTotalFrames.Size = new System.Drawing.Size(68, 13);
            this.lblNaviTotalFrames.TabIndex = 4;
            this.lblNaviTotalFrames.Text = "Total Frames";
            // 
            // txtNaviGotoFrame
            // 
            this.txtNaviGotoFrame.Location = new System.Drawing.Point(86, 39);
            this.txtNaviGotoFrame.Name = "txtNaviGotoFrame";
            this.txtNaviGotoFrame.Size = new System.Drawing.Size(55, 20);
            this.txtNaviGotoFrame.TabIndex = 3;
            // 
            // btnNaviGotoFrame
            // 
            this.btnNaviGotoFrame.Location = new System.Drawing.Point(7, 37);
            this.btnNaviGotoFrame.Name = "btnNaviGotoFrame";
            this.btnNaviGotoFrame.Size = new System.Drawing.Size(75, 23);
            this.btnNaviGotoFrame.TabIndex = 2;
            this.btnNaviGotoFrame.Text = "Go to Frame";
            this.btnNaviGotoFrame.UseVisualStyleBackColor = true;
            // 
            // txtNaviCurrFrame
            // 
            this.txtNaviCurrFrame.Location = new System.Drawing.Point(86, 17);
            this.txtNaviCurrFrame.Name = "txtNaviCurrFrame";
            this.txtNaviCurrFrame.ReadOnly = true;
            this.txtNaviCurrFrame.Size = new System.Drawing.Size(55, 20);
            this.txtNaviCurrFrame.TabIndex = 1;
            // 
            // lblNaviCurrFrame
            // 
            this.lblNaviCurrFrame.AutoSize = true;
            this.lblNaviCurrFrame.Location = new System.Drawing.Point(7, 20);
            this.lblNaviCurrFrame.Name = "lblNaviCurrFrame";
            this.lblNaviCurrFrame.Size = new System.Drawing.Size(73, 13);
            this.lblNaviCurrFrame.TabIndex = 0;
            this.lblNaviCurrFrame.Text = "Current Frame";
            // 
            // pnlAttributes
            // 
            this.pnlAttributes.Controls.Add(this.grpAttributes);
            this.pnlAttributes.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAttributes.Location = new System.Drawing.Point(639, 94);
            this.pnlAttributes.Name = "pnlAttributes";
            this.pnlAttributes.Size = new System.Drawing.Size(154, 456);
            this.pnlAttributes.TabIndex = 9;
            // 
            // grpAttributes
            // 
            this.grpAttributes.Location = new System.Drawing.Point(4, 4);
            this.grpAttributes.Name = "grpAttributes";
            this.grpAttributes.Size = new System.Drawing.Size(147, 449);
            this.grpAttributes.TabIndex = 0;
            this.grpAttributes.TabStop = false;
            this.grpAttributes.Text = "Attributes";
            // 
            // pnlTimeLine
            // 
            this.pnlTimeLine.Controls.Add(this.trbNaviSlider);
            this.pnlTimeLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTimeLine.Location = new System.Drawing.Point(154, 501);
            this.pnlTimeLine.Name = "pnlTimeLine";
            this.pnlTimeLine.Size = new System.Drawing.Size(485, 49);
            this.pnlTimeLine.TabIndex = 10;
            // 
            // trbNaviSlider
            // 
            this.trbNaviSlider.Location = new System.Drawing.Point(3, 4);
            this.trbNaviSlider.Name = "trbNaviSlider";
            this.trbNaviSlider.Size = new System.Drawing.Size(428, 42);
            this.trbNaviSlider.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 572);
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
            this.grpTracking.ResumeLayout(false);
            this.grpTracking.PerformLayout();
            this.grpNavigation.ResumeLayout(false);
            this.grpNavigation.PerformLayout();
            this.pnlAttributes.ResumeLayout(false);
            this.pnlTimeLine.ResumeLayout(false);
            this.pnlTimeLine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbNaviSlider)).EndInit();
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
        private System.Windows.Forms.TrackBar trbNaviSlider;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuTraceCreate;
        private System.Windows.Forms.ToolStripMenuItem menuTraceDelete;
        private System.Windows.Forms.Timer tmrPlayTimer;
        private System.Windows.Forms.Button btnNaviNext;
        private System.Windows.Forms.Button btnNaviPrevious;
        private System.Windows.Forms.TextBox txtNaviTotalFrames;
        private System.Windows.Forms.Label lblNaviTotalFrames;
        private System.Windows.Forms.TextBox txtNaviGotoFrame;
        private System.Windows.Forms.Button btnNaviGotoFrame;
        private System.Windows.Forms.TextBox txtNaviCurrFrame;
        private System.Windows.Forms.Label lblNaviCurrFrame;
        private System.Windows.Forms.ComboBox cmbNaviPlaySpeed;
        private System.Windows.Forms.Label lblNaviPlaySpeed;
        private System.Windows.Forms.CheckBox chkNaviPlayReverse;
        private System.Windows.Forms.Label lblNaviPlayDir;
        private System.Windows.Forms.RadioButton radNaviMarkerMajor;
        private System.Windows.Forms.RadioButton radNaviBoxMajor;
        private System.Windows.Forms.Button btnNaviPlayStop;
        private System.Windows.Forms.Label lblTrackingAttributes;
        private System.Windows.Forms.CheckBox chkTrackingReverse;
        private System.Windows.Forms.Label lblTrackingDir;
        private System.Windows.Forms.Label lblTrackingMethod;
        private System.Windows.Forms.ComboBox cmbTrackingMethod;
        private System.Windows.Forms.Button btnTrackingTruncate;
        private System.Windows.Forms.Button btnTrackingSeekExtent;
        private System.Windows.Forms.Button btnTrackingTrack;
        private System.Windows.Forms.CheckBox chkTrackingIsShaded;
        private System.Windows.Forms.CheckBox chkTrackingIsOccluded;
    }
}

