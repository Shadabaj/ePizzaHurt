using PizzaHurt.Repositories.Interfaces;
using PizzaHurt.Services.Interface;
using PizzaHut.Core.Entities;


namespace PizzaHurt.Services.Implementation
{
    public class CatalogService :ICatalogService
    {

        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<ItemType> _itemTypeRepository;
        private readonly IRepository<Category> _categoryRepository;

        public CatalogService(IRepository<Item> itemRepository, IRepository<ItemType> itemTypeRepository, IRepository<Category> categoryRepository)
        {
            _itemRepository= itemRepository;
            _itemTypeRepository= itemTypeRepository;
            _categoryRepository= categoryRepository;
        }
        public void AddItem(Item item)
        {
            _itemRepository.Add(item);
            _itemRepository.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            _itemRepository.Delete(id);
            _itemRepository.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
           return _categoryRepository.GetAll();
        }

        public Item GetItem(int id)
        {
            return _itemRepository.Find(id);
        }

        public IEnumerable<Item> GetItems()
        {
           return _itemRepository.GetAll().OrderBy(c=>c.CategoryId).ThenBy(i=>i.ItemTypeId);
        }

        public IEnumerable<ItemType> GetItemTypes()
        {
           return _itemTypeRepository.GetAll();
        }

        public void UpdateItem(Item item)
        {
            _itemRepository.Update(item);
            _itemRepository.SaveChanges();
        }
    }
}
