using AutoMapper;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CityBookMVCOnionPersistence.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IPositionRepository _positionRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _http;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, 
            IEmailService emailService, IPositionRepository positionRepository, IWebHostEnvironment env,
            IHttpContextAccessor http, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _emailService = emailService;
            _positionRepository = positionRepository;
            _env = env;
            _http = http;
            _configuration = configuration;
        }

        public async Task<PaginationVM<ItemUserVM>> GetFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            double count = await _userManager.Users.CountAsync();

            ICollection<User> users = new List<User>();

            switch (order)
            {
                case 1:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == false).OrderBy(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 2:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == false).OrderByDescending(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 3:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == false).OrderBy(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 4:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == false).OrderByDescending(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
            }

            ICollection<ItemUserVM> vMs = _mapper.Map<ICollection<ItemUserVM>>(users);

            PaginationVM<ItemUserVM> pagination = new PaginationVM<ItemUserVM>
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

        public async Task<PaginationVM<ItemUserVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            double count = await _userManager.Users.CountAsync();

            ICollection<User> users = new List<User>();

            switch (order)
            {
                case 1:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == true).OrderBy(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 2:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == true).OrderByDescending(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 3:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == true).OrderBy(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 4:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == true).OrderByDescending(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
            }

            ICollection<ItemUserVM> vMs = _mapper.Map<ICollection<ItemUserVM>>(users);

            PaginationVM<ItemUserVM> pagination = new PaginationVM<ItemUserVM>
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

        public async Task<GetUserVM> GetByIdAdminAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User? user = await _userManager.Users
               
                .Include(x => x.Blogs).ThenInclude(x => x.BlogImages)
                .Include(x => x.Reviews)
                .Include(x => x.Replies).ThenInclude(x => x.Comment).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) throw new NotFoundException("Your request was not found");

            GetUserVM get = _mapper.Map<GetUserVM>(user);

            return get;
        }

        public async Task<GetUserVM> GetByUserNameAdminAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.Users
                .Include(x => x.Blogs).ThenInclude(x => x.BlogImages)
                .Include(x => x.Reviews)
                .Include(x => x.Replies).ThenInclude(x => x.Comment).AsNoTracking().FirstOrDefaultAsync(x => x.Id == userName);
            if (user == null) throw new NotFoundException("Your request was not found");

            GetUserVM get = _mapper.Map<GetUserVM>(user);

            return get;
        }

        public async Task<GetUserVM> GetByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.Users
                .Include(x => x.Places).ThenInclude(a => a.Category)
                .Include(x => x.Places).ThenInclude(a => a.Reviews)
                .Include(x => x.Places).ThenInclude(a => a.PlaceImages)
                .Include(x => x.Blogs).ThenInclude(x => x.BlogImages)
                .Include(x => x.Reviews).Include(x => x.Places.Where(a => a.IsDeleted == false)).ThenInclude(x => x.PlaceImages)
                .Include(x => x.Replies).ThenInclude(x => x.Comment).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) throw new NotFoundException("Your request was not found");

            GetUserVM get = _mapper.Map<GetUserVM>(user);

            return get;
        }

        public async Task<GetUserVM> GetByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.Users
                .Include(x => x.Blogs).ThenInclude(x => x.BlogImages)
                .Include(x => x.Reviews).Include(x => x.Places).ThenInclude(x => x.PlaceImages)
                .Include(x => x.Replies).ThenInclude(x => x.Comment).AsNoTracking().FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null) throw new NotFoundException("Your request was not found");

            GetUserVM get = _mapper.Map<GetUserVM>(user);

            return get;
        }

        public async Task IsSoulOfAgencyAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            

            await _userManager.UpdateAsync(user);
        }

        public async Task GiveRoleModeratorAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            await _userManager.AddToRoleAsync(user, AdminRoles.Admin.ToString());
        }
        public async Task DeleteRoleModeratorAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            await _userManager.AddToRoleAsync(user, AdminRoles.Member.ToString());
        }
        public async Task DeleteRoleAgentAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            await _userManager.AddToRoleAsync(user, AdminRoles.Member.ToString());
        }
        public async Task ReverseSoftDeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            user.IsActivate = false;

            await _userManager.UpdateAsync(user);
        }

        public async Task SoftDeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            user.IsActivate = true;

            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            await _userManager.DeleteAsync(user);
        }

        public async Task<BeEmployeeVM> BeEmployee(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            BeEmployeeVM vm = _mapper.Map<BeEmployeeVM>(user);

            vm.Positions = _mapper.Map<List<IncludePositionVM>>(await _positionRepository.GetAll().ToListAsync());

            return vm;
        }

        public async Task<bool> BeEmployeePost(string id, BeEmployeeVM vm, ModelStateDictionary model)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");
            if (!model.IsValid)
            {
                vm.Positions = _mapper.Map<List<IncludePositionVM>>(await _positionRepository.GetAll().ToListAsync());
                return false;
            }
            if (!await _positionRepository.CheckUniqueAsync(x => x.Id == vm.PositionId))
            {
                vm.Positions = _mapper.Map<List<IncludePositionVM>>(await _positionRepository.GetAll().ToListAsync());
                return false;
            }
            user.PositionId = vm.PositionId;

            return true;
        }

        public async Task<EditUserVM> EditUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            EditUserVM get = _mapper.Map<EditUserVM>(user);

            return get;
        }

        public async Task<bool> EditUserAsync(string id, EditUserVM update, ModelStateDictionary model)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            _mapper.Map(update, user);
            user.Name = update.Name.Capitalize();
            user.Surname = update.Surname.Capitalize();

            if (update.Photo != null)
            {
                if (!update.Photo.ValidateType())
                {
                    model.AddModelError("Photo", "File Not supported");
                    return false;
                }
                if (!update.Photo.ValidataSize())
                {
                    model.AddModelError("Photo", "Image should not be larger than 10 mb");
                    return false;
                }
                user.Image.DeleteFile(_env.WebRootPath,  "images");
                user.Image = await update.Photo.CreateFileAsync(_env.WebRootPath, "images");
            }

            await _userManager.UpdateAsync(user);
            //await _signInManager.SignOutAsync();
            //await _signInManager.SignInAsync(user, false);

            return true;
        }

        public async Task ForgotPassword(string id, IUrlHelper url)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmationLink = url.Action("ChangePassword", "User", new { Id = user.Id, Token = token }, _http.HttpContext.Request.Scheme);
            await _emailService.SendMailAsync(user.Email, "Password Reset", confirmationLink);
        }

        public async Task<bool> ChangePassword(string id, string token, ChangePasswordVM fogotPassword, ModelStateDictionary model)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(token)) throw new NotFoundException("Your request was not found");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            var result = await _userManager.ChangePasswordAsync(user, fogotPassword.Password, fogotPassword.NewPassword);
            if (!result.Succeeded)
            {
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                throw new WrongRequestException(errors);
            }

            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, false);
            return true;
        }

        
    }
}
