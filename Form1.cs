using System;
using System.Drawing;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsAppCollect.Services;

namespace WinFormsAppCollect
{
    public partial class Form1 : Form
    {
        private ModbusRtuService _rtuService;
        private ModbusTcpService _tcpService;
        private OpcUaService _opcService;

        public Form1()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            _rtuService = new ModbusRtuService();
            _tcpService = new ModbusTcpService();
            _opcService = new OpcUaService();

            _rtuService.LogMessage += OnLogMessage;
            _tcpService.LogMessage += OnLogMessage;
            _opcService.LogMessage += OnLogMessage;

            InitializeControls();
        }

        private void InitializeControls()
        {
            cmbPortName.Items.AddRange(SerialPort.GetPortNames());
            if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = 0;

            cmbBaudRate.SelectedIndex = 0;
            cmbDataBits.SelectedIndex = 1;
            cmbParity.SelectedIndex = 0;
            cmbStopBits.SelectedIndex = 0;
            cmbFunctionCode.SelectedIndex = 2;
            cmbFunctionCodeTCP.SelectedIndex = 2;
            cmbFunctionCodeOPC.SelectedIndex = 0;
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

        // ========== RTU相关方法 ==========
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (cmbPortName.SelectedItem == null)
            {
                MessageBox.Show("请选择串口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string portName = cmbPortName.SelectedItem.ToString();
                int baudRate = int.Parse(cmbBaudRate.SelectedItem.ToString());
                int dataBits = int.Parse(cmbDataBits.SelectedItem.ToString());

                Parity parity = GetParityFromString(cmbParity.SelectedItem.ToString());
                StopBits stopBits = GetStopBitsFromString(cmbStopBits.SelectedItem.ToString());

                bool success = _rtuService.Connect(portName, baudRate, dataBits, parity, stopBits);

                if (success)
                {
                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;
                    OnLogMessage($"RTU连接成功: {portName}@{baudRate}bps");
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"RTU连接异常: {ex.Message}");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            _rtuService.Disconnect();
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            OnLogMessage("RTU连接已断开");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!_rtuService.IsConnected)
            {
                MessageBox.Show("请先连接串口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReadRTUData();
        }

        private void btnStartContinuousRead_Click(object sender, EventArgs e)
        {
            if (!_rtuService.IsConnected)
            {
                MessageBox.Show("请先连接串口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StartContinuousRTUReading();
        }

        private void btnStopContinuousRead_Click(object sender, EventArgs e)
        {
            StopContinuousRTUReading();
        }

        // ========== TCP连接方法 ==========
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
                    OnLogMessage($"TCP连接成功: {ipAddress}:{port}");
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"TCP连接异常: {ex.Message}");
            }
        }

        private void btnDisconnectTCP_Click(object sender, EventArgs e)
        {
            _tcpService.Disconnect();
            btnConnectTCP.Enabled = true;
            btnDisconnectTCP.Enabled = false;
            OnLogMessage("TCP连接已断开");
        }

        // ========== TCP数据读取方法 ==========
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

        // ========== OPC UA相关方法 ==========
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
                OnLogMessage($"OPC UA连接异常: {ex.Message}");
            }
        }

