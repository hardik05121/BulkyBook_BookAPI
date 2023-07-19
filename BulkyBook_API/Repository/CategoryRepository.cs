using BulkyBook_API.Data;
using BulkyBook_API.Models;
using BulkyBook_API.Repository.IRepostiory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook_API.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<Category> UpdateAsync(Category entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Categories.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
