namespace ClientSandBox.Models;

/// <summary>
/// Состояние службы sing-box.
/// </summary>
public sealed class ServiceState
{
    /// <summary>
    /// Состояние службы.
    /// </summary>
    public ServiceStatus Status { get; init; }

    /// <summary>
    /// Установлена ли служба.
    /// </summary>
    public bool IsInstalled =>
        Status == ServiceStatus.Running
        || Status == ServiceStatus.Stopped;

    /// <summary>
    /// Запущена ли служба.
    /// </summary>
    public bool IsRunning =>
        Status == ServiceStatus.Running;

    /// <summary>
    /// Остановлена ли служба.
    /// </summary>
    public bool IsStopped =>
        Status == ServiceStatus.Stopped;

    /// <summary>
    /// Не удалось определить состояние.
    /// </summary>
    public bool IsUnknown =>
        Status == ServiceStatus.Unknown;

    public bool Success =>
        Status != ServiceStatus.Unknown;

    /// <summary>
    /// Последняя ошибка получения состояния.
    /// </summary>
    public string ErrorMessage { get; init; } = string.Empty;

    /// <summary>
    /// Текстовое представление состояния.
    /// </summary>
    public string DisplayName =>
        Status switch
        {
            ServiceStatus.NotInstalled => "Не установлена",
            ServiceStatus.Stopped => "Остановлена",
            ServiceStatus.Running => "Запущена",
            _ => "Неизвестно"
        };
}