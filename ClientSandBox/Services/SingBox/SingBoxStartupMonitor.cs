using System.Diagnostics;
using System.Text;

namespace ClientSandBox.Services.SingBox;

/// <summary>
/// Выполняет сбор вывода процесса sing-box во время запуска.
/// </summary>
public sealed class SingBoxStartupMonitor
{
    private readonly StringBuilder _output = new();
    private readonly StringBuilder _error = new();

    private readonly object _locker = new();

    /// <summary>
    /// Стандартный вывод процесса.
    /// </summary>
    public string StandardOutput
    {
        get
        {
            lock (_locker)
            {
                return _output.ToString();
            }
        }
    }

    /// <summary>
    /// Вывод ошибок процесса.
    /// </summary>
    public string StandardError
    {
        get
        {
            lock (_locker)
            {
                return _error.ToString();
            }
        }
    }

    /// <summary>
    /// Начинает отслеживание процесса.
    /// </summary>
    /// <param name="process">Процесс sing-box.</param>
    public void Start(Process process)
    {
        process.OutputDataReceived += Process_OutputDataReceived;
        process.ErrorDataReceived += Process_ErrorDataReceived;

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
    }

    /// <summary>
    /// Обрабатывает стандартный вывод процесса.
    /// </summary>
    private void Process_OutputDataReceived(
        object? sender,
        DataReceivedEventArgs arguments)
    {
        if (string.IsNullOrWhiteSpace(arguments.Data))
        {
            return;
        }

        lock (_locker)
        {
            _output.AppendLine(arguments.Data);
        }
    }

    /// <summary>
    /// Обрабатывает вывод ошибок процесса.
    /// </summary>
    private void Process_ErrorDataReceived(
        object? sender,
        DataReceivedEventArgs arguments)
    {
        if (string.IsNullOrWhiteSpace(arguments.Data))
        {
            return;
        }

        lock (_locker)
        {
            _error.AppendLine(arguments.Data);
        }
    }
}