namespace Bank;
/// <summary>
/// Представляет подарочный счёт с возможностью ежемесячного пополнения.
/// </summary>
public class GiftCartAccount:BankAccount
{
    private readonly decimal _monthlyDeposit = 0m;

    //monthlyDeposit - параметр по умолчанию, принимает 0, 
    //при создании объекта new GiftCartAccount("Yana",1000) -monthlyDeposit =0
    //new GiftCartAccount("Yana",1000., 5000) -monthlyDeposit =5000

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="GiftCartAccount"/>.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    /// <param name="monthlyDeposit">Ежемесячная сумма пополнения (по умолчанию 0).</param>
    public GiftCartAccount(string name, decimal initialBalance, decimal monthlyDeposit=0)
        :base(name, initialBalance)
        => _monthlyDeposit = monthlyDeposit;
    /// <summary>
    /// Выполняет ежемесячное пополнение счёта, если задана ненулевая сумма.
    /// </summary>
    public override void PerformMonthAndTransaction()
    {
        if (_monthlyDeposit != 0)
        {
            MakeDeposite(_monthlyDeposit, DateTime.UtcNow, "Add monthly depisit");
        }
    }
    /// <summary>
    /// Возвращает строковое представление подарочного счёта.
    /// </summary>
    /// <returns>Строка с информацией о счёте и ежемесячном пополнении.</returns>
    public override string ToString()
    {
        return base.ToString() + $"monyhly deposit: {_monthlyDeposit}";
    }
}
