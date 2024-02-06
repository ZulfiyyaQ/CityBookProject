using CityBookMVCOnionApplication.ViewModels.Comment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBookMVCOnionApplication.Validators.Comment
{
    public class UpdateCommentVMValidator : AbstractValidator<UpdateCommentVM>
    {
        public UpdateCommentVMValidator()
        {
            RuleFor(x => x.Text)
           .NotEmpty().WithMessage("Comment is required")
           .Length(1, 1500).WithMessage("Comment max characters is 1-1500")
           .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Comment can only contain letters, numbers, spaces, double quotes, commas, and periods.");
        }
    }
}
