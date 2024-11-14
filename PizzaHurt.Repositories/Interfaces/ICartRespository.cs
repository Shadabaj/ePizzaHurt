using PizzaHurt.Models;
using PizzaHut.Core.Entities;


namespace PizzaHurt.Repositories.Interfaces
{
    public interface ICartRespository:IRepository<Cart>
    {
        Cart GetCart(Guid CartId);

        CartModel GetCartDetails(Guid CartId);

        int DeleteItem(Guid CartId,int ItemId);

        int UpdateQuantity(Guid CartId,int ItemId,int Quantity);

        int UpdateCart(Guid CartId, int UserId); 
    }
}
