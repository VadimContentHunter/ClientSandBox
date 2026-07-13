using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ClientSandBox.Models;

public sealed class InboundInfo
{
    /// <summary>
    /// Выбран ли данный Inbound пользователем.
    /// </summary>
    public bool IsSelected { get; set; }

    /// <summary>
    /// Все свойства Inbound из config.json.
    /// </summary>
    public Dictionary<string, JsonElement> Properties { get; } = [];

    /// <summary>
    /// Результат проверки Inbound.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    public string? GetString(string key) { ... }

    public int? GetInt(string key) { ... }

    public bool? GetBool(string key) { ... }

    public JsonElement? Get(string key) { ... }
}
