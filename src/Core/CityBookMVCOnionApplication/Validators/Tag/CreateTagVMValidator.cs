﻿using CityBookMVCOnionApplication.ViewModels;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Tag
{
    public class CreateTagVMValidator : AbstractValidator<CreateTagVM>
    {
        public CreateTagVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(3, 250).WithMessage("Name max characters is 3-250")
                .Matches(@"^[A-Za-z0-9\s"",.]+$").WithMessage("Name can only contain letters, numbers, spaces, double quotes, commas, and periods.");

        }
    }
}
