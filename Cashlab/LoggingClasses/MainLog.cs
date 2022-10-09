namespace Cashlab.LoggingClasses;

public abstract class MainLog: INotifyPropertyChanged
{
    private ObservableCollection<string> logs;

    public ObservableCollection<string> Logs
    {
        get { return logs; }
        set { 
            logs = value; 
            OnPropertyChanged();
        }
    }

    protected async Task AddEntry(string entry)
    {
        await Task.Delay(0);
        logs.Add($"{DateTime.Now,20} " + entry);
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
