using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MyPhotoshop.Filters;

namespace MyPhotoshop.UI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = new MainWindow();

            mainWindow.AddFilter(new PixelFilter<LighteningParameters>(
                "Осветление/Затемнение",
                (pixel, parameters) =>
                {
                    return pixel * parameters.Coefficient;
                }));

            mainWindow.AddFilter(new PixelFilter<EmptyParameters>(
                "Оттенки серого",
                (pixel, parameters) =>
                {
                    var lightness = (pixel.R + pixel.G + pixel.B) / 3;
                    return new Pixel(lightness, lightness, lightness);
                }));

            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}