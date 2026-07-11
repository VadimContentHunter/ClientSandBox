using ClientSandBox.Services;

namespace ClientSandBox
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();

            AppLogger.Initialize();
            SettingsService.Load();

            Application.Run(new MainForm());
        }
    }
}