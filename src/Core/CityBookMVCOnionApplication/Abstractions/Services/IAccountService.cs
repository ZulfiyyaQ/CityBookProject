using CityBookMVCOnionApplication.ViewModels.Account;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace CityBookMVCOnionApplication.Abstractions.Services
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterVM register, ModelStateDictionary model);
        Task<bool> LogInAsync(LoginVM login, ModelStateDictionary model);
        Task LogOutAsync();
    }
}
