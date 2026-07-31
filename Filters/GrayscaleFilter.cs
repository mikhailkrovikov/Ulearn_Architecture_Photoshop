using MyPhotoshop.Filters;

namespace MyPhotoshop;

public class GrayscaleFilter : PixelFilter
{
    public GrayscaleFilter() : base(new EmptyParameters())
    {
        
    }

    public override string ToString()
    {
        return "Оттенки серого";
    }

    public override Pixel ProcessPixel(Pixel original, IParameters parameters)
    {
        var lightness = (original.R + original.G + original.B) / 3;
        return new Pixel(lightness, lightness, lightness);
    }
}
