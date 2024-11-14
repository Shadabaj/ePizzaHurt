using Microsoft.Extensions.Configuration;
using PizzaHub.Services.Implementation;
using PizzaHurt.Models;
using PizzaHurt.Repositories.Interfaces;
using PizzaHurt.Services.Interface;
using PizzaHut.Core.Entities;

namespace PizzaHurt.Services.Implementation
{
    public class CartService : Service<Cart>, ICartService
    {
        ICartRespository _cartRepo;
        IRepository<CartItem> _cartItemRepo;
        IConfiguration _configuration;

        public CartService(ICartRespository cartRepo, IRepository<CartItem> cartItemRepo,IConfiguration configuration) : base(cartRepo)
        {
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _configuration = configuration;
        }

        public Cart AddItem(int UserId, Guid CartId, int ItemId, decimal UnitPrice, int Quantity)
        {
            Cart cart = _cartRepo.GetCart(CartId);
            if (cart == null)
            {
                cart = new Cart();
                CartItem item = new CartItem { ItemId = ItemId, Quantity = Quantity, UnitPrice = UnitPrice, CartId = CartId };

                cart.Id = CartId;
                cart.UserId = UserId;
                cart.CreatedDate = DateTime.Now;
                cart.IsActive = true;

                cart.CartItems.Add(item);
                _cartRepo.Add(cart);
                _cartRepo.SaveChanges();
            }
            else
            {
                CartItem cartItem = cart.CartItems.Where(c => c.ItemId == ItemId).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity += Quantity;
                    _cartItemRepo.Update(cartItem);
                    _cartItemRepo.SaveChanges();
                }
                else
                {
                    CartItem item = new CartItem { ItemId = ItemId, Quantity = Quantity, UnitPrice = UnitPrice, CartId = CartId };

                    cart.CartItems.Add(item);

                    _cartItemRepo.Update(item);
                    _cartItemRepo.SaveChanges();
                }
            }
            return cart;
        }


        public int DeleteItem(Guid CartId, int ItemId)
        {
          return  _cartRepo.DeleteItem(CartId, ItemId);
        }

        public int GetCartCount(Guid CartId)
        {
          var cart=_cartRepo.GetCart(CartId);
            return cart!=null?cart.CartItems.Count() : 0;
        }

        public CartModel GetCartDetails(Guid CartId)
        {
            var model = _cartRepo.GetCartDetails(CartId);
            if (model != null && model.Items.Count > 0)
            {
                decimal SubTotal = 0;
                foreach (var item in model.Items)
                {
                    item.Total=item.UnitPrice* item.Quantity;
                    SubTotal += item.Total;
                }
                model.Total = SubTotal;
                model.Tax= Math.Round(model.Total * Convert.ToInt32(_configuration["Tax:GST"])/100,2);
                model.GrandTotal=model.Total + model.Tax;

            }
            return model;
        }

        public int UpdateCart(Guid CartId, int UserId)
        {
         return _cartRepo.UpdateCart(CartId,UserId);
        }

        public int UpdateQuantity(Guid CartId, int ItemId, int Quantity)
        {
           return _cartRepo.UpdateQuantity(CartId,ItemId,Quantity);
        }
    }
}
