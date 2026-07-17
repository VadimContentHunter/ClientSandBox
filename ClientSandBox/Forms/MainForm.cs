using ClientSandBox.Models;
using ClientSandBox.Services;
using ClientSandBox.Services.Connections;
using ClientSandBox.Services.SingBox;
using System.Diagnostics;
using System.Text.Json;
using System.Text;
using System.Linq;

namespace ClientSandBox.Forms;

public partial class MainForm : Form
{
    private System.IO.FileSystemWatcher? _logWatcher;
    private string? _watchedLogFile;
    private readonly object _logLock = new();
    private readonly System.Windows.Forms.Timer _statusTimer = new();

    private bool? _lastRunningState;

    private int? _lastPid;

    private bool _allowClose;

    private readonly ConnectionStorage _connectionStorage = new();

    private readonly ConnectionManager _connectionManager;

    public MainForm()
    {
        InitializeComponent();

        // Попытка восстановить системные настройки (если ранее были применены)
        try
        {
            ClientSandBox.Services.SystemProxy.SystemProxyService.Restore();
        }
        catch
        {
            // игнорируем ошибки при восстановлении
        }

        _connectionManager = new ConnectionManager(_connectionStorage);
        _connectionStorage.Load();
        RefreshConnections();

        LoadSettings();
        notifyIcon.Icon = Icon;
        _statusTimer.Interval = 2000;
        _statusTimer.Tick += StatusTimer_Tick;
        _statusTimer.Start();

        txtSingBox.TextChanged += (_, _) => SaveSettings();
        txtConfig.TextChanged += (_, _) => SaveSettings();
        chkCloseToTray.CheckedChanged += (_, _) => SaveSettings();
        chkStartWithWindows.CheckedChanged += (_, _) => SaveSettings();
        // Log settings events
        chkEnableLogging.CheckedChanged += (_, _) => SaveSettings();
        cmbLogLevel.SelectedIndexChanged += (_, _) => SaveSettings();
        cmbLogOutput.SelectedIndexChanged += (_, e) => { SaveSettings(); UpdateLogOutputVisibility(); UpdateLogViewerState(); };
        btnBrowseLogPath.Click += BtnBrowseLogPath_Click;
        txtLogOutputPath.TextChanged += (_, _) => SaveSettings();
        chkClearLogsOnStart.CheckedChanged += (_, _) => SaveSettings();
        chkAutoScroll.CheckedChanged += (_, _) => SaveSettings();
        numAutoClearMinutes.ValueChanged += (_, _) => SaveSettings();
        numKeepLastLines.ValueChanged += (_, _) => SaveSettings();
        numTailLinesToShow.ValueChanged += (_, _) => SaveSettings();
        btnOpenLog.Click += BtnOpenLog_Click;
        btnClearLog.Click += BtnClearLog_Click;

        RefreshUI();
    }

    private void LoadSettings()
    {
        txtSingBox.Text = SettingsService.Current.SingBoxPath;
        txtConfig.Text = SettingsService.Current.ConfigPath;
        chkCloseToTray.Checked = SettingsService.Current.CloseToTray;
        chkStartWithWindows.Checked = SettingsService.Current.StartWithWindows;
        // Load log settings
        try
        {
            chkEnableLogging.Checked = SettingsService.Current.EnableLogging;
            cmbLogLevel.SelectedItem = SettingsService.Current.LogLevel;
            cmbLogOutput.SelectedItem = SettingsService.Current.LogOutput;
            txtLogOutputPath.Text = SettingsService.Current.LogOutputPath ?? string.Empty;
            chkClearLogsOnStart.Checked = SettingsService.Current.ClearLogsOnStart;
            // Auto-scroll
            try { chkAutoScroll.Checked = SettingsService.Current.AutoScrollLogs; } catch { }
            numAutoClearMinutes.Value = Math.Max(0, Math.Min((int)numAutoClearMinutes.Maximum, SettingsService.Current.AutoClearMinutes));
            numKeepLastLines.Value = Math.Max(1, Math.Min((int)numKeepLastLines.Maximum, SettingsService.Current.KeepLastLinesOnAutoClear));
            numTailLinesToShow.Value = Math.Max(1, Math.Min((int)numTailLinesToShow.Maximum, SettingsService.Current.TailLinesToShow));
        }
        catch
        {
            // ignore if controls not initialized yet
        }
        // Update UI visibility and viewer after loading
        UpdateLogOutputVisibility();
        UpdateLogViewerState();
    }

