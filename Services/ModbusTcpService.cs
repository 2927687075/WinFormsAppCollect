using NModbus;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using IModbusMaster = NModbus.IModbusMaster;

namespace WinFormsAppCollect.Services
{
    public class ModbusTcpService
    {
        private IModbusMaster _modbusTcpMaster;
        private TcpClient _tcpClient;
        private bool _isConnected = false;
        private bool _isReadingContinuously = false;
        private CancellationTokenSource _cancellationTokenSource;

        public event Action<string> LogMessage;

        public bool IsConnected => _isConnected && _tcpClient != null && _tcpClient.Connected;

        public async Task<bool> ConnectAsync(string ipAddress, int port)
        {
            try
            {
                _tcpClient = new TcpClient();
                await _tcpClient.ConnectAsync(ipAddress, port);

                var factory = new ModbusFactory();
                _modbusTcpMaster = factory.CreateMaster(_tcpClient);
                _isConnected = true;

                return true;
            }
            catch (Exception ex)
            {
                OnLogMessage($"TCP连接异常: {ex.Message}");
                Disconnect();
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                _modbusTcpMaster?.Dispose();
                _tcpClient?.Close();
                _modbusTcpMaster = null;
                _tcpClient = null;
                _isConnected = false;
            }
            catch (Exception ex)
            {
                OnLogMessage($"TCP断开异常: {ex.Message}");
            }
        }

        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return _modbusTcpMaster.ReadCoils(slaveAddress, startAddress, numberOfPoints);
        }

        public bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return _modbusTcpMaster.ReadInputs(slaveAddress, startAddress, numberOfPoints);
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return _modbusTcpMaster.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
        }

        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return _modbusTcpMaster.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
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
                    if (!IsConnected)
                    {
                        OnLogMessage("TCP连接已断开，停止循环读取");
                        break;
                    }

                    readCount++;
                    OnLogMessage($"TCP第 {readCount} 次读取...");

                    ReadData(functionCode, slaveAddress, startAddress, numberOfPoints);
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
        }

        private void ReadData(string functionCode, byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            try
            {
                switch (functionCode)
                {
                    case "01 Read Coils":
                        bool[] coils = ReadCoils(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("TCP线圈", coils, startAddress);
                        break;

                    case "02 Read Inputs":
                        bool[] inputs = ReadInputs(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("TCP输入状态", inputs, startAddress);
                        break;

                    case "03 Read Holding Registers":
                        ushort[] holdingRegisters = ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("TCP保持寄存器", holdingRegisters, startAddress);
                        break;

                        case "04 Read Input Registers":
                        ushort[] inputRegisters = ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
                        DisplayReadResult("TCP输入寄存器", inputRegisters, startAddress);
                        break;
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"TCP读取数据异常: {ex.Message}");
                Disconnect();
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
            _modbusTcpMaster?.Dispose();
            _tcpClient?.Close();
            _cancellationTokenSource?.Cancel();
        }
    }
}