using System;

namespace BoBo.Formatting.Text;

public class Ticks
{
    public override string ToString()
    {
        return DateTime.UtcNow.Ticks.ToString();
    }
}
