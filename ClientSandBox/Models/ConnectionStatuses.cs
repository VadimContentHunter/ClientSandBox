namespace ClientSandBox.Models;

/// <summary>
/// Статусы подключения.
/// </summary>
public static class ConnectionStatuses
{
    public const string Ready = "✔ Готов";

    public const string InvalidJson = "✖ Некорректный JSON";

    public const string MissingTag = "✖ Отсутствует tag";

    public const string MissingType = "✖ Отсутствует type";

    public const string DuplicateTag = "✖ Дублирующийся tag";

    public const string MultipleTun = "✖ Допускается только один TUN";

    public const string PortBusy = "⚠ Порт занят";
}