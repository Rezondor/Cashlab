namespace Cashlab;

public class ShopViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Cash> cashes;
    private Cash selectedCash;
    private QueueGenerator queueGenerator;
    private bool isOpen;


    #region Command

    private CommandTemplate startStopQueue;
    private CommandTemplate addCash;
    private CommandTemplate deleteCash;
    private CommandTemplate openAddInfo;
    public CommandTemplate OpenAddInfo
    {
        get
        {
            return openAddInfo ??= new CommandTemplate(obj =>
            {
                CashRegisterStatistics window = new CashRegisterStatistics(SelectedCash);
                window.ShowDialog();
            }, obj => obj is not null);
        }
    }
    public CommandTemplate StartStopQueue
    {
        get
        {
            return startStopQueue ??= new CommandTemplate(obj =>
            {
                IsOpen = !IsOpen;
            });
        }
    }
    public CommandTemplate AddCash
    {
        get
        {
            return addCash ??= new CommandTemplate(async obj =>
            {
                Cash cash = await CreateCash();

                Cashes.Add(cash);
                await cash.Start();
            });
        }
    }
    public CommandTemplate DeleteCash
    {
        get
        {
            return deleteCash ??= new CommandTemplate(async obj =>
            {
                await Task.Delay(0);
                Cashes.Remove((Cash)obj);
            }, obj => SelectedCash != null);
        }
    }
    

    #endregion

    public QueueGenerator QueueGenerator
    {
        get { return queueGenerator; }
        set
        {
            queueGenerator = value;
            OnPropertyChanged();
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



    public ShopViewModel()
    {
        int MaxTimeClientsGenerate = 1;
        int MaxCountClientsGenerate = 50;

        Cashes = new ObservableCollection<Cash>();
        QueueGenerator = new QueueGenerator(MaxTimeClientsGenerate, MaxCountClientsGenerate);
        IsOpen = true;

        StartQueueGenerate();
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


    public async Task StartQueueGenerate()
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