    private void SaveSettings()
    {
        SettingsService.Current.SingBoxPath = txtSingBox.Text;
        SettingsService.Current.ConfigPath = txtConfig.Text;
        SettingsService.Current.CloseToTray = chkCloseToTray.Checked;
        SettingsService.Current.StartWithWindows = chkStartWithWindows.Checked;
        // Save log settings (if controls available)
        try
        {
            SettingsService.Current.EnableLogging = chkEnableLogging.Checked;
            SettingsService.Current.LogLevel = cmbLogLevel.SelectedItem?.ToString() ?? SettingsService.Current.LogLevel;
            SettingsService.Current.LogOutput = cmbLogOutput.SelectedItem?.ToString() ?? SettingsService.Current.LogOutput;
            SettingsService.Current.LogOutputPath = txtLogOutputPath.Text ?? string.Empty;
            SettingsService.Current.ClearLogsOnStart = chkClearLogsOnStart.Checked;
            SettingsService.Current.AutoScrollLogs = chkAutoScroll.Checked;
            SettingsService.Current.AutoClearMinutes = (int)numAutoClearMinutes.Value;
            SettingsService.Current.KeepLastLinesOnAutoClear = (int)numKeepLastLines.Value;
            SettingsService.Current.TailLinesToShow = (int)numTailLinesToShow.Value;

            // If output is file and path empty, use default logs folder in app base dir
            if (SettingsService.Current.LogOutput == "file")
            {
                if (string.IsNullOrWhiteSpace(SettingsService.Current.LogOutputPath))
                {
                    var defaultLogs = Path.Combine(AppContext.BaseDirectory, "logs");
                    Directory.CreateDirectory(defaultLogs);
                    SettingsService.Current.LogOutputPath = defaultLogs;
                }
                else
                {
                    try
                    {
                        Directory.CreateDirectory(SettingsService.Current.LogOutputPath);
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }
        }
        catch
        {
            // ignore
        }

        SettingsService.Save();

        // update viewer in case output or path changed
        UpdateLogOutputVisibility();
        UpdateLogViewerState();

        //RefreshUI();
    }

    private void UpdateLogOutputVisibility()
    {
        try
        {
            var selected = cmbLogOutput.SelectedItem?.ToString() ?? SettingsService.Current.LogOutput ?? "console";
            bool isCustom = string.Equals(selected, "custom", StringComparison.OrdinalIgnoreCase);
            // show path only for custom
            txtLogOutputPath.Visible = isCustom;
            btnBrowseLogPath.Visible = isCustom;

            // If file selected but path empty, show resolved default path in label
            if (string.Equals(selected, "file", StringComparison.OrdinalIgnoreCase))
            {
                var path = SettingsService.Current.LogOutputPath;
                if (string.IsNullOrWhiteSpace(path))
                {
                    var defaultDir = Path.Combine(AppContext.BaseDirectory, "logs");
                    var defaultFile = Path.Combine(defaultDir, "singbox.log");
                    lblCurrentLogFile.Text = $"Текущий лог: {defaultFile}";
                }
            }
        }
        catch
        {
            // ignore
        }
    }

    private void UpdateLogViewerState()
    {
        try
        {
            // determine effective log file path
            string output = SettingsService.Current.LogOutput ?? "console";
            string effectivePath = string.Empty;
            const string defaultFileName = "singbox.log";

            if (string.Equals(output, "console", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(output))
            {
                lblCurrentLogFile.Text = "Текущий лог: (console)";
                StopWatchingLog();
                rtbLogViewer.Text = "Нет файлового вывода логов. Выберите вывод в файл или custom.";
                return;
            }

            if (string.Equals(output, "file", StringComparison.OrdinalIgnoreCase))
            {
                var path = SettingsService.Current.LogOutputPath;
                if (string.IsNullOrWhiteSpace(path))
                {
                    var defaultDir = Path.Combine(AppContext.BaseDirectory, "logs");
                    effectivePath = Path.Combine(defaultDir, defaultFileName);
                }
                else
                {
                    // if user provided a directory, use default file inside it
                    try
                    {
                        if (Directory.Exists(path))
                        {
                            effectivePath = Path.Combine(path, defaultFileName);
                        }
                        else
                        {
                            effectivePath = path;
                        }
                    }
                    catch
                    {
                        effectivePath = path;
                    }
                }
            }
            else if (string.Equals(output, "custom", StringComparison.OrdinalIgnoreCase))
            {
                effectivePath = SettingsService.Current.LogOutputPath ?? string.Empty;
            }
            else
            {
                // If output contains a filename string
                effectivePath = SettingsService.Current.LogOutputPath ?? output;
            }

            if (string.IsNullOrWhiteSpace(effectivePath))
            {
                lblCurrentLogFile.Text = "Текущий лог: —";
                StopWatchingLog();
                rtbLogViewer.Text = "Не указан путь к файлу логов.";
                return;
            }

            // Ensure file exists for file output
            try
            {
                if (string.Equals(output, "file", StringComparison.OrdinalIgnoreCase))
                {
                    var dir = Path.GetDirectoryName(effectivePath);
                    if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    if (!File.Exists(effectivePath))
                    {
                        try { using (File.Create(effectivePath)) { } } catch { }
                    }
                }
            }
            catch
            {
                // ignore
            }

            lblCurrentLogFile.Text = $"Текущий лог: {effectivePath}";
            StartWatchingLog(effectivePath);
            RefreshLogViewer();
        }
        catch
        {
            // ignore
        }
    }

    private void StartWatchingLog(string path)
    {
        try
        {
            if (string.Equals(_watchedLogFile, path, StringComparison.OrdinalIgnoreCase))
                return;

            StopWatchingLog();

            _watchedLogFile = path;

            var dir = Path.GetDirectoryName(path) ?? Path.GetDirectoryName(AppContext.BaseDirectory) ?? AppContext.BaseDirectory;
            var fileName = Path.GetFileName(path);

            _logWatcher = new System.IO.FileSystemWatcher(dir, fileName)
            {
                NotifyFilter = System.IO.NotifyFilters.LastWrite | System.IO.NotifyFilters.Size | System.IO.NotifyFilters.FileName
            };
            _logWatcher.Changed += LogWatcher_Changed;
            _logWatcher.Created += LogWatcher_Changed;
            _logWatcher.Renamed += LogWatcher_Changed;
            _logWatcher.EnableRaisingEvents = true;
        }
        catch
        {
            // ignore
        }
    }

    private void StopWatchingLog()
    {
        try
        {
            if (_logWatcher is not null)
            {
                _logWatcher.Changed -= LogWatcher_Changed;
                _logWatcher.Created -= LogWatcher_Changed;
                _logWatcher.Renamed -= LogWatcher_Changed;
                _logWatcher.EnableRaisingEvents = false;
                _logWatcher.Dispose();
                _logWatcher = null;
            }

            _watchedLogFile = null;
        }
        catch
        {
            // ignore
        }
    }

    private void LogWatcher_Changed(object? sender, System.IO.FileSystemEventArgs e)
    {
        // debounce briefly
        try
        {
            System.Threading.Thread.Sleep(50);
        }
        catch { }

        RefreshLogViewer();
    }

    private void RefreshLogViewer()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_watchedLogFile) || !File.Exists(_watchedLogFile))
                return;

            int lines = SettingsService.Current.TailLinesToShow > 0 ? SettingsService.Current.TailLinesToShow : (int)numTailLinesToShow.Value;
            var tail = ReadLastLines(_watchedLogFile, lines);

            // update UI on UI thread
            string newText = string.Join(Environment.NewLine, tail);
            if (InvokeRequired)
            {
                BeginInvoke(() => UpdateRtbContent(newText));
            }
            else
            {
                UpdateRtbContent(newText);
            }
        }
        catch
        {
            // ignore
        }
    }

    private void UpdateRtbContent(string text)
    {
        try
        {
            rtbLogViewer.SuspendLayout();
            rtbLogViewer.Text = text;
            bool auto = SettingsService.Current.AutoScrollLogs;
            if (auto)
            {
                rtbLogViewer.SelectionStart = rtbLogViewer.TextLength;
                rtbLogViewer.ScrollToCaret();
            }
            rtbLogViewer.ResumeLayout();
        }
        catch
        {
            // ignore
        }
    }

    private static IEnumerable<string> ReadLastLines(string path, int n)
    {
        try
        {
            var result = new List<string>();
            if (n <= 0)
                return result;

            const int bufferSize = 8192;
            var encoding = Encoding.UTF8;

            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            long fileLength = fs.Length;
            if (fileLength == 0)
                return result;

            long position = fileLength;
            var leftover = string.Empty;

            while (position > 0 && result.Count < n)
            {
                int readSize = (int)Math.Min(bufferSize, position);
                position -= readSize;
                fs.Seek(position, SeekOrigin.Begin);
                var buffer = new byte[readSize];
                int actuallyRead = fs.Read(buffer, 0, readSize);
                string chunk = encoding.GetString(buffer, 0, actuallyRead);

                // Prepend chunk to leftover to build continuous text
                string combined = chunk + leftover;
                var parts = combined.Split('\n');

                // If we read from middle, the first part may be partial line — keep it as leftover for next iteration
                leftover = parts[0];

                // Process remaining parts (from 1..end)
                for (int i = parts.Length - 1; i >= 1 && result.Count < n; i--)
                {
                    var line = parts[i].TrimEnd('\r');
                    // Skip possible empty line at end
                    if (line.Length == 0)
                    {
                        if (i == parts.Length - 1)
                            continue;
                    }
                    result.Insert(0, line);
                }
            }

            // After loop, leftover contains the earliest fragment (start of file to first newline)
            if (result.Count < n && !string.IsNullOrEmpty(leftover))
            {
                var first = leftover.TrimEnd('\r', '\n');
                if (first.Length > 0)
                    result.Insert(0, first);
            }

            // Ensure we return at most n last lines
            if (result.Count > n)
                return result.Skip(result.Count - n).ToList();

            return result;
        }
        catch
        {
            return Array.Empty<string>();
        }
    }

    private void BtnOpenLog_Click(object? sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_watchedLogFile) || !File.Exists(_watchedLogFile))
            {
                MessageBox.Show("Файл логов не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = _watchedLogFile,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось открыть лог: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnClearLog_Click(object? sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_watchedLogFile) || !File.Exists(_watchedLogFile))
            {
                MessageBox.Show("Файл логов не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Трим файла до нуля байт, не удаляя его
                using var fs = new FileStream(_watchedLogFile, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
            }
            catch
            {
                try
                {
                    // Фоллбек: перезаписать пустой строкой
                    File.WriteAllText(_watchedLogFile, string.Empty, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось очистить лог: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            RefreshLogViewer();
        }
        catch
        {
            // ignore
        }
    }

    private void BtnBrowseLogPath_Click(object? sender, EventArgs e)
    {
        using var dlg = new FolderBrowserDialog();
        dlg.Description = "Выберите папку для логов (или отмените для использования дефолтной)";
        if (!string.IsNullOrWhiteSpace(txtLogOutputPath.Text) && Directory.Exists(txtLogOutputPath.Text))
        {
            dlg.SelectedPath = txtLogOutputPath.Text;
        }

        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            txtLogOutputPath.Text = dlg.SelectedPath;
        }
    }

    private void UpdateLogControlsEnabled()
    {
        bool locked = SettingsService.Current.LockLogSettingsWhileRunning && SingBoxRunner.IsRunning;
        // Disable or enable all log-related controls
        try
        {
            chkEnableLogging.Enabled = !locked;
            cmbLogLevel.Enabled = !locked;
            cmbLogOutput.Enabled = !locked;
            txtLogOutputPath.Enabled = !locked;
            btnBrowseLogPath.Enabled = !locked;
            chkClearLogsOnStart.Enabled = !locked;
            numAutoClearMinutes.Enabled = !locked;
            numKeepLastLines.Enabled = !locked;
            numTailLinesToShow.Enabled = !locked;
        }
        catch
        {
            // ignore if controls not ready
        }
    }

    private void StatusTimer_Tick(object? sender, EventArgs e)
    {
        bool running = SingBoxRunner.IsRunning;
        int? pid = SingBoxRunner.ProcessId;

        if (_lastRunningState == running &&
            _lastPid == pid)
        {
            return;
        }

        _lastRunningState = running;
        _lastPid = pid;

        UpdateStatus();
        UpdateButtons();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _statusTimer.Stop();

        if (!_allowClose &&
            chkCloseToTray.Checked &&
            e.CloseReason == CloseReason.UserClosing
        )
        {
            e.Cancel = true;
            Hide();
            notifyIcon.Visible = true;
            return;
        }

        base.OnFormClosing(e);
    }

    private void ValidateSingBoxPath()
    {
        bool exists = File.Exists(txtSingBox.Text);

        txtSingBox.BackColor = exists
            ? SystemColors.Window
            : Color.MistyRose;
    }

    private void ValidateConfigPath()
    {
        bool exists = File.Exists(txtConfig.Text);

        txtConfig.BackColor = exists
            ? SystemColors.Window
            : Color.MistyRose;
    }

    private void RefreshUI()
    {
        ValidateSingBoxPath();
        ValidateConfigPath();

        UpdateVersion();
        UpdateStatus();
        UpdateButtons();
        UpdateLogControlsEnabled();
    }

    private void UpdateVersion()
    {
        if (string.IsNullOrWhiteSpace(txtSingBox.Text))
        {
            lblVersion.Text = "Не указан путь";
            return;
        }

        if (!File.Exists(txtSingBox.Text))
        {
            lblVersion.Text = "Файл не найден";
            return;
        }

        var result = SingBoxService.GetVersion();

        lblVersion.Text = result.Success
            ? result.Output
            : "Ошибка получения версии";
    }

    private void UpdateButtons()
    {
        bool hasExe = File.Exists(txtSingBox.Text);
        bool hasConfig = File.Exists(txtConfig.Text);
        bool canStart = hasExe && hasConfig && !SingBoxRunner.IsRunning;
        bool canStop = SingBoxRunner.IsRunning;

        btnCheckConfig.Enabled = hasExe && hasConfig;
        btnStartSingBox.Enabled = canStart;
        btnStopSingBox.Enabled = canStop;
        btnRestartSingBox.Enabled = canStop;
    }

    private void UpdateStatus()
    {
        if (SingBoxRunner.IsRunning)
        {
            lblStatusSingBox.Text = "🟢 Запущен";
            lblPidInf.Text = SingBoxRunner.ProcessId?.ToString();
            lblStatusSingBox.ForeColor = Color.Green;
        }
        else
        {
            lblStatusSingBox.Text = "✖ Не запущен";
            lblStatusSingBox.ForeColor = Color.Red;
            lblPidInf.Text = "—";
        }
    }

    /// <summary>
    /// Запускает sing-box.
    /// </summary>
    private void btnRestoreNetwork_Click(object? sender, EventArgs e)
    {
        // Попытка восстановить системные прокси и netsh правила
        try
        {
            var res1 = ClientSandBox.Services.SystemProxy.SystemProxyService.Restore();
            // NetshPortProxyService может отсутствовать в проекте, обработаем безопасно
            try
            {
                // Вызовем если тип доступен
                var netshType = Type.GetType("ClientSandBox.Services.SystemProxy.NetshPortProxyService, ClientSandBox");
                if (netshType is not null)
                {
                    var restoreMethod = netshType.GetMethod("Restore", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                    if (restoreMethod is not null)
                    {
                        restoreMethod.Invoke(null, null);
                    }
                }
            }
            catch
            {
                // ignore
            }

            if (!res1.Success)
            {
                MessageBox.Show($"Не удалось восстановить proxy: {res1.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Дополнительные системные команды
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c netsh winhttp reset proxy & netsh interface portproxy reset & netsh winsock reset & ipconfig /flushdns",
                    UseShellExecute = true,
                    Verb = "runas"
                });
            }
            catch
            {
                // ignore
            }

            MessageBox.Show("Команды восстановления выполнены. Перезагрузите систему если проблемы сохранятся.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при восстановлении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void StartSingBox()
    {
        // Перед запуском формируем config.json на основании выбранных подключений
        try
        {
            var builder = new ClientSandBox.Services.Connections.ConnectionBuilder();
            var selected = _connectionManager.GetConnections().Where(c => c.IsEnabled).ToList();
            if (selected.Any())
            {
                var res = builder.BuildAndReplaceConfig(selected, SettingsService.Current.ConfigPath);
                if (!res.Success)
                {
                    MessageBox.Show($"Не удалось сформировать config.json: {res.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Если среди выбранных есть http/mixed - пробуем применить системный прокси
            try
            {
                bool hasProxyType = selected.Any(c => !string.IsNullOrWhiteSpace(c.Type) &&
                    (string.Equals(c.Type, "http", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(c.Type, "mixed", StringComparison.OrdinalIgnoreCase)));

                if (hasProxyType)
                {
                    var applyResult = ClientSandBox.Services.SystemProxy.SystemProxyService.ApplyProxyForSelected(selected);
                    if (!applyResult.Success)
                    {
                        MessageBox.Show($"Не удалось применить системный прокси: {applyResult.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Попытка вызвать NetshPortProxyService.Apply, если такой сервис присутствует
                    try
                    {
                        var netshType = Type.GetType("ClientSandBox.Services.SystemProxy.NetshPortProxyService, ClientSandBox");
                        if (netshType is not null)
                        {
                            var applyMethod = netshType.GetMethod("Apply", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                            if (applyMethod is not null)
                            {
                                // ожидаем, что метод может принимать IEnumerable<ConnectionInfo> или ничего
                                var parameters = applyMethod.GetParameters();
                                if (parameters.Length == 1)
                                {
                                    applyMethod.Invoke(null, new object[] { selected });
                                }
                                else
                                {
                                    applyMethod.Invoke(null, null);
                                }
                            }
                        }
                    }
                    catch
                    {
                        // ignore optional netsh helper failures
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при применении системного прокси: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Очистка логов перед запуском, если включена соответствующая опция
            try
            {
                ClientSandBox.Services.LogSettingsService.ClearLogsOnStart();
            }
            catch
            {
                // ignore
            }

            if (!ExecuteCommand(SingBoxRunner.Start, false))
            {
                return;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при подготовке конфигурации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
    }

    /// <summary>
    /// Останавливает sing-box.
    /// </summary>
    private void StopSingBox()
    {
        ExecuteCommand(SingBoxRunner.Stop);

        // При остановке пытаемся восстановить системный прокси и netsh правила,
        // чтобы трафик вернулся в норму без дополнительного вмешательства пользователя.
        try
        {
            var restoreRes = ClientSandBox.Services.SystemProxy.SystemProxyService.Restore();
            if (!restoreRes.Success)
            {
                // Показываем предупреждение, но не прерываем работу
                MessageBox.Show($"Восстановление системного прокси завершилось с ошибкой: {restoreRes.Message}", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch
        {
            // ignore
        }

        try
        {
            var netshType = Type.GetType("ClientSandBox.Services.SystemProxy.NetshPortProxyService, ClientSandBox");
            if (netshType is not null)
            {
                var restoreMethod = netshType.GetMethod("Restore", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                if (restoreMethod is not null)
                {
                    try
                    {
                        restoreMethod.Invoke(null, null);
                    }
                    catch
                    {
                        // Если netsh требует elevation, предупредим пользователя
                        MessageBox.Show("Не удалось автоматически восстановить netsh правила. Запустите приложение от имени администратора и нажмите \"Восстановить сеть\".", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        catch
        {
            // ignore
        }
    }

    /// <summary>
    /// Перезапускает sing-box.
    /// </summary>
    private void RestartSingBox()
    {
        if (!ExecuteCommand(SingBoxRunner.Restart, false))
        {
            return;
        }
    }

    private void RefreshConnections()
    {
        gridConnections.Rows.Clear();

        foreach (ConnectionInfo connection in _connectionManager.GetConnections())
        {
            string info = FormatConnectionInfo(connection.Json);

            int rowIndex = gridConnections.Rows.Add(
                connection.IsEnabled,
                connection.Tag,
                connection.Type,
                connection.Status,
                info
            );

            gridConnections.Rows[rowIndex].Tag = connection;
        }
    }

    private static string FormatConnectionInfo(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return string.Empty;

        try
        {
            using JsonDocument doc = JsonDocument.Parse(json);
            var sb = new StringBuilder();

            foreach (JsonProperty prop in doc.RootElement.EnumerateObject())
            {
                string name = prop.Name;

                // Пропускаем служебные поля
                if (string.Equals(name, "tag", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(name, "type", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (sb.Length > 0)
                    sb.Append(',');

                sb.Append(name);
                sb.Append(": ");

                string value = FormatElement(prop.Value);
                sb.Append(value);
            }

            return sb.ToString();
        }
        catch
        {
            return json;
        }
    }

    private static string FormatElement(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Array:
                return string.Join(',', element.EnumerateArray().Select(e => FormatElementValue(e)));
            case JsonValueKind.Object:
                // Для объектов сериализуем компактно без пробелов
                return JsonSerializer.Serialize(element, new JsonSerializerOptions { WriteIndented = false });
            default:
                return FormatElementValue(element);
        }
    }

    private static string FormatElementValue(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.String => element.GetString() ?? string.Empty,
            JsonValueKind.Number => element.GetRawText(),
            JsonValueKind.True => "true",
            JsonValueKind.False => "false",
            JsonValueKind.Null => "null",
            _ => element.GetRawText()
        };
    }

    private ConnectionInfo? GetSelectedConnection()
    {
        if (gridConnections.SelectedRows.Count == 0)
        {
            return null;
        }

        return gridConnections.SelectedRows[0].Tag as ConnectionInfo;
    }

    private static bool ShowError((bool Success, string Output) result)
    {
        if (result.Success)
            return false;

        MessageBox.Show(result.Output, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        return true;
    }

    private static void ShowResult((bool Success, string Output) result)
    {
        string message = string.IsNullOrWhiteSpace(result.Output)
            ? (result.Success ? "Операция успешно выполнена."
                              : "Операция завершилась с ошибкой.")
            : result.Output;

        MessageBox.Show(message,
            result.Success ? "Успешно" : "Ошибка",
            MessageBoxButtons.OK,
            result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
    }

    private void ShowMainWindow()
    {
        Show();
        WindowState = FormWindowState.Normal;
        ShowInTaskbar = true;
        BringToFront();
        Activate();
        notifyIcon.Visible = false;
    }

    private bool ExecuteCommand(
    Func<(bool Success, string Output)> command, bool refreshUi = true)
    {
        (bool Success, string Output) result = command();
        ShowError(result);
        if (refreshUi)
        {
            RefreshUI();
        }

        return result.Success;
    }

    private void PathTextBox_Leave(object? sender, EventArgs e)
    {
        RefreshUI();
    }

    private void PathTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
            return;

        RefreshUI();

        e.Handled = true;
        e.SuppressKeyPress = true;
    }

    private void btnBrowseSingBox_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dialog = new();

        dialog.Title = "Выберите sing-box.exe";
        dialog.Filter = "Executable (*.exe)|*.exe";

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        txtSingBox.Text = dialog.FileName;
    }

    private void btnBrowseConfig_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dialog = new();

        dialog.Title = "Выберите config.json";
        dialog.Filter = "JSON (*.json)|*.json|Все файлы (*.*)|*.*";

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        txtConfig.Text = dialog.FileName;
    }

    private void btnStartSingBox_Click(object? sender, EventArgs e)
    {
        StartSingBox();
    }

    private void btnStopSingBox_Click(object? sender, EventArgs e)
    {
        StopSingBox();
    }

    private void btnRestartSingBox_Click(object? sender, EventArgs e)
    {
        RestartSingBox();
    }

    private void btnCheckConfig_Click(object? sender, EventArgs e)
    {
        ShowResult(SingBoxService.CheckConfig());
    }

    private void btnOpenSingBoxFolder_Click(object? sender, EventArgs e)
    {
        if (!File.Exists(txtSingBox.Text))
        {
            MessageBox.Show(
                "Файл sing-box.exe не найден.",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            return;
        }

        Process.Start(new ProcessStartInfo
        {
            FileName = "explorer.exe",
            Arguments = $"/select,\"{txtSingBox.Text}\"",
            UseShellExecute = true
        });
    }

    private void btnOpenConfig_Click(object? sender, EventArgs e)
    {
        if (!File.Exists(txtConfig.Text))
        {
            MessageBox.Show(
                "Файл config.json не найден.",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            return;
        }

        Process.Start(new ProcessStartInfo
        {
            FileName = txtConfig.Text,
            UseShellExecute = true
        });
    }

    private void notifyIcon_DoubleClick(object? sender, EventArgs e)
    {
        ShowMainWindow();
    }

    private void miOpen_Click(object? sender, EventArgs e)
    {
        ShowMainWindow();
    }

    private void miStart_Click(object sender, EventArgs e)
    {
        StartSingBox();
    }

    private void miStop_Click(object sender, EventArgs e)
    {
        StopSingBox();
    }

    private void miRestart_Click(object sender, EventArgs e)
    {
        RestartSingBox();
    }

    private void miExit_Click(object sender, EventArgs e)
    {
        _allowClose = true;
        notifyIcon.Visible = false;
        Close();
    }

    private void trayMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        miStart.Enabled = btnStartSingBox.Enabled;
        miStop.Enabled = btnStopSingBox.Enabled;
        miRestart.Enabled = btnRestartSingBox.Enabled;
    }

    private void btnAddConnection_Click(object? sender, EventArgs e)
    {
        using ConnectionEditForm form = new();
        if (form.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        ConnectionValidationResult result = _connectionManager.Add(form.ConnectionJson);
        if (!result.Success)
        {
            MessageBox.Show(
                result.Message,
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            return;
        }

        RefreshConnections();
    }

    private void gridConnections_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right || e.RowIndex < 0)
        {
            return;
        }

        gridConnections.ClearSelection();
        gridConnections.Rows[e.RowIndex].Selected = true;
        gridConnections.CurrentCell = gridConnections.Rows[e.RowIndex].Cells[1];
    }

    private void gridConnections_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        // Оставляем пустым — используем CurrentCellDirtyStateChanged для обработки чекбокса
        // чтобы корректно получать новое значение после редактирования ячейки.
    }

    private void gridConnections_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
        if (gridConnections.CurrentCell is null)
            return;

        if (gridConnections.CurrentCell.OwningColumn != colEnabled)
            return;

        if (gridConnections.IsCurrentCellDirty)
        {
            gridConnections.CommitEdit(DataGridViewDataErrorContexts.Commit);

            int rowIndex = gridConnections.CurrentCell.RowIndex;
            DataGridViewCell cell = gridConnections.Rows[rowIndex].Cells[colEnabled.Index];
            bool enabled = false;
            try
            {
                enabled = Convert.ToBoolean(cell.Value);
            }
            catch
            {
                // ignore conversion errors, treat as false
            }

            ConnectionInfo? connection = gridConnections.Rows[rowIndex].Tag as ConnectionInfo;
            if (connection is null)
                return;

            ToggleConnectionEnabled(connection, enabled);
        }
    }

    private void gridConnections_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        ConnectionInfo? connection = gridConnections.Rows[e.RowIndex].Tag as ConnectionInfo;
        if (connection is null)
            return;

        // Двойной клик по строке переключает состояние чекбокса
        ToggleConnectionEnabled(connection, !connection.IsEnabled);
    }

    private void miSelectConnection_Click(object? sender, EventArgs e)
    {
        ConnectionInfo? connection = GetSelectedConnection();
        if (connection is null)
            return;

        ToggleConnectionEnabled(connection, true);
    }

    private void ToggleConnectionEnabled(ConnectionInfo connection, bool enabled)
    {
        // Если включаем подключение
        if (enabled)
        {
            // Если включаем TUN - выключаем все остальные подключения, остаётся только выбранный TUN
            if (string.Equals(connection.Type, "tun", StringComparison.OrdinalIgnoreCase))
            {
                var toDisable = _connectionManager.GetConnections()
                    .Where(other => !EqualityComparer<Guid>.Default.Equals(other.Id, connection.Id) && other.IsEnabled)
                    .ToList();

                foreach (ConnectionInfo other in toDisable)
                {
                    other.IsEnabled = false;
                    _connectionStorage.Update(other);
                }
            }
            else
            {
                // Если включаем не-TUN, то снимаем отметку со всех включённых TUN
                var toDisableTun = _connectionManager.GetConnections()
                    .Where(other => !EqualityComparer<Guid>.Default.Equals(other.Id, connection.Id) &&
                                    other.IsEnabled &&
                                    string.Equals(other.Type, "tun", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (ConnectionInfo other in toDisableTun)
                {
                    other.IsEnabled = false;
                    _connectionStorage.Update(other);
                }
            }
        }

        connection.IsEnabled = enabled;
        _connectionStorage.Update(connection);
        _connectionStorage.Save();

        // Обновляем UI и восстанавливаем выделение на том же подключении
        RefreshConnections();

        for (int i = 0; i < gridConnections.Rows.Count; i++)
        {
            if (gridConnections.Rows[i].Tag is ConnectionInfo info && EqualityComparer<Guid>.Default.Equals(info.Id, connection.Id))
            {
                gridConnections.ClearSelection();
                gridConnections.Rows[i].Selected = true;
                // Ставим текущую ячейку на чекбокс-столбец
                gridConnections.CurrentCell = gridConnections.Rows[i].Cells[colEnabled.Index];
                break;
            }
        }
    }

    private void miEditConnection_Click(object? sender, EventArgs e)
    {
        ConnectionInfo? connection = GetSelectedConnection();
        if (connection is null)
        {
            return;
        }

        using ConnectionEditForm form = new();
        form.ConnectionJson = connection.Json;
        if (form.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        ConnectionValidationResult result = _connectionManager.Update(connection.Id, form.ConnectionJson);
        if (!result.Success)
        {
            MessageBox.Show(
                result.Message,
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            return;
        }

        RefreshConnections();
    }

    private void miDeleteConnection_Click(object? sender, EventArgs e)
    {
        ConnectionInfo? connection = GetSelectedConnection();
        if (connection is null)
        {
            return;
        }

        DialogResult dialogResult = MessageBox.Show(
            $"Удалить подключение \"{connection.Tag}\"?",
            "Удаление подключения",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (dialogResult != DialogResult.Yes)
        {
            return;
        }

        _connectionManager.Delete(connection.Id);
        RefreshConnections();
    }
}
