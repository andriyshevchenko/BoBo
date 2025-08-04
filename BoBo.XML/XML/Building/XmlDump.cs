using BoBo.Enumerable;
using System.Diagnostics;
using System.Xml;

namespace BoBo.Formatting.XML;

public class XmlDump : IDump
{
    private readonly string elementName;
    private readonly string elementNamePlural;

    public XmlDump(string elementName, string elementNamePlural)
    {
        this.elementName = elementName;
        this.elementNamePlural = elementNamePlural;
    }

    public XmlDump() : this("Frame", "Dump")
    {
    }

    public XmlNode MakeDump(Exception exception, XmlDocument document)
    {
        var root = document.CreateElement(elementNamePlural);
        foreach (var frame in new FramesOf(exception))
        {
            if (frame.GetFileLineNumber() > 0)
            {
                var current = document.CreateElement(elementName);
                var methodName = frame.GetMethod()?.Name ?? string.Empty;
                current.InnerXml =
                    $"<File>{frame.GetFileName()}</File>" +
                    $"<Method>{methodName}</Method>" +
                    $"<LineNumber>{frame.GetFileLineNumber()}</LineNumber>";
                root.AppendChild(current);
            }
        }
        return root;
    }
}
