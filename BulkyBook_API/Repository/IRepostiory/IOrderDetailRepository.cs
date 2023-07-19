using BulkyBook_API.Models;
using BulkyBook_API.Models.Dto;
using System.Linq.Expressions;

namespace BulkyBook_API.Repository.IRepostiory
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<OrderDetail> UpdateAsync(OrderDetail entity);
    }
}
