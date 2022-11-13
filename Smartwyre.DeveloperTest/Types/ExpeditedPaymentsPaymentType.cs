namespace Smartwyre.DeveloperTest.Types
{
    public class ExpeditedPaymentsPaymentType : IPaymentType
    {
        private readonly Account _account;
        private readonly MakePaymentRequest _request;
        public ExpeditedPaymentsPaymentType(Account account, MakePaymentRequest request)
        {
            _account = account;
            _request = request;
        }
        public MakePaymentResult GetResult()
        {
            if (!_account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.ExpeditedPayments))
            {
                return new MakePaymentResult();
            }
            else if (_account.Balance < _request.Amount)
            {
                return new MakePaymentResult();
            }

            return new MakePaymentResult { Success = true };
        }
    }
}
