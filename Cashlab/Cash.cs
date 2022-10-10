using System.Windows.Media;

namespace Cashlab;

public class Cash : IServeClient, INotifyPropertyChanged
{
    private static int CountCash = 0;

    private int id = 0;
    private ObservableCollection<Client> clients;
    private bool isOpen = true;
    private Random random;
    private int minTimeService;
    private int maxTimeService;

    private CashLog log;
    private Statistics cashRegisterStatistic;

    private SolidColorBrush color;

    public SolidColorBrush Color
    {
        get { return color; }
        set {
            color = value;
            OnPropertyChanged();
        }
    }



    public CashLog Log
    {
        get { return log; }
        set
        {
            log = value;
            OnPropertyChanged();
        }
    }
    public Statistics CashRegisterStatistic
    {
        get { return cashRegisterStatistic; }
        set
        {
            cashRegisterStatistic = value;
            OnPropertyChanged();
        }
    }
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

    
    public Cash()
    {
        CountCash += 1;
        Id = CountCash;

        Clients = new();
        random = new Random();
        Color = new SolidColorBrush(Colors.Yellow);
        Log = new CashLog($"Касса - {id}");
        CashRegisterStatistic = new Statistics();

        MinTimeService = 1;
        MaxTimeService = 5;
    }

    public async Task ServeClient(int time)
    {
        await Log.AddClientServedEntity(Clients[0], time);
        await CashRegisterStatistic.AddTimeServiceClient(time);
        await CashRegisterStatistic.AddCountServiceClient(1);
        Clients.RemoveAt(0);
        await Task.Delay(time);
    }

    public async Task AddClient(Client client)
    {
        await Task.Delay(0);
        Clients.Add(client);
    }

    public async Task Start()
    {
        while (true)
        {
            await Task.Delay(10);
            if (Clients.Count <= 0 || !isOpen)
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
