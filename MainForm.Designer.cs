namespace HTTPInterceptor
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除使用中的資源
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region

        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            lblExePath = new Label();
            btnBrowse = new Button();
            lblTarget = new Label();
            txtTarget = new TextBox();
            lblOrigin = new Label();
            txtOriginList = new TextBox();
            lblStatus = new Label();
            txtExePath = new TextBox();
            btnControl = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.9310341F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.896553F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(lblExePath, 0, 0);
            tableLayoutPanel1.Controls.Add(btnBrowse, 2, 0);
            tableLayoutPanel1.Controls.Add(lblTarget, 0, 1);
            tableLayoutPanel1.Controls.Add(txtTarget, 1, 1);
            tableLayoutPanel1.Controls.Add(lblOrigin, 0, 2);
            tableLayoutPanel1.Controls.Add(lblStatus, 1, 3);
            tableLayoutPanel1.Controls.Add(txtExePath, 1, 0);
            tableLayoutPanel1.Controls.Add(btnControl, 0, 3);
            tableLayoutPanel1.Controls.Add(txtOriginList, 1, 2);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(580, 360);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // lblExePath
            // 
            lblExePath.Anchor = AnchorStyles.None;
            lblExePath.AutoSize = true;
            lblExePath.Font = new Font("Segoe UI", 14F);
            lblExePath.Location = new Point(35, 32);
            lblExePath.Name = "lblExePath";
            lblExePath.Size = new Size(92, 25);
            lblExePath.TabIndex = 0;
            lblExePath.Text = "目標程式:";
            lblExePath.Click += lblExePath_Click;
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.None;
            btnBrowse.Font = new Font("Segoe UI", 12F);
            btnBrowse.Location = new Point(479, 30);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(85, 30);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "瀏覽...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // lblTarget
            // 
            lblTarget.Anchor = AnchorStyles.None;
            lblTarget.AutoSize = true;
            lblTarget.Font = new Font("Segoe UI", 14F);
            lblTarget.Location = new Point(35, 122);
            lblTarget.Name = "lblTarget";
            lblTarget.Size = new Size(92, 25);
            lblTarget.TabIndex = 3;
            lblTarget.Text = "目標地址:";
            lblTarget.Click += lblTarget_Click;
            // 
            // txtTarget
            // 
            txtTarget.Anchor = AnchorStyles.None;
            txtTarget.Font = new Font("Segoe UI", 14F);
            txtTarget.Location = new Point(165, 119);
            txtTarget.Name = "txtTarget";
            txtTarget.Size = new Size(295, 32);
            txtTarget.TabIndex = 4;
            // 
            // lblOrigin
            // 
            lblOrigin.Anchor = AnchorStyles.None;
            lblOrigin.AutoSize = true;
            lblOrigin.Font = new Font("Segoe UI", 14F);
            lblOrigin.Location = new Point(16, 212);
            lblOrigin.Name = "lblOrigin";
            lblOrigin.Size = new Size(130, 25);
            lblOrigin.TabIndex = 3;
            lblOrigin.Text = "原始地址列表:";
            lblOrigin.Click += lblOrigin_Click;
            // 
            // txtOriginList
            // 
            txtOriginList.Anchor = AnchorStyles.None;
            txtOriginList.Font = new Font("Segoe UI", 12F);
            txtOriginList.Location = new Point(165, 210);
            txtOriginList.Name = "txtOriginList";
            txtOriginList.Size = new Size(295, 29);
            txtOriginList.TabIndex = 7;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 14F);
            lblStatus.Location = new Point(165, 302);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(116, 25);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "狀態: 已停止";
            // 
            // txtExePath
            // 
            txtExePath.Anchor = AnchorStyles.None;
            txtExePath.BackColor = SystemColors.Window;
            txtExePath.Font = new Font("Segoe UI", 14F);
            txtExePath.Location = new Point(165, 29);
            txtExePath.Name = "txtExePath";
            txtExePath.ReadOnly = true;
            txtExePath.Size = new Size(295, 32);
            txtExePath.TabIndex = 1;
            txtExePath.TextChanged += txtExePath_TextChanged;
            // 
            // btnControl
            // 
            btnControl.Anchor = AnchorStyles.None;
            btnControl.Font = new Font("Segoe UI", 12F);
            btnControl.Location = new Point(35, 287);
            btnControl.Name = "btnControl";
            btnControl.Size = new Size(92, 55);
            btnControl.TabIndex = 7;
            btnControl.Text = "開始監控";
            btnControl.UseVisualStyleBackColor = true;
            btnControl.Click += btnControl_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 361);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "高級流量管理工具";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblExePath;
        private System.Windows.Forms.TextBox txtExePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Label lblOrigin;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Button btnControl;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtOriginList;
    }
}
