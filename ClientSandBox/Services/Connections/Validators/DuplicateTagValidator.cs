using ClientSandBox.Models;

namespace ClientSandBox.Services.Connections.Validators;

/// <summary>
/// Проверяет, что тег подключения уникален.
/// </summary>
public sealed class DuplicateTagValidator : IConnectionValidator
{
    public ConnectionValidationResult Validate(
        ConnectionInfo connection,
        IReadOnlyList<ConnectionInfo> connections)
    {
        if (string.IsNullOrWhiteSpace(connection.Tag))
        {
            return ConnectionValidationResult.Ok(ConnectionStatuses.Ready);
        }

        bool duplicate = connections.Any(x =>
            !EqualityComparer<Guid>.Default.Equals(x.Id, connection.Id) &&
            string.Equals(x.Tag?.Trim(), connection.Tag?.Trim(), StringComparison.OrdinalIgnoreCase));

        if (duplicate)
        {
            return ConnectionValidationResult.Error(
                ConnectionStatuses.DuplicateTag,
                "Подключение с таким tag уже существует.");
        }

        return ConnectionValidationResult.Ok(ConnectionStatuses.Ready);
    }
}
