using System.Text.Json;

namespace ClientSandBox.Models;

/// <summary>
/// Информация об одном Inbound из config.json.
/// </summary>
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
    /// Результат анализа Inbound.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Проверяет наличие свойства.
    /// </summary>
    /// <param name="key">Название свойства.</param>
    /// <returns>true, если свойство существует.</returns>
    public bool Contains(string key)
    {
        return Properties.ContainsKey(key);
    }

    /// <summary>
    /// Возвращает JsonElement по имени свойства.
    /// </summary>
    /// <param name="key">Название свойства.</param>
    /// <returns>Значение свойства или null.</returns>
    public JsonElement? Get(string key)
    {
        return Properties.TryGetValue(key, out JsonElement value)
            ? value
            : null;
    }

    /// <summary>
    /// Возвращает строковое значение свойства.
    /// </summary>
    /// <param name="key">Название свойства.</param>
    /// <returns>Строковое значение или null.</returns>
    public string? GetString(string key)
    {
        JsonElement? value = Get(key);

        if (value is null)
        {
            return null;
        }

        return value.Value.ValueKind == JsonValueKind.String
            ? value.Value.GetString()
            : value.Value.ToString();
    }

    /// <summary>
    /// Возвращает целочисленное значение свойства.
    /// </summary>
    /// <param name="key">Название свойства.</param>
    /// <returns>Целочисленное значение или null.</returns>
    public int? GetInt(string key)
    {
        JsonElement? value = Get(key);

        if (value is null)
        {
            return null;
        }

        if (value.Value.ValueKind == JsonValueKind.Number &&
            value.Value.TryGetInt32(out int result))
        {
            return result;
        }

        return null;
    }

    /// <summary>
    /// Возвращает логическое значение свойства.
    /// </summary>
    /// <param name="key">Название свойства.</param>
    /// <returns>Логическое значение или null.</returns>
    public bool? GetBool(string key)
    {
        JsonElement? value = Get(key);

        if (value is null)
        {
            return null;
        }

        return value.Value.ValueKind switch
        {
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            _ => null
        };
    }

    public bool IsType(params string[] types)
    {
        string? currentType = GetString("type");

        if (string.IsNullOrWhiteSpace(currentType))
        {
            return false;
        }

        return types.Any(type =>
            string.Equals(currentType, type, StringComparison.OrdinalIgnoreCase));
    }
}