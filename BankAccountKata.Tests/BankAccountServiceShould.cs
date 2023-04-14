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

        [TestMethod]
        [ExpectedException(typeof(AmountTooHighException), "the requested amount is too high")]
        public void ThrowExceptionIfWithdrawalAmountIsGreaterThanTheBalance()
        {
            //arrange
            var operationRepository = new TestOperationRepository();
            var bankAccountService = new BankAccountService(operationRepository);

            Account account = new("IBAN1");
            decimal amount = 1000;

            //act 
            bankAccountService.Withdrawal(account, amount, clock);
        }

        [TestMethod]
        public void DisplayEmptyAccountStatement()
        {
            //arrange
            var operationRepository = new TestOperationRepository();
            var bankAccountService = new BankAccountService(operationRepository);

            Account account = new("IBAN1");

            string expectedConsoleOutput = "";

            //act
            var original = Console.Out;
            var sw = new StringWriter();
            Console.SetOut(sw);

            bankAccountService.DisplayAccountStatement(account);

            string result = sw.ToString();

            //assert
            Assert.AreEqual(expectedConsoleOutput, result);
            Console.SetOut(original);
        }

        [TestMethod]
        public void DisplayAccountStatement()
        {

            //arrange
            var operationRepository = new TestOperationRepository();
            var bankAccountService = new BankAccountService(operationRepository);

            const decimal amount1 = 1900;
            const decimal amount2 = 1000;

            Account account = new("IBAN1");

            string expectedConsoleOutput = "Amount: 1900 Account: IBAN1 Balance: 1900 Date: 01/01/2023 00:00:00 OperationType: Deposit\r\nAmount: 1000 Account: IBAN1 Balance: 900 Date: 01/01/2023 00:00:00 OperationType: Withdrawal\r\n";

            //act
            var original = Console.Out;
            var sw = new StringWriter();
            Console.SetOut(sw);

            bankAccountService.Deposit(account, amount1, clock);
            bankAccountService.Withdrawal(account, amount2, clock);

            bankAccountService.DisplayAccountStatement(account);

            string result = sw.ToString();

            //assert
            Assert.AreEqual(expectedConsoleOutput, result);
            Console.SetOut(original);
        }
    }
}
