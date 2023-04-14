using BankAccountKata;

namespace TestBankAccount
{
    internal class TestOperationRepository : OperationRepository
    {
        public List<Operation> GetAllOperations()
        {
            return _operations;
        }
    }
}
