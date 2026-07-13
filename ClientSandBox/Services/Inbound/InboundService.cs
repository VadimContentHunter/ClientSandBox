using ClientSandBox.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ClientSandBox.Services;

namespace ClientSandBox.Services.Inbound;

/// <summary>
/// Сервис работы с Inbound.
/// </summary>
public static class InboundService
{
    /// <summary>
    /// Считывает список Inbound из конфигурации.
    /// </summary>
    /// <param name="configPath">Путь к config.json.</param>
    /// <returns>Список найденных Inbound.</returns>
    public static List<InboundInfo> Load(string configPath)
    {
        JsonDocument? document = ConfigReader.Read(configPath);

        if (document is null)
        {
            return [];
        }

        using (document)
        {
            return InboundReader.Read(document);
        }
    }
}
