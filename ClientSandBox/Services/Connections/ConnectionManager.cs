using ClientSandBox.Models;
using ClientSandBox.Services.Connections.Validators;
using System.Text.Json;

namespace ClientSandBox.Services.Connections;

/// <summary>
/// Управляет пользовательскими подключениями.
/// </summary>
public sealed class ConnectionManager(ConnectionStorage storage)
{
    private readonly ConnectionStorage _storage = storage;

    private readonly ConnectionJsonParser _parser = new();

    private readonly IReadOnlyList<IConnectionValidator> _validators =
    [
        new RequiredFieldsValidator(),
        new DuplicateTagValidator(),
        new TunValidator(),
        new SystemProxyValidator()
    ];

    /// <summary>
    /// Возвращает список подключений.
    /// </summary>
    /// <returns>Список подключений.</returns>
    public IReadOnlyList<ConnectionInfo> GetConnections()
    {
        return _storage.Connections;
    }

    /// <summary>
    /// Выполняет проверку подключения.
    /// </summary>
    /// <param name="connection">Подключение.</param>
    /// <returns>Результат проверки.</returns>
    private ConnectionValidationResult Validate(ConnectionInfo connection)
    {
        foreach (IConnectionValidator validator in _validators)
        {
            ConnectionValidationResult result = validator.Validate(connection, _storage.Connections);

            if (!result.Success)
            {
                connection.Status = result.Status;
                return result;
            }
        }

        connection.Status = ConnectionStatuses.Ready;
        return ConnectionValidationResult.Ok(ConnectionStatuses.Ready);
    }

    /// <summary>
    /// Добавляет новое подключение.
    /// </summary>
    /// <param name="json">JSON подключения.</param>
    /// <returns>Результат операции.</returns>
    public ConnectionValidationResult Add(string json)
    {
        if (!_parser.TryParse(json, out JsonDocument? document))
        {
            return ConnectionValidationResult.Error(ConnectionStatuses.InvalidJson, "Некорректный JSON.");
        }

        ConnectionInfo connection = new()
        {
            Json = json,
            Tag = _parser.GetTag(document!) ?? string.Empty,
            Type = _parser.GetType(document!) ?? string.Empty
        };

        ConnectionValidationResult result = Validate(connection);

        if (!result.Success)
        {
            return result;
        }

        _storage.Add(connection);
        _storage.Save();

        return result;
    }

    /// <summary>
    /// Обновляет подключение.
    /// </summary>
    /// <param name="id">Идентификатор подключения.</param>
    /// <param name="json">Новый JSON.</param>
    /// <returns>Результат операции.</returns>
    public ConnectionValidationResult Update(Guid id, string json)
    {
        ConnectionInfo? connection = _storage.Find(id);

        if (connection is null)
        {
            return ConnectionValidationResult.Error(ConnectionStatuses.InvalidJson, "Подключение не найдено.");
        }

        if (!_parser.TryParse(json, out JsonDocument? document))
        {
            return ConnectionValidationResult.Error(ConnectionStatuses.InvalidJson, "Некорректный JSON.");
        }

        connection.Json = json;
        connection.Tag = _parser.GetTag(document!) ?? string.Empty;
        connection.Type = _parser.GetType(document!) ?? string.Empty;

        ConnectionValidationResult result = Validate(connection);

        if (!result.Success)
        {
            return result;
        }

        _storage.Update(connection);
        _storage.Save();
        return result;
    }

    /// <summary>
    /// Удаляет подключение.
    /// </summary>
    /// <param name="id">Идентификатор подключения.</param>
    public void Delete(Guid id)
    {
        _storage.Delete(id);
        _storage.Save();
    }

}