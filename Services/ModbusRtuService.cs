using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppCollect.Services
{
    public class ModbusRtuService
    {
        private ModbusHelper _modbusHelper;
        private bool _isReadingContinuously = false;
        private CancellationTokenSource _cancellationTokenSource;

        public event Action<string> LogMessage;

        public bool IsConnected => _modbusHelper?.IsConnected ?? false;

        public ModbusRtuService()
        {
            _modbusHelper = new ModbusHelper();
            _modbusHelper.LogMessage += OnLogMessage;
        }

        public bool Connect(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits)
        {
            return _modbusHelper.Connect(portName, baudRate, dataBits, parity, stopBits);
        }

        public void Disconnect()
        {
            _modbusHelper.Disconnect();
        }

        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return _modbusHelper.ReadCoils(slaveAddress, startAddress, numberOfPoints);
        }

        public bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return _modbusHelper.ReadInputs(slaveAddress, startAddress, numberOfPoints);
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return _modbusHelper.ReadHoldingRegistersWithActivation(slaveAddress, startAddress, numberOfPoints);
        }

        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return _modbusHelper.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
        }

        public void StartContinuousReading(int interval, byte slaveAddress, ushort startAddress, ushort numberOfPoints, string functionCode)
        {
            _isReadingContinuously = true;
            _cancellationTokenSource = new CancellationTokenSource();

            Task.Run(() => ContinuousReadLoop(interval, slaveAddress, startAddress, numberOfPoints, functionCode, _cancellationTokenSource.Token));
        }

        public void StopContinuousReading()
        {
            _isReadingContinuously = false;
            _cancellationTokenSource?.Cancel();
        }

        private async Task ContinuousReadLoop(int interval, byte slaveAddress, ushort startAddress, ushort numberOfPoints, string functionCode, CancellationToken cancellationToken)
        {
            int readCount = 0;

            while (!cancellationToken.IsCancellationRequested && _isReadingContinuously)
            {
                try
                {
                    readCount++;
                    OnLogMessage($"RTU第 {readCount} 次读取...");

                    ReadData(functionCode, slaveAddress, startAddress, numberOfPoints);
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
        }

        private void ReadData(string functionCode, byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            switch (functionCode)
            {
                case "01 Read Coils":
                    bool[] coils = ReadCoils(slaveAddress, startAddress, numberOfPoints);
                    DisplayReadResult("RTU线圈", coils, startAddress);
                    break;

                case "02 Read Inputs":
                    bool[] inputs = ReadInputs(slaveAddress, startAddress, numberOfPoints);
                    DisplayReadResult("RTU输入状态", inputs, startAddress);
                    break;

                case "03 Read Holding Registers":
                    ushort[] holdingRegisters = ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                    DisplayReadResult("RTU保持寄存器", holdingRegisters, startAddress);
                    break;

                case "04 Read Input Registers":
                    ushort[] inputRegisters = ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
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

        private void OnLogMessage(string message)
        {
            LogMessage?.Invoke(message);
        }

        public void Dispose()
        {
            _modbusHelper?.Dispose();
            _cancellationTokenSource?.Cancel();
        }
    }
}