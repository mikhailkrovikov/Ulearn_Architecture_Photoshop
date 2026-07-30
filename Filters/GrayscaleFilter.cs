namespace MyPhotoshop;

public class GrayscaleFilter : PixelFilter
{
    public override ParameterInfo[] GetParameters()
    {
        return [];
    }

    public override string ToString()
    {
        return "Оттенки серого";
    }

    public override Pixel ProcessPixel(Pixel original, double[] parameters)
    {
        var lightness = (original.R + original.G + original.B) / 3;
        return new Pixel(lightness, lightness, lightness);
    }
}
