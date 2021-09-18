using Newtonsoft.Json.Linq;

namespace BoBo.Formatting.JSON;

public interface IDump
{
    JToken MakeDump(Exception exception);
}
