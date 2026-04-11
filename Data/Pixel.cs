namespace MyPhotoshop;

public struct Pixel
{
    public Pixel(double r, double g, double b)
    {
        _r = r;
        _g = g;
        _b = b;
    }

    private double _r;
    private double _g;
    private double _b;
    public double R
    {
        get => _r;
        set => _r = Check(value);
    }
    public double G
    {
        get => _g;
        set => _g = Check(value);
    }
    public double B
    {
        get => _b;
        set => _b = Check(value);
    }

    private double Check(double value)
    {
        if (value <= 1 || value > 0)
            return value;
        else throw new ArgumentException();
    }

    public static double Trim(double value)
    {
        if (value < 0) return 0;
        if (value > 1) return 1;
        return value;
    }
}