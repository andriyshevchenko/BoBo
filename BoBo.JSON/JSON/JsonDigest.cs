using BoBo.Formatting;
using BoBo.Formatting.JSON;
using System.Text;

namespace BoBo.Formatting.JSON;

public class JsonDigest : IDigest
{
    private readonly IDump algorithm;

    public JsonDigest(IDump algorithm)
    {
        this.algorithm = algorithm;
    }

    public async Task Write(Exception exception, Stream stream)
    {
        var jToken = algorithm.MakeDump(exception);
        var text = jToken.ToString();
        var bytes = Encoding.UTF8.GetBytes(text);
        await stream.WriteAsync(bytes, CancellationToken.None);
    }
}