using System;
using System.Windows.Forms;
using WinFormsAppCollect.Services;

namespace WinFormsAppCollect.Forms
{
    public partial class TcpForm : UserControl
    {
        private ModbusTcpService _tcpService;

        public event Action<string> LogMessage;

        public TcpForm()
        {
            InitializeComponent();
            InitializeService();
        }

        private void InitializeService()
        {
            _tcpService = new ModbusTcpService();
            _tcpService.LogMessage += OnLogMessage;
            InitializeControls();
        }

        private void InitializeControls()
        {
            cmbFunctionCodeTCP.SelectedIndex = 2;
            txtIntervalTCP.Text = "1000";
            txtSlaveAddressTCP.Text = "1";
            txtStartAddressTCP.Text = "0";
            txtNumberOfPointsTCP.Text = "10";
            txtIPAddress.Text = "127.0.0.1";
            txtPort.Text = "502";
        }

        private void OnLogMessage(string message)
        {
            LogMessage?.Invoke($"TCP - {message}");
        }

        private async void btnConnectTCP_Click(object sender, EventArgs e)
        {
            try
            {
                string ipAddress = txtIPAddress.Text.Trim();
                if (string.IsNullOrEmpty(ipAddress))
                {
                    MessageBox.Show("请输入IP地址", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtPort.Text, out int port) || port < 1 || port > 65535)
                {
                    MessageBox.Show("请输入有效的端口号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success = await _tcpService.ConnectAsync(ipAddress, port);

                if (success)
                {
                    btnConnectTCP.Enabled = false;
                    btnDisconnectTCP.Enabled = true;
                    OnLogMessage($"连接成功: {ipAddress}:{port}");
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"连接异常: {ex.Message}");
            }
        }

        private void btnDisconnectTCP_Click(object sender, EventArgs e)
        {
            _tcpService.Disconnect();
            btnConnectTCP.Enabled = true;
            btnDisconnectTCP.Enabled = false;
        }

        private void btnReadTCP_Click(object sender, EventArgs e)
        {
            if (!_tcpService.IsConnected)
            {
                MessageBox.Show("请先连接TCP", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReadTCPData();
        }

        private void btnStartContinuousReadTCP_Click(object sender, EventArgs e)
        {
            if (!_tcpService.IsConnected)
            {
                MessageBox.Show("请先连接TCP", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StartContinuousTCPReading();
        }

        private void btnStopContinuousReadTCP_Click(object sender, EventArgs e)
        {
            StopContinuousTCPReading();
        }

        private void ReadTCPData()
        {
            try
            {
                byte slaveAddress = byte.Parse(txtSlaveAddressTCP.Text);
                ushort startAddress = ushort.Parse(txtStartAddressTCP.Text);
                ushort numberOfPoints = ushort.Parse(txtNumberOfPointsTCP.Text);

                string functionCode = cmbFunctionCodeTCP.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(functionCode))
                {
                    MessageBox.Show("请选择功能码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                switch (functionCode)
                {
                    case "01 Read Coils":
                        bool[] coils = _tcpService.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("线圈", coils, startAddress);
                        break;

                    case "02 Read Inputs":
                        bool[] inputs = _tcpService.ReadInputs(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("输入状态", inputs, startAddress);
                        break;

                    case "03 Read Holding Registers":
                        ushort[] holdingRegisters = _tcpService.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("保持寄存器", holdingRegisters, startAddress);
                        break;

                    case "04 Read Input Registers":
                        ushort[] inputRegisters = _tcpService.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("输入寄存器", inputRegisters, startAddress);
                        break;
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"读取数据异常: {ex.Message}");
            }
        }

        private void StartContinuousTCPReading()
        {
            if (!int.TryParse(txtIntervalTCP.Text, out int interval) || interval < 100)
            {
                MessageBox.Show("请输入有效的间隔时间（毫秒），最小100ms", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte slaveAddress = byte.Parse(txtSlaveAddressTCP.Text);
            ushort startAddress = ushort.Parse(txtStartAddressTCP.Text);
            ushort numberOfPoints = ushort.Parse(txtNumberOfPointsTCP.Text);
            string functionCode = cmbFunctionCodeTCP.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(functionCode))
            {
                MessageBox.Show("请选择功能码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnStartContinuousReadTCP.Enabled = false;
            btnStopContinuousReadTCP.Enabled = true;
            btnReadTCP.Enabled = false;

            OnLogMessage($"开始循环读取，间隔: {interval}ms");

            _tcpService.StartContinuousReading(interval, slaveAddress, startAddress, numberOfPoints, functionCode);
        }

        private void StopContinuousTCPReading()
        {
            _tcpService.StopContinuousReading();
            UpdateButtonStates();
            OnLogMessage("已停止循环读取");
        }

        private void UpdateButtonStates()
        {
            btnStartContinuousReadTCP.Enabled = true;
            btnStopContinuousReadTCP.Enabled = false;
            btnReadTCP.Enabled = true;
        }

        private void DisplayReadResult<T>(string dataType, T[] data, ushort startAddr)
        {
            OnLogMessage($"成功读取{dataType}数据，共{data.Length}个:");

            for (int i = 0; i < data.Length; i++)
            {
                OnLogMessage($"  地址 {i + startAddr}: {data[i]}");
            }
        }

        public new void Dispose()
        {
            _tcpService?.Dispose();
            base.Dispose();
        }
    }
}