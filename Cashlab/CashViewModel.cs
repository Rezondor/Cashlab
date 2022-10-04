using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Cashlab;

public class CashViewModel : INotifyPropertyChanged
{
    private List<Cash> cashes;
    private Cash selectCash;

    public Cash SelectCash
    {
        get { return selectCash; }
        set
        {
            selectCash = value;
            OnPropertyChanged();
        }
    }

    public List<Cash> Cashes
    {
        get { return cashes; }
        set
        {
            cashes = value;
            OnPropertyChanged();
        }
    }

    public CashViewModel()
    {
        Cashes = new List<Cash>();
        for (int i = 0; i < 10; i++)
        {
            Cashes.Add(new Cash(i));
        }
        SelectCash = new Cash(1);
        SelectCash.AddClient(new Client(new SolidBrush(Color.FromArgb(0,0,0))));
        SelectCash.AddClient(new Client(new SolidBrush(Color.FromArgb(0,0,0))));
        SelectCash.AddClient(new Client(new SolidBrush(Color.FromArgb(0,0,0))));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}
