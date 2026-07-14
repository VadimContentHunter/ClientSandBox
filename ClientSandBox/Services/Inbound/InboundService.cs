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

            return _currentInbounds;
        }
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
}