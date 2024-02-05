using CityBookMVCOnionDomain.Enums;

namespace CityBookMVCOnionApplication.ViewModels.Account
{
    public record LoginVM( string UsernameOrEmail, string Password, bool IsRemembered);
   
}
