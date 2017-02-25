using System.Data.Common;

namespace Powell
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public abstract class DataBase<TParameter>
        where TParameter : DbParameter
    {
        protected abstract TParameter CreateParameter<T>(string parameterName, T value);

        protected abstract TParameter CreateParameter<T>(string parameterName, T value, int size);
    }
}
