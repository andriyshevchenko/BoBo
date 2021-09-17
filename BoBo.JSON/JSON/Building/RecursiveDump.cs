using Newtonsoft.Json.Linq;

namespace BoBo.JSON;

public class RecursiveDump : IFootprint
{
    private readonly IFootprint body;

    public RecursiveDump(IFootprint body)
    {
        this.body = body;
    }

    public JToken MakeFootprint(Exception exception)
    {
        JObject root = new()
        {
            { "Footprint", body.MakeFootprint(exception) },
            { "Message", exception.Message },
        };
        JObject currentRoot = root;
        Exception current = exception;
        while (current.InnerException != null)
        {
            current = current.InnerException;
            JObject innerException = new()
            {
                { "Footprint", body.MakeFootprint(exception) },
                { "Message", current.Message }
            };
            currentRoot.Add("InnerException", innerException);
            currentRoot = innerException;
        }
        return root;
    }
}
