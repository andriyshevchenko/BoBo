using System;
using System.IO;
using System.Threading.Tasks;

namespace BoBo.Formatting;

public interface IDigest
{
    Task Write(Exception exception, Stream destination);
}
