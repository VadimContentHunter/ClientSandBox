using System.Diagnostics;

namespace ClientSandBox.Services;

/// <summary>
/// Управление процессом sing-box.
/// </summary>
public static class SingBoxRunner
{
    private static Process? _process;

    /// <summary>
    /// Время запуска процесса.
    /// </summary>
    public static DateTime? StartTime { get; private set; }

    /// <summary>
    /// Запущен ли sing-box.
    /// </summary>
    public static bool IsRunning =>
        _process is { HasExited: false };

    /// <summary>
    /// Идентификатор процесса.
    /// </summary>
    public static int? ProcessId =>
        IsRunning ? _process!.Id : null;

    /// <summary>
    /// Запустить sing-box.
    /// </summary>
    public static (bool Success, string Output) Start()
    {
        try
        {
            if (IsRunning)
                return (false, "Sing-box уже запущен.");

            if (!File.Exists(SettingsService.Current.SingBoxPath))
                return (false, "Не найден sing-box.exe.");

            if (!File.Exists(SettingsService.Current.ConfigPath))
                return (false, "Не найден config.json.");

            string? workingDirectory =
                Path.GetDirectoryName(SettingsService.Current.SingBoxPath);

            if (string.IsNullOrWhiteSpace(workingDirectory))
                return (false, "Не удалось определить рабочую папку.");

            ProcessStartInfo info = new()
            {
                FileName = SettingsService.Current.SingBoxPath,
                Arguments = $"run -c \"{SettingsService.Current.ConfigPath}\"",
                WorkingDirectory = workingDirectory,

                UseShellExecute = false,
                CreateNoWindow = true
            };

            _process = Process.Start(info);
            if (_process is null)
                return (false, "Не удалось запустить sing-box.");
            _process.EnableRaisingEvents = true;
            _process.Exited += (_, _) =>
            {
                AppLogger.Info("Sing-box завершил работу.");
                CleanupProcess();
            };

            StartTime = DateTime.Now;
            AppLogger.Info($"Sing-box запущен. PID={_process.Id}");
            return (true, "Sing-box успешно запущен.");
        }
        catch (Exception ex)
        {
            AppLogger.Exception(ex);
            return (false, ex.Message);
        }
    }

    /// <summary>
    /// Остановить sing-box.
    /// </summary>
    public static (bool Success, string Output) Stop()
    {
        try
        {
            if (!IsRunning)
                return (false, "Sing-box не запущен.");

            _process!.Kill(true);
            _process.WaitForExit();

            AppLogger.Info("Sing-box остановлен пользователем.");
            CleanupProcess();
            return (true, "Sing-box успешно остановлен.");
        }
        catch (Exception ex)
        {
            AppLogger.Exception(ex);
            return (false, ex.Message);
        }
    }

    /// <summary>
    /// Перезапустить sing-box.
    /// </summary>
    public static (bool Success, string Output) Restart()
    {
        var stopResult = Stop();
        if (!stopResult.Success)
            return stopResult;

        return Start();
    }

    /// <summary>
    /// Освобождение ресурсов процесса.
    /// </summary>
    private static void CleanupProcess()
    {
        try
        {
            _process?.Dispose();
        }
        catch
        {
            // Игнорируем ошибки освобождения ресурсов.
        }

        _process = null;
        StartTime = null;
    }
}