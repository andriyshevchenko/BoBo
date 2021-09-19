using BoBo.Formatting.Enumerable;
using BoBo.Formatting.Text;
using Newtonsoft.Json.Linq;

namespace BoBo.Formatting.JSON;

public class RecursiveDump : IDump
{
    private readonly IDump algorithm;

    public RecursiveDump(IDump algorithm)
    {
        this.algorithm = algorithm;
    }

    public JToken MakeDump(Exception exception)
    {
        JObject root = new()
        {
            { "Footprint", algorithm.MakeDump(exception) },
            { "Message", exception.Message },
            { "Timestamp", new Ticks().ToString() }
        };
        JObject currentRoot = root;
        foreach (var current in new InnerExceptionsOf(exception))
        {
            JObject innerException = new()
            {
                { "Footprint", algorithm.MakeDump(exception) },
                { "Message", current.Message },
            };
            currentRoot.Add("InnerException", innerException);
            currentRoot = innerException;
        }
        return root;
    }
}
