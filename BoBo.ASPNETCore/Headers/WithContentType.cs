using System.Collections.Generic;

namespace BoBo.ASPNETCore.Middleware;

/// <summary>
/// Produces a <c>Content-Type</c> header with a specified value.
/// </summary>
public class WithContentType : IHeaders
{
    private readonly string type;

    /// <summary>
    /// Initializes a new instance of the <see cref="WithContentType"/> class.
    /// </summary>
    /// <param name="type">Value of the <c>Content-Type</c> header.</param>
    public WithContentType(string type)
    {
        this.type = type;
    }

    /// <summary>
    /// Creates the header collection containing the <c>Content-Type</c> entry.
    /// </summary>
    /// <returns>A list with a single header pair.</returns>
    public List<KeyValuePair<string, string>> Make()
    {
        return new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("Content-Type", type)
        };
    }
}
