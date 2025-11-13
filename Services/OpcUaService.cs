using Opc.Ua;
using Opc.Ua.Client;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace WinFormsAppCollect.Services
{
    public class OpcUaService : IDisposable
    {
        private bool _isConnected = false;
        private bool _isReadingContinuously = false;
        private CancellationTokenSource _cancellationTokenSource;
        private ApplicationConfiguration _configuration;
        private Session _session;
        private string _serverUrl;

        public event Action<string> LogMessage;

        public bool IsConnected => _isConnected && _session != null && _session.Connected;

        public OpcUaService()
        {
            InitializeConfiguration();
        }

        private void InitializeConfiguration()
        {
            try
            {
                _configuration = new ApplicationConfiguration()
                {
                    ApplicationName = "ModbusDataCollector",
                    ApplicationType = ApplicationType.Client,
                    ApplicationUri = Utils.Format(@"urn:localhost:ModbusDataCollector:{0}", System.Net.Dns.GetHostName()),
                    SecurityConfiguration = new SecurityConfiguration
                    {
                        ApplicationCertificate = new CertificateIdentifier
                        {
                            StoreType = @"Directory",
                            StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\MachineDefault"
                        },
                        TrustedPeerCertificates = new CertificateTrustList
                        {
                            StoreType = @"Directory",
                            StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\UA Certificate Authorities"
                        },
                        TrustedIssuerCertificates = new CertificateTrustList
                        {
                            StoreType = @"Directory",
                            StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\UA Certificate Authorities"
                        },
                        RejectedCertificateStore = new CertificateTrustList
                        {
                            StoreType = @"Directory",
                            StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\RejectedCertificates"
                        },
                        AutoAcceptUntrustedCertificates = true
                    },
                    TransportConfigurations = new TransportConfigurationCollection(),
                    TransportQuotas = new TransportQuotas { OperationTimeout = 15000 },
                    ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 },
                    TraceConfiguration = new TraceConfiguration()
                };

                _configuration.Validate(ApplicationType.Client).Wait();
            }
            catch (Exception ex)
            {
                OnLogMessage($"OPC UA配置初始化失败: {ex.Message}");
            }
        }

        public async Task<bool> ConnectAsync(string serverUrl)
        {
            try
            {
                if (!IsValidOpcServerUrl(serverUrl))
                {
                    OnLogMessage("无效的OPC UA服务器URL格式");
                    return false;
                }

                OnLogMessage($"正在连接OPC UA服务器: {serverUrl}");

                // 测试服务器连接
                bool connectionTest = await TestServerConnection(serverUrl);
                if (!connectionTest)
                {
                    OnLogMessage("无法连接到OPC UA服务器");
                    return false;
                }

                // 使用DiscoveryClient获取端点信息
                EndpointDescription endpointDescription = null;
                try
                {
                    using (var discoveryClient = DiscoveryClient.Create(new Uri(serverUrl)))
                    {
                        var endpoints = await discoveryClient.GetEndpointsAsync(null);
                        if (endpoints != null && endpoints.Count > 0)
                        {
                            // 优先选择匿名认证的端点
                            foreach (var endpoint1 in endpoints)
                            {
                                if (endpoint1.UserIdentityTokens.Any(t => t.TokenType == UserTokenType.Anonymous))
                                {
                                    endpointDescription = endpoint1;
                                    break;
                                }
                            }

                            // 如果没有匿名端点，使用第一个端点
                            if (endpointDescription == null && endpoints.Count > 0)
                            {
                                endpointDescription = endpoints[0];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnLogMessage($"发现端点失败: {ex.Message}");
                    // 手动创建端点作为备选
                    endpointDescription = new EndpointDescription
                    {
                        EndpointUrl = serverUrl,
                        SecurityMode = MessageSecurityMode.None,
                        SecurityPolicyUri = SecurityPolicies.None,
                        Server = new ApplicationDescription
                        {
                            ApplicationName = "OPC UA Server",
                            ApplicationType = ApplicationType.Server,
                            ApplicationUri = $"urn:{new Uri(serverUrl).Host}:OPC:Server"
                        }
                    };
                }

                var endpointConfiguration = EndpointConfiguration.Create(_configuration);
                var endpoint = new ConfiguredEndpoint(null, endpointDescription, endpointConfiguration);

                // 使用匿名用户身份
                var userIdentity = new UserIdentity(new AnonymousIdentityToken());

                // 创建会话
                var session = await Session.Create(
                    _configuration,
                    endpoint,
                    false,
                    false,
                    _configuration.ApplicationName,
                    60000,
                    userIdentity,  // 使用匿名身份
                    null);

                _session = session;
                _serverUrl = serverUrl;
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
            try
            {
                _isConnected = false;
                _isReadingContinuously = false;
                _cancellationTokenSource?.Cancel();

                if (_session != null)
                {
                    _session.Close();
                    _session.Dispose();
                    _session = null;
                }

                OnLogMessage("OPC UA连接已断开");
            }
            catch (Exception ex)
            {
                OnLogMessage($"OPC UA断开异常: {ex.Message}");
            }
        }

        public (double value, DateTime timestamp, string quality) ReadNodeValue(string nodeId)
        {
            if (!IsConnected)
                throw new InvalidOperationException("OPC UA连接已断开");

            if (!IsValidNodeId(nodeId))
                throw new ArgumentException("无效的节点ID格式");

            try
            {
                // 创建节点ID
                NodeId nodeToRead = new NodeId(nodeId);

                // 创建读取值集合
                ReadValueIdCollection nodesToRead = new ReadValueIdCollection
                {
                    new ReadValueId
                    {
                        NodeId = nodeToRead,
                        AttributeId = Attributes.Value
                    }
                };

                // 读取数据
                DataValueCollection results;
                DiagnosticInfoCollection diagnosticInfos;

                _session.Read(
                    null,
                    0,
                    TimestampsToReturn.Both,
                    nodesToRead,
                    out results,
                    out diagnosticInfos);

                // 检查结果
                if (results != null && results.Count > 0 && results[0] != null)
                {
                    var dataValue = results[0];

                    // 转换值为double
                    double value = 0;
                    if (dataValue.Value != null)
                    {
                        try
                        {
                            value = Convert.ToDouble(dataValue.Value);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"数据类型转换失败: {ex.Message}");
                        }
                    }

                    DateTime timestamp = dataValue.SourceTimestamp;
                    string quality = GetStatusDescription(dataValue.StatusCode);

                    return (value, timestamp, quality);
                }
                else
                {
                    throw new Exception("未能读取到节点数据");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"读取节点数据失败: {ex.Message}", ex);
            }
        }

        public void StartContinuousReading(int interval, string nodeId)
        {
            if (!IsConnected)
                throw new InvalidOperationException("OPC UA连接已断开");

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

            while (!cancellationToken.IsCancellationRequested && _isReadingContinuously)
            {
                try
                {
                    if (!IsConnected)
                    {
                        OnLogMessage("OPC UA连接已断开，停止循环读取");
                        break;
                    }

                    readCount++;

                    var result = ReadNodeValue(nodeId);

                    OnLogMessage($"OPC UA节点 {nodeId} 读取成功,值: {result.value:F2},时间: {result.timestamp:yyyy-MM-dd HH:mm:ss}");
                    
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

        public (string appName, string appUri, string productUri) GetServerInfo()
        {
            if (!IsConnected)
                throw new InvalidOperationException("OPC UA连接已断开");

            try
            {
                // 读取服务器状态节点来获取信息
                var serverStatusNode = new NodeId("i=2256"); // ServerStatus节点
                var serverNameNode = new NodeId("i=2267");   // ProductName节点
                var serverUriNode = new NodeId("i=2268");    // ServerUri节点

                // 读取服务器名称
                string appName = ReadStringValue(serverNameNode);

                // 读取服务器URI
                string appUri = ReadStringValue(serverUriNode);

                // 读取产品名称（如果没有单独的ProductName，使用ServerName）
                string productUri = appName; // 或者从其他节点读取

                return (appName, appUri, productUri);
            }
            catch (Exception ex)
            {
                throw new Exception($"获取服务器信息失败: {ex.Message}", ex);
            }
        }

        private string ReadStringValue(NodeId nodeId)
        {
            try
            {
                ReadValueIdCollection nodesToRead = new ReadValueIdCollection
                {
                    new ReadValueId
                    {
                        NodeId = nodeId,
                        AttributeId = Attributes.Value
                    }
                };

                DataValueCollection results;
                DiagnosticInfoCollection diagnosticInfos;

                _session.Read(
                    null,
                    0,
                    TimestampsToReturn.Both,
                    nodesToRead,
                    out results,
                    out diagnosticInfos);

                if (results != null && results.Count > 0 && results[0] != null && results[0].Value != null)
                {
                    return results[0].Value.ToString();
                }

                return "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }


        private string GetStatusDescription(StatusCode statusCode)
        {
            if (StatusCode.IsGood(statusCode))
                return "Good";
            else if (StatusCode.IsUncertain(statusCode))
                return "Uncertain";
            else
                return "Bad";
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

                // 尝试创建NodeId来验证格式
                var testNodeId = new NodeId(nodeId);
                return true;
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
            try
            {
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource?.Dispose();

                if (_session != null)
                {
                    if (_session.Connected)
                    {
                        _session.Close();
                    }
                    _session.Dispose();
                }
            }
            catch (Exception ex)
            {
                OnLogMessage($"OPC UA服务释放异常: {ex.Message}");
            }
        }
    }
}