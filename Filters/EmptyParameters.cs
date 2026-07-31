namespace MyPhotoshop.Filters
{
    public class EmptyParameters : IParameters
    {
        public ParameterInfo[] GetDescription()
        {
            return [];
        }

        public void SetValues(double[] values)
        {

        }
    }
}
