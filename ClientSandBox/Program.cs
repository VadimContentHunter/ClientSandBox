using ClientSandBox.Forms;
using ClientSandBox.Services;
using ClientSandBox.Services.AppLoggerService;
using System.Text;
using System.Threading;

namespace ClientSandBox;

internal static class Program
{
    private static Mutex? _mutex;

    [STAThread]
    private static void Main()
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

        try
        {
            Application.Run(new MainForm());
        }
        finally
        {
            _mutex.Dispose();
        }
    }
}