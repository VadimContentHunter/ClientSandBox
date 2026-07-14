using Microsoft.Win32;

namespace ClientSandBox.Services.SystemProxy;

/// <summary>
/// Управление системным прокси Windows.
/// </summary>
public static class SystemProxyService
{
    private const string InternetSettings =
        @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";

    /// <summary>
    /// Включает системный прокси.
    /// </summary>
    public static (bool Success, string Output) Enable(string host, int port)
    {
        try
        {
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(InternetSettings, writable: true);

            if (key is null)
            {
                return (false, "Не удалось открыть настройки Internet Settings.");
            }

            key.SetValue("ProxyEnable", 1);
            key.SetValue("ProxyServer", $"{host}:{port}");

            Refresh();

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    /// <summary>
    /// Отключает системный прокси.
    /// </summary>
    public static (bool Success, string Output) Disable()
    {
        try
        {
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey( InternetSettings, writable: true);

            if (key is null)
            {
                return (false, "Не удалось открыть настройки Internet Settings.");
            }

            key.SetValue("ProxyEnable", 0);

            Refresh();

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    /// <summary>
    /// Проверяет, включен ли системный прокси.
    /// </summary>
    public static bool IsEnabled()
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(InternetSettings);

        if (key is null)
        {
            return false;
        }

        object? value = key.GetValue("ProxyEnable");
        return value is int enabled && enabled == 1;
    }

    /// <summary>
    /// Уведомляет систему об изменении настроек.
    /// </summary>
    private static void Refresh()
    {
        NativeMethods.InternetSetOption(
            IntPtr.Zero,
            InternetOptions.INTERNET_OPTION_SETTINGS_CHANGED,
            IntPtr.Zero,
            0);

        NativeMethods.InternetSetOption(
            IntPtr.Zero,
            InternetOptions.INTERNET_OPTION_REFRESH,
            IntPtr.Zero,
            0);
    }
}