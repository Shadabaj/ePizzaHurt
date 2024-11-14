using PizzaHub.Services.Interface;
using PizzaHurt.Models;
using PizzaHut.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHurt.Services.Interface
{
    public interface IOrderService:IService<Order>
    {
        int placeOrder(int UserId,string OrderId,string PaymentId,CartModel cart, AddressModel Address);


        PagingListModel<OrderModel> GetOrderList(int page = 1, int pageSize = 10);

        IEnumerable<Order> GetUserOrders(int userid);
        //User
        OrderModel GetOrderDetails(string orderid);

    }

   

}
