namespace BankAccountKata
{
    public class OperationRepository
    {
        protected readonly List<Operation> _operations = new List<Operation>();

        public void AddOperation(Operation operation)
        {
            _operations.Add(operation);
        }

        public Operation? GetLastOperation(Account account)
        {
            return _operations.LastOrDefault(o => o.Account == account);
        }
    }
}
