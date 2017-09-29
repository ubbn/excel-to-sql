// ubbuyan, XLS2SQL convertor, version 1.2

using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace XLS2SQL_Converter
{
    public partial class frmMain : Form
    {
        // Regular expression for date
        Regex regxDate = new Regex(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$", RegexOptions.IgnoreCase);

        private CheckBox checkBox;
        static DataSet ds = new DataSet();

        // Initialisation
        public frmMain()
        {
            InitializeComponent();

            checkBox = new CheckBox();
            checkBox.Size = new Size(15, 15);
            
            Point location = new Point(this.dgvTable.GetCellDisplayRectangle(-1, -1, false).Location.X + 10, 
                this.dgvTable.GetCellDisplayRectangle(-1, -1, false).Location.Y + 4);

            checkBox.Location = location;
            checkBox.CheckedChanged += new EventHandler(chkBx_CheckedChanged);
            
            this.dgvTable.Controls.Add(checkBox);
            checkBox.Checked = true;
        }

        #region Events

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == sfdProfile.ShowDialog())
            {
                
                StringBuilder strbTable = new StringBuilder();

                foreach (DataGridViewRow data in dgvTable.Rows)
                {
                    strbTable.Append(data.Cells[0].Value + "-*-" + data.Cells[1].Value + "-*-" + data.Cells[4].Value + ";");
                }

                StringBuilder strbColumns = new StringBuilder();

                foreach (DataGridViewRow data in dgvColumns.Rows)
                {
                    strbColumns.Append(data.Cells[1].Value + "-*-" + data.Cells[2].Value + "-*-" + data.Cells[3].Value + "-*-" +
                        data.Cells[4].Value + ";");
                }
                
                File.WriteAllText(sfdProfile.FileName, strbTable.ToString() + Environment.NewLine + strbColumns.ToString());

            }
        }

        private void btnUploadProfile_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == ofdProfile.ShowDialog())
            {
                string[] rows = File.ReadAllLines(ofdProfile.FileName);
                if (dgvTable.Rows.Count != rows[0].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Count())
                {
                    MessageBox.Show("Layout doesn't match with selected worksheet");
                    return;
                }

                string[] Table = rows[0].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < dgvTable.Rows.Count; i++)
                {
                    string[] Atribs = Table[i].Split(new string[] { "-*-" }, StringSplitOptions.RemoveEmptyEntries);
                    dgvTable.Rows[i].Cells[0].Value = Atribs[0];
                    dgvTable.Rows[i].Cells[1].Value = Atribs[1];
                    dgvTable.Rows[i].Cells[4].Value = Atribs[2];
                }

                if (dgvColumns.Rows.Count != rows[1].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Count())
                {
                    MessageBox.Show("Layout does not match the selected worksheet");
                    return;
                }

                string[] Columns = rows[1].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < dgvColumns.Rows.Count; i++)
                {
                    string[] Atribs = Columns[i].Split(new string[] { "-*-" }, StringSplitOptions.RemoveEmptyEntries);
                    dgvColumns.Rows[i].Cells[1].Value = Atribs[0];
                    dgvColumns.Rows[i].Cells[2].Value = Atribs[1];
                    dgvColumns.Rows[i].Cells[3].Value = Atribs[2];
                    dgvColumns.Rows[i].Cells[4].Value = Atribs[3];
                }
            }
        }

        private void dgvColumns_EditingControls(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewComboBoxColumn cmbBoxColumn = (DataGridViewComboBoxColumn)dgvColumns.Columns[2];
            if (dgvColumns.CurrentCellAddress.X == cmbBoxColumn.DisplayIndex)
            {
                ComboBox combox = (ComboBox)e.Control;

                if (combox != null)
                {
                    combox.DropDownStyle = ComboBoxStyle.DropDown;
                }
            }
        }

        private void dgvColumns_CellValidate(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewComboBoxColumn comboBoxColumn = (DataGridViewComboBoxColumn)dgvColumns.Columns[2];
            if (e.ColumnIndex == comboBoxColumn.DisplayIndex)
            {
                if (!comboBoxColumn.Items.Contains(e.FormattedValue))
                {
                    comboBoxColumn.Items.Add(e.FormattedValue);
                    dgvColumns.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = e.FormattedValue;
                }
            }
        }

        private void chkBx_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < this.dgvTable.RowCount; j++)
            {
                this.dgvTable[0, j].Value = this.checkBox.Checked;
                this.dgvTable.Rows[j].Selected = false;
            }
            this.dgvTable.RefreshEdit();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ds.Clear();
            ds.Reset();
            OpenFileDialog ofdNew = ofdExcel;

            if (DialogResult.OK == ofdNew.ShowDialog())
            {
                ProgessProcessing progproc = new ProgessProcessing();
                Thread threadproc = new Thread(new ThreadStart(progproc.progresso));
                threadproc.IsBackground = true;
                progproc.Value = true;
                threadproc.Start();

                Clear();
                txtExcel.Text = ofdNew.FileName;
                txtExcel.Enabled = false;
                btnSearch.Enabled = false;
                ProcessingArchive(txtExcel.Text);

                progproc.Value = false;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void dgvTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            UploadColumns();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();

            btnSearch.Enabled = true;
            txtExcel.Enabled = true;
        }

        private void dgvTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    string RowNumber = dgvTable.Rows[e.RowIndex].Cells[1].Value.ToString();

                    for (int i = 0; i < dgvTable.Rows.Count; i++)
                    {
                        dgvTable.Rows[i].Cells[1].Value = RowNumber;
                    }
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        private void btnExporter_Click(object sender, EventArgs e)
        {
            ProgessProcessing progproc = new ProgessProcessing();
            Thread threadproc = new Thread(new ThreadStart(progproc.progresso));
            threadproc.IsBackground = true;
            progproc.Value = true;
            threadproc.Start();

            int totalCount = 0;
            StringBuilder strb = new StringBuilder();

            foreach (DataGridViewRow dgvr in dgvTable.Rows)
            {

                if (!Convert.ToBoolean(dgvr.Cells[0].Value))
                    continue;

                HeadingManip(ref strb, dgvr.Cells[4].Value.ToString());

                if (ckbxDropIfExists.Checked)
                {
                    DropIfExist(ref strb, dgvr.Cells[4].Value.ToString());
                }

                if (ckbxCreateTable.Checked)
                {
                    CreateTable(ref strb, dgvr.Cells[3].Value.ToString(), dgvr.Cells[4].Value.ToString(), 
                        Convert.ToInt32(dgvr.Cells[1].Value.ToString()), dgvColumns);
                }

                if (rbProc.Checked)
                {
                    if (ckbxProcTypeSelect.Checked)
                    {
                        SelectProc(ref strb, dgvr.Cells[3].Value.ToString(), dgvr.Cells[4].Value.ToString(), 
                            Convert.ToInt32(dgvr.Cells[1].Value.ToString()), dgvColumns);
                    }

                    if (ckbxProcTypeInsert.Checked)
                    {
                        InsProc(ref strb, dgvr.Cells[3].Value.ToString(), dgvr.Cells[4].Value.ToString(), 
                            Convert.ToInt32(dgvr.Cells[1].Value.ToString()), dgvColumns);
                    }

                    if (ckbxProcTypeUpdate.Checked)
                    {
                        if (totalCount == 0 && !verifyPKey())
                        {
                            if (DialogResult.OK == MessageBox.Show("Generating UPDATE without Primary key may cause problem, " + 
                                " continue anyway?", "Okay", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                            {
                                totalCount++;
                                UpdProc(ref strb, dgvr.Cells[3].Value.ToString(), dgvr.Cells[4].Value.ToString(), 
                                    Convert.ToInt32(dgvr.Cells[1].Value.ToString()), dgvColumns);
                            }
                            else
                                return;
                        }
                        else
                        {
                            UpdProc(ref strb, dgvr.Cells[3].Value.ToString(), dgvr.Cells[4].Value.ToString(), 
                                Convert.ToInt32(dgvr.Cells[1].Value.ToString()), dgvColumns);
                        }
                    }

                    if (ckbxProcTypeDelete.Checked)
                    {
                        DeleteProc(ref strb, dgvr.Cells[3].Value.ToString(), dgvr.Cells[4].Value.ToString(), 
                            Convert.ToInt32(dgvr.Cells[1].Value.ToString()), dgvColumns);
                    }

                    if (ckbxProcTypeList.Checked)
                    {
                        ListProc(ref strb, dgvr.Cells[3].Value.ToString(), dgvr.Cells[4].Value.ToString(), 
                            Convert.ToInt32(dgvr.Cells[1].Value.ToString()), dgvColumns);
                    }
                }
                else if (rbData.Checked)
                {
                    if (ckbxTypeSelect.Checked)
                    {

                    }

                    if (ckbxTypeDeleteAll.Checked)
                    {
                        DeleteAllDataMan(ref strb, dgvr.Cells[4].Value.ToString());
                    }

                    if (ckbxTypeTruncate.Checked)
                    {
                        TruncateDataMan(ref strb, dgvr.Cells[4].Value.ToString());
                    }

                    if (ckbxTypeInsert.Checked)
                    {
                        InsertDataMan(ref strb, dgvr.Cells[3].Value.ToString(), dgvr.Cells[4].Value.ToString(), 
                            Convert.ToInt32(dgvr.Cells[1].Value.ToString()), dgvColumns);
                    }

                    if (ckbxTypeUpdate.Checked)
                    {
                        if (totalCount == 0 && !verifyPKey())
                        {
                            if (DialogResult.OK == MessageBox.Show("Generating UPDATE without Primary key may cause problem, " + 
                                "continue anyway?", "Okay", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                            {
                                totalCount++;
                                UpdateDataMan(ref strb, dgvr.Cells[3].Value.ToString(), dgvr.Cells[4].Value.ToString(), 
                                    Convert.ToInt32(dgvr.Cells[1].Value.ToString()), dgvColumns);
                            }
                            else
                                return;
                        }
                        else
                        {
                            UpdateDataMan(ref strb, dgvr.Cells[3].Value.ToString(), dgvr.Cells[4].Value.ToString(), 
                                Convert.ToInt32(dgvr.Cells[1].Value.ToString()), dgvColumns);
                        }
                    }
                    if (ckbxTypeDelete.Checked)
                    {
                        DeleteDataMan(ref strb, dgvr.Cells[3].Value.ToString(), dgvr.Cells[4].Value.ToString(), 
                            Convert.ToInt32(dgvr.Cells[1].Value.ToString()), dgvColumns);
                    }
                }

                strb.AppendLine("GO");

                if (cbFileSplit.Checked)
                {
                    if (sfdSQLFile.FileName == "" && DialogResult.Cancel == sfdSQLFile.ShowDialog())
                    {
                        sfdSQLFile.FileName = "";
                        return;
                    }
                    else
                    {
                        String diretorio = Path.GetDirectoryName(sfdSQLFile.FileName);
                        File.WriteAllText(diretorio + "\\" + dgvr.Cells[4].Value.ToString() + ".sql", strb.ToString());
                        strb.Clear();
                    }

                }
            }
            if (!cbFileSplit.Checked)
            {
                if (DialogResult.OK == sfdSQLFile.ShowDialog())
                {
                    File.WriteAllText(@sfdSQLFile.FileName, strb.ToString());
                }
            }

            sfdSQLFile.FileName = "";

            progproc.Value = false;
        }

        private void dgvColumns_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (Convert.ToBoolean(dgvColumns.Rows[e.RowIndex].Cells[4].Value))
                dgvColumns.Rows[e.RowIndex].Cells[3].Value = false;
        }

        private void dgvColumns_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvColumns.IsCurrentCellDirty)
            {
                dgvColumns.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvTable_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvTable.IsCurrentCellDirty)
            {
                dgvTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Select Procedure building up 
        /// </summary>
        private void SelectProc(ref StringBuilder strb, string nonePrimary, string nameCurrent, Int32 columnNamesRow, DataGridView dgvColumns)
        {
            DataTable dtable = new DataTable();
            dtable = ds.Tables[nonePrimary + "$"];

            string[] PrimitiveColumns = new string[dtable.Columns.Count];

            for (int i = 0; i < dtable.Columns.Count; i++)
            {
                PrimitiveColumns[i] = dtable.Rows[columnNamesRow][i].ToString();
                dtable.Columns[i].ColumnName = PrimitiveColumns[i];
            }

            List<string> lstNewColumns = new List<string>();
            List<string> lstNewColumnsName = new List<string>();
            List<string> lstPKNew = new List<string>();
            List<string> lstPKPrimary = new List<string>();
            List<string> lstPKType = new List<string>();

            foreach (string primitiveColumn in PrimitiveColumns)
            {
                lstNewColumns.Add("[" +
                                dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                                row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() +
                                "]");
                lstNewColumnsName.Add("[" + primitiveColumn + "]");

                if ((Convert.ToBoolean(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[4].Value).First().ToString())))
                {
                    lstPKNew.Add("[" + dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() + "]");

                    lstPKType.Add(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[2].Value).First().ToString());

                    lstPKPrimary.Add("[" + primitiveColumn + "]");
                }
            }

            string selectClause = string.Empty;
            string whereClause = string.Empty;
            string strParams = string.Empty;

            for (int l = 0; l < lstNewColumns.Count; l++)
            {
                selectClause += (l == 0 ? "\t\t" : Environment.NewLine + "\t\t,") + lstNewColumns[l];
            }

            for (int l = 0; l < lstPKNew.Count; l++)
            {
                whereClause += (l == 0 ? "\t\t" : Environment.NewLine + "\t\tAND ") + lstPKNew[l] + 
                    " = @" + RemoveAccents(lstPKNew[l].Replace(" ", "").Replace("[", "").Replace("]", ""));
            }

            for (int l = 0; l < lstPKNew.Count; l++)
            {
                strParams += (l == 0 ? "\t" : Environment.NewLine + "\t,") + "@" + 
                    RemoveAccents(lstPKNew[l].Replace(" ", "").Replace("[", "").Replace("]", "")) + " " + lstPKType[l];
            }

            // Appending 

            strb.AppendLine("CREATE PROCEDURE [dbo].[SPSel_" + nameCurrent + "]");
            strb.AppendLine(strParams);
            strb.AppendLine("AS");
            strb.AppendLine("BEGIN");
            strb.AppendLine("\tSELECT");
            strb.AppendLine(selectClause);
            strb.AppendLine("\tFROM");
            strb.AppendLine("\t\t[dbo].[" + nameCurrent + "]");

            if (whereClause.Trim() != "")
            {
                strb.AppendLine("\tWHERE");
                strb.AppendLine(whereClause);
            }

            // Close sql
            strb.AppendLine("END");
            strb.AppendLine("GO");
            strb.AppendLine(Environment.NewLine);
        }

        /// <summary>
        /// List Procedure building up
        /// </summary>
        private void ListProc(ref StringBuilder strb, string nonePrimary, string nameCurrent, Int32 columnNamesRow, DataGridView dgvColumns)
        {
            DataTable dtable = new DataTable();
            dtable = ds.Tables[nonePrimary + "$"];

            string[] PrimitiveColumns = new string[dtable.Columns.Count];
            for (int i = 0; i < dtable.Columns.Count; i++)
            {
                PrimitiveColumns[i] = dtable.Rows[columnNamesRow][i].ToString();
                dtable.Columns[i].ColumnName = PrimitiveColumns[i];
            }

            List<string> lstNewColumns = new List<string>();
            List<string> lstNewColumnsName = new List<string>();

            foreach (string primitiveColumn in PrimitiveColumns)
            {
                lstNewColumns.Add("[" +
                                dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                                row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() +
                                "]");
                lstNewColumnsName.Add("[" + primitiveColumn + "]");
            }

            string selectClause = string.Empty;
            string whereClause = string.Empty;
            string strParams = string.Empty;

            for (int l = 0; l < lstNewColumns.Count; l++)
            {
                selectClause += (l == 0 ? "\t\t" : Environment.NewLine + "\t\t,") + lstNewColumns[l];
            }

            strb.AppendLine("CREATE PROCEDURE [dbo].[SPSel_" + nameCurrent + "]");
            strb.AppendLine("AS");
            strb.AppendLine("BEGIN");
            strb.AppendLine("\tSELECT");
            strb.AppendLine(selectClause);
            strb.AppendLine("\tFROM");
            strb.AppendLine("\t\t[dbo].[" + nameCurrent + "]");
            strb.AppendLine("END");
            strb.AppendLine("GO");
            strb.AppendLine(Environment.NewLine);
        }

        /// <summary>
        /// Update Procedure building up
        /// </summary>
        private void UpdProc(ref StringBuilder strb, string nonePrimary, string nameCurrent, Int32 columnNamesRow, DataGridView dgvColumns)
        {
            DataTable dtable = new DataTable();
            dtable = ds.Tables[nonePrimary + "$"];

            string[] PrimitiveColumns = new string[dtable.Columns.Count];

            for (int i = 0; i < dtable.Columns.Count; i++)
            {
                PrimitiveColumns[i] = dtable.Rows[columnNamesRow][i].ToString();
                dtable.Columns[i].ColumnName = PrimitiveColumns[i];
            }

            List<string> lstNewColumns = new List<string>();
            List<string> lstNewColumnsName = new List<string>();
            List<string> lstNewColumnsType = new List<string>();
            List<string> lstPKNew = new List<string>();
            List<string> lstPKPrimary = new List<string>();
            List<string> lstPKType = new List<string>();

            foreach (string primitiveColumn in PrimitiveColumns)
            {
                if (!(Convert.ToBoolean(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[4].Value).First().ToString())))
                {
                    lstNewColumns.Add("[" +
                                    dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() +
                                    "]");
                    lstNewColumnsName.Add("[" + primitiveColumn + "]");

                    lstNewColumnsType.Add(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[2].Value).First().ToString());
                }

                if ((Convert.ToBoolean(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[4].Value).First().ToString())))
                {
                    lstPKNew.Add("[" + dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() + "]");

                    lstPKType.Add(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[2].Value).First().ToString());

                    lstPKPrimary.Add("[" + primitiveColumn + "]");
                }
            }

            string updateClause = string.Empty;
            string whereClause = string.Empty;
            string strParams = string.Empty;

            for (int l = 0; l < lstNewColumnsName.Count; l++)
            {
                updateClause += (l == 0 ? "\t\t" : Environment.NewLine + "\t\t,") + lstNewColumnsName[l] + " = @" + 
                    RemoveAccents(lstNewColumnsName[l].Replace(" ", "").Replace("[", "").Replace("]", ""));
            }

            for (int l = 0; l < lstPKNew.Count; l++)
            {
                whereClause += (l == 0 ? "\t\t" : Environment.NewLine + "\t\tAND ") + lstPKNew[l] + " = @" + 
                    RemoveAccents(lstPKNew[l].Replace(" ", "").Replace("[", "").Replace("]", ""));
            }

            for (int l = 0; l < lstPKNew.Count; l++)
            {
                strParams += (l == 0 ? "\t" : Environment.NewLine + "\t,") + "@" + 
                    RemoveAccents(lstPKNew[l].Replace(" ", "").Replace("[", "").Replace("]", "")) + " " + lstPKType[l];
            }

            for (int l = 0; l < lstNewColumnsType.Count; l++)
            {
                strParams += (strParams.Trim() == "" ? "\t" : Environment.NewLine + "\t,") + "@" + 
                    RemoveAccents(lstNewColumnsName[l].Replace(" ", "").Replace("[", "").Replace("]", "")) + " " + lstNewColumnsType[l];
            }

            strb.AppendLine("CREATE PROCEDURE [dbo].[SPUpd_" + nameCurrent + "]");
            strb.AppendLine(strParams);
            strb.AppendLine("AS");
            strb.AppendLine("BEGIN");
            strb.AppendLine("\tUPDATE");
            strb.AppendLine("\t\t[dbo].[" + nameCurrent + "]");
            strb.AppendLine("\tSET");
            strb.AppendLine(updateClause);

            if (whereClause.Trim() != "")
            {
                strb.AppendLine("\tWHERE");
                strb.AppendLine(whereClause);
            }

            strb.AppendLine("END");
            strb.AppendLine("GO");
            strb.AppendLine(Environment.NewLine);
        }

        /// <summary>
        /// Insert Procedure building up
        /// </summary>
        private void InsProc(ref StringBuilder strb, string nonePrimary, string nameCurrent, Int32 columnNamesRow, DataGridView dgvColumns)
        {
            DataTable dtable = new DataTable();
            dtable = ds.Tables[nonePrimary + "$"];

            string[] PrimitiveColumns = new string[dtable.Columns.Count];
            for (int i = 0; i < dtable.Columns.Count; i++)
            {
                PrimitiveColumns[i] = dtable.Rows[columnNamesRow][i].ToString();
                dtable.Columns[i].ColumnName = PrimitiveColumns[i];
            }

            List<string> lstNewColumns = new List<string>();
            List<string> lstNewColumnsName = new List<string>();
            List<string> lstNewColumnsType = new List<string>();

            foreach (string primitiveColumn in PrimitiveColumns)
            {
                lstNewColumns.Add("[" +
                                dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                                row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() +
                                "]");
                lstNewColumnsName.Add("[" + primitiveColumn + "]");

                lstNewColumnsType.Add(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[2].Value).First().ToString());
            }

            string values = string.Empty;
            string columnsName = string.Empty;
            string strParams = string.Empty;

            foreach (string Column in lstNewColumnsName)
            {
                columnsName += Column + ",";
            }
            columnsName = columnsName.Substring(0, (columnsName.Length - 1));

            for (int l = 0; l < lstNewColumnsName.Count; l++)
            {
                values += (l == 0 ? "" : ",") + "@" + RemoveAccents(lstNewColumnsName[l].Replace(" ", "").Replace("[", "").Replace("]", ""));
            }

            for (int l = 0; l < lstNewColumnsType.Count; l++)
            {
                strParams += (strParams.Trim() == "" ? "\t" : Environment.NewLine + "\t,") + "@" + 
                    RemoveAccents(lstNewColumnsName[l].Replace(" ", "").Replace("[", "").Replace("]", "")) + " " + lstNewColumnsType[l];
            }

            strb.AppendLine("CREATE PROCEDURE [dbo].[SPIns_" + nameCurrent + "]");
            strb.AppendLine(strParams);
            strb.AppendLine("AS");
            strb.AppendLine("BEGIN");
            strb.AppendLine("\tINSERT INTO");
            strb.Append("\t\t[dbo].[" + nameCurrent + "]");
            strb.AppendLine("\t(" + columnsName + ")");
            strb.AppendLine("\tVALUES");
            strb.AppendLine("\t\t(" + values + ")");
            strb.AppendLine("END");
            strb.AppendLine("GO");
            strb.AppendLine(Environment.NewLine);
        }

        /// <summary>
        /// Delete Procedure building up
        /// </summary>
        private void DeleteProc(ref StringBuilder strb, string nonePrimary, string nameCurrent, Int32 columnNamesRow, DataGridView dgvColumns)
        {
            DataTable dtable = new DataTable();
            dtable = ds.Tables[nonePrimary + "$"];

            string[] PrimitiveColumns = new string[dtable.Columns.Count];
            for (int i = 0; i < dtable.Columns.Count; i++)
            {
                PrimitiveColumns[i] = dtable.Rows[columnNamesRow][i].ToString();
                dtable.Columns[i].ColumnName = PrimitiveColumns[i];
            }

            List<string> lstPKNew = new List<string>();
            List<string> lstPKPrimary = new List<string>();
            List<string> lstPKType = new List<string>();

            foreach (string primitiveColumn in PrimitiveColumns)
            {
                if ((Convert.ToBoolean(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[4].Value).First().ToString())))
                {
                    lstPKNew.Add("[" + dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() + "]");

                    lstPKType.Add(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[2].Value).First().ToString());

                    lstPKPrimary.Add("[" + primitiveColumn + "]");
                }
            }

            string strParams = string.Empty;
            string whereClause = string.Empty;

            for (int l = 0; l < lstPKNew.Count; l++)
            {
                strParams += (strParams.Trim() == "" ? "\t" : Environment.NewLine + "\t,") + "@" + 
                    RemoveAccents(lstPKNew[l].Replace(" ", "").Replace("[", "").Replace("]", "")) + " " + lstPKType[l];
            }

            for (int l = 0; l < lstPKNew.Count; l++)
            {
                whereClause += (whereClause.Trim() == "" ? "\t\t" : Environment.NewLine + "\t\t,") + 
                    lstPKNew[l] + " = " + "@" + RemoveAccents(lstPKNew[l].Replace(" ", "").Replace("[", "").Replace("]", ""));
            }

            strb.AppendLine("CREATE PROCEDURE [dbo].[SPDel_" + nameCurrent + "]");
            strb.AppendLine(strParams);
            strb.AppendLine("AS");
            strb.AppendLine("BEGIN");
            strb.AppendLine("\tDELETE FROM");
            strb.AppendLine("\t\t[dbo].[" + nameCurrent + "]");

            if (whereClause.Trim() != "")
            {
                strb.AppendLine("\tWHERE");
                strb.AppendLine(whereClause);
            }

            strb.AppendLine("END");
            strb.AppendLine("GO");
            strb.AppendLine(Environment.NewLine);
        }

        private static string RemoveAccents(string str)
        {
            StringBuilder strbReturn = new StringBuilder();
            var arrayText = str.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    strbReturn.Append(letter);
            }
            return strbReturn.ToString();
        }

        /// <summary>
        /// Verify primary keys
        /// </summary>
        private bool verifyPKey()
        {
            foreach (DataGridViewRow dr in dgvColumns.Rows)
            {
                if (Convert.ToBoolean(dr.Cells[4].Value.ToString()))
                {
                    return true;
                }
            }
            return false;
        }


        private void ProcessingArchive(string directory)
        {
            try
            {
                OleDbConnection xlsCon = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + 
                    directory + ";Extended Properties=\"Excel 12.0 Xml;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text;IMEX=1\"");
                xlsCon.Open();

                // Get a list of sheets 
                DataTable dtSheets = new DataTable();
                dtSheets = xlsCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
                List<string> ListaSheets = new List<string>();

                foreach (DataRow dr in dtSheets.Rows)
                {
                    ListaSheets.Add(dr["TABLE_NAME"].ToString().Split(new string[] { "$" }, StringSplitOptions.None)[0].Replace("'", ""));
                }
                
                ListaSheets = ListaSheets.Distinct().ToList();

                foreach (string SheetName in ListaSheets)
                {
                    dgvTable.Rows.Add(true, "1", "", SheetName, SheetName);
                }

                xlsCon.Close();

                UploadDataSet();

                UploadColumns();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// To clear all components
        /// </summary>
        /// <param name="specific">
        /// Table - datagidvTable
        /// Columns - datagidvColumns
        /// </param>
        private void Clear(string specific = "")
        {
            switch (specific)
            {
                case "Table":
                    dgvTable.Rows.Clear();
                    break;
                case "Columns":
                    dgvColumns.Rows.Clear();
                    break;
                case "":
                    dgvTable.Rows.Clear();
                    dgvColumns.Rows.Clear();
                    txtExcel.Text = "Choose a excel file";
                    break;
            }

        }

        /// <summary>
        // Upload data grid
        /// </summary>
        private void UploadDataSet()
        {
            foreach (DataGridViewRow dgvr in dgvTable.Rows)
            {
                if (!Convert.ToBoolean(dgvr.Cells[0].Value))
                    continue;

                DataTable dtable= new DataTable();

                // connection string
                OleDbConnection xlsCon = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + 
                    txtExcel.Text + ";Extended Properties=\"Excel 12.0 Xml;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text;IMEX=1\"");
                
                xlsCon.Open();

                // retrieval
                OleDbDataAdapter xlsDa = new OleDbDataAdapter("SELECT * FROM [" + dgvr.Cells[3].Value.ToString() + "$]", xlsCon);
                xlsDa.Fill(dtable);

                dtable.TableName = dgvr.Cells[3].Value.ToString() + "$";

                ds.Tables.Add(dtable);

                xlsCon.Close();
            }
        }

        private void UploadColumns()
        {
            try
            {
                Clear("Columns");
                List<string> Columns = new List<string>();

                foreach (DataGridViewRow dgvr in dgvTable.Rows)
                {
                    if (!Convert.ToBoolean(dgvr.Cells[0].Value))
                        continue;

                    DataTable dtable = new DataTable();

                    dtable = ds.Tables[dgvr.Cells[3].Value.ToString() + "$"];

                    if (dtable != null)
                    {
                        for (int j = 0; j < dtable.Columns.Count; j++)
                        {
                            try
                            {
                                Columns.Add(dtable.Rows[Convert.ToInt32(dgvr.Cells[1].Value)][j].ToString());
                            }
                            catch (IndexOutOfRangeException)
                            {
                                continue;
                            }
                        }
                    }
                }

                Columns = Columns.Distinct().ToList();
                foreach (string column in Columns)
                {
                    dgvColumns.Rows.Add(column, column, "[VARCHAR](1024)", true, false);
                }

            }
            catch
            {
                throw new Exception();
            }
        }

        private void HeadingManip(ref StringBuilder strb, string nameCurrent)
        {
            int quantity = 0;
            quantity = ((50 - nameCurrent.Length) / 2);

            strb.AppendLine(String.Empty.PadRight(50, '-'));
            strb.Append(String.Empty.PadRight(quantity, '-'));
            strb.Append(nameCurrent);

            strb.AppendLine(String.Empty.PadRight(quantity, '-'));
            strb.AppendLine(String.Empty.PadRight(50, '-'));
        }

        /// <summary>
        /// Table creation
        /// </summary>
        private void CreateTable(ref StringBuilder strb, string nonePrimary, string nameCurrent, Int32 columnNamesRow, DataGridView dgvColumns)
        {
            DataTable dtable= new DataTable();
            dtable= ds.Tables[nonePrimary + "$"];

            string[] PrimitiveColumns = new string[dtable.Columns.Count];

            for (int i = 0; i < dtable.Columns.Count; i++)
            {
                PrimitiveColumns[i] = dtable.Rows[columnNamesRow][i].ToString();
            }

            string lstNewColumns = string.Empty;
            string PKeys = string.Empty;

            foreach (string primitiveColumn in PrimitiveColumns)
            {
                lstNewColumns += "\t\t[" +
                    dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() +
                    "] " +
                    dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[2].Value).First().ToString() +
                    " " +
                    (Convert.ToBoolean(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[3].Value).First().ToString()) ? "NULL" : "NOT NULL") +
                    "," + Environment.NewLine;

                if ((Convert.ToBoolean(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[4].Value).First().ToString())))
                {
                    PKeys += "[" + dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() + "],";
                }
            }

            lstNewColumns = lstNewColumns.Substring(0, lstNewColumns.Length - 3);

            if (PKeys.Trim() != string.Empty)
                PKeys = PKeys.Substring(0, PKeys.Length - 1);

            strb.AppendLine("IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[" + nameCurrent + "]')");
            strb.AppendLine("AND OBJECTPROPERTY(id, N'IsUserTable') = 1)");
            strb.AppendLine("BEGIN");
            strb.AppendLine("\tCREATE TABLE [dbo].[" + nameCurrent + "]");
            strb.AppendLine("\t(");
            strb.AppendLine(lstNewColumns);

            if (PKeys.Trim() != string.Empty)
                strb.AppendLine("\tCONSTRAINT [PK_" + nameCurrent + "] PRIMARY KEY (" + PKeys + ")");
            
            strb.AppendLine("\t)");
            strb.AppendLine("END");
            strb.AppendLine("GO");
            strb.AppendLine(Environment.NewLine);

        }

        /// <summary>
        /// If exists, drop it
        /// </summary>
        private void DropIfExist(ref StringBuilder strb, string nameCurrent)
        {
            strb.AppendLine("IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[" + nameCurrent + "]')");
            strb.AppendLine("AND OBJECTPROPERTY(id, N'IsUserTable') = 1)");
            strb.AppendLine("DROP TABLE [dbo].[" + nameCurrent + "];");
            strb.AppendLine("GO");
            strb.AppendLine(Environment.NewLine);
        }

        /// <summary>
        /// Data Manipulation: Insert
        /// </summary>
        private void InsertDataMan(ref StringBuilder strb, string nonePrimary, string nameCurrent, Int32 columnNamesRow, DataGridView dgvColumns)
        {
            DataTable dtable = new DataTable();
            dtable = ds.Tables[nonePrimary + "$"];

            string[] PrimitiveColumns = new string[dtable.Columns.Count];

            for (int i = 0; i < dtable.Columns.Count; i++)
            {
                PrimitiveColumns[i] = dtable.Rows[columnNamesRow][i].ToString();
            }

            string lstNewColumns = string.Empty;

            foreach (string primitiveColumn in PrimitiveColumns)
            {
                lstNewColumns += "[" +
                    dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() +
                    "],";
            }
            lstNewColumns = lstNewColumns.Substring(0, lstNewColumns.Length - 1);

            for (int index = (columnNamesRow + 1); index < dtable.Rows.Count; index++)
            {
                string values = string.Empty;
                for (int j = 0; j < dtable.Columns.Count; j++)
                {
                    if (regxDate.IsMatch(dtable.Rows[index][j].ToString()))
                        values += "'" + Convert.ToDateTime(dtable.Rows[index][j]).ToString("MM/dd/yyyy") + "'" + (j == (dtable.Columns.Count - 1) ? "" : ",");
                    else
                        values += "'" + dtable.Rows[index][j].ToString().Replace("'", "''") + "'" + (j == (dtable.Columns.Count - 1) ? "" : ",");

                    if (cbEmptyNULL.Checked)
                        values = values.Replace("''", "NULL");
                }

                if (values.Replace("'", "").Replace(",", "").Replace("NULL", "").Trim() == "")
                    continue;

                strb.Append("INSERT INTO [dbo].[" + nameCurrent + "]");
                strb.Append("(");
                strb.Append(lstNewColumns);
                strb.AppendLine(")");
                strb.AppendLine("VALUES (" + values + ");");

                if (index % 1000 == 0)
                    strb.AppendLine("GO");
                
                strb.AppendLine(Environment.NewLine);
            }
        }

        /// <summary>
        /// Data Manipulation: Delete
        /// </summary>
        private void DeleteDataMan(ref StringBuilder strb, string nonePrimary, string nameCurrent, Int32 columnNamesRow, DataGridView dgvColumns)
        {
            DataTable dtable = new DataTable();
            dtable = ds.Tables[nonePrimary + "$"];

            string[] PrimitiveColumns = new string[dtable.Columns.Count];

            for (int i = 0; i < dtable.Columns.Count; i++)
            {
                PrimitiveColumns[i] = dtable.Rows[columnNamesRow][i].ToString();
                dtable.Columns[i].ColumnName = PrimitiveColumns[i];
            }

            List<string> lstNewColumns = new List<string>();
            List<string> lstPKNew = new List<string>();
            List<string> lstPKPrimary = new List<string>();

            foreach (string primitiveColumn in PrimitiveColumns)
            {
                lstNewColumns.Add("[" +
                                dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                                row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() +
                                "]");
                if ((Convert.ToBoolean(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[4].Value).First().ToString())))
                {
                    lstPKNew.Add("[" + dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() + "]");

                    lstPKPrimary.Add("[" + primitiveColumn + "]");
                }
            }

            for (int k = (columnNamesRow + 1); k < dtable.Rows.Count; k++)
            {
                string whereClause = string.Empty;
                //if (lstPKNew.Count == 1)
                //{
                //    if (dtable.Rows[k][lstPKPrimary[0].Replace("[", "").Replace("]", "")].ToString().Trim() == String.Empty)
                //        continue;

                //    whereClause += lstPKNew[0] + " = '" + dtable.Rows[k][lstPKPrimary[0].Replace("[","").Replace("]","")] + "'";
                //}
                //else
                if (lstPKNew.Count == 0)
                {
                    string verify = string.Empty;
                    for (int l = 0; l < lstNewColumns.Count; l++)
                    {
                        if (regxDate.IsMatch(dtable.Rows[k][l].ToString()))
                            whereClause += (l == 0 ? "" : " AND ") + lstNewColumns[l] + " = '" + 
                                Convert.ToDateTime(dtable.Rows[k][l]).ToString("MM/dd/yyyy") + "'";
                        else
                            whereClause += (l == 0 ? "" : " AND ") + lstNewColumns[l] + " = '" + 
                                dtable.Rows[k][l].ToString().Replace("'", "''") + "'";
                        verify += dtable.Rows[k][l];
                    }
                    if (verify.Trim() == "")
                        continue;
                }
                else if (lstPKNew.Count > 0)
                {
                    string verify = string.Empty;

                    for (int l = 0; l < lstPKNew.Count; l++)
                    {
                        if (regxDate.IsMatch(dtable.Rows[k][lstPKPrimary[l].Replace("[", "").Replace("]", "")].ToString()))
                            whereClause += (l == 0 ? "" : " AND ") + lstPKNew[l] + " = '" + Convert.ToDateTime(dtable.Rows[k][lstPKPrimary[l].Replace("[", "").Replace("]", "")]).ToString("MM/dd/yyyy") + "'";
                        else
                            whereClause += (l == 0 ? "" : " AND ") + lstPKNew[l] + " = '" + dtable.Rows[k][lstPKPrimary[l].Replace("[", "").Replace("]", "")].ToString().Replace("'", "''") + "'";
                        verify += dtable.Rows[k][lstPKPrimary[l].Replace("[", "").Replace("]", "")];
                    }

                    if (verify.Trim() == "")
                        continue;
                }

                strb.AppendLine("DELETE FROM [dbo].[" + nameCurrent + "]");
                strb.AppendLine("WHERE " + whereClause + ";");

                if (k % 1000 == 0)
                    strb.AppendLine("GO");
                strb.AppendLine(Environment.NewLine);

            }
        }

        /// <summary>
        /// Data Manipulation: Delete All
        /// </summary>
        private void DeleteAllDataMan(ref StringBuilder strb, string nameCurrent)
        {
            strb.AppendLine("DELETE FROM [dbo].[" + nameCurrent + "]" + ";");
            //strb.AppendLine("GO");
            strb.AppendLine(Environment.NewLine);
        }

        /// <summary>
        /// Data Manipulation: Truncate
        /// </summary>
        private void TruncateDataMan(ref StringBuilder strb, string nameCurrent)
        {
            strb.AppendLine("TRUNCATE TABLE [dbo].[" + nameCurrent + "]" + ";");
            //strb.AppendLine("GO");
            strb.AppendLine(Environment.NewLine);
        }

        /// <summary>
        /// Data Manipulation: Update
        /// </summary>
        private void UpdateDataMan(ref StringBuilder strb, string nonePrimary, string nameCurrent, Int32 columnNamesRow, DataGridView dgvColumns)
        {
            DataTable dtable = new DataTable();
            dtable = ds.Tables[nonePrimary + "$"];

            string[] PrimitiveColumns = new string[dtable.Columns.Count];

            for (int i = 0; i < dtable.Columns.Count; i++)
            {
                PrimitiveColumns[i] = dtable.Rows[columnNamesRow][i].ToString();
                dtable.Columns[i].ColumnName = PrimitiveColumns[i];
            }

            List<string> lstNewColumns = new List<string>();
            List<string> lstNewColumnsName = new List<string>();
            List<string> lstPKNew = new List<string>();
            List<string> lstPKPrimary = new List<string>();

            foreach (string primitiveColumn in PrimitiveColumns)
            {
                if (!(Convert.ToBoolean(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[4].Value).First().ToString())))
                {
                    lstNewColumns.Add("[" +
                                    dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() +
                                    "]");
                    lstNewColumnsName.Add("[" + primitiveColumn + "]");
                }

                if ((Convert.ToBoolean(dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[4].Value).First().ToString())))
                {
                    lstPKNew.Add("[" + dgvColumns.Rows.Cast<DataGridViewRow>().Where(
                    row => (row.Cells[0].Value.ToString() == primitiveColumn)).Select(row => row.Cells[1].Value).First().ToString() + "]");

                    lstPKPrimary.Add("[" + primitiveColumn + "]");
                }
            }

            for (int k = (columnNamesRow + 1); k < dtable.Rows.Count; k++)
            {
                string updateClause = string.Empty;
                string whereClause = string.Empty;
                string verify = string.Empty;

                for (int l = 0; l < lstNewColumns.Count; l++)
                {
                    updateClause += (l == 0 ? "" : ", ") + lstNewColumns[l] + " = '" + 
                        dtable.Rows[k][lstNewColumnsName[l].Replace("[", "").Replace("]", "")].ToString().Replace("'", "''") + "'";

                    if (cbEmptyNULL.Checked)
                        updateClause = updateClause.Replace("''", "NULL");

                    verify += dtable.Rows[k][l];
                }

                //if (verify.Trim() == "")
                //continue;
                if (verify.Replace("'", "").Replace(",", "").Replace("NULL", "").Trim() == "")
                    continue;

                for (int l = 0; l < lstPKNew.Count; l++)
                {
                    whereClause += (l == 0 ? "" : " AND ") + lstPKNew[l] + " = '" + 
                        dtable.Rows[k][lstPKPrimary[l].Replace("[", "").Replace("]", "")].ToString().Replace("'", "''") + "'";
                }

                strb.AppendLine("UPDATE [dbo].[" + nameCurrent + "]");
                strb.AppendLine("SET " + updateClause);
                strb.AppendLine("WHERE " + whereClause + ";");

                if (k % 1000 == 0)
                    strb.AppendLine("GO");
                
                strb.AppendLine(Environment.NewLine);
            }
        }

        #endregion

    }
}