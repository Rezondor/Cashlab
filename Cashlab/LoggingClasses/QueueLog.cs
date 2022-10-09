namespace Cashlab.LoggingClasses;

public class QueueLog : MainLog
{
    public async Task AddCountClientGeneratedEntity(int count, int time)
    {
        string entry = $"| Создано {count,7} клиентов за {time, 4} секунд";
        await AddEntry(entry);
    }
}
