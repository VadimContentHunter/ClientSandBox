using ClientSandBox.Services.AppLoggerService;
using System.Diagnostics;
using System.Text;

namespace ClientSandBox.Services.SingBox;

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

            using Process process = Process.Start(info)!;

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

        if (!string.Equals(_validatedPath, currentPath, StringComparison.OrdinalIgnoreCase))
        {
            _validatedPath = currentPath;
            _validationState = ValidationState.Unknown;
        }

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

            using Process process = Process.Start(info)!;

            string output = process.StandardOutput.ReadToEnd();

            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            string result = string.IsNullOrWhiteSpace(error)
                ? output
                : error;

            bool success =
                process.ExitCode == 0 &&
                output.Contains("sing-box", StringComparison.OrdinalIgnoreCase);

            _validationState =
                success
                    ? ValidationState.Valid
                    : ValidationState.Invalid;

            return success
                ? (true, result.Trim())
                : (false,
                    string.IsNullOrWhiteSpace(result)
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