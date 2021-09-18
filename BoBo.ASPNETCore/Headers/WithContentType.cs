using System.Collections.Generic;

namespace BoBo.ASPNETCore.Middleware;

public class WithContentType : IHeaders
{
    private readonly string type;

    public WithContentType(string type)
    {
        this.type = type;
    }

    public List<KeyValuePair<string, string>> Make()
    {
        return new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("Content-Type", type)
        };
    }
}
