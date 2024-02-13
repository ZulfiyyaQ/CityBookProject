using CityBookMVCOnionDomain.Enums;

namespace CityBookMVCOnionApplication.ViewModels
{
    public record LoginVM( string UsernameOrEmail, string Password, bool IsRemembered);
   
}
