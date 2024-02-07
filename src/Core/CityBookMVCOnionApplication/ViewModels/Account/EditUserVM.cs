using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels.Account
{
    public record EditUserVM(string UserName, string? Img)
    {
        public IFormFile? Photo { get; init; }
    }
    
}
