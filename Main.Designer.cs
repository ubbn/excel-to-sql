namespace XLS2SQL_Converter
{
    partial class frmMain
    {
        /// <summary>
        /// Mandatory designer variable
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean used resources.
        /// </summary>
        protected override void Dispose(bool dispose)
        {
            if (dispose && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(dispose);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Mandatory method for Designer support
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtExcel = new System.Windows.Forms.TextBox();
            this.dgvColumns = new System.Windows.Forms.DataGridView();
            this.SheetColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SQLColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SqlColumnNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SqlColumnPK = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.gbOpcoes = new System.Windows.Forms.GroupBox();
            this.btnUploadProfile = new System.Windows.Forms.Button();
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.cbEmptyNULL = new System.Windows.Forms.CheckBox();
            this.rbData = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbxTypeDeleteAll = new System.Windows.Forms.CheckBox();
            this.ckbxTypeTruncate = new System.Windows.Forms.CheckBox();
            this.ckbxTypeSelect = new System.Windows.Forms.CheckBox();
            this.ckbxTypeInsert = new System.Windows.Forms.CheckBox();
            this.ckbxTypeDelete = new System.Windows.Forms.CheckBox();
            this.ckbxTypeUpdate = new System.Windows.Forms.CheckBox();
            this.gbProc = new System.Windows.Forms.GroupBox();
            this.ckbxProcTypeList = new System.Windows.Forms.CheckBox();
            this.ckbxProcTypeSelect = new System.Windows.Forms.CheckBox();
            this.ckbxProcTypeInsert = new System.Windows.Forms.CheckBox();
            this.ckbxProcTypeDelete = new System.Windows.Forms.CheckBox();
            this.rbProc = new System.Windows.Forms.RadioButton();
            this.ckbxProcTypeUpdate = new System.Windows.Forms.CheckBox();
            this.cbFileSplit = new System.Windows.Forms.CheckBox();
            this.ckbxCreateTable = new System.Windows.Forms.CheckBox();
            this.ckbxDropIfExists = new System.Windows.Forms.CheckBox();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.CheckExport = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RowColumnsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnCopyRowColumnName = new System.Windows.Forms.DataGridViewButtonColumn();
            this.SheetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sfdSQLFile = new System.Windows.Forms.SaveFileDialog();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ofdExcel = new System.Windows.Forms.OpenFileDialog();
            this.btnExporter = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.sfdProfile = new System.Windows.Forms.SaveFileDialog();
            this.ofdProfile = new System.Windows.Forms.OpenFileDialog();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            this.gbOpcoes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbProc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.SuspendLayout();
            // 
            // txtExcel
            // 
            this.txtExcel.AllowDrop = true;
            this.txtExcel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcel.Location = new System.Drawing.Point(12, 29);
            this.txtExcel.Name = "txtExcel";
            this.txtExcel.Size = new System.Drawing.Size(694, 20);
            this.txtExcel.TabIndex = 2;
            this.txtExcel.Text = "Choose a excel file...";
            // 
            // dgvColumns
            // 
            this.dgvColumns.AllowUserToAddRows = false;
            this.dgvColumns.AllowUserToDeleteRows = false;
            this.dgvColumns.AllowUserToResizeRows = false;
            this.dgvColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SheetColumnName,
            this.SQLColumnName,
            this.DataType,
            this.SqlColumnNull,
            this.SqlColumnPK});
            this.dgvColumns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvColumns.Location = new System.Drawing.Point(12, 315);
            this.dgvColumns.Name = "dgvColumns";
            this.dgvColumns.RowHeadersVisible = false;
            this.dgvColumns.Size = new System.Drawing.Size(694, 250);
            this.dgvColumns.TabIndex = 1;
            this.dgvColumns.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvColumns_CellValidate);
            this.dgvColumns.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvColumns_CellValueChanged);
            this.dgvColumns.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvColumns_CurrentCellDirtyStateChanged);
            this.dgvColumns.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvColumns_EditingControls);
            // 
            // SheetColumnName
            // 
            this.SheetColumnName.HeaderText = "Sheet Column Name";
            this.SheetColumnName.Name = "SheetColumnName";
            this.SheetColumnName.ReadOnly = true;
            this.SheetColumnName.Width = 270;
            // 
            // SQLColumnName
            // 
            this.SQLColumnName.HeaderText = "SQL Column Name";
            this.SQLColumnName.Name = "SQLColumnName";
            this.SQLColumnName.Width = 270;
            // 
            // DataType
            // 
            this.DataType.DisplayStyleForCurrentCellOnly = true;
            this.DataType.HeaderText = "Data Type";
            this.DataType.Items.AddRange(new object[] {
            "[BIGINT]",
            "[INT]",
            "[DECIMAL]",
            "[DATE]",
            "[DATETIME]",
            "[TEXT]",
            "[VARCHAR](1024)",
            "[CHAR](1)",
            "[BINARY]",
            "[TIMESTAMP]"});
            this.DataType.Name = "DataType";
            this.DataType.Width = 180;
            // 
            // SqlColumnNull
            // 
            this.SqlColumnNull.FalseValue = "False";
            this.SqlColumnNull.HeaderText = "NULL";
            this.SqlColumnNull.IndeterminateValue = "False";
            this.SqlColumnNull.Name = "SqlColumnNull";
            this.SqlColumnNull.TrueValue = "True";
            this.SqlColumnNull.Width = 50;
            // 
            // SqlColumnPK
            // 
            this.SqlColumnPK.FalseValue = "False";
            this.SqlColumnPK.HeaderText = "PK";
            this.SqlColumnPK.IndeterminateValue = "False";
            this.SqlColumnPK.Name = "SqlColumnPK";
            this.SqlColumnPK.TrueValue = "True";
            this.SqlColumnPK.Width = 50;
            // 
            // gbOpcoes
            // 
            this.gbOpcoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOpcoes.Controls.Add(this.btnUploadProfile);
            this.gbOpcoes.Controls.Add(this.btnSaveProfile);
            this.gbOpcoes.Controls.Add(this.cbEmptyNULL);
            this.gbOpcoes.Controls.Add(this.rbData);
            this.gbOpcoes.Controls.Add(this.rbProc);
            this.gbOpcoes.Controls.Add(this.groupBox1);
            this.gbOpcoes.Controls.Add(this.gbProc);
            this.gbOpcoes.Controls.Add(this.cbFileSplit);
            this.gbOpcoes.Controls.Add(this.ckbxCreateTable);
            this.gbOpcoes.Controls.Add(this.ckbxDropIfExists);
            this.gbOpcoes.Location = new System.Drawing.Point(712, 59);
            this.gbOpcoes.Name = "gbOpcoes";
            this.gbOpcoes.Size = new System.Drawing.Size(288, 379);
            this.gbOpcoes.TabIndex = 2;
            this.gbOpcoes.TabStop = false;
            this.gbOpcoes.Text = "Conversion Settings";
            // 
            // btnUploadProfile
            // 
            this.btnUploadProfile.Location = new System.Drawing.Point(6, 350);
            this.btnUploadProfile.Name = "btnUploadProfile";
            this.btnUploadProfile.Size = new System.Drawing.Size(276, 23);
            this.btnUploadProfile.TabIndex = 20;
            this.btnUploadProfile.Text = "Load Profile";
            this.btnUploadProfile.UseVisualStyleBackColor = true;
            this.btnUploadProfile.Click += new System.EventHandler(this.btnUploadProfile_Click);
            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.Location = new System.Drawing.Point(6, 321);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(276, 23);
            this.btnSaveProfile.TabIndex = 19;
            this.btnSaveProfile.Text = "Save Profile";
            this.btnSaveProfile.UseVisualStyleBackColor = true;
            this.btnSaveProfile.Click += new System.EventHandler(this.btnSaveProfile_Click);
            // 
            // cbEmptyNULL
            // 
            this.cbEmptyNULL.AutoSize = true;
            this.cbEmptyNULL.Location = new System.Drawing.Point(11, 267);
            this.cbEmptyNULL.Name = "cbEmptyNULL";
            this.cbEmptyNULL.Size = new System.Drawing.Size(146, 17);
            this.cbEmptyNULL.TabIndex = 16;
            this.cbEmptyNULL.Text = "Suppress empty by NULL";
            this.cbEmptyNULL.UseVisualStyleBackColor = true;
            // 
            // rbData
            // 
            this.rbData.AutoSize = true;
            this.rbData.Location = new System.Drawing.Point(11, 31);
            this.rbData.Name = "rbData";
            this.rbData.Size = new System.Drawing.Size(113, 17);
            this.rbData.TabIndex = 2;
            this.rbData.TabStop = true;
            this.rbData.Tag = "";
            this.rbData.Text = "Data Management";
            this.rbData.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbxTypeDeleteAll);
            this.groupBox1.Controls.Add(this.ckbxTypeTruncate);
            this.groupBox1.Controls.Add(this.ckbxTypeSelect);
            this.groupBox1.Controls.Add(this.ckbxTypeInsert);
            this.groupBox1.Controls.Add(this.ckbxTypeDelete);
            this.groupBox1.Controls.Add(this.ckbxTypeUpdate);
            this.groupBox1.Location = new System.Drawing.Point(9, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 172);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // ckbxTypeDeleteAll
            // 
            this.ckbxTypeDeleteAll.AutoSize = true;
            this.ckbxTypeDeleteAll.Location = new System.Drawing.Point(23, 141);
            this.ckbxTypeDeleteAll.Name = "ckbxTypeDeleteAll";
            this.ckbxTypeDeleteAll.Size = new System.Drawing.Size(90, 17);
            this.ckbxTypeDeleteAll.TabIndex = 9;
            this.ckbxTypeDeleteAll.Text = "DELETE ALL";
            this.ckbxTypeDeleteAll.UseVisualStyleBackColor = true;
            // 
            // ckbxTypeTruncate
            // 
            this.ckbxTypeTruncate.AutoSize = true;
            this.ckbxTypeTruncate.Location = new System.Drawing.Point(23, 71);
            this.ckbxTypeTruncate.Name = "ckbxTypeTruncate";
            this.ckbxTypeTruncate.Size = new System.Drawing.Size(85, 17);
            this.ckbxTypeTruncate.TabIndex = 6;
            this.ckbxTypeTruncate.Text = "TRUNCATE";
            this.ckbxTypeTruncate.UseVisualStyleBackColor = true;
            // 
            // ckbxTypeSelect
            // 
            this.ckbxTypeSelect.AutoSize = true;
            this.ckbxTypeSelect.Location = new System.Drawing.Point(23, 25);
            this.ckbxTypeSelect.Name = "ckbxTypeSelect";
            this.ckbxTypeSelect.Size = new System.Drawing.Size(67, 17);
            this.ckbxTypeSelect.TabIndex = 4;
            this.ckbxTypeSelect.Text = "SELECT";
            this.ckbxTypeSelect.UseVisualStyleBackColor = true;
            // 
            // ckbxTypeInsert
            // 
            this.ckbxTypeInsert.AutoSize = true;
            this.ckbxTypeInsert.Location = new System.Drawing.Point(23, 48);
            this.ckbxTypeInsert.Name = "ckbxTypeInsert";
            this.ckbxTypeInsert.Size = new System.Drawing.Size(66, 17);
            this.ckbxTypeInsert.TabIndex = 5;
            this.ckbxTypeInsert.Text = "INSERT";
            this.ckbxTypeInsert.UseVisualStyleBackColor = true;
            // 
            // ckbxTypeDelete
            // 
            this.ckbxTypeDelete.AutoSize = true;
            this.ckbxTypeDelete.Location = new System.Drawing.Point(23, 117);
            this.ckbxTypeDelete.Name = "ckbxTypeDelete";
            this.ckbxTypeDelete.Size = new System.Drawing.Size(68, 17);
            this.ckbxTypeDelete.TabIndex = 8;
            this.ckbxTypeDelete.Text = "DELETE";
            this.ckbxTypeDelete.UseVisualStyleBackColor = true;
            // 
            // ckbxTypeUpdate
            // 
            this.ckbxTypeUpdate.AutoSize = true;
            this.ckbxTypeUpdate.Location = new System.Drawing.Point(23, 94);
            this.ckbxTypeUpdate.Name = "ckbxTypeUpdate";
            this.ckbxTypeUpdate.Size = new System.Drawing.Size(70, 17);
            this.ckbxTypeUpdate.TabIndex = 7;
            this.ckbxTypeUpdate.Text = "UPDATE";
            this.ckbxTypeUpdate.UseVisualStyleBackColor = true;
            // 
            // gbProc
            // 
            this.gbProc.Controls.Add(this.ckbxProcTypeList);
            this.gbProc.Controls.Add(this.ckbxProcTypeSelect);
            this.gbProc.Controls.Add(this.ckbxProcTypeInsert);
            this.gbProc.Controls.Add(this.ckbxProcTypeDelete);
            this.gbProc.Controls.Add(this.ckbxProcTypeUpdate);
            this.gbProc.Location = new System.Drawing.Point(158, 44);
            this.gbProc.Name = "gbProc";
            this.gbProc.Size = new System.Drawing.Size(116, 172);
            this.gbProc.TabIndex = 7;
            this.gbProc.TabStop = false;
            // 
            // ckbxProcTypeList
            // 
            this.ckbxProcTypeList.AutoSize = true;
            this.ckbxProcTypeList.Location = new System.Drawing.Point(23, 72);
            this.ckbxProcTypeList.Name = "ckbxProcTypeList";
            this.ckbxProcTypeList.Size = new System.Drawing.Size(49, 17);
            this.ckbxProcTypeList.TabIndex = 12;
            this.ckbxProcTypeList.Text = "LIST";
            this.ckbxProcTypeList.UseVisualStyleBackColor = true;
            // 
            // ckbxProcTypeSelect
            // 
            this.ckbxProcTypeSelect.AutoSize = true;
            this.ckbxProcTypeSelect.Location = new System.Drawing.Point(23, 25);
            this.ckbxProcTypeSelect.Name = "ckbxProcTypeSelect";
            this.ckbxProcTypeSelect.Size = new System.Drawing.Size(67, 17);
            this.ckbxProcTypeSelect.TabIndex = 10;
            this.ckbxProcTypeSelect.Text = "SELECT";
            this.ckbxProcTypeSelect.UseVisualStyleBackColor = true;
            // 
            // ckbxProcTypeInsert
            // 
            this.ckbxProcTypeInsert.AutoSize = true;
            this.ckbxProcTypeInsert.Location = new System.Drawing.Point(23, 48);
            this.ckbxProcTypeInsert.Name = "ckbxProcTypeInsert";
            this.ckbxProcTypeInsert.Size = new System.Drawing.Size(66, 17);
            this.ckbxProcTypeInsert.TabIndex = 11;
            this.ckbxProcTypeInsert.Text = "INSERT";
            this.ckbxProcTypeInsert.UseVisualStyleBackColor = true;
            // 
            // ckbxProcTypeDelete
            // 
            this.ckbxProcTypeDelete.AutoSize = true;
            this.ckbxProcTypeDelete.Location = new System.Drawing.Point(23, 118);
            this.ckbxProcTypeDelete.Name = "ckbxProcTypeDelete";
            this.ckbxProcTypeDelete.Size = new System.Drawing.Size(68, 17);
            this.ckbxProcTypeDelete.TabIndex = 14;
            this.ckbxProcTypeDelete.Text = "DELETE";
            this.ckbxProcTypeDelete.UseVisualStyleBackColor = true;
            // 
            // rbProc
            // 
            this.rbProc.AutoSize = true;
            this.rbProc.Checked = true;
            this.rbProc.Location = new System.Drawing.Point(160, 31);
            this.rbProc.Name = "rbProc";
            this.rbProc.Size = new System.Drawing.Size(108, 17);
            this.rbProc.TabIndex = 3;
            this.rbProc.TabStop = true;
            this.rbProc.Tag = "";
            this.rbProc.Text = "Create Procedure";
            this.rbProc.UseVisualStyleBackColor = true;
            // 
            // ckbxProcTypeUpdate
            // 
            this.ckbxProcTypeUpdate.AutoSize = true;
            this.ckbxProcTypeUpdate.Location = new System.Drawing.Point(23, 95);
            this.ckbxProcTypeUpdate.Name = "ckbxProcTypeUpdate";
            this.ckbxProcTypeUpdate.Size = new System.Drawing.Size(70, 17);
            this.ckbxProcTypeUpdate.TabIndex = 13;
            this.ckbxProcTypeUpdate.Text = "UPDATE";
            this.ckbxProcTypeUpdate.UseVisualStyleBackColor = true;
            // 
            // cbFileSplit
            // 
            this.cbFileSplit.AutoSize = true;
            this.cbFileSplit.Location = new System.Drawing.Point(11, 244);
            this.cbFileSplit.Name = "cbFileSplit";
            this.cbFileSplit.Size = new System.Drawing.Size(126, 17);
            this.cbFileSplit.TabIndex = 15;
            this.cbFileSplit.Text = "Create a file per table";
            this.cbFileSplit.UseVisualStyleBackColor = true;
            // 
            // ckbxCreateTable
            // 
            this.ckbxCreateTable.AutoSize = true;
            this.ckbxCreateTable.Location = new System.Drawing.Point(174, 267);
            this.ckbxCreateTable.Name = "ckbxCreateTable";
            this.ckbxCreateTable.Size = new System.Drawing.Size(87, 17);
            this.ckbxCreateTable.TabIndex = 18;
            this.ckbxCreateTable.Text = "Create Table";
            this.ckbxCreateTable.UseVisualStyleBackColor = true;
            // 
            // ckbxDropIfExists
            // 
            this.ckbxDropIfExists.AutoSize = true;
            this.ckbxDropIfExists.Location = new System.Drawing.Point(174, 244);
            this.ckbxDropIfExists.Name = "ckbxDropIfExists";
            this.ckbxDropIfExists.Size = new System.Drawing.Size(86, 17);
            this.ckbxDropIfExists.TabIndex = 17;
            this.ckbxDropIfExists.Text = "Drop if exists";
            this.ckbxDropIfExists.UseVisualStyleBackColor = true;
            // 
            // dgvTable
            // 
            this.dgvTable.AllowUserToAddRows = false;
            this.dgvTable.AllowUserToDeleteRows = false;
            this.dgvTable.AllowUserToResizeRows = false;
            this.dgvTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckExport,
            this.RowColumnsName,
            this.BtnCopyRowColumnName,
            this.SheetName,
            this.TableName});
            this.dgvTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvTable.Enabled = false;
            this.dgvTable.Location = new System.Drawing.Point(12, 59);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.RowHeadersVisible = false;
            this.dgvTable.Size = new System.Drawing.Size(694, 250);
            this.dgvTable.TabIndex = 3;
            this.dgvTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTable_CellContentClick);
            this.dgvTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTable_CellValueChanged);
            this.dgvTable.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvTable_CurrentCellDirtyStateChanged);
            // 
            // CheckExport
            // 
            this.CheckExport.FalseValue = "False";
            this.CheckExport.HeaderText = "";
            this.CheckExport.IndeterminateValue = "False";
            this.CheckExport.Name = "CheckExport";
            this.CheckExport.TrueValue = "True";
            this.CheckExport.Width = 30;
            // 
            // RowColumnsName
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RowColumnsName.DefaultCellStyle = dataGridViewCellStyle1;
            this.RowColumnsName.HeaderText = "Row Columns Name";
            this.RowColumnsName.Name = "RowColumnsName";
            this.RowColumnsName.Width = 150;
            // 
            // BtnCopyRowColumnName
            // 
            this.BtnCopyRowColumnName.HeaderText = "";
            this.BtnCopyRowColumnName.Name = "BtnCopyRowColumnName";
            this.BtnCopyRowColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BtnCopyRowColumnName.Text = "Copy";
            this.BtnCopyRowColumnName.UseColumnTextForButtonValue = true;
            this.BtnCopyRowColumnName.Width = 50;
            // 
            // SheetName
            // 
            this.SheetName.HeaderText = "Sheet Name";
            this.SheetName.Name = "SheetName";
            this.SheetName.ReadOnly = true;
            this.SheetName.Width = 200;
            // 
            // TableName
            // 
            this.TableName.HeaderText = "Table Name";
            this.TableName.Name = "TableName";
            this.TableName.Width = 200;
            // 
            // sfdSQLFile
            // 
            this.sfdSQLFile.DefaultExt = "sql";
            this.sfdSQLFile.Filter = "SQL File|*.sql";
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleDescription = "";
            this.btnSearch.AccessibleName = "";
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(712, 29);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(288, 20);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "&Browse ...";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ofdExcel
            // 
            this.ofdExcel.Filter = "Excel|*.xls;*.xlsx";
            // 
            // btnExporter
            // 
            this.btnExporter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExporter.Location = new System.Drawing.Point(718, 486);
            this.btnExporter.Name = "btnExporter";
            this.btnExporter.Size = new System.Drawing.Size(276, 23);
            this.btnExporter.TabIndex = 21;
            this.btnExporter.Text = "&Export";
            this.btnExporter.UseVisualStyleBackColor = true;
            this.btnExporter.Click += new System.EventHandler(this.btnExporter_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(718, 515);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(276, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "&Clear";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // sfdProfile
            // 
            this.sfdProfile.DefaultExt = "layout";
            this.sfdProfile.Filter = "layout|*.layout";
            // 
            // ofdProfile
            // 
            this.ofdProfile.Filter = "layout|*.layout";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(718, 544);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(276, 23);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Clo&se";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 580);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExporter);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvTable);
            this.Controls.Add(this.gbOpcoes);
            this.Controls.Add(this.dgvColumns);
            this.Controls.Add(this.txtExcel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XLS2SQL Converter 1.2";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            this.gbOpcoes.ResumeLayout(false);
            this.gbOpcoes.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbProc.ResumeLayout(false);
            this.gbProc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtExcel;
        private System.Windows.Forms.DataGridView dgvColumns;
        private System.Windows.Forms.GroupBox gbOpcoes;
        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.SaveFileDialog sfdSQLFile;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.OpenFileDialog ofdExcel;
        private System.Windows.Forms.Button btnExporter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowColumnsName;
        private System.Windows.Forms.DataGridViewButtonColumn BtnCopyRowColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SheetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableName;
        private System.Windows.Forms.CheckBox ckbxProcTypeDelete;
        private System.Windows.Forms.CheckBox ckbxProcTypeUpdate;
        private System.Windows.Forms.CheckBox ckbxProcTypeInsert;
        private System.Windows.Forms.CheckBox ckbxProcTypeSelect;
        private System.Windows.Forms.CheckBox ckbxDropIfExists;
        private System.Windows.Forms.CheckBox ckbxCreateTable;
        private System.Windows.Forms.CheckBox cbFileSplit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbData;
        private System.Windows.Forms.CheckBox ckbxTypeSelect;
        private System.Windows.Forms.CheckBox ckbxTypeInsert;
        private System.Windows.Forms.CheckBox ckbxTypeDelete;
        private System.Windows.Forms.CheckBox ckbxTypeUpdate;
        private System.Windows.Forms.GroupBox gbProc;
        private System.Windows.Forms.RadioButton rbProc;
        private System.Windows.Forms.DataGridViewTextBoxColumn SheetColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SQLColumnName;
        private System.Windows.Forms.DataGridViewComboBoxColumn DataType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SqlColumnNull;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SqlColumnPK;
        private System.Windows.Forms.CheckBox ckbxProcTypeList;
        private System.Windows.Forms.CheckBox ckbxTypeDeleteAll;
        private System.Windows.Forms.CheckBox ckbxTypeTruncate;
        private System.Windows.Forms.CheckBox cbEmptyNULL;
        private System.Windows.Forms.Button btnUploadProfile;
        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.SaveFileDialog sfdProfile;
        private System.Windows.Forms.OpenFileDialog ofdProfile;
        private System.Windows.Forms.Button btnClose;
    }
}

