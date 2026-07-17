using Microsoft.Win32;
using System.Diagnostics;

namespace ClientSandBox.Services;

public static class WindowsStartupService
{
    private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";

    private const string AppName = "ClientSandBox";

    public static bool IsEnabled()
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKey);
        string? value = key?.GetValue(AppName) as string;
        return !string.IsNullOrWhiteSpace(value);
    }

    public static void Enable()
    {
        string exePath = Environment.ProcessPath
            ?? Process.GetCurrentProcess().MainModule?.FileName
            ?? throw new InvalidOperationException("Не удалось определить путь к приложению.");

        string command = $"\"{exePath}\" --autostart";
        using RegistryKey key = Registry.CurrentUser.CreateSubKey(RunKey);
        key.SetValue(AppName, command);
    }

    public static void Disable()
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKey, writable: true);
        key?.DeleteValue(AppName, throwOnMissingValue: false);
    }
}