        private void btnDisconnectOPC_Click(object sender, EventArgs e)
        {
            _opcService.Disconnect();
            btnConnectOPC.Enabled = true;
            btnDisconnectOPC.Enabled = false;
            UpdateOpcButtonStates();
            OnLogMessage("OPC UA连接已断开");
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

        // ========== 数据读取核心方法 ==========
        private void ReadRTUData()
        {
            try
            {
                byte slaveAddress = byte.Parse(txtSlaveAddress.Text);
                ushort startAddress = ushort.Parse(txtStartAddress.Text);
                ushort numberOfPoints = ushort.Parse(txtNumberOfPoints.Text);

                string functionCode = cmbFunctionCode.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(functionCode))
                {
                    MessageBox.Show("请选择功能码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                switch (functionCode)
                {
                    case "01 Read Coils":
                        bool[] coils = _rtuService.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("RTU线圈", coils, startAddress);
                        break;

                    case "02 Read Inputs":
                        bool[] inputs = _rtuService.ReadInputs(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("RTU输入状态", inputs, startAddress);
                        break;

                    case "03 Read Holding Registers":
                        ushort[] holdingRegisters = _rtuService.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("RTU保持寄存器", holdingRegisters, startAddress);
                        break;

                    case "04 Read Input Registers":
                        ushort[] inputRegisters = _rtuService.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("RTU输入寄存器", inputRegisters, startAddress);
                        break;
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"RTU读取数据异常: {ex.Message}");
            }
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
                        DisplayReadResult("TCP线圈", coils, startAddress);
                        break;

                    case "02 Read Inputs":
                        bool[] inputs = _tcpService.ReadInputs(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("TCP输入状态", inputs, startAddress);
                        break;

                    case "03 Read Holding Registers":
                        ushort[] holdingRegisters = _tcpService.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("TCP保持寄存器", holdingRegisters, startAddress);
                        break;

                    case "04 Read Input Registers":
                        ushort[] inputRegisters = _tcpService.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("TCP输入寄存器", inputRegisters, startAddress);
                        break;
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"TCP读取数据异常: {ex.Message}");
            }
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

                OnLogMessage($"OPC UA节点 {nodeId} 读取成功,值: {result.value:F2},时间: {result.timestamp:yyyy-MM-dd HH:mm:ss}");
            }
            catch (Exception ex)
            {
                OnLogMessage($"OPC UA读取数据异常: {ex.Message}");
            }
        }

        // ========== 循环读取方法 ==========
        private void StartContinuousRTUReading()
        {
            if (!int.TryParse(txtInterval.Text, out int interval) || interval < 100)
            {
                MessageBox.Show("请输入有效的间隔时间（毫秒），最小100ms", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte slaveAddress = byte.Parse(txtSlaveAddress.Text);
            ushort startAddress = ushort.Parse(txtStartAddress.Text);
            ushort numberOfPoints = ushort.Parse(txtNumberOfPoints.Text);
            string functionCode = cmbFunctionCode.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(functionCode))
            {
                MessageBox.Show("请选择功能码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnStartContinuousRead.Enabled = false;
            btnStopContinuousRead.Enabled = true;
            btnRead.Enabled = false;

            OnLogMessage($"RTU开始循环读取，间隔: {interval}ms");

            _rtuService.StartContinuousReading(interval, slaveAddress, startAddress, numberOfPoints, functionCode);
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

            OnLogMessage($"TCP开始循环读取，间隔: {interval}ms");

            _tcpService.StartContinuousReading(interval, slaveAddress, startAddress, numberOfPoints, functionCode);
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

        private void StopContinuousRTUReading()
        {
            _rtuService.StopContinuousReading();
            UpdateRTUButtonStates();
            OnLogMessage("RTU已停止循环读取");
        }

        private void StopContinuousTCPReading()
        {
            _tcpService.StopContinuousReading();
            UpdateTCPButtonStates();
            OnLogMessage("TCP已停止循环读取");
        }

        private void StopContinuousOpcReading()
        {
            _opcService.StopContinuousReading();
            UpdateOpcButtonStates();
            OnLogMessage("OPC UA已停止循环读取");
        }

        // ========== 按钮状态更新方法 ==========
        private void UpdateRTUButtonStates()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateRTUButtonStates));
                return;
            }

            btnStartContinuousRead.Enabled = true;
            btnStopContinuousRead.Enabled = false;
            btnRead.Enabled = true;
        }

        private void UpdateTCPButtonStates()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateTCPButtonStates));
                return;
            }

            btnStartContinuousReadTCP.Enabled = true;
            btnStopContinuousReadTCP.Enabled = false;
            btnReadTCP.Enabled = true;
        }

        private void UpdateOpcButtonStates()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateOpcButtonStates));
                return;
            }

            btnStartContinuousReadOPC.Enabled = _opcService.IsConnected;
            btnStopContinuousReadOPC.Enabled = false;
            btnReadOPC.Enabled = _opcService.IsConnected;
        }

        // ========== 辅助方法 ==========
        private void DisplayReadResult<T>(string dataType, T[] data, ushort startAddr)
        {
            OnLogMessage($"成功读取{dataType}数据，共{data.Length}个:");

            for (int i = 0; i < data.Length; i++)
            {
                OnLogMessage($"  地址 {i + startAddr}: {data[i]}");
            }
        }

        private Parity GetParityFromString(string parityStr)
        {
            switch (parityStr)
            {
                case "奇校验": return Parity.Odd;
                case "偶校验": return Parity.Even;
                default: return Parity.None;
            }
        }

        private StopBits GetStopBitsFromString(string stopBitsStr)
        {
            switch (stopBitsStr)
            {
                case "1.5": return StopBits.OnePointFive;
                case "2": return StopBits.Two;
                default: return StopBits.One;
            }
        }

        // ========== 窗体关闭清理 ==========
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _rtuService?.Dispose();
            _tcpService?.Dispose();
            _opcService?.Dispose();
            base.OnFormClosing(e);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }
    }
}