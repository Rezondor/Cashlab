namespace Cashlab;

public class Cash : IServeClient, INotifyPropertyChanged
{
    private int id = 0;
    private ObservableCollection<Client> clients;
    private bool isOpen = true;
    private Random random;
    private int minTimeService;
    private int maxTimeService;

    public ObservableCollection<Client> Clients
    {
        get { return clients; }
        set
        {
            clients = value;
            OnPropertyChanged();
        }
    }
    public int MinTimeService
    {
        get { return minTimeService; }
        set
        {
            minTimeService = value;
            OnPropertyChanged();
        }
    }
    public int MaxTimeService
    {
        get { return maxTimeService; }
        set
        {
            maxTimeService = value;
            OnPropertyChanged();
        }
    }


    public bool IsOpen
    {
        get { return isOpen; }
        set
        {
            isOpen = value;
            OnPropertyChanged();
        }
    }
    public int Id
    {
        get { return id; }
        private set
        {
            id = value;
            OnPropertyChanged();
        }
    }
    public Cash(int id)
    {
        Id = id;
        Clients = new();
        MinTimeService = 1;
        MaxTimeService = 5;
        random = new Random();
    }

    public async Task ServeClient(int time)
    {
        await Task.Delay(time);
        Clients.RemoveAt(0);
    }

    public async Task AddClient(Client client)
    {
        await Task.Delay(0);
        Clients.Add(client);
    }

    public async Task Start()
    {
        while (IsOpen)
        {
            await Task.Delay(10);
            if (Clients.Count <= 0)
            {
                continue;
            }
            await ServeClientRandomInterval();

        }

    }

    private async Task ServeClientRandomInterval()
    {
        int time = await GenerateTime();
        await ServeClient(time);
    }

    private async Task<int> GenerateTime()
    {
        await Task.Delay(0);
        return random.Next(MinTimeService * 1000, MaxTimeService * 1000);
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
