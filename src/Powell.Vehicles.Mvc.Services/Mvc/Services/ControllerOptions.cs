using System.Collections.Generic;

namespace Powell.Vehicles.Mvc.Services
{
    public class ControllerOptions<TMessageKey> : IControllerOptions<TMessageKey>
    {
        public IDictionary<TMessageKey, string> Messages { get; internal set; }

        public string GetMessage(TMessageKey key)
        {
            return Messages.ContainsKey(key) ? Messages[key] : string.Empty;
        }
    }
}
