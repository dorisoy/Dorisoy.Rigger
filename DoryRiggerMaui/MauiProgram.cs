using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Maui.LifecycleEvents;
using MudBlazor.Services;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Xaml;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif


namespace DoryRiggerMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddMudServices();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif


        //builder.ConfigureLifecycleEvents(lifecycle =>
        //{


        //#if WINDOWS
        //    lifecycle
        //        .AddWindows (windows => {
        //            _ = windows.OnWindowCreated ((w) => 
        //            {
        //                w.ExtendsContentIntoTitleBar = true;

        //                var window = w as Microsoft.Maui.MauiWinUIWindow;
        //                if (window is null)
        //                    return;
        //            });

        //        });
        //#endif


        //#if WINDOWS
        //         //lifecycle
        //         //    .AddWindows(windows =>
        //         //        windows.OnNativeMessage((app, args) => {
        //         //            if (WindowExtensions.Hwnd == IntPtr.Zero)
        //         //            {
        //         //                WindowExtensions.Hwnd = args.Hwnd;
        //         //                WindowExtensions.SetIcon("Platforms/Windows/trayicon.ico");
        //         //            }
        //         //        }));

        //             lifecycle.AddWindows(windows => windows.OnWindowCreated((del) => {
        //                 del.ExtendsContentIntoTitleBar = true;
        //             }));
        //#endif

        //        });



#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                    AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);    
                    if(winuiAppWindow.Presenter is OverlappedPresenter p)
                    { 
                       //p.Maximize();
                       //p.IsAlwaysOnTop=true;
                       //初始最小化
                       p.Minimize();
                       p.IsResizable=false;
                       p.IsMaximizable = false;
                       p.IsMinimizable=false;
                    }                     
                    else
                    {
                        const int width = 1024;
                        const int height = 768;
                        winuiAppWindow.MoveAndResize(new RectInt32(1024 / 2 - width / 2, 768 / 2 - height / 2, width, height));                      
                    }                        
                });
            });
        });
#endif

        return builder.Build();
    }
}

