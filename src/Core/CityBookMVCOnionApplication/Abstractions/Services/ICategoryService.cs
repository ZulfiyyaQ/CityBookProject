using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<ICollection<ItemCategoryVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemCategoryVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Category, object>>? orderExpression, int page = 1);
        Task<PaginationVM<ItemCategoryVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemCategoryVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetCategoryVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCategoryVM create, ModelStateDictionary model);
        Task<UpdateCategoryVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateCategoryVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
       
    }
}
