using System.IO;
using System.Threading.Tasks;

namespace BoBo.Formatting;

public interface IDigest
{
    Task WriteTo(Stream stream);
}
