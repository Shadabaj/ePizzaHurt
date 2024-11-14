using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PizzaHub.Services.Interface;
using PizzaHurt.UI.Models;
using PizzaHut.Core.Entities;
using System.Diagnostics;

namespace PizzaHurt.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IService<Item> _itemService;

        private IMemoryCache _memoryCache;
        public HomeController(ILogger<HomeController> logger,IService<Item> item,IMemoryCache memoryCache)
        {
            _logger = logger;
            _itemService = item;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {

            /*try
            {
                int x = 0, y = 3;
                int z = y / x;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }*/

            string key = "catalog";
            var items = _memoryCache.GetOrCreate(key, entry =>
            {
                entry.AbsoluteExpiration = DateTime.Now.AddHours(12);
                entry.SlidingExpiration = TimeSpan.FromMinutes(15);
                return _itemService.GetAll();
            });

            //var items=_itemService.GetAll();
            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
