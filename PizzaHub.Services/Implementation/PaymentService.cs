using Microsoft.Extensions.Configuration;
using PizzaHub.Services.Implementation;
using PizzaHurt.Repositories.Interfaces;
using PizzaHurt.Services.Interface;
using PizzaHut.Core.Entities;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;


namespace PizzaHurt.Services.Implementation
{
    public class PaymentService : Service<PaymentDetail>, IPaymentService
    {
        private readonly RazorpayClient _razorpayClient;
        IRepository<PaymentDetail> _paymentRepository;
        ICartRespository _cartRespository;
        IConfiguration _configuration;
        
       
        public PaymentService(IRepository<PaymentDetail> paymentRepository, ICartRespository cartRespository,IConfiguration configuration):base(paymentRepository)
        {
            _paymentRepository = paymentRepository;
            _configuration =configuration;
            _cartRespository = cartRespository;
            _razorpayClient = new RazorpayClient(_configuration["RazorPay:Key"], _configuration["RazorPay:Secret"]);
        }
        public string CreateOrder(decimal amount, string Currency, string receipt)
        {
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", amount); // amount in the smallest currency unit
            options.Add("receipt", receipt);
            options.Add("currency",Currency);
            Razorpay.Api.Order order = _razorpayClient.Order.Create(options);
            return order["id"].ToString();
        }

        public Payment GetPaymentDetails(string paymentId)
        {
            return _razorpayClient.Payment.Fetch(paymentId);
        }

        public bool verifySignature(string signature, string orderId, string paymentId)
        {
            string payload = string.Format("{0}|{1}", orderId, paymentId); //The values of orderId and paymentId are combined into a single string 123|456
            string secret = RazorpayClient.Secret;
            string actualSignature = getActualSignature(payload, secret);
            return actualSignature.Equals(signature);

        }
        private static string getActualSignature(string payload, string secret)
        {
            byte[] secretBytes = StringEncode(secret);
            HMACSHA256 hashHmac = new HMACSHA256(secretBytes);
            var bytes = StringEncode(payload);

            return HashEncode(hashHmac.ComputeHash(bytes));
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower(); //.Replace("-", ""): Removes the hyphens from the string:
        }
        
        
        
        
        
        
        public int SavePaymentdetails(PaymentDetail model)
        {
           _paymentRepository.Add(model);
            Cart cart=_cartRespository.Find(model.CartId);
            cart.IsActive = false;
            return _paymentRepository.SaveChanges();
        }
    }
}
