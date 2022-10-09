namespace Cashlab.LoggingClasses;

public class CashLog : MainLog
{
    public async Task AddClientServedEntity(Client client, int time)
    {
        string entry = $"| Клиент №{client.Id,7} обслужен за {time,4} секунд";
        await AddEntry(entry);
    }
}
