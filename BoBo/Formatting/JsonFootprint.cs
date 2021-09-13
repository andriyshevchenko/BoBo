using Newtonsoft.Json.Linq;

namespace BoBo.Formatting;

public class JsonFootprint : IFootprint
{
    private readonly Exception exception;

    public JsonFootprint(Exception exception)
    {
        this.exception = exception;
    }

    public JToken MakeFootprint()
    {
        JObject root = new()
        {
            { "Footprint", new BasicFootprint(exception).MakeFootprint() },
            { "Message", exception.Message },
        };
        JObject currentRoot = root;
        Exception current = exception;
        while (current.InnerException != null)
        {
            current = current.InnerException;
            JObject innerException = new()
            {
                { "Footprint", new BasicFootprint(current).MakeFootprint() },
                { "Message", current.Message }
            };
            currentRoot.Add("InnerException", innerException);
            currentRoot = innerException;
        }
        return root;
    }
}
