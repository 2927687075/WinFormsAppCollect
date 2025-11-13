using Opc.Ua;
using System;
using System.Windows.Forms;
using WinFormsAppCollect.Services;

namespace WinFormsAppCollect.Forms
{
    public partial class OpcForm : UserControl
    {
        private OpcUaService _opcService;

        public event Action<string> LogMessage;

        public OpcForm()
        {
            InitializeComponent();
            InitializeService();
        }

        private void InitializeService()
        {
            _opcService = new OpcUaService();
            _opcService.LogMessage += OnLogMessage;
            InitializeControls();
        }

        private void InitializeControls()
        {
            txtIntervalOPC.Text = "1000";
            txtServerUrl.Text = "opc.tcp://localhost:4840";
            txtNodeId.Text = "ns=2;s=Demo.Dynamic.Scalar.Double";
        }

        private void OnLogMessage(string message)
        {
            LogMessage?.Invoke($"OPC UA - {message}");
        }

        private async void btnConnectOPC_Click(object sender, EventArgs e)
        {
            try
            {
                string serverUrl = txtServerUrl.Text.Trim();
                if (string.IsNullOrEmpty(serverUrl))
                {
                    MessageBox.Show("请输入服务器URL", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = await _opcService.ConnectAsync(serverUrl);

                if (success)
                {
                    btnConnectOPC.Enabled = false;
                    btnDisconnectOPC.Enabled = true;
                    btnStartContinuousReadOPC.Enabled = true;
                    btnStopContinuousReadOPC.Enabled = true;
                    btnReadOPC.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"连接异常: {ex.Message}");
            }
        }

        private void btnDisconnectOPC_Click(object sender, EventArgs e)
        {
            _opcService.Disconnect();
            btnConnectOPC.Enabled = true;
            btnDisconnectOPC.Enabled = false;
            UpdateButtonStates();
            OnLogMessage("连接已断开");
        }

        private void btnReadOPC_Click(object sender, EventArgs e)
        {
            if (!_opcService.IsConnected)
            {
                MessageBox.Show("请先连接OPC UA服务器", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReadOpcData();
        }

        private void btnStartContinuousReadOPC_Click(object sender, EventArgs e)
        {
            if (!_opcService.IsConnected)
            {
                MessageBox.Show("请先连接OPC UA服务器", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StartContinuousOpcReading();
        }

        private void btnStopContinuousReadOPC_Click(object sender, EventArgs e)
        {
            StopContinuousOpcReading();
        }

        private void ReadOpcData()
        {
            try
            {
                string nodeId = txtNodeId.Text.Trim();
                if (string.IsNullOrEmpty(nodeId))
                {
                    MessageBox.Show("请输入节点ID", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = _opcService.ReadNodeValue(nodeId);

                OnLogMessage($"节点 {nodeId} 读取成功,值: {result.value:F2},时间: {result.timestamp:yyyy-MM-dd HH:mm:ss}");
            }
            catch (Exception ex)
            {
                OnLogMessage($"读取数据异常: {ex.Message}");
            }
        }

        private void StartContinuousOpcReading()
        {
            if (!int.TryParse(txtIntervalOPC.Text, out int interval) || interval < 100)
            {
                MessageBox.Show("请输入有效的间隔时间（毫秒），最小100ms", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nodeId = txtNodeId.Text.Trim();
            if (string.IsNullOrEmpty(nodeId))
            {
                MessageBox.Show("请输入节点ID", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnStartContinuousReadOPC.Enabled = false;
            btnStopContinuousReadOPC.Enabled = true;
            btnReadOPC.Enabled = false;
            _opcService.StartContinuousReading(interval, nodeId);
        }

        private void StopContinuousOpcReading()
        {
            _opcService.StopContinuousReading();
            UpdateButtonStates();
            OnLogMessage("已停止循环读取");
        }

        private void UpdateButtonStates()
        {
            btnStartContinuousReadOPC.Enabled = _opcService.IsConnected;
            btnStopContinuousReadOPC.Enabled = false;
            btnReadOPC.Enabled = _opcService.IsConnected;
        }

        public new void Dispose()
        {
            _opcService?.Dispose();
            base.Dispose();
        }
    }
}