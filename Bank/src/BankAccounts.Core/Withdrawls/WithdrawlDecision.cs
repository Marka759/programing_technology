namespace BankAccounts.Core.Withdrawls;
// Единый контракт для всех политик снятия: возвращаем решение, а не выбрасываем исключения. Это устраняет SLP между базовым и переопределённым методом
public sealed record WithdrawlDecision(bool IsAllowed, decimal Fee, string? RejectionReason)
{
    public static WithdrawlDecision Allow() => new(true, 0m, null);
    public static WithdrawlDecision AllowWithFee(decimal fee) => new(true, fee, null);
    public static WithdrawlDecision Reject(string reason) => new(false, 0m, reason);
}
