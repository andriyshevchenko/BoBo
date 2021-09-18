using System.Xml;

namespace BoBo.Formatting.XML;

public interface IDump
{
    XmlNode MakeDump(Exception exception, XmlDocument root);
}