using CityBookMVCOnionApplication.ViewModels.Position;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;
using CityBookMVCOnionApplication.ViewModels.Service;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface IServiceService
    {
        Task<ICollection<ItemServiceVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemServiceVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Service, object>>? orderExpression, int page = 1);
        Task<PaginationVM<ItemServiceVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemServiceVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetServiceVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateServiceVM create, ModelStateDictionary model);
        Task<UpdateServiceVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateServiceVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
