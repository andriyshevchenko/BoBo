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
        StackFrame[] frames = new StackTrace(exception, true).GetFrames() ?? Array.Empty<StackFrame>();
        foreach (var frame in frames.Reverse())
        {
            if (frame.GetFileLineNumber() > 0)
            {
                var current = document.CreateElement(elementName);
                current.InnerXml =
                    $"<File>{frame.GetFileName()}</File>" +
                    $"<Method>{frame.GetMethod().Name}</Method>" +
                    $"<LineNumber>{frame.GetFileLineNumber()}</LineNumber>";
                root.AppendChild(current);
            }
        }
        return root;
    }
}
