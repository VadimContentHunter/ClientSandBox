using ClientSandBox.Models;
using ClientSandBox.Services;
using ClientSandBox.Services.Inbound;
using ClientSandBox.Services.SingBox;
using System.Diagnostics;
using System.Drawing;

namespace ClientSandBox.Forms;

public partial class MainForm : Form
{
    private readonly System.Windows.Forms.Timer _statusTimer = new();
    private bool? _lastRunningState;
    private int? _lastPid;
    private bool _allowClose;
    private const string SelectedColumnName = "colSelected";

    public MainForm()
    {
        InitializeComponent();
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

    #region Connections
    /// <summary>
    /// Обновляет список подключений.
    /// </summary>
    private void RefreshConnections()
    {
        if (!File.Exists(SettingsService.Current.ConfigPath))
        {
            gridConnections.Rows.Clear();
            gridConnections.Enabled = false;
            lblLastUpdate.Text = "—";

            return;
        }

        IReadOnlyList<InboundInfo> inbounds =
            InboundService.Load(SettingsService.Current.ConfigPath);

        FillConnectionsGrid(inbounds);
        UpdateLastRefreshTime();
        gridConnections.Enabled = inbounds.Count > 0;
    }

    /// <summary>
    /// Заполняет таблицу подключений.
    /// </summary>
    /// <param name="inbounds">Список Inbound.</param>
    private void FillConnectionsGrid(IReadOnlyList<InboundInfo> inbounds)
    {
        gridConnections.Rows.Clear();

        foreach (InboundInfo inbound in inbounds)
        {
            int rowIndex = gridConnections.Rows.Add(
                inbound.IsSelected,
                inbound.GetString("tag") ?? "—",
                inbound.GetString("type") ?? "—",
                BuildConnectionAddress(inbound),
                inbound.Status
            );

            DataGridViewRow row = gridConnections.Rows[rowIndex];
            row.Tag = inbound;

            row.Cells[SelectedColumnName].ReadOnly =
                !CanSelectInbound(inbound);

            ApplyStatusStyle(row, inbound);
        }

        gridConnections.ClearSelection();
    }

    /// <summary>
    /// Определяет, доступен ли Inbound для выбора.
    /// </summary>
    private static bool CanSelectInbound(InboundInfo inbound)
    {
        return inbound.Status == InboundStatuses.Ready;
    }

    /// <summary>
    /// Делает выбранным только один Inbound.
    /// </summary>
    /// <param name="selectedRow">Выбранная строка.</param>
    private void SelectInbound(DataGridViewRow selectedRow)
    {
        if (selectedRow.Tag is not InboundInfo inbound)
        {
            return;
        }

        if (!CanSelectInbound(inbound))
        {
            return;
        }

        InboundService.Select(inbound);
        RefreshConnections();
    }

    private void ApplyStatusStyle(DataGridViewRow row, InboundInfo inbound)
    {
        DataGridViewCell cell = row.Cells["colStatus"];

        switch (inbound.Status)
        {
            case InboundStatuses.Ready:
                cell.Style.ForeColor = Color.Green;
                break;

            case InboundStatuses.PortInUse:
                cell.Style.ForeColor = Color.Red;
                break;

            case InboundStatuses.InsufficientData:
                cell.Style.ForeColor = Color.DarkOrange;
                break;

            case InboundStatuses.UnknownType:
                cell.Style.ForeColor = Color.Gray;
                break;

            case InboundStatuses.AnalysisError:
                cell.Style.ForeColor = Color.Red;
                break;
        }

        cell.ToolTipText = inbound.Status;
    }

    /// <summary>
    /// Формирует отображаемый адрес подключения.
    /// </summary>
    /// <param name="inbound">Inbound.</param>
    /// <returns>Адрес подключения или "—".</returns>
    private static string BuildConnectionAddress(InboundInfo inbound)
    {
        string? type = inbound.GetString("type");

        switch (type)
        {
            case "mixed":
            case "http":
            case "socks":
                {
                    string? listen = inbound.GetString("listen");
                    int? port = inbound.GetInt("listen_port");

                    if (string.IsNullOrWhiteSpace(listen) || port is null)
                    {
                        return "—";
                    }

                    return $"{listen}:{port}";
                }

            case "tun":
                {
                    string? interfaceName = inbound.GetString("interface_name");
                    string? address = inbound.GetString("inet4_address");

                    if (string.IsNullOrWhiteSpace(interfaceName) ||
                        string.IsNullOrWhiteSpace(address))
                    {
                        return "—";
                    }

                    return $"{interfaceName} ({address})";
                }

            default:
                return "—";
        }
    }

    /// <summary>
    /// Обновляет время последнего обновления.
    /// </summary>
    private void UpdateLastRefreshTime()
    {
        lblLastUpdate.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
    }
    #endregion

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
        UpdateTraffic();
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
        UpdateTraffic();
        UpdateButtons();

        RefreshConnections();
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
        btnReconnectTraffic.Enabled = SingBoxRunner.IsRunning && InboundConnectionService.CanConnect;
        btnDisconnectTraffic.Enabled = InboundConnectionService.IsConnected;
    }

