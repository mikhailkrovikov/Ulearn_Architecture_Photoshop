using MyPhotoshop.Filters.Parameters;

namespace MyPhotoshop
{
    public class SimpleParametersHandler<TParameteres> : IParametersHandler<TParameteres> where TParameteres : IParameters, new()
    {
        public TParameteres CreateParameters(double[] values)
        {
            var parameters = new TParameteres();
            var properties = parameters
                .GetType()
                .GetProperties()
                .Where(z => z.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
                .ToArray();

            for (int i = 0; i < values.Length; i++)
                properties[i].SetValue(parameters, values[i], new object[0]);

            return parameters;
        }

        public ParameterInfo[] GetDescription()
        {
            return typeof(TParameteres)
               .GetType()
               .GetProperties()
               .Select(z => z.GetCustomAttributes(typeof(ParameterInfo), false))
               .Where(z => z.Length > 0)
               .Select(z => z[0])
               .Cast<ParameterInfo>()
               .ToArray();
        }
    }
}
