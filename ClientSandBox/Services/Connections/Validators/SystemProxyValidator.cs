using System.Text.Json;
using ClientSandBox.Models;

namespace ClientSandBox.Services.Connections.Validators;

/// <summary>
/// Валидатор для проверок System Proxy-подключений.
/// Требует наличия свойств 'listen' и 'listen_port' (не пустые).
/// </summary>
public sealed class SystemProxyValidator : IConnectionValidator
{
    private readonly ConnectionJsonParser _parser = new();

    public ConnectionValidationResult Validate(
        ConnectionInfo connection,
        IReadOnlyList<ConnectionInfo> connections)
    {
        // Валидатор применим только к proxy-типам подключений.
        // Поддерживаемые типы: socks, mixed, http
        if (string.IsNullOrWhiteSpace(connection.Type))
        {
            return ConnectionValidationResult.Ok(ConnectionStatuses.Ready);
        }

        string type = connection.Type.Trim();
        string[] supported = new[] { "socks", "mixed", "http" };
        if (!supported.Any(t => string.Equals(t, type, StringComparison.OrdinalIgnoreCase)))
        {
            return ConnectionValidationResult.Ok(ConnectionStatuses.Ready);
        }

        if (string.IsNullOrWhiteSpace(connection.Json))
        {
            return ConnectionValidationResult.Ok(ConnectionStatuses.Ready);
        }

        if (!_parser.TryParse(connection.Json, out JsonDocument? document) || document is null)
        {
            return ConnectionValidationResult.Error(ConnectionStatuses.InvalidJson, "Некорректный JSON.");
        }

        // Проверяем наличие и непустоту listen
        string? listen = _parser.GetString(document, "listen");
        if (string.IsNullOrWhiteSpace(listen))
        {
            return ConnectionValidationResult.Error(
                ConnectionStatuses.MissingListen,
                "Подключение должно содержать непустое свойство 'listen'.");
        }

        // Проверяем наличие и непустоту listen_port
        string? port = _parser.GetString(document, "listen_port");
        if (string.IsNullOrWhiteSpace(port))
        {
            return ConnectionValidationResult.Error(
                ConnectionStatuses.MissingListenPort,
                "Подключение должно содержать непустое свойство 'listen_port'.");
        }

        return ConnectionValidationResult.Ok(ConnectionStatuses.Ready);
    }
}
