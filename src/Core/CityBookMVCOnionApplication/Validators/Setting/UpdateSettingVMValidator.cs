using CityBookMVCOnionApplication.ViewModels.Setting;
using FluentValidation;

namespace CityBookMVCOnionApplication.Validators.Setting
{
    public class UpdateSettingVMValidator : AbstractValidator<UpdateSettingVM>
    {
        public UpdateSettingVMValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Value is required")
                .Length(1, 1500).WithMessage("Name max characters is 1-1500");
        }
    }
}
