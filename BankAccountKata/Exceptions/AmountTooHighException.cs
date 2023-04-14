namespace BankAccountKata.Exceptions
{
    public class AmountTooHighException : Exception
    {
        public AmountTooHighException(string message) : base(message)
        {
        }

    }
}