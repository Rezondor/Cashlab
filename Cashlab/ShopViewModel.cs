﻿using System.Windows.Media;
namespace Cashlab;

public class ShopViewModel : INotifyPropertyChanged
{
    private CommandTemplate addQueue;
    private int countClients;
    private ObservableCollection<Cash> cashes;

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
                    var aa = await gg();
                    CountClients = aa.Count;
                    Clients.AddRange(aa);
                    GoQueue();
                }));
        }
    }
    public List<Client> Clients { get; set; }
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
        Clients = new();

        for (int i = 0; i < 7; i++)
        {
            Cashes.Add(new Cash(i + 1));
            Cashes[i].Start();
        }

        for (int i = 0; i <= 5; i++)
        {
            Clients.Add(new Client(new SolidColorBrush(Colors.Black)));
        }

        GoQueue();
    }

    private async void GoQueue()
    {
        foreach (var item in Clients)
        {
            await Cashes.
                 Where(c => c.Clients.Count == Cashes.Min(c => c.Clients.Count)).
                 First().
                 AddClient(item);
        }
        Clients.Clear();
    }

    private async Task<List<Client>> gg()
    {
        return await new QueueGenerator(1, 1000).Generate();
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
