using BoBo.Formatting;
using System.Text;

namespace BoBo.JSON;

public class JsonDigest : IDigest
{
    private readonly Exception exception;
    private readonly IFootprint algorithm;

    public JsonDigest(Exception exception, IFootprint algorithm)
    {
        this.exception = exception;
        this.algorithm = algorithm;
    }

    public async Task WriteTo(Stream stream)
    {
        var jToken = algorithm.MakeFootprint(exception);
        var text = jToken.ToString();
        var bytes = Encoding.UTF8.GetBytes(text);
        await stream.WriteAsync(bytes, CancellationToken.None);
    }
}