namespace HTTPInterceptor
{
    partial class LogForm
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

        #region Windows Form Designer 產生的程式碼

        private void InitializeComponent()
        {
            txtLog = new TextBox();
            SuspendLayout();
            // 
            // txtLog
            // 
            txtLog.BackColor = SystemColors.Desktop;
            txtLog.Dock = DockStyle.Fill;
            txtLog.ForeColor = SystemColors.ControlLightLight;
            txtLog.Location = new Point(0, 0);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(500, 300);
            txtLog.TabIndex = 0;
            txtLog.TextChanged += txtLog_TextChanged;
            // 
            // LogForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonShadow;
            ClientSize = new Size(500, 300);
            Controls.Add(txtLog);
            ForeColor = SystemColors.ControlLightLight;
            Name = "LogForm";
            Text = "Log";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
    }
}
