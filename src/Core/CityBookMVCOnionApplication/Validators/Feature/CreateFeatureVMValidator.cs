using CityBookMVCOnionApplication.ViewModels;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Feature
{
    public class CreateFeatureVMValidator : AbstractValidator<CreateFeatureVM>
    {
        public CreateFeatureVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 50).WithMessage("Name max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Name can only contain letters, numbers, and spaces");
            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("Icon is required")
                .Length(2, 50).WithMessage("Icon max characters is 2-50");
                
        }
    }
    
}
