using Newtonsoft.Json.Linq;

namespace BoBo.JSON;

public interface IDump
{
    JToken MakeDump(Exception exception);
}
