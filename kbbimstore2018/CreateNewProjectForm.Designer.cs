namespace KbBimstore
{
    partial class CreateNewProjectForm
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
            System.Windows.Forms.PictureBox pictureBox1;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonCreateProj = new System.Windows.Forms.Button();
            this.buttonSaveConfig = new System.Windows.Forms.Button();
            this.buttonLoadConfig = new System.Windows.Forms.Button();
            this.labelFrontSheetSetup = new System.Windows.Forms.Label();
            this.labelMainSheetSetup = new System.Windows.Forms.Label();
            this.labelDistBetweenLavels = new System.Windows.Forms.Label();
            this.textBoxDistBetweenLavels = new System.Windows.Forms.TextBox();
            this.labelAmountOfLavels = new System.Windows.Forms.Label();
            this.numericUpDownAmountOfLevels = new System.Windows.Forms.NumericUpDown();
            this.labelFrontSheetAmount = new System.Windows.Forms.Label();
            this.numericUpDownFrontSheetAmount = new System.Windows.Forms.NumericUpDown();
            this.labelMainSheetAmount = new System.Windows.Forms.Label();
            this.numericUpDownMainSheetAmount = new System.Windows.Forms.NumericUpDown();
            this.dataGridViewFrontSheet = new System.Windows.Forms.DataGridView();
            this.sheetNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sheetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.draftingViewAssign = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewMainSheet = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chooseTemplate = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.labelSelectReferenceFile = new System.Windows.Forms.Label();
            this.SelectReferenceFile = new System.Windows.Forms.Button();
            this.openRvtFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelSelectTitleBlock = new System.Windows.Forms.Label();
            this.comboBoxSelectTitleBlock = new System.Windows.Forms.ComboBox();
            this.buttonExportConfig = new System.Windows.Forms.Button();
            this.buttonImportConfig = new System.Windows.Forms.Button();
            this.openXmlFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveXmlFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.logopicture = new System.Windows.Forms.PictureBox();
            this.comboBoxChooseScale = new System.Windows.Forms.ComboBox();
            this.labelChooseScale = new System.Windows.Forms.Label();
            this.numericUpDownStartingNumber = new System.Windows.Forms.NumericUpDown();
            this.labelStartingNumber = new System.Windows.Forms.Label();
            this.labelPrefix = new System.Windows.Forms.Label();
            this.labelSuffix = new System.Windows.Forms.Label();
            this.textBoxSuffix = new System.Windows.Forms.TextBox();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.textBoxBetween = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectExcelFileButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.excelFilePathComboBox = new System.Windows.Forms.ComboBox();
            this.openExcelFileDialog = new System.Windows.Forms.OpenFileDialog();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmountOfLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrontSheetAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMainSheetAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFrontSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMainSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logopicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartingNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = global::KbBimstore.Resource.ArrowDirection;
            pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pictureBox1.ErrorImage = global::KbBimstore.Resource.ArrowDirection;
            pictureBox1.InitialImage = global::KbBimstore.Resource.ArrowDirection;
            pictureBox1.Location = new System.Drawing.Point(414, 696);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(196, 20);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 24;
            pictureBox1.TabStop = false;
            // 
            // buttonCreateProj
            // 
            this.buttonCreateProj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateProj.Location = new System.Drawing.Point(353, 725);
            this.buttonCreateProj.Name = "buttonCreateProj";
            this.buttonCreateProj.Size = new System.Drawing.Size(300, 30);
            this.buttonCreateProj.TabIndex = 1;
            this.buttonCreateProj.Text = "CREATE NEW PROJECT";
            this.buttonCreateProj.UseVisualStyleBackColor = true;
            this.buttonCreateProj.Click += new System.EventHandler(this.OnCreateProject);
            // 
            // buttonSaveConfig
            // 
            this.buttonSaveConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveConfig.Location = new System.Drawing.Point(10, 705);
            this.buttonSaveConfig.Name = "buttonSaveConfig";
            this.buttonSaveConfig.Size = new System.Drawing.Size(300, 30);
            this.buttonSaveConfig.TabIndex = 2;
            this.buttonSaveConfig.Text = "SAVE CONFIGURATION";
            this.buttonSaveConfig.UseVisualStyleBackColor = true;
            this.buttonSaveConfig.Click += new System.EventHandler(this.OnSaveConfiguration);
            // 
            // buttonLoadConfig
            // 
            this.buttonLoadConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadConfig.Location = new System.Drawing.Point(706, 705);
            this.buttonLoadConfig.Name = "buttonLoadConfig";
            this.buttonLoadConfig.Size = new System.Drawing.Size(300, 30);
            this.buttonLoadConfig.TabIndex = 4;
            this.buttonLoadConfig.Text = "LOAD CONFIGURATION";
            this.buttonLoadConfig.UseVisualStyleBackColor = true;
            this.buttonLoadConfig.Click += new System.EventHandler(this.OnLoadConfiguration);
            // 
            // labelFrontSheetSetup
            // 
            this.labelFrontSheetSetup.AutoSize = true;
            this.labelFrontSheetSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrontSheetSetup.Location = new System.Drawing.Point(10, 95);
            this.labelFrontSheetSetup.Name = "labelFrontSheetSetup";
            this.labelFrontSheetSetup.Size = new System.Drawing.Size(200, 20);
            this.labelFrontSheetSetup.TabIndex = 5;
            this.labelFrontSheetSetup.Text = "FRONT SHEET SETUP:";
            // 
            // labelMainSheetSetup
            // 
            this.labelMainSheetSetup.AutoSize = true;
            this.labelMainSheetSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMainSheetSetup.Location = new System.Drawing.Point(10, 420);
            this.labelMainSheetSetup.Name = "labelMainSheetSetup";
            this.labelMainSheetSetup.Size = new System.Drawing.Size(185, 20);
            this.labelMainSheetSetup.TabIndex = 6;
            this.labelMainSheetSetup.Text = "MAIN SHEET SETUP:";
            // 
            // labelDistBetweenLavels
            // 
            this.labelDistBetweenLavels.AutoSize = true;
            this.labelDistBetweenLavels.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDistBetweenLavels.Location = new System.Drawing.Point(10, 130);
            this.labelDistBetweenLavels.Name = "labelDistBetweenLavels";
            this.labelDistBetweenLavels.Size = new System.Drawing.Size(232, 17);
            this.labelDistBetweenLavels.TabIndex = 7;
            this.labelDistBetweenLavels.Text = "DISTANCE BETWEEN LEVELS:";
            // 
            // textBoxDistBetweenLavels
            // 
            this.textBoxDistBetweenLavels.Location = new System.Drawing.Point(245, 130);
            this.textBoxDistBetweenLavels.Name = "textBoxDistBetweenLavels";
            this.textBoxDistBetweenLavels.Size = new System.Drawing.Size(100, 20);
            this.textBoxDistBetweenLavels.TabIndex = 8;
            this.textBoxDistBetweenLavels.Text = "0.0";
            this.textBoxDistBetweenLavels.TextChanged += new System.EventHandler(this.OnLevelsDistanceChanged);
            // 
            // labelAmountOfLavels
            // 
            this.labelAmountOfLavels.AutoSize = true;
            this.labelAmountOfLavels.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAmountOfLavels.Location = new System.Drawing.Point(395, 130);
            this.labelAmountOfLavels.Name = "labelAmountOfLavels";
            this.labelAmountOfLavels.Size = new System.Drawing.Size(168, 17);
            this.labelAmountOfLavels.TabIndex = 9;
            this.labelAmountOfLavels.Text = "AMOUNT OF LEVELS:";
            // 
            // numericUpDownAmountOfLevels
            // 
            this.numericUpDownAmountOfLevels.Location = new System.Drawing.Point(566, 130);
            this.numericUpDownAmountOfLevels.Name = "numericUpDownAmountOfLevels";
            this.numericUpDownAmountOfLevels.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownAmountOfLevels.TabIndex = 10;
            this.numericUpDownAmountOfLevels.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAmountOfLevels.ValueChanged += new System.EventHandler(this.OnLevelsAmountChanged);
            // 
            // labelFrontSheetAmount
            // 
            this.labelFrontSheetAmount.AutoSize = true;
            this.labelFrontSheetAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrontSheetAmount.Location = new System.Drawing.Point(730, 130);
            this.labelFrontSheetAmount.Name = "labelFrontSheetAmount";
            this.labelFrontSheetAmount.Size = new System.Drawing.Size(193, 17);
            this.labelFrontSheetAmount.TabIndex = 11;
            this.labelFrontSheetAmount.Text = "FRONT SHEET AMOUNT:";
            // 
            // numericUpDownFrontSheetAmount
            // 
            this.numericUpDownFrontSheetAmount.Location = new System.Drawing.Point(926, 130);
            this.numericUpDownFrontSheetAmount.Name = "numericUpDownFrontSheetAmount";
            this.numericUpDownFrontSheetAmount.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownFrontSheetAmount.TabIndex = 12;
            this.numericUpDownFrontSheetAmount.ValueChanged += new System.EventHandler(this.OnFrontSheetAmountChanged);
            // 
            // labelMainSheetAmount
            // 
            this.labelMainSheetAmount.AutoSize = true;
            this.labelMainSheetAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMainSheetAmount.Location = new System.Drawing.Point(743, 420);
            this.labelMainSheetAmount.Name = "labelMainSheetAmount";
            this.labelMainSheetAmount.Size = new System.Drawing.Size(177, 17);
            this.labelMainSheetAmount.TabIndex = 13;
            this.labelMainSheetAmount.Text = "MAIN SHEET AMOUNT:";
            // 
            // numericUpDownMainSheetAmount
            // 
            this.numericUpDownMainSheetAmount.Location = new System.Drawing.Point(926, 420);
            this.numericUpDownMainSheetAmount.Name = "numericUpDownMainSheetAmount";
            this.numericUpDownMainSheetAmount.ReadOnly = true;
            this.numericUpDownMainSheetAmount.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownMainSheetAmount.TabIndex = 14;
            this.numericUpDownMainSheetAmount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownMainSheetAmount.ValueChanged += new System.EventHandler(this.OnMainSheetAmountChanged);
            // 
            // dataGridViewFrontSheet
            // 
            this.dataGridViewFrontSheet.AllowUserToAddRows = false;
            this.dataGridViewFrontSheet.AllowUserToDeleteRows = false;
            this.dataGridViewFrontSheet.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFrontSheet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewFrontSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFrontSheet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sheetNumber,
            this.sheetName,
            this.draftingViewAssign});
            this.dataGridViewFrontSheet.Location = new System.Drawing.Point(14, 155);
            this.dataGridViewFrontSheet.Name = "dataGridViewFrontSheet";
            this.dataGridViewFrontSheet.RowHeadersVisible = false;
            this.dataGridViewFrontSheet.Size = new System.Drawing.Size(992, 250);
            this.dataGridViewFrontSheet.TabIndex = 15;
            this.dataGridViewFrontSheet.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFrontSheet_CellValidated);
            this.dataGridViewFrontSheet.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewFrontSheet_CellValidating);
            this.dataGridViewFrontSheet.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewFrontSheet_DataError);
            this.dataGridViewFrontSheet.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewFrontSheet_EditingControlShowing);
            this.dataGridViewFrontSheet.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataGridViewFrontSheet_PreviewKeyDown);
            // 
            // sheetNumber
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.sheetNumber.DefaultCellStyle = dataGridViewCellStyle2;
            this.sheetNumber.HeaderText = "SHEET NUMBER";
            this.sheetNumber.Name = "sheetNumber";
            this.sheetNumber.Width = 150;
            // 
            // sheetName
            // 
            this.sheetName.HeaderText = "SHEET NAME";
            this.sheetName.Name = "sheetName";
            this.sheetName.Width = 250;
            // 
            // draftingViewAssign
            // 
            this.draftingViewAssign.AutoComplete = false;
            this.draftingViewAssign.HeaderText = "DRAFTING VIEW ASSIGNMENT";
            this.draftingViewAssign.Name = "draftingViewAssign";
            this.draftingViewAssign.Width = 588;
            // 
            // dataGridViewMainSheet
            // 
            this.dataGridViewMainSheet.AllowUserToAddRows = false;
            this.dataGridViewMainSheet.AllowUserToDeleteRows = false;
            this.dataGridViewMainSheet.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMainSheet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewMainSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMainSheet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.chooseTemplate});
            this.dataGridViewMainSheet.Location = new System.Drawing.Point(14, 480);
            this.dataGridViewMainSheet.Name = "dataGridViewMainSheet";
            this.dataGridViewMainSheet.RowHeadersVisible = false;
            this.dataGridViewMainSheet.Size = new System.Drawing.Size(992, 215);
            this.dataGridViewMainSheet.TabIndex = 16;
            this.dataGridViewMainSheet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMainSheet_CellContentClick);
            this.dataGridViewMainSheet.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMainSheet_CellValidated);
            this.dataGridViewMainSheet.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewMainSheet_CellValidating);
            this.dataGridViewMainSheet.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewMainSheet_DataError);
            this.dataGridViewMainSheet.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewMainSheet_EditingControlShowing);
            this.dataGridViewMainSheet.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataGridViewMainSheet_PreviewKeyDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "SHEET NUMBER";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "SHEET NAME";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // chooseTemplate
            // 
            this.chooseTemplate.AutoComplete = false;
            this.chooseTemplate.HeaderText = "CHOOSE TEMPLATE";
            this.chooseTemplate.Name = "chooseTemplate";
            this.chooseTemplate.Width = 588;
            // 
            // labelSelectReferenceFile
            // 
            this.labelSelectReferenceFile.AutoSize = true;
            this.labelSelectReferenceFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectReferenceFile.Location = new System.Drawing.Point(242, 98);
            this.labelSelectReferenceFile.Name = "labelSelectReferenceFile";
            this.labelSelectReferenceFile.Size = new System.Drawing.Size(235, 17);
            this.labelSelectReferenceFile.TabIndex = 18;
            this.labelSelectReferenceFile.Text = "SELECT REFERENCE .rvt FILE:";
            // 
            // SelectReferenceFile
            // 
            this.SelectReferenceFile.Location = new System.Drawing.Point(482, 95);
            this.SelectReferenceFile.Name = "SelectReferenceFile";
            this.SelectReferenceFile.Size = new System.Drawing.Size(82, 23);
            this.SelectReferenceFile.TabIndex = 19;
            this.SelectReferenceFile.Text = "SELECT";
            this.SelectReferenceFile.UseVisualStyleBackColor = true;
            this.SelectReferenceFile.Click += new System.EventHandler(this.SelectReferenceFile_Click);
            // 
            // openRvtFileDialog
            // 
            this.openRvtFileDialog.Filter = "*.rvt|*.RVT";
            this.openRvtFileDialog.Title = "SELECT REFERENCE .rvt FILE";
            // 
            // labelSelectTitleBlock
            // 
            this.labelSelectTitleBlock.AutoSize = true;
            this.labelSelectTitleBlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectTitleBlock.Location = new System.Drawing.Point(585, 98);
            this.labelSelectTitleBlock.Name = "labelSelectTitleBlock";
            this.labelSelectTitleBlock.Size = new System.Drawing.Size(171, 17);
            this.labelSelectTitleBlock.TabIndex = 20;
            this.labelSelectTitleBlock.Text = "SELECT TITLEBLOCK:";
            // 
            // comboBoxSelectTitleBlock
            // 
            this.comboBoxSelectTitleBlock.FormattingEnabled = true;
            this.comboBoxSelectTitleBlock.Location = new System.Drawing.Point(756, 97);
            this.comboBoxSelectTitleBlock.Name = "comboBoxSelectTitleBlock";
            this.comboBoxSelectTitleBlock.Size = new System.Drawing.Size(250, 21);
            this.comboBoxSelectTitleBlock.TabIndex = 21;
            this.comboBoxSelectTitleBlock.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectTitleBlock_SelectedIndexChanged);
            // 
            // buttonExportConfig
            // 
            this.buttonExportConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExportConfig.Location = new System.Drawing.Point(10, 740);
            this.buttonExportConfig.Name = "buttonExportConfig";
            this.buttonExportConfig.Size = new System.Drawing.Size(300, 30);
            this.buttonExportConfig.TabIndex = 22;
            this.buttonExportConfig.Text = "EXPORT CONFIGURATION";
            this.buttonExportConfig.UseVisualStyleBackColor = true;
            this.buttonExportConfig.Click += new System.EventHandler(this.buttonExportConfig_Click);
            // 
            // buttonImportConfig
            // 
            this.buttonImportConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImportConfig.Location = new System.Drawing.Point(706, 740);
            this.buttonImportConfig.Name = "buttonImportConfig";
            this.buttonImportConfig.Size = new System.Drawing.Size(300, 30);
            this.buttonImportConfig.TabIndex = 23;
            this.buttonImportConfig.Text = "IMPORT CONFIGURATION";
            this.buttonImportConfig.UseVisualStyleBackColor = true;
            this.buttonImportConfig.Click += new System.EventHandler(this.buttonImportConfig_Click);
            // 
            // openXmlFileDialog
            // 
            this.openXmlFileDialog.Filter = "*.xml|*.XML";
            this.openXmlFileDialog.Title = "OPEN CONFIGURATION .xml FILE";
            // 
            // saveXmlFileDialog
            // 
            this.saveXmlFileDialog.Filter = "*.xml|*.XML";
            this.saveXmlFileDialog.Title = "SAVE CONFIGURATION .xml FILE";
            // 
            // logopicture
            // 
            this.logopicture.BackgroundImage = global::KbBimstore.Resource.Create_Project;
            this.logopicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logopicture.Location = new System.Drawing.Point(209, 6);
            this.logopicture.Name = "logopicture";
            this.logopicture.Size = new System.Drawing.Size(640, 80);
            this.logopicture.TabIndex = 17;
            this.logopicture.TabStop = false;
            // 
            // comboBoxChooseScale
            // 
            this.comboBoxChooseScale.FormattingEnabled = true;
            this.comboBoxChooseScale.Location = new System.Drawing.Point(610, 419);
            this.comboBoxChooseScale.Name = "comboBoxChooseScale";
            this.comboBoxChooseScale.Size = new System.Drawing.Size(110, 21);
            this.comboBoxChooseScale.TabIndex = 25;
            // 
            // labelChooseScale
            // 
            this.labelChooseScale.AutoSize = true;
            this.labelChooseScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChooseScale.Location = new System.Drawing.Point(478, 420);
            this.labelChooseScale.Name = "labelChooseScale";
            this.labelChooseScale.Size = new System.Drawing.Size(127, 17);
            this.labelChooseScale.TabIndex = 26;
            this.labelChooseScale.Text = "CHOOSE SCALE";
            // 
            // numericUpDownStartingNumber
            // 
            this.numericUpDownStartingNumber.Location = new System.Drawing.Point(370, 419);
            this.numericUpDownStartingNumber.Name = "numericUpDownStartingNumber";
            this.numericUpDownStartingNumber.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownStartingNumber.TabIndex = 27;
            this.numericUpDownStartingNumber.ValueChanged += new System.EventHandler(this.numericUpDownStartingNumber_ValueChanged);
            // 
            // labelStartingNumber
            // 
            this.labelStartingNumber.AutoSize = true;
            this.labelStartingNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStartingNumber.Location = new System.Drawing.Point(208, 422);
            this.labelStartingNumber.Name = "labelStartingNumber";
            this.labelStartingNumber.Size = new System.Drawing.Size(156, 17);
            this.labelStartingNumber.TabIndex = 28;
            this.labelStartingNumber.Text = "STARTING NUMBER";
            // 
            // labelPrefix
            // 
            this.labelPrefix.AutoSize = true;
            this.labelPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrefix.Location = new System.Drawing.Point(21, 454);
            this.labelPrefix.Name = "labelPrefix";
            this.labelPrefix.Size = new System.Drawing.Size(62, 17);
            this.labelPrefix.TabIndex = 30;
            this.labelPrefix.Text = "PREFIX";
            this.labelPrefix.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelSuffix
            // 
            this.labelSuffix.AutoSize = true;
            this.labelSuffix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSuffix.Location = new System.Drawing.Point(479, 451);
            this.labelSuffix.Name = "labelSuffix";
            this.labelSuffix.Size = new System.Drawing.Size(61, 17);
            this.labelSuffix.TabIndex = 32;
            this.labelSuffix.Text = "SUFFIX";
            // 
            // textBoxSuffix
            // 
            this.textBoxSuffix.Location = new System.Drawing.Point(546, 451);
            this.textBoxSuffix.Name = "textBoxSuffix";
            this.textBoxSuffix.Size = new System.Drawing.Size(100, 20);
            this.textBoxSuffix.TabIndex = 33;
            this.textBoxSuffix.Text = ".00";
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Location = new System.Drawing.Point(85, 454);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(100, 20);
            this.textBoxPrefix.TabIndex = 34;
            this.textBoxPrefix.Text = "A-";
            // 
            // textBoxBetween
            // 
            this.textBoxBetween.Location = new System.Drawing.Point(353, 450);
            this.textBoxBetween.Name = "textBoxBetween";
            this.textBoxBetween.Size = new System.Drawing.Size(100, 20);
            this.textBoxBetween.TabIndex = 35;
            this.textBoxBetween.Text = "0";
            this.textBoxBetween.TextChanged += new System.EventHandler(this.textBoxBetween_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.label1.Location = new System.Drawing.Point(206, 452);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 17);
            this.label1.TabIndex = 36;
            this.label1.Text = "BETWEEN VALUE";
            // 
            // selectExcelFileButton
            // 
            this.selectExcelFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectExcelFileButton.Location = new System.Drawing.Point(973, 449);
            this.selectExcelFileButton.Name = "selectExcelFileButton";
            this.selectExcelFileButton.Size = new System.Drawing.Size(31, 24);
            this.selectExcelFileButton.TabIndex = 42;
            this.selectExcelFileButton.Text = "...";
            this.selectExcelFileButton.UseVisualStyleBackColor = true;
            this.selectExcelFileButton.Click += new System.EventHandler(this.selectExcelFileButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(661, 452);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 41;
            this.label2.Text = "EXCEL";
            // 
            // excelFilePathComboBox
            // 
            this.excelFilePathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.excelFilePathComboBox.FormattingEnabled = true;
            this.excelFilePathComboBox.Location = new System.Drawing.Point(724, 450);
            this.excelFilePathComboBox.Name = "excelFilePathComboBox";
            this.excelFilePathComboBox.Size = new System.Drawing.Size(243, 21);
            this.excelFilePathComboBox.TabIndex = 40;
            this.excelFilePathComboBox.SelectedIndexChanged += new System.EventHandler(this.excelFilePathComboBox_SelectedIndexChanged);
            // 
            // openExcelFileDialog
            // 
            this.openExcelFileDialog.Filter = "Excel files | *.xlsx; *xls";
            // 
            // CreateNewProjectForm
            // 
            this.AcceptButton = this.buttonCreateProj;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 761);
            this.Controls.Add(this.selectExcelFileButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.excelFilePathComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxBetween);
            this.Controls.Add(this.textBoxPrefix);
            this.Controls.Add(this.textBoxSuffix);
            this.Controls.Add(this.labelSuffix);
            this.Controls.Add(this.labelPrefix);
            this.Controls.Add(this.labelStartingNumber);
            this.Controls.Add(this.numericUpDownStartingNumber);
            this.Controls.Add(this.labelChooseScale);
            this.Controls.Add(this.comboBoxChooseScale);
            this.Controls.Add(pictureBox1);
            this.Controls.Add(this.buttonImportConfig);
            this.Controls.Add(this.buttonExportConfig);
            this.Controls.Add(this.comboBoxSelectTitleBlock);
            this.Controls.Add(this.labelSelectTitleBlock);
            this.Controls.Add(this.SelectReferenceFile);
            this.Controls.Add(this.labelSelectReferenceFile);
            this.Controls.Add(this.logopicture);
            this.Controls.Add(this.dataGridViewMainSheet);
            this.Controls.Add(this.dataGridViewFrontSheet);
            this.Controls.Add(this.numericUpDownMainSheetAmount);
            this.Controls.Add(this.labelMainSheetAmount);
            this.Controls.Add(this.numericUpDownFrontSheetAmount);
            this.Controls.Add(this.labelFrontSheetAmount);
            this.Controls.Add(this.numericUpDownAmountOfLevels);
            this.Controls.Add(this.labelAmountOfLavels);
            this.Controls.Add(this.textBoxDistBetweenLavels);
            this.Controls.Add(this.labelDistBetweenLavels);
            this.Controls.Add(this.labelMainSheetSetup);
            this.Controls.Add(this.labelFrontSheetSetup);
            this.Controls.Add(this.buttonLoadConfig);
            this.Controls.Add(this.buttonSaveConfig);
            this.Controls.Add(this.buttonCreateProj);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 800);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 800);
            this.Name = "CreateNewProjectForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "NEW PROJECT";
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmountOfLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrontSheetAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMainSheetAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFrontSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMainSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logopicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartingNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateProj;
        private System.Windows.Forms.Button buttonSaveConfig;
        private System.Windows.Forms.Button buttonLoadConfig;
        private System.Windows.Forms.Label labelFrontSheetSetup;
        private System.Windows.Forms.Label labelMainSheetSetup;
        private System.Windows.Forms.Label labelDistBetweenLavels;
        private System.Windows.Forms.TextBox textBoxDistBetweenLavels;
        private System.Windows.Forms.Label labelAmountOfLavels;
        private System.Windows.Forms.NumericUpDown numericUpDownAmountOfLevels;
        private System.Windows.Forms.Label labelFrontSheetAmount;
        private System.Windows.Forms.NumericUpDown numericUpDownFrontSheetAmount;
        private System.Windows.Forms.Label labelMainSheetAmount;
        private System.Windows.Forms.NumericUpDown numericUpDownMainSheetAmount;
        private System.Windows.Forms.DataGridView dataGridViewFrontSheet;
        private System.Windows.Forms.DataGridView dataGridViewMainSheet;
        private System.Windows.Forms.PictureBox logopicture;
        private System.Windows.Forms.Label labelSelectReferenceFile;
        private System.Windows.Forms.Button SelectReferenceFile;
        private System.Windows.Forms.OpenFileDialog openRvtFileDialog;
        private System.Windows.Forms.Label labelSelectTitleBlock;
        private System.Windows.Forms.ComboBox comboBoxSelectTitleBlock;
        private System.Windows.Forms.DataGridViewTextBoxColumn sheetNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn sheetName;
        private System.Windows.Forms.DataGridViewComboBoxColumn draftingViewAssign;
        private System.Windows.Forms.Button buttonExportConfig;
        private System.Windows.Forms.Button buttonImportConfig;
        private System.Windows.Forms.OpenFileDialog openXmlFileDialog;
        private System.Windows.Forms.SaveFileDialog saveXmlFileDialog;
        private System.Windows.Forms.ComboBox comboBoxChooseScale;
        private System.Windows.Forms.Label labelChooseScale;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewComboBoxColumn chooseTemplate;
        private System.Windows.Forms.NumericUpDown numericUpDownStartingNumber;
        private System.Windows.Forms.Label labelStartingNumber;
        private System.Windows.Forms.Label labelPrefix;
        private System.Windows.Forms.Label labelSuffix;
        private System.Windows.Forms.TextBox textBoxSuffix;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.TextBox textBoxBetween;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selectExcelFileButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox excelFilePathComboBox;
        private System.Windows.Forms.OpenFileDialog openExcelFileDialog;
    }
}