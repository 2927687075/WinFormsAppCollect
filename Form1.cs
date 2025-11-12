using Modbus.Device;
using NModbus;
using NModbus.IO;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IModbusMaster = NModbus.IModbusMaster;

namespace WinFormsAppCollect
{
    public partial class Form1 : Form
    {
        private IModbusMaster _modbusTcpMaster;
        private TcpClient _tcpClient;
        private bool _isTcpConnected = false;

        private ModbusHelper _modbusHelper;
        private bool _isReadingContinuously = false;
        private bool _isReadingContinuouslyTCP = false;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationTokenSource _cancellationTokenSourceTCP;

        private bool _isOpcConnected = false;
        private bool _isOpcReadingContinuously = false;
        private CancellationTokenSource _opcCancellationTokenSource;


        public Form1()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            _modbusHelper = new ModbusHelper();
            _modbusHelper.LogMessage += OnLogMessage;
            InitializeControls();
            SetDefaultValues();
        }

        private void InitializeControls()
        {
            // 初始化串口列表
            cmbPortName.Items.AddRange(SerialPort.GetPortNames());
            if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = 0;

            // 设置默认选择
            cmbBaudRate.SelectedIndex = 0;
            cmbDataBits.SelectedIndex = 1;
            cmbParity.SelectedIndex = 0;
            cmbStopBits.SelectedIndex = 0;
            cmbFunctionCode.SelectedIndex = 2;
            cmbFunctionCodeTCP.SelectedIndex = 2;
            cmbFunctionCodeOPC.SelectedIndex = 0;


        }

        private void SetDefaultValues()
        {
            // 设置默认值
            txtInterval.Text = "1000";
            txtIntervalTCP.Text = "1000";

            txtIntervalOPC.Text = "1000";
            txtServerUrl.Text = "opc.tcp://localhost:4840";
            txtNodeId.Text = "ns=2;s=Demo.Dynamic.Scalar.Double";
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
        private void btnConnect_Click(object sender, EventArgs e)
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

                bool success = _modbusHelper.Connect(portName, baudRate, dataBits, parity, stopBits);

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
            _modbusHelper.Disconnect();
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            OnLogMessage("RTU连接已断开");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!_modbusHelper.IsConnected)
            {
                MessageBox.Show("请先连接串口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReadRTUData();
        }

        private void btnStartContinuousRead_Click(object sender, EventArgs e)
        {
            if (!_modbusHelper.IsConnected)
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
        private void btnConnectTCP_Click(object sender, EventArgs e)
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

                // 创建TCP连接 - 正确的实现方式
                _tcpClient = new TcpClient();
                _tcpClient.Connect(ipAddress, port);

                // 创建Modbus TCP主站
                var factory = new ModbusFactory();
                _modbusTcpMaster = factory.CreateMaster(_tcpClient);
                _isTcpConnected = true;

                btnConnectTCP.Enabled = false;
                btnDisconnectTCP.Enabled = true;
                OnLogMessage($"TCP连接成功: {ipAddress}:{port}");
            }
            catch (Exception ex)
            {
                OnLogMessage($"TCP连接异常: {ex.Message}");
                _isTcpConnected = false;
                _modbusTcpMaster?.Dispose();
                _tcpClient?.Close();
            }
        }

        private void btnDisconnectTCP_Click(object sender, EventArgs e)
        {
            try
            {
                _modbusTcpMaster?.Dispose();
                _tcpClient?.Close();
                _modbusTcpMaster = null;
                _tcpClient = null;
                _isTcpConnected = false;

                btnConnectTCP.Enabled = true;
                btnDisconnectTCP.Enabled = false;
                OnLogMessage("TCP连接已断开");
            }
            catch (Exception ex)
            {
                OnLogMessage($"TCP断开异常: {ex.Message}");
            }
        }

