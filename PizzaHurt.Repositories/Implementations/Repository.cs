using PizzaHurt.Repositories.Interfaces;
using PizzaHut.Core;


namespace PizzaHurt.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {

       protected AppDbContext _Db;

        public Repository(AppDbContext Db)
        {
            _Db = Db;
        }
        public void Add(T entity)
        {
            _Db.Set<T>().Add(entity);
        }

        public void Delete(object id)
        {
            T entity= _Db.Set<T>().Find(id);

            if (entity!=null)
            {
                //_Db.Set<T>().Remove(entity);
                this.Remove(entity);
            }
        }

        public T Find(object id)
        {
           return _Db.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _Db.Set<T>().ToList();
        }

        public int SaveChanges()
        {
            return _Db.SaveChanges();
        }

       public void Remove(T entity)
        {
            _Db.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _Db.Set<T>().Update(entity);
        }
    }
}
