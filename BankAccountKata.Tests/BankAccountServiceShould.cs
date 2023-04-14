using BankAccountKata;
using BankAccountKata.Exceptions;
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
    
        [TestMethod]
        [ExpectedException(typeof(InvalidAmountException), "the amount must be positive")]
        public void ThrowExceptionIfDepositAmountIsNegative()
        {
            //arrange
            var operationRepository = new TestOperationRepository();
            var bankAccountService = new BankAccountService(operationRepository);
            Account account = new("IBAN1");
            decimal amount = -2000;

            //act
            bankAccountService.Deposit(account, amount, clock);
        }

        [TestMethod]
        public void AllowValidWithdrawal()
        {
            //arrange
            var operationRepository = new TestOperationRepository();
            var bankAccountService = new BankAccountService(operationRepository);

            const decimal amount1 = 1900;
            const decimal amount2 = 1000;

            Account account = new("IBAN1");

            var expected = new List<Operation>
            {
                new Operation (amount1,account,1900,when,OperationType.Deposit),
                new Operation (amount2,account,900,when,OperationType.Withdrawal)
            };

            //act
            bankAccountService.Deposit(account, amount1, clock);
            bankAccountService.Withdrawal(account, amount2, clock);

            var actual = operationRepository.GetAllOperations();

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAmountException), "the amount must be positive")]
        public void ThrowExceptionIfWithdrawalAmountIsNegative()
        {
            //arrange
            var operationRepository = new TestOperationRepository();
            var bankAccountService = new BankAccountService(operationRepository);
            Account account = new("IBAN1");
            decimal amount = -2000;

            //act 
            bankAccountService.Withdrawal(account, amount, clock);
        }

    }
}
