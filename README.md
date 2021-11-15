# Portable WebView2 Kiosk with Tabs
minimal kiosk mode app for webview2 with tabs

![demo](https://github.com/gritplex/tabbed-kiosk-webView2/blob/main/media/demo.gif)

Features:
- Configure tabs in settings.json
- Tabs are added in the order they are set in the config file
- Choose screen on which the kiosk gets opened (first, last or specific screen)
- Openening the application multiple times will bring the already running app into the foreground


Based on x64 runtime of WebView2.
See [https://developer.microsoft.com/en-us/microsoft-edge/webview2/](https://developer.microsoft.com/en-us/microsoft-edge/webview2/).

If you have MS Edge not installed and cannot install it, you need to provide a FixedVersionRuntime from the link above.
The path to this runtime needs to be set in the settings.json.

The v0.1 release includes a runtime already downloaded.

If you are starting this application by using the cmd line or another application and the settings file cannot be found, try adding the full path to the settings file as a cmd line parameter to the TabbedKioskPortable.exe

MIT License, use at your own risk.

**Q: How do i close the app?**

A: Alt+F4
