﻿using BulkyBook_Web.Models.Dto;

namespace BulkyBook_Web.Service.IService
{
    public interface ICategoryService
    {
      
            Task<T> GetAllAsync<T>(string token);
            Task<T> GetAsync<T>(int id, string token);
            Task<T> CreateAsync<T>(CategoryCreateDTO dto, string token);
            Task<T> UpdateAsync<T>(CategoryUpdateDTO dto, string token);
            Task<T> DeleteAsync<T>(int id, string token);
        
    }
}
