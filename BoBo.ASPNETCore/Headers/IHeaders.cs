using System.Collections.Generic;

namespace BoBo.ASPNETCore.Middleware;

/// <summary>
/// Represents a factory for HTTP headers that will be added to the response.
/// </summary>
public interface IHeaders
{
    /// <summary>
    /// Builds the collection of headers.
    /// </summary>
    /// <returns>A list of header name and value pairs.</returns>
    List<KeyValuePair<string, string>> Make();
}
