namespace ClientSandBox.Models;

/// <summary>
/// Пользовательское подключение.
/// </summary>
public sealed class ConnectionInfo
{
    /// <summary>
    /// Уникальный идентификатор подключения.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Используется ли подключение.
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Тег подключения.
    /// </summary>
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Тип подключения.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// JSON подключения.
    /// </summary>
    public string Json { get; set; } = string.Empty;

    /// <summary>
    /// Результат проверки подключения.
    /// </summary>
    public string Status { get; set; } = string.Empty;
}