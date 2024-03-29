﻿using AutoMapper;
using CityBookMVCOnionApplication.Abstractions.Repositories;
using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionDomain.Enums;
using CityBookMVCOnionInfrastructure.Exceptions;
using CityBookMVCOnionInfrastructure.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.RegularExpressions;

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
        private readonly IReservationRepository _reservationRepository;
        private readonly IEmailService _email;
        private readonly UserManager<User> _userManager;

        public PlaceService(IMapper mapper, IPlaceRepository repository, ITagRepository tagRepository,
            IFeatureRepository featureRepository, ICategoryRepository categoryRepository, IHttpContextAccessor http,
            IWebHostEnvironment env, IReservationRepository reservationRepository, IEmailService email, UserManager<User> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _tagRepository = tagRepository;
            _featureRepository = featureRepository;
            _categoryRepository = categoryRepository;
            _http = http;
            _env = env;
            _reservationRepository = reservationRepository;
            _email = email;
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
                    ImageUrl = await photo.CreateFileAsync(_env.WebRootPath, "images")
                });
            }
            item.UserId = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
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
                $"{nameof(Place.Reviews)}",
                $"{nameof(Place.PlaceFeatures)}.{nameof(PlaceFeature.Feature)}",
                $"{nameof(Place.PlaceTags)}.{nameof(PlaceTag.Tag)}",
                $"{nameof(Place.PlaceImages)}" };
            Place item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");
            foreach (var image in item.PlaceImages)
            {
                image.ImageUrl.DeleteFile(_env.WebRootPath, "images");
            }
            _repository.Delete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemPlaceVM>> GetAllWhereAsync(int take, int page = 1)
        {
            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.User)}",
                $"{nameof(Place.Reviews)}",
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
                $"{nameof(Place.User)}",
                $"{nameof(Place.Reviews)}",
                $"{nameof(Place.PlaceFeatures)}.{nameof(PlaceFeature.Feature)}",
                $"{nameof(Place.PlaceTags)}.{nameof(PlaceTag.Tag)}",
                $"{nameof(Place.PlaceImages)}" };
            ICollection<Place> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemPlaceVM> vMs = _mapper.Map<ICollection<ItemPlaceVM>>(items);

            return vMs;
        }
        public async Task<PaginationVM<PlaceFilterVM>> GetFilteredAsync(string? search, int take, int page, int order, int? categoryId)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.User)}",
                $"{nameof(Place.Reviews)}",
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

            PlaceFilterVM placeFilter = new PlaceFilterVM
            {
                Places = _mapper.Map<ICollection<ItemPlaceVM>>(items),
                Categories = _mapper.Map<ICollection<IncludeCategoryVM>>(await _categoryRepository.GetAllWhere().ToListAsync())
            };

            PaginationVM<PlaceFilterVM> pagination = new PaginationVM<PlaceFilterVM>
            {
                Take = take,
                Search = search,
                Order = order,
                CategoryId = categoryId,
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / take),
                Item = placeFilter
            };

            return pagination;
        }

        public async Task<PaginationVM<PlaceFilterVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order, int? categoryId)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.User)}",
                $"{nameof(Place.Reviews)}",
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

            PlaceFilterVM placeFilter = new PlaceFilterVM
            {
                Places = _mapper.Map<ICollection<ItemPlaceVM>>(items),
                Categories = _mapper.Map<ICollection<IncludeCategoryVM>>(await _categoryRepository.GetAll().ToListAsync())
            };

            PaginationVM<PlaceFilterVM> pagination = new PaginationVM<PlaceFilterVM>
            {
                Take = take,
                Search = search,
                Order = order,
                CategoryId = categoryId,
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / take),
                Item = placeFilter
            };

            return pagination;
        }
        public async Task<PaginationVM<PlaceFilterVM>> GetAllWhereByOrderFilterAsync(int take, int page, Expression<Func<Place, object>>? orderExpression = null)
        {
            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.PlaceImages)}" };
            ICollection<Place> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemPlaceVM> vMs = _mapper.Map<ICollection<ItemPlaceVM>>(items);

            PlaceFilterVM filtered = new PlaceFilterVM
            {
                Places = _mapper.Map<ICollection<ItemPlaceVM>>(items),
                Categories = _mapper.Map<ICollection<IncludeCategoryVM>>(await _categoryRepository.GetAllWhereByOrder(orderException: x => x.Places.Count).ToListAsync())
            };
            PaginationVM<PlaceFilterVM> pagination = new PaginationVM<PlaceFilterVM>
            {
                Take = take,
                Item = filtered
            };

            return pagination;
        }
        public async Task<GetPlaceVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Place.Category)}",
                $"{nameof(Place.Reviews)}",
                $"{nameof(Place.User)}",
                $"{nameof(Place.PlaceFeatures)}.{nameof(PlaceFeature.Feature)}",
                $"{nameof(Place.PlaceTags)}.{nameof(PlaceTag.Tag)}",
                $"{nameof(Place.PlaceImages)}" };
            Place item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetPlaceVM get = _mapper.Map<GetPlaceVM>(item);
            get.CurrentUser = _mapper.Map<IncludeUserVM>(await _userManager.FindByIdAsync(_http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
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
                $"{nameof(Place.Reviews)}",
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
                image.ImageUrl.DeleteFile(_env.WebRootPath, "images");
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
                        ImageUrl = await photo.CreateFileAsync(_env.WebRootPath, "images")
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
                $"{nameof(Place.Reviews)}",
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
        public async Task<bool> AddReservation(int id, string dayOrMonth, int? persons, string reservationDate,
            string? reservationDateTo, string about, ITempDataDictionary tempData)
        {
            tempData["Reserv"] = "";

            if (string.IsNullOrWhiteSpace(dayOrMonth))
            {
                tempData["Reserv"] += $"<p class=\"text-danger\"> You must enter reservation day or month </p>";
                return false;
            }

            if (string.IsNullOrWhiteSpace(about))
            {
                tempData["Reserv"] += $"<p class=\"text-danger\"> You must enter additional information </p>";
                return false;
            }

            if (string.IsNullOrWhiteSpace(reservationDate))
            {
                tempData["Reserv"] += $"<p class=\"text-danger\"> You need to choose reservation date </p>";
                return false;
            }
            if(DateTime.Parse(reservationDate) < DateTime.Parse(reservationDateTo))
            {
                tempData["Reserv"] += $"<p class=\"text-danger\">Reservation date cannot be earlier than the last reservation end date</p>";
                return false;
            }
            if (dayOrMonth.Contains(DayOrMonth.Month.ToString()) && reservationDateTo == null)
            {
                tempData["Reserv"] += $"<p class=\"text-danger\"> You need to choose expiration date</p>";
                return false;
            }

            if (dayOrMonth.Contains(DayOrMonth.Day.ToString()) && persons <= 0 && persons != null)
            {
                tempData["Reserv"] += $"<p class=\"text-danger\">The count of people cannot be less than 1</p>";
                return false;
            }
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Place item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");
            Reservation reserv = new Reservation
            {
                UserId = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                PlaceId = item.Id,
                Persons = persons,
                ReservationDate = reservationDate,
                ReservationDateTo = reservationDateTo,
                About = about
            };

            await _reservationRepository.AddAsync(reserv);
            await _reservationRepository.SaveChanceAsync();
            return true;
        }
        public async Task AcceptReservation(int id)
        {
            Reservation reserv = await _reservationRepository.GetByIdAsync(id, true, $"{nameof(Reservation.User)}", $"{nameof(Reservation.Place)}");
            if (reserv.ReservationDateTo == null)
            {
                await _email.SendMailAsync(reserv.User.Email, "Accepted ", $"accepted by {_http.HttpContext.User.FindFirstValue(ClaimTypes.GivenName)} {reserv.Place.Name} {reserv.ReservationDate}");
            }
            else
            {
                await _email.SendMailAsync(reserv.User.Email, "Accepted ", $"accepted by {_http.HttpContext.User.FindFirstValue(ClaimTypes.GivenName)} {reserv.Place.Name} {reserv.ReservationDate} {reserv.ReservationDateTo}");
            }

            reserv.IsApproved = true;
            _reservationRepository.Update(reserv);
            _repository.SaveChanceAsync();
        }
        public async Task CanceledReservation(int id)
        {
            Reservation reserv = await _reservationRepository.GetByIdAsync(id, true, $"{nameof(Reservation.User)}", $"{nameof(Reservation.Place)}");
            if (reserv.ReservationDateTo == null)
            {
                await _email.SendMailAsync(reserv.User.Email, "Canceled ", $"Canceled by {_http.HttpContext.User.FindFirstValue(ClaimTypes.GivenName)} {reserv.Place.Name} {reserv.ReservationDate}");
            }
            else
            {
                await _email.SendMailAsync(reserv.User.Email, "Canceled ", $"Canceled by {_http.HttpContext.User.FindFirstValue(ClaimTypes.GivenName)} {reserv.Place.Name} {reserv.ReservationDate} {reserv.ReservationDateTo}");
            }

            reserv.IsApproved = false;
            _reservationRepository.Update(reserv);
            _repository.SaveChanceAsync();
        }
        public async Task<bool> Review(int id, int rating, string comment, ModelStateDictionary model)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                model.AddModelError("Error", "Comment is required");
                return false;
            }
            if (comment.Length > 1500)
            {
                model.AddModelError("Error", "Comment max characters is 1-1500");
                return false;
            }
            if (!Regex.IsMatch(comment, @"^[A-Za-z0-9\s,\.]+$"))
            {
                model.AddModelError("Error", "Comment can only contain letters, numbers, spaces, commas, and periods.");
                return false;
            }
            Review review = new Review
            {
                Text = comment,
                RatingStar = rating,
                PlaceId = id,
                UserId = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            await _repository.AddReview(review);
            await _repository.SaveChanceAsync();
            return true;
        }
    }
}
