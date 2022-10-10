namespace Cashlab.StatisticsClasses;

public class Statistics : INotifyPropertyChanged
{
    private int serviceClientCount;
    private int totalServiceTime;
    private double avgServiceTime;

    public Statistics()
    {
        ServiceClientCount = 0;
        TotalServiceTime = 0;
        AVGServiceTime = 0;
    }

    public int ServiceClientCount
    {
        get { return serviceClientCount; }
        private set
        {
            serviceClientCount = value;
            SearchAVGServiceTime();
            OnPropertyChanged();
        }
    }


    public int TotalServiceTime
    {
        get { return totalServiceTime; }
        private set
        {
            totalServiceTime = value;
            SearchAVGServiceTime();
            OnPropertyChanged();
        }
    }

    public double AVGServiceTime
    {
        get { return avgServiceTime; }
        private set { 
            avgServiceTime = value;
            OnPropertyChanged();
        }
    }

    private void SearchAVGServiceTime()
    {
        double totalTime = TotalServiceTime;
        if (totalTime == 0)
            totalTime = 1;
        totalTime = totalTime / 1000;
        AVGServiceTime = ServiceClientCount / totalTime;
    }
    public async Task AddCountServiceClient(int count)
    {
        await Task.Delay(0);
        ServiceClientCount += count;
    }

    public async Task AddTimeServiceClient(int time)
    {
        await Task.Delay(0);
        TotalServiceTime += time;
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
