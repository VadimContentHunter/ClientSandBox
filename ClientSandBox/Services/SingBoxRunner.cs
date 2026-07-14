using ClientSandBox.Services.AppLoggerService;
using System.Diagnostics;

namespace ClientSandBox.Services;

/// <summary>
/// Управление процессом sing-box.
/// </summary>
public static class SingBoxRunner
{
    /// <summary>
    /// Запущен ли sing-box.
    /// </summary>
    public static bool IsRunning => GetProcess() is not null;

    /// <summary>
    /// Идентификатор процесса.
    /// </summary>
    public static int? ProcessId => GetProcess()?.Id;

    private static Process? GetProcess()
    {
        int? pid = SettingsService.Current.SingBoxProcessId;
        DateTime? startUtc = SettingsService.Current.SingBoxStartTimeUtc;

        if (pid is null || startUtc is null)
            return null;

        try
        {
            Process process = Process.GetProcessById(pid.Value);

            if (process.HasExited)
            {
                ClearProcessInfo();
                return null;
            }

            if (process.StartTime.ToUniversalTime() != startUtc.Value)
            {
                ClearProcessInfo();
                process.Dispose();
                return null;
            }

            return process;
        }
        catch
        {
            ClearProcessInfo();
            return null;
        }
    }

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

            Process? process = Process.Start(info);

            if (process is null)
                return (false, "Не удалось запустить sing-box.");

            SettingsService.Current.SingBoxProcessId = process.Id;
            SettingsService.Current.SingBoxStartTimeUtc = process.StartTime.ToUniversalTime();
            SettingsService.Save();

            AppLogger.Info($"Sing-box запущен. PID={process.Id}");

            return (true, "Sing-box успешно запущен.");
        }
        catch (Exception ex)
        {
            AppLogger.Exception(ex);
            return (false, ex.Message);
        }
    }

    public static (bool Success, string Output) Stop()
    {
        try
        {
            Process? process = GetProcess();

            if (process is null)
                return (false, "Sing-box не запущен.");

            process.Kill(true);
            process.WaitForExit();
            process.Dispose();

            ClearProcessInfo();

            AppLogger.Info("Sing-box остановлен пользователем.");

            return (true, "Sing-box успешно остановлен.");
        }
        catch (Exception ex)
        {
            AppLogger.Exception(ex);
            return (false, ex.Message);
        }
    }

    public static (bool Success, string Output) Restart()
    {
        var stop = Stop();

        if (!stop.Success)
            return stop;

        return Start();
    }

    private static void ClearProcessInfo()
    {
        SettingsService.Current.SingBoxProcessId = null;
        SettingsService.Current.SingBoxStartTimeUtc = null;
        SettingsService.Save();
    }
}
