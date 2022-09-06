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
using DoryRiggerMaui.WinUI;
using SharedUI.Models;
#elif MACCATALYST
using SharedUI.Models;
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

#if WINDOWS

        builder.Services.AddTransient<SharedUI.Models.IFolderPicker, DoryRiggerMaui.Windows.FolderPicker>();

        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    window.ExtendsContentIntoTitleBar = true; 

                    window.SetTitleBar(new CustomTitleBar());

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

                       //p.SetBorderAndTitleBar(false, false);
                    }                     
                    else
                    {
                        const int width = 1200;
                        const int height = 800;
                        winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));                      
                    }                        
                });
            });
        });

#elif MACCATALYST
		builder.Services.AddTransient<SharedUI.Models.IFolderPicker, DoryRiggerMaui.MacCatalyst.FolderPicker>();
#endif
        return builder.Build();
    }
}

