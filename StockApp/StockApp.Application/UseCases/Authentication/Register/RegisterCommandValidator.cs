using FluentValidation;

namespace StockApp.Application.UseCases.Authentication.Register;

using FluentValidation;

public class RegisterCommandValidator : AbstractValidator<Command>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("Formato de e-mail inválido.");
        
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("O username é obrigatório.")
            .MinimumLength(3).WithMessage("O username deve ter pelo menos 3 caracteres.");
        
        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("A senha não pode estar vazia.")
            .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
            .MaximumLength(16).WithMessage("A senha não pode ter mais de 16 caracteres.")
            .Matches(@"[A-Z]+").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches(@"[a-z]+").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches(@"[0-9]+").WithMessage("A senha deve conter pelo menos um número.")
            .Matches(@"[\!\?\*\.]+").WithMessage("A senha deve conter pelo menos um dos seguintes caracteres: ! ? * .");

    }
}
