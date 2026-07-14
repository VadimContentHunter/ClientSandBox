using ClientSandBox.Models;

namespace ClientSandBox.Services.Connections.Validators;

/// <summary>
/// Проверяет корректность пользовательского подключения.
/// </summary>
public interface IConnectionValidator
{
    /// <summary>
    /// Выполняет проверку подключения.
    /// </summary>
    /// <param name="connection">Проверяемое подключение.</param>
    /// <param name="connections">Список существующих подключений.</param>
    /// <returns>Результат проверки.</returns>
    ConnectionValidationResult Validate(
        ConnectionInfo connection,
        IReadOnlyList<ConnectionInfo> connections);
}