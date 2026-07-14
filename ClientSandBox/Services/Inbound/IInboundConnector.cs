using ClientSandBox.Models;

namespace ClientSandBox.Services.Inbound;

/// <summary>
/// Выполняет подключение через определенный тип Inbound.
/// </summary>
public interface IInboundConnector
{
    /// <summary>
    /// Может ли обработать данный Inbound.
    /// </summary>
    /// <param name="inbound">Inbound.</param>
    /// <returns>true, если данный Connector поддерживает Inbound.</returns>
    bool CanHandle(InboundInfo inbound);

    /// <summary>
    /// Выполняет подключение.
    /// </summary>
    /// <param name="inbound">Inbound.</param>
    /// <returns>Результат операции.</returns>
    (bool Success, string Output) Connect(InboundInfo inbound);

    /// <summary>
    /// Отключает подключение.
    /// </summary>
    /// <returns>Результат операции.</returns>
    (bool Success, string Output) Disconnect();

    /// <summary>
    /// Выполнено ли подключение данным Connector.
    /// </summary>
    bool IsConnected { get; }
}