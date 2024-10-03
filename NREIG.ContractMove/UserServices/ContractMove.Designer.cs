namespace NREIG.ContractMove
{
    partial class ContractMove
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblServer = new Label();
            cmbServer = new ComboBox();
            pnlCurrentServer = new Panel();
            lblServerName = new Label();
            btnBrowse = new Button();
            txtUploadedFile = new TextBox();
            dgvUploadedFile = new DataGridView();
            btnUploadData = new Button();
            pnlCurrentServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUploadedFile).BeginInit();
            SuspendLayout();
            // 
            // lblServer
            // 
            lblServer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblServer.AutoSize = true;
            lblServer.Location = new Point(379, 15);
            lblServer.Name = "lblServer";
            lblServer.Size = new Size(82, 15);
            lblServer.TabIndex = 0;
            lblServer.Text = "Choose Server";
            // 
            // cmbServer
            // 
            cmbServer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbServer.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbServer.FormattingEnabled = true;
            cmbServer.Location = new Point(467, 12);
            cmbServer.Name = "cmbServer";
            cmbServer.Size = new Size(121, 23);
            cmbServer.TabIndex = 1;
            cmbServer.SelectedIndexChanged += cmdServer_SelectedIndexChanged;
            // 
            // pnlCurrentServer
            // 
            pnlCurrentServer.Controls.Add(lblServerName);
            pnlCurrentServer.Dock = DockStyle.Bottom;
            pnlCurrentServer.Location = new Point(0, 541);
            pnlCurrentServer.Name = "pnlCurrentServer";
            pnlCurrentServer.Size = new Size(600, 20);
            pnlCurrentServer.TabIndex = 2;
            // 
            // lblServerName
            // 
            lblServerName.AutoSize = true;
            lblServerName.Dock = DockStyle.Right;
            lblServerName.Location = new Point(518, 0);
            lblServerName.Name = "lblServerName";
            lblServerName.Padding = new Padding(0, 3, 0, 0);
            lblServerName.Size = new Size(82, 18);
            lblServerName.TabIndex = 3;
            lblServerName.Text = "Choose Server";
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(513, 51);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 3;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtUploadedFile
            // 
            txtUploadedFile.Location = new Point(12, 52);
            txtUploadedFile.Name = "txtUploadedFile";
            txtUploadedFile.ReadOnly = true;
            txtUploadedFile.Size = new Size(495, 23);
            txtUploadedFile.TabIndex = 4;
            // 
            // dgvUploadedFile
            // 
            dgvUploadedFile.AllowUserToAddRows = false;
            dgvUploadedFile.AllowUserToDeleteRows = false;
            dgvUploadedFile.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUploadedFile.Location = new Point(12, 80);
            dgvUploadedFile.Name = "dgvUploadedFile";
            dgvUploadedFile.ReadOnly = true;
            dgvUploadedFile.RowTemplate.Height = 25;
            dgvUploadedFile.Size = new Size(574, 375);
            dgvUploadedFile.TabIndex = 5;
            dgvUploadedFile.DataSourceChanged += dgvUploadedFile_DataSourceChanged;
            // 
            // btnUploadData
            // 
            btnUploadData.Enabled = false;
            btnUploadData.Location = new Point(70, 466);
            btnUploadData.Name = "btnUploadData";
            btnUploadData.Size = new Size(105, 23);
            btnUploadData.TabIndex = 6;
            btnUploadData.Text = "UPLOAD DATA";
            btnUploadData.UseVisualStyleBackColor = true;
            btnUploadData.Click += btnUploadData_Click;
            // 
            // ContractMove
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 561);
            Controls.Add(btnUploadData);
            Controls.Add(dgvUploadedFile);
            Controls.Add(txtUploadedFile);
            Controls.Add(btnBrowse);
            Controls.Add(pnlCurrentServer);
            Controls.Add(cmbServer);
            Controls.Add(lblServer);
            Name = "ContractMove";
            Text = "ContractMove";
            Load += ContractMove_Load;
            pnlCurrentServer.ResumeLayout(false);
            pnlCurrentServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUploadedFile).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblServer;
        private ComboBox cmbServer;
        private Panel pnlCurrentServer;
        private Label lblServerName;
        private Button btnBrowse;
        private TextBox txtUploadedFile;
        private DataGridView dgvUploadedFile;
        private Button btnUploadData;
    }
}