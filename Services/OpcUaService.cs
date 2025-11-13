using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace WinFormsAppCollect.Services
{
    public class OpcUaService
    {
        private bool _isConnected = false;
        private bool _isReadingContinuously = false;
        private CancellationTokenSource _cancellationTokenSource;

        public event Action<string> LogMessage;

        public bool IsConnected => _isConnected;

        public async Task<bool> ConnectAsync(string serverUrl)
        {
            try
            {
                if (!IsValidOpcServerUrl(serverUrl))
                {
                    OnLogMessage("无效的OPC UA服务器URL格式");
                    return false;
                }

                // 测试服务器连接
                bool connectionTest = await TestServerConnection(serverUrl);
                if (!connectionTest)
                {
                    OnLogMessage("无法连接到OPC UA服务器");
                    return false;
                }

                _isConnected = true;
                OnLogMessage($"OPC UA连接成功: {serverUrl}");
                return true;
            }
            catch (Exception ex)
            {
                OnLogMessage($"OPC UA连接失败: {ex.Message}");
                _isConnected = false;
                return false;
            }
        }

        public void Disconnect()
        {
            _isConnected = false;
            _isReadingContinuously = false;
            _cancellationTokenSource?.Cancel();
        }

        public (double value, DateTime timestamp, string quality) ReadNodeValue(string nodeId)
        {
            if (!_isConnected)
                throw new InvalidOperationException("OPC UA连接已断开");

            if (!IsValidNodeId(nodeId))
                throw new ArgumentException("无效的节点ID格式");

            // 模拟读取数据
            Random rand = new Random();
            double value = rand.NextDouble() * 100;
            DateTime timestamp = DateTime.Now;

            return (value, timestamp, "Good");
        }

        public void StartContinuousReading(int interval, string nodeId)
        {
            _isReadingContinuously = true;
            _cancellationTokenSource = new CancellationTokenSource();

            Task.Run(() => ContinuousReadLoop(interval, nodeId, _cancellationTokenSource.Token));
        }

        public void StopContinuousReading()
        {
            _isReadingContinuously = false;
            _cancellationTokenSource?.Cancel();
        }

        private async Task ContinuousReadLoop(int interval, string nodeId, CancellationToken cancellationToken)
        {
            int readCount = 0;
            Random rand = new Random();

            while (!cancellationToken.IsCancellationRequested && _isReadingContinuously)
            {
                try
                {
                    if (!_isConnected)
                    {
                        OnLogMessage("OPC UA连接已断开，停止循环读取");
                        break;
                    }

                    readCount++;
                    OnLogMessage($"OPC UA第 {readCount} 次读取...");

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
        }

        private bool IsValidOpcServerUrl(string url)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                    return false;

                if (!url.StartsWith("opc.tcp://", StringComparison.OrdinalIgnoreCase))
                    return false;

                string[] parts = url.Split(new[] { "opc.tcp://" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 1)
                    return false;

                string hostPort = parts[0];
                string[] hostPortParts = hostPort.Split(':');
                if (hostPortParts.Length != 2)
                    return false;

                if (!int.TryParse(hostPortParts[1], out int port) || port < 1 || port > 65535)
                    return false;

                string host = hostPortParts[0];
                return !string.IsNullOrWhiteSpace(host);
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidNodeId(string nodeId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nodeId))
                    return false;

                return nodeId.Contains("ns=") || nodeId.Contains("s=") || nodeId.Contains("i=");
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> TestServerConnection(string serverUrl)
        {
            try
            {
                string[] parts = serverUrl.Split(new[] { "opc.tcp://" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 1)
                    return false;

                string hostPort = parts[0];
                string[] hostPortParts = hostPort.Split(':');
                if (hostPortParts.Length != 2)
                    return false;

                string host = hostPortParts[0];
                if (!int.TryParse(hostPortParts[1], out int port))
                    return false;

                using (var tcpClient = new TcpClient())
                {
                    var timeoutTask = Task.Delay(3000);
                    var connectTask = tcpClient.ConnectAsync(host, port);

                    var completedTask = await Task.WhenAny(connectTask, timeoutTask);

                    if (completedTask == timeoutTask)
                    {
                        OnLogMessage("连接超时：服务器未响应");
                        return false;
                    }

                    return tcpClient.Connected;
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"连接测试异常: {ex.Message}");
                return false;
            }
        }

        private void OnLogMessage(string message)
        {
            LogMessage?.Invoke(message);
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}