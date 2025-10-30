using Modbus.Device;
using System;
using System.IO.Ports;
using System.Threading;

namespace WinFormsAppCollect
{
    public class ModbusHelper : IDisposable
    {
        private SerialPort _serialPort;
        private IModbusMaster _master;
        private bool _isConnected = false;

        public bool IsConnected => _isConnected;

        public event Action<string> LogMessage;

        public ModbusHelper()
        {
            _serialPort = new SerialPort();
        }

        public bool Connect(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits)
        {
            try
            {
                if (_isConnected) return true;

                _serialPort.PortName = portName;
                _serialPort.BaudRate = baudRate;
                _serialPort.DataBits = dataBits;
                _serialPort.Parity = parity;
                _serialPort.StopBits = stopBits;
                _serialPort.ReadTimeout = 1000;
                _serialPort.WriteTimeout = 1000;

                _serialPort.Open();
                _master = ModbusSerialMaster.CreateRtu(_serialPort);
                _isConnected = true;

                LogMessage?.Invoke($"成功连接到端口 {portName}");
                return true;
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"连接失败: {ex.Message}");
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                if (_master != null)
                {
                    _master.Dispose();
                    _master = null;
                }

                if (_serialPort != null && _serialPort.IsOpen)
                {
                    _serialPort.Close();
                }

                _isConnected = false;
                LogMessage?.Invoke("已断开连接");
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"断开连接异常: {ex.Message}");
            }
        }

        // 发送特定的激活报文: 02 03 00 00 00 01 84 39
        public void SendActivationMessage()
        {
            try
            {
                if (!_isConnected) throw new InvalidOperationException("串口未连接");
                if (_serialPort == null || !_serialPort.IsOpen) throw new InvalidOperationException("串口未打开");

                // 您指定的激活报文: 02 03 00 00 00 01 84 39
                byte[] activationMessage = new byte[] { 0x02, 0x03, 0x00, 0x00, 0x00, 0x01, 0x84, 0x39 };

                // 清空接收缓冲区
                _serialPort.DiscardInBuffer();

                // 发送激活报文
                _serialPort.Write(activationMessage, 0, activationMessage.Length);
                LogMessage?.Invoke($"发送激活报文: 02 03 00 00 00 01 84 39");

                // 等待设备处理
                Thread.Sleep(300);

                // 清空可能存在的响应数据
                _serialPort.DiscardInBuffer();
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"发送激活报文失败: {ex.Message}");
                throw;
            }
        }

        // 带激活的读取保持寄存器方法
        public ushort[] ReadHoldingRegistersWithActivation(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            if (!_isConnected) throw new InvalidOperationException("串口未连接");

            try
            {
                // 先发送激活报文
                SendActivationMessage();
                LogMessage?.Invoke("激活报文发送完成，开始读取数据...");

                // 等待设备准备数据
                Thread.Sleep(300);

                // 然后读取数据
                return _master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"带激活读取保持寄存器失败: {ex.Message}");
                throw;
            }
        }

        // 带重试和激活的读取方法
        public ushort[] ReadHoldingRegistersWithRetry(byte slaveAddress, ushort startAddress, ushort numberOfPoints, int maxRetries = 3)
        {
            if (!_isConnected) throw new InvalidOperationException("串口未连接");

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    LogMessage?.Invoke($"第{attempt}次尝试读取保持寄存器...");

                    // 发送激活报文
                    SendActivationMessage();

                    // 等待设备响应
                    Thread.Sleep(300);

                    // 读取数据
                    var result = _master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                    LogMessage?.Invoke($"第{attempt}次读取成功");
                    return result;
                }
                catch (TimeoutException)
                {
                    LogMessage?.Invoke($"第{attempt}次读取超时");
                    if (attempt == maxRetries)
                        throw new TimeoutException($"读取保持寄存器失败，{maxRetries}次尝试均超时");

                    // 等待后重试
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    LogMessage?.Invoke($"第{attempt}次读取失败: {ex.Message}");
                    if (attempt == maxRetries)
                        throw;

                    Thread.Sleep(500);
                }
            }

            throw new Exception("读取保持寄存器失败");
        }

        // 原有的读取方法保持不变
        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            if (!_isConnected) throw new InvalidOperationException("串口未连接");

            try
            {
                return _master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"读取线圈失败: {ex.Message}");
                throw;
            }
        }

        public bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            if (!_isConnected) throw new InvalidOperationException("串口未连接");

            try
            {
                return _master.ReadInputs(slaveAddress, startAddress, numberOfPoints);
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"读取输入状态失败: {ex.Message}");
                throw;
            }
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            if (!_isConnected) throw new InvalidOperationException("串口未连接");

            try
            {
                return _master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"读取保持寄存器失败: {ex.Message}");
                throw;
            }
        }

        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            if (!_isConnected) throw new InvalidOperationException("串口未连接");

            try
            {
                return _master.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"读取输入寄存器失败: {ex.Message}");
                throw;
            }
        }

        public void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value)
        {
            if (!_isConnected) throw new InvalidOperationException("串口未连接");

            try
            {
                _master.WriteSingleCoil(slaveAddress, coilAddress, value);
                LogMessage?.Invoke($"成功写入线圈地址 {coilAddress} = {value}");
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"写入线圈失败: {ex.Message}");
                throw;
            }
        }

        public void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value)
        {
            if (!_isConnected) throw new InvalidOperationException("串口未连接");

            try
            {
                _master.WriteSingleRegister(slaveAddress, registerAddress, value);
                LogMessage?.Invoke($"成功写入寄存器地址 {registerAddress} = {value}");
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"写入寄存器失败: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            Disconnect();
            _serialPort?.Dispose();
        }
    }
}