using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface IPlaceService
    {
        Task<ICollection<ItemPlaceVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemPlaceVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Place, object>>? orderExpression, int page = 1);
        Task<PaginationVM<ItemPlaceVM>> GetFilteredAsync(string? search, int take, int page, int order, int? categoryId);
        Task<PaginationVM<ItemPlaceVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order, int? categoryId);
        Task<GetPlaceVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreatePlaceVM create, ModelStateDictionary model, ITempDataDictionary tempData);
        Task<UpdatePlaceVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdatePlaceVM update, ModelStateDictionary model, ITempDataDictionary tempData);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
        Task CreatePopulateDropdowns(CreatePlaceVM create);
        Task UpdatePopulateDropdowns(UpdatePlaceVM update);
        Task<bool> AddReservation(int id, string dayOrMonth, int? persons, string reservationDate,
                  string? reservationDateTo, string about, ITempDataDictionary tempData);
        Task AcceptReservation(int id);
        Task CanceledReservation(int id);
        Task<bool> Review(int id, int rating, string comment, ModelStateDictionary model);
        Task<PaginationVM<PlaceFilterVM>> GetAllWhereByOrderFilterAsync(int take, int page, Expression<Func<Place, object>>? orderExpression = null);
    }
}
