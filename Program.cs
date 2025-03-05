using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace HTTPInterceptor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // 若未以管理員權限執行，則重啟程式以管理員身份執行
            if (!IsRunAsAdmin())
            {
                ProcessStartInfo proc = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Application.ExecutablePath,
                    Verb = "runas"
                };

                try
                {
                    Process.Start(proc);
                }
                catch
                {
                    // 使用者拒絕管理員權限時，直接離開
                    return;
                }
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
