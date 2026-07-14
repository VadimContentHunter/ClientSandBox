using ClientSandBox.Models;

namespace ClientSandBox.Services.Inbound;

/// <summary>
/// Управляет подключением выбранного Inbound.
/// </summary>
public static class InboundConnectionService
{
    /// <summary>
    /// Выполняет подключение выбранного Inbound.
    /// </summary>
    public static (bool Success, string Output) Connect()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Отключает текущее подключение.
    /// </summary>
    public static (bool Success, string Output) Disconnect()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Переподключает выбранный Inbound.
    /// </summary>
    public static (bool Success, string Output) Reconnect()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Возвращает текущий активный Inbound.
    /// </summary>
    public static InboundInfo? GetCurrentInbound()
    {
        throw new NotImplementedException();
    }
}