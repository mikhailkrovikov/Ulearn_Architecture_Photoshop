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

            mainWindow.AddFilter(new TrasnsformFilter(
                "Отразить по горизонтиали",
                size => size,
                (point, size) => new System.Drawing.Point(size.Width - point.X - 1, point.Y)
                ));

            mainWindow.AddFilter(new TrasnsformFilter(
                "Против часовой",
                size => new System.Drawing.Size(size.Height, size.Width),
                (point, size) => new System.Drawing.Point(size.Width - point.Y - 1, point.X)
                ));

            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}