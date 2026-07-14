using ClientSandBox.Models;

namespace ClientSandBox.Services.Inbound.Connectors;

/// <summary>
/// Выполняет подключение через системный прокси.
/// </summary>
public sealed class SystemProxyConnector : IInboundConnector
{
    private bool _isConnected;

    public bool IsConnected => _isConnected;

    public bool CanHandle(InboundInfo inbound)
    {
        return inbound.IsType("mixed", "http", "socks");
    }

    public (bool Success, string Output) Connect(InboundInfo inbound)
    {
        _isConnected = true;

        return (true, "SystemProxyConnector пока не реализован.");
    }

    public (bool Success, string Output) Disconnect()
    {
        _isConnected = false;

        return (true, string.Empty);
    }
}