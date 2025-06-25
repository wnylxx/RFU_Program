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
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(190, 20);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(100, 21);
            this.txtVersion.TabIndex = 1;
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(310, 20);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(75, 23);
            this.btnAddFile.TabIndex = 2;
            this.btnAddFile.Text = "파일 추가";
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // gridFiles
            // 
            this.gridFiles.Location = new System.Drawing.Point(20, 60);
            this.gridFiles.Name = "gridFiles";
            this.gridFiles.Size = new System.Drawing.Size(500, 200);
            this.gridFiles.TabIndex = 0;
            this.gridFiles.Columns.AddRange(new DataGridViewColumn[]
            {
                this.FileNameCol, this.TargetPathCol
            });
            this.FileNameCol.HeaderText = "파일명";
            this.FileNameCol.Width = 200;

            this.TargetPathCol.HeaderText = "Client 목적 경로";
            this.TargetPathCol.Width = 280;

            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(420, 270);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "전송";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);

            // 
            // FileNameCol
            // 
            this.FileNameCol.Name = "FileNameCol";
            // 
            // TargetPathCol
            // 
            this.TargetPathCol.Name = "TargetPathCol";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(550, 320);
            this.Controls.Add(this.comboProject);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.gridFiles);
            this.Controls.Add(this.btnUpload);
            this.Name = "Remote File Uploader";
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}

