namespace Smartwyre.DeveloperTest.Types
{
    public class AutomatedPaymentSystemPaymentType : IPaymentType
    {
        private Account _account;
        public AutomatedPaymentSystemPaymentType(Account account)
        {
            _account = account;
        }
        public MakePaymentResult GetResult()
        {
            if (!_account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.AutomatedPaymentSystem))
            {
                return new MakePaymentResult();
            }
            else if (_account.Status != AccountStatus.Live)
            {
                return new MakePaymentResult();
            }

            return new MakePaymentResult { Success = true };
        }
    }
}
