using System.Drawing;

namespace MyPhotoshop.Filters
{
    public class TrasnsformFilter : ParametrizedFilter<EmptyParameters>
    {
        private readonly Func<Size, Size> _transformSize;
        private readonly Func<Point, Size, Point> _transformPoint;
        private readonly string _name;

        public TrasnsformFilter(string name, Func<Size, Size> transformSize, Func<Point, Size, Point> transformPoint)
        {
            _name = name;
            _transformSize = transformSize;
            _transformPoint = transformPoint;
        }

        public override string ToString()
        {
            return _name;
        }

        public override Photo Process(Photo original, EmptyParameters parameters)
        {
            var oldSize = new Size(original.Width, original.Height);
            var newSize = _transformSize(oldSize);
            var result = new Photo(newSize.Width, newSize.Height);
            for(int x = 0; x < newSize.Width; x++)
                for(int y = 0; y < newSize.Height; y++)
                {
                    var point = new Point(x, y);
                    var oldPoint = _transformPoint(point, oldSize);
                    result[x,y] = original[oldPoint.X, oldPoint.Y];
                }
            return result;
        }
    }
}
