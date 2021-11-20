using FluentValidation;
using People.Domain.Entities;

namespace People.Domain.Validators
{
    public class TechnologyValidator : AbstractValidator<TechnologyEntity>
    {
        public TechnologyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome não pode ser vazio");
            RuleFor(x => x.Link).NotEmpty().WithMessage("Link não pode ser vazio");
            RuleFor(x => x.Logo).NotEmpty().WithMessage("Logo não pode ser vazio");
        }
    }
}
