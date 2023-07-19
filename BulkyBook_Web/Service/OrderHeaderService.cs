using BulkyBook_Utility;
using BulkyBook_Web.Models;
using BulkyBook_Web.Models.Dto;
using BulkyBook_Web.Models.VM;
using BulkyBook_Web.Service.IService;

namespace BulkyBook_Web.Service
{
    public class OrderHeaderService : BaseService, IOrderHeaderService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string categoryUrl;

        public OrderHeaderService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            categoryUrl = configuration.GetValue<string>("ServiceUrls:CategoryAPI");
        }

        //public Task<T> CreateAsync<T>(OrderHeaderCreateDTO dto, string token)
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        ApiType = SD.ApiType.POST,
        //        Data = dto,
        //        Url = categoryUrl + "/api/v1/OrderHeaderAPI/CreateOrderHeader",
        //        Token = token
        //    });
        //}

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = categoryUrl + "/api/v1/OrderHeaderAPI/DeleteOrderHeader" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = categoryUrl + "/api/v1/OrderHeaderAPI/GetOrderHeaders",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = categoryUrl + "/api/v1/OrderHeaderAPI/GetOrderHeader/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(OrderVM orderVM, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = orderVM,
                Url = categoryUrl + "/api/v1/OrderHeaderAPI/UpdateOrderHeader/" + orderVM.OrderHeader.Id,
                Token = token
            }) ;
        }
        public Task<T> StartProcessing<T>(OrderVM orderVM, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = orderVM,
                Url = categoryUrl + "/api/v1/OrderHeaderAPI/StartProcessing/" + orderVM.OrderHeader.Id,
                Token = token
            }) ;
        }

        public Task<T> ShipOrder<T>(OrderVM orderVM, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = orderVM,
                Url = categoryUrl + "/api/v1/OrderHeaderAPI/ShipOrder/" + orderVM.OrderHeader.Id,
                Token = token
            }) ;
        }

        public Task<T> CancelOrder<T>(OrderVM orderVM, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = orderVM,
                Url = categoryUrl + "/api/v1/OrderHeaderAPI/CancelOrder/" + orderVM.OrderHeader.Id,
                Token = token
            }) ;
        }
    }
}
