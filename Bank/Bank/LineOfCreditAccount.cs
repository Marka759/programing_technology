namespace Bank;
/// <summary>
/// Представляет кредитный счёт с возможностью овердрафта.
/// </summary>
public class LineOfCreditAccount : BankAccount
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LineOfCreditAccount"/>.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    /// <param name="creditLimit">Кредитный лимит.</param>
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit)

    {

    }
    /// <summary>
    /// Начисляет ежемесячные проценты на отрицательный баланс.
    /// </summary>
    public override void PerformMonthAndTransaction()
    {
        if (Balance < 0)
        {
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.UtcNow, "Charge month interest");
        }
    }
    /// <summary>
    /// Проверяет лимит снятия и применяет транзакцию овердрафта при превышении.
    /// </summary>
    /// <param name="isOverdrawn">Флаг, указывающий, превышен ли кредитный лимит.</param>
    /// <returns>Транзакция овердрафта или <c>null</c>, если овердрафт не требуется.</returns>
    protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        => isOverdrawn ? new Transaction(-20, DateTime.UtcNow, "apply overdraft") : default;

}
