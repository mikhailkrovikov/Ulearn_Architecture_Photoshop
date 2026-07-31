namespace MyPhotoshop.Filters
{
    public abstract class ParametrizedFilter : IFilter
    {
        IParameters parameters;
        protected ParametrizedFilter(IParameters parameters)
        {
            this.parameters = parameters;
        }
        public ParameterInfo[] GetParameters()
        {
            return parameters.GetDescription();
        }

        public Photo Process(Photo original, double[] parameters)
        {
            this.parameters.SetValues(parameters);
            return Process(original, this.parameters);
        }

        public abstract Photo Process(Photo original, IParameters parameters);
    }
}
