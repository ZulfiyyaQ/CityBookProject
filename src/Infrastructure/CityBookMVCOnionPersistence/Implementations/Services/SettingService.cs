using AutoMapper;
using CityBookMVCOnionApplication.Abstractions.Repositories;
using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionApplication.ViewModels.Setting;
using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionInfrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CityBookMVCOnionPersistence.Implementations.Services
{
    public class SettingService : ISettingService
    {
        private readonly IMapper _mapper;
        private readonly ISettingRepository _repository;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<User> _userManager;

        public SettingService(ISettingRepository repository, IHttpContextAccessor http, UserManager<User> userManager, IMapper mapper)
        {
            _repository = repository;
            _http = http;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<PaginationVM<ItemSettingVM>> GetFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            double count = await _repository.CountAsync();

            ICollection<Setting> items = new List<Setting>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Key.ToLower().Contains(search.ToLower()) : true,
                        x => x.Key, false, false, (page - 1) * take, take, false).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(expression: x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Key.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, false, (page - 1) * take, take, false).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Key.ToLower().Contains(search.ToLower()) : true,
                        x => x.Key, true, false, (page - 1) * take, take, false).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Key.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, false, (page - 1) * take, take, false).ToListAsync();
                    break;
            }

            ICollection<ItemSettingVM> vMs = _mapper.Map<ICollection<ItemSettingVM>>(items);

            PaginationVM<ItemSettingVM> pagination = new PaginationVM<ItemSettingVM>
            {
                Take = take,
                Search = search,
                Order = order,
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / take),
                Items = vMs
            };

            return pagination;
        }

        public async Task<UpdateSettingVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Setting item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateSettingVM update = new UpdateSettingVM { Key = item.Key, Value = item.Value };

            return update;
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateSettingVM update, ModelStateDictionary model)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Setting item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            if (await _repository.CheckUniqueAsync(x => x.Key.ToLower().Trim() == update.Key.ToLower().Trim() && x.Id != id))
            {
                model.AddModelError("Key", "Key is exists");
                return false;
            }
            item.Value = update.Value;
            //item.CreatedBy = user.UserName;
            _repository.Update(item);
            await _repository.SaveChanceAsync();
            return true;
        }
    }
}
