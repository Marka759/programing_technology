using System.Text;

namespace Bank;
//BankAccount -  потомок класса Object

/// <summary>
/// Представляет банковский счёт с базовыми операциями: внесение, снятие, история операций.
/// Является потомком класса <see cref="object"/>.
/// </summary>
public class BankAccount
    {
    /// <summary>
    /// Минимально допустимый баланс счёта.
    /// </summary>
    private readonly decimal _minimumBalance;
    /// <summary>
    /// Список всех транзакций по счёту.
    /// </summary>
    private List<Transaction> _allTransactions = new List<Transaction>();
    /// <summary>
    /// Владелец счёта.
    /// </summary>

    public string Owner { get; private set; }

    /// <summary>
    /// Номер счёта.
    /// </summary>
    public string Number { get;  }

    /// <summary>
    /// Статус клиента банка.
    /// </summary>
    public AccountStatus Status { get; set; } = AccountStatus.Regular;

    /// <summary>
    /// Текущий баланс счёта, вычисляемый как сумма всех транзакций.
    /// </summary>
    /// 
    public decimal Balance 
        { get
            {
                decimal balance = 0;
                foreach (var transactions in _allTransactions)
                { 
                    balance += transactions.Amount;
                }
                return balance;
            }
    
        }

        private static int s_accauntNumberS = 1000000000;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="BankAccount"/> с минимальным балансом по умолчанию (0).
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0)
    { 
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="BankAccount"/> с заданным минимальным балансом.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    /// <param name="minimumBalance">Минимально допустимый баланс счёта.</param>
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
            Owner = name;
            Number = s_accauntNumberS.ToString();
            s_accauntNumberS++;

        _minimumBalance = minimumBalance;
        if (initialBalance>0)
            MakeDeposite(initialBalance,DateTime.UtcNow, "initial balance"); // this.Balance = initialBalance;
            
        }
    /// <summary>
    /// Возвращает историю операций по счёту в виде форматированной строки.
    /// </summary>
    /// <returns>Строка с историей транзакций, включая дату, сумму, баланс и примечание.</returns>
    public string GetAccountHistory()
    {
        var report = new StringBuilder();

        decimal balance = 0;
        report.AppendLine("Data\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions )
        {
            balance += item.Amount;
            report.AppendLine($""+
                $"{item.Date.ToShortDateString()}\t"+
                $"{item.Amount}\t{balance}\t{item.Note}");
        }
        return report.ToString();

    }

    // virtual - ключевое слово, позволяющее в дочернем классе предоставить другую реализацию этому методу 

    /// <summary>
    /// Выполняет ежемесячные транзакции в зависимости от статуса клиента.
    /// </summary>
    public virtual void PerformMonthAndTransaction()
    {
      
        if (Status == AccountStatus.Regular)
        {
            MakeWithdrawal(50m, DateTime.UtcNow, "Monthly service fee");
        }

     
        if (Status == AccountStatus.Premium || Status == AccountStatus.VIP)
        {
            if (Balance > 10_000m)
            {
                decimal rate = Status == AccountStatus.Premium ? 0.01m : 0.03m;
                MakeDeposite((Balance - 10_000m) * rate, DateTime.UtcNow, "Monthly interest");
                AuditLog.Instance.Record(Number, AuditOperationType.Interest, interest, DateTime.UtcNow);
            }
        }

        
        if (Status == AccountStatus.VIP)
        {
            DateTime monthStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            decimal withdrawalsThisMonth = _allTransactions
                .Where(t => t.Amount < 0 && t.Date >= monthStart)
                .Sum(t => -t.Amount);

            if (withdrawalsThisMonth > 0)
            {
                decimal cashback = withdrawalsThisMonth * 0.05m;
                MakeDeposite(cashback, DateTime.UtcNow, "VIP cashback 5%");

                AuditLog.Instance.Record(Number, AuditOperationType.Interest, cashback, DateTime.UtcNow);
            }
        }
    }


    //Переопределяем метод базового класса -  класса object
    //ToString - возвращает строку с информацией об объекте

    /// <summary>
    /// Возвращает строковое представление счёта.
    /// </summary>
    /// <returns>Строка с информацией о владельце, номере счёта и типе счёта.</returns>
    public override string ToString()
    {
        return $"Owner: {Owner}\taccount number: {Number} (тип счёта {GetType()}";
    }
    /// <summary>
    /// Вносит указанную сумму на счёт.
    /// </summary>
    /// <param name="amount">Сумма для внесения. Должна быть положительной.</param>
    /// <param name="date">Дата транзакции.</param>
    /// <param name="note">Примечание к транзакции.</param>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если <paramref name="amount"/> меньше или равен нулю.</exception>
        public void MakeDeposite  (decimal amount, DateTime date, string note)
        {
            if(amount<=0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount),"Amount of deposite must be positive");
        }
            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);

        AuditLog.Instance.Record(Number, AuditOperationType.Deposit, amount, date);
    }
    /// <summary>
    /// Снимает указанную сумму со счёта.
    /// </summary>
    /// <param name="amount">Сумма для снятия. Должна быть положительной.</param>
    /// <param name="date">Дата транзакции.</param>
    /// <param name="note">Примечание к транзакции.</param>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если <paramref name="amount"/> меньше или равен нулю.</exception>
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);

        Transaction? withdrawal = new(-amount, date, note);

        _allTransactions.Add(withdrawal);
        AuditLog.Instance.Record(Number, AuditOperationType.Withdrawal, -amount, date);
        if (overdraftTransaction is not null ) 
            _allTransactions.Add(overdraftTransaction);
        AuditLog.Instance.Record(Number, AuditOperationType.Withdrawal,overdraftTransaction.Amount, overdraftTransaction.Date);
    }
    // protected - модификатор доступа, который означает, что этот метод можно вызвать только из текущего и дочернего класса 
    // Клиент (внешний код) данный метод вызвать не может 

    /// <summary>
    /// Проверяет, не превышен ли лимит снятия. Может быть переопределён в дочерних классах.
    /// </summary>
    /// <param name="isOverDrawn">Флаг, указывающий, превышен ли минимальный баланс.</param>
    /// <returns>Транзакция овердрафта или <c>null</c>, если овердрафт не требуется.</returns>
    /// <exception cref="InvalidOperationException">Выбрасывается, если <paramref name="isOverDrawn"/> равен <c>true</c> и овердрафт не разрешён.</exception>
    protected virtual Transaction? CheckWithdrawalLimit(bool isOverDrawn)
    {
        if (isOverDrawn)
        {
            throw new InvalidOperationException("Not sufficient rubls for this withdrawl");

        }
        else 
        {
            // default содержит значение по умолчанию, так как тип возвращаемого значения - ссылочный, то эта переменная возрашает null
            return default;// == return null;
        }
    }


}

