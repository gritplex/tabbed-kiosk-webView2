using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreBrowser
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var pcs = Process.GetProcessesByName("CoreBrowser");

            if (pcs != null && pcs.Length == 2)
            {
                var other = pcs.Where(x =>
                {
                    return x.Id != Environment.ProcessId;
                }).First();
                SetForegroundWindow(other.MainWindowHandle);
                return;
            }

            string? settingsPath = null;
            if(args.Length > 0)
            {
                settingsPath = args[0];
            }

            var config = GetConfig(settingsPath);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TabbedKiosk(config));
        }

        private static Configuration GetConfig(string? configFilePath = null)
        {
            if (string.IsNullOrWhiteSpace(configFilePath))
            {
                // get current folder
                var executionFolder = Path.GetDirectoryName(
                    (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly())
                    .Location);

                configFilePath = Path.Combine(executionFolder ?? ".", "settings.json");
            }

            if (File.Exists(configFilePath))
            {
                var json = File.ReadAllText(configFilePath);

                var config = JsonSerializer.Deserialize<Configuration>(
                    json,
                    GetJsonOptions());

                if (config == null)
                    throw new Exception("could not read configuration");

                return config;
            }

            throw new FileNotFoundException($"cannot find configuration {configFilePath}");
        }

        private static JsonSerializerOptions? GetJsonOptions()
        {
            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
            };

            options.Converters.Add(new JsonStringEnumConverter());

            return options;
        }


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
