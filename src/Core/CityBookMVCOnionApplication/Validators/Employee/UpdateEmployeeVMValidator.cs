using CityBookMVCOnionApplication.ViewModels.Employee;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Employee
{
    public class UpdateEmployeeVMValidator : AbstractValidator<UpdateEmployeeVM>
    {
        public UpdateEmployeeVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 50).WithMessage("Name max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Name can only contain letters, numbers, and spaces");
            RuleFor(x => x.Surname)
               .NotEmpty().WithMessage("Surname is required")
               .Length(2, 50).WithMessage("Surname max characters is 2-50")
               .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Surname can only contain letters, numbers, and spaces");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(2, 1500).WithMessage("Description max characters is 2-50")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Biography can only contain letters, numbers, spaces, double quotes, commas, and periods.");

            RuleFor(x => x.Face)
                .NotEmpty().WithMessage("Facebook is required")
                .Matches(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format");

            RuleFor(x => x.Tvit)
                .NotEmpty().WithMessage("Twitter is required")
                .Matches(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format");
            RuleFor(x => x.Link)
                .NotEmpty().WithMessage("Linked-In is required")
                .Matches(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format");

           


        }
    }
}
