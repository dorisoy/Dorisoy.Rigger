using Microsoft.Maui.Handlers;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace DoryRiggerMaui.WinUI;

public partial class App : MauiWinUIApplication
{
    private MauiWinUIWindow mianWindow;
    private AppWindow appWindow;
    public App()
    {
        this.InitializeComponent();

        WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (h, v) =>
        {
            mianWindow = h.PlatformView as MauiWinUIWindow;

            //////隐藏默认标题栏，允许应用插入新的标题栏
            //mianWindow.ExtendsContentIntoTitleBar = false;

            var nativeWindow = h.PlatformView;
            nativeWindow.Activate();

            var hWnd = WindowNative.GetWindowHandle(nativeWindow);
            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            appWindow = AppWindow.GetFromWindowId(windowId);
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
}
