using MyPhotoshop.Filters;
using MyPhotoshop.Filters.Parameters;
using System.Linq.Expressions;
using System.Reflection;
using ParameterInfo = MyPhotoshop.Filters.Parameters.ParameterInfo;

namespace MyPhotoshop
{
    public class ExpressionParametersHandler<TParameteres> : IParametersHandler<TParameteres> where TParameteres : IParameters, new()
    {
        private static Func<double[], TParameteres> parser;
        private static ParameterInfo[] descriptions;

        static ExpressionParametersHandler()
        {
            descriptions = typeof(TParameteres)
               .GetType()
               .GetProperties()
               .Select(z => z.GetCustomAttributes(typeof(ParameterInfo), false))
               .Where(z => z.Length > 0)
               .Select(z => z[0])
               .Cast<ParameterInfo>()
               .ToArray();

            // values => new LighteningParameters { Coefficient = values[0] };
            var properties = typeof(TParameteres)
                 .GetProperties()
                 .Where(z => z.GetCustomAttributes(typeof(ParameterInfo), false).Length != 0)
                 .ToArray();

            var arg = Expression.Parameter(typeof(double[]), "values");

            var bindings = new List<MemberBinding>();
            for (int i = 0; i < properties.Length; i++)
            {
                var binding = Expression.Bind(
                    properties[i],
                    Expression.ArrayIndex(arg, Expression.Constant(i))
                );
                bindings.Add(binding);
            }

            var body = Expression.MemberInit(Expression.New(typeof(TParameteres).GetConstructor(new Type[0])), bindings);
            var lambda = Expression.Lambda<Func<double[], TParameteres>>(body, arg);

            parser = lambda.Compile();
        }

        public TParameteres CreateParameters(double[] values)
        {
            return parser(values);
        }

        public ParameterInfo[] GetDescription()
        {
            return descriptions;
        }
    }
}
