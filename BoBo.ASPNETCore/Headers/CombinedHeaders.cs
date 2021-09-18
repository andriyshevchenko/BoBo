using System.Collections.Generic;
using System.Linq;

namespace BoBo.ASPNETCore.Middleware;

public class CombinedHeaders : IHeaders
{
    private readonly List<IHeaders> hooks;

    public CombinedHeaders(params IHeaders[] hooks) : this(new List<IHeaders>(hooks))
    {
    }

    public CombinedHeaders(List<IHeaders> hooks)
    {
        this.hooks = hooks;
    }

    public List<KeyValuePair<string, string>> Make()
    {
        return hooks
            .SelectMany(x => x.Make())
            .ToList();
    }
}
