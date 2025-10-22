using System.Windows.Forms;

namespace RemoteFileUpdate
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private ComboBox comboProject;
        private TextBox txtVersion;
        private Button btnAddFile;
        private Button btnUpload;
        private DataGridView gridFiles;
        private DataGridViewTextBoxColumn FileNameCol;
        private DataGridViewTextBoxColumn TargetPathCol;
        private Label lblVersion;
        private TextBox txtLog;
        private DataGridView gridDevices;
        private DataGridViewTextBoxColumn DeviceIdCol;
        private DataGridViewTextBoxColumn StatusCol;
        private DataGridViewTextBoxColumn VersionCol;
        private DataGridViewTextBoxColumn UpdateResultCol;
        private Button btnRollbackOnly;
        private Button btnClearFiles;


        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        //#region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboProject = new System.Windows.Forms.ComboBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.gridFiles = new System.Windows.Forms.DataGridView();
            this.FileNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetPathCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnRollbackOnly = new System.Windows.Forms.Button();
            this.gridDevices = new System.Windows.Forms.DataGridView();
            this.DeviceIdCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VersionCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateResultCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClearFiles = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDevices)).BeginInit();
            this.SuspendLayout();
            // 
            // comboProject
            // 
            this.comboProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboProject.Items.AddRange(new object[] {
            "CU"});
            this.comboProject.Location = new System.Drawing.Point(20, 20);
            this.comboProject.Name = "comboProject";
            this.comboProject.Size = new System.Drawing.Size(150, 20);
            this.comboProject.TabIndex = 3;
            this.comboProject.SelectedIndexChanged += new System.EventHandler(this.comboProject_SelectedIndexChanged);
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(190, 20);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(78, 21);
            this.txtVersion.TabIndex = 4;
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(415, 18);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(75, 25);
            this.btnAddFile.TabIndex = 5;
            this.btnAddFile.Text = "파일 추가";
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(464, 270);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(106, 25);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Text = "업데이트";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // gridFiles
            // 
            this.gridFiles.AllowUserToAddRows = false;
            this.gridFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileNameCol,
            this.TargetPathCol});
            this.gridFiles.Location = new System.Drawing.Point(20, 60);
            this.gridFiles.Name = "gridFiles";
            this.gridFiles.RowHeadersVisible = false;
            this.gridFiles.Size = new System.Drawing.Size(550, 200);
            this.gridFiles.TabIndex = 6;
            // 
            // FileNameCol
            // 
            this.FileNameCol.HeaderText = "파일명";
            this.FileNameCol.Name = "FileNameCol";
            this.FileNameCol.Width = 230;
            // 
            // TargetPathCol
            // 
            this.TargetPathCol.HeaderText = "Client 목적 경로";
            this.TargetPathCol.Name = "TargetPathCol";
            this.TargetPathCol.Width = 310;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(274, 24);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(53, 12);
            this.lblVersion.TabIndex = 7;
            this.lblVersion.Text = "버전정보";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(20, 310);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(550, 280);
            this.txtLog.TabIndex = 8;
            // 
            // btnRollbackOnly
            // 
            this.btnRollbackOnly.Location = new System.Drawing.Point(20, 270);
            this.btnRollbackOnly.Name = "btnRollbackOnly";
            this.btnRollbackOnly.Size = new System.Drawing.Size(106, 25);
            this.btnRollbackOnly.TabIndex = 1;
            this.btnRollbackOnly.Text = "롤백";
            this.btnRollbackOnly.Click += new System.EventHandler(this.btnRollbackOnly_Click);
            // 
            // gridDevices
            // 
            this.gridDevices.AllowUserToAddRows = false;
            this.gridDevices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeviceIdCol,
            this.StatusCol,
            this.VersionCol,
            this.UpdateResultCol});
            this.gridDevices.Location = new System.Drawing.Point(590, 20);
            this.gridDevices.Name = "gridDevices";
            this.gridDevices.ReadOnly = true;
            this.gridDevices.RowHeadersVisible = false;
            this.gridDevices.Size = new System.Drawing.Size(506, 570);
            this.gridDevices.TabIndex = 0;
            // 
            // DeviceIdCol
            // 
            this.DeviceIdCol.HeaderText = "Device ID";
            this.DeviceIdCol.Name = "DeviceIdCol";
            this.DeviceIdCol.ReadOnly = true;
            this.DeviceIdCol.Width = 120;
            // 
            // StatusCol
            // 
            this.StatusCol.HeaderText = "연결 상태";
            this.StatusCol.Name = "StatusCol";
            this.StatusCol.ReadOnly = true;
            // 
            // VersionCol
            // 
            this.VersionCol.HeaderText = "현재 버전";
            this.VersionCol.Name = "VersionCol";
            this.VersionCol.ReadOnly = true;
            this.VersionCol.Width = 120;
            // 
            // UpdateResultCol
            // 
            this.UpdateResultCol.HeaderText = "업데이트 결과";
            this.UpdateResultCol.Name = "UpdateResultCol";
            this.UpdateResultCol.ReadOnly = true;
            this.UpdateResultCol.Width = 160;
            // 
            // btnClearFiles
            // 
            this.btnClearFiles.Location = new System.Drawing.Point(495, 18);
            this.btnClearFiles.Name = "btnClearFiles";
            this.btnClearFiles.Size = new System.Drawing.Size(75, 25);
            this.btnClearFiles.TabIndex = 3;
            this.btnClearFiles.Text = "파일 삭제";
            this.btnClearFiles.Click += new System.EventHandler(this.btnClearFiles_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1140, 620);
            this.Controls.Add(this.gridDevices);
            this.Controls.Add(this.btnRollbackOnly);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.comboProject);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.gridFiles);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnClearFiles);
            this.Name = "Form1";
            this.Text = "Remote File Update";
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDevices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

