using CityBookMVCOnionApplication.ViewModels.Category;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Category
{
    public class CreateCategoryVMValidator : AbstractValidator<CreateCategoryVM>
    {
        public CreateCategoryVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 50).WithMessage("Name max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Name can only contain letters, numbers, and spaces");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(2, 1500).WithMessage("Description max characters is 2-50")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Biography can only contain letters, numbers, spaces, double quotes, commas, and periods.");
            RuleFor(x => x.Photo)
              .NotEmpty().WithMessage("Images is required");
        }
    }
}
