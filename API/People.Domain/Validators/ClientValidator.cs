using FluentValidation;
using People.Domain.Entities;

namespace People.Domain.Validators
{
    public class ClientValidator : AbstractValidator<ClientEntity>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome não pode ser vazio");
            RuleFor(x => x.DateStart).NotEmpty().WithMessage("Data de inicio não pode ser vazia");
        }
    }
}
