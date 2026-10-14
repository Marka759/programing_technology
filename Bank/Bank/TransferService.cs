namespace Bank;

/// <summary>
/// Сервис перевода денег между счетами.
/// </summary>
public static class BankTransferService
{
    public static void Transfer(BankAccount from, BankAccount to, decimal amount, DateTime date, string note)
    {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        from.MakeWithdrawal(amount, date, $"Transfer to {to.Number}: {note}");

        try
        {
            to.MakeDeposite(amount, date, $"Transfer from {from.Number}: {note}");
        }
        catch
        {
            from.MakeDeposite(amount, date, $"Rollback transfer to {to.Number}");
            throw;
        }
    }
}