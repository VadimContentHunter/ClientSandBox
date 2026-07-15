using System.Text.Json;
using ClientSandBox.Models;

namespace ClientSandBox.Services.Connections.Validators;

/// <summary>
/// Валидатор для подключений типа TUN.
/// Проверяет, что в JSON есть свойство 'address' и это массив с минимум одним элементом.
/// </summary>
public sealed class TunValidator : IConnectionValidator
{
    private readonly ConnectionJsonParser _parser = new();

    public ConnectionValidationResult Validate(
        ConnectionInfo connection,
        IReadOnlyList<ConnectionInfo> connections)
    {
        if (string.IsNullOrWhiteSpace(connection.Type))
        {
            return ConnectionValidationResult.Ok(ConnectionStatuses.Ready);
        }

        if (!string.Equals(connection.Type, "tun", StringComparison.OrdinalIgnoreCase))
        {
            return ConnectionValidationResult.Ok(ConnectionStatuses.Ready);
        }

        // Проверяем JSON на наличие address
        if (!_parser.TryParse(connection.Json, out JsonDocument? document) || document is null)
        {
            return ConnectionValidationResult.Error(ConnectionStatuses.InvalidJson, "Некорректный JSON.");
        }

        JsonElement? address = _parser.GetProperty(document, "address");

        if (address is null || address.Value.ValueKind != JsonValueKind.Array || !address.Value.EnumerateArray().Any())
        {
            return ConnectionValidationResult.Error(
                ConnectionStatuses.MissingAddress,
                "Подключение типа TUN должно содержать свойство 'address' в виде массива с минимум одним элементом.");
        }

        return ConnectionValidationResult.Ok(ConnectionStatuses.Ready);
    }
}
