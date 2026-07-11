namespace ClientSandBox.Models;

public class Settings
{
    /// <summary>
    /// Путь к sing-box.exe
    /// </summary>
    public string SingBoxPath { get; set; } = string.Empty;

    /// <summary>
    /// Путь к config.json
    /// </summary>
    public string ConfigPath { get; set; } = string.Empty;

    /// <summary>
    /// Сворачивать в трей при закрытии.
    /// </summary>
    public bool CloseToTray { get; set; } = true;
}