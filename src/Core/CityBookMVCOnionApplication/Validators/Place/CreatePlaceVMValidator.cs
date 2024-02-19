using CityBookMVCOnionApplication.ViewModels;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Place
{
    public class CreatePlaceVMValidator : AbstractValidator<CreatePlaceVM>
    {
        public CreatePlaceVMValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("CategoryId must be greater than 0");

            RuleFor(x => x.FeatureIds).NotNull().WithMessage("Feature was not be emty");
            RuleForEach(x => x.FeatureIds).GreaterThan(0).WithMessage("Feature must be greater than 0");
            RuleFor(x => x.TagIds).NotNull().WithMessage("Tag was not be emty");
            RuleForEach(x => x.TagIds).GreaterThan(0).WithMessage("Tag must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(3, 250).WithMessage("Name max characters is 10-250")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Name can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .Length(5, 250).WithMessage("Address max characters is 25-250");
                

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(15, 1000).WithMessage("Description max characters is 25-1000");
                

            RuleFor(x => x.Photos)
                .NotEmpty().WithMessage("Image is required");
        }
    }
}
