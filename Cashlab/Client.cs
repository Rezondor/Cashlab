using System.Windows.Media;
namespace Cashlab;

public class Client : INotifyPropertyChanged
{
    private static int clientCount = 0;
    private int id;
    private Brush color;

    public int Id
    {
        get { return id; }
        set
        {
            id = value;
            OnPropertyChanged();
        }
    }
    public Brush Color
    {
        get { return color; }
        set
        {
            color = value;
            OnPropertyChanged();
        }
    }

    public Client(Brush color)
    {
        clientCount += 1;
        Id = clientCount;
        Color = color;
    }

    #region MVVM 
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
    #endregion
}
