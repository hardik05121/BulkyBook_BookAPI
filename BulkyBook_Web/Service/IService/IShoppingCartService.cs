using BulkyBook_Web.Models.Dto;
using BulkyBook_Web.Models.VM;

namespace BulkyBook_Web.Service.IService
{
    public interface IShoppingCartService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ShoppingCartCreateDTO dto,string token);
        Task<T> SummaryPOST<T>(string token);

        Task<T> UpdateAsync<T>(ShoppingCartUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int Id, string token);
    }
}
