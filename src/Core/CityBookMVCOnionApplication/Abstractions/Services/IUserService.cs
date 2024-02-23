using CityBookMVCOnionApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface IUserService
    {
        Task<PaginationVM<ItemUserVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemUserVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetUserVM> GetByIdAdminAsync(string id);
        Task<GetUserVM> GetByIdAsync(string id);
        Task<GetUserVM> GetByUserNameAdminAsync(string userName);
        Task<GetUserVM> GetByUserNameAsync(string userName);
        Task ReverseSoftDeleteAsync(string id);
        Task SoftDeleteAsync(string id);
        Task DeleteAsync(string id);
        Task GiveRoleModeratorAsync(string id);
        Task DeleteRoleModeratorAsync(string id);
        Task DeleteRoleAgentAsync(string id);
        
        Task<EditUserVM> EditUser(string id);
        Task<bool> EditUserAsync(string id, EditUserVM update, ModelStateDictionary model);
        Task ForgotPassword(string id, IUrlHelper url);
        Task<bool> ChangePassword(string id, string token, ChangePasswordVM changePassword, ModelStateDictionary model);
        
        
    }
}
