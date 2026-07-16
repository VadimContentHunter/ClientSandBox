namespace ClientSandBox.Services.SystemProxy;

/// <summary>
/// Команды WinAPI для функции InternetSetOption().
/// Используются для уведомления Windows
/// об изменении системных интернет-настроек.
/// </summary>
internal enum InternetOptions
{
    /// <summary>
    /// Значеник 39 - Сообщает Windows, что настройки Internet Settings были изменены.
    /// </summary>
    INTERNET_OPTION_SETTINGS_CHANGED = 39,

    /// <summary>
    /// Значеник 37 - Принудительно перечитывает и применяет новые настройки.
    /// </summary>
    INTERNET_OPTION_REFRESH = 37
}
