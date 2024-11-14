using PizzaHurt.Models;
using PizzaHut.Core.Entities;


namespace PizzaHurt.Repositories.Interfaces
{
    public interface IActivityLogRepository
    {
        void Add(TrackActivityLog log);
    }
}
