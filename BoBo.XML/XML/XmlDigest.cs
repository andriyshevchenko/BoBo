using System.Text;
using System.Xml;

namespace BoBo.Formatting.XML;

public class XmlDigest : IDigest
{
    private readonly IDump algorithm;

    public XmlDigest(IDump algorithm)
    {
        this.algorithm = algorithm;
    }

    public async Task Write(Exception exception, Stream stream)
    {
        XmlDocument root = new XmlDocument();
        root.AppendChild(algorithm.MakeDump(exception, root));
        using (var xw = new XmlTextWriter(stream, Encoding.UTF8))
        {
            xw.Formatting = System.Xml.Formatting.Indented;
            root.WriteTo(xw);
        }
        await stream.FlushAsync();
    }
}
