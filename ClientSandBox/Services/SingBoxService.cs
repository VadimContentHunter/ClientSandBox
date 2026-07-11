using System.Diagnostics;

namespace ClientSandBox.Services;

public static class SingBoxService
{
    /// <summary>
    /// Получить версию sing-box.
    /// </summary>
    public static (bool Success, string Output) GetVersion()
    {
        return Execute("version");
    }

    /// <summary>
    /// Проверить конфигурацию.
    /// </summary>
    public static (bool Success, string Output) CheckConfig()
    {
        if (!File.Exists(SettingsService.Current.ConfigPath))
            return (false, "Файл config.json не найден.");

        return Execute($"check -c \"{SettingsService.Current.ConfigPath}\"");
    }

    /// <summary>
    /// Установить службу.
    /// </summary>
    public static (bool Success, string Output) InstallService()
    {
        return Execute("service install");
    }

    /// <summary>
    /// Удалить службу.
    /// </summary>
    public static (bool Success, string Output) UninstallService()
    {
        return Execute("service uninstall");
    }

    /// <summary>
    /// Запустить службу.
    /// </summary>
    public static (bool Success, string Output) StartService()
    {
        var check = CheckConfig();

        if (!check.Success)
            return check;

        return Execute("service start");
    }

    /// <summary>
    /// Остановить службу.
    /// </summary>
    public static (bool Success, string Output) StopService()
    {
        return Execute("service stop");
    }

    /// <summary>
    /// Перезапустить службу.
    /// </summary>
    public static (bool Success, string Output) RestartService()
    {
        var stop = StopService();

        if (!stop.Success)
            return stop;

        return StartService();
    }

    private static (bool Success, string Output) Execute(string arguments)
    {
        try
        {
            if (!File.Exists(SettingsService.Current.SingBoxPath))
                return (false, "Не найден sing-box.exe.");

            ProcessStartInfo info = new()
            {
                FileName = SettingsService.Current.SingBoxPath,
                Arguments = arguments,

                RedirectStandardOutput = true,
                RedirectStandardError = true,

                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = new();

            process.StartInfo = info;

            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            string result = string.IsNullOrWhiteSpace(error)
                ? output
                : error;

            bool success = process.ExitCode == 0;

            AppLogger.Info(
                $"{Path.GetFileName(SettingsService.Current.SingBoxPath)} {arguments}");

            if (!success)
                AppLogger.Error(result);

            return (success, result.Trim());
        }
        catch (Exception ex)
        {
            AppLogger.Exception(ex);

            return (false, ex.Message);
        }
    }
}