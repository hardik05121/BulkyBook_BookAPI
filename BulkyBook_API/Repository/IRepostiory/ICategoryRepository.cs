using BulkyBook_API.Models;
using System.Linq.Expressions;

namespace BulkyBook_API.Repository.IRepostiory
{
    public interface ICategoryRepository : IRepository<Category>
    {
      
        Task<Category> UpdateAsync(Category entity);
  
    }
}
