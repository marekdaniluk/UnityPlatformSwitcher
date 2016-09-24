using UnityEditor;
using System.Diagnostics;
using System.IO;

public class PlatformSwitcher {

    [MenuItem("Platform Switcher/Switch Standalone")]
    public static void SwitchStandalone() {
        SwitchPlatformUnix(BuildTarget.StandaloneOSXIntel);
    }

    [MenuItem("Platform Switcher/Switch iOS")]
    public static void SwitchIOS() {
        SwitchPlatformUnix(BuildTarget.iOS);
    }

    [MenuItem("Platform Switcher/Switch Android")]
    public static void SwitchAndroid() {
        SwitchPlatformUnix(BuildTarget.Android);
    }

    #region UNIX_METHODS

    private static void SwitchPlatformUnix(BuildTarget target) {
        if (Directory.Exists(target.ToString())) {
            RemoveLibrarySymbolicLinkUnix();
        } else {
            CreateDirectoryUnix(target.ToString());
        }
        Process process = new Process();
        process.StartInfo.FileName = "ln";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.Arguments = string.Format("-s {0}/ Library", target.ToString());
        process.Start();

        process.WaitForExit();
        process.Close();
        EditorUserBuildSettings.SwitchActiveBuildTarget(target);
    }

    private static void RemoveLibrarySymbolicLinkUnix() {
        Process process = new Process();
        process.StartInfo.FileName = "rm";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.Arguments = "-rf Library";
        process.Start();

        process.WaitForExit();
        process.Close();
    }

    private static void CreateDirectoryUnix(string dirname) {
        Process process = new Process();
        process.StartInfo.FileName = "mkdir";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.Arguments = dirname.ToString();
        process.Start();

        process.WaitForExit();
        process.Close();
    }

#endregion
}
