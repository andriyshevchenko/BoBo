using BoBo.Enumerable;
using BoBo.Formatting.Text;
using System.Xml;

namespace BoBo.Formatting.XML;

public class RecursiveDump : IDump
{
    private readonly string elementName;
    private readonly IDump algorithm;

    public RecursiveDump(string elementName, IDump algorithm)
    {
        this.elementName = elementName;
        this.algorithm = algorithm;
    }

    public RecursiveDump(IDump algorithm) : this("Exception", algorithm)
    {
    }

    public XmlNode MakeDump(Exception exception, XmlDocument document)
    {
        var root = document.CreateElement(elementName);
        var message = document.CreateElement("Message");
        message.InnerText = exception.Message;
        root.AppendChild(message);
        var footprint = algorithm.MakeDump(exception, document);
        root.AppendChild(footprint);
        XmlNode currentRoot = root;
        foreach (var current in new InnerExceptionsOf(exception))
        {
            var innerException = document.CreateElement("InnerException");
            var innerMessage = document.CreateElement("Message");
            innerMessage.InnerText = current.Message;
            footprint = algorithm.MakeDump(current, document);
            innerException.AppendChild(innerMessage);
            innerException.AppendChild(footprint);
            currentRoot.AppendChild(innerException);
            currentRoot = innerException;
        }
        return root;
    }
}
