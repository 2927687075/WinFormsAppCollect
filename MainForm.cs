using System;
using System.Windows.Forms;
using WinFormsAppCollect.Forms;

namespace WinFormsAppCollect
{
    public partial class MainForm : Form
    {
        private RtuForm _rtuForm;
        private TcpForm _tcpForm;
        private OpcForm _opcForm;

        public MainForm()
        {
            InitializeComponent();
            InitializeForms();
        }

        private void InitializeForms()
        {
            // 创建子窗体
            _rtuForm = new RtuForm();
            _tcpForm = new TcpForm();
            _opcForm = new OpcForm();

            // 将子窗体添加到TabControl
            tabControl1.TabPages[0].Controls.Add(_rtuForm);
            tabControl1.TabPages[1].Controls.Add(_tcpForm);
            tabControl1.TabPages[2].Controls.Add(_opcForm);

            // 设置子窗体属性
            _rtuForm.Dock = DockStyle.Fill;
            _tcpForm.Dock = DockStyle.Fill;
            _opcForm.Dock = DockStyle.Fill;

            // 订阅日志事件
            _rtuForm.LogMessage += OnLogMessage;
            _tcpForm.LogMessage += OnLogMessage;
            _opcForm.LogMessage += OnLogMessage;
        }

        private void OnLogMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(OnLogMessage), message);
                return;
            }

            string timestamp = $"[{DateTime.Now:HH:mm:ss}]";
            string formattedMessage = $"{timestamp} {message}\n";

            txtLog.AppendText(formattedMessage);
            txtLog.ScrollToCaret();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 清理资源
            _rtuForm?.Dispose();
            _tcpForm?.Dispose();
            _opcForm?.Dispose();
            base.OnFormClosing(e);
        }
    }
}