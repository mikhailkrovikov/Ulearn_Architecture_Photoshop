namespace MyPhotoshop.Filters.Parameters
{
    public abstract class ParametrizedFilter<TParameters> : IFilter
        where TParameters : IParameters, new()
    {

        public ParameterInfo[] GetParameters()
        {
            return new TParameters().GetDescription();
        }

        public Photo Process(Photo original, double[] parameters)
        {
            var t = new TParameters();
            t.SetValues(parameters);
            return Process(original, t);
        }

        public abstract Photo Process(Photo original, TParameters parameters);
    }
}
