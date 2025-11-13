using System;
using System.Windows.Forms;

namespace WinFormsAppCollect.Controls
{
    public partial class LogControl : UserControl
    {
        public LogControl()
        {
            InitializeComponent();
        }

        public void AppendLog(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendLog), message);
                return;
            }

            string timestamp = $"[{DateTime.Now:HH:mm:ss}]";
            string formattedMessage = $"{timestamp} {message}\n";

            txtLog.AppendText(formattedMessage);
            txtLog.ScrollToCaret();
        }

        public void ClearLog()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ClearLog));
                return;
            }

            txtLog.Clear();
        }

        // 添加btnClear_Click事件处理方法
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearLog();
        }
    }
}