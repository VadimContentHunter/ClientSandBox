using System.Diagnostics;

namespace ClientSandBox.Services;

public static class SingBoxService
{
    private enum ValidationState
    {
        Unknown,
        Valid,
        Invalid
    }

    private static ValidationState _validationState = ValidationState.Unknown;
    private static string? _validatedPath;

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

            if (!arguments.Equals("version", StringComparison.OrdinalIgnoreCase))
            {
                var validation = ValidateSingBox();

                if (!validation.Success)
                    return validation;
            }

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
            _validationState = ValidationState.Unknown;
            AppLogger.Exception(ex);
            return (false, ex.Message);
        }
    }

    private static (bool Success, string Output) ValidateSingBox()
    {
        string currentPath = SettingsService.Current.SingBoxPath;

        // Если путь изменился, сбрасываем состояние проверки
        if (!string.Equals(_validatedPath, currentPath, StringComparison.OrdinalIgnoreCase))
        {
            _validatedPath = currentPath;
            _validationState = ValidationState.Unknown;
        }

        // Проверка уже выполнялась для данного пути
        switch (_validationState)
        {
            case ValidationState.Valid:
                return (true, string.Empty);

            case ValidationState.Invalid:
                return (false, "Указанный файл не является sing-box.");
        }

        try
        {
            ProcessStartInfo info = new()
            {
                FileName = currentPath,
                Arguments = "version",

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

            bool success = process.ExitCode == 0 &&
                           output.Contains("sing-box", StringComparison.OrdinalIgnoreCase);

            _validationState = success
                ? ValidationState.Valid
                : ValidationState.Invalid;

            if(_validationState == ValidationState.Valid)
                AppLogger.Info("Проверка исполняемого файла sing-box.");
            else
                AppLogger.Error("Проверка исполняемого файла sing-box не удалась.");

            return success
                ? (true, result.Trim())
                : (false, string.IsNullOrWhiteSpace(result)
                    ? "Указанный файл не является sing-box."
                    : result.Trim());
        }
        catch (Exception ex)
        {
            _validationState = ValidationState.Invalid;

            AppLogger.Exception(ex);

            return (false, ex.Message);
        }
    }
}