    private void UpdateStatus()
    {
        if (!SingBoxRunner.IsRunning && InboundConnectionService.IsConnected)
        {
            InboundConnectionService.Disconnect();
        }

        if (SingBoxRunner.IsRunning)
        {
            lblStatusSingBox.Text = "🟢 Запущен";
            lblPidInf.Text = SingBoxRunner.ProcessId?.ToString();
            lblStatusSingBox.ForeColor = Color.Green;
        }else{
            lblStatusSingBox.Text = "✖ Не запущен";
            lblStatusSingBox.ForeColor = Color.Red;
            lblPidInf.Text = "—";
        }
    }

    private void UpdateTraffic()
    {
        bool connected = InboundConnectionService.IsConnected;

        lblTrafficInfo.ForeColor = connected
            ? Color.Green
            : Color.Red;

        lblTrafficInfo.Text = connected
            ? $"🟢 {InboundConnectionService.GetConnectionInfo()}"
            : $"✖ {InboundConnectionService.GetConnectionInfo()}";
    }

    /// <summary>
    /// Запускает sing-box и перенаправляет трафик.
    /// </summary>
    private void StartSingBox()
    {
        if (!ExecuteCommand(SingBoxRunner.Start, false))
        {
            return;
        }

        ExecuteCommand(InboundConnectionService.Connect);
    }

    /// <summary>
    /// Останавливает перенаправление трафика и sing-box.
    /// </summary>
    private void StopSingBox()
    {
        if (!ExecuteCommand(InboundConnectionService.Disconnect, false))
        {
            return;
        }

        ExecuteCommand(SingBoxRunner.Stop);
    }

    /// <summary>
    /// Перезапускает sing-box и повторно перенаправляет трафик.
    /// </summary>
    private void RestartSingBox()
    {
        if (!ExecuteCommand(InboundConnectionService.Disconnect, false))
        {
            return;
        }

        if (!ExecuteCommand(SingBoxRunner.Restart, false))
        {
            return;
        }

        ExecuteCommand(InboundConnectionService.Connect);
    }

    /// <summary>
    /// Перенаправляет трафик через выбранный Inbound.
    /// </summary>
    private void ReconnectTraffic()
    {
        ExecuteCommand(InboundConnectionService.Reconnect);
    }

    /// <summary>
    /// Отключает перенаправление трафика.
    /// </summary>
    private void DisconnectTraffic()
    {
        ExecuteCommand(InboundConnectionService.Disconnect);
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

    private void btnReconnectTraffic_Click(object? sender, EventArgs e)
    {
        ReconnectTraffic();
    }

    private void btnDisconnectTraffic_Click(object? sender, EventArgs e)
    {
        DisconnectTraffic();
    }

    private void trayMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        miStart.Enabled = btnStartSingBox.Enabled;
        miStop.Enabled = btnStopSingBox.Enabled;
        miRestart.Enabled = btnRestartSingBox.Enabled;
    }

    private void btnRefreshConnections_Click(object sender, EventArgs e)
    {
        RefreshConnections();
    }

    private void gridConnections_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        if (gridConnections.Columns[e.ColumnIndex].Name != SelectedColumnName)
        {
            return;
        }

        SelectInbound(gridConnections.Rows[e.RowIndex]);
    }

    private void gridConnections_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        SelectInbound(gridConnections.Rows[e.RowIndex]);
    }
}
