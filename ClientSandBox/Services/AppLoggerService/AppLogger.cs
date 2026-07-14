using System.Text;

namespace ClientSandBox.Services.AppLoggerService;

/// <summary>
/// Логгер приложения.
/// </summary>
public static class AppLogger
{
    private static readonly object Locker = new();

    public static string LogDirectory =>
        AppContext.BaseDirectory;

    public static string LogFilePath =>
        Path.Combine(LogDirectory, "application.log");

    /// <summary>
    /// Инициализация логгера.
    /// </summary>
    public static void Initialize()
    {
        try
        {
            if (!File.Exists(LogFilePath))
            {
                using FileStream stream = File.Create(LogFilePath);
            }

            Info("========================================");
            Info("Запуск приложения");
            Info("========================================");
        }
        catch
        {
            // Ничего не делаем.
        }
    }

    /// <summary>
    /// Записать информационное сообщение.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    public static void Info(string message)
    {
        Write(LogLevel.Info, message);
    }

    /// <summary>
    /// Записать предупреждение.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    public static void Warning(string message)
    {
        Write(LogLevel.Warning, message);
    }

    /// <summary>
    /// Записать ошибку.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    public static void Error(string message)
    {
        Write(LogLevel.Error, message);
    }

    /// <summary>
    /// Записать сообщение указанного уровня.
    /// </summary>
    /// <param name="level">Уровень сообщения.</param>
    /// <param name="message">Сообщение.</param>
    public static void Write(LogLevel level, string message)
    {
        try
        {
            lock (Locker)
            {
                File.AppendAllText(
                    LogFilePath,
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}{Environment.NewLine}",
                    Encoding.UTF8);
            }
        }
        catch
        {
            // Игнорируем ошибки логирования,
            // чтобы приложение не падало.
        }
    }

    /// <summary>
    /// Записать исключение.
    /// </summary>
    /// <param name="exception">Исключение.</param>
    public static void Exception(Exception exception)
    {
        StringBuilder builder = new();

        builder.AppendLine(exception.Message);
        builder.AppendLine(exception.StackTrace ?? string.Empty);

        if (exception.InnerException != null)
        {
            builder.AppendLine();
            builder.AppendLine("Внутреннее исключение:");
            builder.AppendLine(exception.InnerException.Message);
            builder.AppendLine(exception.InnerException.StackTrace ?? string.Empty);
        }

        Error(builder.ToString());
    }
}