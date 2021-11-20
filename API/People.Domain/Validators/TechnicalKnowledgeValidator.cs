using FluentValidation;
using People.Domain.Entities;

namespace People.Domain.Validators
{
    public class TechnicalKnowledgeValidator : AbstractValidator<TechnicalRatingEntity>
    {
        public TechnicalKnowledgeValidator()
        {
            RuleFor(x => x.Technology).SetValidator(new TechnologyValidator());
            RuleFor(x => x.Rating).LessThan(0).GreaterThan(5).WithMessage("Avaliação não pode ser menor que 0 ou maior que 5");
            RuleFor(x => x.RatingDate).NotEmpty().WithMessage("Data da avaliação não pode ser vazio");
        }
    }
}
