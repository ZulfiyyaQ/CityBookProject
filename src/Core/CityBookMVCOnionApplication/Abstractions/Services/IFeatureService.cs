
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;
using CityBookMVCOnionApplication.ViewModels.Feature;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface IFeatureService
    {
        Task<ICollection<ItemFeatureVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemFeatureVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Feature, object>>? orderExpression, int page = 1);
        Task<PaginationVM<ItemFeatureVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemFeatureVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetFeatureVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateFeatureVM create, ModelStateDictionary model);
        Task<UpdateFeatureVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateFeatureVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
