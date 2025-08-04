using System.Collections.Generic;
using System.Linq;

namespace BoBo.ASPNETCore.Middleware;

/// <summary>
/// Combines multiple <see cref="IHeaders"/> instances into a single collection.
/// </summary>
public class CombinedHeaders : IHeaders
{
    private readonly List<IHeaders> hooks;

    /// <summary>
    /// Initializes a new instance of the <see cref="CombinedHeaders"/> class.
    /// </summary>
    /// <param name="hooks">Headers providers to combine.</param>
    public CombinedHeaders(params IHeaders[] hooks) : this(new List<IHeaders>(hooks))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CombinedHeaders"/> class.
    /// </summary>
    /// <param name="hooks">Headers providers to combine.</param>
    public CombinedHeaders(List<IHeaders> hooks)
    {
        this.hooks = hooks;
    }

    /// <summary>
    /// Builds a combined list of headers from all wrapped providers.
    /// </summary>
    /// <returns>The aggregated list of headers.</returns>
    public List<KeyValuePair<string, string>> Make()
    {
        return hooks
            .SelectMany(x => x.Make())
            .ToList();
    }
}
