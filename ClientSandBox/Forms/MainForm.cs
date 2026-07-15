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
    private readonly System.Windows.Forms.Timer _statusTimer = new();

    private bool? _lastRunningState;

    private int? _lastPid;

    private bool _allowClose;

    private readonly ConnectionStorage _connectionStorage = new();

    private readonly ConnectionManager _connectionManager;

    public MainForm()
    {
        InitializeComponent();

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

        RefreshUI();
    }

    private void LoadSettings()
    {
        txtSingBox.Text = SettingsService.Current.SingBoxPath;
        txtConfig.Text = SettingsService.Current.ConfigPath;
        chkCloseToTray.Checked = SettingsService.Current.CloseToTray;
    }

    private void SaveSettings()
    {
        SettingsService.Current.SingBoxPath = txtSingBox.Text;
        SettingsService.Current.ConfigPath = txtConfig.Text;
        SettingsService.Current.CloseToTray = chkCloseToTray.Checked;

        SettingsService.Save();

        //RefreshUI();
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
    private void StartSingBox()
    {
        // 1) Проверяем, что выбрано хотя бы одно подключение
        var selected = _connectionManager.GetConnections().Where(x => x.IsEnabled).ToList();

        if (!selected.Any())
        {
            MessageBox.Show(
                "Не выбрано ни одного подключения. Выберите подключение для запуска.",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            return;
        }

        // 2) Сохраняем исходный путь к конфигу
        string originalConfig = SettingsService.Current.ConfigPath;

        // 3) Формируем новый конфиг и перезаписываем оригинал, сохраняя единый бэкап
        var builder = new ConnectionBuilder();
        var buildResult = builder.BuildAndReplaceConfig(selected, originalConfig);

        if (!buildResult.Success)
        {
            MessageBox.Show(buildResult.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // 4) Применяем системный прокси при необходимости (для http/mixed)
        var proxyApplyResult = ClientSandBox.Services.SystemProxy.SystemProxyService.ApplyProxyForSelected(selected);
        if (proxyApplyResult.Success)
        {
            // nothing, proxy applied
        }
        else
        {
            // Если не найдено подходящих proxy — это не фатально, продолжаем; иначе покажем информацию
            if (!string.Equals(proxyApplyResult.Message, "No applicable http/mixed proxy found among selected connections."))
            {
                MessageBox.Show($"Не удалось применить системный прокси: {proxyApplyResult.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Попробуем восстановить бекап и выйти
                string backupPath = originalConfig + ".backup";
                if (File.Exists(backupPath))
                {
                    File.Copy(backupPath, originalConfig, overwrite: true);
                }

                return;
            }
        }

        // 5) Проверяем конфиг с помощью sing-box (CheckConfig читает Settings.Current.ConfigPath)
        var check = SingBoxService.CheckConfig();
        if (!check.Success)
        {
            MessageBox.Show($"Проверка конфигурации не пройдена: {check.Output}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Если проверка не пройдена, восстанавливаем бекап (если есть) и откатываем proxy
            string backupPath = originalConfig + ".backup";
            if (File.Exists(backupPath))
            {
                File.Copy(backupPath, originalConfig, overwrite: true);
            }

            ClientSandBox.Services.SystemProxy.SystemProxyService.Restore();

            return;
        }

        // 6) Запускаем sing-box с обновлённым config.json
        bool started = ExecuteCommand(SingBoxRunner.Start, false);

        if (started)
        {
            MessageBox.Show("Sing-box запущен с обновлённым конфигом.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    /// <summary>
    /// Останавливает sing-box.
    /// </summary>
    private void StopSingBox()
    {
        ExecuteCommand(SingBoxRunner.Stop);

        // При остановке приложения восстанавливаем системный прокси если применяли
        ClientSandBox.Services.SystemProxy.SystemProxyService.Restore();
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
