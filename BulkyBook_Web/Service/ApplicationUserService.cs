using BulkyBook_Utility;
using BulkyBook_Web.Models;
using BulkyBook_Web.Models.Dto;
using BulkyBook_Web.Service.IService;

namespace BulkyBook_Web.Service{    public class ApplicationUserService : BaseService, IApplicationUserService    {        private readonly IHttpClientFactory _clientFactory;        private string categoryUrl;        public ApplicationUserService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)        {            _clientFactory = clientFactory;            categoryUrl = configuration.GetValue<string>("ServiceUrls:CategoryAPI");        }

        //public Task<T> CreateAsync<T>(ApplicationUserCreateDTO dto, string token)
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        ApiType = SD.ApiType.POST,
        //        Data = dto,
        //        Url = categoryUrl + "/api/v1/ApplicationUserAPI",
        //        Token = token
        //    });
        //}

        //public Task<T> DeleteAsync<T>(int id, string token)
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        ApiType = SD.ApiType.DELETE,
        //        Url = categoryUrl + "/api/v1/ApplicationUserAPI/" + id,
        //        Token = token
        //    });
        //}

        public Task<T> GetAllAsync<T>(string token)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/applicationUserAPI",                Token = token            });        }        public Task<T> GetAsync<T>(string Id, string token)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/applicationUserAPI/" + Id,                Token = token            });        }        //public Task<T> UpdateAsync<T>(ApplicationUserUpdateDTO dto, string token)        //{        //    return SendAsync<T>(new APIRequest()        //    {        //        ApiType = SD.ApiType.PUT,        //        Data = dto,        //        Url = categoryUrl + "/api/v1/ApplicationUserAPI/" + dto.Id,        //        Token = token        //    });        //}    }}
