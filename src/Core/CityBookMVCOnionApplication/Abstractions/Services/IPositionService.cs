
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface IPositionService
    {
        Task<ICollection<ItemPositionVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemPositionVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Position, object>>? orderExpression, int page = 1);
        Task<PaginationVM<ItemPositionVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemPositionVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetPositionVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreatePositionVM create, ModelStateDictionary model);
        Task<UpdatePositionVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdatePositionVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
