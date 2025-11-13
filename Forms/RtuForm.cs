using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using WinFormsAppCollect.Services;

namespace WinFormsAppCollect.Forms
{
    public partial class RtuForm : UserControl
    {
        private ModbusRtuService _rtuService;

        public event Action<string> LogMessage;

        public RtuForm()
        {
            InitializeComponent();
            InitializeService();
        }

        private void InitializeService()
        {
            _rtuService = new ModbusRtuService();
            _rtuService.LogMessage += OnLogMessage;
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

            txtInterval.Text = "1000";
            txtSlaveAddress.Text = "1";
            txtStartAddress.Text = "0";
            txtNumberOfPoints.Text = "10";
        }

        private void OnLogMessage(string message)
        {
            LogMessage?.Invoke($"RTU - {message}");
        }

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
                    OnLogMessage($"连接成功: {portName}@{baudRate}bps");
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"连接异常: {ex.Message}");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            _rtuService.Disconnect();
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            OnLogMessage("连接已断开");
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
                        DisplayReadResult("线圈", coils, startAddress);
                        break;

                    case "02 Read Inputs":
                        bool[] inputs = _rtuService.ReadInputs(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("输入状态", inputs, startAddress);
                        break;

                    case "03 Read Holding Registers":
                        ushort[] holdingRegisters = _rtuService.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("保持寄存器", holdingRegisters, startAddress);
                        break;

                    case "04 Read Input Registers":
                        ushort[] inputRegisters = _rtuService.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("输入寄存器", inputRegisters, startAddress);
                        break;
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"读取数据异常: {ex.Message}");
            }
        }

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

            OnLogMessage($"开始循环读取，间隔: {interval}ms");

            _rtuService.StartContinuousReading(interval, slaveAddress, startAddress, numberOfPoints, functionCode);
        }

        private void StopContinuousRTUReading()
        {
            _rtuService.StopContinuousReading();
            UpdateButtonStates();
            OnLogMessage("已停止循环读取");
        }

        private void UpdateButtonStates()
        {
            btnStartContinuousRead.Enabled = true;
            btnStopContinuousRead.Enabled = false;
            btnRead.Enabled = true;
        }

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

        public new void Dispose()
        {
            _rtuService?.Dispose();
            base.Dispose();
        }
    }
}