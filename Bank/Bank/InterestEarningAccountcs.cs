
namespace Bank;
/// <summary>
/// Представляет счёт с начислением процентов на остаток.
/// </summary>
public class InterestEarningAccountcs: BankAccount
{    /// <summary>
     /// Инициализирует новый экземпляр класса <see cref="InterestEarningAccountcs"/>.
     /// </summary>
     /// <param name="name">Имя владельца счёта.</param>
     /// <param name="initialBalance">Начальный баланс счёта.</param>
    public InterestEarningAccountcs(string name, decimal initialBalance) : base(name, initialBalance) 
    { 
    
    }
    //override позволяет в дочернем классе определить новую реалиацию  метода
 
    /// <summary>
    /// Начисляет ежемесячные проценты на остаток, если баланс превышает 500.
    /// </summary>
    public override void PerformMonthAndTransaction()
    {
        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposite(interest,DateTime.UtcNow, "Apply month interest");
        }
    }
}
