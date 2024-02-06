using CityBookMVCOnionApplication.ViewModels.Setting;
using CityBookMVCOnionApplication.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface ISettingService
    {
        Task<PaginationVM<ItemSettingVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<UpdateSettingVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateSettingVM update, ModelStateDictionary model);
    }
}
