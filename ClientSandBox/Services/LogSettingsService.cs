using System.Text.Json.Nodes;
using ClientSandBox.Models;
using ClientSandBox.Services.AppLoggerService;

namespace ClientSandBox.Services;

/// <summary>
/// Сервис управления настройками логирования и их применением к config.json sing-box.
/// Отвечает за построение секции "log" для config.json и операции с файловыми логами.
/// </summary>
public static class LogSettingsService
{
    /// <summary>
    /// Построить JsonNode для секции "log" на основании настроек приложения.
    /// </summary>
    public static JsonNode BuildLogNode(Settings settings)
    {
        var obj = new JsonObject();

        // sing-box использует поле disabled для отключения логов
        obj["disabled"] = !settings.EnableLogging;

        obj["level"] = settings.LogLevel ?? "info";

        // timestamp всегда включён
        obj["timestamp"] = true;

        // output — sing-box ожидает строку (путь или "console" и т.п.)
        string outputValue;

        string outputType = string.IsNullOrWhiteSpace(settings.LogOutput)
            ? "console"
            : settings.LogOutput;

        if (string.Equals(outputType, "console", StringComparison.OrdinalIgnoreCase))
        {
            outputValue = "console";
        }
        else
        {
            // file или custom
            string path = settings.LogOutputPath;

            // default path and filename (singbox.log) under app logs dir
            string defaultDir = Path.Combine(AppContext.BaseDirectory, "logs");
            string defaultFileName = "singbox.log";
            string defaultFull = Path.Combine(defaultDir, defaultFileName);

            // If outputType == file => prefer default singbox.log unless user explicitly provided a valid file that ends with singbox.log
            if (string.Equals(outputType, "file", StringComparison.OrdinalIgnoreCase))
            {
                bool useDefault = false;

                if (string.IsNullOrWhiteSpace(path))
                {
                    useDefault = true;
                }
                else
                {
                    try
                    {
                        if (Directory.Exists(path))
                        {
                            // explicit directory provided -> ignore and use default
                            useDefault = true;
                        }
                    }
                    catch { useDefault = true; }
                }

                if (useDefault)
                {
                    try { Directory.CreateDirectory(defaultDir); } catch { }
                    // ensure file exists
                    try { if (!File.Exists(defaultFull)) using (File.Create(defaultFull)) { } } catch { }
                    outputValue = defaultFull;
                }
                else
                {
                    // path provided, check it's a file and ends with singbox.log
                    string fileName = Path.GetFileName(path);
                    string dir = Path.GetDirectoryName(path);

                    if (string.IsNullOrWhiteSpace(Path.GetExtension(path)))
                    {
                        // no extension -> fallback to default
                        try { Directory.CreateDirectory(defaultDir); } catch { }
                        try { if (!File.Exists(defaultFull)) using (File.Create(defaultFull)) { } } catch { }
                        outputValue = defaultFull;
                    }
                    else
                    {
                        string ext = Path.GetExtension(path).ToLowerInvariant();
                        if (ext != ".log" && ext != ".txt")
                        {
                            // invalid extension -> fallback to default
                            try { Directory.CreateDirectory(defaultDir); } catch { }
                            try { if (!File.Exists(defaultFull)) using (File.Create(defaultFull)) { } } catch { }
                            outputValue = defaultFull;
                        }
                        else
                        {
                            // if filename does not end with singbox.log use default
                            if (!fileName.EndsWith(defaultFileName, StringComparison.OrdinalIgnoreCase))
                            {
                                try { Directory.CreateDirectory(defaultDir); } catch { }
                                try { if (!File.Exists(defaultFull)) using (File.Create(defaultFull)) { } } catch { }
                                outputValue = defaultFull;
                            }
                            else
                            {
                                // use provided
                                try { if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir); } catch { }
                                try { if (!File.Exists(path)) using (File.Create(path)) { } } catch { }
                                outputValue = path;
                            }
                        }
                    }
                }
            }
            else // custom
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    AppLogger.Error("Custom output selected, but path is empty.");
                    return null;
                }

