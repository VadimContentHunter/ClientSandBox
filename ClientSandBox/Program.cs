using ClientSandBox.Forms;
using ClientSandBox.Services;
using System.Text;

namespace ClientSandBox;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        ApplicationConfiguration.Initialize();

        AppLogger.Initialize();
        SettingsService.Load();

        Application.Run(new MainForm());
    }
}