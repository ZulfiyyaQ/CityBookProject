using CityBookMVCOnionApplication.ViewModels;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Service
{
    public class CreateServiceVMValidator:AbstractValidator<CreateServiceVM>
    {
        public CreateServiceVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(10, 250).WithMessage("Name max characters is 10-250")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Name can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("Icon is required")
                .Length(5, 1500).WithMessage("Icon max characters is 5-1500");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(10, 1500).WithMessage("Description max characters is 10-1500");
                

        }
    }
}
