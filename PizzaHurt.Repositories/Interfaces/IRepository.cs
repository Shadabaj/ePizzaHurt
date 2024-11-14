

namespace PizzaHurt.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {

        IEnumerable<T> GetAll();

        T Find(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete (object id);

        void Remove(T entity);

        int SaveChanges();

    }
}
