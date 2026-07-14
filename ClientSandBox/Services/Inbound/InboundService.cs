using System.Text.Json;
using ClientSandBox.Models;
using ClientSandBox.Services.Inbound.Validators;

namespace ClientSandBox.Services.Inbound;

/// <summary>
/// Сервис работы с Inbound.
/// </summary>
public static class InboundService
{
    /// <summary>
    /// Список анализаторов Inbound.
    /// </summary>
    private static readonly IReadOnlyList<IInboundValidator> Validators =
    [
        new SystemProxyValidator(),
        new TunValidator(),
        new UnknownValidator()
    ];

    /// <summary>
    /// Последний загруженный список Inbound.
    /// </summary>
    private static List<InboundInfo> _currentInbounds = [];

    /// <summary>
    /// Загружает и анализирует список Inbound.
    /// </summary>
    /// <param name="configPath">Путь к config.json.</param>
    /// <returns>Список Inbound.</returns>
    public static List<InboundInfo> Load(string configPath)
    {
        JsonDocument? document = ConfigReader.Read(configPath);

        if (document is null)
        {
            _currentInbounds = [];
            return _currentInbounds;
        }

        using (document)
        {
            _currentInbounds = InboundReader.Read(document);

            Analyze(_currentInbounds);
            RestoreSelection();

            return _currentInbounds;
        }
    }

    /// <summary>
    /// Делает Inbound выбранным.
    /// </summary>
    /// <param name="inbound">Inbound.</param>
    public static void Select(InboundInfo inbound)
    {
        foreach (InboundInfo current in _currentInbounds)
        {
            current.IsSelected = false;
        }

        inbound.IsSelected = true;

        SettingsService.Current.SelectedInboundTag =
            inbound.GetString("tag") ?? string.Empty;

        SettingsService.Save();
    }

    /// <summary>
    /// Возвращает выбранный Inbound.
    /// </summary>
    public static InboundInfo? GetSelected()
    {
        return _currentInbounds.FirstOrDefault(i => i.IsSelected);
    }

    /// <summary>
    /// Выполняет анализ всех Inbound.
    /// </summary>
    private static void Analyze(IEnumerable<InboundInfo> inbounds)
    {
        foreach (InboundInfo inbound in inbounds)
        {
            foreach (IInboundValidator validator in Validators)
            {
                validator.Analyze(inbound);
            }
        }
    }

    /// <summary>
    /// Восстанавливает выбранный Inbound из настроек.
    /// </summary>
    private static void RestoreSelection()
    {
        string tag = SettingsService.Current.SelectedInboundTag;

        if (string.IsNullOrWhiteSpace(tag))
        {
            return;
        }

        InboundInfo? selected =
            _currentInbounds.FirstOrDefault(x =>
                string.Equals(
                    x.GetString("tag"),
                    tag,
                    StringComparison.OrdinalIgnoreCase));

        if (selected is null)
        {
            return;
        }

        selected.IsSelected = true;
    }
}