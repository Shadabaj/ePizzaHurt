using Microsoft.AspNetCore.Mvc;
using PizzaHurt.Models;
using PizzaHurt.Services.Interface;
using PizzaHut.Core;

namespace PizzaHurt.UI.Areas.User.Controllers
{
    public class OrderController : BaseController
    {

        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            var orders = _orderService.GetUserOrders(CurrentUser.Id);

            return View(orders);
        }

        [Route("~/User/Order/Details/{OrderId}")]
        public IActionResult Details(string OrderId)
        {
            OrderModel model=_orderService.GetOrderDetails(OrderId);
            return View(model);
        }

    }
}
