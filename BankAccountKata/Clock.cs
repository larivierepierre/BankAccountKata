using BankAccountKata.Interfaces;

namespace BankAccountKata
{
    public class Clock : IClock
    {
        public DateTime Now => DateTime.Now;
    }
}
