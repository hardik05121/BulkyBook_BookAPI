using BulkyBook_API.Models;
using System.Linq.Expressions;

namespace BulkyBook_API.Repository.IRepostiory
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        Task<ShoppingCart> UpdateAsync(ShoppingCart entity);
    }
}
