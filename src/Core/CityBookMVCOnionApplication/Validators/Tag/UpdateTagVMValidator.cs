using CityBookMVCOnionApplication.ViewModels.Tag;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Tag
{
    public class UpdateTagVMValidator : AbstractValidator<UpdateTagVM>
    {
        public UpdateTagVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(10, 250).WithMessage("Name max characters is 10-250")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Name can only contain letters, numbers, spaces, double quotes, commas, and periods.");

        }
    }
}
