using Newtonsoft.Json.Linq;

namespace BoBo.Formatting;

public class JsonDetails : IFootprint
{
    private readonly Exception exception;
    private readonly IFootprint describe;

    public JsonDetails(Exception exception, IFootprint describe)
    {
        this.exception = exception;
        this.describe = describe;
    }

    public IFootprint MakeCopy(Exception exception)
    {
        return this;
    }

    public JToken MakeFootprint()
    {
        JObject root = new()
        {
            { "Footprint", describe
                .MakeCopy(exception)
                .MakeFootprint() },
            { "Message", exception.Message },
        };
        JObject currentRoot = root;
        Exception current = exception;
        while (current.InnerException != null)
        {
            current = current.InnerException;
            JObject innerException = new()
            {
                { "Footprint", describe
                    .MakeCopy(current)
                    .MakeFootprint() },
                { "Message", current.Message }
            };
            currentRoot.Add("InnerException", innerException);
            currentRoot = innerException;
        }
        return root;
    }
}