        // ========== TCP数据读取方法 ==========
        private void ReadDataTCP(string functionCode, byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            try
            {
                switch (functionCode)
                {
                    case "01 Read Coils":
                        bool[] coils = _modbusTcpMaster.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("TCP线圈", coils, startAddress);
                        break;

                    case "02 Read Inputs":
                        bool[] inputs = _modbusTcpMaster.ReadInputs(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("TCP输入状态", inputs, startAddress);
                        break;

                    case "03 Read Holding Registers":
                        ushort[] holdingRegisters = _modbusTcpMaster.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("TCP保持寄存器", holdingRegisters, startAddress);
                        break;

                    case "04 Read Input Registers":
                        ushort[] inputRegisters = _modbusTcpMaster.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("TCP输入寄存器", inputRegisters, startAddress);
                        break;
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"TCP读取数据异常: {ex.Message}");
                // 发生异常时自动断开连接
                btnDisconnectTCP_Click(null, null);
            }
        }

        // ========== TCP循环读取方法 ==========
        private async Task ContinuousReadLoopTCP(int interval, CancellationToken cancellationToken)
        {
            int readCount = 0;

            while (!cancellationToken.IsCancellationRequested && _isReadingContinuouslyTCP)
            {
                try
                {
                    // 检查TCP连接状态
                    if (!_isTcpConnected || _tcpClient == null || !_tcpClient.Connected)
                    {
                        OnLogMessage("TCP连接已断开，停止循环读取");
                        break;
                    }

                    readCount++;
                    OnLogMessage($"TCP第 {readCount} 次读取...");

                    byte slaveAddress = byte.Parse(txtSlaveAddressTCP.Text);
                    ushort startAddress = ushort.Parse(txtStartAddressTCP.Text);
                    ushort numberOfPoints = ushort.Parse(txtNumberOfPointsTCP.Text);

                    string functionCode = null;
                    // 检查是否需要在UI线程上执行
                    if (cmbFunctionCodeTCP.InvokeRequired)
                    {
                        // 如果需要跨线程访问，使用Invoke在UI线程上执行
                        cmbFunctionCodeTCP.Invoke(new Action(() =>
                        {
                            functionCode = cmbFunctionCodeTCP.SelectedItem?.ToString();
                        }));
                    }
                    else
                    {

                        // 如果已经在UI线程上，直接访问
                        functionCode = cmbFunctionCodeTCP.SelectedItem?.ToString();
                    }
                    if (string.IsNullOrEmpty(functionCode))
                    {
                        OnLogMessage("TCP未选择功能码，跳过本次读取。");
                        await Task.Delay(1000, cancellationToken);
                        continue;
                    }

                    ReadDataTCP(functionCode, slaveAddress, startAddress, numberOfPoints);
                    await Task.Delay(interval, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    OnLogMessage($"TCP循环读取出错: {ex.Message}");
                    await Task.Delay(1000, cancellationToken);
                }
            }

            UpdateTCPButtonStates();
        }

        // ========== 窗体关闭时的清理 ==========
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 清理资源
            _modbusHelper?.Dispose();
            _modbusTcpMaster?.Dispose();
            _tcpClient?.Close();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSourceTCP?.Cancel();
            _opcCancellationTokenSource?.Cancel();

            base.OnFormClosing(e);
        }


        private void btnReadTCP_Click(object sender, EventArgs e)
        {
            if (!_isTcpConnected)
            {
                MessageBox.Show("请先连接TCP", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReadTCPData();
        }

        private void btnStartContinuousReadTCP_Click(object sender, EventArgs e)
        {
            if (!_isTcpConnected)
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

        // ========== 数据读取核心方法 ==========
        private void ReadRTUData()
        {
            try
            {
                byte slaveAddress = byte.Parse(txtSlaveAddress.Text);
                ushort startAddress = ushort.Parse(txtStartAddress.Text);
                ushort numberOfPoints = ushort.Parse(txtNumberOfPoints.Text);

                string functionCode = null;
                // 检查是否需要在UI线程上执行
                if (cmbFunctionCode.InvokeRequired)
                {
                    // 如果需要跨线程访问，使用Invoke在UI线程上执行
                    cmbFunctionCode.Invoke(new Action(() =>
                    {
                        functionCode = cmbFunctionCode.SelectedItem?.ToString();
                    }));
                }
                else
                {

                    // 如果已经在UI线程上，直接访问
                    functionCode = cmbFunctionCode.SelectedItem?.ToString();
                }
                ReadDataRTU(functionCode, slaveAddress, startAddress, numberOfPoints);
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

                string functionCode = null;
                // 检查是否需要在UI线程上执行
                if (cmbFunctionCode.InvokeRequired)
                {
                    // 如果需要跨线程访问，使用Invoke在UI线程上执行
                    cmbFunctionCode.Invoke(new Action(() =>
                    {
                        functionCode = cmbFunctionCode.SelectedItem?.ToString();
                    }));
                }
                else
                {

                    // 如果已经在UI线程上，直接访问
                    functionCode = cmbFunctionCode.SelectedItem?.ToString();
                }
                ReadDataTCP(functionCode, slaveAddress, startAddress, numberOfPoints);
            }
            catch (Exception ex)
            {
                OnLogMessage($"TCP读取数据异常: {ex.Message}");
            }
        }

        private void ReadDataRTU(string functionCode, byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            switch (functionCode)
            {
                case "01 Read Coils":
                    bool[] coils = _modbusHelper.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                    DisplayReadResult("RTU线圈", coils, startAddress);
                    break;

                case "02 Read Inputs":
                    bool[] inputs = _modbusHelper.ReadInputs(slaveAddress, startAddress, numberOfPoints);
                    DisplayReadResult("RTU输入状态", inputs, startAddress);
                    break;

                case "03 Read Holding Registers":
                    ushort[] holdingRegisters = _modbusHelper.ReadHoldingRegistersWithActivation(slaveAddress, startAddress, numberOfPoints);
                    DisplayReadResult("RTU保持寄存器", holdingRegisters, startAddress);
                    break;

                case "04 Read Input Registers":
                    ushort[] inputRegisters = _modbusHelper.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
                    DisplayReadResult("RTU输入寄存器", inputRegisters, startAddress);
                    break;
            }
        }

        private void DisplayReadResult<T>(string dataType, T[] data, ushort startAddr)
        {
            OnLogMessage($"成功读取{dataType}数据，共{data.Length}个:");

            for (int i = 0; i < data.Length; i++)
            {
                OnLogMessage($"  地址 {i + startAddr}: {data[i]}");
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

            _isReadingContinuously = true;
            _cancellationTokenSource = new CancellationTokenSource();

            btnStartContinuousRead.Enabled = false;
            btnStopContinuousRead.Enabled = true;
            btnRead.Enabled = false;

            OnLogMessage($"RTU开始循环读取，间隔: {interval}ms");

            Task.Run(() => ContinuousReadLoopRTU(interval, _cancellationTokenSource.Token));
        }

        private void StartContinuousTCPReading()
        {
            if (!int.TryParse(txtIntervalTCP.Text, out int interval) || interval < 100)
            {
                MessageBox.Show("请输入有效的间隔时间（毫秒），最小100ms", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _isReadingContinuouslyTCP = true;
            _cancellationTokenSourceTCP = new CancellationTokenSource();

            btnStartContinuousReadTCP.Enabled = false;
            btnStopContinuousReadTCP.Enabled = true;
            btnReadTCP.Enabled = false;

            OnLogMessage($"TCP开始循环读取，间隔: {interval}ms");

            Task.Run(() => ContinuousReadLoopTCP(interval, _cancellationTokenSourceTCP.Token));
        }

        private async Task ContinuousReadLoopRTU(int interval, CancellationToken cancellationToken)
        {
            int readCount = 0;

            while (!cancellationToken.IsCancellationRequested && _isReadingContinuously)
            {
                try
                {
                    readCount++;
                    OnLogMessage($"RTU第 {readCount} 次读取...");

                    byte slaveAddress = byte.Parse(txtSlaveAddress.Text);
                    ushort startAddress = ushort.Parse(txtStartAddress.Text);
                    ushort numberOfPoints = ushort.Parse(txtNumberOfPoints.Text);

                    string functionCode = null;
                    // 检查是否需要在UI线程上执行
                    if (cmbFunctionCode.InvokeRequired)
                    {
                        // 如果需要跨线程访问，使用Invoke在UI线程上执行
                        cmbFunctionCode.Invoke(new Action(() =>
                        {
                            functionCode = cmbFunctionCode.SelectedItem?.ToString();
                        }));
                    }
                    else
                    {

                        // 如果已经在UI线程上，直接访问
                        functionCode = cmbFunctionCode.SelectedItem?.ToString();
                    }
                    if (string.IsNullOrEmpty(functionCode))
                    {
                        OnLogMessage("RTU未选择功能码，跳过本次读取。");
                        await Task.Delay(1000, cancellationToken);
                        continue;
                    }

                    ReadDataRTU(functionCode, slaveAddress, startAddress, numberOfPoints);
                    await Task.Delay(interval, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    OnLogMessage($"RTU循环读取出错: {ex.Message}");
                    await Task.Delay(1000, cancellationToken);
                }
            }

            UpdateRTUButtonStates();
        }

        private void StopContinuousRTUReading()
        {
            if (_isReadingContinuously)
            {
                _isReadingContinuously = false;
                _cancellationTokenSource?.Cancel();
                UpdateRTUButtonStates();
                OnLogMessage("RTU已停止循环读取");
            }
        }

        private void StopContinuousTCPReading()
        {
            if (_isReadingContinuouslyTCP)
            {
                _isReadingContinuouslyTCP = false;
                _cancellationTokenSourceTCP?.Cancel();
                UpdateTCPButtonStates();
                OnLogMessage("TCP已停止循环读取");
            }
        }

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

        // ========== 辅助方法 ==========
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

        // ========== OPC UA 方法 ==========
        private void btnConnectOPC_Click(object sender, EventArgs e)
        {
            try
            {
                string serverUrl = txtServerUrl.Text.Trim();
                if (string.IsNullOrEmpty(serverUrl))
                {
                    MessageBox.Show("请输入服务器URL", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ConnectToOpcServer(serverUrl);
            }
            catch (Exception ex)
            {
                OnLogMessage($"OPC UA连接异常: {ex.Message}");
            }
        }

        private void ConnectToOpcServer(string serverUrl)
        {
            try
            {
                // 模拟OPC UA连接
                _isOpcConnected = true;
                btnConnectOPC.Enabled = false;
                btnDisconnectOPC.Enabled = true;
                OnLogMessage($"OPC UA连接成功: {serverUrl}");
                OnLogMessage("注意：这是OPC UA的简化实现，需要安装正确的NuGet包");
            }
            catch (Exception ex)
            {
                OnLogMessage($"OPC UA连接失败: {ex.Message}");
                _isOpcConnected = false;
            }
        }

        private void btnDisconnectOPC_Click(object sender, EventArgs e)
        {
            DisconnectOpcServer();
        }

        private void DisconnectOpcServer()
        {
            try
            {
                _isOpcConnected = false;
                _isOpcReadingContinuously = false;
                _opcCancellationTokenSource?.Cancel();

                btnConnectOPC.Enabled = true;
                btnDisconnectOPC.Enabled = false;
                btnStartContinuousReadOPC.Enabled = true;
                btnStopContinuousReadOPC.Enabled = false;
                btnReadOPC.Enabled = true;

                OnLogMessage("OPC UA连接已断开");
            }
            catch (Exception ex)
            {
                OnLogMessage($"OPC UA断开异常: {ex.Message}");
            }
        }

        private void btnReadOPC_Click(object sender, EventArgs e)
        {
            if (!_isOpcConnected)
            {
                MessageBox.Show("请先连接OPC UA服务器", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReadOpcData();
        }

        private void ReadOpcData()
        {
            try
            {
                string nodeId = txtNodeId.Text.Trim();
                if (string.IsNullOrEmpty(nodeId))
                {
                    MessageBox.Show("请输入节点ID", "极提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ReadOpcNodeValue(nodeId);
            }
            catch (Exception ex)
            {
                OnLogMessage($"OPC UA读取数据异常: {ex.Message}");
            }
        }

        private void ReadOpcNodeValue(string nodeId)
        {
            try
            {
                // 模拟读取OPC UA数据
                Random rand = new Random();
                double value = rand.NextDouble() * 100;
                DateTime timestamp = DateTime.Now;

                OnLogMessage($"OPC UA节点 {nodeId} 读取成功:");
                OnLogMessage($"  值: {value:F2}");
                OnLogMessage($"  时间: {timestamp:yyyy-MM-dd HH:mm:ss}");
                OnLogMessage($"  质量: Good");
            }
            catch (Exception ex)
            {
                OnLogMessage($"OPC UA节点读取异常: {ex.Message}");
            }
        }

        private void btnStartContinuousReadOPC_Click(object sender, EventArgs e)
        {
            if (!_isOpcConnected)
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

            _isOpcReadingContinuously = true;
            _opcCancellationTokenSource = new CancellationTokenSource();

            btnStartContinuousReadOPC.Enabled = false;
            btnStopContinuousReadOPC.Enabled = true;
            btnReadOPC.Enabled = false;

            OnLogMessage($"OPC UA开始循环读取，间隔: {interval}ms");

            Task.Run(() => ContinuousOpcReadLoop(interval, nodeId, _opcCancellationTokenSource.Token));
        }

        private async Task ContinuousOpcReadLoop(int interval, string nodeId, CancellationToken cancellationToken)
        {
            int readCount = 0;
            Random rand = new Random();

            while (!cancellationToken.IsCancellationRequested && _isOpcReadingContinuously)
            {
                try
                {
                    readCount++;
                    OnLogMessage($"OPC UA第 {readCount} 极次读取...");

                    // 模拟读取数据
                    double value = rand.NextDouble() * 100;
                    DateTime timestamp = DateTime.Now;

                    OnLogMessage($"OPC UA节点 {nodeId}:");
                    OnLogMessage($"  值: {value:F2}");
                    OnLogMessage($"  时间: {timestamp:HH:mm:ss}");
                    OnLogMessage($"  质量: Good");

                    await Task.Delay(interval, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    OnLogMessage($"OPC UA循环读取出错: {ex.Message}");
                    await Task.Delay(1000, cancellationToken);
                }
            }

            UpdateOpcButtonStates();
        }

        private void StopContinuousOpcReading()
        {
            if (_isOpcReadingContinuously)
            {
                _isOpcReadingContinuously = false;
                _opcCancellationTokenSource?.Cancel();
                UpdateOpcButtonStates();
                OnLogMessage("OPC UA已停止循环读取");
            }
        }

        private void UpdateOpcButtonStates()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateOpcButtonStates));
                return;
            }

            btnStartContinuousReadOPC.Enabled = true;
            btnStopContinuousReadOPC.Enabled = false;
            btnReadOPC.Enabled = true;
        }

        private void btnBrowseOPC_Click(object sender, EventArgs e)
        {
            if (!_isOpcConnected)
            {
                MessageBox.Show("请先连接OPC UA服务器", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BrowseOpcServer();
        }

        private void BrowseOpcServer()
        {
            try
            {
                // 模拟浏览节点
                OnLogMessage("=== OPC UA服务器节点浏览（模拟）===");
                OnLogMessage("节点: Temperature (ID: ns=2;s=Temperature)");
                OnLogMessage("节点: Pressure (ID: ns=2;s=Pressure)");
                OnLogMessage("节点: FlowRate (ID: ns极=2;s=FlowRate)");
                OnLogMessage("极节点: Level (ID: ns=2;s=Level)");
                OnLogMessage("节点: Voltage (ID: ns=2;s=Voltage)");
                OnLogMessage("节点: Current (ID: ns=极2;s=Current)");
            }
            catch (Exception ex)
            {
                OnLogMessage($"浏览服务器节点失败: {ex.Message}");
            }
        }


        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }
    }
}