using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface IHomeReviewService
    {
        Task<ICollection<ItemHomeReviewVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemHomeReviewVM>> GetAllWhereByOrderAsync(int take, Expression<Func<HomeReview, object>>? orderExpression, int page = 1);
        Task<PaginationVM<ItemHomeReviewVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemHomeReviewVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetHomeReviewVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateHomeReviewVM create, ModelStateDictionary model);
        Task<UpdateHomeReviewVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateHomeReviewVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
        
        
    }
    
}
