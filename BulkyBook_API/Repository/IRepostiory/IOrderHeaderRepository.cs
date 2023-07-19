using BulkyBook_API.Models;
using BulkyBook_API.Models.Dto;
using System.Linq.Expressions;

namespace BulkyBook_API.Repository.IRepostiory
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        Task<OrderHeader> UpdateAsync(OrderHeader entity);
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
        void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId);
    }
}