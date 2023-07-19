using BulkyBook_API.Data;
using BulkyBook_API.Models;
using BulkyBook_API.Repository.IRepostiory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook_API.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<ShoppingCart> UpdateAsync(ShoppingCart entity)
        {
            _db.ShoppingCarts.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
