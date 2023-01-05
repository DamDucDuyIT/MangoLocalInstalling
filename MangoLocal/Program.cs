using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MangoLocal
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        public static IConfiguration Configuration;
        [STAThread]
        static void Main()
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            try
            {
                ExecutePowerShellCommand();
            }
            catch (Exception)
            {
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        static void ExecutePowerShellCommand()
        {
            string _Script_Path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "aaa.ps1") + "\\";
            var startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = "\"& \"\"" + _Script_Path + "\"\"\"",
                UseShellExecute = true,
                Verb = "runas"
            };
            Process.Start(startInfo);
        }
    }
}
