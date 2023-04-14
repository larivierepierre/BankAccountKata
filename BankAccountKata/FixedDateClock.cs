using BankAccountKata.Interfaces;

namespace BankAccountKata
{
    public class FixedDateClock : IClock
    {
        private readonly DateTime _when;

        public FixedDateClock(DateTime when)
        {
            _when = when;
        }

        public DateTime Now
            => _when;
    }
}
