using FluentValidation;
using People.Domain.Entities;

namespace People.Domain.Validators
{
    public class PeopleValidator : AbstractValidator<PeopleEntity>
    {
        public PeopleValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Nome não pode ser vazio");
            RuleFor(x => x.Birthdate).NotEmpty().WithMessage("Data de nascimento não pode ser vazia");
            RuleFor(x => x.EmailCorporate).NotEmpty().WithMessage("Email Corporativo não pode ser vazio");
            RuleFor(x => x.EmailCorporate).MinimumLength(10).WithMessage("Email Corporativo menor que 10 caracteres");
            RuleForEach(x => x.SocialNetworks).SetValidator(new SocialNetworkValidator());
            RuleForEach(x => x.TechnicalKnowledge).SetValidator(new TechnicalKnowledgeValidator());
            RuleForEach(x => x.Clients).SetValidator(new ClientValidator());
        }
    }
}
