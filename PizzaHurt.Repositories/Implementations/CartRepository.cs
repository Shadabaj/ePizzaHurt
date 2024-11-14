using Microsoft.EntityFrameworkCore;
using PizzaHurt.Models;
using PizzaHurt.Repositories.Interfaces;
using PizzaHut.Core;
using PizzaHut.Core.Entities;


namespace PizzaHurt.Repositories.Implementations
{
    public class CartRepository :Repository<Cart>, ICartRespository
    {

        public CartRepository(AppDbContext _Db):base(_Db) { }

        public int DeleteItem(Guid CartId, int ItemId)
        {
            var item = _Db.CartItems.Where(c => c.CartId == CartId && c.Id == ItemId).FirstOrDefault();
            if (item != null)
            {
                _Db.CartItems.Remove(item);
               _Db.SaveChanges();
            }
            return 0;
        }

        public Cart GetCart(Guid CartId)
        {
            return _Db.Carts.Include(c => c.CartItems).Where(c => c.Id == CartId && c.IsActive == true).FirstOrDefault();
        }

        public CartModel GetCartDetails(Guid CartId)
        {
            var model = (from c in _Db.Carts
                         where c.Id == CartId && c.IsActive == true
                         select new CartModel
                         {
                             Id = c.Id,
                             UserId = c.UserId,
                             CreatedDate = c.CreatedDate,
                             Items = (from CartItem in _Db.CartItems
                                      join item in _Db.Items
                                      on CartItem.ItemId equals item.Id
                                      where CartItem.CartId == CartId
                                      select new ItemModel
                                      {
                                          Id = CartItem.Id,
                                          Quantity = CartItem.Quantity,
                                          UnitPrice = CartItem.UnitPrice,
                                          ItemId = item.Id,
                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl
                                      }).ToList()
                         }).FirstOrDefault();
            return model;
        }

        public int UpdateCart(Guid CartId, int UserId)
        {
            Cart cart = GetCart(CartId);
            cart.UserId = UserId;
            return _Db.SaveChanges();
        }

        public int UpdateQuantity(Guid cartId, int itemId, int Quantity)
        {
            bool flag = false;
            var cart = GetCart(cartId);
            if (cart != null)
            {
                var cartItems = cart.CartItems.ToList();
                for (int i = 0; i < cartItems.Count; i++)
                {
                    if (cartItems[i].Id == itemId)
                    {
                        cartItems[i].Quantity += Quantity;
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    cart.CartItems = cartItems;
                    return _Db.SaveChanges();
                }
            }
            return 0;
        }
    }
}
