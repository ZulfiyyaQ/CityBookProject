using CityBookMVCOnionApplication.ViewModels;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Place
{
    public class UpdatePlaceVMValidator : AbstractValidator<UpdatePlaceVM>
    {
        public UpdatePlaceVMValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("CategoryId must be greater than 0");

            RuleFor(x => x.FeatureIds).NotNull().WithMessage("Feature was not be emty");
            RuleForEach(x => x.FeatureIds).GreaterThan(0).WithMessage("Feature must be greater than 0");

            RuleFor(x => x.ImageIds).NotNull().WithMessage("ImageIds was not be emty");
            RuleForEach(x => x.ImageIds).GreaterThan(0).WithMessage("ImageIds must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(10, 250).WithMessage("Name max characters is 10-250")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Name can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .Length(25, 250).WithMessage("Address max characters is 25-250")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Address can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(25, 250).WithMessage("Description max characters is 25-250")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Description can only contain letters, numbers, spaces, double quotes, commas, and periods.");

        }
    }
}
