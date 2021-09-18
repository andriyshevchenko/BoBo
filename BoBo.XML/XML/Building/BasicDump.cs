using BoBo.Formatting.Text;
using System.Xml;

namespace BoBo.Formatting.XML;

public class BasicDump : IDump
{
    private readonly string elementName;

    public BasicDump(string elementName)
    {
        this.elementName = elementName;
    }

    public XmlNode MakeDump(Exception exception, XmlDocument root)
    {
        var node = root.CreateElement(elementName);
        node.InnerText = new SimpleDump(exception).ToString();
        return node;
    }
}
