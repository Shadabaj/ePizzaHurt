using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using PizzaHurt.Models;
using PizzaHurt.Services.Interface;
using System.Data;

namespace PizzaHurt.UI.Areas.Admin.Controllers
{

    public class HomeController : BaseController
    {
        private IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index(int page=1, int pagesize=5)
        {
            var model = _orderService.GetOrderList(page,pagesize);
            return View(model);
        }

        [Route("~/Admin/Home/Details/{OrderId}")]
        public IActionResult Details(string OrderId)
        {
            OrderModel order = _orderService.GetOrderDetails(OrderId);
            return View(order);
        }

        public IActionResult ExportData(int page = 1, int pageSize = 10)
        {
            var data = _orderService.GetOrderList(page, pageSize);
            var filename = "Report_" + DateTime.UtcNow.ToString("yyyyMMdd_HHmmss") + ".xlsx";  // Changed to .xlsx

            var orderModels = data.Data.Select(orders => new OrderModel
            {
                Id = orders.Id,
                UserId = orders.UserId,
                PaymentId = orders.PaymentId,
                CreatedDate = orders.CreatedDate,
                GrandTotal = orders.GrandTotal,
                Locality = orders.Locality
            });

            return GenerateExcel(filename, orderModels);
        }

        private FileResult GenerateExcel(string fileName, IEnumerable<OrderModel> orders)
        {
            // Setting up the DataTable
            DataTable dataTable = new DataTable("order");
            dataTable.Columns.AddRange(new DataColumn[]
            {
        new DataColumn("OrderID"),
        new DataColumn("UserID"),
        new DataColumn("PaymentId"),
        new DataColumn("CreatedDate", typeof(DateTime)),  // Specifying column data types
        new DataColumn("GrandTotal", typeof(decimal)),
        new DataColumn("Locality")
            });

            // Adding rows to DataTable
            foreach (var order in orders)
            {
                dataTable.Rows.Add(order.Id, order.UserId, order.PaymentId, order.CreatedDate, order.GrandTotal, order.Locality);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",  // MIME type for .xlsx
                        fileName);
                }
            }
        }

    }
}
