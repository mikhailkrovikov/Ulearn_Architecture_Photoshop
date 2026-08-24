namespace MyPhotoshop.Filters.Parameters
{
    public interface IParametersHandler<TParameters>
    {
        ParameterInfo[] GetDescription();
        TParameters CreateParameters(double[] values);
    }
}
