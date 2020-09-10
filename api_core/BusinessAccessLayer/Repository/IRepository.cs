using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Repository
{
    public interface IRepository<T>
    {

        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();

        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);

    }
}
