namespace Smartwyre.DeveloperTest.Types
{
    public class BankToBankTransferPaymentType : IPaymentType
    {
        private Account _account;
        public BankToBankTransferPaymentType(Account account) 
        { 
            _account= account;
        }
        public MakePaymentResult GetResult()
        {
            if (!_account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.BankToBankTransfer))
            {
                return new MakePaymentResult();
            }

            return new MakePaymentResult { Success= true };
        }
    }
}
