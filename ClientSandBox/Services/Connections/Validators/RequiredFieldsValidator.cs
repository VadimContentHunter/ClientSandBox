using ClientSandBox.Models;

namespace ClientSandBox.Services.Connections.Validators;

/// <summary>
/// Проверяет обязательные свойства подключения.
/// </summary>
public sealed class RequiredFieldsValidator : IConnectionValidator
{
    /// <inheritdoc/>
    public ConnectionValidationResult Validate(
        ConnectionInfo connection,
        IReadOnlyList<ConnectionInfo> connections)
    {
        if (string.IsNullOrWhiteSpace(connection.Tag))
        {
            return ConnectionValidationResult.Error(
                ConnectionStatuses.MissingTag,
                "Подключение должно содержать свойство 'tag'.");
        }

        if (string.IsNullOrWhiteSpace(connection.Type))
        {
            return ConnectionValidationResult.Error(
                ConnectionStatuses.MissingType,
                "Подключение должно содержать свойство 'type'.");
        }

        return ConnectionValidationResult.Ok(
            ConnectionStatuses.Ready);
    }
}