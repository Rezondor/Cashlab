namespace Cashlab.LoggingClasses;

public class QueueLog : MainLog
{
    public QueueLog(string nameLog) : base(nameLog)
    {
        AddStartWorkEntity();
    }

    public async Task AddCountClientGeneratedEntity(int count, int time)
    {
        string entry = $"| Создано {count:D4} клиентов за {time:D2} секунд";
        await AddEntry(entry);
    }

    private async Task AddStartWorkEntity()
    {
        string entry = $"| Начата генерация очереди!";
        await AddEntry(entry);
    }
}
