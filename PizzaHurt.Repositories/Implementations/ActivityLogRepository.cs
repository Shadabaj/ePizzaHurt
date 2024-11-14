using PizzaHurt.Repositories.Interfaces;
using PizzaHut.Core;
using PizzaHut.Core.Entities;


namespace PizzaHurt.Repositories.Implementations
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        protected AppDbContext _Db;

        public ActivityLogRepository(AppDbContext db) 
        {
            _Db=db;
        }
        public void Add(TrackActivityLog log)
        {
             _Db.Add(log);
             _Db.SaveChanges();

        }
    }
}
