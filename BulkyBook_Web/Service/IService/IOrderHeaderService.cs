using BulkyBook_Web.Models.Dto;
using BulkyBook_Web.Models.VM;

namespace BulkyBook_Web.Service.IService
{
    public interface IOrderHeaderService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
      //  Task<T> CreateAsync<T>(OrderHeaderCreateDTO dto,string token);
        Task<T> UpdateAsync<T>(OrderVM orderVM, string token);
        Task<T> StartProcessing<T>(OrderVM orderVM, string token); 
        Task<T> ShipOrder<T>(OrderVM orderVM, string token); 
        Task<T> CancelOrder<T>(OrderVM orderVM, string token);
      //  Task<T> DeleteAsync<T>(int id, string token);
    }
}
