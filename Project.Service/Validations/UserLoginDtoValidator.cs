using FluentValidation;
using OMS.Core.DTOs.Users;

namespace OMS.Service.Validations;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull().WithMessage("UserName is required")
            .NotEmpty().WithMessage("UserName is required");
        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password is required")
            .NotEmpty().WithMessage("Password is required");
    }
}