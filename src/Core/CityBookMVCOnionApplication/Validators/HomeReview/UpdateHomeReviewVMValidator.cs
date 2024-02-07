using CityBookMVCOnionApplication.ViewModels.HomeReview;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.HomeReview
{
    public class UpdateHomeReviewVMValidator : AbstractValidator<UpdateHomeReviewVM>
    {
        public UpdateHomeReviewVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 50).WithMessage("Name max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Name can only contain letters, numbers, and spaces");
            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required")
                .Length(2, 50).WithMessage("Surname max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Surname can only contain letters, numbers, and spaces");
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required")
                .Length(1, 3000).WithMessage("Text max characters is 1-3000")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Text can only contain letters, numbers, spaces, double quotes, commas, and periods.");
        }

    }
    
}
