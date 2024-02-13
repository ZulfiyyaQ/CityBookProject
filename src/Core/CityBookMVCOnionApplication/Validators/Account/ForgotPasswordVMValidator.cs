using CityBookMVCOnionApplication.ViewModels;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Account
{
    public class ForgotPasswordVMValidator : AbstractValidator<ForgotPasswordVM>
    {
        public ForgotPasswordVMValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(8, 25).WithMessage("Password max characters is 8-25")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Password can only contain letters, numbers, and spaces");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required")
                .Length(8, 25).WithMessage("New Password max characters is 8-25")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("New Password can only contain letters, numbers, and spaces");

            RuleFor(x => x.NewConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("New Confirm Password must match New Password")
                .Length(8, 25).WithMessage("New Confirm Password max characters is 8-25")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("New Confirm Password can only contain letters, numbers, and spaces");
        }
    }
    
}
