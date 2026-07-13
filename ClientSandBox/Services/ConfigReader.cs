using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ClientSandBox.Services;

/// <summary>
/// Чтение конфигурации sing-box.
/// </summary>
public static class ConfigReader
{
    /// <summary>
    /// Считывает config.json.
    /// </summary>
    /// <param name="configPath">Путь к config.json.</param>
    /// <returns>JsonDocument либо null, если чтение невозможно.</returns>
    public static JsonDocument? Read(string configPath)
    {
        if (string.IsNullOrWhiteSpace(configPath))
        {
            return null;
        }

        if (!File.Exists(configPath))
        {
            return null;
        }

        using FileStream stream = File.OpenRead(configPath);

        return JsonDocument.Parse(stream);
    }
}
