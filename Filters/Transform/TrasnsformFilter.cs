using System.Drawing;

namespace MyPhotoshop.Filters.Transform
{
    public class TrasnsformFilter : TrasnsformFilter<EmptyParameters>
    {
        public TrasnsformFilter(string name, Func<Size, Size> sizeTransformer, Func<Point, Size, Point> pointTransformer) :
            base(name, new FreeTransformer(sizeTransformer, pointTransformer)) 
            {
            }

    }
}
