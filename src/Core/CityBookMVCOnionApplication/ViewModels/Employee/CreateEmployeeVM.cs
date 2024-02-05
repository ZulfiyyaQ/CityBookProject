using Microsoft.AspNetCore.Http;

namespace CityBookMVCOnionApplication.ViewModels.Employee
{
    public record CreateEmployeeVM
    {
        public IFormFile Photo { get; init; }
        public string Surname { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Face { get; init; }
        public string Tvit { get; init; }
        public string Link { get; init; }
       
        public int PositionId { get; init; }
        
    }
}
