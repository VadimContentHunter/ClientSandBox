using System.Text.Json;
using ClientSandBox.Models;

namespace ClientSandBox.Services.Connections;

/// <summary>
/// Хранилище пользовательских подключений.
/// </summary>
public sealed class ConnectionStorage
{
    private const string FileName = "connections.json";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    private string FilePath => Path.Combine(AppContext.BaseDirectory, FileName);

    /// <summary>
    /// Список подключений.
    /// </summary>
    private readonly List<ConnectionInfo> _connections = [];

    /// <summary>
    /// Список подключений только для чтения.
    /// </summary>
    public IReadOnlyList<ConnectionInfo> Connections => _connections;

    /// <summary>
    /// Загружает подключения.
    /// </summary>
    public void Load()
    {
        _connections.Clear();

        if (!File.Exists(FilePath))
        {
            Save();
            return;
        }

        string json = File.ReadAllText(FilePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return;
        }

        List<ConnectionInfo>? connections = JsonSerializer.Deserialize<List<ConnectionInfo>>(json);
        if (connections is null)
        {
            return;
        }

        _connections.AddRange(connections);
    }

    /// <summary>
    /// Сохраняет подключения.
    /// </summary>
    public void Save()
    {
        string json = JsonSerializer.Serialize(_connections, JsonOptions);
        File.WriteAllText(FilePath, json);
    }

    /// <summary>
    /// Добавляет подключение.
    /// </summary>
    /// <param name="connection">Подключение.</param>
    public void Add(ConnectionInfo connection)
    {
        _connections.Add(connection);
    }

    /// <summary>
    /// Обновляет подключение.
    /// </summary>
    /// <param name="connection">Подключение.</param>
    public void Update(ConnectionInfo connection)
    {
        int index = _connections.FindIndex(x => x.Id == connection.Id);

        if (index < 0)
        {
            return;
        }

        _connections[index] = connection;
    }

    /// <summary>
    /// Удаляет подключение.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    public void Delete(Guid id)
    {
        ConnectionInfo? connection = Find(id);

        if (connection is null)
        {
            return;
        }

        _connections.Remove(connection);
    }

    /// <summary>
    /// Выполняет поиск подключения.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Подключение или null.</returns>
    public ConnectionInfo? Find(Guid id)
    {
        return _connections.FirstOrDefault(x => x.Id == id);
    }
}