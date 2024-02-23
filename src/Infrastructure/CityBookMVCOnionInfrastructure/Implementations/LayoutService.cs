using AutoMapper;
using CityBookMVCOnionApplication.Abstractions.Repositories;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CityBookMVCOnionInfrastructure.Implementations
{
    public class LayoutServices
    {
        private readonly ISettingRepository _repository;
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public LayoutServices(ISettingRepository repository, IBlogRepository blogRepository, IMapper mapper)
        {
            _repository = repository;
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            Dictionary<string, string> keyValuePairs = await _repository.GetAll(false).ToDictionaryAsync(p => p.Key, p => p.Value);
            return keyValuePairs;
        }

        public async Task<List<ItemBlogVM>> GetBlogsAsync()
        {
            List<ItemBlogVM> vMs = _mapper.Map<List<ItemBlogVM>>(_blogRepository.GetAll(false, $"{nameof(Blog.BlogImages)}").ToList());
            return vMs;
        }
    }
}