using CityBookMVCOnionApplication.ViewModels.Reply;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Reply
{
    public class UpdateReplyVMValidator : AbstractValidator<UpdateReplyVM>
    {
        public UpdateReplyVMValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required")
                .Length(10, 1500).WithMessage("Text max characters is 10-1500")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Text can only contain letters, numbers, spaces, double quotes, commas, and periods.");

        }
    }
}
