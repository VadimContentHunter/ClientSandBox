using System.Diagnostics;

namespace ClientSandBox.Services;

public static class WindowsStartupService
{
    private const string TaskName = "ClientSandBox";

    public static bool IsEnabled()
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = "schtasks.exe",
            Arguments = $"/Query /TN \"{TaskName}\"",
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using Process? process = Process.Start(startInfo);

        if (process is null)
        {
            return false;
        }

        process.WaitForExit();

        return process.ExitCode == 0;
    }

    public static (bool Success, string Output) Enable()
    {
        string exePath =
            Environment.ProcessPath
            ?? Process.GetCurrentProcess().MainModule?.FileName
            ?? throw new InvalidOperationException(
                "Не удалось определить путь к приложению.");

        string arguments =
            $"/Create " +
            $"/F " +
            $"/TN \"{TaskName}\" " +
            $"/SC ONLOGON " +
            $"/RL HIGHEST " +
            $"/TR \"\\\"{exePath}\\\" --autostart\"";

        return Execute(arguments);
    }

    public static (bool Success, string Output) Disable()
    {
        return Execute($"/Delete /F /TN \"{TaskName}\"");
    }

    private static (bool Success, string Output) Execute(string arguments)
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = "schtasks.exe",
            Arguments = arguments,
            UseShellExecute = true,
            Verb = "runas",
            CreateNoWindow = true
        };

        using Process? process = Process.Start(startInfo);

        if (process is null)
        {
            return (false, "Не удалось запустить schtasks.exe.");
        }

        process.WaitForExit();

        if (process.ExitCode == 0)
        {
            return (true, string.Empty);
        }

        return (false, $"schtasks.exe завершился с кодом {process.ExitCode}.");
    }
}