using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ClientSandBox.Models;

namespace ClientSandBox.Services.Inbound;

/// <summary>
/// Чтение Inbound из конфигурации sing-box.
/// </summary>
public static class InboundReader
{
    /// <summary>
    /// Считывает все Inbound из JsonDocument.
    /// </summary>
    /// <param name="document">Конфигурация sing-box.</param>
    /// <returns>Список найденных Inbound.</returns>
    public static List<InboundInfo> Read(JsonDocument document)
    {
        List<InboundInfo> result = [];

        if (!document.RootElement.TryGetProperty("inbounds", out JsonElement inbounds))
        {
            return result;
        }

        if (inbounds.ValueKind != JsonValueKind.Array)
        {
            return result;
        }

        foreach (JsonElement inboundElement in inbounds.EnumerateArray())
        {
            if (inboundElement.ValueKind != JsonValueKind.Object)
            {
                continue;
            }

            InboundInfo inbound = new();

            foreach (JsonProperty property in inboundElement.EnumerateObject())
            {
                inbound.Properties[property.Name] = property.Value.Clone();
            }

            result.Add(inbound);
        }

        return result;
    }
}
