namespace ClientSandBox.Models;

/// <summary>
/// Результат проверки подключения.
/// </summary>
public sealed class ConnectionValidationResult
{
    /// <summary>
    /// Проверка успешно пройдена.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// Статус подключения.
    /// </summary>
    public string Status { get; init; } = string.Empty;

    /// <summary>
    /// Сообщение пользователю.
    /// </summary>
    public string Message { get; init; } = string.Empty;

    /// <summary>
    /// Успешный результат.
    /// </summary>
    public static ConnectionValidationResult Ok(string status)
    {
        return new()
        {
            Success = true,
            Status = status
        };
    }

    /// <summary>
    /// Ошибка проверки.
    /// </summary>
    public static ConnectionValidationResult Error(
        string status,
        string message)
    {
        return new()
        {
            Success = false,
            Status = status,
            Message = message
        };
    }
}