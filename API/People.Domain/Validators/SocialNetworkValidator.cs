using FluentValidation;
using People.Domain.Entities;

namespace People.Domain.Validators
{
    public class SocialNetworkValidator : AbstractValidator<SocialNetworkEntity>
    {
        public SocialNetworkValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome não pode ser vazio");
            RuleFor(x => x.Link).NotEmpty().WithMessage("Link não pode ser vazio");
            RuleFor(x => x.Icon).NotEmpty().WithMessage("Icone não pode ser vazio");
        }
    }
}
