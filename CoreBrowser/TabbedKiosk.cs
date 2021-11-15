using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreBrowser
{
    public partial class TabbedKiosk : Form
    {
        public TabbedKiosk(Configuration configuration)
        {
            InitializeComponent();

            this.MaximizeBox = true;
            this.MinimizeBox = false;
            this.WindowState = FormWindowState.Maximized;
            this.ControlBox = false;
            this.Text = "";

            var sc = GetOpenDisplay(configuration);

            this.StartPosition = FormStartPosition.Manual;
            this.SetDesktopBounds(sc.WorkingArea.X, sc.WorkingArea.Y, sc.WorkingArea.Width, sc.WorkingArea.Height);

            foreach (var tab in configuration.Tabs)
            {
                var web = new WebView2();
                if (!string.IsNullOrWhiteSpace(configuration.BrowserExecutableFolder))
                {
                    web.CreationProperties = new CoreWebView2CreationProperties()
                    {
                        BrowserExecutableFolder = configuration.BrowserExecutableFolder
                    };
                }
                web.Dock = DockStyle.Fill;
                var tabPage = new TabPage();
                tabPage.Controls.Add(web);
                tabPage.Text = tab.Name;
                web.Source = new Uri(tab.Uri);
                tabControl.TabPages.Add(tabPage);
            }
        }

        private Screen GetOpenDisplay(Configuration configuration)
        {
            var allScreens = Screen.AllScreens.OrderBy(x => x.WorkingArea.X);

            switch (configuration.OpenOnDisplay)
            {
                case DisplayOpenMode.First:
                    return allScreens.First();
                case DisplayOpenMode.Last:
                    return allScreens.Last();
                case DisplayOpenMode.Id:

                    if (!configuration.DisplayId.HasValue ||
                        configuration.DisplayId == 0 ||
                        configuration.DisplayId > allScreens.Count())
                    {
                        throw new Exception($"Invalid DisplayId {configuration.DisplayId}");
                    }

                    return allScreens.ElementAt(configuration.DisplayId.Value - 1);
            }

            throw new InvalidOperationException();
        }
    }
}
