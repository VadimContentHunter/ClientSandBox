namespace ClientSandBox.Models;

public class Settings
{
    /// <summary>
    /// Путь к sing-box.exe
    /// </summary>
    public string SingBoxPath { get; set; } = string.Empty;

    /// <summary>
    /// Путь к config.json
    /// </summary>
    public string ConfigPath { get; set; } = string.Empty;

    /// <summary>
    /// Сворачивать в трей при закрытии.
    /// </summary>
    public bool CloseToTray { get; set; } = true;

    /// <summary>
    /// PID процесса sing-box.
    /// </summary>
    public int? SingBoxProcessId { get; set; }

    /// <summary>
    /// Время запуска процесса (UTC).
    /// </summary>
    public DateTime? SingBoxStartTimeUtc { get; set; }

    // ----------------------------
    // Параметры логирования
    // ----------------------------

    /// <summary>
    /// Включить логирование (в приложении — управление логированием).
    /// По умолчанию выключено.
    /// </summary>
    public bool EnableLogging { get; set; } = false;

    /// <summary>
    /// Уровень логов (например: trace, debug, info, warn, error).
    /// </summary>
    public string LogLevel { get; set; } = "info";

    /// <summary>
    /// Тип вывода логов: "console" | "file" | "custom".
    /// </summary>
    public string LogOutput { get; set; } = "console";

    /// <summary>
    /// Путь к директории/файлу логов, используется если LogOutput == "file" или "custom".
    /// Если пустой и LogOutput=="file", будет использована папка "logs" в корне проекта (создаётся автоматически).
    /// </summary>
    public string LogOutputPath { get; set; } = string.Empty;

    /// <summary>
    /// Должен ли лог содержать timestamp — всегда включён (нельзя отключить в UI).
    /// </summary>
    public bool Timestamp => true;

    /// <summary>
    /// Очищать логи при запуске sing-box (при нажатии кнопки "Запустить" или при фактическом запуске процесса).
    /// </summary>
    public bool ClearLogsOnStart { get; set; } = false;

    /// <summary>
    /// Автоочистка логов через указанное количество минут. 0 — отключено.
    /// При очистке сохраняются последние KeepLastLinesOnAutoClear строк.
    /// </summary>
    public int AutoClearMinutes { get; set; } = 0;

    /// <summary>
    /// Сколько последних строк сохранять при автоочистке (по умолчанию 100).
    /// </summary>
    public int KeepLastLinesOnAutoClear { get; set; } = 100;

    /// <summary>
    /// Сколько последних строк показывать пользователю из файла логов (по умолчанию 50).
    /// </summary>
    public int TailLinesToShow { get; set; } = 50;

    /// <summary>
    /// Флаг: блокировать изменение настроек вкладки ЛОГ во время работы sing-box.
    /// (Поведение UI — при true запрещает редактирование; сам флаг сохраняется в настройках.)
    /// </summary>
    public bool LockLogSettingsWhileRunning { get; set; } = true;

    /// <summary>
    /// Автоскролл просмотра логов в UI (если включено — при обновлении viewer прокручиваем в конец).
    /// </summary>
    public bool AutoScrollLogs { get; set; } = true;

    /// <summary>
    /// Запускать приложение вместе с Windows.
    /// </summary>
    public bool StartWithWindows { get; set; }
}