using Microsoft.UI.Xaml;
using Microsoft.Maui.Handlers;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using WinRT.Interop;

namespace DoryRiggerMaui.WinUI;

public partial class App : MauiWinUIApplication
{
    private MauiWinUIWindow _mianWindow;
    private AppWindow _appWindow;
    public App()
    {
        this.InitializeComponent();

        WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (h, v) =>
        {
            _mianWindow = h.PlatformView as MauiWinUIWindow;

            ////隐藏默认标题栏，允许应用插入新的标题栏
            //_mianWindow.ExtendsContentIntoTitleBar = true;

            _mianWindow.ExtendsContentIntoTitleBar = true;

            //_mianWindow.SetTitleBar(AppTitleBar);

            SetTitleBar(h);
        });
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        //var currentWindow = Application.Windows[0].Handler.PlatformView;
        //IntPtr _windowHandle = WindowNative.GetWindowHandle(currentWindow);
        //var windowId = Win32Interop.GetWindowIdFromWindow(_windowHandle);

        //AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
        //appWindow.Resize(new SizeInt32(1920, 1080));
    }

    public bool SetTitleBar(IWindowHandler handler)
    {
        var nativeWindow = handler.PlatformView;
        nativeWindow.Activate();

        //var appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow)));
        //appWindow.TitleBar.ButtonBackgroundColor = appWindow.TitleBar.BackgroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);
        //appWindow.TitleBar.ForegroundColor = appWindow.TitleBar.ButtonHoverForegroundColor = appWindow.TitleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(255, 255, 255, 255);
        //appWindow.Title = "OBJECT:SOCIAL";
        //appWindow.Changed += (s, a) =>
        //{
        //    if (s.ClientSize.Height < 700 || s.ClientSize.Width < 900)
        //    {
        //        s.Resize(new SizeInt32(900, 700));
        //        return;
        //    }
        //};
        //appWindow.TitleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;

        if (!AppWindowTitleBar.IsCustomizationSupported())
        {
            return false;
        }

        if (_appWindow is null)
        {
            var windowId = Win32Interop.GetWindowIdFromWindow(_mianWindow.WindowHandle);
            _appWindow = AppWindow.GetFromWindowId(windowId);
        }

        if(_appWindow is null) 
            return false;

        _mianWindow.ExtendsContentIntoTitleBar = false;

        var titleBar = _appWindow.TitleBar;

        titleBar.ExtendsContentIntoTitleBar = true;

        titleBar.ButtonBackgroundColor  = Windows.UI.Color.FromArgb(0, 0, 0, 0);
        titleBar.ForegroundColor  = Windows.UI.Color.FromArgb(255, 255, 255, 255);
        titleBar.BackgroundColor = Microsoft.UI.Colors.Blue;
        titleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;


        return true;
    }


}
