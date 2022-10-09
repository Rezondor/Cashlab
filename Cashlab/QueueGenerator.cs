using System.Windows.Media;
namespace Cashlab;

public class QueueGenerator
{
    private Random random;
    private int minGenerateTime;
    private int maxGenerateTime;
    private int minClientCount;
    private int maxClientCount;
    private int clientCountGenerate;
    private int clientTimeGenerate;

    public int MinGenerateTime
    {
        get { return minGenerateTime; }
        private set
        {
            minGenerateTime = value;
            OnPropertyChanged();
        }
    }
    public int MaxGenerateTime
    {
        get { return maxGenerateTime; }
        set
        {
            maxGenerateTime = value;
            OnPropertyChanged();
        }
    }
    public int MinClientCount
    {
        get { return minClientCount; }
        private set
        {
            minClientCount = value;
            OnPropertyChanged();
        }
    }
    public int MaxClientCount
    {
        get { return maxClientCount; }
        set
        {
            maxClientCount = value;
            OnPropertyChanged();
        }
    }


    public int ClientTimeGenerate
    {
        get { return clientTimeGenerate; }
        set
        {
            clientTimeGenerate = value;
            OnPropertyChanged();
        }
    }


    public int ClientCountGenerate
    {
        get { return clientCountGenerate; }
        set
        {
            clientCountGenerate = value;
            OnPropertyChanged();
        }
    }


    public QueueGenerator(int maxGenerateTime, int maxClientCount)
    {
        random= new Random();
        MinClientCount = 0;
        MinGenerateTime = 1;
        MaxGenerateTime = maxGenerateTime;
        MaxClientCount = maxClientCount;
        /*ClientCountGenerate = 0;
        ClientTimeGenerate = 0;*/
    }

    public async Task<List<Client>> Generate()
    {
        ClientTimeGenerate = random.Next(MinGenerateTime, MaxGenerateTime);
        var wait = Task.Delay(ClientTimeGenerate * 1000);
        ClientCountGenerate = random.Next(MinClientCount, MaxClientCount);

        List<Client> clients = new();

        for (int i = 0; i < ClientCountGenerate; i++)
        {
            Brush color = new SolidColorBrush(
                Color.FromArgb(
                    (byte)random.Next(255),
                    (byte)random.Next(255),
                    (byte)random.Next(255),
                    255));

            clients.Add(new Client(color));
        }

        await wait;

        return clients;
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
