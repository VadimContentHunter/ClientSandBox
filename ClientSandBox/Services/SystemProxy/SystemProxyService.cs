using System;
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
                int? port = parser.GetInt(doc, "listen_port");

                if (string.IsNullOrWhiteSpace(listen) || port is null)
                    continue;

                string proxy = $"{listen}:{port.Value}";

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
    /// При отсутствии сохранённых значений удаляет ProxyServer/ProxyOverride если ProxyEnable==0.
    /// </summary>
    public static (bool Success, string Message) Restore()
    {
        try
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(InternetSettingsKey, writable: true)!;
            if (key is null)
                return (false, "Не удалось открыть реестр для Internet Settings.");

            if (_applied)
            {
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

            // Если мы не применяли прокси, но в реестре остался ProxyServer при отключённом ProxyEnable,
            // это может мешать отдельным компонентам/приложениям. Попробуем аккуратно удалить такие значения.
            object? pe = key.GetValue("ProxyEnable");
            int proxyEnable = pe is int i ? i : 0;
            object? currentServer = key.GetValue("ProxyServer");

            if (proxyEnable == 0 && currentServer is string server && !string.IsNullOrWhiteSpace(server))
            {
                try
                {
                    key.DeleteValue("ProxyServer", throwOnMissingValue: false);
                    key.DeleteValue("ProxyOverride", throwOnMissingValue: false);
                    // Оповещаем систему
                    RefreshInternetOptions();
                }
                catch
                {
                    // Игнорируем ошибки удаления
                }
            }

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    /// <summary>
    /// Утилита: включает системный прокси по хосту/порту.
    /// </summary>
    public static (bool Success, string Message) Enable(string host, int port)
    {
        if (string.IsNullOrWhiteSpace(host))
            return (false, "Host is empty.");

        return ApplyProxy($"{host}:{port}");
    }

    /// <summary>
    /// Утилита: отключает системный прокси (удаляет ProxyServer/ProxyOverride).
    /// </summary>
    public static (bool Success, string Message) Disable()
    {
        try
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(InternetSettingsKey, writable: true)!;
            if (key is null)
                return (false, "Не удалось открыть реестр для Internet Settings.");

            key.SetValue("ProxyEnable", 0, RegistryValueKind.DWord);

            try
            {
                key.DeleteValue("ProxyServer", throwOnMissingValue: false);
                key.DeleteValue("ProxyOverride", throwOnMissingValue: false);
            }
            catch
            {
                // ignore
            }

            RefreshInternetOptions();

            _applied = false;

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
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(InternetSettingsKey);

        if (key is null)
            return false;

        object? value = key.GetValue("ProxyEnable");
        return value is int enabled && enabled == 1;
    }

    private static void RefreshInternetOptions()
    {
        // Notify the system that the registry settings have changed
        NativeMethods.InternetSetOption(IntPtr.Zero, InternetOptions.INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
        NativeMethods.InternetSetOption(IntPtr.Zero, InternetOptions.INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
    }
}
