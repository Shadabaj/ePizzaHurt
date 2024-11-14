using PizzaHurt.Models;
using PizzaHut.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHurt.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetUserOrders(int userid);

        OrderModel GetOrderDetails(string orderid);

        //Admin Page Number
        PagingListModel<OrderModel> GetOrderList(int page, int PageSize);
    }
}
