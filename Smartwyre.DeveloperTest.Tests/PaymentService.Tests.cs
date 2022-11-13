using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        [Fact]
        public void TestBankToBankTransferSuccess()
        {
            var accountDataStoreMock = new Mock<IAccountDataStore>();
            var fakeAccount = new Account
            {
                AccountNumber = "D123",
                AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer,
                Balance = 2000,
                Status = AccountStatus.Live
            };

            accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>()))
                .Returns(fakeAccount);
            accountDataStoreMock.Setup(x => x.UpdateAccount(It.IsAny<Account>()));

            var paymentService = new PaymentService(accountDataStoreMock.Object);
            var paymentRequest = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.BankToBankTransfer,
                DebtorAccountNumber = "D123",
                PaymentDate = DateTime.Now,
                Amount = 1000,
                CreditorAccountNumber = "C987"
            };

            var paymentResult = paymentService.MakePayment(paymentRequest);
            Assert.True(paymentResult.Success);
            accountDataStoreMock.VerifyAll();
        }

        [Fact]
        public void TestBankToBankTransferFailsOnNullAccount()
        {
            var accountDataStoreMock = new Mock<IAccountDataStore>();

            var paymentService = new PaymentService(accountDataStoreMock.Object);
            var paymentRequest = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.BankToBankTransfer,
                DebtorAccountNumber = "D123",
                PaymentDate = DateTime.Now,
                Amount = 1000,
                CreditorAccountNumber = "C987"
            };

            var paymentResult = paymentService.MakePayment(paymentRequest);
            Assert.False(paymentResult.Success);
        }

        [Fact]
        public void TestBankToBankTransferFailsOnInvalidPaymentScheme()
        {
            var accountDataStoreMock = new Mock<IAccountDataStore>();
            var fakeAccount = new Account
            {
                AccountNumber = "D123",
                AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments | AllowedPaymentSchemes.AutomatedPaymentSystem,
                Balance = 2000,
                Status = AccountStatus.Live
            };

            accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>()))
                .Returns(fakeAccount);


            var paymentService = new PaymentService(accountDataStoreMock.Object);
            var paymentRequest = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.BankToBankTransfer,
                DebtorAccountNumber = "D123",
                PaymentDate = DateTime.Now,
                Amount = 1000,
                CreditorAccountNumber = "C987"
            };

            var paymentResult = paymentService.MakePayment(paymentRequest);
            Assert.False(paymentResult.Success);
        }

        [Fact]
        public void TestExpeditedPaymentsSuccess()
        {
            var accountDataStoreMock = new Mock<IAccountDataStore>();
            var fakeAccount = new Account
            {
                AccountNumber = "D123",
                AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments,
                Balance = 2000,
                Status = AccountStatus.Live
            };

            accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>()))
                .Returns(fakeAccount);
            accountDataStoreMock.Setup(x => x.UpdateAccount(It.IsAny<Account>()));

            var paymentService = new PaymentService(accountDataStoreMock.Object);
            var paymentRequest = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.ExpeditedPayments,
                DebtorAccountNumber = "D123",
                PaymentDate = DateTime.Now,
                Amount = 1000,
                CreditorAccountNumber = "C987"
            };

            var paymentResult = paymentService.MakePayment(paymentRequest);
            Assert.True(paymentResult.Success);
            accountDataStoreMock.VerifyAll();
        }

        [Fact]
        public void TestExpeditedPaymentsFailsOnNullAccount()
        {
            var accountDataStoreMock = new Mock<IAccountDataStore>();

            var paymentService = new PaymentService(accountDataStoreMock.Object);
            var paymentRequest = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.ExpeditedPayments,
                DebtorAccountNumber = "D123",
                PaymentDate = DateTime.Now,
                Amount = 1000,
                CreditorAccountNumber = "C987"
            };

            var paymentResult = paymentService.MakePayment(paymentRequest);
            Assert.False(paymentResult.Success);
            accountDataStoreMock.VerifyAll();
        }

        [Fact]
        public void TestExpeditedPaymentsFailsOnInvalidPaymentScheme()
        {
            var accountDataStoreMock = new Mock<IAccountDataStore>();
            var fakeAccount = new Account
            {
                AccountNumber = "D123",
                AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer,
                Balance = 2000,
                Status = AccountStatus.Live
            };

            accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>()))
                .Returns(fakeAccount);
            accountDataStoreMock.Setup(x => x.UpdateAccount(It.IsAny<Account>()));

            var paymentService = new PaymentService(accountDataStoreMock.Object);
            var paymentRequest = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.ExpeditedPayments,
                DebtorAccountNumber = "D123",
                PaymentDate = DateTime.Now,
                Amount = 1000,
                CreditorAccountNumber = "C987"
            };

            var paymentResult = paymentService.MakePayment(paymentRequest);
            Assert.False(paymentResult.Success);
        }

        [Fact]
        public void TestExpeditedPaymentsFailsOnBalance()
        {
            var accountDataStoreMock = new Mock<IAccountDataStore>();
            var fakeAccount = new Account
            {
                AccountNumber = "D123",
                AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments,
                Balance = 500,
                Status = AccountStatus.Live
            };

            accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>()))
                .Returns(fakeAccount);
            accountDataStoreMock.Setup(x => x.UpdateAccount(It.IsAny<Account>()));

            var paymentService = new PaymentService(accountDataStoreMock.Object);
            var paymentRequest = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.ExpeditedPayments,
                DebtorAccountNumber = "D123",
                PaymentDate = DateTime.Now,
                Amount = 1000,
                CreditorAccountNumber = "C987"
            };

            var paymentResult = paymentService.MakePayment(paymentRequest);
            Assert.False(paymentResult.Success);
        }

        [Fact]
        public void TestAutomatedPaymentSystemSuccess()
        {
            var accountDataStoreMock = new Mock<IAccountDataStore>();
            var fakeAccount = new Account
            {
                AccountNumber = "D123",
                AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem,
                Balance = 2000,
                Status = AccountStatus.Live
            };

            accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>()))
                .Returns(fakeAccount);
            accountDataStoreMock.Setup(x => x.UpdateAccount(It.IsAny<Account>()));

            var paymentService = new PaymentService(accountDataStoreMock.Object);
            var paymentRequest = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.AutomatedPaymentSystem,
                DebtorAccountNumber = "D123",
                PaymentDate = DateTime.Now,
                Amount = 1000,
                CreditorAccountNumber = "C987"
            };

            var paymentResult = paymentService.MakePayment(paymentRequest);
            Assert.True(paymentResult.Success);
            accountDataStoreMock.VerifyAll();
        }

        [Fact]
        public void TestAutomatedPaymentSystemFailsOnNullAccount()
        {
            var accountDataStoreMock = new Mock<IAccountDataStore>();

            var paymentService = new PaymentService(accountDataStoreMock.Object);
            var paymentRequest = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.AutomatedPaymentSystem,
                DebtorAccountNumber = "D123",
                PaymentDate = DateTime.Now,
                Amount = 1000,
                CreditorAccountNumber = "C987"
            };

            var paymentResult = paymentService.MakePayment(paymentRequest);
            Assert.False(paymentResult.Success);
        }

        [Fact]
        public void TestAutomatedPaymentSystemFailsOnInvalidPaymentScheme()
        {
            var accountDataStoreMock = new Mock<IAccountDataStore>();
            var fakeAccount = new Account
            {
                AccountNumber = "D123",
                AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer | AllowedPaymentSchemes.ExpeditedPayments,
                Balance = 2000,
                Status = AccountStatus.Live
            };

            accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>()))
                .Returns(fakeAccount);

            var paymentService = new PaymentService(accountDataStoreMock.Object);
            var paymentRequest = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.AutomatedPaymentSystem,
                DebtorAccountNumber = "D123",
                PaymentDate = DateTime.Now,
                Amount = 1000,
                CreditorAccountNumber = "C987"
            };

            var paymentResult = paymentService.MakePayment(paymentRequest);
            Assert.False(paymentResult.Success);
        }

        [Fact]
        public void TestAutomatedPaymentSystemFailsOnNotLive()
        {
            var accountDataStoreMock = new Mock<IAccountDataStore>();
            var fakeAccount = new Account
            {
                AccountNumber = "D123",
                AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem,
                Balance = 2000,
                Status = AccountStatus.Disabled
            };

            accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>()))
                .Returns(fakeAccount);

            var paymentService = new PaymentService(accountDataStoreMock.Object);
            var paymentRequest = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.AutomatedPaymentSystem,
                DebtorAccountNumber = "D123",
                PaymentDate = DateTime.Now,
                Amount = 1000,
                CreditorAccountNumber = "C987"
            };

            var paymentResult = paymentService.MakePayment(paymentRequest);
            Assert.False(paymentResult.Success);
        }

    }
}
