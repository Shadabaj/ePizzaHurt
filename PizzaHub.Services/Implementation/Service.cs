using PizzaHub.Services.Interface;
using PizzaHurt.Repositories.Interfaces;

namespace PizzaHub.Services.Implementation
{
    public class Service<T> : IService<T> where T : class
    {

    IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }
        public void Add(T entity)
        {
            _repository.Add(entity);
        }

        public void Delete(object id)
        {
            _repository.Delete(id);
            _repository.SaveChanges();
        }

        public T Find(object id)
        {
         return _repository.Find(id);
          
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public void Remove(T entity)
        {
           _repository.Remove(entity);
            _repository.SaveChanges();
        }

        public void Update(T entity)
        {
          _repository.Update(entity);
          _repository.SaveChanges();
        }
    }
}
