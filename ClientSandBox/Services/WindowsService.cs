using System.Diagnostics;
using System.ServiceProcess;
using ClientSandBox.Models;

namespace ClientSandBox.Services;

public static class WindowsService
{
    public const string ServiceName = "sing-box";

    /// <summary>
    /// Получить состояние службы.
    /// </summary>
    public static ServiceState GetServiceState()
    {
        try
        {
            using ServiceController controller = new(ServiceName);

            return controller.Status switch
            {
                ServiceControllerStatus.Running => new ServiceState
                {
                    Status = ServiceStatus.Running
                },

                ServiceControllerStatus.Stopped => new ServiceState
                {
                    Status = ServiceStatus.Stopped
                },

                ServiceControllerStatus.StartPending => new ServiceState
                {
                    Status = ServiceStatus.Running
                },

                ServiceControllerStatus.StopPending => new ServiceState
                {
                    Status = ServiceStatus.Stopped
                },

                _ => new ServiceState
                {
                    Status = ServiceStatus.Unknown
                }
            };
        }
        catch (InvalidOperationException)
        {
            return new ServiceState
            {
                Status = ServiceStatus.NotInstalled
            };
        }
        catch (Exception ex)
        {
            AppLogger.Exception(ex);

            return new ServiceState
            {
                Status = ServiceStatus.Unknown,
                ErrorMessage = ex.Message
            };
        }
    }

    /// <summary>
    /// Установить службу.
    /// </summary>
    public static (bool Success, string Output) Install()
    {
        var state = GetServiceState();

        if (state.IsInstalled)
            return (false, "Служба уже установлена.");

        if (!File.Exists(SettingsService.Current.SingBoxPath))
            return (false, "Не найден sing-box.exe.");

        if (!File.Exists(SettingsService.Current.ConfigPath))
            return (false, "Не найден config.json.");

        string binPath =
            $"\"{SettingsService.Current.SingBoxPath}\" " +
            $"run -c \"{SettingsService.Current.ConfigPath}\"";

        return Execute(
            $"create {ServiceName} " +
            $"binPath= \"{binPath}\" " +
            $"start= auto " +
            $"DisplayName= \"Sing-Box\"");
    }

    /// <summary>
    /// Удалить службу.
    /// </summary>
    public static (bool Success, string Output) Uninstall()
    {
        var state = GetServiceState();

        if (!state.IsInstalled)
            return (false, "Служба не установлена.");

        return Execute($"delete {ServiceName}");
    }

    /// <summary>
    /// Запустить службу.
    /// </summary>
    public static (bool Success, string Output) Start()
    {
        var state = GetServiceState();

        if (!state.IsInstalled)
            return (false, "Служба не установлена.");

        if (state.IsRunning)
            return (false, "Служба уже запущена.");

        return Execute($"start {ServiceName}");
    }

    /// <summary>
    /// Остановить службу.
    /// </summary>
    public static (bool Success, string Output) Stop()
    {
        var state = GetServiceState();

        if (!state.IsInstalled)
            return (false, "Служба не установлена.");

        if (state.IsStopped)
            return (false, "Служба уже остановлена.");

        return Execute($"stop {ServiceName}");
    }

    /// <summary>
    /// Перезапустить службу.
    /// </summary>
    public static (bool Success, string Output) Restart()
    {
        var stop = Stop();

        if (!stop.Success)
            return stop;

        Thread.Sleep(1000);

        return Start();
    }

    private static (bool Success, string Output) Execute(string arguments)
    {
        try
        {
            ProcessStartInfo info = new()
            {
                FileName = "sc.exe",
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

            if (success)
                AppLogger.Info($"sc.exe {arguments}");
            else
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