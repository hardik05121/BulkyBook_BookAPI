using BulkyBook_Utility;
using BulkyBook_Web.Models;
using BulkyBook_Web.Models.Dto;
using BulkyBook_Web.Models.VM;
using BulkyBook_Web.Service.IService;

namespace BulkyBook_Web.Service
{
    public class OrderDetailService : BaseService, IOrderDetailService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string categoryUrl;

        public OrderDetailService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            categoryUrl = configuration.GetValue<string>("ServiceUrls:CategoryAPI");

        }

        public Task<T> CreateAsync<T>(OrderDetailCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = categoryUrl + "/api/v1/OrderDetailAPI/CreateOrderDetail",
                Token = token
            });
        }



        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = categoryUrl + "/api/v1/OrderDetailAPI/DeleteOrderDetail" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = categoryUrl + "/api/v1/OrderDetailAPI/GetOrderDetails/" + id,
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = categoryUrl + "/api/v1/OrderDetailAPI/GetOrderDetail/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(OrderDetailUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = categoryUrl + "/api/v1/OrderDetailAPI/UpdateOrderDetail" + dto.Id,
                Token = token
            }) ;
        }
    }
}
