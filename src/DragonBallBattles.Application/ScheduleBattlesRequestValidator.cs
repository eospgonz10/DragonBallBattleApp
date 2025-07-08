using DragonBallBattles.Application.DTOs;
using FluentValidation;

namespace DragonBallBattles.Application.Validations;

public class ScheduleBattlesRequestValidator : AbstractValidator<ScheduleBattlesRequest>
{
    public ScheduleBattlesRequestValidator()
    {
        RuleFor(x => x.NumeroParticipantes)
            .GreaterThan(1).WithMessage("Debe haber al menos 2 participantes.")
            .Must(x => x % 2 == 0).WithMessage("El número de participantes debe ser par.")
            .LessThanOrEqualTo(16).WithMessage("El número máximo de participantes es 16.");
    }
}
