using BulkyBook_API.Models;

namespace BulkyBook_API.Repository.IRepostiory
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> UpdateAsync(ApplicationUser entity);
    }
}
