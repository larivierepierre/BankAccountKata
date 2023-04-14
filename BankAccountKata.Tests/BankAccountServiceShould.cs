using BankAccountKata;
using BankAccountKata.Interfaces;

namespace TestBankAccount
{
    [TestClass]
    public class BankAccountServiceShould
    {
        private IClock clock;
        private DateTime when;

        [TestInitialize]
        public void Setup()
        {
            this.when = new DateTime(2023, 01, 01);
            this.clock = new FixedDateClock(when);
        }

        [TestMethod]
        public void AllowValidDeposit()
        {
            //arrange
            var operationRepository = new TestOperationRepository();
            var bankAccountService = new BankAccountService(operationRepository);


            Account account = new("IBAN1");
            const decimal amount = 1000;

            var expected = new List<Operation>
            {
                new Operation (amount,account,1000,when,OperationType.Deposit)
            };

            //act 
            bankAccountService.Deposit(account, amount, clock);
            var actual = operationRepository.GetAllOperations();

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}