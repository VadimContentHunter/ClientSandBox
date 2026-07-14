using System.Runtime.InteropServices;

namespace ClientSandBox.Services.SystemProxy;

/// <summary>
/// Импортируемые функции WinAPI.
///
/// Используются для вызова функций Windows,
/// отсутствующих в .NET.
/// </summary>
internal static class NativeMethods
{
    [DllImport("wininet.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool InternetSetOption(
        IntPtr hInternet,
        InternetOptions dwOption,
        IntPtr lpBuffer,
        int dwBufferLength);
}