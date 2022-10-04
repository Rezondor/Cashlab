

namespace Cashlab;

public class Cash : IServeClient, INotifyPropertyChanged
{
    private int id = 0;

    private Queue<Client> clients;
    private bool isOpen = true;
    private (int minTime, int maxTime) serviceTime = (0, 0);
    private double avgServiceTime = 0;
    private int orderCount = 0;
    private int timeServiceCount = 0;
    private int clientCount = 0;
    private Random random;

    public Queue<Client> Clients
    {
        get { return clients; }
        set
        {
            clients = value;
            ClientCount = clients.Count;
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
    public double AvgServiceTime
    {
        get { return avgServiceTime; }
        private set
        {
            avgServiceTime = value;
            OnPropertyChanged();
        }
    }
    public int OrderCount
    {
        get { return orderCount; }
        private set
        {
            orderCount = value;
            OnPropertyChanged();
        }
    }
    public int TimeServiceCount
    {
        get { return timeServiceCount; }
        private set
        {
            timeServiceCount = value;
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
    public int ClientCount
    {
        get { return clientCount; }
        private set
        {
            clientCount = value;
            OnPropertyChanged();
        }
    }

    public Cash(int id)
    {
        Id = id;
        Clients = new();
        ServiceTime = (1, 2000);
        AvgServiceTime = 0;
        OrderCount = 0;
        TimeServiceCount = 0;
        ClientCount = 0;
        random = new Random();
    }

    public async Task ServeClient(int time)
    {
        await Task.Delay(time);
        Clients.Dequeue();
        OrderCount++;
        ClientCount--;
        TimeServiceCount += time;
    }

    public async Task AddClient(Client client)
    {
        await Task.Delay(0);
        Clients.Enqueue(client);
        ClientCount++;
    }

    public async Task Start()
    {
        while (IsOpen)
        {
            await Task.Delay(100);
            if (ClientCount <= 0)
            {
                continue;
            }
            int time = await GenerateTime();
            await ServeClient(time);

        }
          
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
