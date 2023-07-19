using BulkyBook_API.Data;
using BulkyBook_API.Models;
using BulkyBook_API.Repository.IRepostiory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook_API.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<ApplicationUser> UpdateAsync(ApplicationUser entity)
        {
         
            _db.ApplicationUsers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
