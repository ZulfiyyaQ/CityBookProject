using CityBookMVCOnionApplication.ViewModels.Account;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterVM register, ModelStateDictionary model, IUrlHelper url);
        Task<bool> LogInAsync(LoginVM login, ModelStateDictionary model);
        Task LogOutAsync();
        Task<bool> ConfirmEmail(string token, string email);
    }
}
