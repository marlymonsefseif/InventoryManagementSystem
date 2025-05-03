using System.Linq.Expressions;
using InventoryManagementSystem.DTO;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Data
{
    public interface IGeneralRepository<T> where T : BaseModel
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        Task SaveChangesAsync();
        T SpecificGet(Expression<Func<T, bool>> expression);
    }
}
