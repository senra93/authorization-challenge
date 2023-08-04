using Authorization.Business.Interfaces;

namespace Authorization.Business
{
    public class UFEService : IUFEService
    {
        private readonly Random _random;
        private DateTime _hour;
        private decimal _previousFee;

        public UFEService()
        {
            _random = new Random();
            _hour = DateTime.Now;
            _previousFee = 100;
        }

        public async Task<decimal> GetFee()
        {
            var today = DateTime.Now;
            if( _hour.Year != today.Year || 
                _hour.Month != today.Month || 
                _hour.Day != today.Day || 
                _hour.Hour != today.Hour)
            {
                var randomDecimal = (decimal)_random.NextDouble() * 2;
                decimal newFeeAmount = _previousFee * randomDecimal;
                _previousFee = newFeeAmount;

                return newFeeAmount;
            } else { 
                return _previousFee; 
            }
        }
    }
}
