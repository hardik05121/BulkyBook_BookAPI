using BulkyBook_API.Data;
using BulkyBook_API.Models;
using BulkyBook_API.Repository.IRepostiory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook_API.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<OrderDetail> UpdateAsync(OrderDetail entity)
        {
            _db.OrderDetails.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
