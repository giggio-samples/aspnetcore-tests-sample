using System;
using System.Diagnostics;

namespace AcceptanceTests
{
    public class FrontendServer : IDisposable
    {
        private readonly string projectDir;
        private Process process;

        public FrontendServer(string projectDir) => this.projectDir = projectDir;

        public string BaseUrl { get; } = "http://localhost:7200";

        public void Dispose()
        {
            if (process == null)
                return;
            if (!process.HasExited)
                KillWindowsProcess(process.Id);
            process.Dispose();
            process = null;
        }

        public void StartFrontEnd() => process = Process.Start(new ProcessStartInfo("npm", "run start:test") { UseShellExecute = true, WorkingDirectory = projectDir });

        private void KillWindowsProcess(int processId) // todo: multi platform
        {
            using (var killer = Process.Start(new ProcessStartInfo("taskkill.exe", $"/PID {processId} /T /F") { UseShellExecute = false }))
                killer.WaitForExit(2000);
        }

    }
}