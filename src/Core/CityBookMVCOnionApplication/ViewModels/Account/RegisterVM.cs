using CityBookMVCOnionDomain.Enums;

namespace CityBookMVCOnionApplication.ViewModels
{
    public record RegisterVM(string Name,string Surname,string Username,string Email,
        string Password, string ConfirmPassword, string role,bool AllowTerms); 
    
}
