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
            this.grpCategory = new System.Windows.Forms.GroupBox();
            this.btnCategoryEditSave = new System.Windows.Forms.Button();
            this.btnCategoryDelete = new System.Windows.Forms.Button();
            this.btnCategoryNew = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCategoryID = new System.Windows.Forms.ComboBox();
            this.lblCategoryID = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.grpTag = new System.Windows.Forms.GroupBox();
            this.btnTagEditSave = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblTagName = new System.Windows.Forms.Label();
            this.cmbTagID = new System.Windows.Forms.ComboBox();
            this.btnTagDelete = new System.Windows.Forms.Button();
            this.btnTagNew = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lblTagCategoryID = new System.Windows.Forms.Label();
            this.lblTagID = new System.Windows.Forms.Label();
            this.grpNode = new System.Windows.Forms.GroupBox();
            this.chkNodeIsShaded = new System.Windows.Forms.CheckBox();
            this.chkNodeIsOccluded = new System.Windows.Forms.CheckBox();
            this.txtNodeHeight = new System.Windows.Forms.TextBox();
            this.lblNodeHeight = new System.Windows.Forms.Label();
            this.txtNodeWidth = new System.Windows.Forms.TextBox();
            this.lblNodeWidth = new System.Windows.Forms.Label();
            this.txtNodePosY = new System.Windows.Forms.TextBox();
            this.lblNodePosY = new System.Windows.Forms.Label();
            this.txtNodePosX = new System.Windows.Forms.TextBox();
            this.lblNodePosX = new System.Windows.Forms.Label();
            this.grpTrace = new System.Windows.Forms.GroupBox();
            this.txtTraceFrameEnd = new System.Windows.Forms.TextBox();
            this.lblTraceFrameEnd = new System.Windows.Forms.Label();
            this.txtTraceFrameStart = new System.Windows.Forms.TextBox();
            this.lblTraceFrameStart = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblTraceTagID = new System.Windows.Forms.Label();
            this.txtTraceID = new System.Windows.Forms.TextBox();
            this.lblTraceID = new System.Windows.Forms.Label();
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
            this.grpCategory.SuspendLayout();
            this.grpTag.SuspendLayout();
            this.grpNode.SuspendLayout();
            this.grpTrace.SuspendLayout();
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
            this.statusStrip.Location = new System.Drawing.Point(0, 635);
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
            this.pnlDisplay.Size = new System.Drawing.Size(485, 492);
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
            this.pnlTools.Size = new System.Drawing.Size(154, 541);
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
            this.btnNaviPrevious.Size = new System.Drawing.Size(64, 23);
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
            this.pnlAttributes.Controls.Add(this.grpCategory);
            this.pnlAttributes.Controls.Add(this.grpTag);
            this.pnlAttributes.Controls.Add(this.grpNode);
            this.pnlAttributes.Controls.Add(this.grpTrace);
            this.pnlAttributes.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAttributes.Location = new System.Drawing.Point(639, 94);
            this.pnlAttributes.Name = "pnlAttributes";
            this.pnlAttributes.Size = new System.Drawing.Size(154, 541);
            this.pnlAttributes.TabIndex = 9;
            // 
            // grpCategory
            // 
            this.grpCategory.Controls.Add(this.btnCategoryEditSave);
            this.grpCategory.Controls.Add(this.btnCategoryDelete);
            this.grpCategory.Controls.Add(this.btnCategoryNew);
            this.grpCategory.Controls.Add(this.label1);
            this.grpCategory.Controls.Add(this.cmbCategoryID);
            this.grpCategory.Controls.Add(this.lblCategoryID);
            this.grpCategory.Controls.Add(this.txtCategoryName);
            this.grpCategory.Location = new System.Drawing.Point(4, 408);
            this.grpCategory.Name = "grpCategory";
            this.grpCategory.Size = new System.Drawing.Size(147, 101);
            this.grpCategory.TabIndex = 3;
            this.grpCategory.TabStop = false;
            this.grpCategory.Text = "Category";
            // 
            // btnCategoryEditSave
            // 
            this.btnCategoryEditSave.Location = new System.Drawing.Point(98, 67);
            this.btnCategoryEditSave.Name = "btnCategoryEditSave";
            this.btnCategoryEditSave.Size = new System.Drawing.Size(40, 23);
            this.btnCategoryEditSave.TabIndex = 5;
            this.btnCategoryEditSave.Text = "Edit";
            this.btnCategoryEditSave.UseVisualStyleBackColor = true;
            // 
            // btnCategoryDelete
            // 
            this.btnCategoryDelete.Location = new System.Drawing.Point(52, 67);
            this.btnCategoryDelete.Name = "btnCategoryDelete";
            this.btnCategoryDelete.Size = new System.Drawing.Size(40, 23);
            this.btnCategoryDelete.TabIndex = 4;
            this.btnCategoryDelete.Text = "Del";
            this.btnCategoryDelete.UseVisualStyleBackColor = true;
            // 
            // btnCategoryNew
            // 
            this.btnCategoryNew.Location = new System.Drawing.Point(6, 67);
            this.btnCategoryNew.Name = "btnCategoryNew";
            this.btnCategoryNew.Size = new System.Drawing.Size(40, 23);
            this.btnCategoryNew.TabIndex = 3;
            this.btnCategoryNew.Text = "New";
            this.btnCategoryNew.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // cmbCategoryID
            // 
            this.cmbCategoryID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryID.FormattingEnabled = true;
            this.cmbCategoryID.Location = new System.Drawing.Point(47, 17);
            this.cmbCategoryID.Name = "cmbCategoryID";
            this.cmbCategoryID.Size = new System.Drawing.Size(94, 21);
            this.cmbCategoryID.TabIndex = 1;
            // 
            // lblCategoryID
            // 
            this.lblCategoryID.AutoSize = true;
            this.lblCategoryID.Location = new System.Drawing.Point(6, 20);
            this.lblCategoryID.Name = "lblCategoryID";
            this.lblCategoryID.Size = new System.Drawing.Size(18, 13);
            this.lblCategoryID.TabIndex = 0;
            this.lblCategoryID.Text = "ID";
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(47, 41);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.ReadOnly = true;
            this.txtCategoryName.Size = new System.Drawing.Size(94, 20);
            this.txtCategoryName.TabIndex = 1;
            // 
            // grpTag
            // 
            this.grpTag.Controls.Add(this.btnTagEditSave);
            this.grpTag.Controls.Add(this.textBox1);
            this.grpTag.Controls.Add(this.lblTagName);
            this.grpTag.Controls.Add(this.cmbTagID);
            this.grpTag.Controls.Add(this.btnTagDelete);
            this.grpTag.Controls.Add(this.btnTagNew);
            this.grpTag.Controls.Add(this.comboBox2);
            this.grpTag.Controls.Add(this.lblTagCategoryID);
            this.grpTag.Controls.Add(this.lblTagID);
            this.grpTag.Location = new System.Drawing.Point(4, 284);
            this.grpTag.Name = "grpTag";
            this.grpTag.Size = new System.Drawing.Size(147, 118);
            this.grpTag.TabIndex = 2;
            this.grpTag.TabStop = false;
            this.grpTag.Text = "Tag";
            // 
            // btnTagEditSave
            // 
            this.btnTagEditSave.Location = new System.Drawing.Point(98, 84);
            this.btnTagEditSave.Name = "btnTagEditSave";
            this.btnTagEditSave.Size = new System.Drawing.Size(40, 23);
            this.btnTagEditSave.TabIndex = 9;
            this.btnTagEditSave.Text = "Edit";
            this.btnTagEditSave.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(56, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(85, 20);
            this.textBox1.TabIndex = 8;
            // 
            // lblTagName
            // 
            this.lblTagName.AutoSize = true;
            this.lblTagName.Location = new System.Drawing.Point(6, 39);
            this.lblTagName.Name = "lblTagName";
            this.lblTagName.Size = new System.Drawing.Size(35, 13);
            this.lblTagName.TabIndex = 7;
            this.lblTagName.Text = "Name";
            // 
            // cmbTagID
            // 
            this.cmbTagID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTagID.FormattingEnabled = true;
            this.cmbTagID.Location = new System.Drawing.Point(83, 14);
            this.cmbTagID.Name = "cmbTagID";
            this.cmbTagID.Size = new System.Drawing.Size(58, 21);
            this.cmbTagID.TabIndex = 6;
            // 
            // btnTagDelete
            // 
            this.btnTagDelete.Location = new System.Drawing.Point(52, 84);
            this.btnTagDelete.Name = "btnTagDelete";
            this.btnTagDelete.Size = new System.Drawing.Size(40, 23);
            this.btnTagDelete.TabIndex = 5;
            this.btnTagDelete.Text = "Del";
            this.btnTagDelete.UseVisualStyleBackColor = true;
            // 
            // btnTagNew
            // 
            this.btnTagNew.Location = new System.Drawing.Point(6, 84);
            this.btnTagNew.Name = "btnTagNew";
            this.btnTagNew.Size = new System.Drawing.Size(40, 23);
            this.btnTagNew.TabIndex = 4;
            this.btnTagNew.Text = "New";
            this.btnTagNew.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(83, 57);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(58, 21);
            this.comboBox2.TabIndex = 3;
            // 
            // lblTagCategoryID
            // 
            this.lblTagCategoryID.AutoSize = true;
            this.lblTagCategoryID.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblTagCategoryID.Location = new System.Drawing.Point(6, 60);
            this.lblTagCategoryID.Name = "lblTagCategoryID";
            this.lblTagCategoryID.Size = new System.Drawing.Size(67, 13);
            this.lblTagCategoryID.TabIndex = 2;
            this.lblTagCategoryID.Text = "Category ID*";
            // 
            // lblTagID
            // 
            this.lblTagID.AutoSize = true;
            this.lblTagID.Location = new System.Drawing.Point(6, 19);
            this.lblTagID.Name = "lblTagID";
            this.lblTagID.Size = new System.Drawing.Size(40, 13);
            this.lblTagID.TabIndex = 0;
            this.lblTagID.Text = "Tag ID";
            // 
            // grpNode
            // 
            this.grpNode.Controls.Add(this.chkNodeIsShaded);
            this.grpNode.Controls.Add(this.chkNodeIsOccluded);
            this.grpNode.Controls.Add(this.txtNodeHeight);
            this.grpNode.Controls.Add(this.lblNodeHeight);
            this.grpNode.Controls.Add(this.txtNodeWidth);
            this.grpNode.Controls.Add(this.lblNodeWidth);
            this.grpNode.Controls.Add(this.txtNodePosY);
            this.grpNode.Controls.Add(this.lblNodePosY);
            this.grpNode.Controls.Add(this.txtNodePosX);
            this.grpNode.Controls.Add(this.lblNodePosX);
            this.grpNode.Location = new System.Drawing.Point(4, 126);
            this.grpNode.Name = "grpNode";
            this.grpNode.Size = new System.Drawing.Size(147, 151);
            this.grpNode.TabIndex = 1;
            this.grpNode.TabStop = false;
            this.grpNode.Text = "Box";
            // 
            // chkNodeIsShaded
            // 
            this.chkNodeIsShaded.AutoSize = true;
            this.chkNodeIsShaded.ForeColor = System.Drawing.SystemColors.Desktop;
            this.chkNodeIsShaded.Location = new System.Drawing.Point(9, 128);
            this.chkNodeIsShaded.Name = "chkNodeIsShaded";
            this.chkNodeIsShaded.Size = new System.Drawing.Size(78, 17);
            this.chkNodeIsShaded.TabIndex = 9;
            this.chkNodeIsShaded.Text = "Is Shaded*";
            this.chkNodeIsShaded.UseVisualStyleBackColor = true;
            // 
            // chkNodeIsOccluded
            // 
            this.chkNodeIsOccluded.AutoSize = true;
            this.chkNodeIsOccluded.ForeColor = System.Drawing.SystemColors.Desktop;
            this.chkNodeIsOccluded.Location = new System.Drawing.Point(9, 107);
            this.chkNodeIsOccluded.Name = "chkNodeIsOccluded";
            this.chkNodeIsOccluded.Size = new System.Drawing.Size(87, 17);
            this.chkNodeIsOccluded.TabIndex = 8;
            this.chkNodeIsOccluded.Text = "Is Occluded*";
            this.chkNodeIsOccluded.UseVisualStyleBackColor = true;
            // 
            // txtNodeHeight
            // 
            this.txtNodeHeight.Location = new System.Drawing.Point(56, 83);
            this.txtNodeHeight.Name = "txtNodeHeight";
            this.txtNodeHeight.ReadOnly = true;
            this.txtNodeHeight.Size = new System.Drawing.Size(85, 20);
            this.txtNodeHeight.TabIndex = 7;
            // 
            // lblNodeHeight
            // 
            this.lblNodeHeight.AutoSize = true;
            this.lblNodeHeight.Location = new System.Drawing.Point(6, 86);
            this.lblNodeHeight.Name = "lblNodeHeight";
            this.lblNodeHeight.Size = new System.Drawing.Size(38, 13);
            this.lblNodeHeight.TabIndex = 6;
            this.lblNodeHeight.Text = "Height";
            // 
            // txtNodeWidth
            // 
            this.txtNodeWidth.Location = new System.Drawing.Point(56, 61);
            this.txtNodeWidth.Name = "txtNodeWidth";
            this.txtNodeWidth.ReadOnly = true;
            this.txtNodeWidth.Size = new System.Drawing.Size(85, 20);
            this.txtNodeWidth.TabIndex = 5;
            // 
            // lblNodeWidth
            // 
            this.lblNodeWidth.AutoSize = true;
            this.lblNodeWidth.Location = new System.Drawing.Point(6, 64);
            this.lblNodeWidth.Name = "lblNodeWidth";
            this.lblNodeWidth.Size = new System.Drawing.Size(35, 13);
            this.lblNodeWidth.TabIndex = 4;
            this.lblNodeWidth.Text = "Width";
            // 
            // txtNodePosY
            // 
            this.txtNodePosY.Location = new System.Drawing.Point(56, 39);
            this.txtNodePosY.Name = "txtNodePosY";
            this.txtNodePosY.ReadOnly = true;
            this.txtNodePosY.Size = new System.Drawing.Size(85, 20);
            this.txtNodePosY.TabIndex = 3;
            // 
            // lblNodePosY
            // 
            this.lblNodePosY.AutoSize = true;
            this.lblNodePosY.Location = new System.Drawing.Point(6, 43);
            this.lblNodePosY.Name = "lblNodePosY";
            this.lblNodePosY.Size = new System.Drawing.Size(35, 13);
            this.lblNodePosY.TabIndex = 2;
            this.lblNodePosY.Text = "Pos Y";
            // 
            // txtNodePosX
            // 
            this.txtNodePosX.Location = new System.Drawing.Point(56, 17);
            this.txtNodePosX.Name = "txtNodePosX";
            this.txtNodePosX.ReadOnly = true;
            this.txtNodePosX.Size = new System.Drawing.Size(85, 20);
            this.txtNodePosX.TabIndex = 1;
            // 
            // lblNodePosX
            // 
            this.lblNodePosX.AutoSize = true;
            this.lblNodePosX.Location = new System.Drawing.Point(6, 20);
            this.lblNodePosX.Name = "lblNodePosX";
            this.lblNodePosX.Size = new System.Drawing.Size(35, 13);
            this.lblNodePosX.TabIndex = 0;
            this.lblNodePosX.Text = "Pos X";
            // 
            // grpTrace
            // 
            this.grpTrace.Controls.Add(this.txtTraceFrameEnd);
            this.grpTrace.Controls.Add(this.lblTraceFrameEnd);
            this.grpTrace.Controls.Add(this.txtTraceFrameStart);
            this.grpTrace.Controls.Add(this.lblTraceFrameStart);
            this.grpTrace.Controls.Add(this.comboBox1);
            this.grpTrace.Controls.Add(this.lblTraceTagID);
            this.grpTrace.Controls.Add(this.txtTraceID);
            this.grpTrace.Controls.Add(this.lblTraceID);
            this.grpTrace.Location = new System.Drawing.Point(4, 4);
            this.grpTrace.Name = "grpTrace";
            this.grpTrace.Size = new System.Drawing.Size(147, 115);
            this.grpTrace.TabIndex = 0;
            this.grpTrace.TabStop = false;
            this.grpTrace.Text = "Trace";
            // 
            // txtTraceFrameEnd
            // 
            this.txtTraceFrameEnd.Location = new System.Drawing.Point(83, 85);
            this.txtTraceFrameEnd.Name = "txtTraceFrameEnd";
            this.txtTraceFrameEnd.ReadOnly = true;
            this.txtTraceFrameEnd.Size = new System.Drawing.Size(58, 20);
            this.txtTraceFrameEnd.TabIndex = 7;
            // 
            // lblTraceFrameEnd
            // 
            this.lblTraceFrameEnd.AutoSize = true;
            this.lblTraceFrameEnd.Location = new System.Drawing.Point(6, 88);
            this.lblTraceFrameEnd.Name = "lblTraceFrameEnd";
            this.lblTraceFrameEnd.Size = new System.Drawing.Size(58, 13);
            this.lblTraceFrameEnd.TabIndex = 6;
            this.lblTraceFrameEnd.Text = "Frame End";
            // 
            // txtTraceFrameStart
            // 
            this.txtTraceFrameStart.Location = new System.Drawing.Point(83, 64);
            this.txtTraceFrameStart.Name = "txtTraceFrameStart";
            this.txtTraceFrameStart.ReadOnly = true;
            this.txtTraceFrameStart.Size = new System.Drawing.Size(58, 20);
            this.txtTraceFrameStart.TabIndex = 5;
            // 
            // lblTraceFrameStart
            // 
            this.lblTraceFrameStart.AutoSize = true;
            this.lblTraceFrameStart.Location = new System.Drawing.Point(6, 67);
            this.lblTraceFrameStart.Name = "lblTraceFrameStart";
            this.lblTraceFrameStart.Size = new System.Drawing.Size(61, 13);
            this.lblTraceFrameStart.TabIndex = 4;
            this.lblTraceFrameStart.Text = "Frame Start";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(83, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(58, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // lblTraceTagID
            // 
            this.lblTraceTagID.AutoSize = true;
            this.lblTraceTagID.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblTraceTagID.Location = new System.Drawing.Point(6, 45);
            this.lblTraceTagID.Name = "lblTraceTagID";
            this.lblTraceTagID.Size = new System.Drawing.Size(44, 13);
            this.lblTraceTagID.TabIndex = 2;
            this.lblTraceTagID.Text = "Tag ID*";
            // 
            // txtTraceID
            // 
            this.txtTraceID.Location = new System.Drawing.Point(83, 20);
            this.txtTraceID.Name = "txtTraceID";
            this.txtTraceID.ReadOnly = true;
            this.txtTraceID.Size = new System.Drawing.Size(58, 20);
            this.txtTraceID.TabIndex = 1;
            // 
            // lblTraceID
            // 
            this.lblTraceID.AutoSize = true;
            this.lblTraceID.Location = new System.Drawing.Point(6, 23);
            this.lblTraceID.Name = "lblTraceID";
            this.lblTraceID.Size = new System.Drawing.Size(49, 13);
            this.lblTraceID.TabIndex = 0;
            this.lblTraceID.Text = "Trace ID";
            // 
            // pnlTimeLine
            // 
            this.pnlTimeLine.Controls.Add(this.trbNaviSlider);
            this.pnlTimeLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTimeLine.Location = new System.Drawing.Point(154, 586);
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
            this.ClientSize = new System.Drawing.Size(793, 657);
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
            this.grpCategory.ResumeLayout(false);
            this.grpCategory.PerformLayout();
            this.grpTag.ResumeLayout(false);
            this.grpTag.PerformLayout();
            this.grpNode.ResumeLayout(false);
            this.grpNode.PerformLayout();
            this.grpTrace.ResumeLayout(false);
            this.grpTrace.PerformLayout();
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
        private System.Windows.Forms.GroupBox grpTrace;
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
        private System.Windows.Forms.TextBox txtTraceFrameEnd;
        private System.Windows.Forms.Label lblTraceFrameEnd;
        private System.Windows.Forms.TextBox txtTraceFrameStart;
        private System.Windows.Forms.Label lblTraceFrameStart;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblTraceTagID;
        private System.Windows.Forms.TextBox txtTraceID;
        private System.Windows.Forms.Label lblTraceID;
        private System.Windows.Forms.GroupBox grpCategory;
        private System.Windows.Forms.Button btnCategoryEditSave;
        private System.Windows.Forms.Button btnCategoryDelete;
        private System.Windows.Forms.Button btnCategoryNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCategoryID;
        private System.Windows.Forms.Label lblCategoryID;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.GroupBox grpTag;
        private System.Windows.Forms.Button btnTagDelete;
        private System.Windows.Forms.Button btnTagNew;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label lblTagCategoryID;
        private System.Windows.Forms.Label lblTagID;
        private System.Windows.Forms.GroupBox grpNode;
        private System.Windows.Forms.CheckBox chkNodeIsShaded;
        private System.Windows.Forms.CheckBox chkNodeIsOccluded;
        private System.Windows.Forms.TextBox txtNodeHeight;
        private System.Windows.Forms.Label lblNodeHeight;
        private System.Windows.Forms.TextBox txtNodeWidth;
        private System.Windows.Forms.Label lblNodeWidth;
        private System.Windows.Forms.TextBox txtNodePosY;
        private System.Windows.Forms.Label lblNodePosY;
        private System.Windows.Forms.TextBox txtNodePosX;
        private System.Windows.Forms.Label lblNodePosX;
        private System.Windows.Forms.ComboBox cmbTagID;
        private System.Windows.Forms.Button btnTagEditSave;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblTagName;
    }
}

