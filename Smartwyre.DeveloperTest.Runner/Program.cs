using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var paymentService = new PaymentService(new AccountDataStore());
            var paymentRequest = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.BankToBankTransfer,
                DebtorAccountNumber = "D123",
                PaymentDate = DateTime.Now,
                Amount = 1000,
                CreditorAccountNumber = "C987"
            };

            var result = paymentService.MakePayment(paymentRequest);

            Console.WriteLine(result);

        }
    }
}
