namespace Smartwyre.DeveloperTest.Types
{
    public static class PaymentTypeFactory
    {
        public static IPaymentType Create(Account account, MakePaymentRequest paymentRequest)
        {
           
            switch (paymentRequest.PaymentScheme)
            {
                case PaymentScheme.BankToBankTransfer:
                    return new BankToBankTransferPaymentType(account);

                case PaymentScheme.ExpeditedPayments:
                    return new ExpeditedPaymentsPaymentType(account,paymentRequest);

                case PaymentScheme.AutomatedPaymentSystem:
                    return new AutomatedPaymentSystemPaymentType(account);

                default: return null;
            }
        }
    }
}
