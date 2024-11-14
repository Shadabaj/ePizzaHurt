using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaHurt.Models;
using PizzaHut.Core;
using System.Text;

namespace PizzaHurt.UI.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        private AppDbContext _Db;

        public ReportController(AppDbContext db)
        {
            _Db = db;
        }
        public IActionResult Index()
        {
           
            return View();
        }

        public async Task<IActionResult> ExportCsv(DateTime startdate, DateTime enddate)
        {
            if (enddate < startdate)
            {
                TempData["Message"] = "End date must be greater than or equal to start date.";
                return RedirectToAction("Index");
            }
            enddate = enddate.Date.AddDays(1).AddTicks(-1);
            var billDetails = await (from item in _Db.Items
                                     where item.CreatedDate >= startdate && item.CreatedDate <= enddate
                                     join itemtype in _Db.ItemTypes on item.ItemTypeId equals itemtype.Id
                                     join category in _Db.Categories on item.CategoryId equals category.Id
                                     select new ReportModel
                                     {
                                         Id = item.Id,
                                         Name = item.Name,
                                         Description = item.Description,
                                         UnitPrice = item.UnitPrice,
                                         CreatedDate = item.CreatedDate,
                                         ItemType = itemtype.Name,
                                         CategoryName = category.Name,
                                     }).ToListAsync();

            if (!billDetails.Any())
            {
               
                TempData["Message"] = "No records found for the selected date range..";
                return RedirectToAction("Index");
            }
            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,Description,UnitPrice,CreatedDate,ItemType,CategoryName");

            foreach (var detail in billDetails)
            {
                csv.AppendLine($"{detail.Id},{detail.Name},{detail.Description},{detail.UnitPrice},{detail.CreatedDate:yyyy-MM-dd},{detail.ItemType},{detail.CategoryName}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            var output = new FileContentResult(bytes, "text/csv")
            {
                FileDownloadName = "ItemsDetails.csv"
            };

            return output;
        }

    }
}
