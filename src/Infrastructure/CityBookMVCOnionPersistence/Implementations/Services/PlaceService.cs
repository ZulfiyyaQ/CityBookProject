using AutoMapper;
using CityBookMVCOnionApplication.Abstractions.Repositories;
using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionInfrastructure.Exceptions;
using CityBookMVCOnionInfrastructure.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CityBookMVCOnionPersistence.Implementations.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IMapper _mapper;
        private readonly IPlaceRepository _repository;
        private readonly ITagRepository _tagRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _http;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<User> _userManager;

        public PlaceService(IMapper mapper, IPlaceRepository repository, ITagRepository tagRepository,
            IFeatureRepository featureRepository, IHttpContextAccessor http, IWebHostEnvironment env,
            ICategoryRepository categoryRepository, UserManager<User> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _tagRepository = tagRepository;
            _featureRepository = featureRepository;
            _http = http;
            _env = env;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }
        public async Task CreatePopulateDropdowns(CreatePlaceVM create)
        {
            create.Categories = _mapper.Map<List<IncludeCategoryVM>>(await _categoryRepository.GetAll().ToListAsync());
            create.Features = _mapper.Map<List<IncludeFeatureVM>>(await _featureRepository.GetAll().ToListAsync());
            create.Tags = _mapper.Map<List<IncludeTagVM>>(await _tagRepository.GetAll().ToListAsync());
        }
        public async Task UpdatePopulateDropdowns(UpdatePlaceVM update)
        {
            update.Categories = _mapper.Map<List<IncludeCategoryVM>>(await _categoryRepository.GetAll().ToListAsync());
            update.Features = _mapper.Map<List<IncludeFeatureVM>>(await _featureRepository.GetAll().ToListAsync());
            update.Tags = _mapper.Map<List<IncludeTagVM>>(await _tagRepository.GetAll().ToListAsync());
        }

        public async Task<bool> CreateAsync(CreatePlaceVM create, ModelStateDictionary model, ITempDataDictionary tempData)
        {
            if (!model.IsValid)
            {
                await CreatePopulateDropdowns(create);
                return false;
            }

            if (!await _categoryRepository.CheckUniqueAsync(x => x.Id == create.CategoryId))
            {
                await CreatePopulateDropdowns(create);
                return false;
            }
            foreach (int featureId in create.FeatureIds)
            {
                if (!await _featureRepository.CheckUniqueAsync(x => x.Id == featureId))
                {
                    await CreatePopulateDropdowns(create);
                    return false;
                }
            }
            foreach (int exteriorTypeId in create.TagIds)
            {
                if (!await _tagRepository.CheckUniqueAsync(x => x.Id == exteriorTypeId))
                {
                    await CreatePopulateDropdowns(create);
                    return false;
                }
            }

            Place item = _mapper.Map<Place>(create);

            item.PlaceFeatures = create.FeatureIds.Select(id => new PlaceFeature { FeatureId = id }).ToList();
            item.PlaceTags = create.TagIds.Select(id => new PlaceTag { TagId = id }).ToList();


            if (item.PlaceImages == null) item.PlaceImages = new List<PlaceImage>();

            tempData["Message"] = "";

            foreach (IFormFile photo in create.Photos)
            {
                if (!photo.ValidateType())
                {
                    tempData["Message"] += $"<p class=\"text-danger\">{photo.Name} type is not suitable</p>";
                    continue;
                }

                if (!photo.ValidataSize(10))
                {
                    tempData["Message"] += $"<p class=\"text-danger\">{photo.Name} the size is not suitable</p>";
                    continue;
                }

                item.PlaceImages.Add(new PlaceImage
                {
                    Url = await photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
                });
            }
            User user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);
            item.UserId = user.Id;
            //item.CreatedBy = user.UserName;

            await _repository.AddAsync(item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.PlaceFeatures)}.{nameof(PlaceFeature.Feature)}",
                $"{nameof(Place.PlaceTags)}.{nameof(PlaceTag.Tag)}",
                $"{nameof(Place.PlaceImages)}" };
            Place item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");
            foreach (var image in item.PlaceImages)
            {
                image.Url.DeleteFile(_env.WebRootPath, "assets", "images");
            }
            _repository.Delete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemPlaceVM>> GetAllWhereAsync(int take, int page = 1)
        {
            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.PlaceFeatures)}.{nameof(PlaceFeature.Feature)}",
                $"{nameof(Place.PlaceTags)}.{nameof(PlaceTag.Tag)}",
                $"{nameof(Place.PlaceImages)}" };
            ICollection<Place> items = await _repository
                    .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemPlaceVM> vMs = _mapper.Map<ICollection<ItemPlaceVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemPlaceVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Place, object>>? orderExpression, int page)
        {
            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.PlaceFeatures)}.{nameof(PlaceFeature.Feature)}",
                $"{nameof(Place.PlaceTags)}.{nameof(PlaceTag.Tag)}",
                $"{nameof(Place.PlaceImages)}" };
            ICollection<Place> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemPlaceVM> vMs = _mapper.Map<ICollection<ItemPlaceVM>>(items);

            return vMs;
        }
        public async Task<PaginationVM<ItemPlaceVM>> GetFilteredAsync(string? search, int take, int page, int order, int? categoryId)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.PlaceFeatures)}.{nameof(PlaceFeature.Feature)}",
                $"{nameof(Place.PlaceTags)}.{nameof(PlaceTag.Tag)}",
                $"{nameof(Place.PlaceImages)}" };
            double count = await _repository.CountAsync();

            ICollection<Place> items = new List<Place>();
            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => categoryId != null ? x.CategoryId == categoryId : true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, false, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => categoryId != null ? x.CategoryId == categoryId : true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => categoryId != null ? x.CategoryId == categoryId : true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, true, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => categoryId != null ? x.CategoryId == categoryId : true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            PaginationVM<ItemPlaceVM> pagination = new PaginationVM<ItemPlaceVM>
            {
                Take = take,
                Search = search,
                Order = order,
                CategoryId = categoryId,
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / take),
                Items = _mapper.Map<List<ItemPlaceVM>>(items)
            };

            return pagination;
        }

        public async Task<PaginationVM<ItemPlaceVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order, int? categoryId)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.PlaceFeatures)}.{nameof(PlaceFeature.Feature)}",
                $"{nameof(Place.PlaceTags)}.{nameof(PlaceTag.Tag)}",
                $"{nameof(Place.PlaceImages)}" };
            double count = await _repository.CountAsync();

            ICollection<Place> items = new List<Place>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => categoryId != null ? x.CategoryId == categoryId : true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => categoryId != null ? x.CategoryId == categoryId : true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => categoryId != null ? x.CategoryId == categoryId : true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => categoryId != null ? x.CategoryId == categoryId : true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;

            }

            PaginationVM<ItemPlaceVM> pagination = new PaginationVM<ItemPlaceVM>
            {
                Take = take,
                Search = search,
                Order = order,
                CategoryId = categoryId,
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / take),
                Items = _mapper.Map<List<ItemPlaceVM>>(items)
            };

            return pagination;
        }

        public async Task<GetPlaceVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.PlaceFeatures)}.{nameof(PlaceFeature.Feature)}",
                $"{nameof(Place.PlaceTags)}.{nameof(PlaceTag.Tag)}",
                $"{nameof(Place.PlaceImages)}" };
            Place item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetPlaceVM get = _mapper.Map<GetPlaceVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Place item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChanceAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Place item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChanceAsync();
        }

        public async Task<bool> UpdatePostAsync(int id, UpdatePlaceVM update, ModelStateDictionary model, ITempDataDictionary tempData)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.PlaceFeatures)}.{nameof(PlaceFeature.Feature)}",
                $"{nameof(Place.PlaceTags)}.{nameof(PlaceTag.Tag)}",
                $"{nameof(Place.PlaceImages)}" };
            Place item = await _repository.GetByIdAsync(id, includes: includes);
            update.Images = _mapper.Map<ICollection<IncludePlaceImageVM>>(item.PlaceImages);
            if (item == null) throw new NotFoundException("Your request was not found");

            if (!model.IsValid)
            {
                await UpdatePopulateDropdowns(update);
                return false;
            }

            if (!await _categoryRepository.CheckUniqueAsync(x => x.Id == update.CategoryId))
            {
                await UpdatePopulateDropdowns(update);
                return false;
            }
            foreach (int featureId in update.FeatureIds)
            {
                if (!await _featureRepository.CheckUniqueAsync(x => x.Id == featureId))
                {
                    await UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            foreach (int exteriorTypeId in update.TagIds)
            {
                if (!await _tagRepository.CheckUniqueAsync(x => x.Id == exteriorTypeId))
                {
                    await UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            ICollection<PlaceFeature> featureToRemove = item.PlaceFeatures
                .Where(ps => !update.FeatureIds.Contains(ps.FeatureId)).ToList();
            foreach (var featureRemove in featureToRemove)
            {
                item.PlaceFeatures.Remove(featureRemove);
                //_repository.DeleteFeatures(featureRemove);
            }

            ICollection<PlaceFeature> featureToAdd = update.FeatureIds
                .Except(item.PlaceFeatures.Select(ps => ps.FeatureId))
                .Select(featureId => new PlaceFeature { FeatureId = featureId })
                .ToList();
            foreach (var featureAdd in featureToAdd)
            {
                item.PlaceFeatures.Add(featureAdd);
            }

            ICollection<PlaceTag> exteriorTypeToRemove = item.PlaceTags
                .Where(ps => !update.TagIds.Contains(ps.TagId)).ToList();
            foreach (var exteriorTypeRemove in exteriorTypeToRemove)
            {
                item.PlaceTags.Remove(exteriorTypeRemove);
                //_repository.DeleteExteriorType(exteriorTypeRemove);
            }

            ICollection<PlaceTag> exteriorTypeToAdd = update.TagIds
                .Except(item.PlaceTags.Select(ps => ps.TagId))
                .Select(exteriorTypeId => new PlaceTag { TagId = exteriorTypeId })
                .ToList();
            foreach (var exteriorTypeAdd in exteriorTypeToAdd)
            {
                item.PlaceTags.Add(exteriorTypeAdd);
            }

            if (item.PlaceImages == null) item.PlaceImages = new List<PlaceImage>();

            if (update.ImageIds == null) update.ImageIds = new List<int>();

            ICollection<PlaceImage> remove = item.PlaceImages
                    .Where(pi => !update.ImageIds.Exists(imgId => imgId == pi.Id)).ToList();

            foreach (var image in remove)
            {
                image.Url.DeleteFile(_env.WebRootPath, "assets", "images");
                item.PlaceImages.Remove(image);
            }

            tempData["Message"] = "";

            if (update.Photos != null)
            {
                foreach (var photo in update.Photos)
                {
                    if (!photo.ValidateType())
                    {
                        tempData["Message"] += $"<p class=\"text-danger\">{photo.Name} type is not suitable</p>";
                        continue;
                    }

                    if (!photo.ValidataSize(10))
                    {
                        tempData["Message"] += $"<p class=\"text-danger\">{photo.Name} the size is not suitable</p>";
                        continue;
                    }

                    item.PlaceImages.Add(new PlaceImage
                    {
                        Url = await photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
                    });
                }
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdatePlaceVM, Place>()
                    .ForMember(dest => dest.PlaceImages, opt => opt.Ignore());
            });
            var mapper = config.CreateMapper();

            mapper.Map(update, item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task<UpdatePlaceVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.PlaceFeatures)}.{nameof(PlaceFeature.Feature)}",
                $"{nameof(Place.PlaceTags)}.{nameof(PlaceTag.Tag)}",
                $"{nameof(Place.PlaceImages)}" };
            Place item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdatePlaceVM update = _mapper.Map<UpdatePlaceVM>(item);

            update.FeatureIds = item.PlaceFeatures.Select(p => p.FeatureId).ToList();
            update.TagIds = item.PlaceTags.Select(p => p.TagId).ToList();

            await UpdatePopulateDropdowns(update);

            return update;
        }
    }
}
