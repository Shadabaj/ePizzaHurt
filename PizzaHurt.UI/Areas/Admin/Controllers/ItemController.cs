using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaHurt.Services.Interface;
using PizzaHurt.UI.FileHelper;
using PizzaHurt.UI.Helpers;
using PizzaHurt.UI.Models;
using PizzaHut.Core;
using PizzaHut.Core.Entities;

namespace PizzaHurt.UI.Areas.Admin.Controllers
{
    public class ItemController : BaseController
    {
        private ICatalogService _catalogService;
        private AppDbContext _db;
        protected IFileHelper _Ifilehelper;


        public ItemController(ICatalogService catalogService,AppDbContext Db,IFileHelper ifilehelper)
        {
            this._catalogService = catalogService;
            this._db = Db;
            this._Ifilehelper = ifilehelper;
        }
        public IActionResult Index()
        {
            
            var items = _catalogService.GetItems().ToList();
            return View(items);
        }

        public IActionResult Create()
        {
            ViewBag.categories=_db.Categories.ToList();
         
            var itemtype = _db.ItemTypes.ToList();
            ViewBag.itemtype= new SelectList(itemtype, "Id", "Name");
            return View();
        }

        [ServiceFilter(typeof(CustomExceptionFilter))]
        [HttpPost]
        public IActionResult Create(ItemModel model)
        {
            model.ImageUrl = _Ifilehelper.uploadFile(model.File);
            ModelState.Remove("Id");
           
                Item data = new Item
                {
                    Name = model.Name,
                    Description = model.Description,
                    UnitPrice = model.UnitPrice,
                    CategoryId = model.CategoryId,
                   ItemTypeId = model.ItemTypeId,
                    ImageUrl = model.ImageUrl
                };
                _catalogService.AddItem(data);
                return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit (int id)
        {
            ViewBag.categories = _db.Categories.ToList();
            var itemtype = _db.ItemTypes.ToList();
            ViewBag.itemtype = new SelectList(itemtype, "Id", "Name");
            
            var item=_catalogService.GetItem(id);
            ItemModel model = new ItemModel
            {
                Id=item.Id,
                Name=item.Name,
                Description=item.Description,
                UnitPrice=item.UnitPrice,
                CategoryId=item.CategoryId,
                ItemTypeId=item.ItemTypeId,
                ImageUrl=item.ImageUrl
            };
            return View("Create",model);
        }

        [HttpPost]
        public IActionResult Edit(ItemModel model)
        {
            if (model.File != null)
            {
                // If a file is uploaded, delete the existing file (if any)
                _Ifilehelper.DeleteFile(model.ImageUrl);
                // Upload the new file and update the model's ImageUrl
                model.ImageUrl = _Ifilehelper.uploadFile(model.File);
            }

            Item data = new Item
            {
                Id = model.Id,
                Name = model.Name,
                UnitPrice = model.UnitPrice,
                CategoryId = model.CategoryId,
                ItemTypeId = model.ItemTypeId,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };

            _catalogService.UpdateItem(data);   
            return RedirectToAction("Index");
        }


        [Route("~/Admin/Item/Delete/{id}/{url}")]
        public IActionResult Delete(int id, string url)
        {
            url = url.Replace("%2F", "/");
            _catalogService.DeleteItem(id);
            _Ifilehelper.DeleteFile(url);
            return RedirectToAction("Index");
        }
    }
  }

