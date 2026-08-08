using System.Drawing;

namespace MyPhotoshop.Filters.Transform
{
    public class FreeTransformer : ITransformer<EmptyParameters>
    {
        private readonly Func<Size, Size> sizeTransformer;
        private readonly Func<Point, Size, Point> pointTransformer;

        public FreeTransformer(Func<Size, Size> sizeTransformer, Func<Point, Size, Point> pointTransformer)
        {
            this.sizeTransformer = sizeTransformer;
            this.pointTransformer = pointTransformer;
        }

        Size oldSize;
        public Size ResultSize { get; private set; }

        public void Prepare(Size size, EmptyParameters parameters)
        {
            oldSize = size;
            ResultSize = sizeTransformer(oldSize);
        }

        public Point? MapPoint(Point point)
        {
            return pointTransformer(point, oldSize);
        }


    }
}
