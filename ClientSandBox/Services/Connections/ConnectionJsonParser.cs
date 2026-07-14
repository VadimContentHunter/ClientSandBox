using System.Text.Json;

namespace ClientSandBox.Services.Connections;

/// <summary>
/// Выполняет разбор JSON подключения.
/// </summary>
public sealed class ConnectionJsonParser
{
    /// <summary>
    /// Название свойства "tag".
    /// </summary>
    private const string TagProperty = "tag";

    /// <summary>
    /// Название свойства "type".
    /// </summary>
    private const string TypeProperty = "type";

    /// <summary>
    /// Проверяет корректность JSON и преобразует его в JsonDocument.
    /// </summary>
    /// <param name="json">JSON подключения.</param>
    /// <param name="document">Результат разбора JSON.</param>
    /// <returns>
    /// true - если JSON успешно разобран;
    /// false - если JSON некорректный.
    /// </returns>
    public bool TryParse(string json, out JsonDocument? document)
    {
        document = null;

        if (string.IsNullOrWhiteSpace(json))
        {
            return false;
        }

        try
        {
            document = JsonDocument.Parse(json);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Проверяет наличие свойства.
    /// </summary>
    /// <param name="document">JSON документ.</param>
    /// <param name="propertyName">Название свойства.</param>
    /// <returns>
    /// true - если свойство существует;
    /// false - если отсутствует.
    /// </returns>
    public bool HasProperty(JsonDocument document, string propertyName)
    {
        return document.RootElement.TryGetProperty(propertyName, out _);
    }

    /// <summary>
    /// Возвращает строковое значение свойства.
    /// </summary>
    /// <param name="document">JSON документ.</param>
    /// <param name="propertyName">Название свойства.</param>
    /// <returns>Строковое значение или null.</returns>
    public string? GetString(JsonDocument document, string propertyName)
    {
        if (!document.RootElement.TryGetProperty(propertyName, out JsonElement value))
        {
            return null;
        }

        return value.ValueKind == JsonValueKind.String
            ? value.GetString()
            : value.ToString();
    }

    /// <summary>
    /// Возвращает целочисленное значение свойства.
    /// </summary>
    /// <param name="document">JSON документ.</param>
    /// <param name="propertyName">Название свойства.</param>
    /// <returns>Целочисленное значение или null.</returns>
    public int? GetInt(JsonDocument document, string propertyName)
    {
        if (!document.RootElement.TryGetProperty(propertyName, out JsonElement value))
        {
            return null;
        }

        if (value.ValueKind == JsonValueKind.Number &&
            value.TryGetInt32(out int result))
        {
            return result;
        }

        return null;
    }

    /// <summary>
    /// Возвращает логическое значение свойства.
    /// </summary>
    /// <param name="document">JSON документ.</param>
    /// <param name="propertyName">Название свойства.</param>
    /// <returns>Логическое значение или null.</returns>
    public bool? GetBool(JsonDocument document, string propertyName)
    {
        if (!document.RootElement.TryGetProperty(propertyName, out JsonElement value))
        {
            return null;
        }

        return value.ValueKind switch
        {
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            _ => null
        };
    }

    /// <summary>
    /// Возвращает JsonElement свойства.
    /// </summary>
    /// <param name="document">JSON документ.</param>
    /// <param name="propertyName">Название свойства.</param>
    /// <returns>JsonElement или null.</returns>
    public JsonElement? GetProperty(JsonDocument document, string propertyName)
    {
        if (!document.RootElement.TryGetProperty(propertyName, out JsonElement value))
        {
            return null;
        }

        return value.Clone();
    }

    /// <summary>
    /// Возвращает тег подключения.
    /// </summary>
    /// <param name="document">JSON документ.</param>
    /// <returns>Тег подключения или null.</returns>
    public string? GetTag(JsonDocument document)
    {
        return GetString(document, TagProperty);
    }

    /// <summary>
    /// Возвращает тип подключения.
    /// </summary>
    /// <param name="document">JSON документ.</param>
    /// <returns>Тип подключения или null.</returns>
    public string? GetType(JsonDocument document)
    {
        return GetString(document, TypeProperty);
    }

    /// <summary>
    /// Проверяет, принадлежит ли подключение одному из указанных типов.
    /// </summary>
    /// <param name="document">JSON документ.</param>
    /// <param name="types">Допустимые типы.</param>
    /// <returns>
    /// true - если тип подключения найден среди указанных;
    /// false - если тип отсутствует или не совпадает.
    /// </returns>
    public bool IsType(JsonDocument document, params string[] types)
    {
        string? currentType = GetType(document);

        if (string.IsNullOrWhiteSpace(currentType))
        {
            return false;
        }

        return types.Any(type =>
            string.Equals(currentType, type, StringComparison.OrdinalIgnoreCase));
    }
}