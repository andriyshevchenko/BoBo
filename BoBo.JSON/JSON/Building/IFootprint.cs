using Newtonsoft.Json.Linq;

namespace BoBo.JSON;

public interface IFootprint
{
    JToken MakeFootprint(Exception exception);
}
