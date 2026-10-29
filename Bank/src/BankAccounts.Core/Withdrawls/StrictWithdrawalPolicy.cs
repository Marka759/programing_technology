namespace BankAccounts.Core.Withdrawls;
// политика по умолчанию: если баланс ниже минимума - отклоняем
public sealed class StrictWithdrawalPolicy : IWithdrawalPolicy
{
    public WithdrawlDecision Evaluate(decimal currentBalance, decimal amount, decimal minimumBalance)
        =>  currentBalance - amount < minimumBalance
            ? WithdrawlDecision.Reject("Not sufficiant rubles for this withdrawal")
            : WithdrawlDecision.Allow(); 
}
