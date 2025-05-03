using System.Linq.Expressions;
using InventoryManagementSystem.DTO;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Data
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : BaseModel
    {
        private readonly InventoryDbContext _context;
        public GeneralRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            T item = GetById(id);
            _context.Set<T>().Remove(item);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public T SpecificGet(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().FirstOrDefault(expression);
        }

        public T GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
