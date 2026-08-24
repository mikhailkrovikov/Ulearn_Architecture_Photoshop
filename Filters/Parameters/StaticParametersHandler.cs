using MyPhotoshop.Filters.Parameters;
using System.Reflection;
using ParameterInfo = MyPhotoshop.Filters.Parameters.ParameterInfo;

namespace MyPhotoshop
{
    public class StaticParametersHandler<TParameteres> : IParametersHandler<TParameteres> where TParameteres : IParameters, new()
    {
        private static PropertyInfo[] properties;
        private static ParameterInfo[] descriptions;

        static StaticParametersHandler()
        {
            properties = typeof(TParameteres)
                .GetProperties()
                .Where(z => z.GetCustomAttributes(typeof(ParameterInfo), false).Length != 0)
                .ToArray();

            descriptions = typeof(TParameteres)
               .GetType()
               .GetProperties()
               .Select(z => z.GetCustomAttributes(typeof(ParameterInfo), false))
               .Where(z => z.Length > 0)
               .Select(z => z[0])
               .Cast<ParameterInfo>()
               .ToArray();
        }

        public TParameteres CreateParameters(double[] values)
        {
            var parameters = new TParameteres();
          
            if(properties.Length!=values.Length)
                throw new ArgumentException();

            for (int i = 0; i < values.Length; i++)
                properties[i].SetValue(parameters, values[i], new object[0]);

            return parameters;
        }

        public ParameterInfo[] GetDescription()
        {
            return descriptions;
        }
    }
}
