using AutoMapper;
using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionApplication.ViewModels;
using CityBookMVCOnionApplication.ViewModels.Account;
using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionDomain.Enums;
using CityBookMVCOnionInfrastructure.Exceptions;
using CityBookMVCOnionInfrastructure.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;

namespace CityBookMVCOnionPersistence.Implementations.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _http;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager,
            IMapper mapper, IEmailService emailService, IHttpContextAccessor http, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _emailService = emailService;
            _http = http;
            _configuration = configuration;
        }

        public async Task<bool> LogInAsync(LoginVM login, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            User user = await _userManager.FindByNameAsync(login.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(login.UsernameOrEmail);
                if (user == null)
                {
                    model.AddModelError(string.Empty, "Username, Email or Password is wrong");
                    return false;
                }
            }
            if (user.IsActivate == true)
            {
                model.AddModelError("Error", "Your account is not active");
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.IsRemembered, true);
            if (result.IsLockedOut)
            {
                model.AddModelError("Error", "Your Account is locked-out please wait");
                return false;
            }
            if (!result.Succeeded)
            {
                model.AddModelError("Error", "Username, Email or Password is wrong");
                return false;
            }
            return true;
        }
        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<bool> RegisterAsync(RegisterVM register, ModelStateDictionary model, IUrlHelper url)
        {
            if (!model.IsValid) return false;

            User user = _mapper.Map<User>(register);
            user.Name = user.Name.Capitalize();
            user.Surname = user.Surname.Capitalize();
            user.About = "";
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    model.AddModelError("Error", error.Description);
                }
                return false;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = url.Action("ConfirmEmail", "Account", new { token, Email = user.Email }, _http.HttpContext.Request.Scheme);
            await _emailService.SendMailAsync(user.Email, "Email Confirmation", confirmationLink);
            await _emailService.SendMailAsync(_configuration["AdminSettings:Email"], "Email Confirmation", $"{user.UserName} want join us");
            if (register.role.Contains(UserRole.BiznesOwner.ToString()))
            {
                user.IsActivate = true;

                await _userManager.AddToRoleAsync(user, UserRole.BiznesOwner.ToString());

                return true;
            }
            else
            {
                await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());
            }

            return true;
        }
        public async Task<bool> ConfirmEmail(string token, string email)
        {
            User User = await _userManager.FindByEmailAsync(email);
            if (User == null) throw new NotFoundException("Your request was not found");
            var result = await _userManager.ConfirmEmailAsync(User, token);
            if (!result.Succeeded)
            {
                throw new WrongRequestException("The request sent does not exist");
            }
            await _signInManager.SignInAsync(User, false);

            return true;
        }
        public async Task<bool> ForgotPassword(FindAccountVM account, ModelStateDictionary model, IUrlHelper url)
        {
            if (string.IsNullOrWhiteSpace(account.UserNameOrEmail))
            {
                model.AddModelError(string.Empty, "Username, Email or Password is wrong");
                return false;

            }
            User user = await _userManager.FindByNameAsync(account.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(account.UserNameOrEmail);
                if (user == null)
                {
                    model.AddModelError(string.Empty, "Username or Email is wrong");
                    return false;
                }
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmationLink = url.Action("ResetPassword", "Account", new { Id = user.Id, Token = token }, _http.HttpContext.Request.Scheme);
            await _emailService.SendMailAsync(user.Email, "Password Reset", confirmationLink);

            return true;
        }

        public async Task<bool> ResetPassword(string id, string token, ResetPasswordVM resetPassword, ModelStateDictionary model)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(token)) throw new NotFoundException("Your request was not found");
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                if (user == null) throw new NotFoundException("Your request was not found");
            }

            var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
            if (!result.Succeeded)
            {
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                model.AddModelError(string.Empty, "Username, Email or Password is wrong");
                return false;
            }
            _http.HttpContext.Response.Cookies.Delete("FavoriteEstate");

            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, false);
            return true;
        }
    }

}
