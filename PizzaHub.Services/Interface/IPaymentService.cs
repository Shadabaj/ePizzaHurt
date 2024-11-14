using PizzaHub.Services.Interface;
using PizzaHut.Core.Entities;
using Razorpay.Api;

namespace PizzaHurt.Services.Interface
{
    public interface IPaymentService:IService<PaymentDetail>
    {
        string CreateOrder(decimal amount,string Currency,string receipt);

        Payment GetPaymentDetails(string paymentId);

        bool verifySignature(string signature,string orderId, string paymentId);

        int SavePaymentdetails(PaymentDetail model);
    }
}
