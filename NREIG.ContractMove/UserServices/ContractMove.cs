using System.Data;
using System.Text;
using Newtonsoft.Json;
using OfficeOpenXml;
using BL = NREIG.ContractMove.BusinessLayer;

namespace NREIG.ContractMove
{
    public partial class ContractMove : Form
    {
        private readonly List<Servers> servers = new();
        private readonly string[] ExpectedColumns = { "PropertyID", "From", "To", "LOB" }; //index placement is important
        private readonly string[] SelectColumns = { "PolicyID", "LocationNumber", "ContractFr", "ContractTo", "LOB" }; //index placement is important
        private string ContractMoveData { get; set; }
        private class Servers
        {
            public string? Server { get; set; }
            public string? Config { get; set; }
        }
        public ContractMove()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        private void ContractMove_Load(object sender, EventArgs e)
        {
            var connections = Program.Configuration.GetSection("Servers").GetChildren();

            foreach (var Servers in connections) cmdServer.

        }

        private void cmdServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServer.Text != "")
            {
                string? currentServerName = cmbServer.SelectedItem.ToString();
                if (currentServerName != null)
                {
                    Servers? currentServer = servers.FirstOrDefault(s => s.Server == currentServerName);
                    if (currentServer != null && currentServer.Server != null && currentServer.Config != null)
                    {
                        //Initialize Program variables
                        Program.Server = currentServer.Server;
                        Program.ServerConfig = currentServer.Config;
                        UpdateDataSource();

                        //Modify element based on server
                        pnlCurrentServer.BackColor = Program.GetServerColor();
                        lblServerName.Text = Program.ServerName;
                        Program.SetContrastingForeColor(lblServerName);
                    }
                    else
                    {
                        MessageBox.Show("Server not found.", "404", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No server selected.", "404", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private static void UpdateDataSource()
        {
            if (!string.IsNullOrEmpty(Program.ServerConfig))
            {
                string targetData = "Data Source=";
                string[]? data = Program.ServerConfig.Split(';');

                if (data != null)
                    foreach (string s in data)
                    {
                        if (s.StartsWith(targetData, StringComparison.OrdinalIgnoreCase)) Program.ServerName = s.Replace(targetData, "");
                    }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using OpenFileDialog UploadExcelFile = new();
            UploadExcelFile.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (UploadExcelFile.ShowDialog() == DialogResult.OK)
            {
                txtUploadedFile.Text = UploadExcelFile.FileName;
                dgvUploadedFile.DataSource = LoadExcel(UploadExcelFile.FileName);
            }
        }

        private static DataTable LoadExcel(string path)
        {
            FileInfo fileInfo = new(path);
            using ExcelPackage package = new(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            DataTable dt = new();

            // Get the column headers from the first row
            for (int i = 1; i <= worksheet.Dimension.Columns; i++)
            {
                dt.Columns.Add(worksheet.Cells[1, i].Text.Trim());
            }

            // Load rows starting from row 2
            for (int i = 2; i <= worksheet.Dimension.Rows; i++)
            {
                DataRow row = dt.NewRow();
                for (int j = 1; j <= worksheet.Dimension.Columns; j++)
                {
                    row[j - 1] = worksheet.Cells[i, j].Text;
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        private void dgvUploadedFile_DataSourceChanged(object sender, EventArgs e)
        {
            //Check if all expected columns are in the excel file provided
            var missingColumns = GetMissingColumns();
            if (missingColumns.Any())
                MessageBox.Show(string.Concat("Check the excel file provided.\nExpected column/s missing:\n\n", string.Join("\n", missingColumns)), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                bool hasInvalidRow = false;
                foreach (DataGridViewRow row in dgvUploadedFile.Rows)
                {

                    foreach (DataGridViewCell rowCell in row.Cells)
                    {
                        if (rowCell.Value == null || string.IsNullOrEmpty(rowCell.Value.ToString())) hasInvalidRow = true;
                    }
                }
                if (hasInvalidRow)
                    MessageBox.Show("Blank cell detected.\nMake sure no cell contains blank string.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    btnUploadData.Enabled = true;
                    btnViewData.Enabled = true;
                }
            }
        }

        private List<string> GetMissingColumns()
        {
            var currentColumns = dgvUploadedFile.Columns.Cast<DataGridViewColumn>()
                                    .Select(col => col.HeaderText)
                                    .ToList();

            var missingColumns = ExpectedColumns.Where(expected =>
                !currentColumns.Contains(expected, StringComparer.OrdinalIgnoreCase)).ToList();

            return missingColumns;
        }

        private void btnUploadData_Click(object sender, EventArgs e)
        {
            if (cmbServer.SelectedIndex == -1 || string.IsNullOrEmpty(Program.ServerConfig))
                MessageBox.Show("No server selected");
            else
            {
                //Process dataGridView
                ContractMoveData = GenerateSQLQuery() ?? string.Empty;
                MessageBox.Show(ContractMoveData, "");
            }

        }

        private string GenerateSQLQuery()
        {
            StringBuilder selectStatement = new();
            foreach (DataGridViewRow rowData in dgvUploadedFile.Rows)
            {
                if (rowData.Cells[ExpectedColumns[0]].Value != null &&
                    rowData.Cells[ExpectedColumns[1]].Value != null &&
                    rowData.Cells[ExpectedColumns[2]].Value != null &&
                    rowData.Cells[ExpectedColumns[3]].Value != null
                    )
                {
                    //Split PropertyID
                    string[] PropertyID = (rowData.Cells[ExpectedColumns[0]].Value?.ToString() ?? "").Split('-');
                    if (PropertyID.Length == 2)
                    {
                        //Parse PropertyID to PolicyID and LocationNumber
                        if (int.TryParse(PropertyID[0], out int PolicyID) && int.TryParse(PropertyID[1], out int LocationNumber))
                        {
                            string ContractFr = (rowData.Cells[ExpectedColumns[1]].Value?.ToString() ?? "");
                            string ContractTo = (rowData.Cells[ExpectedColumns[2]].Value?.ToString() ?? "");
                            string LOB = (rowData.Cells[ExpectedColumns[3]].Value?.ToString() ?? "");

                            selectStatement.AppendFormat(
                                //Query
                                "SELECT {5} AS [{0}], {6} AS [{1}], '{7}' AS [{2}], '{8}' AS [{3}], '{9}' AS [{4}] UNION\n",

                                //Column Headers
                                SelectColumns[0],           //0
                                SelectColumns[1],           //1
                                SelectColumns[2],           //2
                                SelectColumns[3],           //3
                                SelectColumns[4],           //4

                                //Data
                                PolicyID,                   //5
                                LocationNumber,             //6
                                ContractFr,                 //7
                                ContractTo,                 //8
                                LOB                         //9
                               );
                        }
                    }
                }
            }

            if (selectStatement.Length > 0) selectStatement.Length -= 7; //remove trailing UNION

            return selectStatement.ToString();
        }

    }
}