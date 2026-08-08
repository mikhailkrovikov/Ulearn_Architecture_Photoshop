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
}
