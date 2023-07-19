using BulkyBook_Utility;
using BulkyBook_Web.Models;
using BulkyBook_Web.Models.Dto;
using BulkyBook_Web.Models.VM;
using BulkyBook_Web.Service.IService;

namespace BulkyBook_Web.Service
{
    public class ShoppingCartService : BaseService, IShoppingCartService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string categoryUrl;

        public ShoppingCartService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            categoryUrl = configuration.GetValue<string>("ServiceUrls:CategoryAPI");

        }

        public Task<T> CreateAsync<T>(ShoppingCartCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = categoryUrl + "/api/v1/cartAPI/CreateShoppingCart",
                Token = token
            });
        }

        public Task<T> SummaryPOST<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                //Data = shoppingCartVM,
                Url = categoryUrl + "/api/v1/cartAPI/SummaryPOST",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int Id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = categoryUrl + "/api/v1/cartAPI/DeleteShoppingCart/" + Id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = categoryUrl + "/api/v1/cartAPI/GetShoppingCarts",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = categoryUrl + "/api/v1/cartAPI/GetShoppingCart" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(ShoppingCartUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = categoryUrl + "/api/v1/cartAPI/UpdateShoppingCart/" + dto.Id,
                Token = token
            }) ;
        }
    }
}
