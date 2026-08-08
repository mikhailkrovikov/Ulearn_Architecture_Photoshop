using System.Drawing;

namespace MyPhotoshop.Filters.Transform
{
    public class RotationParameters : IParameters
    {
        public double Angle { get; set; }
        public ParameterInfo[] GetDescription()
        {
            return new[]
            {
                new ParameterInfo
                {
                    Name = "Угол",
                    MaxValue = 360,
                    MinValue = 0,
                    Increment = 5,
                    DefaultValue = 0
                }
            };
        }

        public void SetValues(double[] values)
        {
            Angle = values[0];
        }
    }

    public interface ITransformer<TParameters> where TParameters : IParameters, new()
    {
        void Prepare(Size size, TParameters parameters);
        Size ResultSize { get; }
        Point? MapPoint(Point point);
    }

    public class RotateTransformer : ITransformer<RotationParameters>
    {

        public void Prepare(Size size, RotationParameters parameters)
        {
            OriginalSize = size;
            Angle = Math.PI * parameters.Angle / 180;
            ResultSize = new Size(
                (int)(size.Width * Math.Abs(Math.Cos(Angle)) + size.Height * Math.Abs(Math.Sin(Angle))),
                (int)(size.Height * Math.Abs(Math.Cos(Angle)) + size.Width * Math.Abs(Math.Sin(Angle))));
        }

        public Size OriginalSize { get; private set; }

        public Size ResultSize { get; private set; }

        public double Angle { get; private set; }   

        public Point? MapPoint(Point point)
        {
            var newSize = ResultSize;
            var angle = Angle;
            var oldSize = OriginalSize;
            point = new Point(point.X - newSize.Width / 2, point.Y - newSize.Height / 2);
            var x = oldSize.Width / 2 + (int)(point.X * Math.Cos(angle) + point.Y * Math.Sin(angle));
            var y = oldSize.Height / 2 + (int)(-point.X * Math.Sin(angle) + point.Y * Math.Cos(angle));
            if (x < 0 || x >= oldSize.Width || y < 0 || y >= oldSize.Height) return null;
            return new Point(x, y);
        }
    }
}
