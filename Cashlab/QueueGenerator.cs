using System.Windows.Media;
namespace Cashlab;

public class QueueGenerator
{
    private int minGenerateTime;
    private int maxGenerateTime;
    private int minClientCount;
    private int maxClientCount;

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


    public QueueGenerator(int maxGenerateTime, int maxClientCount)
    {
        MinClientCount = 0;
        MinGenerateTime = 1;
        MaxGenerateTime = maxGenerateTime;
        MaxClientCount = maxClientCount;
    }

    public async Task<List<Client>> Generate()
    {
        Random random = new Random();
        await Task.Delay(random.Next(MinGenerateTime, MaxGenerateTime) * 1000);
        List<Client> clients = new();
        int count = random.Next(MinClientCount, MaxClientCount);
        for (int i = 0; i < count; i++)
        {
            Brush color = new SolidColorBrush(
                Color.FromArgb(
                    (byte)random.Next(255),
                    (byte)random.Next(255),
                    (byte)random.Next(255),
                    255));
            clients.Add(new Client(color));
        }

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
