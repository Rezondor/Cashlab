namespace Cashlab;

public class ShopViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Cash> cashes;
    private Cash selectedCash;

    private int countClients;
    private bool isOpen;
    private QueueGenerator queueGenerator;
    private CommandTemplate startStopQueue;
    private CommandTemplate addCash;
    private CommandTemplate deleteCash;

    public QueueGenerator QueueGenerator
    {
        get { return queueGenerator; }
        set
        {
            queueGenerator = value;
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

            if (Cashes.Count > 0)
            {
                foreach (var cash in Cashes)
                {
                    cash.IsOpen = IsOpen;
                }
            }
        }
    }


    public int CountClients
    {
        get { return countClients; }
        set
        {
            countClients = value;
            OnPropertyChanged();
        }
    }
    public CommandTemplate StartStopQueue
    {
        get
        {
            return startStopQueue ??
                (startStopQueue = new CommandTemplate(obj =>
                {
                    IsOpen = !IsOpen;
                }));
        }
    }
    public CommandTemplate AddCash
    {
        get
        {
            return addCash ??
                (addCash = new CommandTemplate(async obj =>
                {
                    Cash cash = await CreateCash();

                    Cashes.Add(cash);
                    await cash.Start();
                }));
        }
    }

    public CommandTemplate DeleteCash
    {
        get
        {
            return deleteCash ??
                (deleteCash = new CommandTemplate(async obj =>
                {
                    Cashes.Remove(obj as Cash);
                }, obj => SelectedCash != null));
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
    public Cash SelectedCash
    {
        get { return selectedCash; }
        set
        {
            selectedCash = value;
            OnPropertyChanged();
        }
    }

    public ShopViewModel()
    {
        int MaxTimeClientsGenerate = 1;
        int MaxCountClientsGenerate = 50;

        Cashes = new ObservableCollection<Cash>();
        QueueGenerator = new QueueGenerator(MaxTimeClientsGenerate, MaxCountClientsGenerate);
        IsOpen = true;

        StartGenerate();
    }


    private async Task DistributionQueue(IEnumerable<Client> enumerable)
    {
        foreach (var item in enumerable)
        {
            await Cashes.
                 Where(c => c.Clients.Count == Cashes.Min(c => c.Clients.Count)).
                 First().
                 AddClient(item);
        }
    }

    private async Task<Cash> CreateCash()
    {
        await Task.Delay(0);
        return new Cash();
    }

    private async Task<List<Client>> QueueGeneration()
    {
        return await QueueGenerator.Generate();
    }


    public async Task StartGenerate()
    {
        while (true)
        {
            await Task.Delay(10);
            if (!isOpen || Cashes.Count <= 0)
            {
                continue;
            }

            var clients = QueueGeneration();
            await DistributionQueue(await clients);


        }
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
