namespace ClientSandBox.Models;

/// <summary>
/// Состояние службы sing-box.
/// </summary>
public enum ServiceStatus
{
    /// <summary>
    /// Не удалось определить состояние.
    /// </summary>
    Unknown,

    /// <summary>
    /// Служба не установлена.
    /// </summary>
    NotInstalled,

    /// <summary>
    /// Служба остановлена.
    /// </summary>
    Stopped,

    /// <summary>
    /// Служба запущена.
    /// </summary>
    Running
}