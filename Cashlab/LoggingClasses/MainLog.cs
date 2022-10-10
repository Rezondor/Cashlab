namespace Cashlab.LoggingClasses;

public abstract class MainLog : INotifyPropertyChanged
{
    private string nameLog;
    private ObservableCollection<string> logs;

    public string NameLog
    {
        get { return nameLog; }
        set
        {
            nameLog = value;
            OnPropertyChanged();
        }
    }
    public ObservableCollection<string> Logs
    {
        get { return logs; }
        set
        {
            logs = value;
            OnPropertyChanged();
        }
    }

    public MainLog(string nameLog)
    {
        NameLog = nameLog;
        Logs = new ObservableCollection<string>();
    }

    protected async Task AddEntry(string entry)
    {
        await Task.Delay(0);
        Logs.Add($"{DateTime.Now,20} " + entry);
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
