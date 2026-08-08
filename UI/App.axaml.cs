using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MyPhotoshop.Filters;
using MyPhotoshop.Filters.Transform;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

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


            Func<Size, RotationParameters, Size> sizeRotator = (size, parameters) =>
            {
                var angle = Math.PI * parameters.Angle / 180;
                return new Size(
                    (int)(size.Width * Math.Abs(Math.Cos(angle)) + size.Height * Math.Abs(Math.Sin(angle))),
                    (int)(size.Height * Math.Abs(Math.Cos(angle)) + size.Width * Math.Abs(Math.Sin(angle))));
            };

            Func<Point, Size, RotationParameters, Point?> pointRotator = (point, oldSize, parameters) =>
            {
                var newSize = sizeRotator(oldSize, parameters);
                var angle = Math.PI * parameters.Angle / 180;
                point = new Point(point.X - newSize.Width / 2, point.Y - newSize.Height / 2);
                var x = oldSize.Width / 2 + (int)(point.X * Math.Cos(angle) + point.Y * Math.Sin(angle));
                var y = oldSize.Height / 2 + (int)(-point.X * Math.Sin(angle) + point.Y * Math.Cos(angle));
                if (x < 0 || x >= oldSize.Width || y < 0 || y >= oldSize.Height) return null;
                return new Point(x, y);
            };

            mainWindow.AddFilter(new TrasnsformFilter<RotationParameters>(
                "Свободное вращение",
                sizeRotator,
                pointRotator));

            //mainWindow.AddFilter(new TrasnsformFilter(
            //    "Отразить по горизонтиали",
            //    size => size,
            //    (point, size) => new System.Drawing.Point(size.Width - point.X - 1, point.Y)
            //    ));

            //mainWindow.AddFilter(new TrasnsformFilter(
            //    "Против часовой",
            //    size => new System.Drawing.Size(size.Height, size.Width),
            //    (point, size) => new System.Drawing.Point(size.Width - point.Y - 1, point.X)
            //    ));

            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}