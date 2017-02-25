using System.Collections.Generic;

namespace Powell.Vehicles.Mvc.Services
{
    public interface IControllerOptions<TMessageKey>
    {
        IDictionary<TMessageKey, string> Messages { get; }

        string GetMessage(TMessageKey key);
    }
}
