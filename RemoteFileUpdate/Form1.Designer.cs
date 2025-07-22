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
        private Button btnBackupOnly;
        private Button btnRollbackOnly;


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


        #region Windows Form 디자이너에서 생성한 코드

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
            this.btnUploadOnly = new System.Windows.Forms.Button();
            this.btnBackupOnly = new System.Windows.Forms.Button();
            this.btnRollbackOnly = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).BeginInit();
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
            this.comboProject.TabIndex = 0;
            this.comboProject.SelectedIndexChanged += new System.EventHandler(this.comboProject_SelectedIndexChanged);
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(190, 20);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(78, 21);
            this.txtVersion.TabIndex = 1;
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(495, 18);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(75, 23);
            this.btnAddFile.TabIndex = 2;
            this.btnAddFile.Text = "파일 추가";
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(464, 269);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(106, 23);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "전송";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // gridFiles
            // 
            this.gridFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileNameCol,
            this.TargetPathCol});
            this.gridFiles.Location = new System.Drawing.Point(20, 60);
            this.gridFiles.Name = "gridFiles";
            this.gridFiles.Size = new System.Drawing.Size(550, 200);
            this.gridFiles.TabIndex = 0;
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
            this.lblVersion.Location = new System.Drawing.Point(274, 25);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(53, 12);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "버전정보";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(20, 302);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(550, 255);
            this.txtLog.TabIndex = 4;
            // 
            // btnUploadOnly
            // 
            this.btnUploadOnly.Location = new System.Drawing.Point(316, 269);
            this.btnUploadOnly.Name = "btnUploadOnly";
            this.btnUploadOnly.Size = new System.Drawing.Size(106, 23);
            this.btnUploadOnly.TabIndex = 5;
            this.btnUploadOnly.Text = "백업 없이 전송";
            this.btnUploadOnly.Click += new System.EventHandler(this.btnUploadOnly_Click);
            // 
            // btnBackupOnly
            // 
            this.btnBackupOnly.Location = new System.Drawing.Point(168, 269);
            this.btnBackupOnly.Name = "btnBackupOnly";
            this.btnBackupOnly.Size = new System.Drawing.Size(106, 23);
            this.btnBackupOnly.TabIndex = 6;
            this.btnBackupOnly.Text = "백업만 수행";
            this.btnBackupOnly.Click += new System.EventHandler(this.btnBackupOnly_Click);
            // 
            // btnRollbackOnly
            // 
            this.btnRollbackOnly.Location = new System.Drawing.Point(20, 269);
            this.btnRollbackOnly.Name = "btnRollbackOnly";
            this.btnRollbackOnly.Size = new System.Drawing.Size(106, 23);
            this.btnRollbackOnly.TabIndex = 7;
            this.btnRollbackOnly.Text = "롤백만 전송";
            this.btnRollbackOnly.Click += new System.EventHandler(this.btnRollbackOnly_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(592, 569);
            this.Controls.Add(this.btnRollbackOnly);
            this.Controls.Add(this.btnBackupOnly);
            this.Controls.Add(this.btnUploadOnly);
            this.Controls.Add(this.comboProject);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.gridFiles);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.txtLog);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnUploadOnly;
        private Button button1;
        private Button button2;
    }
}

