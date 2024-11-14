using Microsoft.AspNetCore.Mvc;
using PizzaHut.Core;

namespace PizzaHurt.UI.Areas.Admin.Controllers
{
    public class ChartController : BaseController
    {

        private AppDbContext _db;

        public ChartController(AppDbContext db) 
        { 
        _db= db;    
        }

        public IActionResult Index()
        {
         
            return View();
        }

        [HttpPost]
        public List<object> GetSalesData()
        {
            List<object> data = new List<object>();

            List<decimal> labels = _db.PaymentDetails.Select(p => p.Total).ToList();

            data.Add(labels);

            var SalesNumber = _db.PaymentDetails.Join(_db.Users, p => p.UserId, u => u.Id, (p, u) => new
            {
                u.Name
            }).ToList();

            data.Add(SalesNumber);

            return data;
        }
    }
}