                try
                {
                    if (Directory.Exists(path))
                    {
                        AppLogger.Error($"Custom output path указывает на директорию: {path}");
                        return null;
                    }
                }
                catch { }

                if (string.IsNullOrWhiteSpace(Path.GetExtension(path)))
                {
                    AppLogger.Error($"Custom output path не содержит расширения файла: {path}");
                    return null;
                }

                string ext = Path.GetExtension(path).ToLowerInvariant();
                if (ext != ".log" && ext != ".txt")
                {
                    AppLogger.Error($"Неподдерживаемое расширение лог-файла: {ext}. Ожидается .log или .txt");
                    return null;
                }

                string dirCustom = Path.GetDirectoryName(path);
                try
                {
                    if (!string.IsNullOrWhiteSpace(dirCustom) && !Directory.Exists(dirCustom))
                        Directory.CreateDirectory(dirCustom);
                }
                catch (Exception ex)
                {
                    AppLogger.Error($"Не удалось создать директорию для custom логов: {ex.Message}");
                    return null;
                }

                try
                {
                    if (!File.Exists(path)) using (File.Create(path)) { }
                }
                catch (Exception ex)
                {
                    AppLogger.Error($"Не удалось создать custom файл логов: {ex.Message}");
                    return null;
                }

                outputValue = path;
            }
        }

        obj["output"] = outputValue;

        return obj;
    }

    /// <summary>
    /// Очистить файлы логов при запуске sing-box, если опция включена.
    /// Удаляет файлы внутри директории вывода логов (не рекурсивно).
    /// </summary>
    public static void ClearLogsOnStart()
    {
        try
        {
            var settings = SettingsService.Current;

            if (!settings.ClearLogsOnStart)
                return;
            // Определим целевой путь вывода логов так же, как и в BuildLogNode
            string target;

            if (string.Equals(settings.LogOutput, "console", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(settings.LogOutput))
            {
                // нет файлов для очистки
                return;
            }

            if (string.Equals(settings.LogOutput, "file", StringComparison.OrdinalIgnoreCase) || string.Equals(settings.LogOutput, "custom", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(settings.LogOutputPath))
                {
                    string defaultDir = Path.Combine(AppContext.BaseDirectory, "logs");
                    target = Path.Combine(defaultDir, "sing-box.log");
                }
                else
                {
                    target = settings.LogOutputPath;
                }
            }
            else
            {
                // Если значение нестандартное — используем его как путь/имя файла
                target = settings.LogOutputPath ?? settings.LogOutput;
            }

            if (string.IsNullOrWhiteSpace(target))
                return;

            // Если target — папка, очистим все файлы в ней; если файл — удалим его или очистим содержимое
            try
            {
                if (Directory.Exists(target))
                {
                    foreach (var file in Directory.GetFiles(target))
                    {
                        TryDeleteOrTruncate(file);
                    }
                }
                else
                {
                    // Возможно указан файл
                    if (File.Exists(target))
                    {
                        TryDeleteOrTruncate(target);
                    }
                    else
                    {
                        // Может быть путь к файлу в несуществующей папке — попробуем создать папку и пустой файл
                        var dir = Path.GetDirectoryName(target);
                        if (!string.IsNullOrWhiteSpace(dir))
                        {
                            try { Directory.CreateDirectory(dir); } catch { }
                        }
                    }
                }
            }
            catch
            {
                // ignore
            }
        }
        catch
        {
            // ignore any errors to avoid breaking start flow
        }
    }

    private static void TryDeleteOrTruncate(string file)
    {
        try
        {
            File.Delete(file);
        }
        catch
        {
            try
            {
                using var fs = new FileStream(file, FileMode.Truncate);
            }
            catch
            {
                // ignore
            }
        }
    }
}
