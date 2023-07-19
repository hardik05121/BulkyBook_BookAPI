using AutoMapper;
using Azure;
using BulkyBook_API.Models;
using BulkyBook_API.Models.Dto;
using BulkyBook_API.Repository.IRepostiory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace BulkyBook_BookAPI.Controllers.v1
{
    //var claimsIdentity = (ClaimsIdentity)User.Identity;
    //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
    //string ApplicationUserId = userId;

    [Route("api/v{version:apiVersion}/ApplicationUserAPI")]
    [ApiController]
    [ApiVersion("1.0")]

    public class ApplicationUserAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IApplicationUserRepository _dbApplicationUser;

        private readonly IMapper _mapper;
        public ApplicationUserAPIController(IApplicationUserRepository dbApplicationUser, IMapper mapper)
        {
            _dbApplicationUser = dbApplicationUser;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetApplicationUsers()
        {
            try
            {
                IEnumerable<ApplicationUser> applicationUserList = await _dbApplicationUser.GetAllAsync();
                _response.Result = _mapper.Map<List<ApplicationUserDTO>>(applicationUserList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpGet("{Id}", Name = "GetApplicationUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetApplicationUser(string Id)
        {
            try
            {
                //if (Id == 0)
                //{
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    return BadRequest(_response);
                //}
                var applicationUser = await _dbApplicationUser.GetAsync(u => u.Id == Id);
                if (applicationUser == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ApplicationUserDTO>(applicationUser);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


    }

}