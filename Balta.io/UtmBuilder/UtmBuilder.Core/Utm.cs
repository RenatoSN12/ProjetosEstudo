using UtmBuilder.Core.ValueObjects;

namespace UtmBuilder.Core;

public class Utm
{
    public Url Url { get; private set; } = new();
    public Campaign Campaign { get; private set; } = new();
    
}