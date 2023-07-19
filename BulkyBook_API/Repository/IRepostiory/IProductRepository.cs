using BulkyBook_API.Models;
using System.Linq.Expressions;

namespace BulkyBook_API.Repository.IRepostiory
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> UpdateAsync(Product entity);
    }
}
