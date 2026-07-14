using System.Diagnostics;
using System.Text;

namespace ClientSandBox.Services.SingBox;

/// <summary>
/// Выполняет сбор вывода процесса sing-box во время запуска.
/// </summary>
public sealed class SingBoxStartupMonitor : IDisposable
{
    private readonly StringBuilder _standardOutput = new();
    private readonly StringBuilder _standardError = new();
    private readonly object _locker = new();

    private Process? _process;

    public string StandardOutput
    {
        get
        {
            lock (_locker)
            {
                return _standardOutput.ToString();
            }
        }
    }

    public string StandardError
    {
        get
        {
            lock (_locker)
            {
                return _standardError.ToString();
            }
        }
    }

    public void Start(Process process)
    {
        _process = process;

        process.OutputDataReceived += Process_OutputDataReceived;
        process.ErrorDataReceived += Process_ErrorDataReceived;

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
    }

    public void Dispose()
    {
        if (_process is null)
        {
            return;
        }

        _process.OutputDataReceived -= Process_OutputDataReceived;
        _process.ErrorDataReceived -= Process_ErrorDataReceived;

        _process = null;
    }

    private void Process_OutputDataReceived(object? sender, DataReceivedEventArgs arguments)
    {
        if (string.IsNullOrWhiteSpace(arguments.Data))
            return;

        lock (_locker)
        {
            _standardOutput.AppendLine(arguments.Data);
        }
    }

    private void Process_ErrorDataReceived(object? sender, DataReceivedEventArgs arguments)
    {
        if (string.IsNullOrWhiteSpace(arguments.Data))
            return;

        lock (_locker)
        {
            _standardError.AppendLine(arguments.Data);
        }
    }
}
