namespace Classes;

public class Transaction
{
  public decimal Amount { get; }
  public DateTime Date { get; }
  public string Note { get; }
  public Transaction(decimal amount, DateTime date, string note)
  {
    this.Amount = amount;
    this.Date = date;
    this.Note = note;
  }

}


public class BankAccount
{
  private static int acountNumberSeed = 123456789;
  private List<Transaction> allTransations = new List<Transaction>();

  public string Number { get; }
  public string Owner { get; }

  public decimal Balance
  {
    get
    {
      decimal balance = 0;

      foreach (var item in allTransations)
      {
        balance += item.Amount;
      }

      return balance;
    }
  }


  public BankAccount(string name, decimal initialBalance)
  {
    this.Number = acountNumberSeed.ToString();
    acountNumberSeed++;

    this.Owner = name;
    MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
  }




  public void MakeDeposit(decimal amount, DateTime date, string note)
  {
    if (amount <= 0)
    {
      throw new ArgumentOutOfRangeException(nameof(amount), "first error");
    }

    var deposit = new Transaction(amount, date, note);
    allTransations.Add(deposit);
  }


  public void MakeWithdrawal(decimal amount, DateTime date, string note)
  {
    if (amount <= 0)
    {
      throw new ArgumentOutOfRangeException(nameof(amount), "second error");
    }

    if (Balance - amount < 0)
    {
      throw new InvalidOperationException("third error");
    }

    var withdrawal = new Transaction(-amount, date, note);
    allTransations.Add(withdrawal);
  }

  public string GetAccountHistory()
  {
    var report = new System.Text.StringBuilder();

    decimal balance = 0;
    report.AppendLine("Date\tAmount\tBalance\tNote");
    foreach (var item in allTransations)
    {
      balance += item.Amount;
      report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Note}");
    }
    return report.ToString();
  }
}


class Program
{
  static void Main(string[] args)
  {
    // Create a bank account
    BankAccount account = new BankAccount("John Doe", 1000.00m);

    // Make some deposits and withdrawals
    account.MakeDeposit(500.00m, DateTime.Now, "Salary");
    account.MakeWithdrawal(200.00m, DateTime.Now, "Groceries");
    account.MakeDeposit(300.00m, DateTime.Now, "Bonus");

    // Display account history in the console
    Console.WriteLine("Account History:");
    Console.WriteLine(account.GetAccountHistory());
  }
}