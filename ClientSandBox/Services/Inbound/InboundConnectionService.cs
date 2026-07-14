using ClientSandBox.Models;
using ClientSandBox.Services.Inbound.Connectors;

namespace ClientSandBox.Services.Inbound;

/// <summary>
/// Управляет подключением выбранного Inbound.
/// </summary>
public static class InboundConnectionService
{
    private static readonly IReadOnlyList<IInboundConnector> Connectors =
    [
        new SystemProxyConnector(),
        new TunConnector()
    ];

    private static IInboundConnector? _currentConnector;

    private static InboundInfo? _currentInbound;
    public static bool IsConnected => _currentConnector?.IsConnected ?? false;

    public static string CurrentTag => _currentInbound?.GetString("tag") ?? string.Empty;

    public static string CurrentType => _currentInbound?.GetString("type") ?? string.Empty;

    public static bool CanConnect => InboundService.GetSelected() is not null;



    /// <summary>
    /// Выполняет подключение выбранного Inbound.
    /// </summary>
    public static (bool Success, string Output) Connect()
    {
        InboundInfo? inbound = InboundService.GetSelected();

        if (inbound is null)
        {
            return (false, "Не выбран Inbound.");
        }

        IInboundConnector? connector = Connectors.FirstOrDefault(c => c.CanHandle(inbound));
        if (connector is null)
        {
            return (false, "Не найден Connector для выбранного Inbound.");
        }

        (bool Success, string Output) result = connector.Connect(inbound);
        if (result.Success)
        {
            _currentConnector = connector;
            _currentInbound = inbound;
        }

        return result;
    }

    /// <summary>
    /// Отключает текущее подключение.
    /// </summary>
    public static (bool Success, string Output) Disconnect()
    {
        if (_currentConnector is null)
        {
            return (true, string.Empty);
        }

        (bool Success, string Output) result = _currentConnector.Disconnect();
        if (result.Success)
        {
            _currentConnector = null;
            _currentInbound = null;
        }

        return result;
    }

    /// <summary>
    /// Переподключает выбранный Inbound.
    /// </summary>
    public static (bool Success, string Output) Reconnect()
    {
        if (IsConnected)
        {
            var disconnect = Disconnect();

            if (!disconnect.Success)
            {
                return disconnect;
            }
        }

        return Connect();
    }

    /// <summary>
    /// Возвращает текущий активный Inbound.
    /// </summary>
    public static InboundInfo? GetCurrentInbound()
    {
        return _currentInbound;
    }

    /// <summary>
    /// Возвращает информацию о текущем направлении трафика.
    /// </summary>
    /// <returns>Строка для отображения в интерфейсе.</returns>
    public static string GetConnectionInfo()
    {
        if (_currentConnector is null ||
            _currentInbound is null ||
            !_currentConnector.IsConnected)
        {
            return "Не перенаправляется";
        }

        string tag = _currentInbound.GetString("tag") ?? "—";
        string type = _currentInbound.GetString("type") ?? "—";

        return $"{tag} ({type})";
    }
}