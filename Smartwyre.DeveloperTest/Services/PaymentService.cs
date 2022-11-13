using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;

        public PaymentService(IAccountDataStore accountDataStore) 
        { 
            _accountDataStore= accountDataStore;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var result = new MakePaymentResult();
            Account account = _accountDataStore.GetAccount(request.DebtorAccountNumber);
            
            if (account == null) { return result; }

            var paymentType = PaymentTypeFactory.Create(account, request);

            if(paymentType == null) { return result; }

            result = paymentType.GetResult();

            if (result.Success)
            {
                account.Balance -= request.Amount;

                _accountDataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
