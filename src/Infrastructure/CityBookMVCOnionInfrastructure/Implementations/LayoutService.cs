using CityBookMVCOnionApplication.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CityBookMVCOnionInfrastructure.Implementations
{
    public class LayoutServices
    {
        private readonly ISettingRepository _repository;

        public LayoutServices(ISettingRepository repository)
        {
            _repository = repository;
        }

        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            Dictionary<string, string> keyValuePairs = await _repository.GetAll(false).ToDictionaryAsync(p => p.Key, p => p.Value);
            return keyValuePairs;
        }
    }
}
