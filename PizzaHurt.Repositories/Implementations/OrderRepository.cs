using Microsoft.EntityFrameworkCore;
using PizzaHurt.Models;
using PizzaHurt.Repositories.Interfaces;
using PizzaHut.Core;
using PizzaHut.Core.Entities;
using System.Linq;
using X.PagedList;

namespace PizzaHurt.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        public OrderRepository(AppDbContext _Db):base(_Db) { }
        public OrderModel GetOrderDetails(string orderid)
        {
            var model = (from order in _Db.Orders
                        where order.Id == orderid
                        select new OrderModel
                        {
                            Id = order.Id,
                            UserId = order.UserId,
                            CreatedDate = order.CreatedDate,
                            Items = (from orderitem in _Db.OrderItems
                                     join item in _Db.Items
                                     on orderitem.ItemId equals item.Id
                                     where orderitem.OrderId == orderid
                                     select new ItemModel
                                     {

                                         Id = orderitem.Id,
                                         Name = item.Name,
                                         Description = item.Description,
                                         ImageUrl = item.ImageUrl,
                                         Quantity = orderitem.Quantity,
                                         ItemId = item.Id,
                                         UnitPrice = orderitem.UnitPrice
                                     }).ToList()

                        }).FirstOrDefault();
            return model;
        }

        public PagingListModel<OrderModel> GetOrderList(int page, int PageSize)
        {
           var PagingModel= new PagingListModel<OrderModel>();
            var data = from order in _Db.Orders
                       join payment in _Db.PaymentDetails on order.PaymentId equals payment.Id
                       select new OrderModel
                       {
                           Id = order.Id,
                           UserId = order.UserId,
                           PaymentId = payment.Id,
                           CreatedDate = order.CreatedDate,
                           GrandTotal = payment.GrandTotal,
                           Locality = order.Locality
                       };
            int itemCounts = data.Count(); //Item Count =7
            var orders = data.Skip((page - 1) * PageSize).Take(PageSize);

            var pagedListData = new StaticPagedList<OrderModel>(orders, page, PageSize, itemCounts);

            PagingModel.Data = pagedListData;
            PagingModel.Page = page;
            PagingModel.PageSize = PageSize;
            PagingModel.TotalRows = itemCounts;
            return PagingModel;
        }

        public IEnumerable<Order> GetUserOrders(int userid)
        {
            return _Db.Orders.Include(p=>p.OrderItems).Where(u => u.UserId == userid).ToList();
        }
    }
}
