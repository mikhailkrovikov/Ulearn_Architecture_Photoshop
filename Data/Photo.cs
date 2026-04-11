namespace MyPhotoshop;

public class Photo
{
    public readonly int Width;
    public readonly int Height;
    public readonly Pixel[,] Data;

    public Photo(int width, int height)
    {
        Width = width;
        Height = height;
        Data = new Pixel[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                Data[i, j] = new Pixel();
    }
}
