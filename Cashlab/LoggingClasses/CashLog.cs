namespace Cashlab.LoggingClasses;

public class CashLog : MainLog
{
    public CashLog(string nameLog) : base(nameLog)
    {
    }

    public async Task AddClientServedEntity(Client client, int time)
    {
        string entry = $"| Клиент №{client.Id:d7} обслужен за {time:#c ### мс}";
        await AddEntry(entry);
    }
}
