using Microsoft.Maui.Controls.PlatformConfiguration;

namespace DoryRiggerMaui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new MainPage();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);
        window.Title = "";
        return window;
    }

}

