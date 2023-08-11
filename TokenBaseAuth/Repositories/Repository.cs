using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using TokenBaseAuth.Data;

namespace TokenBaseAuth.Repositories
{
    public class Repository<T> : ControllerBase, IRepository<T>, IUnitOfWork where T : class
    {
        protected AppDbContext _context;
        IDbContextTransaction transaction = null;
        public Repository(AppDbContext companyContext)
        {
            _context = companyContext;
            transaction = _context.Database.BeginTransaction();
        }
        [NonAction]
        public List<T> Get() => _context.Set<T>().ToList();
        [NonAction]
        public bool Add(T model)
        {
            _context.Set<T>().Add(model);
            return true;
        }
        [NonAction]
        public bool Add<A>(A model) where A : class
        {
            _context.Set<A>().Add(model);
            return true;
        }
        [NonAction]
        public bool Remove(T model)
        {
            _context.Set<T>().Remove(model);
            return true;
        }
        [NonAction]
        public int Save() => _context.SaveChanges();
        [NonAction]
        public bool Commit(bool state = true)
        {
            Save();
            if (state)
                transaction.Commit();
            else
                transaction.Rollback();

            Dispose();
            return true;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
