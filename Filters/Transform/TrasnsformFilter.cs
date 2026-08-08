using System.Drawing;

namespace MyPhotoshop.Filters.Transform
{
    public class TrasnsformFilter<TParameters> : ParametrizedFilter<TParameters> where TParameters : IParameters, new()
    {
        private readonly Func<Size, TParameters, Size> _transformSize;
        private readonly Func<Point, Size, TParameters, Point?> _transformPoint;
        private readonly string _name;

        public TrasnsformFilter(string name, Func<Size, TParameters, Size> transformSize, Func<Point, Size, TParameters, Point?> transformPoint)
        {
            _name = name;
            _transformSize = transformSize;
            _transformPoint = transformPoint;
        }

        public override string ToString()
        {
            return _name;
        }

        public override Photo Process(Photo original, TParameters parameters)
        {
            var oldSize = new Size(original.Width, original.Height);
            var newSize = _transformSize(oldSize, parameters);
            var result = new Photo(newSize.Width, newSize.Height);
            for (int x = 0; x < newSize.Width; x++)
                for (int y = 0; y < newSize.Height; y++)
                {
                    var point = new Point(x, y);
                    var oldPoint = _transformPoint(point, oldSize, parameters);
                    if (oldPoint.HasValue)
                        result[x, y] = original[oldPoint.Value.X, oldPoint.Value.Y];
                }
            return result;
        }
    }
}
