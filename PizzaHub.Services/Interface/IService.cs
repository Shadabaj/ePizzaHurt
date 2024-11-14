

namespace PizzaHub.Services.Interface
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Find(object id);

        void Remove(T entity);

        void Delete(object id); 

        void Add(T Entity);

        void Update(T entity);

    }
}
