namespace BankAccounts.Core.Time;
//Реализация для продакшена: берёт реальное время
public sealed class SystemClock : IClock
{
    public DateTime Now => DateTime.UtcNow;
}
