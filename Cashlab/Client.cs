using System.Drawing;

namespace Cashlab;

public class Client
{
    private static int clientCount = 0;
    private int id = 0;
    public SolidBrush Color{ get; set; }

    public Client(SolidBrush color)
    {
        clientCount += 1;
        id = clientCount;
        Color = color;
    }
}
