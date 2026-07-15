using System.Text.Json;
using System.Text.Json.Nodes;
using ClientSandBox.Models;

namespace ClientSandBox.Services.Connections;

/// <summary>
/// Формирует секцию inbounds на основании выбранных подключений.
/// </summary>
public sealed class ConnectionBuilder
{
    /// <summary>
    /// Формирует новый config.json на основании baseConfigPath и выбранных подключений.
    /// Перед заменой оригинального файла сохраняет единый бекап (baseConfigPath + ".backup"),
    /// если он ещё не существует. После этого перезаписывает оригинальный config.json.
    /// Возвращает (Success, Message).
    /// </summary>
    public (bool Success, string Message) BuildAndReplaceConfig(IEnumerable<ConnectionInfo> selectedConnections, string baseConfigPath)
    {
        if (selectedConnections is null || !selectedConnections.Any())
            return (false, "Не выбрано ни одного подключения.");

        if (string.IsNullOrWhiteSpace(baseConfigPath) || !File.Exists(baseConfigPath))
            return (false, "Исходный config.json не найден.");

        try
        {
            string baseJson = File.ReadAllText(baseConfigPath);

            JsonNode? root = JsonNode.Parse(baseJson);
            if (root is null || root is not JsonObject)
                return (false, "Некорректный базовый config.json.");

            JsonObject rootObj = (JsonObject)root;

            // Удаляем существующий inbounds
            if (rootObj.ContainsKey("inbounds"))
            {
                rootObj.Remove("inbounds");
            }

            var inbounds = new JsonArray();

            foreach (ConnectionInfo conn in selectedConnections)
            {
                if (string.IsNullOrWhiteSpace(conn.Json))
                    continue;

                JsonNode? node = JsonNode.Parse(conn.Json);
                if (node is null)
                    continue;

                if (node is JsonObject jsonObj)
                {
                    inbounds.Add(jsonObj);
                }
                else if (node is JsonArray jsonArr)
                {
                    // если JSON содержит массив inbounds, добавляем все элементы
                    foreach (JsonNode? item in jsonArr)
                    {
                        if (item is JsonObject o)
                            inbounds.Add(o);
                    }
                }
            }

            rootObj["inbounds"] = inbounds;

            string resultJson = root.ToJsonString(new JsonSerializerOptions { WriteIndented = true });

            // Создаём бекап, если его ещё нет
            string backupPath = baseConfigPath + ".backup";
            if (!File.Exists(backupPath))
            {
                File.Copy(baseConfigPath, backupPath);
            }

            // Перезаписываем оригинальный config
            File.WriteAllText(baseConfigPath, resultJson);

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }
}
