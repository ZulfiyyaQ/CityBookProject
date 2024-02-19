using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels
{
    public record EditUserVM(string UserName, string? Image, string? Face, string? Tvit, string? Link, string? Inst, string? Website, string? About)
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public string Address { get; init; }
        public IFormFile? Photo { get; init; }
    }
    
}
