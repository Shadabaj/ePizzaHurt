using Microsoft.AspNetCore.Mvc;
using PizzaHurt.Services.Interface;

namespace PizzaHurt.UI.ViewComponents
{
    public class PizzaMenuViewComponent:ViewComponent
    {

       private ICatalogService _catalogService;

        public PizzaMenuViewComponent(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public IViewComponentResult Invoke()
        {
            var items=_catalogService.GetItems().ToList();
            return View("~/Views/Shared/_PizzaMenu.cshtml", items);
        }



    }
}
