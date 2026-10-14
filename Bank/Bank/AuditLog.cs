namespace Bank;

/// <summary>
/// Тип операции для журнала аудита.
/// </summary>
public enum AuditOperationType
{
    Deposit,
    Withdrawal,
    Interest
}

/// <summary>
/// Запись в журнале аудита.
/// </summary>
/// <param name="AccountNumber">Номер счёта.</param>
/// <param name="OperationType">Тип операции.</param>
/// <param name="Amount">Сумма операции.</param>
/// <param name="Date">Дата операции.</param>
public record AuditEntry(
    string AccountNumber,
    AuditOperationType OperationType,
    decimal Amount,
    DateTime Date);

/// <summary>
/// Журнал аудита всех банковских операций.
/// </summary>
public class AuditLog
{
    /// <summary>
    /// Единственный экземпляр журнала (Singleton).
    /// </summary>
    public static AuditLog Instance { get; } = new AuditLog();

    private readonly List<AuditEntry> _entries = new();

    private AuditLog() { }

    /// <summary>
    /// Записывает операцию в журнал аудита.
    /// </summary>
    /// <param name="accountNumber">Номер счёта.</param>
    /// <param name="operationType">Тип операции.</param>
    /// <param name="amount">Сумма операции.</param>
    /// <param name="date">Дата операции.</param>
    public void Record(string accountNumber, AuditOperationType operationType,
                       decimal amount, DateTime date)
    {
        _entries.Add(new AuditEntry(accountNumber, operationType, amount, date));
    }

    /// <summary>
    /// Возвращает все записи журнала аудита.
    /// </summary>
    /// <returns>Список записей.</returns>
    public IReadOnlyList<AuditEntry> GetEntries() => _entries;

    /// <summary>
    /// Очищает журнал аудита (удобно для тестов).
    /// </summary>
    public void Clear() => _entries.Clear();
}