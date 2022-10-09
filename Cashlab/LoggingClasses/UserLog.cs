namespace Cashlab.LoggingClasses;

public class UserLog: MainLog
{
    public async Task AddCreateCashEntity(Cash cash)
    {
        string entry = $"| Пользователь создал кассу №{cash.Id}";
        await AddEntry(entry);
    }

    public async Task AddDeleteCashEntity(Cash cash)
    {
        string entry = $"| Пользователь удалил кассу №{cash.Id}";
        await AddEntry(entry);
    }

    public async Task AddStartWorkEntity()
    {
        string entry = $"| Работа началась!";
        await AddEntry(entry);
    }

    public async Task AddStopWorkEntity()
    {
        string entry = $"| Работа приостановлена!";
        await AddEntry(entry);
    }
}
