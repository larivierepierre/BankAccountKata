﻿using BankAccountKata.Exceptions;
using BankAccountKata.Interfaces;

namespace BankAccountKata
{
    public class BankAccountService
    {
        private readonly OperationRepository _operationRepository;

        public BankAccountService(OperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public void Deposit(Account account, decimal amount, IClock Clock)
        {
            if (amount < 0)
            {
                throw new InvalidAmountException("the amount must be positive");
            }

            var now = Clock.Now;

            var balance = _operationRepository.GetLastOperation(account)?.Balance ?? 0;

            var newBalance = balance + amount;

            var operation = new Operation(amount, account, newBalance, now, OperationType.Deposit);

            _operationRepository.AddOperation(operation);
        }

        public void Withdrawal(Account account, decimal amount, IClock Clock)
        {
            if (amount < 0)
            {
                throw new InvalidAmountException("the amount must be positive");
            }

            var balance = _operationRepository.GetLastOperation(account)?.Balance ?? 0;

            if (balance < amount)
            {
                throw new AmountTooHighException("the requested amount is too high");
            }

            var now = Clock.Now;

            var newBalance = balance - amount;

            var operation = new Operation(amount, account, newBalance, now, OperationType.Withdrawal);

            _operationRepository.AddOperation(operation);
        }

        public void DisplayAccountStatement(Account account)
        {
            var operations = _operationRepository.GetAllByAccount(account);

            var lines = operations.Select(operation => "Amount: " + operation.Amount + " Account: " + operation.Account.IBAN + " Balance: " + operation.Balance + " Date: " + operation.Date + " OperationType: " + operation.TypeOperation)
                                  .ToList();

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
