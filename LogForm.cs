using System;
using System.Windows.Forms;

namespace HTTPInterceptor
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 將新日誌訊息加入多行文字框
        /// </summary>
        public void AppendLog(string message)
        {
            txtLog.AppendText(message + Environment.NewLine);
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            // 可根據需要做進一步處理
        }
    }
}
