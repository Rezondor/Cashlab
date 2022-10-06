namespace Cashlab;

public class Cash : IServeClient, INotifyPropertyChanged
{
    private int id = 0;
    private ObservableCollection<Client> clients;
    private bool isOpen = true;
    private (int minTime, int maxTime) serviceTime;
    private Random random;

     
    public ObservableCollection<Client> Clients
    {
        get { return clients; }
        set
        {
            clients = value;
            OnPropertyChanged();
        }
    }
    public (int MinTime, int MaxTime) ServiceTime
    {
        get { return serviceTime; }
        set
        {
            serviceTime = value;
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
        ServiceTime = (1000, 5000);
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
        return random.Next(ServiceTime.MinTime, ServiceTime.MaxTime + 1);
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
