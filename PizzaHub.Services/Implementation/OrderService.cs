using Microsoft.Extensions.Configuration;
using PizzaHub.Services.Implementation;
using PizzaHurt.Models;
using PizzaHurt.Repositories.Interfaces;
using PizzaHurt.Services.Interface;
using PizzaHut.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHurt.Services.Implementation
{
    public class OrderService : Service<Order>, IOrderService
    {
        IRepository<Order> _OrderRepo;
        IOrderRepository _OrderRepository;
        IConfiguration _configuration;

        public OrderService(IRepository<Order> orderRepo,IOrderRepository orderRepository,IConfiguration configuration ):base(orderRepo) 
        {
        _OrderRepo = orderRepo;
        _OrderRepository = orderRepository;
        _configuration = configuration;
        }

       

        public int placeOrder(int UserId, string OrderId, string PaymentId, CartModel cart, AddressModel Address)
        {
            Order order = new Order
            {
                PaymentId=PaymentId,
                UserId=UserId,
                CreatedDate=DateTime.Now,
                Id=OrderId,
                Street=Address.Street,
                Locality=Address.Locality,
                City=Address.City,
                ZipCode=Address.ZipCode,
                PhoneNumber=Address.PhoneNumber
            };

            foreach (var item in cart.Items)
            {
                OrderItem orderItem = new OrderItem
                {
                    ItemId = item.ItemId,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Total = item.Total,
                };
                order.OrderItems.Add(orderItem);
            }
            _OrderRepo.Add(order);
          return  _OrderRepo.SaveChanges();

        }
       

        public OrderModel GetOrderDetails(string orderid)
        {
            var model=_OrderRepository.GetOrderDetails(orderid);
            if (model!=null && model.Items.Count>0)
            {
                decimal subTotal=0;
                foreach (var item in model.Items) 
                { 
                item.Total=item.UnitPrice * item.Quantity;
                subTotal += item.Total;
                }
               model.Total= subTotal;
               model.Tax = Math.Round((model.Total * Convert.ToInt32(_configuration["Tax:GST"])) / 100, 2); 
               model.GrandTotal=model.Total + model.Tax;
            }
            return model;
        }

        public PagingListModel<OrderModel> GetOrderList(int page = 1, int pageSize = 10)
        {
           return _OrderRepository.GetOrderList(page, pageSize);
        }

        public IEnumerable<Order> GetUserOrders(int userid)
        {
            return _OrderRepository.GetUserOrders(userid);
        }
    }
}
