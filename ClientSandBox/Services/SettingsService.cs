using System.Text.Json;
using ClientSandBox.Models;

namespace ClientSandBox.Services;

public static class SettingsService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    private static readonly string SettingsFile =
        Path.Combine(AppContext.BaseDirectory, "settings.json");

    public static Settings Current { get; private set; } = new();

    public static void Load()
    {
        try
        {
            if (!File.Exists(SettingsFile))
            {
                Current = new Settings();
                Save();
                return;
            }

            string json = File.ReadAllText(SettingsFile);

            Current = JsonSerializer.Deserialize<Settings>(json)
                      ?? new Settings();
        }
        catch (Exception ex)
        {
            AppLogger.Exception(ex);

            Current = new Settings();
        }
    }

    public static void Save()
    {
        try
        {
            string json = JsonSerializer.Serialize(Current, JsonOptions);

            File.WriteAllText(SettingsFile, json);
        }
        catch (Exception ex)
        {
            AppLogger.Exception(ex);
        }
    }
}