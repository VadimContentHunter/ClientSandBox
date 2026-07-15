using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using ClientSandBox.Models;
using ClientSandBox.Services.Connections;

namespace ClientSandBox.Services.SystemProxy;

/// <summary>
/// Управление системным прокси (WinINet / CurrentUser).
/// Сохраняет текущие настройки и умеет применять/восстанавливать прокси.
/// Работает на уровне текущего пользователя (HKCU) и вызывает InternetSetOption для применения.
/// </summary>
public static class SystemProxyService
{
    private const string InternetSettingsKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";

    private static int? _oldProxyEnable;
    private static string? _oldProxyServer;
    private static string? _oldProxyOverride;
    private static bool _applied = false;

    private const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
    private const int INTERNET_OPTION_REFRESH = 37;

    [DllImport("wininet.dll", SetLastError = true)]
    private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

    /// <summary>
    /// Применяет системный proxy на основании первого подходящего connection (type == http|mixed).
    /// Возвращает (success, message).
    /// </summary>
    public static (bool Success, string Message) ApplyProxyForSelected(IEnumerable<ConnectionInfo> selected)
    {
        if (selected is null)
            return (false, "No selected connections.");

        var parser = new ConnectionJsonParser();

        foreach (ConnectionInfo conn in selected)
        {
            if (string.IsNullOrWhiteSpace(conn.Type))
                continue;

            string type = conn.Type.Trim();
            if (!string.Equals(type, "http", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(type, "mixed", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(conn.Json))
                continue;

            try
            {
                if (!parser.TryParse(conn.Json, out var doc) || doc is null)
                    continue;

                string? listen = parser.GetString(doc, "listen");
                string? port = parser.GetString(doc, "listen_port");

                if (string.IsNullOrWhiteSpace(listen) || string.IsNullOrWhiteSpace(port))
                    continue;

                string proxy = $"{listen}:{port}";

                return ApplyProxy(proxy);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        return (false, "No applicable http/mixed proxy found among selected connections.");
    }

    /// <summary>
    /// Применяет proxy-server (формат host:port или "http=host:port;https=host:port").
    /// Сохраняет старые значения (если ещё не сохранены) и помечает как применённое.
    /// </summary>
    public static (bool Success, string Message) ApplyProxy(string proxyServer)
    {
        try
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(InternetSettingsKey, writable: true)!;
            if (key is null)
                return (false, "Не удалось открыть реестр для Internet Settings.");

            if (!_applied)
            {
                // save old
                object? pe = key.GetValue("ProxyEnable");
                _oldProxyEnable = pe is int i ? i : (int?)null;
                _oldProxyServer = key.GetValue("ProxyServer") as string;
                _oldProxyOverride = key.GetValue("ProxyOverride") as string;
            }

            key.SetValue("ProxyEnable", 1, RegistryValueKind.DWord);
            key.SetValue("ProxyServer", proxyServer, RegistryValueKind.String);

            // leave ProxyOverride untouched by default

            // notify system
            RefreshInternetOptions();

            _applied = true;

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    /// <summary>
    /// Восстанавливает сохранённые настройки proxy, если применялись.
    /// </summary>
    public static (bool Success, string Message) Restore()
    {
        if (!_applied)
            return (true, string.Empty);

        try
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(InternetSettingsKey, writable: true)!;
            if (key is null)
                return (false, "Не удалось открыть реестр для Internet Settings.");

            if (_oldProxyEnable.HasValue)
                key.SetValue("ProxyEnable", _oldProxyEnable.Value, RegistryValueKind.DWord);
            else
                key.DeleteValue("ProxyEnable", throwOnMissingValue: false);

            if (_oldProxyServer is not null)
                key.SetValue("ProxyServer", _oldProxyServer, RegistryValueKind.String);
            else
                key.DeleteValue("ProxyServer", throwOnMissingValue: false);

            if (_oldProxyOverride is not null)
                key.SetValue("ProxyOverride", _oldProxyOverride, RegistryValueKind.String);
            else
                key.DeleteValue("ProxyOverride", throwOnMissingValue: false);

            RefreshInternetOptions();

            _applied = false;

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    private static void RefreshInternetOptions()
    {
        // Notify the system that the registry settings have changed
        InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
        InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
    }
}
