using CityBookMVCOnionApplication.ViewModels.Service;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;
using CityBookMVCOnionApplication.ViewModels.Tag;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface ITagService
    {
        Task<ICollection<ItemTagVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemTagVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Tag, object>>? orderExpression, int page = 1);
        Task<PaginationVM<ItemTagVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemTagVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetTagVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateTagVM create, ModelStateDictionary model);
        Task<UpdateTagVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateTagVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
