namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bt_InputFile = new System.Windows.Forms.Button();
            this.InputFileName = new System.Windows.Forms.TextBox();
            this.pB_ShowPhoto = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Result = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_ShowPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.bt_InputFile, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.InputFileName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pB_ShowPhoto, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tb_Result, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(537, 373);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // bt_InputFile
            // 
            this.bt_InputFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bt_InputFile.Location = new System.Drawing.Point(4, 4);
            this.bt_InputFile.Name = "bt_InputFile";
            this.bt_InputFile.Size = new System.Drawing.Size(102, 20);
            this.bt_InputFile.TabIndex = 0;
            this.bt_InputFile.Text = "그림 파일 입력";
            this.bt_InputFile.UseVisualStyleBackColor = true;
            this.bt_InputFile.Click += new System.EventHandler(this.bt_InputFile_Click);
            // 
            // InputFileName
            // 
            this.InputFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputFileName.Location = new System.Drawing.Point(113, 4);
            this.InputFileName.Name = "InputFileName";
            this.InputFileName.Size = new System.Drawing.Size(420, 21);
            this.InputFileName.TabIndex = 1;
            // 
            // pB_ShowPhoto
            // 
            this.pB_ShowPhoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pB_ShowPhoto.Location = new System.Drawing.Point(113, 31);
            this.pB_ShowPhoto.Name = "pB_ShowPhoto";
            this.pB_ShowPhoto.Size = new System.Drawing.Size(420, 311);
            this.pB_ShowPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pB_ShowPhoto.TabIndex = 2;
            this.pB_ShowPhoto.TabStop = false;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 311);
            this.label1.TabIndex = 3;
            this.label1.Text = "입력 그림";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 349);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "분류 결과";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_Result
            // 
            this.tb_Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Result.Location = new System.Drawing.Point(113, 349);
            this.tb_Result.Name = "tb_Result";
            this.tb_Result.Size = new System.Drawing.Size(420, 21);
            this.tb_Result.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 373);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "TensorFlowSharp Test";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_ShowPhoto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bt_InputFile;
        private System.Windows.Forms.TextBox InputFileName;
        private System.Windows.Forms.PictureBox pB_ShowPhoto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_Result;
    }
}

