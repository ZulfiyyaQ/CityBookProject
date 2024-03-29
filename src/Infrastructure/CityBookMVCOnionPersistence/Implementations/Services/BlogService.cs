﻿using AutoMapper;
using CityBookMVCOnionApplication.Abstractions.Repositories;
using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionInfrastructure.Exceptions;
using CityBookMVCOnionInfrastructure.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace CityBookMVCOnionPersistence.Implementations.Services
{
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _repository;
        private readonly IBTagRepository _bTagRepository;
        private readonly IHttpContextAccessor _http;
        private readonly IWebHostEnvironment _env;

        public BlogService(IMapper mapper, IBlogRepository repository, IHttpContextAccessor http,
            IWebHostEnvironment env, IBTagRepository bTagRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _http = http;
            _env = env;
            _bTagRepository = bTagRepository;
        }

        public async Task CreatePopulateDropdowns(CreateBlogVM create)
        {
            create.Tags = _mapper.Map<List<IncludeBTagVM>>(await _bTagRepository.GetAll().ToListAsync());
        }
        public async Task UpdatePopulateDropdowns(UpdateBlogVM update)
        {
            update.Tags = _mapper.Map<List<IncludeBTagVM>>(await _bTagRepository.GetAll().ToListAsync());
        }

        public async Task<bool> CreateAsync(CreateBlogVM create, ModelStateDictionary model, ITempDataDictionary tempData)
        {
            if (!model.IsValid)
            {
                await CreatePopulateDropdowns(create);
                return false;
            }
            if (await _repository.CheckUniqueAsync(x => x.Name.ToLower().Trim() == create.Name.ToLower().Trim()))
            {
                await CreatePopulateDropdowns(create);
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            foreach (int tagId in create.TagIds)
            {
                if (!await _bTagRepository.CheckUniqueAsync(x => x.Id == tagId))
                {
                    await CreatePopulateDropdowns(create);
                    return false;
                }
            }
            Blog item = _mapper.Map<Blog>(create);
            item.BlogTags = create.TagIds.Select(id => new BlogTag { TagId = id }).ToList();
            if (item.BlogImages == null) item.BlogImages = new List<BlogImage>();

            tempData["Message"] = "";

            foreach (var photo in create.Photos)
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

                item.BlogImages.Add(new BlogImage
                {
                    //CreatedBy = _http.HttpContext.User.Identity.Name,
                    Url = await photo.CreateFileAsync(_env.WebRootPath, "images")
                });
            }
            //item.CreatedBy = _http.HttpContext.User.Identity.Name;
            item.UserId = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _repository.AddAsync(item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Blog.User)}",
                $"{nameof(Blog.BlogTags)}.{nameof(BlogTag.Tag)}",
                $"{nameof(Blog.Comments)}.{nameof(Comment.Replies)}",
                $"{nameof(Blog.BlogImages)}" };
            Blog item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");
            foreach (var image in item.BlogImages)
            {

                //image.Url.DeleteFile(_env.WebRootPath, "assets", "images");
            }
            _repository.Delete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemBlogVM>> GetAllWhereAsync(int take, int page = 1)
        {
            string[] includes ={
                $"{nameof(Blog.User)}",
                $"{nameof(Blog.Comments)}.{nameof(Comment.Replies)}.{nameof(Reply.User)}",
                $"{nameof(Blog.Comments)}.{nameof(Comment.User)}",
                $"{nameof(Blog.BlogTags)}.{nameof(BlogTag.Tag)}",
                $"{nameof(Blog.BlogImages)}" };
            ICollection<Blog> items = await _repository
                    .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemBlogVM> vMs = _mapper.Map<ICollection<ItemBlogVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemBlogVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Blog, object>>? orderExpression, int page = 1)
        {
            string[] includes = { $"{nameof(Blog.BlogImages)}" };
            ICollection<Blog> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemBlogVM> vMs = _mapper.Map<ICollection<ItemBlogVM>>(items);

            return vMs;
        }

        public async Task<PaginationVM<ItemBlogVM>> GetFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes = { $"{nameof(Blog.User)}",
                $"{nameof(Blog.BlogTags)}.{nameof(BlogTag.Tag)}",
                $"{nameof(Blog.BlogImages)}" };

            double count = await _repository.CountAsync();

            ICollection<Blog> items = new List<Blog>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, false, false,(page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, true, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            ICollection<ItemBlogVM> vMs = _mapper.Map<ICollection<ItemBlogVM>>(items);

            PaginationVM<ItemBlogVM> pagination = new PaginationVM<ItemBlogVM>
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

        public async Task<PaginationVM<ItemBlogVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes = { $"{nameof(Blog.BlogImages)}" };

            double count = await _repository.CountAsync();

            ICollection<Blog> items = new List<Blog>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            ICollection<ItemBlogVM> vMs = _mapper.Map<ICollection<ItemBlogVM>>(items);

            PaginationVM<ItemBlogVM> pagination = new PaginationVM<ItemBlogVM>
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

        public async Task<GetBlogVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Blog.User)}",
                $"{nameof(Blog.BlogTags)}.{nameof(BlogTag.Tag)}",
                $"{nameof(Blog.Comments)}.{nameof(Comment.Replies)}.{nameof(Reply.User)}",
                $"{nameof(Blog.Comments)}.{nameof(Comment.User)}",
                $"{nameof(Blog.BlogImages)}" };
            Blog item = await _repository.GetByIdAsync(id, IsTracking: false, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetBlogVM get = _mapper.Map<GetBlogVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Blog item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChanceAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Blog item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChanceAsync();
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateBlogVM update, ModelStateDictionary model, ITempDataDictionary tempData)
        {
            if (!model.IsValid)
            {
                await UpdatePopulateDropdowns(update);
                return false;
            }
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Blog.BlogTags)}.{nameof(BlogTag.Tag)}",
                $"{nameof(Blog.BlogImages)}" };
            Blog item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");
            if (await _repository.CheckUniqueAsync(x => x.Name.ToLower().Trim() == update.Name.ToLower().Trim() && x.Id != id))
            {
                await UpdatePopulateDropdowns(update);
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            foreach (int tagId in update.TagIds)
            {
                if (!await _bTagRepository.CheckUniqueAsync(x => x.Id == tagId))
                {
                    await UpdatePopulateDropdowns(update);
                    return false;
                }
            }
            ICollection<BlogTag> tagToRemove = item.BlogTags
                .Where(ps => !update.TagIds.Contains(ps.TagId)).ToList();
            foreach (var tagRemove in tagToRemove)
            {
                _repository.DeleteTag(tagRemove);
            }

            ICollection<BlogTag> tagToAdd = update.TagIds
                .Except(item.BlogTags.Select(ps => ps.TagId))
                .Select(tagId => new BlogTag { TagId = tagId })
                .ToList();
            foreach (var tagAdd in tagToAdd)
            {
                item.BlogTags.Add(tagAdd);
            }

            if (item.BlogImages == null) item.BlogImages = new List<BlogImage>();

            if (update.ImageIds == null) update.ImageIds = new List<int>();

            ICollection<BlogImage> remove = item.BlogImages
                    .Where(pi => !update.ImageIds.Exists(imgId => imgId == pi.Id)).ToList();

            foreach (var image in remove)
            {
                image.Url.DeleteFile(_env.WebRootPath,  "images");
                item.BlogImages.Remove(image);
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

                    item.BlogImages.Add(new BlogImage
                    {
                        //CreatedBy = _http.HttpContext.User.Identity.Name,

                        Url = await photo.CreateFileAsync(_env.WebRootPath, "images")
                    });
                }
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateBlogVM, Blog>()
                    .ForMember(dest => dest.BlogImages, opt => opt.Ignore());
            });
            var mapper = config.CreateMapper();

            mapper.Map(update, item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task<UpdateBlogVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Blog.BlogTags)}.{nameof(BlogTag.Tag)}",
                 
                $"{nameof(Blog.BlogImages)}" };
            Blog item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateBlogVM update = _mapper.Map<UpdateBlogVM>(item);
            update.TagIds = item.BlogTags.Select(p => p.TagId).ToList();
            await UpdatePopulateDropdowns(update);

            return update;
        }
        public async Task<bool> CommentAsync(int blogId, string comment, ModelStateDictionary model)
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
            Comment blogComment = new Comment
            {
                Text = comment,
                BlogId = blogId,
                UserId = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            await _repository.AddComment(blogComment);
            await _repository.SaveChanceAsync();
            return true;
        }
        public async Task<bool> ReplyAsync(int blogCommnetId, string comment, ModelStateDictionary model)
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
            Reply blogComment = new Reply
            {
                Text = comment,
                CommentId = blogCommnetId,
                UserId = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            await _repository.AddReply(blogComment);
            await _repository.SaveChanceAsync();
            return true;
        }
    }
}
