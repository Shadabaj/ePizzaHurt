using PizzaHub.Services.Interface;
using PizzaHurt.Models;
using PizzaHut.Core.Entities;

namespace PizzaHurt.Services.Interface
{
    public interface ICartService:IService<Cart>
    {
        int GetCartCount(Guid CartId);

        CartModel GetCartDetails(Guid CartId);

        Cart AddItem(int UserId, Guid CartId, int ItemId, decimal UnitPrice, int Quantity);

        int DeleteItem(Guid CartId, int ItemId);

        int UpdateQuantity(Guid CartId, int ItemId, int Quantity);

        int UpdateCart(Guid CartId, int UserId);

    }
}
