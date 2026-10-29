namespace BankAccounts.Core.Withdrawls;

public interface IWithdrawalPolicy
{
    WithdrawlDecision Evaluate(decimal currentBalance, decimal amount, decimal minimumBalance);
}
