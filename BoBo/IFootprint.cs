using Newtonsoft.Json.Linq;

namespace BoBo
{
    public interface IFootprint
    {
        JToken MakeFootprint();
        IFootprint MakeCopy(Exception exception);
    }
}
