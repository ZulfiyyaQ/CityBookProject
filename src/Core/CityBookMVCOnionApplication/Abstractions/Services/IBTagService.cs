using CityBookMVCOnionApplication.ViewModels.Blog;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;
using CityBookMVCOnionApplication.ViewModels.BTag;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface IBTagService
    {
        Task<ICollection<ItemBTagVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemBTagVM>> GetAllWhereByOrderAsync(int take, Expression<Func<BTag, object>>? orderExpression, int page = 1);
        Task<PaginationVM<ItemBTagVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemBTagVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetBTagVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateBTagVM create, ModelStateDictionary model);
        Task<UpdateBTagVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateBTagVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
