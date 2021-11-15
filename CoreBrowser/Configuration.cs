using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBrowser
{
    public class Configuration
    {
        public string? BrowserExecutableFolder { get; set; }
        public Tab[] Tabs { get; set; } = new Tab[0];
        public DisplayOpenMode OpenOnDisplay { get; set; } = DisplayOpenMode.Last;
        // if OpenOnDisplay is set to "Id" this is used to determine the display
        // count the displays from left to right, starting at 1
        public int? DisplayId { get; set; } = null;
    }

    public class Tab
    {
        public string Name { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
    }

    public enum DisplayOpenMode
    {
        First,
        Last,
        Id
    }
}
