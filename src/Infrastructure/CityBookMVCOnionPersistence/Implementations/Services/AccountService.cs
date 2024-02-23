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
            await _emailService.SendMailAsync(user.Email, "Email Confirmation", $"<head>\r\n    <meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <style type=\"text/css\">\r\n        body {{\r\n            margin: 0;\r\n            background: #FEFEFE;\r\n            color: #585858;\r\n        }}\r\n\r\n        table {{\r\n            font-size: 15px;\r\n            line-height: 23px;\r\n            max-width: 500px;\r\n            min-width: 460px;\r\n            text-align: center;\r\n        }}\r\n\r\n        .table_inner {{\r\n            min-width: 100% !important;\r\n        }}\r\n\r\n        td {{\r\n            font-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n            vertical-align: top;\r\n        }}\r\n\r\n        .carpool_logo {{\r\n            margin: 30px auto;\r\n        }}\r\n\r\n        .dummy_row {{\r\n            padding-top: 20px !important;\r\n        }}\r\n\r\n        .section,\r\n        .sectionlike {{\r\n            background: #C9F9E9;\r\n        }}\r\n\r\n        .section {{\r\n            padding: 0 20px;\r\n        }}\r\n\r\n        .sectionlike {{\r\n            padding-bottom: 10px;\r\n        }}\r\n\r\n        .section_content {{\r\n            width: 100%;\r\n            background: #fff;\r\n        }}\r\n\r\n        .section_content_padded {{\r\n            padding: 0 35px 40px;\r\n        }}\r\n\r\n        .section_zag {{\r\n            background: #F4FBF9;\r\n        }}\r\n\r\n        .imageless_section {{\r\n            padding-bottom: 20px;\r\n        }}\r\n\r\n        img {{\r\n            display: block;\r\n            margin: 0 auto;\r\n        }}\r\n\r\n        .img_section {{\r\n            width: 100%;\r\n            max-width: 500px;\r\n        }}\r\n\r\n        .img_section_side_table {{\r\n            width: 100% !important;\r\n        }}\r\n\r\n        h1 {{\r\n            font-size: 20px;\r\n            font-weight: 500;\r\n            margin-top: 40px;\r\n            margin-bottom: 0;\r\n        }}\r\n\r\n        .near_title {{\r\n            margin-top: 10px;\r\n        }}\r\n\r\n        .last {{\r\n            margin-bottom: 0;\r\n        }}\r\n\r\n        a {{\r\n            color: #63D3CD;\r\n            font-weight: 500;\r\n            word-break: break-word; /* Footer has long unsubscribe link */\r\n        }}\r\n\r\n        .button {{\r\n            display: block;\r\n            width: 100%;\r\n            max-width: 300px;\r\n            background: #20DA9C;\r\n            border-radius: 8px;\r\n            color: #fff;\r\n            font-size: 18px;\r\n            font-weight: normal; /* Resetting from a */\r\n            padding: 12px 0;\r\n            margin: 30px auto 0;\r\n            text-decoration: none;\r\n        }}\r\n\r\n        small {{\r\n            display: block;\r\n            width: 100%;\r\n            max-width: 330px;\r\n            margin: 14px auto 0;\r\n            font-size: 14px;\r\n        }}\r\n\r\n        .signature {{\r\n            padding: 20px;\r\n        }}\r\n\r\n        .footer,\r\n        .footer_like {{\r\n            background: #1FD99A;\r\n        }}\r\n\r\n        .footer {{\r\n            padding: 0 20px 30px;\r\n        }}\r\n\r\n        .footer_content {{\r\n            width: 100%;\r\n            text-align: center;\r\n            font-size: 12px;\r\n            line-height: initial;\r\n            color: #005750;\r\n        }}\r\n\r\n            .footer_content a {{\r\n                color: #005750;\r\n            }}\r\n\r\n        .footer_item_image {{\r\n            margin: 0 auto 10px;\r\n        }}\r\n\r\n        .footer_item_caption {{\r\n            margin: 0 auto;\r\n        }}\r\n\r\n        .footer_legal {{\r\n            padding: 20px 0 40px;\r\n            margin: 0;\r\n            font-size: 12px;\r\n            color: #A5A5A5;\r\n            line-height: 1.5;\r\n        }}\r\n\r\n        .text_left {{\r\n            text-align: left;\r\n        }}\r\n\r\n        .text_right {{\r\n            text-align: right;\r\n        }}\r\n\r\n        .va {{\r\n            vertical-align: middle;\r\n        }}\r\n\r\n        .stats {{\r\n            min-width: auto !important;\r\n            max-width: 370px;\r\n            margin: 30px auto 0;\r\n        }}\r\n\r\n        .counter {{\r\n            font-size: 22px;\r\n        }}\r\n\r\n        .stats_counter {{\r\n            width: 23%;\r\n        }}\r\n\r\n        .stats_image {{\r\n            width: 18%;\r\n            padding: 0 10px;\r\n        }}\r\n\r\n        .stats_meta {{\r\n            width: 59%;\r\n        }}\r\n\r\n        .stats_spaced {{\r\n            padding-top: 16px;\r\n        }}\r\n\r\n        .walkthrough_spaced {{\r\n            padding-top: 24px;\r\n        }}\r\n\r\n        .walkthrough {{\r\n            max-width: none;\r\n        }}\r\n\r\n        .walkthrough_meta {{\r\n            padding-left: 20px;\r\n        }}\r\n\r\n        .table_checkmark {{\r\n            padding-top: 30px;\r\n        }}\r\n\r\n        .table_checkmark_item {{\r\n            font-size: 15px;\r\n        }}\r\n\r\n        .td_checkmark {{\r\n            width: 24px;\r\n            padding: 7px 12px 0 0;\r\n        }}\r\n\r\n        .padded_bottom {{\r\n            padding-bottom: 40px;\r\n        }}\r\n\r\n        .marginless {{\r\n            margin: 0;\r\n        }}\r\n\r\n        /* Restricting responsive for iOS Mail app only as Inbox/Gmail have render bugs */\r\n        @media only screen and (max-width: 480px) and (-webkit-min-device-pixel-ratio: 2) {{\r\n            table {{\r\n                min-width: auto !important;\r\n            }}\r\n\r\n            .section_content_padded {{\r\n                padding-right: 25px !important;\r\n                padding-left: 25px !important;\r\n            }}\r\n\r\n            .counter {{\r\n                font-size: 18px !important;\r\n            }}\r\n        }}\r\n    </style>\r\n</head>\r\n<body style=\"\tmargin: 0;\r\n\tbackground: #FEFEFE;\r\n\tcolor: #585858;\r\n\">\r\n    <!-- Preivew text -->\r\n    <span class=\"preheader\" style=\"display: none !important; visibility: hidden; opacity: 0; color: transparent; height: 0; width: 0;border-collapse: collapse;border: 0px;\"></span>\r\n    <!-- Carpool logo -->\r\n    <table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"\tfont-size: 15px;\r\n\tline-height: 23px;\r\n\tmax-width: 500px;\r\n\tmin-width: 460px;\r\n\ttext-align: center;\r\n\">\r\n        <tbody>\r\n            <tr>\r\n                <td style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n\">\r\n                    <img src=https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTlKkGB_kTI7AM6iay-j-BWX49yHbnDx5dt_A&usqp=CAU\" class=\"carpool_logo\" width=\"300\" height=\"300\" style=\"\tdisplay: block;\r\n\tmargin: 0 auto;\r\nmargin: 30px auto;border-radius:50%;object-fit:cover\">\r\n                </td>\r\n            </tr>\r\n            <!-- Header -->\r\n            <tr>\r\n                <td class=\"sectionlike imageless_section\" style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n  background:  #1f9dff;\r\n  padding-bottom: 10px;\r\npadding-bottom: 20px;\"></td>\r\n            </tr>\r\n            <!-- Content -->\r\n            <tr>\r\n                <td class=\"section\" style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n    background:  #1f9dff;\r\n\tpadding: 0 20px;\r\n\">\r\n                    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"section_content\" style=\"\tfont-size: 15px;\r\n\tline-height: 23px;\r\n\tmax-width: 500px;\r\n\tmin-width: 460px;\r\n\ttext-align: center;\r\n\twidth: 100%;\r\n    background:  white;\r\n\">\r\n                        <tbody>\r\n                            <tr>\r\n                                <td class=\"section_content_padded\" style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\npadding: 0 35px 40px;\">\r\n                                    <h1 style=\"\tfont-size: 20px;\r\n\tfont-weight: 500;\r\n\tmargin-top: 40px;\r\n\tmargin-bottom: 0;\r\n\">\r\n                                        Dear {user.Name},  We're excited to inform you that your CityBook account has been successfully created!Thank you for joining CityBook Administration. We look forward to seeing you make a positive impact within our community.\r\n                                    </h1>\r\n                                    <p class=\"near_title last\" style=\"margin-top: 10px;margin-bottom: 0;\">Please confirm the email.</p>\r\n                                    <a href=\"{confirmationLink}\" style=\"\tdisplay: block;\r\n\twidth: 100%;\r\n\tmax-width: 300px;\r\n\tbackground:  #061e40 ;\r\n\tborder-radius: 8px;\r\n\tcolor: #fff;\r\n\tfont-size: 18px;\r\n\tpadding: 12px 0;\r\n\tmargin: 30px auto 0;\r\n\ttext-decoration: none;\r\n\" target=\"_blank\">Confirm Email</a>\r\n                                    <small style=\"\tdisplay: block;\r\n\twidth: 100%;\r\n\tmax-width: 330px;\r\n\tmargin: 14px auto 0;\r\n\tfont-size: 14px;\r\n\"></small>\r\n                                </td>\r\n                            </tr>\r\n                        </tbody>\r\n                    </table>\r\n                </td>\r\n            </tr>\r\n            <!-- Signature -->\r\n            <tr>\r\n                <td class=\"section\" style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n    background:  #1f9dff;\r\n\tpadding: 0 20px;\r\n\">\r\n                    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"section_content section_zag\" style=\"\tfont-size: 15px;\r\n\tline-height: 23px;\r\n\tmax-width: 500px;\r\n\tmin-width: 460px;\r\n\ttext-align: center;\r\n\twidth: 100%;\r\n    background:  #1f9dff;\r\nbackground: #F4FBF9;\">\r\n                        <tbody>\r\n                            <tr>\r\n                                <td class=\"signature\" style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\npadding: 20px;\">\r\n                                    <p class=\"marginless\" style=\"margin: 0;\"><br>CityBook Team</p>\r\n                                </td>\r\n                            </tr>\r\n                        </tbody>\r\n                    </table>\r\n                </td>\r\n            </tr>\r\n            <!-- Footer -->\r\n            <tr>\r\n                <td class=\"section dummy_row\" style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n    background:  #1f9dff;\r\n\tpadding: 0 20px;\r\npadding-top: 20px !important;\"></td>\r\n            </tr>\r\n            <tr>\r\n                <td style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n\">\r\n                    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"section_content\" style=\"\tfont-size: 15px;\r\n\tline-height: 23px;\r\n\tmax-width: 500px;\r\n\tmin-width: 460px;\r\n\ttext-align: center;\r\n\twidth: 100%;\r\n\tbackground: #fff;\r\n\">\r\n                    </table>\r\n                </td>\r\n            </tr>\r\n            <!-- Legal footer -->\r\n            <tr>\r\n                <td style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n\">\r\n                    <p class=\"footer_legal\" style=\"\tpadding: 20px 0 40px;\r\n\tmargin: 0;\r\n\tfont-size: 12px;\r\n\tcolor: #A5A5A5;\r\n\tline-height: 1.5;\r\n\">\r\nIf you did not initiate this request, please disregard this message.<br><br>\r\n                        2024\r\n                        <br><br>\r\n\r\n                        Get ready for an exclusive copyrighted update straight from the CityBook headquarters.\r\n                    </p>\r\n                </td>\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n\r\n</body>",true);
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
            if (User.IsActivate == false)
            {
                await _signInManager.SignInAsync(User, false);
            }

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
            await _emailService.SendMailAsync(user.Email, "Password Reset",$"<head>\r\n    <meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <style type=\"text/css\">\r\n        body {{\r\n            margin: 0;\r\n            background: #FEFEFE;\r\n            color: #585858;\r\n        }}\r\n\r\n        table {{\r\n            font-size: 15px;\r\n            line-height: 23px;\r\n            max-width: 500px;\r\n            min-width: 460px;\r\n            text-align: center;\r\n        }}\r\n\r\n        .table_inner {{\r\n            min-width: 100% !important;\r\n        }}\r\n\r\n        td {{\r\n            font-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n            vertical-align: top;\r\n        }}\r\n\r\n        .carpool_logo {{\r\n            margin: 30px auto;\r\n        }}\r\n\r\n        .dummy_row {{\r\n            padding-top: 20px !important;\r\n        }}\r\n\r\n        .section,\r\n        .sectionlike {{\r\n            background: #C9F9E9;\r\n        }}\r\n\r\n        .section {{\r\n            padding: 0 20px;\r\n        }}\r\n\r\n        .sectionlike {{\r\n            padding-bottom: 10px;\r\n        }}\r\n\r\n        .section_content {{\r\n            width: 100%;\r\n            background: #fff;\r\n        }}\r\n\r\n        .section_content_padded {{\r\n            padding: 0 35px 40px;\r\n        }}\r\n\r\n        .section_zag {{\r\n            background: #F4FBF9;\r\n        }}\r\n\r\n        .imageless_section {{\r\n            padding-bottom: 20px;\r\n        }}\r\n\r\n        img {{\r\n            display: block;\r\n            margin: 0 auto;\r\n        }}\r\n\r\n        .img_section {{\r\n            width: 100%;\r\n            max-width: 500px;\r\n        }}\r\n\r\n        .img_section_side_table {{\r\n            width: 100% !important;\r\n        }}\r\n\r\n        h1 {{\r\n            font-size: 20px;\r\n            font-weight: 500;\r\n            margin-top: 40px;\r\n            margin-bottom: 0;\r\n        }}\r\n\r\n        .near_title {{\r\n            margin-top: 10px;\r\n        }}\r\n\r\n        .last {{\r\n            margin-bottom: 0;\r\n        }}\r\n\r\n        a {{\r\n            color: #63D3CD;\r\n            font-weight: 500;\r\n            word-break: break-word; /* Footer has long unsubscribe link */\r\n        }}\r\n\r\n        .button {{\r\n            display: block;\r\n            width: 100%;\r\n            max-width: 300px;\r\n            background: #20DA9C;\r\n            border-radius: 8px;\r\n            color: #fff;\r\n            font-size: 18px;\r\n            font-weight: normal; /* Resetting from a */\r\n            padding: 12px 0;\r\n            margin: 30px auto 0;\r\n            text-decoration: none;\r\n        }}\r\n\r\n        small {{\r\n            display: block;\r\n            width: 100%;\r\n            max-width: 330px;\r\n            margin: 14px auto 0;\r\n            font-size: 14px;\r\n        }}\r\n\r\n        .signature {{\r\n            padding: 20px;\r\n        }}\r\n\r\n        .footer,\r\n        .footer_like {{\r\n            background: #1FD99A;\r\n        }}\r\n\r\n        .footer {{\r\n            padding: 0 20px 30px;\r\n        }}\r\n\r\n        .footer_content {{\r\n            width: 100%;\r\n            text-align: center;\r\n            font-size: 12px;\r\n            line-height: initial;\r\n            color: #005750;\r\n        }}\r\n\r\n            .footer_content a {{\r\n                color: #005750;\r\n            }}\r\n\r\n        .footer_item_image {{\r\n            margin: 0 auto 10px;\r\n        }}\r\n\r\n        .footer_item_caption {{\r\n            margin: 0 auto;\r\n        }}\r\n\r\n        .footer_legal {{\r\n            padding: 20px 0 40px;\r\n            margin: 0;\r\n            font-size: 12px;\r\n            color: #A5A5A5;\r\n            line-height: 1.5;\r\n        }}\r\n\r\n        .text_left {{\r\n            text-align: left;\r\n        }}\r\n\r\n        .text_right {{\r\n            text-align: right;\r\n        }}\r\n\r\n        .va {{\r\n            vertical-align: middle;\r\n        }}\r\n\r\n        .stats {{\r\n            min-width: auto !important;\r\n            max-width: 370px;\r\n            margin: 30px auto 0;\r\n        }}\r\n\r\n        .counter {{\r\n            font-size: 22px;\r\n        }}\r\n\r\n        .stats_counter {{\r\n            width: 23%;\r\n        }}\r\n\r\n        .stats_image {{\r\n            width: 18%;\r\n            padding: 0 10px;\r\n        }}\r\n\r\n        .stats_meta {{\r\n            width: 59%;\r\n        }}\r\n\r\n        .stats_spaced {{\r\n            padding-top: 16px;\r\n        }}\r\n\r\n        .walkthrough_spaced {{\r\n            padding-top: 24px;\r\n        }}\r\n\r\n        .walkthrough {{\r\n            max-width: none;\r\n        }}\r\n\r\n        .walkthrough_meta {{\r\n            padding-left: 20px;\r\n        }}\r\n\r\n        .table_checkmark {{\r\n            padding-top: 30px;\r\n        }}\r\n\r\n        .table_checkmark_item {{\r\n            font-size: 15px;\r\n        }}\r\n\r\n        .td_checkmark {{\r\n            width: 24px;\r\n            padding: 7px 12px 0 0;\r\n        }}\r\n\r\n        .padded_bottom {{\r\n            padding-bottom: 40px;\r\n        }}\r\n\r\n        .marginless {{\r\n            margin: 0;\r\n        }}\r\n\r\n        /* Restricting responsive for iOS Mail app only as Inbox/Gmail have render bugs */\r\n        @media only screen and (max-width: 480px) and (-webkit-min-device-pixel-ratio: 2) {{\r\n            table {{\r\n                min-width: auto !important;\r\n            }}\r\n\r\n            .section_content_padded {{\r\n                padding-right: 25px !important;\r\n                padding-left: 25px !important;\r\n            }}\r\n\r\n            .counter {{\r\n                font-size: 18px !important;\r\n            }}\r\n        }}\r\n    </style>\r\n</head>\r\n<body style=\"\tmargin: 0;\r\n\tbackground: #FEFEFE;\r\n\tcolor: #585858;\r\n\">\r\n    <!-- Preivew text -->\r\n    <span class=\"preheader\" style=\"display: none !important; visibility: hidden; opacity: 0; color: transparent; height: 0; width: 0;border-collapse: collapse;border: 0px;\"></span>\r\n    <!-- Carpool logo -->\r\n    <table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"\tfont-size: 15px;\r\n\tline-height: 23px;\r\n\tmax-width: 500px;\r\n\tmin-width: 460px;\r\n\ttext-align: center;\r\n\">\r\n        <tbody>\r\n            <tr>\r\n                <td style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n\">\r\n                    <img src=https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTlKkGB_kTI7AM6iay-j-BWX49yHbnDx5dt_A&usqp=CAU class=\"carpool_logo\" width=\"300\" height=\"300\" style=\"\tdisplay: block;\r\n\tmargin: 0 auto;\r\nmargin: 30px auto;border-radius:50%;object-fit:cover\">\r\n                </td>\r\n            </tr>\r\n            <!-- Header -->\r\n            <tr>\r\n                <td class=\"sectionlike imageless_section\" style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n  background:  #1f9dff;\r\n  padding-bottom: 10px;\r\npadding-bottom: 20px;\"></td>\r\n            </tr>\r\n            <!-- Content -->\r\n            <tr>\r\n                <td class=\"section\" style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n    background:  #1f9dff;\r\n\tpadding: 0 20px;\r\n\">\r\n                    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"section_content\" style=\"\tfont-size: 15px;\r\n\tline-height: 23px;\r\n\tmax-width: 500px;\r\n\tmin-width: 460px;\r\n\ttext-align: center;\r\n\twidth: 100%;\r\n    background:  white;\r\n\">\r\n                        <tbody>\r\n                            <tr>\r\n                                <td class=\"section_content_padded\" style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\npadding: 0 35px 40px;\">\r\n                                    <h1 style=\"\tfont-size: 20px;\r\n\tfont-weight: 500;\r\n\tmargin-top: 40px;\r\n\tmargin-bottom: 0;\r\n\">\r\n                                        Dear {user.Name},  We're excited to inform you that your CityBook account has been successfully created!Thank you for joining CityBook Administration. We look forward to seeing you make a positive impact within our community.\r\n                                    </h1>\r\n                                    <p class=\"near_title last\" style=\"margin-top: 10px;margin-bottom: 0;\">Please confirm the email.</p>\r\n                                    <a href=\"{confirmationLink}\" style=\"\tdisplay: block;\r\n\twidth: 100%;\r\n\tmax-width: 300px;\r\n\tbackground:  #061e40 ;\r\n\tborder-radius: 8px;\r\n\tcolor: #fff;\r\n\tfont-size: 18px;\r\n\tpadding: 12px 0;\r\n\tmargin: 30px auto 0;\r\n\ttext-decoration: none;\r\n\" target=\"_blank\">Confirm Email</a>\r\n                                    <small style=\"\tdisplay: block;\r\n\twidth: 100%;\r\n\tmax-width: 330px;\r\n\tmargin: 14px auto 0;\r\n\tfont-size: 14px;\r\n\"></small>\r\n                                </td>\r\n                            </tr>\r\n                        </tbody>\r\n                    </table>\r\n                </td>\r\n            </tr>\r\n            <!-- Signature -->\r\n            <tr>\r\n                <td class=\"section\" style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n    background:  #1f9dff;\r\n\tpadding: 0 20px;\r\n\">\r\n                    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"section_content section_zag\" style=\"\tfont-size: 15px;\r\n\tline-height: 23px;\r\n\tmax-width: 500px;\r\n\tmin-width: 460px;\r\n\ttext-align: center;\r\n\twidth: 100%;\r\n    background:  #1f9dff;\r\nbackground: #F4FBF9;\">\r\n                        <tbody>\r\n                            <tr>\r\n                                <td class=\"signature\" style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\npadding: 20px;\">\r\n                                    <p class=\"marginless\" style=\"margin: 0;\"><br>CityBook Team</p>\r\n                                </td>\r\n                            </tr>\r\n                        </tbody>\r\n                    </table>\r\n                </td>\r\n            </tr>\r\n            <!-- Footer -->\r\n            <tr>\r\n                <td class=\"section dummy_row\" style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n    background:  #1f9dff;\r\n\tpadding: 0 20px;\r\npadding-top: 20px !important;\"></td>\r\n            </tr>\r\n            <tr>\r\n                <td style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n\">\r\n                    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"section_content\" style=\"\tfont-size: 15px;\r\n\tline-height: 23px;\r\n\tmax-width: 500px;\r\n\tmin-width: 460px;\r\n\ttext-align: center;\r\n\twidth: 100%;\r\n\tbackground: #fff;\r\n\">\r\n                    </table>\r\n                </td>\r\n            </tr>\r\n            <!-- Legal footer -->\r\n            <tr>\r\n                <td style=\"\tfont-family: -apple-system, BlinkMacSystemFont, Roboto, sans-serif;\r\n\tvertical-align: top;\r\n    border: none !important;\r\n\">\r\n                    <p class=\"footer_legal\" style=\"\tpadding: 20px 0 40px;\r\n\tmargin: 0;\r\n\tfont-size: 12px;\r\n\tcolor: #A5A5A5;\r\n\tline-height: 1.5;\r\n\">\r\nIf you did not initiate this request, please disregard this message.<br><br>\r\n                        2024\r\n                        <br><br>\r\n\r\n                        Get ready for an exclusive copyrighted update straight from the CityBook headquarters.\r\n                    </p>\r\n                </td>\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n\r\n</body>",true );

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
