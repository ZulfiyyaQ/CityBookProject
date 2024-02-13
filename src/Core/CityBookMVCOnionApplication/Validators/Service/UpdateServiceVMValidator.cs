using CityBookMVCOnionApplication.ViewModels;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Service
{
    public class UpdateServiceVMValidator : AbstractValidator<UpdateServiceVM>
    {
        public UpdateServiceVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(10, 250).WithMessage("Name max characters is 10-250")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Name can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("Icon is required")
                .Length(10, 1500).WithMessage("Icon max characters is 10-1500")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Icon can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(10, 1500).WithMessage("Description max characters is 10-1500")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Description can only contain letters, numbers, spaces, double quotes, commas, and periods.");

        }
    }
}
