using System.Windows.Media;
namespace Cashlab;

public class ShopViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Cash> cashes;
    private int countClients;
    private CommandTemplate addQueue;

    private int maxCountClientsGenerate;
    private int maxTimeClientsGenerate;

    public int MaxCountClientsGenerate
    {
        get { return maxCountClientsGenerate; }
        set { 
            maxCountClientsGenerate = value;
            OnPropertyChanged();
        }
    } 
    public int MaxTimeClientsGenerate
    {
        get { return maxTimeClientsGenerate; }
        set {
            maxTimeClientsGenerate = value;
            OnPropertyChanged();
        }
    }

    public int CountClients
    {
        get { return countClients; }
        set { 
            countClients = value;
            OnPropertyChanged();
        }
    }
    public CommandTemplate AddQueue
    {
        get
        {
            return addQueue ??
                (addQueue = new CommandTemplate(async obj =>
                {
                    List<Client> clients = await gg();
                    CountClients = clients.Count;
                    
                    await GoQueue(clients);
                }));
        }
    }
    public ObservableCollection<Cash> Cashes
    {
        get { return cashes; }
        set
        {
            cashes = value;
            OnPropertyChanged();
        }
    }

    public ShopViewModel()
    {
        Cashes = new ObservableCollection<Cash>();
        List <Client> clients = new();
        MaxTimeClientsGenerate = 1;
        MaxCountClientsGenerate = 50;
        for (int i = 0; i < 7; i++)
        {
            Cashes.Add(new Cash(i + 1));
            Cashes[i].Start();
        }

        for (int i = 0; i <= 5; i++)
        {
            clients.Add(new Client(new SolidColorBrush(Colors.Black)));
        }

        GoQueue(clients);
    }

    private async Task GoQueue(IEnumerable<Client> enumerable)
    {
        foreach (var item in enumerable)
        {
            await Cashes.
                 Where(c => c.Clients.Count == Cashes.Min(c => c.Clients.Count)).
                 First().
                 AddClient(item);
        }
    }

    private async Task<List<Client>> gg()
    {
        return await 
            new QueueGenerator(
                MaxTimeClientsGenerate, 
                MaxCountClientsGenerate)
            .Generate();
    }

    public async Task StartGenerate()
    {

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
