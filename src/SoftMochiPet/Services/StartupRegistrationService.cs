using System.IO;
using Microsoft.Win32;

namespace SoftMochiPet.Services;

public sealed class StartupRegistrationService
{
    public const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
    public const string ValueName = "SoftMochiPet";

    private readonly string _launchPath;

    public StartupRegistrationService(string? launchPath = null)
    {
        _launchPath = Path.GetFullPath(
            launchPath ?? ResolveLaunchPath(AppContext.BaseDirectory, Environment.ProcessPath));
    }

    public bool IsEnabled()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, writable: false);
        var command = key?.GetValue(ValueName) as string;
        return string.Equals(command, BuildRunCommand(_launchPath), StringComparison.OrdinalIgnoreCase);
    }

    public void SetEnabled(bool enabled)
    {
        if (enabled)
        {
            using var key = Registry.CurrentUser.CreateSubKey(RunKeyPath, writable: true)
                ?? throw new InvalidOperationException("Could not open the current user's Windows startup key.");
            key.SetValue(ValueName, BuildRunCommand(_launchPath), RegistryValueKind.String);
            return;
        }

        using var existingKey = Registry.CurrentUser.OpenSubKey(RunKeyPath, writable: true);
        existingKey?.DeleteValue(ValueName, throwOnMissingValue: false);
    }

    public static string ResolveLaunchPath(string baseDirectory, string? processPath)
    {
        var appDirectory = new DirectoryInfo(Path.GetFullPath(baseDirectory));
        if (appDirectory.Parent is not null)
        {
            var portableLauncher = Path.Combine(appDirectory.Parent.FullName, "啾糯桌宠.exe");
            if (File.Exists(portableLauncher))
            {
                return portableLauncher;
            }
        }

        if (!string.IsNullOrWhiteSpace(processPath))
        {
            return Path.GetFullPath(processPath);
        }

        throw new InvalidOperationException("Could not determine the launcher path for JiuNuo Desktop Pet.");
    }

    public static string BuildRunCommand(string launchPath) => $"\"{Path.GetFullPath(launchPath)}\"";
}
