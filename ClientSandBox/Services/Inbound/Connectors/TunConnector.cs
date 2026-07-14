using ClientSandBox.Models;

namespace ClientSandBox.Services.Inbound.Connectors;

public sealed class TunConnector : IInboundConnector
{
    private bool _isConnected;

    public bool IsConnected => _isConnected;

    public bool CanHandle(InboundInfo inbound)
    {
        return inbound.IsType("tun");
    }

    public (bool Success, string Output) Connect(InboundInfo inbound)
    {
        _isConnected = true;

        return (true, string.Empty);
    }

    public (bool Success, string Output) Disconnect()
    {
        _isConnected = false;

        return (true, string.Empty);
    }
}