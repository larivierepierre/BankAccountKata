﻿namespace BankAccountKata.Exceptions
{
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException(string message) : base(message)
        {
        }

    }
}
