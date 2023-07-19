using BulkyBook_Web.Models.Dto;
using BulkyBook_Web.Models.VM;

namespace BulkyBook_Web.Service.IService
{
    public interface IOrderDetailService
    {
        Task<T> GetAllAsync<T>(int id,string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(OrderDetailCreateDTO dto,string token);
        Task<T> UpdateAsync<T>(OrderDetailUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
