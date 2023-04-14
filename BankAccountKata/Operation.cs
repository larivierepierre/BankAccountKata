namespace BankAccountKata
{
    public record Operation(decimal Amount, Account Account, decimal Balance, DateTime Date, OperationType TypeOperation);
}
