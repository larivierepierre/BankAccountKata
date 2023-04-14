﻿using BankAccountKata.Interfaces;


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
            var now = Clock.Now;

            var balance = _operationRepository.GetLastOperation(account)?.Balance ?? 0;

            var newBalance = balance + amount;

            var operation = new Operation(amount, account, newBalance, now, OperationType.Deposit);

            _operationRepository.AddOperation(operation);
        }
    }
}