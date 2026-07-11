using System.Text;

namespace ClientSandBox.Services;

public static class AppLogger
{
    private static readonly object Locker = new();

    private static string LogFilePath =>
        Path.Combine(AppContext.BaseDirectory, "application.log");

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
    public static void Info(string message)
    {
        Write("INFO", message);
    }

    /// <summary>
    /// Записать предупреждение.
    /// </summary>
    public static void Warning(string message)
    {
        Write("WARNING", message);
    }

    /// <summary>
    /// Записать ошибку.
    /// </summary>
    public static void Error(string message)
    {
        Write("ERROR", message);
    }

    /// <summary>
    /// Записать исключение.
    /// </summary>
    public static void Exception(Exception ex)
    {
        StringBuilder builder = new();

        builder.AppendLine(ex.Message);
        builder.AppendLine(ex.StackTrace);

        if (ex.InnerException != null)
        {
            builder.AppendLine();
            builder.AppendLine("Внутреннее исключение:");
            builder.AppendLine(ex.InnerException.Message);
            builder.AppendLine(ex.InnerException.StackTrace);
        }

        Write("EXCEPTION", builder.ToString());
    }

    private static void Write(string level, string message)
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
}