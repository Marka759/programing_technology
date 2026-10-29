namespace BankAccounts.Core.AccountNumbers;
//Генерация номеров аккаунтов - отдельная ответственность не должна бть внутри счёта (SRP)
public interface IAccountNumberGenerator
{
    string Next();
}
