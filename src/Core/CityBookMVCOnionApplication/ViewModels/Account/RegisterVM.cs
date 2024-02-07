using CityBookMVCOnionDomain.Enums;

namespace CityBookMVCOnionApplication.ViewModels.Account
{
    public record RegisterVM(string Name,string Surname,string Username,string Email,
        string Password, string ConfirmPassword, string role,bool AllowTerms);
    
}
