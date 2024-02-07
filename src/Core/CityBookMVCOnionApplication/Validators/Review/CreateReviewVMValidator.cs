using CityBookMVCOnionApplication.ViewModels.Blog;
using CityBookMVCOnionApplication.ViewModels.Review;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Review
{
    public class CreateReviewVMValidator : AbstractValidator<CreateReviewVM>
    {
        public CreateReviewVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 50).WithMessage("Name max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Name can only contain letters, numbers, and spaces");
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required")
                .Length(1, 3000).WithMessage("Text max characters is 1-3000")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Text can only contain letters, numbers, spaces, double quotes, commas, and periods.");
        }
        
    }
}
