using System.Drawing;

namespace MyPhotoshop.Filters.Transform
{
    public class TrasnsformFilter<TParameters> : ParametrizedFilter<TParameters> where TParameters : IParameters, new()
    {
        private readonly ITransformer<TParameters> _transformer;
        private readonly string _name;

        public TrasnsformFilter(string name, ITransformer<TParameters> transformer)
        {
            _name = name;
            _transformer = transformer;
        }

        public override string ToString()
        {
            return _name;
        }

        public override Photo Process(Photo original, TParameters parameters)
        {
            var oldSize = new Size(original.Width, original.Height);
            _transformer.Prepare(oldSize, parameters);
            var result = new Photo(_transformer.ResultSize.Width, _transformer.ResultSize.Height);
            for (int x = 0; x < result.Width; x++)
                for (int y = 0; y < result.Height; y++)
                {
                    var point = new Point(x, y);
                    var oldPoint = _transformer.MapPoint(point);
                    if (oldPoint.HasValue)
                        result[x, y] = original[oldPoint.Value.X, oldPoint.Value.Y];
                }
            return result;
        }
    }
}
