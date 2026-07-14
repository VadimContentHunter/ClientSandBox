using ClientSandBox.Models;
using ClientSandBox.Services.SystemProxy;

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
        string? host = inbound.GetString("listen");
        int? port = inbound.GetInt("listen_port");

        if (string.IsNullOrWhiteSpace(host) || port is null)
        {
            return (false, "Недостаточно данных.");
        }

        return SystemProxyService.Enable(host, port.Value);
    }

    public (bool Success, string Output) Disconnect()
    {
        return SystemProxyService.Disable();
    }
}