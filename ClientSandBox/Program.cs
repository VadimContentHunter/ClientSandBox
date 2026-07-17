using ClientSandBox.Forms;
using ClientSandBox.Models;
using ClientSandBox.Services;
using ClientSandBox.Services.AppLoggerService;
using System.Linq;
using System.Text;
using System.Threading;

namespace ClientSandBox;

internal static class Program
{
    private static Mutex? _mutex;

    [STAThread]
    private static void Main(string[] args)
    {
        _mutex = new Mutex(
            initiallyOwned: true,
            name: @"Local\VadimContentHunter.ClientSandBox.Manager.8A0F64A1B8D1",
            createdNew: out bool createdNew);

        if (!createdNew)
        {
            MessageBox.Show(
                "Приложение уже запущено.",
                "ClientSandBox",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        ApplicationConfiguration.Initialize();

        AppLogger.Initialize();
        SettingsService.Load();
        StartMode startMode = args.Any(arg =>
            arg.Equals("--autostart", StringComparison.OrdinalIgnoreCase))
            ? StartMode.AutoStart
            : StartMode.Normal;
        bool isAutoStart = args.Any(arg => arg.Equals("--autostart", StringComparison.OrdinalIgnoreCase));

        try
        {
            Application.Run(new MainForm(startMode));
        }
        finally
        {
            _mutex.Dispose();
        }
    }
}