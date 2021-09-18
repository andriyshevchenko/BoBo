using System.Collections.Generic;

namespace BoBo.ASPNETCore.Middleware;

public interface IHeaders
{
    List<KeyValuePair<string, string>> Make();
}
