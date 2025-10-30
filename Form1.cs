using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppCollect
{
    public partial class Form1 : Form
    {
        private ModbusHelper _modbusHelper;
        ushort startAddress;
        private bool _isReadingContinuously = false;
        private CancellationTokenSource _cancellationTokenSource;

        public Form1()
        {
            InitializeComponent();
            _modbusHelper = new ModbusHelper();
            _modbusHelper.LogMessage += OnLogMessage;
            InitializeControls();

        }

        private void InitializeControls()
        {
            // 初始化串口参数
            cmbPortName.Items.AddRange(SerialPort.GetPortNames());
            if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = 0;

            cmbBaudRate.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            cmbBaudRate.SelectedIndex = 0;

            cmbDataBits.Items.AddRange(new object[] { "7", "8" });
            cmbDataBits.SelectedIndex = 1;

            cmbParity.Items.AddRange(new object[] { "无", "奇校验", "偶校验" });
            cmbParity.SelectedIndex = 0;

            cmbStopBits.Items.AddRange(new object[] { "1", "1.5", "2" });
            cmbStopBits.SelectedIndex = 0;

            // 初始化功能码
            cmbFunctionCode.Items.AddRange(new object[] {
                "01 Read Coils",
                "02 Read Inputs",
                "03 Read Holding Registers",
                "04 Read Input Registers"
            });
            cmbFunctionCode.SelectedIndex = 2;

            // 初始化循环读取间隔
            txtInterval.Text = "1000"; // 默认1秒
        }

        private void OnLogMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(OnLogMessage), message);
                return;
            }

            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
            txtLog.ScrollToCaret();
        }

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

                Parity parity = Parity.None;
                switch (cmbParity.SelectedItem.ToString())
                {
                    case "奇校验": parity = Parity.Odd; break;
                    case "偶校验": parity = Parity.Even; break;
                    default: parity = Parity.None; break;
                }

                StopBits stopBits = StopBits.One;
                switch (cmbStopBits.SelectedItem.ToString())
                {
                    case "1.5": stopBits = StopBits.OnePointFive; break;
                    case "2": stopBits = StopBits.Two; break;
                    default: stopBits = StopBits.One; break;
                }

                bool success = _modbusHelper.Connect(portName, baudRate, dataBits, parity, stopBits);

                if (success)
                {
                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"连接异常: {ex.Message}");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            _modbusHelper.Disconnect();
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!_modbusHelper.IsConnected)
            {
                MessageBox.Show("请先连接串口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                byte slaveAddress = byte.Parse(txtSlaveAddress.Text);
                startAddress = ushort.Parse(txtStartAddress.Text);
                ushort numberOfPoints = ushort.Parse(txtNumberOfPoints.Text);

                string functionCode = cmbFunctionCode.SelectedItem.ToString();

                switch (functionCode)
                {
                    case "01 Read Coils":
                        bool[] coils = _modbusHelper.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("线圈", coils);
                        break;

                    case "02 Read Inputs":
                        bool[] inputs = _modbusHelper.ReadInputs(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("输入状态", inputs);
                        break;

                    case "03 Read Holding Registers":
                        // 使用带激活报文的读取方法
                        OnLogMessage("开始读取保持寄存器...");
                        ushort[] holdingRegisters = _modbusHelper.ReadHoldingRegistersWithActivation(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("保持寄存器", holdingRegisters);
                        break;

                    case "04 Read Input Registers":
                        ushort[] inputRegisters = _modbusHelper.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("输入寄存器", inputRegisters);
                        break;
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"读取数据异常: {ex.Message}");
            }
        }

        private void DisplayReadResult<T>(string dataType, T[] data)
        {
            OnLogMessage($"成功读取{dataType}数据，共{data.Length}个:");

            for (int i = 0; i < data.Length; i++)
            {
                OnLogMessage($"  地址 {i + startAddress}: {data[i]}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _modbusHelper?.Dispose();
            base.OnFormClosing(e);
        }

        // 开始循环读取
        private void btnStartContinuousRead_Click(object sender, EventArgs e)
        {
            if (!_modbusHelper.IsConnected)
            {
                MessageBox.Show("请先连接串口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_isReadingContinuously)
            {
                MessageBox.Show("已经在循环读取中", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 获取读取间隔
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

                OnLogMessage($"开始循环读取，间隔: {interval}ms");

                // 启动循环读取任务
                Task.Run(() => ContinuousReadLoop(interval, _cancellationTokenSource.Token));
            }
            catch (Exception ex)
            {
                OnLogMessage($"启动循环读取失败: {ex.Message}");
                _isReadingContinuously = false;
            }
        }

        // 停止循环读取
        private void btnStopContinuousRead_Click(object sender, EventArgs e)
        {
            StopContinuousReading();
        }

        // 循环读取循环
        private async Task ContinuousReadLoop(int interval, CancellationToken cancellationToken)
        {
            int readCount = 0;

            while (!cancellationToken.IsCancellationRequested && _isReadingContinuously)
            {
                try
                {
                    readCount++;
                    OnLogMessage($"第 {readCount} 次读取...");

                    byte slaveAddress = byte.Parse(txtSlaveAddress.Text);
                    startAddress = ushort.Parse(txtStartAddress.Text);
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
                        OnLogMessage("未选择功能码或控件尚未初始化，跳过本次读取。");
                        await Task.Delay(1000, cancellationToken);
                        continue;
                    }

                    //string functionCode = cmbFunctionCode.SelectedItem.ToString();

                    switch (functionCode)
                    {
                        case "01 Read Coils":
                            bool[] coils = _modbusHelper.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                            DisplayReadResult("线圈", coils);
                            break;

                        case "02 Read Inputs":
                            bool[] inputs = _modbusHelper.ReadInputs(slaveAddress, startAddress, numberOfPoints);
                            DisplayReadResult("输入状态", inputs);
                            break;

                        case "03 Read Holding Registers":
                            ushort[] holdingRegisters = _modbusHelper.ReadHoldingRegistersWithActivation(slaveAddress, startAddress, numberOfPoints);
                            DisplayReadResult("保持寄存器", holdingRegisters);
                            break;

                        case "04 Read Input Registers":
                            ushort[] inputRegisters = _modbusHelper.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
                            DisplayReadResult("输入寄存器", inputRegisters);
                            break;
                    }

                    // 等待指定的间隔时间
                    await Task.Delay(interval, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    // 任务被取消，正常退出
                    break;
                }
                catch (Exception ex)
                {
                    OnLogMessage($"循环读取出错: {ex.Message}");

                    // 出错后等待一段时间再继续
                    await Task.Delay(1000, cancellationToken);
                }
            }

            // 回到UI线程更新按钮状态
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    btnStartContinuousRead.Enabled = true;
                    btnStopContinuousRead.Enabled = false;
                    btnRead.Enabled = true;
                }));
            }
        }

        // 停止循环读取
        private void StopContinuousReading()
        {
            if (_isReadingContinuously)
            {
                _isReadingContinuously = false;
                _cancellationTokenSource?.Cancel();

                btnStartContinuousRead.Enabled = true;
                btnStopContinuousRead.Enabled = false;
                btnRead.Enabled = true;

                OnLogMessage("已停止循环读取");
            }
        }

    }
}